using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;

public partial class Admin_dashboard : System.Web.UI.Page
{
    public string Strusername = "",
                  strTotalProduct = "",
                  strTotalOrder = "",
                  strBrand = "",
                  strBlogs = "",
                  strTotalCustomer = "",
                  strContact = "",
                  strToday = "",
          strTotalOrders = "",
              strPendingOrders = "",
              strDeliveredOrders = "",
              strDispatchedOrders = "";

    // Helper — always returns a fresh connection
    private SqlConnection NewCon()
    {
        return new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["t_aid"] == null)
        {
            Response.Redirect("Default.aspx", false);
            return;
        }
        try
        {
            BindUserName();
            strTotalProduct = DashBoard.GetProductCount(NewCon()).ToString();
            strTotalOrder = DashBoard.GetCategoryCount(NewCon()).ToString();
            strBrand = DashBoard.GetBrnadCount(NewCon()).ToString();
            strBlogs = DashBoard.NoOfBlogs(NewCon()).ToString();
            strTotalCustomer = DashBoard.GetStudentCount(NewCon()).ToString();
            strContact = DashBoard.ContactUs(NewCon()).ToString();
            strToday = DashBoard.ProductEnquiry(NewCon()).ToString();
            strTotalOrders = DashBoard.GetOrderCount(NewCon(), "").ToString();
            strPendingOrders = DashBoard.GetOrderCount(NewCon(), "Pending").ToString();
            strDispatchedOrders = DashBoard.GetOrderCount(NewCon(), "Dispatched").ToString();
            strDeliveredOrders = DashBoard.GetOrderCount(NewCon(), "Delivered").ToString();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Page_Load", ex.Message);
        }
    }

    public void BindUserName()
    {
        try
        {
            Strusername = CreateUser.GetLoggedUserName(NewCon(), Request.Cookies["t_aid"].Value);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindUserName", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static MonthlyChart DashBoardD()
    {
        SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
        return DashBoard.GetMonthlyValue(conT);
    }
}