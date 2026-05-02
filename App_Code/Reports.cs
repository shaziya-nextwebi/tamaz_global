using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Reports
/// </summary>
public class Reports
{
    public Reports()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable GetOrderDetails(SqlConnection _conn, string o_guid)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = @"
            SELECT *
            FROM Orders
            WHERE OrderGuid = @OrderGuid";

            SqlCommand cmd = new SqlCommand(query, _conn);
            cmd.Parameters.AddWithValue("@OrderGuid", o_guid);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "GetOrderDetails",
                ex.Message);
        }
        return dt;
    }
    public static int DispatchOrder(SqlConnection _conn, string oGuid, string cName, string trkCode, string cLink, string Status, DateTime addedon, string addedip, string DelDate)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand(@"
    UPDATE Orders 
    SET 
        OrderStatus=@OrderStatus,
        CourierName=@CourierName,
        DeliveryDate=@DeliveryDate,
        TrackingCode=@TrackingCode,
        TrackingLink=@TrackingLink
    WHERE OrderGuid=@id AND OrderStatus='Confirmed'", _conn);
            cmd.Parameters.AddWithValue("@id", oGuid);
            cmd.Parameters.AddWithValue("@OrderStatus", Status);
            cmd.Parameters.AddWithValue("@DeliveryDate", DelDate);
            cmd.Parameters.AddWithValue("@CourierName", cName);
            cmd.Parameters.AddWithValue("@TrackingCode", trkCode);
            cmd.Parameters.AddWithValue("@TrackingLink", cLink);

            _conn.Open();
            x = cmd.ExecuteNonQuery();
            _conn.Close();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "DispatchOrder",
                ex.Message);
        }
        return x;
    }
    public static void SendDispatchMail(SqlConnection _conn, string oGuid, string courierName, string trackingCode, string trackingLink, string deliveryDate)
    {
        try
        {
            var dt = GetOrderDetails(_conn, oGuid);
            if (dt.Rows.Count == 0) return;

            DataRow o = dt.Rows[0];
            string name = o["FirstName"].ToString() + " " + o["LastName"].ToString();
            string email = o["Email"].ToString();
            string orderId = o["Id"].ToString();
            string mobile = o["Mobile"].ToString();
            string address = o["Address"].ToString() + ", " + o["City"].ToString() + " - " + o["Pincode"].ToString() + ", " + o["State"].ToString() + ", " + o["Country"].ToString();
            string grandTotal = "₹" + Convert.ToDecimal(o["GrandTotal"]).ToString("N2");
            string exclTax = "₹" + Convert.ToDecimal(o["ExclTax"]).ToString("N2");
            string taxAmt = "₹" + Convert.ToDecimal(o["TaxAmount"]).ToString("N2");
            string payableNow = "₹" + Convert.ToDecimal(o["PayableNow"]).ToString("N2");
            string codAmt = "₹" + Convert.ToDecimal(o["CODAmount"]).ToString("N2");
            string payType = o["PaymentType"].ToString();
            string orderedOn = Convert.ToDateTime(o["AddedOn"]).ToString("dd MMM yyyy");
            string adminEmail = System.Configuration.ConfigurationManager.AppSettings["ToMail"];
            string domain = System.Configuration.ConfigurationManager.AppSettings["domain"];
            string logoUrl = domain + "/assests/Images/logo.png";

            // ── Fetch order items ─────────────────────────────────────
            string itemsHtml = "";
            string sqlItems = "SELECT ProductName, ProductImage, ProductUrl, UnitPrice, Qty, LineTotal FROM orders_item WHERE OrderGuid = @guid ORDER BY Id ASC";
            DataTable dtItems = new DataTable();
            using (SqlCommand ci = new SqlCommand(sqlItems, _conn))
            {
                ci.Parameters.AddWithValue("@guid", oGuid);
                _conn.Open();
                new SqlDataAdapter(ci).Fill(dtItems);
                _conn.Close();
            }

            var india = new System.Globalization.CultureInfo("en-IN");
            int idx = 1;
            foreach (DataRow ir in dtItems.Rows)
            {
                string pName = System.Web.HttpUtility.HtmlEncode(ir["ProductName"].ToString());
                string pImg = ir["ProductImage"].ToString();
                string pUrl = ir["ProductUrl"].ToString();
                string uPrice = "₹" + Convert.ToDecimal(ir["UnitPrice"]).ToString("N2", india);
                string lTotal = "₹" + Convert.ToDecimal(ir["LineTotal"]).ToString("N2", india);
                int qty = Convert.ToInt32(ir["Qty"]);

                string imgTag = !string.IsNullOrEmpty(pImg)
                    ? "<img src='" + domain + "/" + pImg + "' width='48' height='48' style='object-fit:contain;border-radius:6px;border:1px solid #e2e8f0;'/>"
                    : "<div style='width:48px;height:48px;background:#f1f5f9;border-radius:6px;display:inline-block;'></div>";

                itemsHtml += @"
            <tr style='border-bottom:1px solid #f1f5f9;'>
              <td style='padding:12px 10px;color:#64748b;font-size:13px;'>" + idx + @"</td>
              <td style='padding:12px 10px;'>
                <table border='0' cellpadding='0' cellspacing='0'>
                  <tr>
                    <td style='padding-right:12px;vertical-align:middle;'>" + imgTag + @"</td>
                    <td style='vertical-align:middle;'>
                      <a href='" + domain + "/" + pUrl + @"' style='font-size:13px;font-weight:600;color:#1e293b;text-decoration:none;'>" + pName + @"</a>
                    </td>
                  </tr>
                </table>
              </td>
              <td style='padding:12px 10px;font-size:13px;color:#475569;text-align:center;'>" + uPrice + @"</td>
              <td style='padding:12px 10px;text-align:center;'>
                <span style='display:inline-block;background:#eff6ff;color:#2563eb;border-radius:12px;padding:2px 10px;font-size:12px;font-weight:700;'>" + qty + @"</span>
              </td>
              <td style='padding:12px 10px;font-size:13px;font-weight:700;color:#0f172a;text-align:right;'>" + lTotal + @"</td>
            </tr>";
                idx++;
            }

            string mailbody = @"<!DOCTYPE html>
<html>
<head><meta charset='UTF-8'/><meta name='viewport' content='width=device-width,initial-scale=1'/></head>
<body style='margin:0;padding:0;background:#f1f5f9;font-family:Segoe UI,Arial,sans-serif;'>
<table border='0' cellpadding='0' cellspacing='0' width='100%' style='background:#f1f5f9;padding:30px 0;'>
  <tr><td align='center'>
    <table border='0' cellpadding='0' cellspacing='0' width='620' style='background:#fff;border-radius:12px;overflow:hidden;box-shadow:0 4px 20px rgba(0,0,0,.08);'>

 <!-- LOGO HEADER -->
<tr><td style='background:#ffffff;padding:20px 32px;text-align:center;border-bottom:1px solid #e2e8f0;'>
  <img src='" + domain + @"/assests/Images/logo.png' alt='Logo' height='50' width='auto' 
       style='display:block;margin:0 auto;max-height:50px;border:0;outline:none;text-decoration:none;'/>
</td></tr>

  <!-- BLUE DISPATCH HEADER -->
<tr><td bgcolor='#2563eb' style='background:#2563eb;padding:28px 32px;'>
  <table border='0' cellpadding='0' cellspacing='0' width='100%'>
    <tr><td>
      <span style='display:inline-block;background:rgba(255,255,255,.25);border-radius:20px;padding:4px 14px;font-size:12px;font-weight:600;color:#ffffff !important;margin-bottom:12px;'>&#128666; Order Dispatched</span>
    </td></tr>
    <tr><td>
      <p style='margin:0 0 6px;font-size:22px;font-weight:700;color:#ffffff !important;font-family:Segoe UI,Arial,sans-serif;'>Your order is on the way!</p>
    </td></tr>
    <tr><td>
      <p style='margin:0;font-size:13px;color:#bfdbfe !important;font-family:Segoe UI,Arial,sans-serif;'>Order #" + orderId + @" &nbsp;|&nbsp; " + orderedOn + @"</p>
    </td></tr>
  </table>
</td></tr>

      <!-- BODY -->
      <tr><td style='padding:28px 32px;'>
        <p style='font-size:15px;color:#1e293b;margin:0 0 8px;'>Hi <strong>" + System.Web.HttpUtility.HtmlEncode(name) + @"</strong>,</p>
        <p style='font-size:13px;color:#475569;line-height:1.7;margin:0 0 24px;'>Great news! Your order has been dispatched. Use the tracking details below to follow your shipment.</p>

        <!-- TRACKING BOX -->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='background:#eff6ff;border:1px solid #bfdbfe;border-radius:10px;margin-bottom:24px;'>
          <tr><td style='padding:16px 20px;'>
            <p style='margin:0 0 12px;font-size:12px;font-weight:700;text-transform:uppercase;letter-spacing:.5px;color:#1e40af;'>📦 Tracking Information</p>
            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
              <tr><td style='padding:5px 0;font-size:13px;color:#64748b;width:50%;'>Courier</td><td style='font-size:13px;font-weight:600;color:#1e293b;text-align:right;'>" + System.Web.HttpUtility.HtmlEncode(courierName) + @"</td></tr>
              <tr><td style='padding:5px 0;font-size:13px;color:#64748b;'>Tracking Code</td><td style='font-size:13px;font-weight:600;color:#1e293b;text-align:right;'>" + System.Web.HttpUtility.HtmlEncode(trackingCode) + @"</td></tr>
              <tr><td style='padding:5px 0;font-size:13px;color:#64748b;'>Expected Delivery</td><td style='font-size:13px;font-weight:600;color:#1e293b;text-align:right;'>" + System.Web.HttpUtility.HtmlEncode(deliveryDate) + @"</td></tr>
              <tr><td style='padding:5px 0;font-size:13px;color:#64748b;'>Track Your Order</td><td style='text-align:right;'><a href='/track-order.aspx' style='font-size:13px;font-weight:600;color:#2563eb;'></a></td></tr>
            </table>
          </td></tr>
        </table>

        <!-- ORDER ITEMS TABLE -->
        <p style='font-size:12px;font-weight:700;text-transform:uppercase;letter-spacing:.5px;color:#64748b;margin:0 0 10px;'>🛍 Items in Your Order</p>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border:1px solid #e2e8f0;border-radius:8px;overflow:hidden;margin-bottom:24px;'>
          <thead>
            <tr style='background:#f8fafc;'>
              <th style='padding:10px;font-size:11px;color:#64748b;text-align:left;border-bottom:2px solid #e2e8f0;width:30px;'>#</th>
              <th style='padding:10px;font-size:11px;color:#64748b;text-align:left;border-bottom:2px solid #e2e8f0;'>Product</th>
              <th style='padding:10px;font-size:11px;color:#64748b;text-align:center;border-bottom:2px solid #e2e8f0;'>Unit Price</th>
              <th style='padding:10px;font-size:11px;color:#64748b;text-align:center;border-bottom:2px solid #e2e8f0;'>Qty</th>
              <th style='padding:10px;font-size:11px;color:#64748b;text-align:right;border-bottom:2px solid #e2e8f0;'>Total</th>
            </tr>
          </thead>
          <tbody>" + itemsHtml + @"</tbody>
        </table>

        <!-- TOTALS -->
        <table border='0' cellpadding='0' cellspacing='0' align='right' width='280' style='background:#f8fafc;border:1px solid #e2e8f0;border-radius:8px;margin-bottom:24px;'>
          <tr><td style='padding:14px 16px;'>
            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
              <tr><td style='font-size:12px;color:#64748b;padding:4px 0;'>Excl. Tax</td><td style='font-size:12px;font-weight:600;color:#1e293b;text-align:right;'>" + exclTax + @"</td></tr>
              <tr><td style='font-size:12px;color:#64748b;padding:4px 0;'>Tax (GST)</td><td style='font-size:12px;font-weight:600;color:#1e293b;text-align:right;'>" + taxAmt + @"</td></tr>
              <tr><td colspan='2' style='border-top:1px solid #e2e8f0;padding-top:8px;margin-top:8px;'></td></tr>
              <tr><td style='font-size:14px;font-weight:700;color:#1e293b;padding:4px 0;'>Grand Total</td><td style='font-size:16px;font-weight:700;color:#1d4ed8;text-align:right;'>" + grandTotal + @"</td></tr>
              <tr><td style='font-size:12px;color:#64748b;padding:4px 0;'>Paid Now</td><td style='font-size:12px;font-weight:600;color:#16a34a;text-align:right;'>" + payableNow + @"</td></tr>
              <tr><td style='font-size:12px;color:#64748b;padding:4px 0;'>COD Amount</td><td style='font-size:12px;font-weight:600;color:#dc2626;text-align:right;'>" + codAmt + @"</td></tr>
            </table>
          </td></tr>
        </table>

        <!-- INFO GRID -->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='margin-bottom:20px;'>
          <tr>
            <td width='48%' style='background:#f8fafc;border:1px solid #e2e8f0;border-radius:8px;padding:12px 14px;'>
              <div style='font-size:10px;text-transform:uppercase;letter-spacing:.5px;color:#94a3b8;font-weight:700;'>Mobile</div>
              <div style='font-size:13px;color:#1e293b;font-weight:600;'>" + System.Web.HttpUtility.HtmlEncode(mobile) + @"</div>
            </td>
            <td width='4%'></td>
            <td width='48%' style='background:#f8fafc;border:1px solid #e2e8f0;border-radius:8px;padding:12px 14px;'>
              <div style='font-size:10px;text-transform:uppercase;letter-spacing:.5px;color:#94a3b8;font-weight:700;'>Payment Type</div>
              <div style='font-size:13px;color:#1e293b;font-weight:600;'>" + System.Web.HttpUtility.HtmlEncode(payType) + @"</div>
            </td>
          </tr>
        </table>

        <div style='background:#f8fafc;border:1px solid #e2e8f0;border-radius:8px;padding:14px 16px;font-size:13px;color:#475569;'>
          <strong style='color:#1e293b;'>Shipping To:</strong><br/>" + System.Web.HttpUtility.HtmlEncode(address) + @"
        </div>
      </td></tr>

      <!-- FOOTER -->
      <tr><td style='background:#f8fafc;border-top:1px solid #e2e8f0;padding:18px 32px;text-align:center;font-size:12px;color:#94a3b8;'>
        Thank you for shopping with us &nbsp;•&nbsp; <a href='" + domain + @"' style='color:#2563eb;text-decoration:none;'>" + domain + @"</a>
      </td></tr>

    </table>
  </td></tr>
</table>
</body></html>";

            SendMail(email, adminEmail, "Your Order #" + orderId + " Has Been Dispatched! 🚚", mailbody);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendDispatchMail", ex.Message);
        }
    }
    public static void SendDeliveredMail(SqlConnection _conn, string oGuid)
    {
        try
        {
            var dt = GetOrderDetails(_conn, oGuid);
            if (dt.Rows.Count == 0) return;

            DataRow o = dt.Rows[0];
            string name = o["FirstName"].ToString() + " " + o["LastName"].ToString();
            string email = o["Email"].ToString();
            string orderId = o["Id"].ToString();
            string mobile = o["Mobile"].ToString();
            string address = o["Address"].ToString() + ", " + o["City"].ToString() + " - " + o["Pincode"].ToString() + ", " + o["State"].ToString() + ", " + o["Country"].ToString();
            string grandTotal = "₹" + Convert.ToDecimal(o["GrandTotal"]).ToString("N2");
            string exclTax = "₹" + Convert.ToDecimal(o["ExclTax"]).ToString("N2");
            string taxAmt = "₹" + Convert.ToDecimal(o["TaxAmount"]).ToString("N2");
            string payableNow = "₹" + Convert.ToDecimal(o["PayableNow"]).ToString("N2");
            string codAmt = "₹" + Convert.ToDecimal(o["CODAmount"]).ToString("N2");
            string payType = o["PaymentType"].ToString();
            string courier = o["CourierName"].ToString();
            string tracking = o["TrackingCode"].ToString();
            string orderedOn = Convert.ToDateTime(o["AddedOn"]).ToString("dd MMM yyyy");
            string deliveredOn = DateTime.Now.ToString("dd MMM yyyy");
            string adminEmail = System.Configuration.ConfigurationManager.AppSettings["ToMail"];
            string domain = System.Configuration.ConfigurationManager.AppSettings["domain"];
            string logoUrl = domain + "/assests/Images/logo.png";

            // ── Fetch order items ─────────────────────────────────────
            string itemsHtml = "";
            string sqlItems = "SELECT ProductName, ProductImage, ProductUrl, UnitPrice, Qty, LineTotal FROM orders_item WHERE OrderGuid = @guid ORDER BY Id ASC";
            DataTable dtItems = new DataTable();
            using (SqlCommand ci = new SqlCommand(sqlItems, _conn))
            {
                ci.Parameters.AddWithValue("@guid", oGuid);
                _conn.Open();
                new SqlDataAdapter(ci).Fill(dtItems);
                _conn.Close();
            }

            var india = new System.Globalization.CultureInfo("en-IN");
            int idx = 1;
            foreach (DataRow ir in dtItems.Rows)
            {
                string pName = System.Web.HttpUtility.HtmlEncode(ir["ProductName"].ToString());
                string pImg = ir["ProductImage"].ToString();
                string pUrl = ir["ProductUrl"].ToString();
                string uPrice = "₹" + Convert.ToDecimal(ir["UnitPrice"]).ToString("N2", india);
                string lTotal = "₹" + Convert.ToDecimal(ir["LineTotal"]).ToString("N2", india);
                int qty = Convert.ToInt32(ir["Qty"]);

                string imgTag = !string.IsNullOrEmpty(pImg)
                    ? "<img src='" + domain + "/" + pImg + "' width='48' height='48' style='object-fit:contain;border-radius:6px;border:1px solid #e2e8f0;'/>"
                    : "<div style='width:48px;height:48px;background:#f1f5f9;border-radius:6px;display:inline-block;'></div>";

                itemsHtml += @"
            <tr style='border-bottom:1px solid #f1f5f9;'>
              <td style='padding:12px 10px;color:#64748b;font-size:13px;'>" + idx + @"</td>
              <td style='padding:12px 10px;'>
                <table border='0' cellpadding='0' cellspacing='0'>
                  <tr>
                    <td style='padding-right:12px;vertical-align:middle;'>" + imgTag + @"</td>
                    <td style='vertical-align:middle;'>
                      <a href='" + domain + "/" + pUrl + @"' style='font-size:13px;font-weight:600;color:#1e293b;text-decoration:none;'>" + pName + @"</a>
                    </td>
                  </tr>
                </table>
              </td>
              <td style='padding:12px 10px;font-size:13px;color:#475569;text-align:center;'>" + uPrice + @"</td>
              <td style='padding:12px 10px;text-align:center;'>
                <span style='display:inline-block;background:#f0fdf4;color:#16a34a;border-radius:12px;padding:2px 10px;font-size:12px;font-weight:700;'>" + qty + @"</span>
              </td>
              <td style='padding:12px 10px;font-size:13px;font-weight:700;color:#0f172a;text-align:right;'>" + lTotal + @"</td>
            </tr>";
                idx++;
            }

            string mailbody = @"<!DOCTYPE html>
<html>
<head><meta charset='UTF-8'/><meta name='viewport' content='width=device-width,initial-scale=1'/></head>
<body style='margin:0;padding:0;background:#f1f5f9;font-family:Segoe UI,Arial,sans-serif;'>
<table border='0' cellpadding='0' cellspacing='0' width='100%' style='background:#f1f5f9;padding:30px 0;'>
  <tr><td align='center'>
    <table border='0' cellpadding='0' cellspacing='0' width='620' style='background:#fff;border-radius:12px;overflow:hidden;box-shadow:0 4px 20px rgba(0,0,0,.08);'>

      <!-- LOGO HEADER -->
      <tr><td style='background:#ffffff;padding:20px 32px;text-align:center;border-bottom:1px solid #e2e8f0;'>
        <img src='" + logoUrl + @"' alt='Logo' height='50' style='display:inline-block;'/>
      </td></tr>

<!-- GREEN DELIVERED HEADER -->
<tr><td bgcolor='#059669' style='background:#059669;padding:28px 32px;'>
  <table border='0' cellpadding='0' cellspacing='0' width='100%'>
    <tr><td>
      <span style='display:inline-block;background:rgba(255,255,255,.25);border-radius:20px;padding:4px 14px;font-size:12px;font-weight:600;color:#ffffff !important;margin-bottom:12px;'>&#10003; Order Delivered</span>
    </td></tr>
    <tr><td>
      <p style='margin:0 0 6px;font-size:22px;font-weight:700;color:#ffffff !important;font-family:Segoe UI,Arial,sans-serif;'>Your order has been delivered!</p>
    </td></tr>
    <tr><td>
      <p style='margin:0;font-size:13px;color:#d1fae5 !important;font-family:Segoe UI,Arial,sans-serif;'>Order #" + orderId + @" &nbsp;|&nbsp; Delivered on " + deliveredOn + @"</p>
    </td></tr>
  </table>
</td></tr>

      <!-- BODY -->
      <tr><td style='padding:28px 32px;'>
        <p style='font-size:15px;color:#1e293b;margin:0 0 8px;'>Hi <strong>" + System.Web.HttpUtility.HtmlEncode(name) + @"</strong>,</p>
        <p style='font-size:13px;color:#475569;line-height:1.7;margin:0 0 20px;'>We're happy to let you know that your order has been successfully delivered. We hope you love your purchase!</p>

        <!-- SUCCESS BOX -->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='background:#f0fdf4;border:1px solid #bbf7d0;border-radius:10px;margin-bottom:24px;'>
          <tr><td style='padding:20px;text-align:center;'>
            <div style='font-size:36px;margin-bottom:6px;'>🎉</div>
            <p style='margin:0 0 4px;font-size:16px;font-weight:700;color:#065f46;'>Delivered Successfully</p>
            <p style='margin:0;font-size:13px;color:#047857;'>Delivered on " + deliveredOn + @" &nbsp;|&nbsp; Courier: " + System.Web.HttpUtility.HtmlEncode(courier) + @"</p>
          </td></tr>
        </table>

        <!-- ORDER ITEMS TABLE -->
        <p style='font-size:12px;font-weight:700;text-transform:uppercase;letter-spacing:.5px;color:#64748b;margin:0 0 10px;'>🛍 Items Delivered</p>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='border:1px solid #e2e8f0;border-radius:8px;overflow:hidden;margin-bottom:24px;'>
          <thead>
            <tr style='background:#f8fafc;'>
              <th style='padding:10px;font-size:11px;color:#64748b;text-align:left;border-bottom:2px solid #e2e8f0;width:30px;'>#</th>
              <th style='padding:10px;font-size:11px;color:#64748b;text-align:left;border-bottom:2px solid #e2e8f0;'>Product</th>
              <th style='padding:10px;font-size:11px;color:#64748b;text-align:center;border-bottom:2px solid #e2e8f0;'>Unit Price</th>
              <th style='padding:10px;font-size:11px;color:#64748b;text-align:center;border-bottom:2px solid #e2e8f0;'>Qty</th>
              <th style='padding:10px;font-size:11px;color:#64748b;text-align:right;border-bottom:2px solid #e2e8f0;'>Total</th>
            </tr>
          </thead>
          <tbody>" + itemsHtml + @"</tbody>
        </table>

        <!-- TOTALS -->
        <table border='0' cellpadding='0' cellspacing='0' align='right' width='280' style='background:#f8fafc;border:1px solid #e2e8f0;border-radius:8px;margin-bottom:24px;'>
          <tr><td style='padding:14px 16px;'>
            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
              <tr><td style='font-size:12px;color:#64748b;padding:4px 0;'>Excl. Tax</td><td style='font-size:12px;font-weight:600;color:#1e293b;text-align:right;'>" + exclTax + @"</td></tr>
              <tr><td style='font-size:12px;color:#64748b;padding:4px 0;'>Tax (GST)</td><td style='font-size:12px;font-weight:600;color:#1e293b;text-align:right;'>" + taxAmt + @"</td></tr>
              <tr><td colspan='2' style='border-top:1px solid #e2e8f0;padding-top:8px;'></td></tr>
              <tr><td style='font-size:14px;font-weight:700;color:#1e293b;padding:4px 0;'>Grand Total</td><td style='font-size:16px;font-weight:700;color:#059669;text-align:right;'>" + grandTotal + @"</td></tr>
              <tr><td style='font-size:12px;color:#64748b;padding:4px 0;'>Paid Now</td><td style='font-size:12px;font-weight:600;color:#16a34a;text-align:right;'>" + payableNow + @"</td></tr>
              <tr><td style='font-size:12px;color:#64748b;padding:4px 0;'>COD Amount</td><td style='font-size:12px;font-weight:600;color:#dc2626;text-align:right;'>" + codAmt + @"</td></tr>
            </table>
          </td></tr>
        </table>

        <!-- INFO ROW -->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='margin-bottom:20px;'>
          <tr>
            <td width='31%' style='background:#f8fafc;border:1px solid #e2e8f0;border-radius:8px;padding:12px 14px;'>
              <div style='font-size:10px;text-transform:uppercase;letter-spacing:.5px;color:#94a3b8;font-weight:700;'>Tracking Code</div>
              <div style='font-size:13px;color:#1e293b;font-weight:600;'>" + System.Web.HttpUtility.HtmlEncode(tracking) + @"</div>
            </td>
            <td width='2%'></td>
            <td width='31%' style='background:#f8fafc;border:1px solid #e2e8f0;border-radius:8px;padding:12px 14px;'>
              <div style='font-size:10px;text-transform:uppercase;letter-spacing:.5px;color:#94a3b8;font-weight:700;'>Mobile</div>
              <div style='font-size:13px;color:#1e293b;font-weight:600;'>" + System.Web.HttpUtility.HtmlEncode(mobile) + @"</div>
            </td>
            <td width='2%'></td>
            <td width='31%' style='background:#f8fafc;border:1px solid #e2e8f0;border-radius:8px;padding:12px 14px;'>
              <div style='font-size:10px;text-transform:uppercase;letter-spacing:.5px;color:#94a3b8;font-weight:700;'>Payment Type</div>
              <div style='font-size:13px;color:#1e293b;font-weight:600;'>" + System.Web.HttpUtility.HtmlEncode(payType) + @"</div>
            </td>
          </tr>
        </table>

        <div style='background:#f8fafc;border:1px solid #e2e8f0;border-radius:8px;padding:14px 16px;margin-bottom:20px;font-size:13px;color:#475569;'>
          <strong style='color:#1e293b;'>Delivered To:</strong><br/>" + System.Web.HttpUtility.HtmlEncode(address) + @"
        </div>

        <!-- FEEDBACK BOX -->
        <div style='background:#fffbeb;border:1px solid #fde68a;border-radius:8px;padding:14px 16px;font-size:13px;color:#78350f;'>
          💬 <strong>Your feedback matters!</strong> Write to us at 
          <a href='mailto:" + adminEmail + @"' style='color:#92400e;'>" + adminEmail + @"</a>
        </div>
      </td></tr>

      <!-- FOOTER -->
      <tr><td style='background:#f8fafc;border-top:1px solid #e2e8f0;padding:18px 32px;text-align:center;font-size:12px;color:#94a3b8;'>
        Thank you for shopping with us &nbsp;•&nbsp; <a href='" + domain + @"' style='color:#2563eb;text-decoration:none;'>" + domain + @"</a>
      </td></tr>

    </table>
  </td></tr>
</table>
</body></html>";

            SendMail(email, adminEmail, "Your Order #" + orderId + " Has Been Delivered! ✅", mailbody);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendDeliveredMail", ex.Message);
        }
    }
    private static void SendMail(string customerEmail, string adminEmail, string subject, string body)
    {
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        smtp.Host = System.Configuration.ConfigurationManager.AppSettings["host"];
        smtp.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["port"]);
        smtp.Credentials = new System.Net.NetworkCredential(
            System.Configuration.ConfigurationManager.AppSettings["userName"],
            System.Configuration.ConfigurationManager.AppSettings["password"]);
        smtp.EnableSsl = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["enableSsl"]);

        // Send to customer
        System.Net.Mail.MailMessage mail1 = new System.Net.Mail.MailMessage();
        mail1.To.Add(customerEmail);
        mail1.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["from"], System.Configuration.ConfigurationManager.AppSettings["fromName"]);
        mail1.Subject = subject;
        mail1.Body = body;
        mail1.IsBodyHtml = true;
        smtp.Send(mail1);

        // Send copy to admin
        System.Net.Mail.MailMessage mail2 = new System.Net.Mail.MailMessage();
        mail2.To.Add(adminEmail);
        mail2.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["from"], System.Configuration.ConfigurationManager.AppSettings["fromName"]);
        mail2.Subject = "[ADMIN COPY] " + subject;
        mail2.Body = body;
        mail2.IsBodyHtml = true;
        smtp.Send(mail2);
    }
}