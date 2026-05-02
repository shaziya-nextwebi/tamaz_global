<%@ WebHandler Language="C#" Class="PaymentCallback" %>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;

public class PaymentCallback : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        string paymentId = context.Request.Form["razorpay_payment_id"];
        string orderId = context.Request.Form["razorpay_order_id"];
        string signature = context.Request.Form["razorpay_signature"];

        if (string.IsNullOrEmpty(paymentId))
        {
            context.Response.StatusCode = 400;
            context.Response.Write("{\"error\":\"Missing payment ID\"}");
            return;
        }

        if (!VerifyRazorpaySignature(orderId, paymentId, signature))
        {
            context.Response.StatusCode = 400;
            context.Response.Write("{\"error\":\"Invalid signature\"}");
            return;
        }

        // ── Fetch order from DB using RazorOrderId ───────────────────────
        DataRow orderRow = GetOrderByRazorOrderId(orderId);
        if (orderRow == null)
        {
            context.Response.StatusCode = 400;
            context.Response.Write("{\"error\":\"Order not found\"}");
            return;
        }

        string orderGuid = orderRow["OrderGuid"].ToString();
        int dbOrderId = Convert.ToInt32(orderRow["Id"]);


        SavePayment(dbOrderId, orderGuid, paymentId, orderId, signature,
                    Convert.ToDecimal(orderRow["PayableNow"]),
                    orderRow["PaymentType"].ToString());

        UpdateOrderStatus(orderGuid, paymentId, orderId);

        // ── Clear cart ───────────────────────────────────────────────────
        using (var conT2 = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString))
        {
            DeleteOrderedItemsFromCart(conT2, orderGuid);
        }
        // ── Send emails from DB data ──────────────────────────────────────
        try { SendOrderEmails(orderRow, orderGuid); }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException("/PaymentCallback.ashx", "SendOrderEmails", ex.Message);
        }

        // ── Store minimal session flags for success page ─────────────────
        context.Session["payment_verified"] = true;
        context.Session["razorpay_payment_id"] = paymentId;

        context.Response.StatusCode = 200;
        context.Response.Write("{\"success\":true}");
    }
    private void DeleteOrderedItemsFromCart(SqlConnection conT, string orderGuid)
    {
        try
        {
            // Get the ProductIds that were actually ordered
            string sqlGetIds = "SELECT ProductId FROM orders_item WHERE OrderGuid = @guid";
            List<int> orderedProductIds = new List<int>();

            using (SqlCommand cmd = new SqlCommand(sqlGetIds, conT))
            {
                cmd.Parameters.AddWithValue("@guid", orderGuid);
                conT.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        orderedProductIds.Add(Convert.ToInt32(dr["ProductId"]));
                }
                conT.Close();
            }

            // Delete only those specific products from cart
            foreach (int productId in orderedProductIds)
            {
                AddtoCart.Deletecartlist(conT, productId.ToString());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException("/PaymentCallback.ashx", "DeleteOrderedItemsFromCart", ex.Message);
        }
    }
    // ── Signature verification ────────────────────────────────────────────
    private bool VerifyRazorpaySignature(string orderId, string paymentId, string signature)
    {
        try
        {
            string secret = ConfigurationManager.AppSettings["razorsecret"];
            string payload = orderId + "|" + paymentId;
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                string generated = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return generated == signature;
            }
        }
        catch { return false; }
    }

    // ── Fetch order from orders table by RazorOrderId ────────────────────
    private DataRow GetOrderByRazorOrderId(string razorOrderId)
    {
        try
        {
            using (var conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString))
            {
                string sql = "SELECT * FROM orders WHERE RazorOrderId = @rid";
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand(sql, conT))
                {
                    cmd.Parameters.AddWithValue("@rid", razorOrderId);
                    conT.Open();
                    new SqlDataAdapter(cmd).Fill(dt);
                    conT.Close();
                }
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException("/PaymentCallback.ashx", "GetOrderByRazorOrderId", ex.Message);
            return null;
        }
    }

  private void SavePayment(int orderId, string orderGuid,
    string razorPaymentId, string razorOrderId, string signature,
    decimal amount, string paymentType)
{
    try
    {
        string sql = @"
INSERT INTO payment
(OrderId, OrderGuid, RazorPaymentId, RazorOrderId, Signature, Amount, PaymentType, PaymentStatus, AddedOn, AddedIp)
VALUES
(@OrderId, @OrderGuid, @RazorPaymentId, @RazorOrderId, @Signature, @Amount, @PaymentType, @PaymentStatus, @AddedOn, @AddedIp)";

        using (var conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conT))
        {
            cmd.Parameters.AddWithValue("@OrderId", orderId);
            cmd.Parameters.AddWithValue("@OrderGuid", orderGuid);
            cmd.Parameters.AddWithValue("@RazorPaymentId", razorPaymentId);
            cmd.Parameters.AddWithValue("@RazorOrderId", razorOrderId);
            cmd.Parameters.AddWithValue("@Signature", signature);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@PaymentType", paymentType);
            cmd.Parameters.AddWithValue("@PaymentStatus", "Paid");
            cmd.Parameters.AddWithValue("@AddedOn", DateTime.Now);
            cmd.Parameters.AddWithValue("@AddedIp",
                HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "");

            conT.Open();
            cmd.ExecuteNonQuery();
            conT.Close();
        }
    }
    catch (Exception ex)
    {
        ExceptionCapture.CaptureException(
            "/PaymentCallback.ashx",
            "SavePayment",
            ex.ToString() // IMPORTANT
        );
    }
}
    private void UpdateOrderStatus(string orderGuid, string razorPaymentId, string razorOrderId)
    {
        try
        {
            string sql = @"
UPDATE orders
SET PaymentStatus  = 'Paid',
    OrderStatus    = 'Confirmed',
    RazorPaymentId = @pid,
    PaidOn         = @now
WHERE OrderGuid = @guid";

            using (var conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conT))
            {
                cmd.Parameters.AddWithValue("@pid", razorPaymentId);
                cmd.Parameters.AddWithValue("@now", TimeStamps.UTCTime());
                cmd.Parameters.AddWithValue("@guid", orderGuid);
                conT.Open();
                cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException("/PaymentCallback.ashx", "UpdateOrderStatus", ex.Message);
        }
    }

    // ── Email sending — all data read from DB ─────────────────────────────
    private void SendOrderEmails(DataRow orderRow, string orderGuid)
    {
        string firstName = orderRow["FirstName"].ToString();
        string lastName = orderRow["LastName"].ToString();
        string email = orderRow["Email"].ToString();
        string mobile = orderRow["Mobile"].ToString();
        string address = orderRow["Address"].ToString();
        string pincode = orderRow["Pincode"].ToString();
        string city = orderRow["City"].ToString();
        string state = orderRow["State"].ToString();
        string country = orderRow["Country"].ToString();
        string companyName = orderRow["CompanyName"].ToString();
        string gstNumber = orderRow["GSTNumber"].ToString();
        string payType = orderRow["PaymentType"].ToString();

        decimal grandTotal = Convert.ToDecimal(orderRow["GrandTotal"]);
        decimal exclTax = Convert.ToDecimal(orderRow["ExclTax"]);
        decimal taxAmount = Convert.ToDecimal(orderRow["TaxAmount"]);
        decimal payableNow = Convert.ToDecimal(orderRow["PayableNow"]);
        decimal codAmount = Convert.ToDecimal(orderRow["CODAmount"]);

        string paymentType = payType == "Partial" ? "Partial Payment (20% now, rest COD)" : "Full Payment";
        string grandTotalDisplay = grandTotal > 0 ? "&#8377;" + grandTotal.ToString("N2") : "Price On Request";
        string payableDisplay = payableNow > 0 ? "&#8377;" + payableNow.ToString("N2") : "Price On Request";
        string exclTaxDisplay = "&#8377;" + exclTax.ToString("N2");
        string taxDisplay = "&#8377;" + taxAmount.ToString("N2");
        string codDisplay = codAmount > 0 ? "&#8377;" + codAmount.ToString("N2") : "&#8377;0.00";

        // Items from orders_item
        List<ItemRow> items = GetOrderItems(orderGuid);

        string header = BuildEmailHeader();
        string footer = BuildEmailFooter();
        string productTable = BuildProductTable(items);
        string totalsTable = BuildTotalsTable(grandTotalDisplay, exclTaxDisplay, taxDisplay,
                                               payableDisplay, codDisplay, paymentType);
        string customerTable = BuildCustomerTable(
            firstName, lastName, email, mobile,
            address, pincode, city, state, country,
            companyName, gstNumber, paymentType);

        string fullName = HttpUtility.HtmlEncode(firstName + " " + lastName);

        string adminBody =
            header +
                "<h1 style='color:#ffffff;margin:0;font-size:20px;'>New Order Placed</h1>" +
            "</td></tr><tr><td style='padding:28px 32px;'>" +
                "<p style='font-size:15px;color:#334155;'>A new order has been placed and payment received.</p>" +
                productTable + totalsTable + customerTable +
            "</td></tr>" + footer;

        string userBody =
            header +
                "<h1 style='color:#ffffff;margin:0;font-size:20px;'>Order Confirmation</h1>" +
            "</td></tr><tr><td style='padding:28px 32px;'>" +
                "<p style='font-size:15px;color:#334155;'>Dear <strong>" + fullName + "</strong>,<br/>" +
                "Thank you! Payment received successfully. Our team will contact you shortly.</p>" +
                productTable + totalsTable + customerTable +
            "</td></tr>" + footer;

        SmtpClient smtp = BuildSmtp();

        MailMessage adminMail = new MailMessage();
        adminMail.To.Add(ConfigurationManager.AppSettings["ToMail"]);
        string cc = ConfigurationManager.AppSettings["CCMail"];
        string bcc = ConfigurationManager.AppSettings["BCCMail"];
        if (!string.IsNullOrWhiteSpace(cc)) adminMail.CC.Add(cc);
        if (!string.IsNullOrWhiteSpace(bcc)) adminMail.Bcc.Add(bcc);
        adminMail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
        adminMail.Subject = "TAMAZ Global - New Order from " + firstName + " " + lastName;
        adminMail.Body = adminBody;
        adminMail.IsBodyHtml = true;
        smtp.Send(adminMail);

        if (!string.IsNullOrWhiteSpace(email))
        {
            MailMessage userMail = new MailMessage();
            userMail.To.Add(email);
            userMail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            userMail.Subject = "Your Order Confirmation - TAMAZ Global";
            userMail.Body = userBody;
            userMail.IsBodyHtml = true;
            smtp.Send(userMail);
        }
    }

    // ── Fetch order items from orders_item ────────────────────────────────
    private List<ItemRow> GetOrderItems(string orderGuid)
    {
        List<ItemRow> list = new List<ItemRow>();
        try
        {
            using (var conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString))
            {
                string sql = "SELECT ProductName, ProductImage, UnitPrice, Qty, LineTotal FROM orders_item WHERE OrderGuid = @g";
                using (SqlCommand cmd = new SqlCommand(sql, conT))
                {
                    cmd.Parameters.AddWithValue("@g", orderGuid);
                    conT.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new ItemRow
                            {
                                ProductName = dr["ProductName"].ToString(),
                                ProductImage = dr["ProductImage"].ToString(),
                                UnitPrice = Convert.ToDecimal(dr["UnitPrice"]),
                                Qty = Convert.ToInt32(dr["Qty"]),
                                LineTotal = Convert.ToDecimal(dr["LineTotal"])
                            });
                        }
                    }
                    conT.Close();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException("/PaymentCallback.ashx", "GetOrderItems", ex.Message);
        }
        return list;
    }

    private class ItemRow
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public decimal LineTotal { get; set; }
    }

    // ── Email builder helpers ─────────────────────────────────────────────
    private string BuildProductTable(List<ItemRow> items)
    {
        string rows = "";
        foreach (var item in items)
        {
            string priceDisplay = item.UnitPrice > 0 ? "&#8377;" + item.UnitPrice.ToString("N2") : "Price On Request";
            string subtotalDisplay = item.UnitPrice > 0 ? "&#8377;" + item.LineTotal.ToString("N2") : "Price On Request";
            string imgUrl = BuildImageUrl(item.ProductImage);

            rows +=
                "<tr style='border-bottom:1px solid #eee;'>" +
                    "<td style='padding:12px 8px;vertical-align:middle;'>" +
                        (string.IsNullOrEmpty(imgUrl) ? "" :
                            "<img src='" + imgUrl + "' width='70' height='70'" +
                            " style='object-fit:cover;border-radius:6px;border:1px solid #e5e7eb;display:block;'" +
                            " alt='" + HttpUtility.HtmlEncode(item.ProductName) + "' />") +
                    "</td>" +
                    "<td style='padding:12px 8px;vertical-align:middle;font-size:14px;color:#1e293b;font-weight:600;'>" + HttpUtility.HtmlEncode(item.ProductName) + "</td>" +
                    "<td style='padding:12px 8px;vertical-align:middle;font-size:14px;color:#475569;text-align:center;'>" + priceDisplay + "</td>" +
                    "<td style='padding:12px 8px;vertical-align:middle;font-size:14px;color:#475569;text-align:center;'>" + item.Qty + "</td>" +
                    "<td style='padding:12px 8px;vertical-align:middle;font-size:14px;font-weight:700;color:#0f172a;text-align:right;'>" + subtotalDisplay + "</td>" +
                "</tr>";
        }
        return
            "<table width='100%' cellpadding='0' cellspacing='0' style='border:1px solid #e2e8f0;border-radius:8px;overflow:hidden;margin-bottom:24px;'>" +
                "<thead><tr style='background:#f8fafc;'>" +
                    "<th style='padding:10px 8px;font-size:12px;color:#64748b;font-weight:600;text-align:left;'>IMAGE</th>" +
                    "<th style='padding:10px 8px;font-size:12px;color:#64748b;font-weight:600;text-align:left;'>PRODUCT</th>" +
                    "<th style='padding:10px 8px;font-size:12px;color:#64748b;font-weight:600;text-align:center;'>PRICE</th>" +
                    "<th style='padding:10px 8px;font-size:12px;color:#64748b;font-weight:600;text-align:center;'>QTY</th>" +
                    "<th style='padding:10px 8px;font-size:12px;color:#64748b;font-weight:600;text-align:right;'>SUBTOTAL</th>" +
                "</tr></thead>" +
                "<tbody>" + rows + "</tbody>" +
            "</table>";
    }

    private string BuildTotalsTable(
        string grandTotalDisplay, string exclTaxDisplay, string taxDisplay,
        string payableDisplay, string codDisplay, string paymentType)
    {
        return
            "<table width='100%' cellpadding='0' cellspacing='0' style='margin-bottom:24px;border:1px solid #e2e8f0;border-radius:8px;overflow:hidden;'>" +
                "<tr style='background:#f8fafc;'>" +
                    "<td style='padding:10px 16px;font-size:14px;color:#475569;'>Subtotal (Excl. Tax)</td>" +
                    "<td style='padding:10px 16px;font-size:14px;font-weight:600;color:#0f172a;text-align:right;'>" + exclTaxDisplay + "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td style='padding:10px 16px;font-size:14px;color:#475569;'>Tax (18% GST)</td>" +
                    "<td style='padding:10px 16px;font-size:14px;font-weight:600;color:#0f172a;text-align:right;'>" + taxDisplay + "</td>" +
                "</tr>" +
                "<tr style='background:#f8fafc;'>" +
                    "<td style='padding:10px 16px;font-size:14px;color:#475569;'>Shipping</td>" +
                    "<td style='padding:10px 16px;font-size:14px;font-weight:600;color:#0f172a;text-align:right;'>Free</td>" +
                "</tr>" +
                                "<tr style='background:#eff6ff;'>" +
                    "<td style='padding:12px 16px;font-size:16px;font-weight:700;color:#0a1b50;'>Grand Total</td>" +
                    "<td style='padding:12px 16px;font-size:16px;font-weight:700;color:#0a1b50;text-align:right;'>" + grandTotalDisplay + "</td>" +
                "</tr>" +
                "<tr style='background:#eff6ff;'>" +
                    "<td style='padding:10px 16px;font-size:14px;color:#0a1b50;'>Amount Paid Now (" + paymentType + ")</td>" +
                    "<td style='padding:10px 16px;font-size:16px;font-weight:700;color:#0a1b50;text-align:right;'>" + payableDisplay + "</td>" +
                "</tr>" +
                (codDisplay != "&#8377;0.00" ?
                    "<tr style='background:#fffbeb;'>" +
                        "<td style='padding:10px 16px;font-size:14px;color:#92400e;'>Remaining COD (80%)</td>" +
                        "<td style='padding:10px 16px;font-size:14px;font-weight:600;color:#92400e;text-align:right;'>" + codDisplay + "</td>" +
                    "</tr>" : "") +

            "</table>";
    }

    private string BuildCustomerTable(
        string firstName, string lastName, string email, string mobile,
        string address, string pincode, string city, string state, string country,
        string companyName, string gstNumber, string paymentType)
    {
        string gstRows = "";
        if (!string.IsNullOrWhiteSpace(companyName) || !string.IsNullOrWhiteSpace(gstNumber))
        {
            gstRows =
                "<tr style='border-bottom:1px solid #f1f5f9;'>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#64748b;width:140px;'>Company</td>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + HttpUtility.HtmlEncode(companyName) + "</td>" +
                "</tr>" +
                "<tr style='border-bottom:1px solid #f1f5f9;background:#fafafa;'>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#64748b;'>GST Number</td>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + HttpUtility.HtmlEncode(gstNumber) + "</td>" +
                "</tr>";
        }

        return
            "<table width='100%' cellpadding='0' cellspacing='0' style='border:1px solid #e2e8f0;border-radius:8px;overflow:hidden;'>" +
                "<tr style='background:#f8fafc;'>" +
                    "<td colspan='2' style='padding:10px 16px;font-size:13px;font-weight:700;color:#0a1b50;letter-spacing:0.5px;'>DELIVERY DETAILS</td>" +
                "</tr>" +
                "<tr style='border-bottom:1px solid #f1f5f9;'>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#64748b;width:140px;'>Name</td>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + HttpUtility.HtmlEncode(firstName + " " + lastName) + "</td>" +
                "</tr>" +
                "<tr style='border-bottom:1px solid #f1f5f9;background:#fafafa;'>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#64748b;'>Email</td>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + HttpUtility.HtmlEncode(email) + "</td>" +
                "</tr>" +
                "<tr style='border-bottom:1px solid #f1f5f9;'>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#64748b;'>Mobile</td>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + HttpUtility.HtmlEncode(mobile) + "</td>" +
                "</tr>" +
                "<tr style='border-bottom:1px solid #f1f5f9;background:#fafafa;'>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#64748b;'>Address</td>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#1e293b;'>" + HttpUtility.HtmlEncode(address) + "</td>" +
                "</tr>" +
                "<tr style='border-bottom:1px solid #f1f5f9;'>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#64748b;'>City / Pin</td>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + HttpUtility.HtmlEncode(city + " - " + pincode) + "</td>" +
                "</tr>" +
                "<tr style='border-bottom:1px solid #f1f5f9;background:#fafafa;'>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#64748b;'>State</td>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + HttpUtility.HtmlEncode(state) + "</td>" +
                "</tr>" +
                "<tr style='border-bottom:1px solid #f1f5f9;'>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#64748b;'>Country</td>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + HttpUtility.HtmlEncode(country) + "</td>" +
                "</tr>" +
                gstRows +
                "<tr>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#64748b;'>Payment Type</td>" +
                    "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + paymentType + "</td>" +
                "</tr>" +
            "</table>";
    }

    private string BuildEmailHeader()
    {
        string domain = ConfigurationManager.AppSettings["domain"];
        if (!domain.EndsWith("/")) domain += "/";

        string logoUrl = domain + "/assests/Images/logo.png";
        return
            "<table width='100%' cellpadding='0' cellspacing='0' style='background:#f1f5f9;padding:32px 0;'>" +
            "<tr><td align='center'>" +
            "<table width='620' cellpadding='0' cellspacing='0' style='background:#ffffff;border-radius:12px;overflow:hidden;box-shadow:0 2px 12px rgba(0,0,0,0.08);'>" +
                "<tr><td style='background:#0a1b50;padding:28px 32px;text-align:center;'>" +
                    "<img src='" + logoUrl + "' height='50' alt='TAMAZ Global' style='display:block;margin:0 auto 10px;' />";
    }

    private string BuildEmailFooter()
    {
        return
            "<tr><td style='background:#f8fafc;padding:20px 32px;text-align:center;border-top:1px solid #e2e8f0;'>" +
                "<p style='margin:0 0 6px;font-size:13px;color:#94a3b8;'>&copy; " + DateTime.Now.Year + " TAMAZ Global Trading Co. All rights reserved.</p>" +
                "<a href='https://www.tamazglobal.com' style='font-size:13px;color:#0a1b50;font-weight:600;text-decoration:none;'>www.tamazglobal.com</a>" +
            "</td></tr>" +
            "</table></td></tr></table>";
    }

    private string BuildImageUrl(string rawImg)
    {
        if (string.IsNullOrEmpty(rawImg)) return "";

        // Get domain from web.config
        string domain = ConfigurationManager.AppSettings["domain"];

        if (string.IsNullOrEmpty(domain))
            domain = ""; // fallback if missing

        // Ensure domain ends with /
        if (!domain.EndsWith("/"))
            domain += "/";

        // If already full URL → return as is
        if (rawImg.StartsWith("http"))
            return rawImg;

        // Remove ~/ or leading /
        rawImg = rawImg.TrimStart('~').TrimStart('/');

        return domain + rawImg;
    }

    private SmtpClient BuildSmtp()
    {
        return new SmtpClient
        {
            Host = ConfigurationManager.AppSettings["host"],
            Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]),
            EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]),
            Credentials = new System.Net.NetworkCredential(
                ConfigurationManager.AppSettings["userName"],
                ConfigurationManager.AppSettings["password"])
        };
    }

    public bool IsReusable { get { return false; } }
}
