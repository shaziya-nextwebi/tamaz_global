using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_banner_images : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strImages = "";
    public string strThumbImage = "";
    public string strThumbImageMob = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllBannerImages();
            if (Request.QueryString["id"] != null)
            {
                GetBanner();
            }
        }
    }

    // ===================== LOAD BANNER FOR EDIT =====================

    private void GetBanner()
    {
        try
        {
            var banner = BannerImages.GetBannerImage(conT)
                .Where(s => s.Id == Convert.ToInt32(Request.QueryString["id"]))
                .SingleOrDefault();

            if (banner != null)
            {
                btnUpload.Text = "Update";
                txtTitle.Text = banner.BannerTitle;
                txtlink.Text = banner.Link;
                txtOrder.Text = banner.DisplayOrder;

                if (!string.IsNullOrEmpty(banner.DesktopImage))
                {
                    strThumbImage = "<img src='/" + banner.DesktopImage +
                                    "' style='max-height:60px;border-radius:4px;' />";
                    lblThumb.Text = banner.DesktopImage;
                    reqUpload.Enabled = false;
                }

                if (!string.IsNullOrEmpty(banner.MobImage))
                {
                    strThumbImageMob = "<img src='/" + banner.MobImage +
                                       "' style='max-height:60px;border-radius:4px;' />";
                    lblThumbMob.Text = banner.MobImage;
                    reqUploadMob.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetBanner", ex.Message);
        }
    }

    // ===================== GET ALL BANNERS =====================

    private void GetAllBannerImages()
    {
        try
        {
            strImages = "";
            var list = BannerImages.GetBannerImage(conT)
                .OrderBy(s => Convert.ToInt32(s.DisplayOrder)).ToList();
            int i = 0;
            foreach (var img in list)
            {
                strImages += @"<tr>
                    <td>" + (i + 1) + @"</td>
                    <td>" + img.BannerTitle + @"</td>
                    <td>
                        <a href='/" + img.DesktopImage + @"' target='_blank'>
                            <img src='/" + img.DesktopImage + @"'
                                 style='max-height:50px;border-radius:4px;' />
                        </a>
                    </td>
                    <td>
                        <a href='/" + img.MobImage + @"' target='_blank'>
                            <img src='/" + img.MobImage + @"'
                                 style='max-height:50px;border-radius:4px;' />
                        </a>
                    </td>
                    <td>" + img.DisplayOrder + @"</td>
                    <td>" + img.UpdatedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"</td>
                    <td class='text-center'>
                        <a href='banner-images.aspx?id=" + img.Id + @"'
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
                           class='bs-tooltip deleteItem link-danger' data-id='" + img.Id + @"'
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
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetAllBannerImages", ex.Message);
        }
    }

    // ===================== SAVE / UPDATE =====================

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        try
        {
            // Desktop image
            string desktopResult = CheckAndUploadDesktop();
            if (desktopResult == "Format")
            {
                ShowError("Invalid desktop image format. Use .png .jpeg .jpg .webp");
                return;
            }
            if (desktopResult == "Size")
            {
                ShowError("Desktop image size should be 1903px × 520px");
                return;
            }

            // Mobile image
            string mobileResult = CheckAndUploadMobile();
            if (mobileResult == "Format")
            {
                ShowError("Invalid mobile image format. Use .png .jpeg .jpg .webp");
                return;
            }
            if (mobileResult == "Size")
            {
                ShowError("Mobile image size should be 800px × 1000px");
                return;
            }

            BannerImages banner = new BannerImages();
            banner.BannerTitle = txtTitle.Text.Trim();
            banner.Link = txtlink.Text.Trim();
            banner.DisplayOrder = string.IsNullOrEmpty(txtOrder.Text) ? "1000" : txtOrder.Text.Trim();
            banner.DesktopImage = desktopResult;
            banner.MobImage = mobileResult;

            if (btnUpload.Text == "Update")
            {
                banner.Id = Convert.ToInt32(Request.QueryString["id"]);
                banner.UpdatedOn = CommonModel.UTCTime();
                banner.UpdatedIp = CommonModel.IPAddress();

                int result = BannerImages.UpdateBannerImage(conT, banner);
                if (result > 0)
                {
                    GetBanner();
                    GetAllBannerImages();
                    ShowSuccess("Banner updated successfully.");
                }
                else ShowError("Something went wrong. Please try again.");
            }
            else
            {
                banner.AddedOn = CommonModel.UTCTime();
                banner.AddedIp = CommonModel.IPAddress();

                int result = BannerImages.InsertBannerImage(conT, banner);
                if (result > 0)
                {
                    txtTitle.Text = txtlink.Text = txtOrder.Text = "";
                    reqUpload.Enabled = true;
                    reqUploadMob.Enabled = true;
                    strThumbImage = "";
                    strThumbImageMob = "";
                    lblThumb.Text = "";
                    lblThumbMob.Text = "";
                    GetAllBannerImages();
                    ShowSuccess("Banner added successfully.");
                }
                else ShowError("Something went wrong. Please try again.");
            }
        }
        catch (Exception ex)
        {
            ShowError("Something went wrong. Please try again.");
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "btnUpload_Click", ex.Message);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtTitle.Text = txtlink.Text = txtOrder.Text = "";
        lblThumb.Text = lblThumbMob.Text = "";
        strThumbImage = strThumbImageMob = "";
        btnUpload.Text = "Save";
        reqUpload.Enabled = true;
        reqUploadMob.Enabled = true;
    }

    // ===================== UPLOAD DESKTOP IMAGE =====================

    private string CheckAndUploadDesktop()
    {
        string imagePath = "";
        string guid = Guid.NewGuid().ToString();

        if (FileUpload1.HasFile)
        {
            string ext = Path.GetExtension(FileUpload1.PostedFile.FileName.ToLower());
            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".webp")
            {
                string imageGuid = guid + "_bannerdesk";
                string savePath = Server.MapPath(".") +
                    "\\../UploadImages\\" + imageGuid + ext;

                try
                {
                    if (!string.IsNullOrEmpty(lblThumb.Text) &&
                        File.Exists(Server.MapPath("~/" + lblThumb.Text)))
                        File.Delete(Server.MapPath("~/" + lblThumb.Text));
                }
                catch { }

                if (ext == ".webp")
                {
                    FileUpload1.SaveAs(savePath);
                }
                else
                {
                    System.Drawing.Bitmap bmp =
                        new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);
                    if (bmp.Width != 1903 || bmp.Height != 520) return "Size";

                    System.Drawing.Image imgScaled =
                        CommonModel.ScaleImageBig(bmp, bmp.Height, bmp.Width);
                    if (ext == ".png") CommonModel.SavePNG(savePath, imgScaled, 99);
                    else CommonModel.SaveJpeg(savePath, imgScaled, 80);
                }
                imagePath = "UploadImages/" + imageGuid + ext;
            }
            else return "Format";
        }
        else
        {
            imagePath = lblThumb.Text;
        }
        return imagePath;
    }

    // ===================== UPLOAD MOBILE IMAGE =====================

    private string CheckAndUploadMobile()
    {
        string imagePath = "";
        string guid = Guid.NewGuid().ToString();

        if (FileUpload2.HasFile)
        {
            string ext = Path.GetExtension(FileUpload2.PostedFile.FileName.ToLower());
            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".webp")
            {
                string imageGuid = guid + "_bannermob";
                string savePath = Server.MapPath(".") +
                    "\\../UploadImages\\" + imageGuid + ext;

                try
                {
                    if (!string.IsNullOrEmpty(lblThumbMob.Text) &&
                        File.Exists(Server.MapPath("~/" + lblThumbMob.Text)))
                        File.Delete(Server.MapPath("~/" + lblThumbMob.Text));
                }
                catch { }

                if (ext == ".webp")
                {
                    FileUpload2.SaveAs(savePath);
                }
                else
                {
                    System.Drawing.Bitmap bmp =
                        new System.Drawing.Bitmap(FileUpload2.PostedFile.InputStream);
                    if (bmp.Width != 800 || bmp.Height != 1000) return "Size";

                    System.Drawing.Image imgScaled =
                        CommonModel.ScaleImageBig(bmp, bmp.Height, bmp.Width);
                    if (ext == ".png") CommonModel.SavePNG(savePath, imgScaled, 99);
                    else CommonModel.SaveJpeg(savePath, imgScaled, 80);
                }
                imagePath = "UploadImages/" + imageGuid + ext;
            }
            else return "Format";
        }
        else
        {
            imagePath = lblThumbMob.Text;
        }
        return imagePath;
    }

    // ===================== DELETE =====================

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            BannerImages bis = new BannerImages
            {
                Id = Convert.ToInt32(id),
                UpdatedOn = CommonModel.UTCTime(),
                UpdatedIp = CommonModel.IPAddress(),
                Status = "Deleted"
            };

            int result = BannerImages.DeleteBannerImage(conT, bis);
            return result > 0 ? "Success" : "W";
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "Delete", ex.Message);
            return "W";
        }
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