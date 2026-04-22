using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_my_profile : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    public string strThumbImage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["t_aid"] == null)
        {
            Response.Redirect("dashboard.aspx");
        }
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");

        if (!IsPostBack)
        {
            GetProfileDetails();
        }
    }
    public void GetProfileDetails()
    {
        try
        {
            List<CreateUser> comps = CreateUser.GetAllUser(conT).Where(x => x.UserGuid == Request.Cookies["t_aid"].Value).ToList();
            if (comps.Count > 0)
            {
                btnSave.Text = "Update";
                txtContactNo.Text = comps[0].ContactNo;
                txtEmail.Text = comps[0].EmailId;
                txtUserId.Text = comps[0].UserId;
                txtName.Text = comps[0].UserName;
                if (comps[0].ProfileImage != "")
                {
                    strThumbImage = "<img src='/" + comps[0].ProfileImage + @"' style='max-height:50px;' />";
                    lblThumb.Text = comps[0].ProfileImage;
                }

                txtUserId.ReadOnly = true;
                ddlRole.SelectedIndex = ddlRole.Items.IndexOf(ddlRole.Items.FindByValue(comps[0].UserRole));
                ddlRole.Enabled = false;
                ddlRole.CssClass = "form-control mb-2 mr-sm-2";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProfileDetails", ex.Message);
        }
    }
    public string UploadProfileImage()
    {
        #region upload image
        string thumbImage = "";
        string guid = Guid.NewGuid().ToString();
        if (fuProfile.HasFile)
        {
            string fileExtension = Path.GetExtension(fuProfile.PostedFile.FileName.ToLower()), ImageGuid1 = guid + "_profile".Replace(" ", "-").Replace(".", "");
            string iconPath = Server.MapPath(".") + "\\../UploadImages\\" + ImageGuid1 + "" + fileExtension;
            if ((fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".webp"))
            {
                try
                {
                    if (File.Exists(Server.MapPath("~/" + Convert.ToString(lblThumb.Text))))
                    {
                        File.Delete(Server.MapPath("~/" + Convert.ToString(lblThumb.Text)));
                    }
                }
                catch (Exception eeex)
                {

                }

                if (fileExtension == ".webp")
                {
                    fuProfile.SaveAs(iconPath);
                }
                else if (fileExtension == ".png")
                {
                    System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(fuProfile.PostedFile.InputStream);
                    System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                    CommonModel.SavePNG(iconPath, objImagesmallBig, 99);
                }
                else
                {
                    System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(fuProfile.PostedFile.InputStream);
                    System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                    CommonModel.SaveJpeg(iconPath, objImagesmallBig, 99);
                } 
                thumbImage = "UploadImages/" + ImageGuid1 + "" + fileExtension;
            }
            else
            {
                return "Format";
            }
        }
        else
        {
            thumbImage = lblThumb.Text;
        }
        #endregion
        return thumbImage;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblStatus.Visible = true;
        try
        {
            if (Page.IsValid)
            {
                string pageName = Path.GetFileName(Request.Path);
                if (CreateUser.CheckAccess(conT, pageName, "Edit", Request.Cookies["t_aid"].Value))
                {
                    var upload = UploadProfileImage();
                    if(upload == "Format")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format. Please upload .png, .jpeg, .jpg, .webp, .gif',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        return;
                    }
                    CreateUser comp = new CreateUser();
                    comp.UserGuid = Request.Cookies["t_aid"].Value;
                    //comp.AddedBy = Request.Cookies["t_aid"].Value;
                    comp.ContactNo = txtContactNo.Text.Trim();
                    comp.EmailId = txtEmail.Text.Trim();
                    comp.UserName = txtName.Text.Trim();
                    comp.UserRole = ddlRole.SelectedItem.Text.Trim();
                    comp.ContactNo = txtContactNo.Text.Trim();
                    comp.UserId = txtUserId.Text.Trim();
                    comp.ProfileImage = UploadProfileImage();
                    comp.AddedIP = CommonModel.IPAddress();
                    comp.AddedOn = TimeStamps.UTCTime();
                    comp.Status = "Active";
                    int result = CreateUser.UpdateUserByUser(conT, comp);
                    if (result > 0)
                    {
                        GetProfileDetails();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Profile Updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
 
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Access denied. Please contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                } 
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }
}