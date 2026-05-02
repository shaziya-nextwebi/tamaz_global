using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class Admin_report : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strReport = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindReport();
    }
    private void BindReport()
    {
        try
        {
            strReport = "";
           

            string sqlOrders = @"
SELECT
    o.Id, o.OrderGuid, o.RazorOrderId, o.RazorPaymentId,
    o.FirstName, o.LastName, o.Email, o.Mobile,
    o.Address, o.City, o.State, o.Pincode, o.Country,
    o.CompanyName, o.GSTNumber,
    o.PaymentType, o.GrandTotal, o.ExclTax, o.TaxAmount,
    o.PayableNow, o.CODAmount,
    o.OrderStatus, o.PaymentStatus, o.PaidOn, o.AddedOn, o.AddedIp,
 o.CourierName,
    o.TrackingCode,
    o.TrackingLink,
    o.DeliveryDate
FROM orders o
WHERE o.OrderStatus != 'Deleted'
ORDER BY o.Id DESC";

            DataTable dtOrders = new DataTable();
            using (SqlCommand cmd = new SqlCommand(sqlOrders, conT))
            {
                conT.Open();
                new SqlDataAdapter(cmd).Fill(dtOrders);
                conT.Close();
            }

            if (dtOrders.Rows.Count == 0)
            {
                strReport = "<tr><td colspan='16' class='text-center text-muted py-4'>No orders found.</td></tr>";
                return;
            }

            // ── Fetch all order items in one query ───────────────────────
            string sqlItems = @"
SELECT
    oi.OrderGuid, oi.ProductId, oi.ProductName, oi.ProductImage, oi.ProductUrl,
    oi.UnitPrice, oi.Qty, oi.LineTotal, oi.ExclTax, oi.TaxAmount
FROM orders_item oi
ORDER BY oi.Id ASC";

            DataTable dtItems = new DataTable();
            using (SqlCommand cmd2 = new SqlCommand(sqlItems, conT))
            {
                conT.Open();
                new SqlDataAdapter(cmd2).Fill(dtItems);
                conT.Close();
            }

            // ── Fetch all payments in one query ──────────────────────────
            string sqlPay = @"
SELECT
    p.OrderGuid, p.RazorPaymentId, p.RazorOrderId,
    p.Amount, p.PaymentType, p.PaymentStatus, p.AddedOn AS PayAddedOn
FROM payment p
ORDER BY p.Id DESC";

            DataTable dtPay = new DataTable();
            using (SqlCommand cmd3 = new SqlCommand(sqlPay, conT))
            {
                conT.Open();
                new SqlDataAdapter(cmd3).Fill(dtPay);
                conT.Close();
            }

            var india = new System.Globalization.CultureInfo("en-IN");
            int i = 1;

            foreach (DataRow r in dtOrders.Rows)
            {
                string guid = r["OrderGuid"].ToString();
                string orderId = r["Id"].ToString();
                string firstName = r["FirstName"].ToString();
                string lastName = r["LastName"].ToString();
                string fullName = (firstName + " " + lastName).Trim();
                string email = r["Email"].ToString();
                string mobile = r["Mobile"].ToString();
                string address = r["Address"].ToString();
                string city = r["City"].ToString();
                string state = r["State"].ToString();
                string pincode = r["Pincode"].ToString();
                string country = r["Country"].ToString();
                string fullAddress = address + ", " + city + " - " + pincode + ", " + state + ", " + country;
                string company = r["CompanyName"].ToString();
                string gst = r["GSTNumber"].ToString();
                string payType = r["PaymentType"].ToString();
                string orderStatus = r["OrderStatus"].ToString();
                string payStatus = r["PaymentStatus"].ToString();
                string razorOrder = r["RazorOrderId"].ToString();
                string razorPay = r["RazorPaymentId"].ToString();
                string addedIp = r["AddedIp"].ToString();
                string paidOn = r["PaidOn"] == DBNull.Value || r["PaidOn"].ToString() == ""
                                         ? "-"
                                         : Convert.ToDateTime(r["PaidOn"]).ToString("dd MMM yyyy hh:mm tt");
                string addedOn = Convert.ToDateTime(r["AddedOn"]).ToString("dd MMM yyyy hh:mm tt");

                decimal grandTotal = r["GrandTotal"] == DBNull.Value ? 0 : Convert.ToDecimal(r["GrandTotal"]);
                decimal exclTax = r["ExclTax"] == DBNull.Value ? 0 : Convert.ToDecimal(r["ExclTax"]);
                decimal taxAmt = r["TaxAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(r["TaxAmount"]);
                decimal payableNow = r["PayableNow"] == DBNull.Value ? 0 : Convert.ToDecimal(r["PayableNow"]);
                decimal codAmount = r["CODAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(r["CODAmount"]);

                // ── Order Status badge ─────────────────────────────────
                string osBadge = orderStatus == "Confirmed"
                    ? "<span class='badge-status badge-confirmed'>Confirmed</span>"
                    : orderStatus == "Cancelled"
                    ? "<span class='badge-status badge-cancelled'>Cancelled</span>"
                    : orderStatus == "Shipped"
                    ? "<span class='badge-status badge-shipped'>Shipped</span>"
                    : orderStatus == "Delivered"
                    ? "<span class='badge-status badge-delivered'>Delivered</span>"
                    : "<span class='badge-status badge-pending'>" + HttpUtility.HtmlEncode(orderStatus) + "</span>";

                // ── Payment Status badge ───────────────────────────────
                string psBadge = payStatus == "Paid"
                    ? "<span class='badge-status badge-paid'>Paid</span>"
                    : payStatus == "Partial"
                    ? "<span class='badge-status badge-partial'>Partial</span>"
                    : "<span class='badge-status badge-unpaid'>" + HttpUtility.HtmlEncode(payStatus) + "</span>";

                // ── Payment Type badge ─────────────────────────────────
                string ptBadge = payType == "Partial"
                    ? "<span class='badge-status badge-partial'>Partial (20%+COD)</span>"
                    : "<span class='badge-status badge-confirmed'>Full</span>";

                // ── Items for this order ───────────────────────────────
                DataRow[] orderItems = dtItems.Select("OrderGuid = '" + guid.Replace("'", "''") + "'");
                int itemCount = orderItems.Length;

                // Build items JSON for modal
                string itemsJson = "[";
                int ii = 1;
                foreach (DataRow ir in orderItems)
                {
                    decimal unitPrice = ir["UnitPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(ir["UnitPrice"]);
                    decimal lineTotal = ir["LineTotal"] == DBNull.Value ? 0 : Convert.ToDecimal(ir["LineTotal"]);
                    decimal itExcl = ir["ExclTax"] == DBNull.Value ? 0 : Convert.ToDecimal(ir["ExclTax"]);
                    decimal itTax = ir["TaxAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(ir["TaxAmount"]);
                    int qty = ir["Qty"] == DBNull.Value ? 0 : Convert.ToInt32(ir["Qty"]);
                    string prodName = ir["ProductName"].ToString().Replace("\"", "&quot;").Replace("'", "\\'");
                    string prodUrl = ir["ProductUrl"].ToString();
                    string prodImg = ir["ProductImage"].ToString();

                    if (ii > 1) itemsJson += ",";
                    itemsJson += "{" +
                        "\"no\":" + ii + "," +
                        "\"name\":\"" + prodName + "\"," +
                        "\"url\":\"" + HttpUtility.JavaScriptStringEncode(prodUrl) + "\"," +
                        "\"img\":\"" + HttpUtility.JavaScriptStringEncode(prodImg) + "\"," +
                        "\"unitPrice\":\"₹" + unitPrice.ToString("N2", india) + "\"," +
                        "\"qty\":" + qty + "," +
                        "\"lineTotal\":\"₹" + lineTotal.ToString("N2", india) + "\"," +
                        "\"exclTax\":\"₹" + itExcl.ToString("N2", india) + "\"," +
                        "\"taxAmt\":\"₹" + itTax.ToString("N2", india) + "\"" +
                        "}";
                    ii++;
                }
                itemsJson += "]";

                // ── Build the data attributes for the unified modal ────
                // Encode all data into a single data-json attribute to keep markup clean
                string dataJson = "{" +
                    "\"orderId\":\"" + HttpUtility.JavaScriptStringEncode(orderId) + "\"," +
                    "\"name\":\"" + HttpUtility.JavaScriptStringEncode(fullName) + "\"," +
                    "\"email\":\"" + HttpUtility.JavaScriptStringEncode(email) + "\"," +
                    "\"mobile\":\"" + HttpUtility.JavaScriptStringEncode(mobile) + "\"," +
                    "\"address\":\"" + HttpUtility.JavaScriptStringEncode(address) + "\"," +
                    "\"city\":\"" + HttpUtility.JavaScriptStringEncode(city) + "\"," +
                    "\"state\":\"" + HttpUtility.JavaScriptStringEncode(state) + "\"," +
                    "\"pincode\":\"" + HttpUtility.JavaScriptStringEncode(pincode) + "\"," +
                    "\"country\":\"" + HttpUtility.JavaScriptStringEncode(country) + "\"," +
                    "\"company\":\"" + HttpUtility.JavaScriptStringEncode(company) + "\"," +
                    "\"gst\":\"" + HttpUtility.JavaScriptStringEncode(gst) + "\"," +
                    "\"payType\":\"" + HttpUtility.JavaScriptStringEncode(payType) + "\"," +
                    "\"grandTotal\":\"₹" + grandTotal.ToString("N2", india) + "\"," +
                    "\"exclTax\":\"₹" + exclTax.ToString("N2", india) + "\"," +
                    "\"taxAmt\":\"₹" + taxAmt.ToString("N2", india) + "\"," +
                    "\"payableNow\":\"₹" + payableNow.ToString("N2", india) + "\"," +
                    "\"codAmt\":\"₹" + codAmount.ToString("N2", india) + "\"," +
                    "\"orderStatus\":\"" + HttpUtility.JavaScriptStringEncode(orderStatus) + "\"," +
                    "\"payStatus\":\"" + HttpUtility.JavaScriptStringEncode(payStatus) + "\"," +
                    "\"razorOrder\":\"" + HttpUtility.JavaScriptStringEncode(razorOrder) + "\"," +
                    "\"razorPay\":\"" + HttpUtility.JavaScriptStringEncode(razorPay) + "\"," +
                    "\"paidOn\":\"" + HttpUtility.JavaScriptStringEncode(paidOn) + "\"," +
                    "\"addedOn\":\"" + HttpUtility.JavaScriptStringEncode(addedOn) + "\"," +
                    "\"addedIp\":\"" + HttpUtility.JavaScriptStringEncode(addedIp) + "\"," +
                    "\"items\":" + itemsJson +
                    "}";

                // Encode for HTML attribute (base64 encode to avoid quote issues)
                string dataB64 = Convert.ToBase64String(
                    System.Text.Encoding.UTF8.GetBytes(dataJson));

                string dispatchIcon = "";
                string viewShipIcon = "";
                string courier = r["CourierName"].ToString();
                string tracking = r["TrackingCode"].ToString();
                string link = r["TrackingLink"].ToString();
                string delDate = r["DeliveryDate"] == DBNull.Value ? "" : Convert.ToDateTime(r["DeliveryDate"]).ToString("yyyy-MM-dd");

                // DISPATCH BUTTON
                if (string.IsNullOrEmpty(courier))
                {
                    dispatchIcon = "<a href='javascript:void(0);' class='btn-action btn-status dispatchItem' data-id='" + guid + @"' title='Dispatch'><i class='ri-truck-line'></i></a>";
                }
                else
                {
                    dispatchIcon = "<a href='javascript:void(0);' class='btn-action btn-status text-muted'><i class='ri-truck-line'></i></a>";
                }

                string deliverIcon;
                if (orderStatus == "Dispatched")
                {
                    deliverIcon = "<a href='javascript:void(0);' class='btn-action btn-deliver deliverItem' data-id='" + HttpUtility.HtmlAttributeEncode(guid) + @"' title='Mark as Delivered'><i class='ri-checkbox-circle-line'></i></a>";
                }
                else if (orderStatus == "Delivered")
                {
                    deliverIcon = "<a href='javascript:void(0);' class='btn-action btn-deliver text-muted' style='cursor:default;' title='Already Delivered'><i class='ri-checkbox-circle-line'></i></a>";
                }
                else
                {
                    deliverIcon = "<a href='javascript:void(0);' class='btn-action btn-deliver-disabled' title='Only dispatched orders can be marked delivered' style='cursor:not-allowed;opacity:.4;'><i class='ri-checkbox-circle-line'></i></a>";
                }
                // VIEW SHIPPING DETAILS
                viewShipIcon = "<a href='javascript:void(0);' class='btn-action btn-view viewShipInfo' " +
                    "data-courier='" + HttpUtility.HtmlAttributeEncode(courier) + @"' " +
                    "data-tracking='" + HttpUtility.HtmlAttributeEncode(tracking) + @"' " +
                    "data-link='" + HttpUtility.HtmlAttributeEncode(link) + @"' " +
                    "data-date='" + delDate + @"' " +
                    "title='Shipping Info'>" +
                    "<i class='mdi mdi-truck-fast'></i></a>";
                string shippingCell = @"
<a href='javascript:void(0);' 
   class='btn-action btn-view viewShip'
   data-b64='" + dataB64 + @"'
   title='View Shipping'>
   <i class='mdi mdi-map-marker-path'></i>
</a>";
                strReport += @"<tr>
                    <td>" + i + @"</td>
                  <td>
    <strong>
        <a class='badge badge-outline-primary'
           href='/view-invoice.aspx?o=" + HttpUtility.UrlEncode(guid) + @"'
           target='_blank'>
            #" + HttpUtility.HtmlEncode(orderId) + @"
        </a>
    </strong>
    <br>
    <small class='text-muted'>" + addedOn + @"</small>
</td>
                    <td>" + shippingCell + @"" + viewShipIcon + @"</td>
                    <td>" + ptBadge + @"</td>
                    <td class='amount-cell'>₹" + grandTotal.ToString("N2", india) + @"</td>
                    <td class='amount-cell'>₹" + exclTax.ToString("N2", india) + @"</td>
                    <td class='amount-cell'>₹" + taxAmt.ToString("N2", india) + @"</td>
                    <td class='amount-cell'>₹" + payableNow.ToString("N2", india) + @"</td>
                    <td class='amount-cell'>₹" + codAmount.ToString("N2", india) + @"</td>
                    <td>" + osBadge + @"</td>
                    <td>" + psBadge + @"</td>
                    <td>" + paidOn + @"</td>
              
                    <td class='text-center action-col'>
                 <div class='action-btn-group'>
    " + dispatchIcon + @"
        " + deliverIcon + @"                  
                            <a href='javascript:void(0);' class='btn-action btn-delete deleteItem'
                               data-id='" + HttpUtility.HtmlAttributeEncode(guid) + @"'
                               title='Delete Order'>
                                <i class='mdi mdi-trash-can-outline'></i>
                            </a>
                        </div>
                    </td>
                </tr>";

                i++;
            }
        }
        catch (Exception ex)
        {
            strReport = "<tr><td colspan='14' class='text-danger'>Error loading report: " +
                        HttpUtility.HtmlEncode(ex.Message) + "</td></tr>";
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "BindReport", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string DispatchOrder(string OrderGuid, string courierName, string trackingCode, string trackingLink, string oStatus, string DelDate)
    {
        try
        {
            SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            var oDetails = Reports.GetOrderDetails(conT, OrderGuid);

            if (oDetails.Rows.Count == 0)
                return "NotFound";

            var currentStatus = Convert.ToString(oDetails.Rows[0]["OrderStatus"]);

            if (currentStatus == "Dispatched")
                return "Dispatched";

            if (currentStatus != "Confirmed")
                return "InvalidStatus";

         
            int rows = Reports.DispatchOrder(
                conT,
                OrderGuid,
                courierName,
                trackingCode,
                trackingLink,
                "Dispatched",       // Status
                DateTime.Now,       // addedOn
                HttpContext.Current.Request.UserHostAddress, // addedIp
                DelDate
            );


            if (rows > 0)
            {

                try { Reports.SendDispatchMail(conT, OrderGuid, courierName, trackingCode, trackingLink, DelDate); }
                catch
                {
                }
                return "Success";
            }
            return "W";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "DispatchOrder", ex.Message);
            return "W";
        }
    }
    [WebMethod(EnableSession = true)]
    public static string MarkDelivered(string id)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

 
            // string aid = HttpContext.Current.Request.Cookies["t_aid"].Value;
            // if (!CreateUser.CheckAccess(conT, "report.aspx", "Edit", aid))
            //     return "Permission";

            // Verify current status is Dispatched
            string sqlCheck = "SELECT OrderStatus FROM orders WHERE OrderGuid = @guid";
            string currentStatus = "";
            using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conT))
            {
                cmdCheck.Parameters.AddWithValue("@guid", id);
                conT.Open();
                object result = cmdCheck.ExecuteScalar();
                conT.Close();
                currentStatus = result != null ? result.ToString() : "";
            }

            if (currentStatus == "")
                return "NotFound";

            if (currentStatus == "Delivered")
                return "AlreadyDelivered";

            if (currentStatus != "Dispatched")
                return "InvalidStatus";

            string sql = "UPDATE orders SET OrderStatus = 'Delivered' WHERE OrderGuid = @guid";
            using (SqlCommand cmd = new SqlCommand(sql, conT))
            {
                cmd.Parameters.AddWithValue("@guid", id);
                conT.Open();
                int rows = cmd.ExecuteNonQuery();
                conT.Close();

                if (rows > 0)
                {
                    try { Reports.SendDeliveredMail(conT, id); }
                    catch { /* mail failure should not break the response */ }

                    return "Success";
                }
                return "W";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "MarkDelivered", ex.Message);
            return "W";
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            //string adminCookieKey = "t_new_id";
            //string aid = HttpContext.Current.Request.Cookies[adminCookieKey] != null
            //    ? HttpContext.Current.Request.Cookies[adminCookieKey].Value
            //    : "";

            //if (!CreateUser.CheckAccess(conT, "report.aspx", "Delete", aid))
            //    return "Permission";

            string sql = @"UPDATE orders SET OrderStatus = 'Deleted' WHERE OrderGuid = @guid";
            using (SqlCommand cmd = new SqlCommand(sql, conT))
            {
                cmd.Parameters.AddWithValue("@guid", id);
                conT.Open();
                int rows = cmd.ExecuteNonQuery();
                conT.Close();
                return rows > 0 ? "Success" : "W";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
            return "W";
        }
    }

    private string Enc(string v)
    {
        return HttpUtility.HtmlAttributeEncode(v ?? "");
    }
}