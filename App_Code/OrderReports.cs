using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

// ─────────────────────────────────────────────────────────────────────────────
//  OrderReports.cs  —  App_Code helper for orders / orders_item / payment
//  Compatible with .NET 4.0 / 4.5  (no $ strings, no => members, no ?. operator)
// ─────────────────────────────────────────────────────────────────────────────

#region ── Model classes ──────────────────────────────────────────────────────

public class OrderRow
{
    private static readonly System.Globalization.CultureInfo _in =
        new System.Globalization.CultureInfo("en-IN");

    public int Id { get; set; }
    public string OrderGuid { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Pincode { get; set; }
    public string Country { get; set; }
    public string CompanyName { get; set; }
    public string GSTNumber { get; set; }
    public string PaymentType { get; set; }
    public decimal GrandTotal { get; set; }
    public decimal ExclTax { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal PayableNow { get; set; }
    public decimal CODAmount { get; set; }
    public string RazorOrderId { get; set; }
    public string RazorPaymentId { get; set; }
    public string OrderStatus { get; set; }
    public string PaymentStatus { get; set; }
    public DateTime? PaidOn { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }

    public List<OrderItemRow> Items { get; set; }
    public List<PaymentRow> Payments { get; set; }

    public OrderRow()
    {
        Items = new List<OrderItemRow>();
        Payments = new List<PaymentRow>();
    }

    // ── Computed string properties (no => shorthand) ──────────────────
    public string FullName
    {
        get { return ((FirstName ?? "") + " " + (LastName ?? "")).Trim(); }
    }

    public string FullAddress
    {
        get
        {
            return (Address ?? "") + ", " + (City ?? "") + " - " +
                   (Pincode ?? "") + ", " + (State ?? "") + ", " + (Country ?? "");
        }
    }

    public string GrandTotalFmt { get { return "Rs." + GrandTotal.ToString("N2", _in); } }
    public string ExclTaxFmt { get { return "Rs." + ExclTax.ToString("N2", _in); } }
    public string TaxAmountFmt { get { return "Rs." + TaxAmount.ToString("N2", _in); } }
    public string PayableNowFmt { get { return "Rs." + PayableNow.ToString("N2", _in); } }
    public string CODAmountFmt { get { return "Rs." + CODAmount.ToString("N2", _in); } }

    public string PaidOnFmt
    {
        get
        {
            if (PaidOn.HasValue)
                return PaidOn.Value.ToString("dd MMM yyyy hh:mm tt");
            return "-";
        }
    }

    public string AddedOnFmt
    {
        get { return AddedOn.ToString("dd MMM yyyy hh:mm tt"); }
    }
}

// ─────────────────────────────────────────────────────────────────────────────

public class OrderItemRow
{
    private static readonly System.Globalization.CultureInfo _in =
        new System.Globalization.CultureInfo("en-IN");

    public int Id { get; set; }
    public int OrderId { get; set; }
    public string OrderGuid { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public string ProductUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public int Qty { get; set; }
    public decimal LineTotal { get; set; }
    public decimal ExclTax { get; set; }
    public decimal TaxAmount { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }

    public string UnitPriceFmt { get { return "Rs." + UnitPrice.ToString("N2", _in); } }
    public string LineTotalFmt { get { return "Rs." + LineTotal.ToString("N2", _in); } }
    public string ExclTaxFmt { get { return "Rs." + ExclTax.ToString("N2", _in); } }
    public string TaxAmtFmt { get { return "Rs." + TaxAmount.ToString("N2", _in); } }
}

// ─────────────────────────────────────────────────────────────────────────────

public class PaymentRow
{
    private static readonly System.Globalization.CultureInfo _in =
        new System.Globalization.CultureInfo("en-IN");

    public int Id { get; set; }
    public int OrderId { get; set; }
    public string OrderGuid { get; set; }
    public string RazorPaymentId { get; set; }
    public string RazorOrderId { get; set; }
    public string Signature { get; set; }
    public decimal Amount { get; set; }
    public string PaymentType { get; set; }
    public string PaymentStatus { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }

    public string AmountFmt { get { return "Rs." + Amount.ToString("N2", _in); } }
}

// ─────────────────────────────────────────────────────────────────────────────

public class OrderOpResult
{
    public bool Success { get; set; }
    public string Message { get; set; }

    public static OrderOpResult Ok()
    {
        OrderOpResult r = new OrderOpResult();
        r.Success = true;
        r.Message = "Success";
        return r;
    }

    public static OrderOpResult Ok(string msg)
    {
        OrderOpResult r = new OrderOpResult();
        r.Success = true;
        r.Message = msg;
        return r;
    }

    public static OrderOpResult Fail(string msg)
    {
        OrderOpResult r = new OrderOpResult();
        r.Success = false;
        r.Message = msg;
        return r;
    }
}

// ─────────────────────────────────────────────────────────────────────────────

public class OrderSummary
{
    public int TotalOrders { get; set; }
    public int Pending { get; set; }
    public int Confirmed { get; set; }
    public int Shipped { get; set; }
    public int Delivered { get; set; }
    public int Cancelled { get; set; }
    public int Paid { get; set; }
    public int PartialPaid { get; set; }
    public int Unpaid { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal PaidRevenue { get; set; }
}

public class MonthlyRevenuePoint
{
    public string Label { get; set; }
    public decimal Revenue { get; set; }
    public int OrderCount { get; set; }
}

#endregion

// ─────────────────────────────────────────────────────────────────────────────
//  Static helper class
// ─────────────────────────────────────────────────────────────────────────────

public static class OrderReports
{
    private static SqlConnection Conn()
    {
        return new SqlConnection(
            ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    }

    // ─────────────────────────────────────────────────────────────────────────
    #region ── READ — orders ────────────────────────────────────────────────────

    public static List<OrderRow> GetAllOrders()
    {
        List<OrderRow> list = new List<OrderRow>();
        try
        {
            string sql = "SELECT Id, OrderGuid, FirstName, LastName, Email, Mobile," +
                         " Address, City, State, Pincode, Country," +
                         " CompanyName, GSTNumber," +
                         " PaymentType, GrandTotal, ExclTax, TaxAmount, PayableNow, CODAmount," +
                         " RazorOrderId, RazorPaymentId," +
                         " OrderStatus, PaymentStatus, PaidOn, AddedOn, AddedIp" +
                         " FROM orders" +
                         " WHERE OrderStatus <> 'Deleted'" +
                         " ORDER BY Id DESC";

            using (SqlConnection con = Conn())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        list.Add(MapOrder(rdr));
                }
            }
        }
        catch (Exception ex) { Capture("GetAllOrders", ex); }
        return list;
    }

    public static List<OrderRow> GetOrdersWithFilters(
        string orderStatus,
        string paymentStatus,
        string searchParam,
        DateTime? fromDate,
        DateTime? toDate)
    {
        List<OrderRow> list = new List<OrderRow>();
        try
        {
            string sql = "SELECT Id, OrderGuid, FirstName, LastName, Email, Mobile," +
                         " Address, City, State, Pincode, Country," +
                         " CompanyName, GSTNumber," +
                         " PaymentType, GrandTotal, ExclTax, TaxAmount, PayableNow, CODAmount," +
                         " RazorOrderId, RazorPaymentId," +
                         " OrderStatus, PaymentStatus, PaidOn, AddedOn, AddedIp" +
                         " FROM orders" +
                         " WHERE OrderStatus <> 'Deleted'" +
                         "   AND (@oStatus = '' OR OrderStatus   = @oStatus)" +
                         "   AND (@pStatus = '' OR PaymentStatus = @pStatus)" +
                         "   AND (@param   = '' OR (" +
                         "        FirstName LIKE '%' + @param + '%' OR" +
                         "        LastName  LIKE '%' + @param + '%' OR" +
                         "        Email     LIKE '%' + @param + '%' OR" +
                         "        Mobile    LIKE '%' + @param + '%' OR" +
                         "        CAST(Id AS NVARCHAR) = @param" +
                         "   ))" +
                         "   AND (@fromDate IS NULL OR AddedOn >= @fromDate)" +
                         "   AND (@toDate   IS NULL OR AddedOn <  DATEADD(DAY, 1, @toDate))" +
                         " ORDER BY Id DESC";

            using (SqlConnection con = Conn())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@oStatus", orderStatus ?? "");
                cmd.Parameters.AddWithValue("@pStatus", paymentStatus ?? "");
                cmd.Parameters.AddWithValue("@param", searchParam ?? "");

                SqlParameter pFrom = cmd.Parameters.Add("@fromDate", SqlDbType.DateTime);
                pFrom.Value = fromDate.HasValue ? (object)fromDate.Value : DBNull.Value;

                SqlParameter pTo = cmd.Parameters.Add("@toDate", SqlDbType.DateTime);
                pTo.Value = toDate.HasValue ? (object)toDate.Value : DBNull.Value;

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        list.Add(MapOrder(rdr));
                }
            }
        }
        catch (Exception ex) { Capture("GetOrdersWithFilters", ex); }
        return list;
    }

    public static OrderRow GetSingleOrder(string orderGuid)
    {
        OrderRow order = null;
        try
        {
            string sql = "SELECT Id, OrderGuid, FirstName, LastName, Email, Mobile," +
                         " Address, City, State, Pincode, Country," +
                         " CompanyName, GSTNumber," +
                         " PaymentType, GrandTotal, ExclTax, TaxAmount, PayableNow, CODAmount," +
                         " RazorOrderId, RazorPaymentId," +
                         " OrderStatus, PaymentStatus, PaidOn, AddedOn, AddedIp" +
                         " FROM orders" +
                         " WHERE OrderGuid = @guid AND OrderStatus <> 'Deleted'";

            using (SqlConnection con = Conn())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@guid", orderGuid);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                            order = MapOrder(rdr);
                    }
                }
                if (order == null) return null;
                order.Items = GetOrderItems(orderGuid, con);
                order.Payments = GetPayments(orderGuid, con);
            }
        }
        catch (Exception ex) { Capture("GetSingleOrder", ex); }
        return order;
    }

    public static OrderRow GetSingleOrderById(int id)
    {
        OrderRow order = null;
        try
        {
            string sql = "SELECT Id, OrderGuid, FirstName, LastName, Email, Mobile," +
                         " Address, City, State, Pincode, Country," +
                         " CompanyName, GSTNumber," +
                         " PaymentType, GrandTotal, ExclTax, TaxAmount, PayableNow, CODAmount," +
                         " RazorOrderId, RazorPaymentId," +
                         " OrderStatus, PaymentStatus, PaidOn, AddedOn, AddedIp" +
                         " FROM orders" +
                         " WHERE Id = @id AND OrderStatus <> 'Deleted'";

            using (SqlConnection con = Conn())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                            order = MapOrder(rdr);
                    }
                }
                if (order == null) return null;
                order.Items = GetOrderItems(order.OrderGuid, con);
                order.Payments = GetPayments(order.OrderGuid, con);
            }
        }
        catch (Exception ex) { Capture("GetSingleOrderById", ex); }
        return order;
    }

    #endregion

    // ─────────────────────────────────────────────────────────────────────────
    #region ── READ — order items ───────────────────────────────────────────────

    public static List<OrderItemRow> GetOrderItems(string orderGuid)
    {
        using (SqlConnection con = Conn())
        {
            con.Open();
            return GetOrderItems(orderGuid, con);
        }
    }

    public static List<OrderItemRow> GetOrderItems(string orderGuid, SqlConnection con)
    {
        List<OrderItemRow> list = new List<OrderItemRow>();
        try
        {
            string sql = "SELECT Id, OrderId, OrderGuid," +
                         " ProductId, ProductName, ProductImage, ProductUrl," +
                         " UnitPrice, Qty, LineTotal, ExclTax, TaxAmount," +
                         " AddedOn, AddedIp" +
                         " FROM orders_item" +
                         " WHERE OrderGuid = @guid" +
                         " ORDER BY Id ASC";

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@guid", orderGuid);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        list.Add(MapItem(rdr));
                }
            }
        }
        catch (Exception ex) { Capture("GetOrderItems", ex); }
        return list;
    }

    public static DataTable GetAllOrderItemsRaw()
    {
        DataTable dt = new DataTable();
        try
        {
            string sql = "SELECT OrderGuid, ProductId, ProductName, ProductImage, ProductUrl," +
                         " UnitPrice, Qty, LineTotal, ExclTax, TaxAmount" +
                         " FROM orders_item ORDER BY Id ASC";

            using (SqlConnection con = Conn())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                new SqlDataAdapter(cmd).Fill(dt);
            }
        }
        catch (Exception ex) { Capture("GetAllOrderItemsRaw", ex); }
        return dt;
    }

    #endregion

    // ─────────────────────────────────────────────────────────────────────────
    #region ── READ — payments ──────────────────────────────────────────────────

    public static List<PaymentRow> GetPayments(string orderGuid)
    {
        using (SqlConnection con = Conn())
        {
            con.Open();
            return GetPayments(orderGuid, con);
        }
    }

    public static List<PaymentRow> GetPayments(string orderGuid, SqlConnection con)
    {
        List<PaymentRow> list = new List<PaymentRow>();
        try
        {
            string sql = "SELECT Id, OrderId, OrderGuid," +
                         " RazorPaymentId, RazorOrderId, Signature," +
                         " Amount, PaymentType, PaymentStatus," +
                         " AddedOn, AddedIp" +
                         " FROM payment" +
                         " WHERE OrderGuid = @guid" +
                         " ORDER BY Id DESC";

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@guid", orderGuid);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        list.Add(MapPayment(rdr));
                }
            }
        }
        catch (Exception ex) { Capture("GetPayments", ex); }
        return list;
    }

    public static DataTable GetAllPaymentsRaw()
    {
        DataTable dt = new DataTable();
        try
        {
            string sql = "SELECT OrderGuid, RazorPaymentId, RazorOrderId," +
                         " Amount, PaymentType, PaymentStatus, AddedOn AS PayAddedOn" +
                         " FROM payment ORDER BY Id DESC";

            using (SqlConnection con = Conn())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                new SqlDataAdapter(cmd).Fill(dt);
            }
        }
        catch (Exception ex) { Capture("GetAllPaymentsRaw", ex); }
        return dt;
    }

    #endregion

    // ─────────────────────────────────────────────────────────────────────────
    #region ── WRITE ─────────────────────────────────────────────────────────────

    public static OrderOpResult UpdateOrderStatus(string orderGuid, string newStatus)
    {
        string[] allowed = new string[] { "Pending", "Confirmed", "Shipped", "Delivered", "Cancelled", "Deleted" };
        bool valid = false;
        foreach (string s in allowed)
        {
            if (string.Compare(s, newStatus, StringComparison.OrdinalIgnoreCase) == 0)
            { valid = true; break; }
        }
        if (!valid)
            return OrderOpResult.Fail("Invalid status: " + newStatus);

        try
        {
            string sql = "UPDATE orders SET OrderStatus = @status" +
                         " WHERE OrderGuid = @guid AND OrderStatus <> 'Deleted'";

            using (SqlConnection con = Conn())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@guid", orderGuid);
                cmd.Parameters.AddWithValue("@status", newStatus);
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0
                    ? OrderOpResult.Ok()
                    : OrderOpResult.Fail("No rows updated.");
            }
        }
        catch (Exception ex)
        {
            Capture("UpdateOrderStatus", ex);
            return OrderOpResult.Fail(ex.Message);
        }
    }

    public static OrderOpResult UpdatePaymentStatus(string orderGuid, string newStatus)
    {
        return UpdatePaymentStatus(orderGuid, newStatus, null);
    }

    public static OrderOpResult UpdatePaymentStatus(string orderGuid, string newStatus, DateTime? paidOn)
    {
        string[] allowed = new string[] { "Pending", "Paid", "Partial", "Failed", "Refunded" };
        bool valid = false;
        foreach (string s in allowed)
        {
            if (string.Compare(s, newStatus, StringComparison.OrdinalIgnoreCase) == 0)
            { valid = true; break; }
        }
        if (!valid)
            return OrderOpResult.Fail("Invalid payment status: " + newStatus);

        try
        {
            string sql = paidOn.HasValue
                ? "UPDATE orders SET PaymentStatus = @status, PaidOn = @paidOn WHERE OrderGuid = @guid"
                : "UPDATE orders SET PaymentStatus = @status WHERE OrderGuid = @guid";

            using (SqlConnection con = Conn())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@guid", orderGuid);
                cmd.Parameters.AddWithValue("@status", newStatus);
                if (paidOn.HasValue)
                    cmd.Parameters.AddWithValue("@paidOn", paidOn.Value);
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0
                    ? OrderOpResult.Ok()
                    : OrderOpResult.Fail("No rows updated.");
            }
        }
        catch (Exception ex)
        {
            Capture("UpdatePaymentStatus", ex);
            return OrderOpResult.Fail(ex.Message);
        }
    }

    public static OrderOpResult DeleteOrder(string orderGuid)
    {
        try
        {
            string sql = "UPDATE orders SET OrderStatus = 'Deleted'" +
                         " WHERE OrderGuid = @guid AND OrderStatus <> 'Deleted'";

            using (SqlConnection con = Conn())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@guid", orderGuid);
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0
                    ? OrderOpResult.Ok()
                    : OrderOpResult.Fail("Order not found or already deleted.");
            }
        }
        catch (Exception ex)
        {
            Capture("DeleteOrder", ex);
            return OrderOpResult.Fail(ex.Message);
        }
    }

    public static OrderOpResult RecordPayment(PaymentRow p)
    {
        try
        {
            string sqlPay =
                "INSERT INTO payment" +
                " (OrderId, OrderGuid, RazorPaymentId, RazorOrderId, Signature," +
                "  Amount, PaymentType, PaymentStatus, AddedOn, AddedIp)" +
                " VALUES" +
                " (@orderId, @guid, @razorPayId, @razorOrdId, @sig," +
                "  @amount, @payType, @payStatus, GETUTCDATE(), @ip)";

            string sqlOrder =
                "UPDATE orders" +
                " SET PaymentStatus = @payStatus, RazorPaymentId = @razorPayId, PaidOn = GETUTCDATE()" +
                " WHERE OrderGuid = @guid";

            using (SqlConnection con = Conn())
            {
                con.Open();
                SqlTransaction trx = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlPay, con, trx))
                    {
                        cmd.Parameters.AddWithValue("@orderId", p.OrderId);
                        cmd.Parameters.AddWithValue("@guid", p.OrderGuid);
                        cmd.Parameters.AddWithValue("@razorPayId", p.RazorPaymentId);
                        cmd.Parameters.AddWithValue("@razorOrdId", p.RazorOrderId);
                        cmd.Parameters.AddWithValue("@sig", p.Signature);
                        cmd.Parameters.AddWithValue("@amount", p.Amount);
                        cmd.Parameters.AddWithValue("@payType", p.PaymentType);
                        cmd.Parameters.AddWithValue("@payStatus", p.PaymentStatus);
                        cmd.Parameters.AddWithValue("@ip", p.AddedIp ?? "");
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd2 = new SqlCommand(sqlOrder, con, trx))
                    {
                        cmd2.Parameters.AddWithValue("@payStatus", p.PaymentStatus);
                        cmd2.Parameters.AddWithValue("@razorPayId", p.RazorPaymentId);
                        cmd2.Parameters.AddWithValue("@guid", p.OrderGuid);
                        cmd2.ExecuteNonQuery();
                    }
                    trx.Commit();
                    return OrderOpResult.Ok();
                }
                catch
                {
                    trx.Rollback();
                    throw;
                }
            }
        }
        catch (Exception ex)
        {
            Capture("RecordPayment", ex);
            return OrderOpResult.Fail(ex.Message);
        }
    }

    #endregion

    // ─────────────────────────────────────────────────────────────────────────
    #region ── DASHBOARD ────────────────────────────────────────────────────────

    public static OrderSummary GetDashboardSummary()
    {
        return GetDashboardSummary(null, null);
    }

    public static OrderSummary GetDashboardSummary(DateTime? fromDate, DateTime? toDate)
    {
        OrderSummary s = new OrderSummary();
        try
        {
            string sql =
                "SELECT" +
                " COUNT(*) AS TotalOrders," +
                " SUM(CASE WHEN OrderStatus   = 'Pending'   THEN 1 ELSE 0 END) AS Pending," +
                " SUM(CASE WHEN OrderStatus   = 'Confirmed' THEN 1 ELSE 0 END) AS Confirmed," +
                " SUM(CASE WHEN OrderStatus   = 'Shipped'   THEN 1 ELSE 0 END) AS Shipped," +
                " SUM(CASE WHEN OrderStatus   = 'Delivered' THEN 1 ELSE 0 END) AS Delivered," +
                " SUM(CASE WHEN OrderStatus   = 'Cancelled' THEN 1 ELSE 0 END) AS Cancelled," +
                " SUM(CASE WHEN PaymentStatus = 'Paid'      THEN 1 ELSE 0 END) AS Paid," +
                " SUM(CASE WHEN PaymentStatus = 'Partial'   THEN 1 ELSE 0 END) AS PartialPaid," +
                " SUM(CASE WHEN PaymentStatus = 'Pending'   THEN 1 ELSE 0 END) AS Unpaid," +
                " ISNULL(SUM(GrandTotal), 0) AS TotalRevenue," +
                " ISNULL(SUM(CASE WHEN PaymentStatus = 'Paid' THEN GrandTotal ELSE 0 END), 0) AS PaidRevenue" +
                " FROM orders" +
                " WHERE OrderStatus <> 'Deleted'" +
                "   AND (@fromDate IS NULL OR AddedOn >= @fromDate)" +
                "   AND (@toDate   IS NULL OR AddedOn <  DATEADD(DAY, 1, @toDate))";

            using (SqlConnection con = Conn())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                SqlParameter pFrom = cmd.Parameters.Add("@fromDate", SqlDbType.DateTime);
                pFrom.Value = fromDate.HasValue ? (object)fromDate.Value : DBNull.Value;

                SqlParameter pTo = cmd.Parameters.Add("@toDate", SqlDbType.DateTime);
                pTo.Value = toDate.HasValue ? (object)toDate.Value : DBNull.Value;

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        s.TotalOrders = SafeInt(rdr, "TotalOrders");
                        s.Pending = SafeInt(rdr, "Pending");
                        s.Confirmed = SafeInt(rdr, "Confirmed");
                        s.Shipped = SafeInt(rdr, "Shipped");
                        s.Delivered = SafeInt(rdr, "Delivered");
                        s.Cancelled = SafeInt(rdr, "Cancelled");
                        s.Paid = SafeInt(rdr, "Paid");
                        s.PartialPaid = SafeInt(rdr, "PartialPaid");
                        s.Unpaid = SafeInt(rdr, "Unpaid");
                        s.TotalRevenue = SafeDec(rdr, "TotalRevenue");
                        s.PaidRevenue = SafeDec(rdr, "PaidRevenue");
                    }
                }
            }
        }
        catch (Exception ex) { Capture("GetDashboardSummary", ex); }
        return s;
    }

    public static List<MonthlyRevenuePoint> GetMonthlyRevenue(int months)
    {
        List<MonthlyRevenuePoint> list = new List<MonthlyRevenuePoint>();
        try
        {
            string sql =
                "SELECT YEAR(AddedOn) AS Yr, MONTH(AddedOn) AS Mo," +
                " ISNULL(SUM(GrandTotal), 0) AS Revenue, COUNT(*) AS OrderCount" +
                " FROM orders" +
                " WHERE OrderStatus <> 'Deleted'" +
                "   AND AddedOn >= DATEADD(MONTH, -@months, GETUTCDATE())" +
                " GROUP BY YEAR(AddedOn), MONTH(AddedOn)" +
                " ORDER BY Yr ASC, Mo ASC";

            using (SqlConnection con = Conn())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@months", months);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int yr = SafeInt(rdr, "Yr");
                        int mo = SafeInt(rdr, "Mo");
                        MonthlyRevenuePoint pt = new MonthlyRevenuePoint();
                        pt.Label = new DateTime(yr, mo, 1).ToString("MMM yy");
                        pt.Revenue = SafeDec(rdr, "Revenue");
                        pt.OrderCount = SafeInt(rdr, "OrderCount");
                        list.Add(pt);
                    }
                }
            }
        }
        catch (Exception ex) { Capture("GetMonthlyRevenue", ex); }
        return list;
    }

    #endregion

    // ─────────────────────────────────────────────────────────────────────────
    #region ── EXPORT ───────────────────────────────────────────────────────────

    public static DataTable GetOrdersForExport(
        string orderStatus,
        string payStatus,
        DateTime? fromDate,
        DateTime? toDate)
    {
        DataTable dt = new DataTable();
        try
        {
            string sql =
                "SELECT o.Id, o.OrderGuid," +
                " o.FirstName + ' ' + o.LastName AS CustomerName," +
                " o.Email, o.Mobile," +
                " o.Address, o.City, o.State, o.Pincode, o.Country," +
                " o.CompanyName, o.GSTNumber," +
                " o.PaymentType," +
                " o.GrandTotal, o.ExclTax, o.TaxAmount, o.PayableNow, o.CODAmount," +
                " o.RazorOrderId, o.RazorPaymentId," +
                " o.OrderStatus, o.PaymentStatus," +
                " o.PaidOn, o.AddedOn, o.AddedIp," +
                " (SELECT COUNT(*) FROM orders_item oi WHERE oi.OrderGuid = o.OrderGuid) AS ItemCount" +
                " FROM orders o" +
                " WHERE o.OrderStatus <> 'Deleted'" +
                "   AND (@oStatus = '' OR o.OrderStatus   = @oStatus)" +
                "   AND (@pStatus = '' OR o.PaymentStatus = @pStatus)" +
                "   AND (@fromDate IS NULL OR o.AddedOn >= @fromDate)" +
                "   AND (@toDate   IS NULL OR o.AddedOn <  DATEADD(DAY, 1, @toDate))" +
                " ORDER BY o.Id DESC";

            using (SqlConnection con = Conn())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@oStatus", orderStatus ?? "");
                cmd.Parameters.AddWithValue("@pStatus", payStatus ?? "");

                SqlParameter pFrom = cmd.Parameters.Add("@fromDate", SqlDbType.DateTime);
                pFrom.Value = fromDate.HasValue ? (object)fromDate.Value : DBNull.Value;

                SqlParameter pTo = cmd.Parameters.Add("@toDate", SqlDbType.DateTime);
                pTo.Value = toDate.HasValue ? (object)toDate.Value : DBNull.Value;

                con.Open();
                new SqlDataAdapter(cmd).Fill(dt);
            }
        }
        catch (Exception ex) { Capture("GetOrdersForExport", ex); }
        return dt;
    }

    #endregion

    // ─────────────────────────────────────────────────────────────────────────
    #region ── Private helpers ───────────────────────────────────────────────────

    private static OrderRow MapOrder(SqlDataReader r)
    {
        OrderRow o = new OrderRow();
        o.Id = Convert.ToInt32(r["Id"]);
        o.OrderGuid = r["OrderGuid"].ToString();
        o.FirstName = r["FirstName"].ToString();
        o.LastName = r["LastName"].ToString();
        o.Email = r["Email"].ToString();
        o.Mobile = r["Mobile"].ToString();
        o.Address = r["Address"].ToString();
        o.City = r["City"].ToString();
        o.State = r["State"].ToString();
        o.Pincode = r["Pincode"].ToString();
        o.Country = r["Country"].ToString();
        o.CompanyName = r["CompanyName"].ToString();
        o.GSTNumber = r["GSTNumber"].ToString();
        o.PaymentType = r["PaymentType"].ToString();
        o.GrandTotal = SafeDec(r, "GrandTotal");
        o.ExclTax = SafeDec(r, "ExclTax");
        o.TaxAmount = SafeDec(r, "TaxAmount");
        o.PayableNow = SafeDec(r, "PayableNow");
        o.CODAmount = SafeDec(r, "CODAmount");
        o.RazorOrderId = r["RazorOrderId"].ToString();
        o.RazorPaymentId = r["RazorPaymentId"].ToString();
        o.OrderStatus = r["OrderStatus"].ToString();
        o.PaymentStatus = r["PaymentStatus"].ToString();
        o.PaidOn = r["PaidOn"] == DBNull.Value
                               ? (DateTime?)null
                               : Convert.ToDateTime(r["PaidOn"]);
        o.AddedOn = Convert.ToDateTime(r["AddedOn"]);
        o.AddedIp = r["AddedIp"].ToString();
        return o;
    }

    private static OrderItemRow MapItem(SqlDataReader r)
    {
        OrderItemRow it = new OrderItemRow();
        it.Id = Convert.ToInt32(r["Id"]);
        it.OrderId = Convert.ToInt32(r["OrderId"]);
        it.OrderGuid = r["OrderGuid"].ToString();
        it.ProductId = Convert.ToInt32(r["ProductId"]);
        it.ProductName = r["ProductName"].ToString();
        it.ProductImage = r["ProductImage"].ToString();
        it.ProductUrl = r["ProductUrl"].ToString();
        it.UnitPrice = SafeDec(r, "UnitPrice");
        it.Qty = Convert.ToInt32(r["Qty"]);
        it.LineTotal = SafeDec(r, "LineTotal");
        it.ExclTax = SafeDec(r, "ExclTax");
        it.TaxAmount = SafeDec(r, "TaxAmount");
        it.AddedOn = Convert.ToDateTime(r["AddedOn"]);
        it.AddedIp = r["AddedIp"].ToString();
        return it;
    }

    private static PaymentRow MapPayment(SqlDataReader r)
    {
        PaymentRow p = new PaymentRow();
        p.Id = Convert.ToInt32(r["Id"]);
        p.OrderId = Convert.ToInt32(r["OrderId"]);
        p.OrderGuid = r["OrderGuid"].ToString();
        p.RazorPaymentId = r["RazorPaymentId"].ToString();
        p.RazorOrderId = r["RazorOrderId"].ToString();
        p.Signature = r["Signature"].ToString();
        p.Amount = SafeDec(r, "Amount");
        p.PaymentType = r["PaymentType"].ToString();
        p.PaymentStatus = r["PaymentStatus"].ToString();
        p.AddedOn = Convert.ToDateTime(r["AddedOn"]);
        p.AddedIp = r["AddedIp"].ToString();
        return p;
    }

    private static decimal SafeDec(IDataRecord r, string col)
    {
        if (r[col] == DBNull.Value) return 0m;
        return Convert.ToDecimal(r[col]);
    }

    private static int SafeInt(IDataRecord r, string col)
    {
        if (r[col] == DBNull.Value) return 0;
        return Convert.ToInt32(r[col]);
    }

    private static void Capture(string method, Exception ex)
    {
        try
        {
            string path = "/App_Code/OrderReports.cs";
            if (HttpContext.Current != null &&
                HttpContext.Current.Request != null &&
                HttpContext.Current.Request.Url != null)
            {
                path = HttpContext.Current.Request.Url.PathAndQuery;
            }
            ExceptionCapture.CaptureException(path, method, ex.Message);
        }
        catch { }
    }

    #endregion
}