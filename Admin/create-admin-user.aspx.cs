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
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");

      if (!IsPostBack)
        {
            BindRoles();
            if (Request.QueryString["id"] != null)
            {
                GetUserDetails();
            }
        }
    }
    public void GetUserDetails()
    {
        try
        {
            List<CreateUser> comps = CreateUser.GetAllUser(conT).Where(x => x.Id ==Convert.ToInt32(Request.QueryString["id"])).ToList();
            if (comps.Count > 0)
            {
                btnSave.Text = "Update";
                txtName.Text = comps[0].UserName;
                txtContactNo.Text = comps[0].ContactNo;
                txtEmail.Text = comps[0].EmailId; 
                txtPassword.Text = CommonModel.Decrypt(comps[0].Password);
                txtUserId.Text = comps[0].UserId;
                if (comps[0].ProfileImage != "")
                {
                    strThumbImage = "<img src='/" + comps[0].ProfileImage + @"' style='max-height:50px;' />";
                    lblThumb.Text = comps[0].ProfileImage;
                }
                ddlRole.SelectedIndex = ddlRole.Items.IndexOf(ddlRole.Items.FindByValue(comps[0].UserRole));

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetUserDetails", ex.Message);
        }
    }    
    
    public void BindRoles()
    {
        try
        {
            ddlRole.Items.Clear();
            List<UserRoles> comps = UserRoles.GetAllUserRoles(conT);
            if (comps.Count > 0)
            {
                ddlRole.DataSource = comps;
                ddlRole.DataTextField = "RoleName";
                ddlRole.DataValueField = "Id";
                ddlRole.DataBind();
            }
            ddlRole.Items.Insert(0, new ListItem("Select Role", ""));
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindRoles", ex.Message);
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
                CreateUser ca = new CreateUser();
                if (btnSave.Text == "Update")
                {

                    if (CreateUser.CheckAccess(conT, pageName, "Edit", Request.Cookies["t_aid"].Value))
                    {
                        var upload = UploadProfileImage();
                        if (upload == "Format")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format. Please upload .png, .jpeg, .jpg, .webp, .gif',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                            return;
                        }
                        ca.UserName = txtName.Text.Trim();
                        ca.Id = Convert.ToInt32(Request.QueryString["id"]);
                        ca.ContactNo = txtContactNo.Text.Trim();
                        ca.ProfileImage = upload;
                        ca.UserRole = ddlRole.SelectedItem.Value;
                        ca.EmailId = txtEmail.Text.Trim();
                        ca.Password = CommonModel.Encrypt(txtPassword.Text);
                        ca.UserId = txtUserId.Text.Trim();
                        ca.AddedIP = CommonModel.IPAddress();
                        ca.AddedOn = TimeStamps.UTCTime();
                        ca.Status = "Active";
                       // ca.AddedBy = Request.Cookies["t_aid"].Value;
                        int result = CreateUser.UpdateUser(conT, ca);
                        if (result > 0)
                        {
                            GetUserDetails();
                            lblStatus.Text = "Details Updated successfully.";
                            lblStatus.Attributes.Add("class", "alert alert-success");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    }
                }
                else
                {
                    if (CreateUser.CheckAccess(conT, pageName, "Add", Request.Cookies["t_aid"].Value))
                    {
                        var upload = UploadProfileImage();
                        if (upload == "Format")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format. Please upload .png, .jpeg, .jpg, .webp, .gif',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                            return;
                        }
                        ca.UserGuid = Guid.NewGuid().ToString();
                        ca.UserRole = ddlRole.SelectedItem.Value;
                        ca.UserName = txtName.Text.Trim(); 
                        ca.ContactNo = txtContactNo.Text.Trim();
                        ca.EmailId = txtEmail.Text.Trim();
                        ca.ProfileImage = upload;
                        ca.Password = CommonModel.Encrypt(txtPassword.Text);
                        ca.UserId = txtUserId.Text.Trim();
                        ca.AddedIP = CommonModel.IPAddress();
                        ca.AddedOn = TimeStamps.UTCTime();
                        ca.Status = "Active";
                        //ca.AddedBy = Request.Cookies["t_aid"].Value;
                        int result = CreateUser.InsertUser(conT, ca);
                        if (result > 0)
                        {
                            txtName.Text = txtContactNo.Text = txtEmail.Text = txtPassword.Text = "";
                            txtPassword.Text = txtUserId.Text = "";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'User added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Access denied. Contact to your administrator',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("create-admin-user.aspx");
    }
}