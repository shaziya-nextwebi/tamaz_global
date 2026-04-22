using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ContactUs : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        // Always re-set the image URL — ImageUrl is not stored in ViewState
        FillCaptcha();

        if (!IsPostBack)
        {
            Session["captchanum"] = CreateRandomCode(5);
        }

        btnSubmit.Attributes.Add("onclick",
            "if (Page_ClientValidate('ContactForm')) { this.disabled = true; this.value='Please Wait...'; "
            + Page.ClientScript.GetPostBackEventReference(btnSubmit, null) + "; }");
    }

    void FillCaptcha()
    {
        Image1.ImageUrl = ResolveUrl("~/capcha.aspx") + "?" + DateTime.Now.Ticks.ToString();
    }
    string CreateRandomCode(int length)
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        var rnd = new Random();
        char[] code = new char[length];
        for (int i = 0; i < length; i++)
            code[i] = chars[rnd.Next(chars.Length)];
        return new string(code);
    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        Session["captchanum"] = CreateRandomCode(5);
        FillCaptcha();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;

        // Captcha check
        if (!string.Equals(txtCaptcha.Text.Trim(),
                           Convert.ToString(Session["captchanum"]),
                           StringComparison.OrdinalIgnoreCase))
        {
            lblStatus.Visible = true;
            lblStatus.Text = "Invalid verification code. Please try again.";
            lblStatus.Attributes["class"] = "block mb-4 px-4 py-3 rounded-lg text-sm font-medium bg-red-50 text-red-700 border border-red-200";
            Session["captchanum"] = CreateRandomCode(5);
            FillCaptcha();
            txtCaptcha.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "rstBtn", "resetButton()", true);
            return;
        }

        try
        {
            // Email is not in the Contact model/DB — capture it for emails only
            string userEmail = txtEmail.Text.Trim();

            Contact obj = new Contact
            {
                UserName = txtName.Text.Trim(),
                PhoneNo = txtPhone.Text.Trim(),
                City = txtCity.Text.Trim(),
                Comments = txtMessage.Text.Trim(),
                SourcePage = Request.Url.AbsolutePath,
                AddedOn = DateTime.Now,
                AddedIp = CommonModel.IPAddress(),
                Status = "Active"
            };

            int result = Contact.InsertContact(conT, obj);

            if (result > 0)
            {
                SendEmailToUser(obj.UserName, userEmail);
                SendEmailToAdmin(obj.UserName, userEmail, obj.PhoneNo, obj.City, obj.Comments);

                // Clear form
                txtName.Text = "";
                txtEmail.Text = "";
                txtPhone.Text = "";
                txtCity.Text = "";
                txtMessage.Text = "";
                txtCaptcha.Text = "";
                Session["captchanum"] = CreateRandomCode(5);
                FillCaptcha();

                //Response.Redirect("thank-you.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                ShowSnackbarError("Oops! Something went wrong. Please try again later.");
                ScriptManager.RegisterStartupScript(this, GetType(), "rstBtn", "resetButton()", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "btnSubmit_Click", ex.Message);
            ShowSnackbarError("Oops! Something went wrong. Please try again later.");
            ScriptManager.RegisterStartupScript(this, GetType(), "rstBtn", "resetButton()", true);
        }
    }

 
    private void SendEmailToUser(string name, string email)
    {
        try
        {
            string mailBody = @"<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='UTF-8'/>
<meta name='viewport' content='width=device-width,initial-scale=1.0'/>
<title>Thank You - TAMAZ Global</title>
</head>
<body style='margin:0;padding:0;background-color:#f8fafc;font-family:""Segoe UI"",Arial,sans-serif;'>
  <table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:#f8fafc;'>
    <tr>
      <td align='center' style='padding:40px 16px;'>
        <table width='600' cellpadding='0' cellspacing='0' border='0'
          style='max-width:600px;width:100%;background-color:#ffffff;border-radius:16px;
                 overflow:hidden;border:1px solid #e2e8f0;'>

          <tr><td style='height:5px;background:linear-gradient(90deg,#991b1b,#b91c1c,#991b1b);'></td></tr>

          <tr>
            <td align='center' style='padding:40px 40px 24px;'>
              <h1 style='font-size:26px;color:#0f172a;margin:0 0 10px;font-weight:700;'>
                Thank You, " + name + @"!
              </h1>
              <p style='font-size:15px;color:#64748b;margin:0;line-height:1.6;'>
                We have received your message and will get back to you within 24 hours.
              </p>
            </td>
          </tr>

          <tr><td style='height:1px;background:#e2e8f0;'></td></tr>

          <tr>
            <td style='padding:32px 40px;'>
              <p style='font-size:15px;color:#475569;line-height:1.7;margin:0 0 20px;'>
                Thank you for reaching out to <strong>TAMAZ Global Trading Co.</strong>
                Our team will review your enquiry and contact you shortly.
              </p>
              <table cellpadding='0' cellspacing='0' border='0' width='100%' style='margin-bottom:12px;'>
                <tr>
                  <td style='background:#fef2f2;border:1px solid #fecaca;border-radius:10px;padding:14px 18px;'>
                    <p style='font-size:13px;color:#7f1d1d;margin:0 0 4px;font-weight:600;'>&#128222; Phone</p>
                    <p style='font-size:14px;color:#991b1b;margin:0;'>
                      +91 99882 27622 &nbsp;|&nbsp; +91 990074 6748 (Wholesale)
                    </p>
                  </td>
                </tr>
              </table>
              <table cellpadding='0' cellspacing='0' border='0' width='100%'>
                <tr>
                  <td style='background:#fef2f2;border:1px solid #fecaca;border-radius:10px;padding:14px 18px;'>
                    <p style='font-size:13px;color:#7f1d1d;margin:0 0 4px;font-weight:600;'>&#128231; Email</p>
                    <p style='font-size:14px;color:#991b1b;margin:0;'>sales@tamazglobal.com</p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>

          <tr><td style='height:1px;background:#e2e8f0;'></td></tr>

          <tr>
            <td style='padding:20px 40px 30px;background:#f8fafc;'>
              <p style='font-size:12px;color:#94a3b8;margin:0;text-align:center;'>
                &copy; TAMAZ Global Trading Co. | Frazer Town, Bangalore - 560005
              </p>
            </td>
          </tr>

          <tr><td style='height:5px;background:linear-gradient(90deg,#991b1b,#b91c1c,#991b1b);'></td></tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>";

            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Thank You for Contacting TAMAZ Global";
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            BuildSmtpClient().Send(mail);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "SendEmailToUser", ex.Message);
        }
    }


    private void SendEmailToAdmin(string name, string email, string phone, string city, string comments)
    {
        try
        {
            string mailBody =
                "Hello Admin,<br><br>" +
                "You have received a new <strong>Contact Us</strong> request from <strong>" + name + "</strong>.<br><br>" +
                "<u><b>Details:</b></u><br>" +
                "Name&nbsp;&nbsp;&nbsp;&nbsp; : " + name + "<br>" +
                "Email&nbsp;&nbsp;&nbsp;&nbsp; : " + email + "<br>" +
                "Phone&nbsp;&nbsp;&nbsp;&nbsp; : " + phone + "<br>" +
                "City&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : " + (string.IsNullOrWhiteSpace(city) ? "—" : city) + "<br>" +
                "Message&nbsp; : " + (string.IsNullOrWhiteSpace(comments) ? "—" : comments) + "<br><br>" +
                "Regards,<br>TAMAZ Global Website<br>https://www.tamazglobal.com/";

            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["ToMail"]);

            string cc = ConfigurationManager.AppSettings["CCMail"];
            string bcc = ConfigurationManager.AppSettings["BCCMail"];
            if (!string.IsNullOrWhiteSpace(cc)) mail.CC.Add(cc);
            if (!string.IsNullOrWhiteSpace(bcc)) mail.Bcc.Add(bcc);

            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "New Contact Us Request - TAMAZ Global";
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            BuildSmtpClient().Send(mail);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "SendEmailToAdmin", ex.Message);
        }
    }


    private SmtpClient BuildSmtpClient()
    {
        return new SmtpClient
        {
            Host = ConfigurationManager.AppSettings["host"],
            Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]),
            EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]),
            Credentials = new System.Net.NetworkCredential(
                ConfigurationManager.AppSettings["userName"],
                ConfigurationManager.AppSettings["password"])
        };
    }

    private void ShowSnackbarError(string msg)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "Message",
            "Snackbar.show({{pos:'top-right',text:'{msg}',actionTextColor:'#fff',backgroundColor:'#ea1c1c'}});",
            true);
    }
}