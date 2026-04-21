using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class add_alternate_products : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strMapProductOptions = "", strSuccessMsg = "", strErrorMsg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProducts();
            BuildMapProductOptions(null); // no pre-selection on first load
        }
    }

    private void BindProducts()
    {
        try
        {
            List<ProductDetails> list = ProductDetails.GetAllProducts(conT)
                .OrderBy(x => x.ProductName).ToList();

            ddlProduct.Items.Clear();
            ddlProduct.Items.Add(new ListItem("Select Product", "0"));
            foreach (var p in list)
                ddlProduct.Items.Add(new ListItem(p.ProductName, p.Id.ToString()));
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "BindProducts", ex.Message);
        }
    }

    private void BuildMapProductOptions(string[] selectedIds)
    {
        try
        {
            List<ProductDetails> list = ProductDetails.GetAllProducts(conT)
                .OrderBy(x => x.ProductName).ToList();

            strMapProductOptions = "";
            foreach (var p in list)
            {
                string sel = (selectedIds != null && selectedIds.Contains(p.Id.ToString()))
                             ? "selected" : "";
                strMapProductOptions += "<option value='" + p.Id + "' " + sel + ">" + p.ProductName + "</option>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "BuildMapProductOptions", ex.Message);
        }
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string[] selectedIds = null;

            if (ddlProduct.SelectedValue != "0")
            {
                var product = ProductDetails.GetAllProducts(conT)
                    .FirstOrDefault(x => x.Id == Convert.ToInt32(ddlProduct.SelectedValue));

                if (product != null && !string.IsNullOrEmpty(product.AlternativeProduct))
                {
                    selectedIds = product.AlternativeProduct
                        .Split('|')
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .ToArray();

                    // Pass to JS via hidden field so Select2 can pre-select
                    hdnMappedProducts.Value = string.Join("|", selectedIds);
                }
                else
                {
                    hdnMappedProducts.Value = "";
                }
            }

            BuildMapProductOptions(selectedIds);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "ddlProduct_SelectedIndexChanged", ex.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string mappedProducts = Request.Form["chkMapProductsJs"];
            if (string.IsNullOrEmpty(mappedProducts))
            {
                strErrorMsg = "Please select at least one product to map.";
                BuildMapProductOptions(null);
                return;
            }

            // Convert comma-separated (from multi-select form post) to pipe-separated
            string pipeSeparated = string.Join("|",
                mappedProducts.Split(',').Where(s => !string.IsNullOrWhiteSpace(s))) + "|";

            ProductDetails pd = new ProductDetails();
            pd.Id = Convert.ToInt32(ddlProduct.SelectedValue);
            pd.Status = "Active";
            pd.AddedBy = Request.Cookies["t_aid"].Value;
            pd.AddedIp = CommonModel.IPAddress();
            pd.AddedOn = CommonModel.UTCTime();
            pd.AlternativeProduct = pipeSeparated;

            int result = ProductDetails.UpdateAlternateProducts(conT, pd);
            if (result > 0)
                strSuccessMsg = "Products mapped successfully.";
            else
                strErrorMsg = "Something went wrong. Please try again.";

            // Rebuild dropdown keeping selections
            var ids = pipeSeparated.Split('|').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            hdnMappedProducts.Value = string.Join("|", ids);
            BuildMapProductOptions(ids);
        }
        catch (Exception ex)
        {
            strErrorMsg = "Something went wrong. Please try again.";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "btnSave_Click", ex.Message);
        }
    }
}