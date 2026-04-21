using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_blog_tags : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strCategory = "", strSuccessMsg = "", strErrorMsg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        GetAllTags();

        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
                GetTagDetails();
        }
    }

    // ===================== LOAD ALL TAGS =====================

    private void GetAllTags()
    {
        try
        {
            strCategory = "";
            List<BlogTags> tags = BlogTags.GetAllBlogTags(conT);
            int i = 1;
            foreach (BlogTags cat in tags)
            {
                strCategory += @"<tr>
                    <td>" + i + @"</td>
                    <td>" + cat.TagName + @"</td>
                    <td>" + cat.AddedOn.ToString("dd/MMM/yyyy") + @"</td>
                    <td class='text-center'>
                        <a href='blog-tags.aspx?id=" + cat.Id + @"' class='link-info me-2 fs-18' title='Edit'>
                            <i class='mdi mdi-pencil'></i>
                        </a>
                        <a href='javascript:void(0);' class='deleteItem link-danger fs-18'
                           data-id='" + cat.Id + @"' title='Delete'>
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
                "GetAllTags", ex.Message);
        }
    }

    // ===================== LOAD FOR EDIT =====================

    private void GetTagDetails()
    {
        try
        {
            var tag = BlogTags.GetAllBlogTags(conT)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(Request.QueryString["id"]));

            if (tag != null)
            {
                btnSave.Text = "Update";
                txtName.Text = tag.TagName;
                txtUrl.Text = tag.TagUrl;
                txtMetaDesc.Text = tag.MetaDesc;
                txtMetaKeys.Text = tag.MetaKeys;
                txtPageTitle.Text = tag.PageTitle;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetTagDetails", ex.Message);
        }
    }

    // ===================== SAVE / UPDATE =====================

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;
        try
        {
            // Duplicate check
            List<BlogTags> existing = BlogTags.GetAllBlogTags(conT)
                .Where(s => s.TagName.ToLower().Trim() == txtName.Text.ToLower().Trim())
                .ToList();

            BlogTags cat = new BlogTags();
            cat.TagName = txtName.Text.Trim();
            cat.TagUrl = txtUrl.Text.Trim();
            cat.MetaKeys = txtMetaKeys.Text.Trim();
            cat.MetaDesc = txtMetaDesc.Text.Trim();
            cat.PageTitle = txtPageTitle.Text.Trim();
            cat.AddedIP = CommonModel.IPAddress();
            cat.AddedBy = Request.Cookies["t_aid"].Value;
            cat.AddedOn = CommonModel.UTCTime();
            cat.Status = "Active";

            if (btnSave.Text == "Update")
            {
                if (existing.Count > 0 &&
                    existing[0].Id != Convert.ToInt32(Request.QueryString["id"]))
                {
                    strErrorMsg = "Blog tag already exists.";
                    return;
                }

                cat.Id = Convert.ToInt32(Request.QueryString["id"]);
                int result = BlogTags.UpdateBlogTags(conT, cat);
                if (result > 0)
                {
                    GetTagDetails();
                    strSuccessMsg = "Tag updated successfully.";
                }
                else strErrorMsg = "Something went wrong. Please try again.";
            }
            else
            {
                if (existing.Count > 0)
                {
                    strErrorMsg = "Blog tag already exists.";
                    return;
                }

                cat.DisplayOrder = 10000;
                int result = BlogTags.InsertBlogTags(conT, cat);
                if (result > 0)
                {
                    txtName.Text = txtUrl.Text = txtOrder.Text = "";
                    txtMetaDesc.Text = txtMetaKeys.Text = txtPageTitle.Text = "";
                    strSuccessMsg = "Tag added successfully.";
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

    // ===================== CLEAR =====================

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtName.Text = txtUrl.Text = txtOrder.Text = "";
        txtMetaDesc.Text = txtMetaKeys.Text = txtPageTitle.Text = "";
        btnSave.Text = "Save";
        Response.Redirect("blog-tags.aspx");
    }

    // ===================== WEB METHOD — DELETE =====================

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            BlogTags cat = new BlogTags();
            cat.Id = Convert.ToInt32(id);
            cat.AddedOn = CommonModel.UTCTime();
            cat.AddedIP = CommonModel.IPAddress();
            cat.AddedBy = HttpContext.Current.Request.Cookies["t_aid"].Value;

            return BlogTags.DeleteBlogTags(conT, cat) > 0 ? "Success" : "W";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "Delete", ex.Message);
            return "W";
        }
    }
}