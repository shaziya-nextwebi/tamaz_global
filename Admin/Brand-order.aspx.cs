using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class Admin_Brand_order : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindBrandDropdown();
    }

    // ===================== BIND BRAND DROPDOWN =====================

    private void BindBrandDropdown()
    {
        try
        {
            ddlCategory.Items.Clear();
            List<Brand> brands = Brand.GetAllBrand(conT);
            if (brands.Count > 0)
            {
                ddlCategory.DataSource = brands;
                ddlCategory.DataTextField = "BrandName";
                ddlCategory.DataValueField = "BrandName";
                ddlCategory.DataBind();
            }
            ddlCategory.Items.Insert(0,
                new System.Web.UI.WebControls.ListItem("-- Select Brand --", "0"));
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "BindBrandDropdown", ex.Message);
        }
    }

    // ===================== GET PRODUCTS BY BRAND (WebMethod) =====================

    [WebMethod(EnableSession = true)]
    public static List<BrandOrder> GetProductsByBrand(string brandName)
    {
        List<BrandOrder> result = new List<BrandOrder>();
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            List<ProductDetails> products = ProductDetails.GetAllProducts(conT)
                .Where(x => x.Brand == brandName)
                .OrderBy(x => x.BrandOrder == "" || x.BrandOrder == "1000"
                    ? int.MaxValue
                    : Convert.ToInt32(x.BrandOrder))
                .ToList();

            result = products.Select(p => new BrandOrder
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Image = p.SmallImage,
                Brand = p.Brand
            }).ToList();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetProductsByBrand", ex.Message);
        }
        return result;
    }

    // ===================== UPDATE ORDER (WebMethod) =====================

    [WebMethod(EnableSession = true)]
    public static string UpdateBrandOrder(List<string> ids)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            for (int i = 0; i < ids.Count; i++)
            {
                ProductDetails pro = new ProductDetails
                {
                    Id = ids[i] == "" ? 0 : Convert.ToInt32(ids[i]),
                    BrandOrder = Convert.ToString(i + 1)
                };
                ProductDetails.UpdateBrandOrder(conT, pro);
            }
            return "Success";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "UpdateBrandOrder", ex.Message);
            return "W";
        }
    }
}