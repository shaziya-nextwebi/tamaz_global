using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web;
using Razorpay.Api;

public partial class PayNow : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strBuyerName = "";
    public string strBuyerEmail = "";
    public string strBuyerMobile = "";
    public string strDeliveryAddress = "";
    public string strCompanyName = "";
    public string strGSTNumber = "";
    public string strPaymentTypeBadge = "Full Payment";
    public string strRazorOrderId = "";
    public string strRazorKey = "";
    public string strBuyerAmountPaise = "0";
    public string strPayableAmountDisplay = "0";
    public string strProductListHtml = "";
    public string strExclTax = "";
    public string strTaxAmount = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            string orderGuid = Request.QueryString["order"];

            if (string.IsNullOrEmpty(orderGuid))
            {
               
                orderGuid = Session["checkout_OrderGuid"] != null
                    ? Session["checkout_OrderGuid"].ToString()
                    : null;
            }

            if (string.IsNullOrEmpty(orderGuid))
            {
                Response.Redirect("/checkout.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

         
            if (!IsValidOrder(orderGuid))
            {
                Response.Redirect("/checkout.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            LoadOrderFromDB(orderGuid);
            BindProductList(orderGuid);
            CreateRazorpayOrder(orderGuid);
        }
    }

    private bool IsValidOrder(string orderGuid)
    {
        try
        {
            string sql = "SELECT COUNT(1) FROM orders WHERE OrderGuid = @g AND PaymentStatus = 'Pending'";
            using (SqlCommand cmd = new SqlCommand(sql, conT))
            {
                cmd.Parameters.AddWithValue("@g", orderGuid);
                conT.Open();
                int count = (int)cmd.ExecuteScalar();
                conT.Close();
                return count > 0;
            }
        }
        catch { return false; }
    }

    private void LoadOrderFromDB(string orderGuid)
    {
        try
        {
            string sql = @"SELECT * FROM orders WHERE OrderGuid = @OrderGuid";
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(sql, conT))
            {
                cmd.Parameters.AddWithValue("@OrderGuid", orderGuid);
                conT.Open();
                new SqlDataAdapter(cmd).Fill(dt);
                conT.Close();
            }

            if (dt.Rows.Count == 0)
            {
                Response.Redirect("/checkout.aspx", false);
                return;
            }

            DataRow r = dt.Rows[0];
            strBuyerName = r["FirstName"] + " " + r["LastName"];
            strBuyerEmail = r["Email"].ToString();
            strBuyerMobile = r["Mobile"].ToString();
            strCompanyName = r["CompanyName"].ToString();
            strGSTNumber = r["GSTNumber"].ToString();

            strDeliveryAddress =
                r["Address"] + ", " +
                r["City"] + " - " + r["Pincode"] + ", " +
                r["State"] + ", " + r["Country"];

            string payType = r["PaymentType"].ToString();
            strPaymentTypeBadge = payType == "Partial"
                ? "Partial Payment (20% now, rest COD)"
                : "Full Payment";

            strRazorKey = ConfigurationManager.AppSettings["razorid"];
        }
        catch (Exception ex)
        {
            if (conT.State == ConnectionState.Open) conT.Close();
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "LoadOrderFromDB", ex.Message);
        }
    }

    private void BindProductList(string orderGuid)
    {
        try
        {
            // Totals come from orders table (already calculated at checkout)
            string sqlOrder = "SELECT GrandTotal, ExclTax, TaxAmount, PayableNow, PaymentType FROM orders WHERE OrderGuid = @g";
            decimal grandTotal = 0, payableNow = 0, exclTax = 0, taxAmt = 0;
            string payType = "Full";

            using (SqlCommand cmd = new SqlCommand(sqlOrder, conT))
            {
                cmd.Parameters.AddWithValue("@g", orderGuid);
                conT.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        grandTotal = Convert.ToDecimal(dr["GrandTotal"]);
                        exclTax = Convert.ToDecimal(dr["ExclTax"]);
                        taxAmt = Convert.ToDecimal(dr["TaxAmount"]);
                        payableNow = Convert.ToDecimal(dr["PayableNow"]);
                        payType = dr["PaymentType"].ToString();
                    }
                }
                conT.Close();
            }

            var india = new System.Globalization.CultureInfo("en-IN");
            string contactUsHtml = "<a href='/contact-us.aspx' style='font-weight:600;color:#000;text-decoration:none;'>Price On Request</a>";

            litSubtotal.Text = exclTax <= 0 ? contactUsHtml : "<span class='text-rupee'>₹</span>" + exclTax.ToString("N2", india);
            strTaxAmount = taxAmt <= 0 ? "0.00" : taxAmt.ToString("N2", india);
            litTotal.Text = grandTotal <= 0 ? contactUsHtml : "<span class='text-rupee'>₹</span>" + grandTotal.ToString("N2", india);
            litPayableAmount.Text = payableNow <= 0 ? contactUsHtml : "<span class='text-rupee'>₹</span>" + payableNow.ToString("N2", india);

            strPayableAmountDisplay = payableNow.ToString("N2", india);
            strBuyerAmountPaise = ((long)(payableNow * 100)).ToString();

            // ── Items from orders_item ────────────────────────────────────
            string sqlItems = @"SELECT ProductName, ProductImage, UnitPrice, Qty FROM orders_item WHERE OrderGuid = @g";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            using (SqlCommand cmd = new SqlCommand(sqlItems, conT))
            {
                cmd.Parameters.AddWithValue("@g", orderGuid);
                conT.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string name = dr["ProductName"].ToString();
                        string rawImg = dr["ProductImage"].ToString();
                        decimal price = Convert.ToDecimal(dr["UnitPrice"]);
                        int qty = Convert.ToInt32(dr["Qty"]);

                        string imgUrl = BuildImageUrl(rawImg);

                        string priceDisplay = price > 0
                            ? "<span class='text-rupee'>₹</span> " + price.ToString("N2", india)
                            : "Price On Request";

                        sb.Append("<div class='product-mini-item'>");
                        if (!string.IsNullOrEmpty(imgUrl))
                            sb.AppendFormat(
                                "<img src='{0}' alt='{1}' onerror=\"this.onerror=null;this.src='/assests/Images/logo.png';\" />",
                                imgUrl, HttpUtility.HtmlEncode(name));
                        sb.Append("<div>");
                        sb.AppendFormat("<div class='name'>{0}</div>", HttpUtility.HtmlEncode(name));
                        sb.AppendFormat("<div class='meta'>Qty: {0} &times; {1}</div>", qty, priceDisplay);
                        sb.Append("</div></div>");
                    }
                }
                conT.Close();
            }
            strProductListHtml = sb.ToString();
        }
        catch (Exception ex)
        {
            if (conT.State == ConnectionState.Open) conT.Close();
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "BindProductList", ex.Message);
        }
    }
    private string BuildImageUrl(string rawImg)
    {
        if (string.IsNullOrEmpty(rawImg)) return "";

        string domain = ConfigurationManager.AppSettings["domain"];

        if (rawImg.StartsWith("http"))
            return rawImg;

        return new Uri(new Uri(domain), rawImg.TrimStart('~', '/')).ToString();
    }
    private void CreateRazorpayOrder(string orderGuid)
    {
        try
        {
            long amountPaise = Convert.ToInt64(strBuyerAmountPaise);
            if (amountPaise <= 0) return;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            RazorpayClient client = new RazorpayClient(
                ConfigurationManager.AppSettings["razorid"],
                ConfigurationManager.AppSettings["razorsecret"]);

            var input = new Dictionary<string, object>
            {
                { "amount",          amountPaise.ToString() },
                { "currency",        "INR" },
                { "receipt",         "TG_" + DateTime.UtcNow.Ticks },
                { "payment_capture", 1 }
            };

            Razorpay.Api.Order order = client.Order.Create(input);
            strRazorOrderId = order["id"].ToString();

            // Persist Razorpay order ID in DB
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE orders SET RazorOrderId = @rid WHERE OrderGuid = @guid", conT))
            {
                cmd.Parameters.AddWithValue("@rid", strRazorOrderId);
                cmd.Parameters.AddWithValue("@guid", orderGuid);
                conT.Open();
                cmd.ExecuteNonQuery();
                conT.Close();
            }

            // Keep in Session only as guard for callback verification
            Session["checkout_RazorOrderId"] = strRazorOrderId;
        }
        catch (Exception ex)
        {
            if (conT.State == ConnectionState.Open) conT.Close();
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "CreateRazorpayOrder", ex.Message);
        }
    }
}