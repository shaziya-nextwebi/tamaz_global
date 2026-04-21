using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class Admin_category_order : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindCategoryDropdown();
    }

    // ===================== BIND CATEGORY DROPDOWN =====================

    private void BindCategoryDropdown()
    {
        try
        {
            ddlCategory.Items.Clear();
            var cats = Category.GetAllCategory(conT);
            ddlCategory.DataSource = cats;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "Id";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0,
                new System.Web.UI.WebControls.ListItem("-- Select Category --", "0"));
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "BindCategoryDropdown", ex.Message);
        }
    }

    // ===================== GET PRODUCTS BY CATEGORY (WebMethod) =====================

    [WebMethod(EnableSession = true)]
    public static List<CategoryOrder> GetProductsByCategory(string categoryId)
    {
        List<CategoryOrder> result = new List<CategoryOrder>();
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            var products = ProductDetails.GetAllProducts(conT)
         .Where(x => x.CategoryId == categoryId && x.Status == "Active")
         .OrderBy(x => Convert.ToInt32(x.CategoryOrder))
         .ToList();

            foreach (var p in products)
            {
                result.Add(new CategoryOrder
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Image = p.SmallImage,
                    Category = p.CategoryId.ToString()
                });
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetProductsByCategory", ex.Message);
        }
        return result;
    }

    // ===================== UPDATE ORDER (WebMethod) =====================

    [WebMethod(EnableSession = true)]
    public static string UpdateProductOrder(List<string> ids)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            for (int i = 0; i < ids.Count; i++)
                ProductDetails.UpdateDisplayOrder(conT, Convert.ToInt32(ids[i]), i + 1);

            return "Success";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "UpdateProductOrder", ex.Message);
            return "W";
        }
    }
}