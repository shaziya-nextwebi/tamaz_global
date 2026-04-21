using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class write_blog : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strBannerImage = "", strMobileImage = "";
    public string strTagOptions = "", strSuccessMsg = "", strErrorMsg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BuildTagOptions(null);

            if (Request.QueryString["id"] != null)
                GetBlogDetails();
        }
    }

    // ===================== LOAD FOR EDIT =====================

    private void GetBlogDetails()
    {
        try
        {
            List<Blogs> list = Blogs.GetBlog(conT, Convert.ToInt32(Request.QueryString["id"]));
            if (list.Count == 0) return;

            Blogs pro = list[0];
            btnSave.Text = "Update";
            txtBlogName.Text = pro.BlogName;
            txtURL.Text = pro.BlogUrl;
            txtPostedBy.Text = pro.PostedBy;
            txtDesc.Text = pro.FullDesc;
            txtPageTitle.Text = pro.PageTitle;
            txtMetaKeys.Text = pro.MetaKeys;
            txtMetaDesc.Text = pro.MetaDesc;
            txtShortDesc.Text = pro.ShortDesc;
            txtDate.Text = pro.PostedOn.ToString("dd/MMM/yyyy");

            // Pre-select tags — pass existing pipe-separated string
            string[] selectedTags = pro.BlogTags.Split('|');
            hdnBlogTags.Value = pro.BlogTags;
            BuildTagOptions(selectedTags);

            if (!string.IsNullOrEmpty(pro.SmallImg.Trim()))
            {
                strBannerImage = "<img src='/" + pro.SmallImg.Trim() +
                                 "' style='max-height:60px; border-radius:4px;' />";
                lblIndustrialInternImage1.Text = pro.SmallImg.Trim();
            }
            if (!string.IsNullOrEmpty(pro.BigImg.Trim()))
            {
                strMobileImage = "<img src='/" + pro.BigImg.Trim() +
                                 "' style='max-height:60px; border-radius:4px;' />";
                lblIndustrialInternImage2.Text = pro.BigImg.Trim();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetBlogDetails", ex.Message);
        }
    }

    // ===================== BUILD TAG OPTIONS =====================

    private void BuildTagOptions(string[] selectedTags)
    {
        try
        {
            List<BlogTags> tags = BlogTags.GetAllBlogTags(conT);
            strTagOptions = "";
            foreach (BlogTags t in tags)
            {
                string sel = (selectedTags != null &&
                    Array.Exists(selectedTags, s => s.Trim() == t.TagName.Trim()))
                    ? "selected" : "";
                strTagOptions += "<option value='" + t.TagName + "' " + sel + ">" + t.TagName + "</option>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "BuildTagOptions", ex.Message);
        }
    }

    // ===================== SAVE / UPDATE =====================

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;
        try
        {
            // Collect tags from hidden field (packed by JS before postback)
            string rawTags = Request.Form["lstTagsJs"];
            string tags = "";
            if (!string.IsNullOrEmpty(rawTags))
            {
                foreach (string t in rawTags.Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(t))
                        tags += t.Trim() + "|";
                }
                tags = tags.TrimEnd('|');
            }

            string ind1 = UploadImage(fuIndSamll, lblIndustrialInternImage1, 580, 390);
            if (ind1 == "Format") { strErrorMsg = "Invalid format for Thumb Image."; return; }
            if (ind1 == "Size") { strErrorMsg = "Thumb image must be 580x390px."; return; }

            string ind2 = UploadImage(fuIndThumb, lblIndustrialInternImage2, 930, 500);
            if (ind2 == "Format") { strErrorMsg = "Invalid format for Detail Image."; return; }
            if (ind2 == "Size") { strErrorMsg = "Detail image must be 930x500px."; return; }

            Blogs pro = new Blogs();
            pro.BlogName = txtBlogName.Text.Trim();
            pro.BlogUrl = txtURL.Text.Trim();
            pro.PostedBy = txtPostedBy.Text.Trim();
            pro.BlogTags = tags;
            pro.PostedOn = txtDate.Text == "" ? CommonModel.UTCTime() : Convert.ToDateTime(txtDate.Text);
            pro.PageTitle = txtPageTitle.Text.Trim();
            pro.MetaKeys = txtMetaKeys.Text.Trim();
            pro.MetaDesc = txtMetaDesc.Text.Trim();
            pro.ShortDesc = txtShortDesc.Text.Trim();
            pro.FullDesc = txtDesc.Text.Trim();
            pro.SmallImg = ind1;
            pro.BigImg = ind2;
            pro.AddedBy = Request.Cookies["t_aid"].Value;
            pro.AddedIP = CommonModel.IPAddress();
            pro.AddedOn = CommonModel.UTCTime();
            pro.Status = "Active";

            int result;
            if (btnSave.Text == "Update")
            {
                pro.Id = Convert.ToInt32(Request.QueryString["id"]);
                result = Blogs.UpdateBlog(conT, pro);
                if (result > 0)
                {
                    GetBlogDetails();
                    strSuccessMsg = "Blog updated successfully.";
                }
                else strErrorMsg = "Something went wrong. Please try again.";
            }
            else
            {
                result = Blogs.WriteBlog(conT, pro);
                if (result > 0)
                {
                    txtBlogName.Text = txtDesc.Text = txtDate.Text = "";
                    txtMetaDesc.Text = txtMetaKeys.Text = txtPageTitle.Text = "";
                    txtShortDesc.Text = txtURL.Text = txtPostedBy.Text = "";
                    hdnBlogTags.Value = "";
                    BuildTagOptions(null);
                    strSuccessMsg = "Blog added successfully.";
                }
                else strErrorMsg = "Something went wrong. Please try again.";
            }
        }
        catch (Exception ex)
        {
            strErrorMsg = "Something went wrong. Please try again.";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "btnSave_Click", ex.Message);
        }
    }

    // ===================== UPLOAD HELPER =====================

    private string UploadImage(FileUpload fu, Label lblExisting, int reqWidth, int reqHeight)
    {
        if (!fu.HasFile)
            return lblExisting.Text;

        string ext = Path.GetExtension(fu.PostedFile.FileName.ToLower());
        if (!(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif" || ext == ".webp"))
            return "Format";

        string oid = DateTime.Now.ToString("ddMMyyyyHHmmss");
        string baseName = txtBlogName.Text.Trim()
                            .Replace(":", "-").Replace("&", "-")
                            .Replace(" ", "");
        string fileName = baseName + "-" + oid + ext;
        string savePath = Server.MapPath("~/UploadImages/") + fileName;

        // Delete old file if exists
        try
        {
            if (!string.IsNullOrEmpty(lblExisting.Text) &&
                File.Exists(Server.MapPath("~/" + lblExisting.Text)))
                File.Delete(Server.MapPath("~/" + lblExisting.Text));
        }
        catch { }

        if (ext == ".webp" || ext == ".gif")
        {
            fu.SaveAs(savePath);
        }
        else
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(fu.PostedFile.InputStream);
            if (bmp.Width != reqWidth || bmp.Height != reqHeight)
                return "Size";

            System.Drawing.Image img = CommonModel.ScaleImageBig(bmp, bmp.Height, bmp.Width);
            if (ext == ".png")
                CommonModel.SavePNG(savePath, img, 99);
            else
                CommonModel.SaveJpeg(savePath, img, 80);
        }

        lblExisting.Text = "UploadImages/" + fileName;
        return "UploadImages/" + fileName;
    }
}