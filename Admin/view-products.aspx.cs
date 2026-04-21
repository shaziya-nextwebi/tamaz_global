using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class view_products : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strProductsList = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetAllProducts();
    }

    private void GetAllProducts()
    {
        try
        {
            strProductsList = "";
            List<ProductDetails> list = ProductDetails.GetAllProducts(conT)
                .OrderByDescending(s => s.Id)
                .ToList();

            int i = 1;
            foreach (ProductDetails pro in list)
            {
                string statusBadge = pro.Status == "Active"
                    ? "<span id='sts_" + pro.Id + "' class='badge bg-success'>Active</span>"
                    : "<span id='sts_" + pro.Id + "' class='badge bg-warning text-dark'>Draft</span>";

                string toggle = @"<div class='form-check form-switch d-flex justify-content-center'>
                    <input class='form-check-input publishProduct' type='checkbox' data-id='" + pro.Id + @"' "
                    + (pro.Status == "Active" ? "checked" : "") + @">
                </div>";

                strProductsList += @"<tr>
                    <td>" + i + @"</td>
                    <td><a href='/Product/" + pro.ProductUrl + @"' target='_blank'>" + pro.ProductName + @"</a></td>
                    <td>" + pro.Category + @"</td>
                    <td>" + pro.Brand + @"</td>
                    <td>" + statusBadge + @"</td>
                    <td>" + pro.AddedOn.ToString("dd/MMM/yyyy") + @"</td>
                    <td class='text-center'>" + toggle + @"</td>
                    <td class='text-center'>
                        <a href='add-products.aspx?id=" + pro.Id + @"' class='link-info me-2 fs-18' title='Edit'>
                            <i class='mdi mdi-pencil'></i>
                        </a>
                        <a href='javascript:void(0);' class='deleteItem link-danger fs-18' data-id='" + pro.Id + @"' title='Delete'>
                            <i class='mdi mdi-trash-can-outline'></i>
                        </a>
                    </td>
                </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetAllProducts", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            ProductDetails pro = new ProductDetails();
            pro.Id = Convert.ToInt32(id);
            pro.AddedOn = CommonModel.UTCTime();
            pro.AddedIp = CommonModel.IPAddress();
            pro.AddedBy = HttpContext.Current.Request.Cookies["t_aid"].Value;
            pro.Status = "Deleted";

            return ProductDetails.DeleteProductDetails(conT, pro) > 0 ? "Success" : "W";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "Delete", ex.Message);
            return "W";
        }
    }

    [WebMethod(EnableSession = true)]
    public static string PublishProduct(string id, string ftr)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            ProductDetails cat = new ProductDetails();
            cat.Id = Convert.ToInt32(id);
            cat.Status = ftr == "Yes" ? "Active" : "Draft";
            cat.AddedOn = CommonModel.UTCTime();
            cat.AddedBy = HttpContext.Current.Request.Cookies["t_aid"].Value;
            cat.AddedIp = CommonModel.IPAddress();

            return ProductDetails.PublishOrUnPublishProduct(conT, cat) > 0 ? "Success" : "W";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "PublishProduct", ex.Message);
            return "W";
        }
    }
}