using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sitemap : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    public string strBlogs = "";
    public string strCategories = "";
    public string strProducts = "";
    public string strTopBrands = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        BindAllCategories();
        BindAllBlogs();
        BindAllTopBrands();
        BindAllProducts();
    }
    public void BindAllCategories()
    {
        try
        {
            strCategories = "";
            List<Category> BD = Category.GetAllCategory(con).ToList();
            for (int i = 0; i < BD.Count; i++)
            {
                var url = "https://tamazglobal.com/Category/" + BD[i].CategoryUrl;
                strCategories += @"<div class='link-item'><span class='dot'></span>
                    <a href='" + url + @"'>" + BD[i].CategoryName + @"</a>
                </div>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindAllBlogs", ex.Message);
        }
    }
    public void BindAllBlogs()
    {
        try
        {
            strBlogs = "";
            List<Blogs> BD = Blogs.GetAllBlog(con).ToList();
            for (int i = 0; i < BD.Count; i++)
            {
                var url = "https://tamazglobal.com/blog/" + BD[i].BlogUrl;
                strBlogs += @"<div class='link-item'><span class='dot'></span>
                    <a href='" + url + @"'>" + BD[i].BlogName + @"</a>
                </div>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindAllBlogs", ex.Message);
        }
    }
    public void BindAllTopBrands()
    {
        try
        {
            strTopBrands = "";
            List<Brand> BD = Brand.GetAllBrand(con).ToList();
            for (int i = 0; i < BD.Count; i++)
            {
                var url = "https://tamazglobal.com/TopBrands/" + BD[i].BrandUrl;
                strTopBrands += @"<div class='link-item'><span class='dot'></span>
                    <a href='" + url + @"'>" + BD[i].BrandName + @"</a>
                </div>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindAllBlogs", ex.Message);
        }
    }
    public void BindAllProducts()
    {
        try
        {
            strProducts = "";
            List<ProductDetails> BD = ProductDetails.GetAllProducts(con).ToList();
            for (int i = 0; i < BD.Count; i++)
            {
                var url = "https://tamazglobal.com/Product/" + BD[i].ProductUrl;
                strProducts += @"<div class='link-item'><span class='dot'></span>
                    <a href='" + url + @"'>" + BD[i].ProductName + @"</a>
                </div>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindAllBlogs", ex.Message);
        }
    }
}