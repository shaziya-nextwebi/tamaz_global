using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public partial class Admin_view_invoice : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strInvoiceNo = "", strOrderDate = "", strPaymentStatus = "";
    public string strName = "", strAddressLine1 = "", strAddressLine2 = "", strAddressLine3 = "", strAddressLine4 = "";
    public string strItems = "";
    public string strSunTotalWithoutTax = "", strTax = "", strFinalAmount = "";
    public string strPaymentId = "";
    public string strPaidAmount = "";
    public string strRemainingAmount = "";
    public bool isPartial = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Request.QueryString["o"] != null)
        {
            BindInvoice();
        }
    }

    private void BindInvoice()
    {
        try
        {
            string guid = Request.QueryString["o"];

            // ─── ORDER DETAILS ─────────────────────────────
            string sql = "SELECT * FROM orders WHERE OrderGuid=@guid";

            SqlCommand cmd = new SqlCommand(sql, conT);
            cmd.Parameters.AddWithValue("@guid", guid);

            DataTable dt = new DataTable();
            new SqlDataAdapter(cmd).Fill(dt);

            if (dt.Rows.Count == 0) return;

            DataRow r = dt.Rows[0];

            strInvoiceNo = r["Id"].ToString();
            strOrderDate = Convert.ToDateTime(r["AddedOn"]).ToString("dd MMM yyyy");
            strPaymentStatus = r["PaymentStatus"].ToString();
            strPaymentId = r["RazorPaymentId"].ToString();

            // ─── CUSTOMER ─────────────────────────────
            strName = r["FirstName"] + " " + r["LastName"];

            strAddressLine1 = r["Address"].ToString();
            strAddressLine2 = "";
            strAddressLine3 = r["City"] + ", " + r["State"];
            strAddressLine4 = r["Country"] + " - " + r["Pincode"];

            // ─── TOTALS ─────────────────────────────
            decimal excl = r["ExclTax"] == DBNull.Value ? 0 : Convert.ToDecimal(r["ExclTax"]);
            decimal tax = r["TaxAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(r["TaxAmount"]);
            decimal total = r["GrandTotal"] == DBNull.Value ? 0 : Convert.ToDecimal(r["GrandTotal"]);

            strSunTotalWithoutTax = "₹" + excl.ToString("N2");
            strTax = "₹" + tax.ToString("N2");
            strFinalAmount = "₹" + total.ToString("N2");

            decimal payableNow = r["PayableNow"] == DBNull.Value ? 0 : Convert.ToDecimal(r["PayableNow"]);
            decimal codAmt = r["CODAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(r["CODAmount"]);
            string paymentType = r["PaymentType"].ToString();

            if (paymentType == "Partial" || codAmt > 0)
            {
                isPartial = true;
                strPaidAmount = "₹" + payableNow.ToString("N2");
                strRemainingAmount = "₹" + codAmt.ToString("N2");
            }
            else
            {
                isPartial = false;
                strPaidAmount = "₹" + total.ToString("N2");
            }

            // ─── ITEMS (UPDATED CLASSES) ─────────────────────────────
            string sqlItems = "SELECT * FROM orders_item WHERE OrderGuid=@guid";

            SqlCommand cmdItems = new SqlCommand(sqlItems, conT);
            cmdItems.Parameters.AddWithValue("@guid", guid);

            DataTable dtItems = new DataTable();
            new SqlDataAdapter(cmdItems).Fill(dtItems);

            int i = 1;

            foreach (DataRow ir in dtItems.Rows)
            {
                decimal price = ir["UnitPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(ir["UnitPrice"]);
                int qty = ir["Qty"] == DBNull.Value ? 0 : Convert.ToInt32(ir["Qty"]);
                decimal totalLine = ir["LineTotal"] == DBNull.Value ? 0 : Convert.ToDecimal(ir["LineTotal"]);

                // UPDATED STRING FORMAT TO MATCH NEW CSS CLASSES
                strItems += string.Format(
  @"<tr>
    <td>{0}</td>
    <td><span class=""product-name-main"">{1}</span></td>
    <td class=""text-right"">₹{2}</td>
    <td class=""text-center""><span class=""qty-pill"">{3}</span></td>
    <td class=""text-right"">₹{4}</td>
</tr>",
      i,
      ir["ProductName"],
      price.ToString("N2"),
      qty,
      totalLine.ToString("N2")
  );

                i++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "BindInvoice", ex.Message);
        }
    }
}