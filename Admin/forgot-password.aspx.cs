using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

public partial class Admin_forgot_password : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblStatus.Visible = true;
        try
        {
            if (Page.IsValid)
            {
                string logins = CreateUser.ResetPassword(conT, txtEmail.Text.Trim());
                if (logins != "")
                {
                    string r_id = Guid.NewGuid().ToString();
                    int reset = CreateUser.SetRestId(conT, logins, r_id);
                    var username = CreateUser.GetLoggedUserName(conT, Convert.ToString(logins));
                    var link = ConfigurationManager.AppSettings["domain"] + "admin/reset-password.aspx?r=" + r_id;

                    string button = "<table style='border-collapse:collapse;width:99%;' border='0'><tbody><td style='padding: 15px;' align='center'><a style='padding:10px 15px;background-color:#4caf50 !important;color:#fff;border-radius:5px;white-space:nowrap;text-decoration:none;' href='" + link + "'>Reset Password</a></td></tr></tbody></table>";

                    string mailbody = @"<p><strong>If you have made this request, please click on the following link to reset your password</strong></p>
                                             " + button + @"             
                                        <p>If clicking the button does not work, copy the URL below and paste it into your browser:<br>" + link + @" <br><br>If you have not requested a password reset, please ignore this email.<br> Your password remains unchanged.</p>";

                    Emails.SendPasswordRestLink("Tamaz GlobalReset Password", logins, txtEmail.Text.Trim(), link, "Tamaz GlobalReset Password", mailbody);
                    if (reset >= 1)
                    {
                        lblStatus.Text = "<strong>Success !</strong><br/>Password reset link has been sent to your email address";
                        lblStatus.Attributes.Add("class", "alert alert-success d-block");
                    }
                    else
                    {
                        lblStatus.Text = "<strong>Error !</strong><br/>There is some problem now. Please try after some time";
                        lblStatus.Attributes.Add("class", "alert alert-danger d-block");
                    }
                }
                else
                {
                    lblStatus.Text = "<strong>Error !</strong><br/>Entered email is not registered";
                    lblStatus.Attributes.Add("class", "alert alert-danger d-block");
                }
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "<strong>Error !</strong><br/>There is some problem now. Please try after some time";
            lblStatus.Attributes.Add("class", "alert alert-danger d-block");
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnLogin_Click", ex.Message);
        }
    }
}