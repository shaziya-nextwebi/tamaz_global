using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_category : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strCategory = "", strBannerImage = "", strMobileImage = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
                GetCategory();

            GetAllCategories();
        }
    }

    // ===================== GET ALL =====================

    private void GetAllCategories()
    {
        try
        {
            strCategory = "";
            var list = Category.GetAllCategory(conT);
            int i = 0;
            foreach (var cat in list)
            {
                strCategory += @"<tr>
                    <td>" + (i + 1) + @"</td>
                    <td>" + cat.CategoryName + @"</td>
                    <td>" + cat.DisplayOrder + @"</td>
                    <td>" + cat.DisplayHome + @"</td>
                    <td>
                        <a href='/" + cat.BannerImage + @"' target='_blank'>
                            <img src='/" + cat.BannerImage + @"' style='height:50px;border-radius:4px;' />
                        </a>
                    </td>
                    <td>" + cat.PageTitle + @"</td>
                    <td>" + cat.AddedOn.ToString("dd/MMM/yyyy") + @"</td>
                    <td class='text-center'>
                        <a href='category.aspx?id=" + cat.Id + @"'
                           class='bs-tooltip link-info me-1' title='Edit'>
                            <svg xmlns='http://www.w3.org/2000/svg' width='20' height='24'
                                 viewBox='0 0 24 24' fill='none' stroke='currentColor'
                                 stroke-width='2' stroke-linecap='round' stroke-linejoin='round'
                                 class='feather feather-edit'>
                                <path d='M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7'></path>
                                <path d='M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z'></path>
                            </svg>
                        </a>
                        <a href='javascript:void(0);'
                           class='bs-tooltip deleteItem link-danger' data-id='" + cat.Id + @"'
                           title='Delete'>
                            <svg xmlns='http://www.w3.org/2000/svg' width='20' height='24'
                                 viewBox='0 0 24 24' fill='none' stroke='currentColor'
                                 stroke-width='2' stroke-linecap='round' stroke-linejoin='round'
                                 class='feather feather-trash-2'>
                                <polyline points='3 6 5 6 21 6'></polyline>
                                <path d='M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2'></path>
                                <line x1='10' y1='11' x2='10' y2='17'></line>
                                <line x1='14' y1='11' x2='14' y2='17'></line>
                            </svg>
                        </a>
                    </td>
                </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetAllCategories", ex.Message);
        }
    }

    // ===================== LOAD FOR EDIT =====================

    private void GetCategory()
    {
        try
        {
            var cat = Category.GetAllCategory(conT)
                .Where(x => x.Id == Convert.ToInt32(Request.QueryString["id"]))
                .FirstOrDefault();

            if (cat != null)
            {
                btnSave.Text = "Update";
                btnNew.Visible = true;
                txtCategory.Text = cat.CategoryName;
                txtUrl.Text = cat.CategoryUrl;
                txtOrder.Text = cat.DisplayOrder.ToString();
                txtPageTitle.Text = cat.PageTitle;
                txtMetaKeys.Text = cat.MetaKeys;
                txtMetaDesc.Text = cat.MetaDesc;
                txtShortDesc.Text = cat.ShortDesc;
                txtFullDesc.Text = cat.FullDesc;
                chkHome.Checked = cat.DisplayHome == "Yes";

                if (!string.IsNullOrEmpty(cat.BannerImage))
                {
                    strBannerImage = "<img src='/" + cat.BannerImage +
                                     "' style='max-height:60px;border-radius:4px;' />";
                    lblIndustrialInternImage1.Text = cat.BannerImage;
                    ReqFileupload1.Enabled = false;
                }

                if (!string.IsNullOrEmpty(cat.MobileImage))
                {
                    strMobileImage = "<img src='/" + cat.MobileImage +
                                     "' style='max-height:60px;border-radius:4px;' />";
                    lblIndustrialInternImage2.Text = cat.MobileImage;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetCategory", ex.Message);
        }
    }

    // ===================== SAVE / UPDATE =====================

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;
        try
        {
            string ind1 = UploadThumbImage();
            if (ind1 == "Format") { ShowError("Invalid image format. Use .png .jpeg .jpg .gif .webp"); return; }
            if (ind1 == "Size") { ShowError("Thumb image size should be 500px × 500px"); return; }

            string ind2 = UploadMobileImage();
            if (ind2 == "Format") { ShowError("Invalid mobile image format."); return; }

            string aid = Request.Cookies["t_aid"].Value;

            Category cat = new Category();
            cat.CategoryName = txtCategory.Text.Trim();
            cat.CategoryUrl = txtUrl.Text.Trim();
            cat.DisplayOrder = txtOrder.Text != "" ? Convert.ToInt32(txtOrder.Text) : 10000;
            cat.PageTitle = txtPageTitle.Text.Trim();
            cat.MetaKeys = txtMetaKeys.Text.Trim();
            cat.MetaDesc = txtMetaDesc.Text.Trim();
            cat.ShortDesc = txtShortDesc.Text.Trim();
            cat.FullDesc = txtFullDesc.Text.Trim();
            cat.DisplayHome = chkHome.Checked ? "Yes" : "No";
            cat.BannerImage = ind1;
            cat.MobileImage = ind2;
            cat.AddedBy = aid;
            cat.AddedIP = CommonModel.IPAddress();
            cat.AddedOn = CommonModel.UTCTime();
            cat.Status = "Active";

            if (btnSave.Text == "Update")
            {
                cat.Id = Convert.ToInt32(Request.QueryString["id"]);
                int result = Category.UpdateCategory(conT, cat);
                if (result > 0)
                {
                    GetCategory();
                    GetAllCategories();
                    ShowSuccess("Category updated successfully.");
                }
                else ShowError("Something went wrong. Please try again.");
            }
            else
            {
                int result = Category.InsertCategory(conT, cat);
                if (result > 0)
                {
                    txtCategory.Text = txtUrl.Text = txtOrder.Text =
                    txtPageTitle.Text = txtMetaKeys.Text = txtMetaDesc.Text =
                    txtShortDesc.Text = txtFullDesc.Text = "";
                    chkHome.Checked = false;
                    strBannerImage = strMobileImage = "";
                    ReqFileupload1.Enabled = true;
                    GetAllCategories();
                    ShowSuccess("Category added successfully.");
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

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("category.aspx");
    }

    // ===================== DELETE (WebMethod) =====================

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            Category cat = new Category
            {
                Id = Convert.ToInt32(id),
                AddedOn = CommonModel.UTCTime(),
                AddedIP = CommonModel.IPAddress(),
                AddedBy = HttpContext.Current.Request.Cookies["t_aid"].Value
            };

            int result = Category.DeleteCategory(conT, cat);
            return result > 0 ? "Success" : "W";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "Delete", ex.Message);
            return "W";
        }
    }

    // ===================== UPLOAD THUMB IMAGE =====================

    private string UploadThumbImage()
    {
        string thumbImage = "";
        if (fuIndSamll.HasFile)
        {
            string ext = Path.GetExtension(fuIndSamll.PostedFile.FileName.ToLower());
            if (!(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif" || ext == ".webp"))
                return "Format";

            string guid = Guid.NewGuid().ToString();
            string savePath = Server.MapPath("~/UploadImages/") + guid + "_CategoryThumb" + ext;

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
            else
            {
                System.Drawing.Bitmap bmp =
                    new System.Drawing.Bitmap(fuIndSamll.PostedFile.InputStream);
                if (bmp.Width != 500 || bmp.Height != 500) return "Size";

                System.Drawing.Image imgScaled =
                    CommonModel.ScaleImageBig(bmp, bmp.Height, bmp.Width);
                if (ext == ".png") CommonModel.SavePNG(savePath, imgScaled, 99);
                else CommonModel.SaveJpeg(savePath, imgScaled, 80);
            }
            thumbImage = "UploadImages/" + guid + "_CategoryThumb" + ext;
        }
        else
        {
            thumbImage = lblIndustrialInternImage1.Text;
        }
        return thumbImage;
    }

    // ===================== UPLOAD MOBILE IMAGE =====================

    private string UploadMobileImage()
    {
        string mobileImage = "";
        if (fuIndThumb.HasFile)
        {
            string ext = Path.GetExtension(fuIndThumb.PostedFile.FileName.ToLower());
            if (!(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif" || ext == ".webp"))
                return "Format";

            string guid = Guid.NewGuid().ToString();
            string savePath = Server.MapPath("~/UploadImages/") + guid + "_CategoryMobile" + ext;

            try
            {
                if (!string.IsNullOrEmpty(lblIndustrialInternImage2.Text) &&
                    File.Exists(Server.MapPath("~/" + lblIndustrialInternImage2.Text)))
                    File.Delete(Server.MapPath("~/" + lblIndustrialInternImage2.Text));
            }
            catch { }

            fuIndThumb.SaveAs(savePath);
            mobileImage = "UploadImages/" + guid + "_CategoryMobile" + ext;
        }
        else
        {
            mobileImage = lblIndustrialInternImage2.Text;
        }
        return mobileImage;
    }

    // ===================== HELPERS =====================

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