using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

public partial class Checkout : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public int intCartItemCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindOrderSummary();
        }
    }

    private void BindOrderSummary()
    {
        List<AddtoCart> products = AddtoCart.GetAllcartproducts(conT);

        if (products.Count == 0)
        {
            Response.Redirect("/cart.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
            return;
        }

        // Block ONLY if ALL items are Price On Request
        bool allPriceOnRequest = products.All(p => p.RetailPrice <= 0);
        if (allPriceOnRequest)
        {
            Response.Redirect("/cart.aspx?msg=priceOnRequest", false);
            Context.ApplicationInstance.CompleteRequest();
            return;
        }

        // Filter — only process items that have a price
        var pricedProducts = products.Where(p => p.RetailPrice > 0).ToList();

        int totalQty = 0;
        decimal inclTax = 0;

        foreach (var item in pricedProducts)
        {
            totalQty += item.Qty;
            inclTax += item.RetailPrice * item.Qty;
        }

        intCartItemCount = totalQty;

        const decimal TAX_RATE = 0.18m;
        decimal exclTax = inclTax / (1 + TAX_RATE);
        decimal taxAmt = inclTax - exclTax;

        var india = new System.Globalization.CultureInfo("en-IN");

        lblExclTax.Text = "<span class='text-rupee'>₹</span>" + exclTax.ToString("N2", india);
        lblTaxAmt.Text = "<span class='text-rupee'>₹</span>" + taxAmt.ToString("N2", india);
        lblSubtotal.Text = "<span class='text-rupee'>₹</span>" + inclTax.ToString("N2", india);
        lblTotal.Text = "<span class='text-rupee'>₹</span>" + inclTax.ToString("N2", india);
    }

    protected void btnPlaceOrder_Click(object sender, EventArgs e)
    {
        if (!chkTerms.Checked)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "terms",
                "tgShowSnackbar('Please accept the Terms and Conditions and Privacy Policy.');", true);
            return;
        }

        try
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string mobile = txtMobile.Text.Trim();
            string email = txtEmail.Text.Trim();
            string address = txtAddress.Text.Trim();
            string pincode = txtPincode.Text.Trim();
            string city = txtCity.Text.Trim();
            string state = txtState.Text.Trim();
            string country = txtCountry.Text.Trim();
            string companyName = txtCompanyName.Text.Trim();
            string gstNumber = txtGSTNumber.Text.Trim();
            string payType = rbFullPayment.Checked ? "Full" : "Partial";

            List<AddtoCart> products = AddtoCart.GetAllcartproducts(conT);
            var pricedProducts = products.Where(p => p.RetailPrice > 0).ToList();
            if (products.Count == 0)
            {
                Response.Redirect("/cart.aspx", false);
                return;
            }

            const decimal TAX_RATE = 0.18m;
            decimal grandTotal = 0;
            decimal exclTaxSum = 0;
            decimal taxSum = 0;

            foreach (var item in pricedProducts)
            {
                if (item.RetailPrice > 0)
                {
                    decimal lineTotal = item.RetailPrice * item.Qty;
                    grandTotal += lineTotal;
                    decimal excl = lineTotal / (1 + TAX_RATE);
                    exclTaxSum += excl;
                    taxSum += lineTotal - excl;
                }
            }

            decimal payableNow = payType == "Partial" && grandTotal > 0
                ? Math.Round(grandTotal * 0.20m, 2)
                : grandTotal;

            decimal codAmount = payType == "Partial" ? Math.Round(grandTotal * 0.80m, 2) : 0;

            string ipAddress = CommonModel.IPAddress();
            DateTime orderedOn = TimeStamps.UTCTime();
            string orderGuid = Guid.NewGuid().ToString();

            // ── Insert into orders table ──────────────────────────────────
            int orderId = InsertOrder(
                orderGuid, firstName, lastName, email, mobile,
                address, city, state, pincode, country,
                companyName, gstNumber, payType,
                grandTotal, exclTaxSum, taxSum, payableNow, codAmount,
                ipAddress, orderedOn);

            if (orderId <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "tgShowSnackbar('Something went wrong. Please try again.');", true);
                return;
            }

            // ── Insert into orders_item table ─────────────────────────────
            foreach (var item in pricedProducts)
            {
                InsertOrderItem(orderId, orderGuid, item, TAX_RATE, orderedOn, ipAddress);
            }

            Response.Redirect("paynow.aspx?order=" + orderGuid, false);
            Context.ApplicationInstance.CompleteRequest();

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "btnPlaceOrder_Click", ex.Message);

            ScriptManager.RegisterStartupScript(this, GetType(), "err",
                "tgShowSnackbar('Something went wrong. Please try again.');", true);
        }
    }

    private int InsertOrder(
        string orderGuid, string firstName, string lastName,
        string email, string mobile,
        string address, string city, string state, string pincode, string country,
        string companyName, string gstNumber, string payType,
        decimal grandTotal, decimal exclTax, decimal taxAmt,
        decimal payableNow, decimal codAmount,
        string ipAddress, DateTime orderedOn)
    {
        try
        {
            string sql = @"
INSERT INTO orders
    (OrderGuid, FirstName, LastName, Email, Mobile,
     Address, City, State, Pincode, Country,
     CompanyName, GSTNumber, PaymentType,
     GrandTotal, ExclTax, TaxAmount, PayableNow, CODAmount,
     OrderStatus, PaymentStatus,
     AddedOn, AddedIp)
VALUES
    (@OrderGuid, @FirstName, @LastName, @Email, @Mobile,
     @Address, @City, @State, @Pincode, @Country,
     @CompanyName, @GSTNumber, @PaymentType,
     @GrandTotal, @ExclTax, @TaxAmount, @PayableNow, @CODAmount,
     'Pending', 'Pending',
     @AddedOn, @AddedIp);
SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(sql, conT))
            {
                cmd.Parameters.AddWithValue("@OrderGuid", orderGuid);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Mobile", mobile);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@State", state);
                cmd.Parameters.AddWithValue("@Pincode", pincode);
                cmd.Parameters.AddWithValue("@Country", country);
                cmd.Parameters.AddWithValue("@CompanyName", companyName);
                cmd.Parameters.AddWithValue("@GSTNumber", gstNumber);
                cmd.Parameters.AddWithValue("@PaymentType", payType);
                cmd.Parameters.AddWithValue("@GrandTotal", grandTotal);
                cmd.Parameters.AddWithValue("@ExclTax", exclTax);
                cmd.Parameters.AddWithValue("@TaxAmount", taxAmt);
                cmd.Parameters.AddWithValue("@PayableNow", payableNow);
                cmd.Parameters.AddWithValue("@CODAmount", codAmount);
                cmd.Parameters.AddWithValue("@AddedOn", orderedOn);
                cmd.Parameters.AddWithValue("@AddedIp", ipAddress);

                conT.Open();
                object result = cmd.ExecuteScalar();
                conT.Close();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }
        catch (Exception ex)
        {
            if (conT.State == ConnectionState.Open) conT.Close();
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "InsertOrder", ex.Message);
            return 0;
        }
    }

    private void InsertOrderItem(
        int orderId, string orderGuid,
        AddtoCart item, decimal taxRate,
        DateTime orderedOn, string ipAddress)
    {
        try
        {
            decimal lineTotal = item.RetailPrice * item.Qty;
            decimal exclLine = item.RetailPrice > 0 ? lineTotal / (1 + taxRate) : 0;
            decimal taxLine = lineTotal - exclLine;

            string sql = @"
INSERT INTO orders_item
    (OrderId, OrderGuid, ProductId, ProductName, ProductImage, ProductUrl,
     UnitPrice, Qty, LineTotal, ExclTax, TaxAmount,
     AddedOn, AddedIp)
VALUES
    (@OrderId, @OrderGuid, @ProductId, @ProductName, @ProductImage, @ProductUrl,
     @UnitPrice, @Qty, @LineTotal, @ExclTax, @TaxAmount,
     @AddedOn, @AddedIp)";

            using (SqlCommand cmd = new SqlCommand(sql, conT))
            {
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                cmd.Parameters.AddWithValue("@OrderGuid", orderGuid);
                cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", item.ProductName);
                cmd.Parameters.AddWithValue("@ProductImage", item.SmallImage ?? "");
                cmd.Parameters.AddWithValue("@ProductUrl", item.ProductUrl ?? "");
                cmd.Parameters.AddWithValue("@UnitPrice", item.RetailPrice);
                cmd.Parameters.AddWithValue("@Qty", item.Qty);
                cmd.Parameters.AddWithValue("@LineTotal", lineTotal);
                cmd.Parameters.AddWithValue("@ExclTax", exclLine);
                cmd.Parameters.AddWithValue("@TaxAmount", taxLine);
                cmd.Parameters.AddWithValue("@AddedOn", orderedOn);
                cmd.Parameters.AddWithValue("@AddedIp", ipAddress);

                conT.Open();
                cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            if (conT.State == ConnectionState.Open) conT.Close();
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "InsertOrderItem", ex.Message);
        }
    }
}