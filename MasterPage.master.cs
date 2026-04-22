using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public string strNavCategories = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        BindNavCategories();
    }
    public void BindNavCategories()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            List<Category> cats = Category.GetAllCategory(con);
            strNavCategories = "";

            foreach (Category c in cats)
            {
                strNavCategories +=
                    "<a href='/Category/" + c.CategoryUrl + "' class='dropdown-item'>" +
                        "<div class='dropdown-icon'>" +
                            "<svg viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2'>" +
                                "<path d='M12 2L2 7l10 5 10-5-10-5z'/>" +
                            "</svg>" +
                        "</div>" +
                        "<span class='dropdown-text'>" + c.CategoryName + "</span>" +
                    "</a>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindNavCategories", ex.Message);
        }
    }
}
