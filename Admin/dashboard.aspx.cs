using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;

public partial class Admin_dashboard : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string Strusername = "",
                  strTotalProduct = "",
                  strTotalOrder = "",
                  strBrand = "",
                  strBlogs = "",
                  strTotalCustomer = "",
                  strContact = "",
                  strToday = "";

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
            strTotalProduct = DashBoard.GetProductCount(conT).ToString();
            strTotalOrder = DashBoard.GetCategoryCount(conT).ToString();
            strBrand = DashBoard.GetBrnadCount(conT).ToString();
            strBlogs = DashBoard.NoOfBlogs(conT).ToString();
            strTotalCustomer = DashBoard.GetStudentCount(conT).ToString();
            strContact = DashBoard.ContactUs(conT).ToString();
            strToday = DashBoard.ProductEnquiry(conT).ToString();
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
            Strusername = CreateUser.GetLoggedUserName(conT, Request.Cookies["t_aid"].Value);
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