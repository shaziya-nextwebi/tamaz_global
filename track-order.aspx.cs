using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class track_order : System.Web.UI.Page
{
    // ── Use a method so connection string errors surface clearly ──
    // Change "conT" below to match your actual web.config key
    private SqlConnection GetConnection()
    {
        string key = "conT";
        if (ConfigurationManager.ConnectionStrings[key] == null)
            throw new Exception("Connection string '" + key + "' not found in web.config.");
        return new SqlConnection(ConfigurationManager.ConnectionStrings[key].ConnectionString);
    }

    // ── Public fields rendered in ASPX ──
    public string strShipmentNo = "-";
    public string strBookedDate = "-";
    public string strOrigin = "-";
    public string strDestination = "-";
    public string strPieces = "-";
    public string strStatus = "-";
    public string strStatusBadge = "";
    public string strExpectedDeliveryDate = "-";
    public string strRevExpectedDeliveryDate = "-";
    public string strPaymentType = "-";
    public string strPaymentStatus = "-";
    public string strGrandTotal = "-";
    public string strTrackDetails = "";
    public string strOrderItems = "";
    public string strCourierLink = "";

    private class TimelineStep
    {
        public string Icon { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string DateStr { get; set; }
        public string StepClass { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string tc = Request.QueryString["tc"];
                if (!string.IsNullOrEmpty(tc))
                {
                    txtOrderId.Text = tc.Trim();
                    BindTrackingDetails();
                }
            }
        }
        catch (Exception ex)
        {
            LogError("Page_Load", ex.Message);
        }
    }

    protected void btnFindOrder_Click(object sender, EventArgs e)
    {
        try
        {
            string entered = (txtOrderId != null && txtOrderId.Text != null)
                ? txtOrderId.Text.Trim() : "";

            if (string.IsNullOrEmpty(entered))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg",
                    "alert('Please enter a tracking code.');", true);
                return;
            }

            BindTrackingDetails();
        }
        catch (Exception ex)
        {
            LogError("btnFindOrder_Click", ex.Message);
        }
    }

    private void BindTrackingDetails()
    {
        SqlConnection con = null;
        try
        {
            string trackingCode = (txtOrderId != null && txtOrderId.Text != null)
                ? txtOrderId.Text.Trim() : "";

            if (string.IsNullOrEmpty(trackingCode))
            {
                ShowNotFound();
                return;
            }

            con = GetConnection();
            con.Open();

            string orderSql =
      "SELECT TOP 1 " +
      "    o.Id, o.OrderGuid, o.FirstName, o.LastName, " +
      "    o.Email, o.Mobile, o.City, o.State, o.Country, " +
      "    o.PaymentType, o.GrandTotal, o.PaymentStatus, " +
      "    o.OrderStatus, o.PaidOn, o.AddedOn, " +
      "    o.CourierName, o.TrackingCode, o.TrackingLink, o.DeliveryDate " +
      "FROM orders o " +
      "WHERE " +
      "    LTRIM(RTRIM(o.TrackingCode)) = LTRIM(RTRIM(@tc)) " +
      "    OR LTRIM(RTRIM(o.OrderGuid)) = LTRIM(RTRIM(@tc)) " +
      "ORDER BY o.Id DESC";

            DataTable dtOrder = ExecuteQuery(con, orderSql,
                new SqlParameter("@tc", trackingCode));

            if (dtOrder == null || dtOrder.Rows.Count == 0)
            {
                ShowNotFound();
                return;
            }

            DataRow row = dtOrder.Rows[0];

            // ── 2. Populate header fields ──
            strShipmentNo = trackingCode;
            //strOrigin = "-";
            //strDestination = "-";
            strBookedDate = SafeDate(row["AddedOn"], "dd-MM-yyyy", "-");
            strStatus = SafeStr(row["OrderStatus"], "-");
            strExpectedDeliveryDate = SafeDate(row["DeliveryDate"], "dd-MM-yyyy", "-");
            strPaymentType = SafeStr(row["PaymentType"], "-");
            strPaymentStatus = SafeStr(row["PaymentStatus"], "-");
            strStatusBadge = BuildStatusBadge(strStatus);

            decimal grandTotal = 0;
            if (row["GrandTotal"] != null && row["GrandTotal"] != DBNull.Value)
            {
                try { grandTotal = Convert.ToDecimal(row["GrandTotal"]); }
                catch { grandTotal = 0; }
            }
            strGrandTotal = "Rs." + grandTotal.ToString("N2");

            string orderGuid = SafeStr(row["OrderGuid"]);

            // ── 3. Fetch latest payment record ──
            if (!string.IsNullOrEmpty(orderGuid))
            {
                string paySql =
                    "SELECT TOP 1 PaymentStatus, Amount, PaymentType, AddedOn " +
                    "FROM payment " +
                    "WHERE OrderGuid = @guid " +
                    "ORDER BY Id DESC";

                DataTable dtPay = ExecuteQuery(con, paySql,
                    new SqlParameter("@guid", orderGuid));

                if (dtPay != null && dtPay.Rows.Count > 0)
                {
                    string ps = SafeStr(dtPay.Rows[0]["PaymentStatus"]);
                    if (!string.IsNullOrEmpty(ps))
                        strPaymentStatus = ps;
                }
            }

            // ── 4. Fetch order items ──
            strOrderItems = "";
            if (!string.IsNullOrEmpty(orderGuid))
            {
                // orders_item joined back to orders so we can also match by TrackingCode on the order
                string itemsSql =
                    "SELECT oi.Id, oi.OrderId, oi.ProductName, oi.Qty, " +
                    "       oi.UnitPrice, oi.LineTotal, oi.AddedOn " +
                    "FROM orders_item oi " +
                    "INNER JOIN orders o ON o.OrderGuid = oi.OrderGuid " +
                    "WHERE oi.OrderGuid = @guid " +
                    "   OR LTRIM(RTRIM(o.TrackingCode)) COLLATE SQL_Latin1_General_CP1_CI_AS = LTRIM(RTRIM(@tc2)) COLLATE SQL_Latin1_General_CP1_CI_AS " +
                    "ORDER BY oi.Id ASC";

                DataTable dtItems = ExecuteQuery(con, itemsSql,
                    new SqlParameter("@guid", orderGuid),
                    new SqlParameter("@tc2", trackingCode));

                if (dtItems != null && dtItems.Rows.Count > 0)
                {
                    strPieces = dtItems.Rows.Count.ToString();
                    orderItemDetailsDiv.Visible = true;

                    foreach (DataRow ir in dtItems.Rows)
                    {
                        decimal unitPrice = 0;
                        decimal lineTotal = 0;

                        if (ir["UnitPrice"] != null && ir["UnitPrice"] != DBNull.Value)
                        {
                            try { unitPrice = Convert.ToDecimal(ir["UnitPrice"]); }
                            catch { unitPrice = 0; }
                        }
                        if (ir["LineTotal"] != null && ir["LineTotal"] != DBNull.Value)
                        {
                            try { lineTotal = Convert.ToDecimal(ir["LineTotal"]); }
                            catch { lineTotal = 0; }
                        }

                        strOrderItems +=
                            "<tr>" +
                            "<td>" + SafeStr(ir["OrderId"]) + "</td>" +
                            "<td>" + SafeStr(ir["ProductName"], "-") + "</td>" +
                            "<td>" + SafeStr(ir["Qty"], "1") + "</td>" +
                            "<td>Rs." + unitPrice.ToString("N2") + "</td>" +
                            "<td>Rs." + lineTotal.ToString("N2") + "</td>" +
                            "</tr>";
                    }
                }
                else
                {
                    strPieces = "0";
                    orderItemDetailsDiv.Visible = false;
                }
            }
            else
            {
                strPieces = "0";
                orderItemDetailsDiv.Visible = false;
            }

            // ── 5. Build timeline ──
            BuildTimeline(row);

            // ── 6. Courier tracking link ──
            string courierName = SafeStr(row["CourierName"]);
            string trackingLink = SafeStr(row["TrackingLink"]);

            //if (!string.IsNullOrEmpty(trackingLink))
            //{
            //    string label = !string.IsNullOrEmpty(courierName)
            //        ? "Track on " + courierName
            //        : "Track on Courier Website";
            //    strCourierLink =
            //        "<a href='" + trackingLink + "' target='_blank' " +
            //        "class='tg-courier-link'>" + label + "</a>";
            //}

            orderDetailsDiv.Visible = true;
            detailDiv.Visible = true;
            noDetailsDiv.Visible = false;
        }
        catch (Exception ex)
        {
            LogError("BindTrackingDetails", ex.Message);
            ShowNotFound();
        }
        finally
        {
            if (con != null && con.State == ConnectionState.Open)
                con.Close();
        }
    }

    private void BuildTimeline(DataRow row)
    {
        string orderStatus = SafeStr(row["OrderStatus"]).Trim();
        string statusLower = orderStatus.ToLower();
        string courierName = SafeStr(row["CourierName"]);
        string trackingCode = SafeStr(row["TrackingCode"]);
        string dispatchStr = SafeDate(row["DeliveryDate"], "dd-MMM-yyyy", "");
        string bookedStr = SafeDate(row["AddedOn"], "dd-MMM-yyyy", "-");

        List<TimelineStep> steps = new List<TimelineStep>();

        // Step 1: Order Placed (always shown)
        steps.Add(new TimelineStep
        {
            Icon = "📝",
            Label = "Order Placed",
            Description = "Your order has been placed successfully.",
            DateStr = bookedStr,
            StepClass = "completed"
        });

        // Cancelled
        if (statusLower == "cancelled" || statusLower == "cancel")
        {
            steps.Add(new TimelineStep
            {
                Icon = "X",
                Label = "Order Cancelled",
                Description = "This order has been cancelled.",
                DateStr = "-",
                StepClass = "cancelled"
            });
            strTrackDetails = RenderSteps(steps, true);
            return;
        }

        // Returned
        if (statusLower == "returned" || statusLower == "return")
        {
            steps.Add(new TimelineStep
            {
                Icon = "↩",
                Label = "Returned",
                Description = "This order has been returned.",
                DateStr = "-",
                StepClass = "cancelled"
            });
            strTrackDetails = RenderSteps(steps, true);
            return;
        }

        // Payment Confirmed
        string payStatus = SafeStr(row["PaymentStatus"]).ToLower();
        if (payStatus == "paid" || payStatus == "success" || payStatus == "completed")
        {
            steps.Add(new TimelineStep
            {
                Icon = "💳",
                Label = "Payment Confirmed",
                Description = "Payment received successfully.",
                DateStr = SafeDate(row["PaidOn"], "dd-MMM-yyyy", "-"),
                StepClass = "completed"
            });
        }

        bool isProcessing = statusLower == "processing"
                         || statusLower == "confirmed"
                         || statusLower == "accepted";

        bool isDispatched = statusLower == "dispatched"
                         || statusLower == "shipped"
                         || statusLower == "in transit"
                         || statusLower == "out for delivery"
                         || statusLower == "delivered";

        bool isInTransit = statusLower == "in transit"
                         || statusLower == "out for delivery"
                         || statusLower == "delivered";

        bool isOutForDel = statusLower == "out for delivery"
                         || statusLower == "delivered";

        bool isDelivered = statusLower == "delivered";

        if (isProcessing || isDispatched)
        {
            steps.Add(new TimelineStep
            {
                Icon = "✔",
                Label = "Order Confirmed",
                Description = "Your order has been confirmed and is being prepared.",
                DateStr = "-",
                StepClass = "completed"
            });
        }

        if (isDispatched)
        {
            string desc = "Your order has been dispatched.";
            if (!string.IsNullOrEmpty(courierName)) desc += " Courier: " + courierName + ".";
            if (!string.IsNullOrEmpty(trackingCode)) desc += " Tracking: " + trackingCode + ".";

            steps.Add(new TimelineStep
            {
                Icon = "🚚",
                Label = "Dispatched",
                Description = desc,
                DateStr = !string.IsNullOrEmpty(dispatchStr) ? dispatchStr : "-",
                StepClass = "completed"
            });
        }

        if (isInTransit)
        {
            steps.Add(new TimelineStep
            {
                Icon = "🚛",
                Label = "In Transit",
                Description = "Your order is on its way to you.",
                DateStr = "-",
                StepClass = "completed"
            });
        }

        if (isOutForDel)
        {
            steps.Add(new TimelineStep
            {
                Icon = "📍",
                Label = "Out for Delivery",
                Description = "Your order is out for delivery today.",
                DateStr = "-",
                StepClass = "completed"
            });
        }

        if (isDelivered)
        {
            steps.Add(new TimelineStep
            {
                Icon = "✅",
                Label = "Delivered",
                Description = "Your order has been delivered successfully.",
                DateStr = SafeDate(row["DeliveryDate"], "dd-MMM-yyyy", "-"),
                StepClass = "active"
            });
            strTrackDetails = RenderSteps(steps, false);
            return;
        }

        // If nothing matched, add a generic current-status step
        if (!isDispatched && !isProcessing)
        {
            steps.Add(new TimelineStep
            {
                Icon = "🔹",
                Label = !string.IsNullOrEmpty(orderStatus) ? orderStatus : "Processing",
                Description = "We are working on your order.",
                DateStr = "-",
                StepClass = "active"
            });
        }
        else
        {
            // Mark the last added step as active
            if (steps.Count > 0)
                steps[steps.Count - 1].StepClass = "active";
        }

        strTrackDetails = RenderSteps(steps, false);
    }

    private string RenderSteps(List<TimelineStep> steps, bool lastIsActive)
    {
        string html = "";
        for (int i = 0; i < steps.Count; i++)
        {
            string cls = steps[i].StepClass;
            if (lastIsActive && i == steps.Count - 1)
                cls = (cls == "cancelled") ? "cancelled" : "active";

            html +=
                "<div class='tg-step " + cls + "'>" +
                  "<div class='tg-node " + cls + "'>" + steps[i].Icon + "</div>" +
                  "<div class='tg-step-body'>" +
                    "<div class='tg-step-label'>" + steps[i].Label + "</div>" +
                    "<div class='tg-step-desc'>" + steps[i].Description + "</div>" +
                    "<div class='tg-step-date'>" + steps[i].DateStr + "</div>" +
                  "</div>" +
                "</div>";
        }
        return html;
    }

    private string BuildStatusBadge(string status)
    {
        if (string.IsNullOrEmpty(status)) return "";
        string cls;
        string s = status.ToLower().Trim();
        if (s == "delivered") cls = "badge-delivered";
        else if (s == "in transit" || s == "shipped") cls = "badge-transit";
        else if (s == "dispatched" || s == "out for delivery") cls = "badge-dispatched";
        else cls = "badge-pending";
        return "<span class='tg-status-badge " + cls + "'>" + status + "</span>";
    }

    private void ShowNotFound()
    {
        orderDetailsDiv.Visible = false;
        detailDiv.Visible = false;
        orderItemDetailsDiv.Visible = false;
        noDetailsDiv.Visible = true;
    }

    private DataTable ExecuteQuery(SqlConnection con, string sql, params SqlParameter[] parameters)
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        catch (Exception ex)
        {
            LogError("ExecuteQuery", ex.Message);
            return null;
        }
    }

    // ── Safe helpers — never throw on null / DBNull ──
    private static string SafeStr(object val)
    {
        if (val == null || val == DBNull.Value) return string.Empty;
        return Convert.ToString(val).Trim();
    }

    private static string SafeStr(object val, string fallback)
    {
        if (val == null || val == DBNull.Value) return fallback;
        string s = Convert.ToString(val).Trim();
        return string.IsNullOrEmpty(s) ? fallback : s;
    }

    private static string SafeDate(object val, string format, string fallback)
    {
        if (val == null || val == DBNull.Value) return fallback;
        try { return Convert.ToDateTime(val).ToString(format); }
        catch { return fallback; }
    }

    private void LogError(string method, string message)
    {
        try
        {
            string path = "/track-order.aspx";
            if (HttpContext.Current != null && HttpContext.Current.Request != null
                && HttpContext.Current.Request.Url != null)
            {
                path = HttpContext.Current.Request.Url.PathAndQuery;
            }
            ExceptionCapture.CaptureException(path, method, message);
        }
        catch { }
    }
}