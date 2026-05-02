using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

public partial class payment_success : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        // Guard: only allow if payment was verified in this session
        if (Session["payment_verified"] == null || !(bool)Session["payment_verified"])
        {
            Response.Redirect("/", false);
            Context.ApplicationInstance.CompleteRequest();
            return;
        }

        // Clear session flags after displaying success once
        Session.Remove("payment_verified");
        Session.Remove("checkout_OrderGuid");
        Session.Remove("checkout_Email");
        Session.Remove("checkout_PaymentType");
        Session.Remove("checkout_RazorOrderId");
        Session.Remove("razorpay_payment_id");
    }
}