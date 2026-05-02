using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_add_products : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strBannerImage = "", strTourId = "", strProdFaqs = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        strTourId = Request.QueryString["id"] ?? "";

        if (!IsPostBack)
        {
            BindCategories();
            BindProductLabels();
            idPid.Value = Guid.NewGuid().ToString();

            if (Request.QueryString["id"] != null)
            {
                GetProductDetails();
                GetAllProductsFaqs();
                GetEditedSeo();
            }

            if (Request.QueryString["pfaqid"] != null)
                GetEditedFAQs();
        }
    }
    private void BindCategories()
    {
        try
        {
            List<Category> cats = Category.GetAllCategory(conT);
            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(new ListItem("Select Category", "0"));
            foreach (var cat in cats)
                ddlCategory.Items.Add(new ListItem(cat.CategoryName, cat.Id.ToString()));
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "BindCategories", ex.Message);
        }
    }

    private void BindProductLabels()
    {
        try
        {
            List<ProductLabelMaster> labels = ProductLabelMaster.GetAllDProductlabel(conT);
            ddllabel.Items.Clear();
            ddllabel.Items.Add(new ListItem("Select Label", "0"));
            foreach (var lbl in labels)
                ddllabel.Items.Add(new ListItem(lbl.ProductLabel, lbl.Id.ToString()));
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "BindProductLabels", ex.Message);
        }
    }

    private void GetProductDetails()
    {
        try
        {
            var categories = ProductDetails.GetAllProducts(conT)
                .Where(x => x.Id == Convert.ToInt32(Request.QueryString["id"]))
                .ToList();

            if (categories.Count > 0)
            {
                btnSave.Text = "Update";
                var p = categories[0];

                ddlCategory.SelectedIndex = p.Category == "" ? 0
                    : ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(p.Category));
                ddllabel.SelectedValue = p.ProductLabel;
                ddlavalibality.SelectedIndex = p.ProductAvailability == "" ? 0
                    : ddlavalibality.Items.IndexOf(ddlavalibality.Items.FindByText(p.ProductAvailability));

                txtBrandName.Text = p.Brand;
                txtRetailPrice.Text = p.RetailPrice;
                txtProdName.Text = p.ProductName;
                txtURL.Text = p.ProductUrl;
                txtPlaceOrigin.Text = p.PlaceOfOrigin;
                txtKeyIndred.Text = p.KeyIngredient;
                txtFullDesc.Text = p.FullDesc;
                txtBrnfdesc.Text = p.BenefitsDesc;
                txtIngDesc.Text = p.IngredientsDesc;
                txtUsgDesc.Text = p.UsageDesc;
                txtPTitle.Text = p.PageTitle;
                txtMKeys.Text = p.MetaKeys;
                txtMetaDesc.Text = p.MetaDesc;
                chbDispHome.Checked = p.Status == "Active";

                if (!string.IsNullOrEmpty(p.SmallImage))
                {
                    strBannerImage = "<img src='/" + p.SmallImage +
                                     "' style='max-height:60px;border-radius:4px;' />";
                    lblIndustrialInternImage1.Text = p.SmallImage;
                    ReqFileupload1.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetProductDetails", ex.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;
        try
        {
            string ind1 = UploadThumbImage();
            if (ind1 == "Format") { ShowError("Invalid image format."); return; }
            if (ind1 == "Size") { ShowError("Thumb image size incorrect."); return; }

            string aid = Request.Cookies["t_aid"].Value;

            ProductDetails cat = new ProductDetails();
            cat.Category = ddlCategory.SelectedIndex > 0 ? ddlCategory.SelectedItem.Text.Trim() : "";
            cat.Brand = txtBrandName.Text.Trim();
            cat.ProductAvailability = ddlavalibality.SelectedIndex > 0 ? ddlavalibality.SelectedItem.Text.Trim() : "";
            cat.ProductLabel = ddllabel.SelectedValue;
            cat.RetailPrice = txtRetailPrice.Text.Trim();
            cat.ProductName = txtProdName.Text.Trim();
            cat.ProductUrl = txtURL.Text.Trim();
            cat.PlaceOfOrigin = txtPlaceOrigin.Text.Trim();
            cat.KeyIngredient = txtKeyIndred.Text.Trim();
            cat.FullDesc = txtFullDesc.Text.Trim();
            cat.BenefitsDesc = txtBrnfdesc.Text.Trim();
            cat.IngredientsDesc = txtIngDesc.Text.Trim();
            cat.UsageDesc = txtUsgDesc.Text.Trim();
            cat.SmallImage = ind1;
            cat.CategoryId = ddlCategory.SelectedValue;
            cat.BrandId = "";
            cat.DeliveredBy = "";
            cat.DisplayHome = chbDispHome.Checked ? "Yes" : "No";
            cat.AddedBy = aid;
            cat.AddedOn = CommonModel.UTCTime();
            cat.AddedIp = CommonModel.IPAddress();
            cat.Status = chbDispHome.Checked ? "Active" : "Draft";
            cat.InStock = "Yes";
            cat.ProductShortDesc = "";
            cat.AlternativeProduct = "";

            if (btnSave.Text == "Update")
            {
                cat.Id = Convert.ToInt32(Request.QueryString["id"]);
                int result = ProductDetails.UpdateProductDetails(conT, cat);
                if (result > 0)
                {
                    GetProductDetails();
                    ShowSuccess("Product updated successfully.");
                }
                else ShowError("Something went wrong. Please try again.");
            }
            else
            {
                string pGuid = idPid.Value;
                cat.ProductGuid = pGuid;
                cat.ProductId = "TMZ000" + GetMaxId();
                cat.BrandOrder = "1000";
                cat.CategoryOrder = "1000";

                int result = ProductDetails.InsertProductDetails(conT, cat);
                if (result > 0)
                {
                    int newId = ProductDetails.GetLastProductId(conT, pGuid);
                    Response.Redirect("add-products.aspx?id=" + newId);
                }
                else ShowError("Something went wrong. Please try again.");
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "btnSave_Click", ex.Message);
        }
    }

    private string GetMaxId()
    {
        string x = "1";
        try
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT ISNULL(MAX(Id),0)+1 AS Mid FROM ProductDetails", conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0) x = dt.Rows[0]["Mid"].ToString();
        }
        catch { }
        return x;
    }

    private string UploadThumbImage()
    {
        string thumbImage = "";
        if (fuIndSamll.HasFile)
        {
            string ext = Path.GetExtension(fuIndSamll.PostedFile.FileName.ToLower());
            if (!(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif" || ext == ".webp"))
                return "Format";

            string imageGuid = Regex.Replace(
                fuIndSamll.PostedFile.FileName.Split('.')[0], @"[^0-9a-zA-Z-]+", "-");
            string savePath = Server.MapPath("~/UploadImages/") + imageGuid + ext;

            try
            {
                if (!string.IsNullOrEmpty(lblIndustrialInternImage1.Text) &&
                    File.Exists(Server.MapPath("~/" + lblIndustrialInternImage1.Text)))
                    File.Delete(Server.MapPath("~/" + lblIndustrialInternImage1.Text));
            }
            catch { }

            if (ext == ".webp")
            {
                fuIndSamll.SaveAs(savePath);
            }
            else if (ext == ".png")
            {
                System.Drawing.Bitmap bmp =
                    new System.Drawing.Bitmap(fuIndSamll.PostedFile.InputStream);
                System.Drawing.Image img = CommonModel.ScaleImageBig(bmp, bmp.Height, bmp.Width);
                CommonModel.SavePNG(savePath, img, 99);
            }
            else
            {
                System.Drawing.Bitmap bmp =
                    new System.Drawing.Bitmap(fuIndSamll.PostedFile.InputStream);
                System.Drawing.Image img = CommonModel.ScaleImageBig(bmp, bmp.Height, bmp.Width);
                CommonModel.SaveJpeg(savePath, img, 80);
            }
            thumbImage = "UploadImages/" + imageGuid + ext;
        }
        else
        {
            thumbImage = lblIndustrialInternImage1.Text;
        }
        return thumbImage;
    }

    private void GetEditedSeo()
    {
        try
        {
            var categories = ProductDetails.GetAllProducts(conT)
                .Where(x => x.Id == Convert.ToInt32(Request.QueryString["id"]))
                .ToList();

            if (categories.Count > 0)
            {
                txtPTitle.Text = categories[0].PageTitle;
                txtMKeys.Text = categories[0].MetaKeys;
                txtMetaDesc.Text = categories[0].MetaDesc;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetEditedSeo", ex.Message);
        }
    }

    protected void btnSeo_Click(object sender, EventArgs e)
    {
        try
        {
            string aid = Request.Cookies["t_aid"].Value;

            ProductDetails cat = new ProductDetails();
            cat.Id = Convert.ToInt32(Request.QueryString["id"]);
            cat.PageTitle = txtPTitle.Text.Trim();
            cat.MetaKeys = txtMKeys.Text.Trim();
            cat.MetaDesc = txtMetaDesc.Text.Trim();
            cat.AddedBy = aid;
            cat.AddedIp = CommonModel.IPAddress();
            cat.AddedOn = CommonModel.UTCTime();
            cat.Status = "Active";

            int result = ProductDetails.UpdateProductSeoDetails(conT, cat);
            if (result > 0) ShowSuccess("SEO updated successfully.");
            else ShowError("Something went wrong.");
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "btnSeo_Click", ex.Message);
        }
    }


    private void GetAllProductsFaqs()
    {
        try
        {
            strProdFaqs = "";
            var list = FAQs.GetAllProductFAQ(conT)
                .Where(x => x.Pid == Request.QueryString["id"])
                .ToList();

            int i = 0;
            foreach (FAQs pro in list)
            {
                strProdFaqs += @"<tr>
                    <td>" + (i + 1) + @"</td>
                    <td>" + pro.Question + @"</td>
                    <td>" + pro.Answer + @"</td>
                    <td>" + pro.AddedOn.ToString("dd/MMM/yyyy") + @"</td>
                    <td class='text-center'>
                        <a href='javascript:void(0);'
                           class='editFaqItem link-info me-1 fs-18'
                           data-id='" + pro.Id + @"'
                           data-question='" + pro.Question.Replace("'", "&#39;") + @"'
                           data-answer='" + pro.Answer.Replace("'", "&#39;") + @"'
                           title='Edit'>
                            <i class='mdi mdi-pencil'></i>
                        </a>
                        <a href='javascript:void(0);'
                           class='deletepfaqItem link-danger fs-18'
                           data-id='" + pro.Id + @"'
                           title='Delete'>
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
                "GetAllProductsFaqs", ex.Message);
        }
    }

    private void GetEditedFAQs()
    {
        try
        {
            var faq = FAQs.GetAllProductFAQ(conT)
                .Where(x => x.Id == Convert.ToInt32(Request.QueryString["pfaqid"]))
                .FirstOrDefault();

            if (faq != null)
            {
                btnFAQ.Text = "Update FAQ";
                txtQues.Text = faq.Question;
                txtAnswer.Text = faq.Answer;
                lblFaqId.Text = faq.Id.ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetEditedFAQs", ex.Message);
        }
    }
    // Replace your existing btnFAQ_Click with this in add-products.aspx.cs

    protected void btnFAQ_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;
        try
        {
            string aid = Request.Cookies["t_aid"].Value;

            FAQs faq = new FAQs();
            faq.Question = txtQues.Text.Trim();
            faq.Answer = txtAnswer.Text.Trim();
            faq.AddedBy = aid;
            faq.AddedIp = CommonModel.IPAddress();
            faq.AddedOn = CommonModel.UTCTime();
            faq.Status = "Active";

            int result;
            if (!string.IsNullOrEmpty(lblFaqId.Text))
            {
                faq.Id = Convert.ToInt32(lblFaqId.Text);
                faq.Pid = Request.QueryString["id"];
                result = FAQs.UpdateProductFaq(conT, faq);
            }
            else
            {
                faq.Pid = Request.QueryString["id"];
                result = FAQs.InsertProductFAQs(conT, faq);
            }

            if (result > 0)
            {
                // FIX: redirect back to the FAQ tab so the updated table is visible
                Response.Redirect("add-products.aspx?id=" + Request.QueryString["id"] + "&tab=faqs");
            }
            else
            {
                ShowError("Something went wrong.");
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "btnFAQ_Click", ex.Message);
        }
    }

    protected void btnClearFAQ_Click(object sender, EventArgs e)
    {
        txtQues.Text = txtAnswer.Text = lblFaqId.Text = "";
        btnFAQ.Text = "Save FAQ";
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteProductFaqs(string id)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            FAQs pro = new FAQs();
            pro.Id = Convert.ToInt32(id);
            pro.AddedOn = CommonModel.UTCTime();
            pro.AddedIp = CommonModel.IPAddress();
            pro.AddedBy = HttpContext.Current.Request.Cookies["t_aid"].Value;
            pro.Status = "Deleted";

            return FAQs.DeleteProductFaq(conT, pro) > 0 ? "Success" : "W";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "DeleteProductFaqs", ex.Message);
            return "W";
        }
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteGallery(string id)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            ProductGallery pg = new ProductGallery();
            pg.Id = Convert.ToInt32(id);
            pg.AddedOn = CommonModel.UTCTime();
            pg.AddedIp = CommonModel.IPAddress();
            pg.AddedBy = HttpContext.Current.Request.Cookies["t_aid"].Value;
            pg.Status = "Deleted";

            return ProductGallery.DeleteProductGallery(conT, pg) > 0 ? "Success" : "W";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "DeleteGallery", ex.Message);
            return "W";
        }
    }

    [WebMethod(EnableSession = true)]
    public static string ImageOrderUpdate(List<string> ids)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            for (int i = 0; i < ids.Count; i++)
            {
                ProductGallery pg = new ProductGallery();
                pg.Id = ids[i] == "" ? 0 : Convert.ToInt32(ids[i]);
                pg.GalleryOrder = i + 1;
                ProductGallery.UpdateProductGalleryOrder(conT, pg);
            }
            return "Success";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "ImageOrderUpdate", ex.Message);
            return "W";
        }
    }

    [WebMethod]
    public static List<ProductGallery> GetGalleryImage(string id)
    {
        List<ProductGallery> tr = new List<ProductGallery>();
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            tr = ProductGallery.GetAllProductGallery(conT, id)
                .OrderBy(x => x.GalleryOrder)
                .ToList();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetGalleryImage", ex.Message);
        }
        return tr;
    }

    [WebMethod]
    public static List<Brand> GetBrandAutoComplete(string sName)
    {
        List<Brand> cd = new List<Brand>();
        try
        {
            if (string.IsNullOrEmpty(sName)) return cd;
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            cd = Brand.GetAllBrand(conT)
                .Where(x => x.BrandName.ToLower().Contains(sName.ToLower()))
                .Take(10)
                .ToList();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetBrandAutoComplete", ex.Message);
        }
        return cd;
    }

    private void ShowSuccess(string msg)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "msg",
            "Snackbar.show({pos:'top-right',text:'" + msg +
            "',actionTextColor:'#fff',backgroundColor:'#008a3d'});", true);
    }

    private void ShowError(string msg)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "msg",
            "Snackbar.show({pos:'top-right',text:'" + msg +
            "',actionTextColor:'#fff',backgroundColor:'#ea1c1c'});", true);
    }
}