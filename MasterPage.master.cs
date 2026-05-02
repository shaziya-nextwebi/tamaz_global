using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public string strNavCategories = "";
    public string strCartCount = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
        strCartCount = AddtoCart.GetcartlistQunatity(conT);

        BindNavCategories();

    }

    //public void BindNavCategories()
    //{
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    //        List<Category> cats = Category.GetAllCategory(con);
    //        strNavCategories = "";

    //        foreach (Category c in cats)
    //        {
    //            int productCount = GetProductCount(c.Id);

    //            strNavCategories +=
    //                "<a href='/Category/" + c.CategoryUrl + "' class='dropdown-item'>" +
    //                    "<div class='dropdown-icon'>" +
    //                        "<svg viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2'>" +
    //                            "<path d='M12 2L2 7l10 5 10-5-10-5z'/>" +
    //                        "</svg>" +
    //                    "</div>" +
    //                    "<span class='dropdown-text'>" + c.CategoryName + "</span>" +
    //                    "<span class='dropdown-count'>(" + productCount + ")</span>" +
    //                "</a>";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindNavCategories", ex.Message);
    //    }
    //}

    public void BindNavCategories()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            List<Category> cats = Category.GetAllCategory(con)
                .Where(x => x.Status == "Active")
                .OrderBy(o => o.DisplayOrder)
                .ToList();

            var iconMap = new Dictionary<string, string>
        {
            { "Glutathione Injections",        "<span class='iconify' data-icon='bx:injection'></span>" },
            { "Other Injections",              "<span class='iconify' data-icon='bx:injection'></span>" },
            { "Skin Whitening Pills",          "<span class='iconify' data-icon='emojione-monotone:pill'></span>" },
            { "Skin Whitening Cream",          "<span class='iconify' data-icon='ph:hand-soap-thin'></span>" },
            { "Skin Whitening Soap",           "<span class='iconify' data-icon='fa-solid:soap'></span>" },
            { "Skin Whitening Lotions",        "<span class='iconify' data-icon='mdi:lotion'></span>" },
            { "Glutathione Whitening Products","<span class='iconify' data-icon='icon-park-twotone:lotion'></span>" },
            { "Weight Gain Products",          "<span class='iconify' data-icon='healthicons:weight'></span>" },
            { "Weight Loss Products",          "<span class='iconify' data-icon='icon-park-solid:weight'></span>" },
            { "Men Health Care",               "<span class='iconify' data-icon='bx:male'></span>" },
            { "Women Health Care",             "<span class='iconify' data-icon='bx:female'></span>" },
            { "Other Health Care Products",    "<span class='iconify' data-icon='arcticons:samsung-health'></span>" },
        };

            string defaultIcon = "<span class='iconify' data-icon='ph:hand-soap-thin'></span>";

            strNavCategories = "";
            foreach (Category c in cats)
            {
                string iconimg = iconMap.ContainsKey(c.CategoryName) ? iconMap[c.CategoryName] : defaultIcon;
                int productCount = GetProductCount(c.Id);

                strNavCategories +=
                    "<a href='/Category/" + c.CategoryUrl + "' class='dropdown-item'>" +
                        "<div class='dropdown-icon'>" + iconimg + "</div>" +
                        "<span class='dropdown-text'>" + c.CategoryName + "</span>" +
                        "<span class='dropdown-count'>(" + productCount + ")</span>" +
                    "</a>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindNavCategories", ex.Message);
        }
    }
    public int GetProductCount(int catId)
    {
        int cnt = 0;

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
                SELECT COUNT(*) 
                FROM ProductDetails 
                WHERE Status = 'Active' 
                AND CategoryId = @CategoryId", con);

                cmd.Parameters.AddWithValue("@CategoryId", catId);

                cnt = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            // log if needed
        }

        return cnt;
    }

}
