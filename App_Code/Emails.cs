using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Net;
using System.IO;
using System.Threading.Tasks;

public class Emails
{
    public static string SendAwsMailEnqMainWebsiteUser(string sendername, string mailbody, string toEmail, string subject)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(toEmail);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["fromMainSite"], sendername);
            mail.Subject = subject;
            mail.Body = mailbody;
            if (ConfigurationManager.AppSettings["replyToMainSite"] != "")
            {
                mail.ReplyToList.Add(new MailAddress(ConfigurationManager.AppSettings["replyToMainSite"], sendername));
            }
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                 (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return "Success";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendAwsMailEnqMainWebsite", ex.Message);
            return ex.Message;
        }
    }

    public static string SendAwsMailEnqMainWebsite(string sendername, string mailbody, string subject)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["ToMailMainSite"]);
            if (ConfigurationManager.AppSettings["ToMailMainSite"] != "")
            {
                mail.CC.Add(ConfigurationManager.AppSettings["CCMailMainSite"]);
            }
            if (ConfigurationManager.AppSettings["BCCMailMainSite"] != "")
            {
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCCMailMainSite"]);
            }
            mail.From = new MailAddress(ConfigurationManager.AppSettings["fromMainSite"], sendername);
            mail.Subject = subject;

            mail.Body = mailbody;

            if (ConfigurationManager.AppSettings["replyToMainSite"] != "")
            {
                mail.ReplyToList.Add(new MailAddress(ConfigurationManager.AppSettings["replyToMainSite"], sendername));
            }

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                 (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return "Success";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendAwsMailEnqMainWebsite", ex.Message);
            return ex.Message;
        }
    }

    public static string SendAwsMailEnqMainWebsiteAttachment(string sendername, string mailbody, string subject, string filename, string postcontent)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["ToMailMainSite"]);
            if (ConfigurationManager.AppSettings["CCMailMainSite"] != "")
            {
                mail.CC.Add(ConfigurationManager.AppSettings["CCMailMainSite"]);
            }
            if (ConfigurationManager.AppSettings["BCCMailMainSite"] != "")
            {
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCCMailMainSite"]);
            }
            mail.From = new MailAddress(ConfigurationManager.AppSettings["fromMainSite"], sendername);
            mail.Subject = subject;
            if (ConfigurationManager.AppSettings["replyToMainSite"] != "")
            {
                mail.ReplyToList.Add(new MailAddress(ConfigurationManager.AppSettings["replyToMainSite"], sendername));
            }
            mail.Body = mailbody;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            foreach (string urls in postcontent.Split('|'))
            {
                var stream = new WebClient().OpenRead(urls.Split('#')[1]);
                string filename1 = Path.GetFileName(urls.Split('#')[0]);
                mail.Attachments.Add(new Attachment(stream, urls.Split('#')[0]));
            }
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();

            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                 (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);

            return "Success";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendAwsMailEnqMainWebsiteAttachment", ex.Message);
            return ex.Message;
        }
    }

    public static int SendAwsHRMailEnqMainWebsiteAttachment(string sendername, string mailbody, string subject, string filename, string postcontent)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["ToMail"]);
            if (ConfigurationManager.AppSettings["CCMail"] != "")
            {
                mail.CC.Add(ConfigurationManager.AppSettings["CCMail"]);
            }

            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = subject;

            mail.Body = mailbody;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            foreach (string urls in postcontent.Split('|'))
            {
                var stream = new WebClient().OpenRead(urls.Split('#')[1]);
                string filename1 = Path.GetFileName(urls.Split('#')[0]);
                mail.Attachments.Add(new Attachment(stream, urls.Split('#')[0]));
            }
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();

            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                 (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);

            smtp.Send(mail);
            return 1;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendAwsMailEnqMainWebsiteAttachment", ex.Message);
            return 0;
        }
    }

    public static int SendMailToStudent(List<string> lst, string comments, string stuName, string eMail)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(eMail);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], "A2N Academy");
            mail.Subject = "A2N Academy Contact Request - Reply";
            string mailBody = "Dear " + stuName + ",<br><br>" + comments + "<br><br>Regards,<br>A2N Academy";
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            foreach (string docs in lst)
            {
                string path = HttpContext.Current.Server.MapPath("~/") + docs;
                mail.Attachments.Add(new Attachment(path));
            }

            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);

            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception exx)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendMailToStudent", exx.Message);
            return 0;
        }
    }

    public static int SendPasswordRestLink(string name, string emails, string link)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(emails);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "A2N Reset Password";
            mail.Body = "Hello " + name + ", <br><br>Please click on the link below to reset your password<br>" + link + "<br><br>Thanks,<br>Nextwebi";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                           (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);

            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Login", ex.Message);
            return 0;
        }
    }


    public static int SendRegistered(string name, string emails)
    {
        try
        {
            #region mailBodys
            string mailBody = @"<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='UTF-8' />
<meta name='viewport' content='width=device-width, initial-scale=1.0' />
<meta http-equiv='X-UA-Compatible' content='IE=edge' />
<title>Welcome to A2N</title>
<link href='https://fonts.googleapis.com/css2?family=DM+Serif+Display&family=DM+Sans:wght@400;500;600&display=swap' rel='stylesheet' />
</head>
<body style='margin:0;padding:0;background-color:#0f0f0f;font-family:""DM Sans"",sans-serif;'>
  <table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:#0f0f0f;'>
    <tr>
      <td align='center' style='padding:40px 16px;'>
        <table width='600' cellpadding='0' cellspacing='0' border='0' style='max-width:600px;width:100%;background-color:#1a1a1a;border-radius:16px;overflow:hidden;border:1px solid #2a2a2a;'>

          <!-- Header accent bar -->
          <tr>
            <td style='height:4px;background:linear-gradient(90deg,#c9a84c 0%,#e8c97a 50%,#c9a84c 100%);'></td>
          </tr>

          <!-- Logo + Header -->
          <tr>
            <td align='center' style='padding:40px 40px 32px;background-color:#1a1a1a;'>
              <img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/a2n-logo.png' width='100' alt='A2N Academy' style='display:block;margin:0 auto 28px;' />
              <div style='display:inline-block;background:rgba(201,168,76,0.12);border:1px solid rgba(201,168,76,0.3);border-radius:50px;padding:6px 18px;margin-bottom:20px;'>
                <span style='font-family:""DM Sans"",sans-serif;font-size:11px;font-weight:600;color:#c9a84c;letter-spacing:2px;text-transform:uppercase;'>Account Created</span>
              </div>
              <h1 style='font-family:""DM Serif Display"",serif;font-size:32px;color:#f5f0e8;margin:0 0 12px;font-weight:400;line-height:1.2;'>Welcome aboard, " + name + @".</h1>
              <p style='font-family:""DM Sans"",sans-serif;font-size:15px;color:#888;margin:0;line-height:1.6;'>Your A2N Academy account has been successfully created.<br/>You're now part of our community.</p>
            </td>
          </tr>

          <!-- Divider -->
          <tr><td style='height:1px;background:linear-gradient(90deg,transparent,#2e2e2e,transparent);margin:0 40px;'></td></tr>

          <!-- Body content -->
          <tr>
            <td style='padding:36px 40px;'>
              <p style='font-family:""DM Sans"",sans-serif;font-size:15px;color:#aaa;line-height:1.7;margin:0 0 28px;'>
                Thank you for becoming a registered member. Your account is ready — you can now log in, browse our courses, and start your learning journey right away.
              </p>

              <!-- CTA Button -->
              <table cellpadding='0' cellspacing='0' border='0' style='margin:0 auto;'>
                <tr>
                  <td align='center' style='background:linear-gradient(135deg,#c9a84c,#e8c97a);border-radius:10px;'>
                    <a href='" + ConfigurationManager.AppSettings["domain"] + @"' target='_blank' style='display:inline-block;padding:15px 36px;font-family:""DM Sans"",sans-serif;font-size:15px;font-weight:600;color:#0f0f0f;text-decoration:none;letter-spacing:0.3px;'>Go to My Account &rarr;</a>
                  </td>
                </tr>
              </table>
            </td>
          </tr>

          <!-- Divider -->
          <tr><td style='height:1px;background:linear-gradient(90deg,transparent,#2e2e2e,transparent);'></td></tr>

          <!-- Support note -->
          <tr>
            <td style='padding:24px 40px;'>
              <p style='font-family:""DM Sans"",sans-serif;font-size:13px;color:#555;line-height:1.6;margin:0;'>
                Have questions? Reach us at <a href='mailto:connect@a2nacademy.com' style='color:#c9a84c;text-decoration:none;'>connect@a2nacademy.com</a> or visit our <a href='" + ConfigurationManager.AppSettings["domain"] + @"/contact' target='_blank' style='color:#c9a84c;text-decoration:none;'>Contact page</a>.
              </p>
            </td>
          </tr>

          <!-- Footer -->
          <tr>
            <td style='padding:24px 40px 32px;background-color:#141414;border-top:1px solid #222;'>
              <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                <tr>
                  <td align='center' style='padding-bottom:16px;'>
                    <a href='https://www.facebook.com/nextwebi' target='_blank' style='display:inline-block;margin:0 6px;'><img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/email-icons/facebook.png' width='22' alt='Facebook' style='display:block;' /></a>
                    <a href='https://www.instagram.com/nextwebi/' target='_blank' style='display:inline-block;margin:0 6px;'><img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/email-icons/instagram.png' width='22' alt='Instagram' style='display:block;' /></a>
                    <a href='https://twitter.com/nextwebi_' target='_blank' style='display:inline-block;margin:0 6px;'><img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/email-icons/twitter.png' width='22' alt='Twitter' style='display:block;' /></a>
                    <a href='mailto:connect@a2nacademy.com' target='_blank' style='display:inline-block;margin:0 6px;'><img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/email-icons/mail.png' width='22' alt='Email' style='display:block;' /></a>
                  </td>
                </tr>
                <tr>
                  <td align='center'>
                    <p style='font-family:""DM Sans"",sans-serif;font-size:12px;color:#444;margin:0;'>&copy; A2N Academy. All rights reserved.</p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>

          <!-- Bottom accent bar -->
          <tr>
            <td style='height:4px;background:linear-gradient(90deg,#c9a84c 0%,#e8c97a 50%,#c9a84c 100%);'></td>
          </tr>

        </table>
      </td>
    </tr>
  </table>
</body>
</html>";
            #endregion

            MailMessage mail = new MailMessage();
            mail.To.Add(emails);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "Welcome to A2N - " + name;
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                           (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);

            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Login", ex.Message);
            return 0;
        }
    }

    public static int SendEmailVerifyLink(string email, string name, string strlink)
    {
        try
        {
            #region MailBody
            string mailBody = @"<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='UTF-8' />
<meta name='viewport' content='width=device-width, initial-scale=1.0' />
<meta http-equiv='X-UA-Compatible' content='IE=edge' />
<title>Verify Your Email</title>
<link href='https://fonts.googleapis.com/css2?family=DM+Serif+Display&family=DM+Sans:wght@400;500;600&display=swap' rel='stylesheet' />
</head>
<body style='margin:0;padding:0;background-color:#0f0f0f;font-family:""DM Sans"",sans-serif;'>
  <table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:#0f0f0f;'>
    <tr>
      <td align='center' style='padding:40px 16px;'>
        <table width='600' cellpadding='0' cellspacing='0' border='0' style='max-width:600px;width:100%;background-color:#1a1a1a;border-radius:16px;overflow:hidden;border:1px solid #2a2a2a;'>

          <!-- Header accent bar -->
          <tr>
            <td style='height:4px;background:linear-gradient(90deg,#3b82f6 0%,#60a5fa 50%,#3b82f6 100%);'></td>
          </tr>

          <!-- Logo + Header -->
          <tr>
            <td align='center' style='padding:40px 40px 32px;background-color:#1a1a1a;'>
              <img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/a2n-logo.png' width='100' alt='A2N Academy' style='display:block;margin:0 auto 28px;' />
              <div style='display:inline-block;background:rgba(59,130,246,0.12);border:1px solid rgba(59,130,246,0.3);border-radius:50px;padding:6px 18px;margin-bottom:20px;'>
                <span style='font-family:""DM Sans"",sans-serif;font-size:11px;font-weight:600;color:#60a5fa;letter-spacing:2px;text-transform:uppercase;'>Email Verification</span>
              </div>
              <h1 style='font-family:""DM Serif Display"",serif;font-size:32px;color:#f5f0e8;margin:0 0 12px;font-weight:400;line-height:1.2;'>One last step, " + name + @".</h1>
              <p style='font-family:""DM Sans"",sans-serif;font-size:15px;color:#888;margin:0;line-height:1.6;'>Please verify your email address to activate<br/>your A2N Academy account.</p>
            </td>
          </tr>

          <!-- Divider -->
          <tr><td style='height:1px;background:linear-gradient(90deg,transparent,#2e2e2e,transparent);'></td></tr>

          <!-- Body content -->
          <tr>
            <td style='padding:36px 40px;'>
              <p style='font-family:""DM Sans"",sans-serif;font-size:15px;color:#aaa;line-height:1.7;margin:0 0 28px;'>
                You have successfully registered. Click the button below to verify your email and gain full access to your account.
              </p>

              <!-- CTA Button -->
              <table cellpadding='0' cellspacing='0' border='0' style='margin:0 auto 28px;'>
                <tr>
                  <td align='center' style='background:linear-gradient(135deg,#3b82f6,#60a5fa);border-radius:10px;'>
                    <a href='" + strlink + @"' target='_blank' style='display:inline-block;padding:15px 36px;font-family:""DM Sans"",sans-serif;font-size:15px;font-weight:600;color:#fff;text-decoration:none;letter-spacing:0.3px;'>Verify My Email &rarr;</a>
                  </td>
                </tr>
              </table>

              <!-- Fallback link box -->
              <table cellpadding='0' cellspacing='0' border='0' width='100%'>
                <tr>
                  <td style='background-color:#222;border:1px solid #2e2e2e;border-radius:8px;padding:14px 18px;'>
                    <p style='font-family:""DM Sans"",sans-serif;font-size:12px;color:#555;margin:0 0 6px;text-transform:uppercase;letter-spacing:1px;'>Or copy this link into your browser</p>
                    <a href='" + strlink + @"' target='_blank' style='font-family:""DM Sans"",sans-serif;font-size:13px;color:#60a5fa;text-decoration:none;word-break:break-all;'>" + strlink + @"</a>
                  </td>
                </tr>
              </table>
            </td>
          </tr>

          <!-- Divider -->
          <tr><td style='height:1px;background:linear-gradient(90deg,transparent,#2e2e2e,transparent);'></td></tr>

          <!-- Support note -->
          <tr>
            <td style='padding:24px 40px;'>
              <p style='font-family:""DM Sans"",sans-serif;font-size:13px;color:#555;line-height:1.6;margin:0;'>
                Questions? Contact us at <a href='mailto:connect@a2nacademy.com' style='color:#60a5fa;text-decoration:none;'>connect@a2nacademy.com</a> or visit our <a href='" + ConfigurationManager.AppSettings["domain"] + @"/contact-us.aspx' target='_blank' style='color:#60a5fa;text-decoration:none;'>Contact page</a>.
              </p>
            </td>
          </tr>

          <!-- Footer -->
          <tr>
            <td style='padding:24px 40px 32px;background-color:#141414;border-top:1px solid #222;'>
              <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                <tr>
                  <td align='center' style='padding-bottom:16px;'>
                    <a href='https://www.facebook.com/nextwebi' target='_blank' style='display:inline-block;margin:0 6px;'><img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/email-icons/facebook.png' width='22' alt='Facebook' style='display:block;' /></a>
                    <a href='https://www.instagram.com/nextwebi/' target='_blank' style='display:inline-block;margin:0 6px;'><img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/email-icons/instagram.png' width='22' alt='Instagram' style='display:block;' /></a>
                    <a href='https://twitter.com/nextwebi_' target='_blank' style='display:inline-block;margin:0 6px;'><img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/email-icons/twitter.png' width='22' alt='Twitter' style='display:block;' /></a>
                    <a href='mailto:connect@a2nacademy.com' target='_blank' style='display:inline-block;margin:0 6px;'><img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/email-icons/mail.png' width='22' alt='Email' style='display:block;' /></a>
                  </td>
                </tr>
                <tr>
                  <td align='center'>
                    <p style='font-family:""DM Sans"",sans-serif;font-size:12px;color:#444;margin:0;'>&copy; A2N Academy. All rights reserved.</p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>

          <!-- Bottom accent bar -->
          <tr>
            <td style='height:4px;background:linear-gradient(90deg,#3b82f6 0%,#60a5fa 50%,#3b82f6 100%);'></td>
          </tr>

        </table>
      </td>
    </tr>
  </table>
</body>
</html>";
            #endregion

            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "A2N Account verification";
            mail.Body = mailBody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                           (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch
        {
            return 0;
        }
    }

    public static int SendPasswordReset(string name, string email, string custId)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);

            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], "A2N Password Reset");
            mail.Subject = "Request for password reset";

            #region mailBody
            string mailBody = @"<!DOCTYPE html>
<html lang='en'>
<head>
<meta charset='UTF-8' />
<meta name='viewport' content='width=device-width, initial-scale=1.0' />
<meta http-equiv='X-UA-Compatible' content='IE=edge' />
<title>Password Reset</title>
<link href='https://fonts.googleapis.com/css2?family=DM+Serif+Display&family=DM+Sans:wght@400;500;600&display=swap' rel='stylesheet' />
</head>
<body style='margin:0;padding:0;background-color:#0f0f0f;font-family:""DM Sans"",sans-serif;'>
  <table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:#0f0f0f;'>
    <tr>
      <td align='center' style='padding:40px 16px;'>
        <table width='600' cellpadding='0' cellspacing='0' border='0' style='max-width:600px;width:100%;background-color:#1a1a1a;border-radius:16px;overflow:hidden;border:1px solid #2a2a2a;'>

          <!-- Header accent bar -->
          <tr>
            <td style='height:4px;background:linear-gradient(90deg,#ef4444 0%,#f87171 50%,#ef4444 100%);'></td>
          </tr>

          <!-- Logo + Header -->
          <tr>
            <td align='center' style='padding:40px 40px 32px;background-color:#1a1a1a;'>
              <img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/a2n-logo.png' width='100' alt='A2N Academy' style='display:block;margin:0 auto 28px;' />
              <div style='display:inline-block;background:rgba(239,68,68,0.12);border:1px solid rgba(239,68,68,0.3);border-radius:50px;padding:6px 18px;margin-bottom:20px;'>
                <span style='font-family:""DM Sans"",sans-serif;font-size:11px;font-weight:600;color:#f87171;letter-spacing:2px;text-transform:uppercase;'>Password Reset</span>
              </div>
              <h1 style='font-family:""DM Serif Display"",serif;font-size:32px;color:#f5f0e8;margin:0 0 12px;font-weight:400;line-height:1.2;'>Reset your password, " + name + @".</h1>
              <p style='font-family:""DM Sans"",sans-serif;font-size:15px;color:#888;margin:0;line-height:1.6;'>We received a request to reset your A2N Academy password.<br/>If this was you, proceed below.</p>
            </td>
          </tr>

          <!-- Divider -->
          <tr><td style='height:1px;background:linear-gradient(90deg,transparent,#2e2e2e,transparent);'></td></tr>

          <!-- Body content -->
          <tr>
            <td style='padding:36px 40px;'>
              <p style='font-family:""DM Sans"",sans-serif;font-size:15px;color:#aaa;line-height:1.7;margin:0 0 28px;'>
                Click the button below to set a new password. This link is unique to your account — do not share it with anyone.
              </p>

              <!-- CTA Button -->
              <table cellpadding='0' cellspacing='0' border='0' style='margin:0 auto 28px;'>
                <tr>
                  <td align='center' style='background:linear-gradient(135deg,#ef4444,#f87171);border-radius:10px;'>
                    <a href='" + ConfigurationManager.AppSettings["domain"] + @"/reset-password.aspx?c=" + custId + @"' target='_blank' style='display:inline-block;padding:15px 36px;font-family:""DM Sans"",sans-serif;font-size:15px;font-weight:600;color:#fff;text-decoration:none;letter-spacing:0.3px;'>Reset My Password &rarr;</a>
                  </td>
                </tr>
              </table>

              <!-- Fallback link box -->
              <table cellpadding='0' cellspacing='0' border='0' width='100%' style='margin-bottom:24px;'>
                <tr>
                  <td style='background-color:#222;border:1px solid #2e2e2e;border-radius:8px;padding:14px 18px;'>
                    <p style='font-family:""DM Sans"",sans-serif;font-size:12px;color:#555;margin:0 0 6px;text-transform:uppercase;letter-spacing:1px;'>Or copy this link into your browser</p>
                    <a href='" + ConfigurationManager.AppSettings["domain"] + @"/reset-password.aspx?c=" + custId + @"' target='_blank' style='font-family:""DM Sans"",sans-serif;font-size:13px;color:#f87171;text-decoration:none;word-break:break-all;'>" + ConfigurationManager.AppSettings["domain"] + @"/reset-password.aspx?c=" + custId + @"</a>
                  </td>
                </tr>
              </table>

              <!-- Disclaimer -->
              <table cellpadding='0' cellspacing='0' border='0' width='100%'>
                <tr>
                  <td style='background-color:#1f1a1a;border:1px solid #2e2222;border-radius:8px;padding:14px 18px;'>
                    <p style='font-family:""DM Sans"",sans-serif;font-size:13px;color:#666;margin:0;line-height:1.6;'>
                      &#128274; If you did not request a password reset, you can safely ignore this email. Your password will remain unchanged.
                    </p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>

          <!-- Divider -->
          <tr><td style='height:1px;background:linear-gradient(90deg,transparent,#2e2e2e,transparent);'></td></tr>

          <!-- Support note -->
          <tr>
            <td style='padding:24px 40px;'>
              <p style='font-family:""DM Sans"",sans-serif;font-size:13px;color:#555;line-height:1.6;margin:0;'>
                Need help? Contact us at <a href='mailto:connect@a2nacademy.com' style='color:#f87171;text-decoration:none;'>connect@a2nacademy.com</a> or visit our <a href='" + ConfigurationManager.AppSettings["domain"] + @"/contact-us.aspx' target='_blank' style='color:#f87171;text-decoration:none;'>Contact page</a>.
              </p>
            </td>
          </tr>

          <!-- Footer -->
          <tr>
            <td style='padding:24px 40px 32px;background-color:#141414;border-top:1px solid #222;'>
              <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                <tr>
                  <td align='center' style='padding-bottom:16px;'>
                    <a href='https://www.facebook.com/nextwebi' target='_blank' style='display:inline-block;margin:0 6px;'><img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/email-icons/facebook.png' width='22' alt='Facebook' style='display:block;' /></a>
                    <a href='https://www.instagram.com/nextwebi/' target='_blank' style='display:inline-block;margin:0 6px;'><img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/email-icons/instagram.png' width='22' alt='Instagram' style='display:block;' /></a>
                    <a href='https://twitter.com/nextwebi_' target='_blank' style='display:inline-block;margin:0 6px;'><img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/email-icons/twitter.png' width='22' alt='Twitter' style='display:block;' /></a>
                    <a href='mailto:connect@a2nacademy.com' target='_blank' style='display:inline-block;margin:0 6px;'><img src='" + ConfigurationManager.AppSettings["domain"] + @"/img/email-icons/mail.png' width='22' alt='Email' style='display:block;' /></a>
                  </td>
                </tr>
                <tr>
                  <td align='center'>
                    <p style='font-family:""DM Sans"",sans-serif;font-size:12px;color:#444;margin:0;'>&copy; A2N Academy. All rights reserved.</p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>

          <!-- Bottom accent bar -->
          <tr>
            <td style='height:4px;background:linear-gradient(90deg,#ef4444 0%,#f87171 50%,#ef4444 100%);'></td>
          </tr>

        </table>
      </td>
    </tr>
  </table>
</body>
</html>";
            #endregion

            mail.Body = mailBody;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();


            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);

            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);
            return 1;
        }
        catch (Exception exx)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "SendPasswordReset", exx.Message);
            return 0;
        }
    }

    #region Contact and Product Enquity Mail
    public static int ProductWholesalepriceRequest(ProductEnquiry con)
    {
        try
        {
            System.Net.ServicePointManager.SecurityProtocol =
            SecurityProtocolType.Tls12;
            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["ToMail"]);
            if (ConfigurationManager.AppSettings["CCMail"] != "")
            {
                mail.CC.Add(ConfigurationManager.AppSettings["CCMail"]);
            }
            if (ConfigurationManager.AppSettings["BCCMail"] != "")
            {
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCCMail"]);
            }
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "TamazGlobal Product Quick Enquiry";
            mail.Body = "Hello Admin, <br><br>You have received a Product Quick Enquiry request  from " + con.Name + ".<br><br><u><b><i>Details : </i></b></u><br>Name : " + con.Name + "<br>Phone_Number : " + con.Mobile + "<br>City : " + con.City + "<br>Message : " + con.Message + "<br>Product Name: " + con.ProductName + "<br>Source Page : " + con.SourcePage + "<br><br>Thanks,<br>Team TamazGlobal" + "<br/>" + "https://www.tamazglobal.com/";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                           (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);

            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);

            return 1;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ProductWholesalepriceRequest", ex.Message);
            return 0;
        }
    }
    public static int ProductWholeENQRequest(ProductEnquiry con)
    {
        try
        {
            System.Net.ServicePointManager.SecurityProtocol =
            SecurityProtocolType.Tls12;
            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["ToMail"]);
            if (ConfigurationManager.AppSettings["CCMail"] != "")
            {
                mail.CC.Add(ConfigurationManager.AppSettings["CCMail"]);
            }
            if (ConfigurationManager.AppSettings["BCCMail"] != "")
            {
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCCMail"]);
            }
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "TamazGlobal Product Wholesale Enquiry";
            mail.Body = "Hello Admin, <br><br>You have received a Product Wholesale Enquiry request  from " + con.Name + ".<br><br><u><b><i>Details : </i></b></u><br>Name : " + con.Name + "<br>Phone_Number : " + con.Mobile + "<br>City : " + con.City + "<br>Message : " + con.Message + "<br>Product Name: " + con.ProductName + "<br>Source Page : " + con.SourcePage + "<br><br>Thanks,<br>Team TamazGlobal" + "<br/>" + "https://www.tamazglobal.com/";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                           (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);

            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            smtp.Send(mail);

            return 1;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ProductWholesalepriceRequest", ex.Message);
            return 0;
        }
    }
    public static int ProductRetailUserMail(ProductEnquiry con)
    {
        try
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            MailMessage mail = new MailMessage();
            mail.To.Add(con.Email); // ✅ send to user

            mail.From = new MailAddress(
                ConfigurationManager.AppSettings["from"],
                ConfigurationManager.AppSettings["fromName"]
            );

            mail.Subject = "Your Enquiry Received - TamazGlobal";

            mail.Body = @"Hello " + con.Name + @",<br><br>
Thank you for your enquiry. Our team will get back to you shortly.<br><br>

<b>Details Submitted:</b><br>
Product: " + con.ProductName + @"<br>
Message: " + con.Message + @"<br><br>

You can revisit the product here:<br>
<a href='" + con.SourcePage + @"'>" + con.SourcePage + @"</a><br><br>

Thanks & Regards,<br>
Team TamazGlobal<br>
https://www.tamazglobal.com/";

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["host"],
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]),
                Credentials = new System.Net.NetworkCredential(
                    ConfigurationManager.AppSettings["userName"],
                    ConfigurationManager.AppSettings["password"]
                ),
                EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"])
            };

            smtp.Send(mail);

            return 1;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "ProductRetailUserMail",
                ex.Message
            );
            return 0;
        }
    }
    public static int ProductWholesaleUserMail(ProductEnquiry con)
    {
        try
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            MailMessage mail = new MailMessage();
            mail.To.Add(con.Email); // ✅ send to user

            mail.From = new MailAddress(
                ConfigurationManager.AppSettings["from"],
                ConfigurationManager.AppSettings["fromName"]
            );

            mail.Subject = "Wholesale Enquiry Received - TamazGlobal";

            mail.Body = @"Hello " + con.Name + @",<br><br>
Thank you for your wholesale enquiry. Our team will contact you soon.<br><br>

<b>Details Submitted:</b><br>
Product: " + con.ProductName + @"<br>
Message: " + con.Message + @"<br><br>

Product Link:<br>
<a href='" + con.SourcePage + @"'>" + con.SourcePage + @"</a><br><br>

Thanks & Regards,<br>
Team TamazGlobal<br>
https://www.tamazglobal.com/";

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["host"],
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]),
                Credentials = new System.Net.NetworkCredential(
                    ConfigurationManager.AppSettings["userName"],
                    ConfigurationManager.AppSettings["password"]
                ),
                EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"])
            };

            smtp.Send(mail);

            return 1;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "ProductWholesaleUserMail",
                ex.Message
            );
            return 0;
        }
    }

    //    public static int OrderDispatched(string oid, string productTable, string name, string email, string mobile, string paidAmount, string pType, string delprname, string delprnumber, string link, string address)
    //    {
    //        try
    //        {
    //            string amt = pType.ToLower() == "cod20" ? "Payable:<br />₹" + paidAmount + @"" : " Amount Paid <br /> </b>₹" + paidAmount + @"";
    //            #region mailbody
    //            string mailbody = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
    //<html xmlns='http://www.w3.org/1999/xhtml'>
    //<head>
    //    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    //    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    //    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    //    <meta name='format-detection' content='telephone=no' />
    //    <title>Archidply</title>
    //    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    //    <style type='text/css'>
    //        html {
    //            background-color: #FFF;
    //            margin: 0;
    //            font-family: 'Lato', sans-serif;
    //            padding: 0;
    //        }

    //        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

    //        body, #bodyTable, #bodyCell, #bodyCell {
    //            height: 100% !important;
    //            margin: 0;
    //            padding: 0;
    //            width: 100% !important;
    //        }

    //        table {
    //            border-collapse: collapse;
    //        }

    //            table[id=bodyTable] {
    //                width: 100% !important;
    //                margin: auto;
    //                max-width: 500px !important;
    //                color: #212121;
    //                font-weight: normal;
    //            }

    //        img, a img {
    //            border: 0;
    //            outline: none;
    //            text-decoration: none;
    //            height: auto;
    //            line-height: 100%;
    //        }

    //        /*a {
    //            text-decoration: none !important;
    //            border-bottom: 1px solid;
    //        }*/

    //        h1, h2, h3, h4, h5, h6 {
    //            color: #5F5F5F;
    //            font-weight: normal;
    //            font-size: 20px;
    //            line-height: 125%;
    //            text-align: Left;
    //            letter-spacing: normal;
    //            margin-top: 0;
    //            margin-right: 0;
    //            margin-bottom: 10px;
    //            margin-left: 0;
    //            padding-top: 0;
    //            padding-bottom: 0;
    //            padding-left: 0;
    //            padding-right: 0;
    //        }

    //        .ReadMsgBody {
    //            width: 100%;
    //        }

    //        .ExternalClass {
    //            width: 100%;
    //        }

    //            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
    //                line-height: 100%;
    //            }

    //        table, td {
    //            mso-table-lspace: 0pt;
    //            mso-table-rspace: 0pt;
    //        }

    //        #outlook a {
    //            padding: 0;
    //        }

    //        img {
    //            -ms-interpolation-mode: bicubic;
    //            display: block;
    //            outline: none;
    //            text-decoration: none;
    //        }

    //        body, table, td, p, a, li, blockquote {
    //            -ms-text-size-adjust: 100%;
    //            -webkit-text-size-adjust: 100%;
    //            font-weight: 500 !important;
    //        }

    //        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
    //            padding-top: 10px !important;
    //        }

    //        h1 {
    //            display: block;
    //            font-size: 26px;
    //            font-style: normal;
    //            font-weight: normal;
    //            line-height: 100%;
    //        }

    //        h2 {
    //            display: block;
    //            font-size: 20px;
    //            font-style: normal;
    //            font-weight: normal;
    //            line-height: 120%;
    //        }

    //        h3 {
    //            display: block;
    //            font-size: 17px;
    //            font-style: normal;
    //            font-weight: normal;
    //            line-height: 110%;
    //        }

    //        h4 {
    //            display: block;
    //            font-size: 18px;
    //            font-style: italic;
    //            font-weight: normal;
    //            line-height: 100%;
    //        }

    //        .flexibleImage {
    //            height: auto;
    //        }

    //        .linkRemoveBorder {
    //            border-bottom: 0 !important;
    //        }

    //        table[class=flexibleContainerCellDivider] {
    //            padding-bottom: 0 !important;
    //            padding-top: 0 !important;
    //        }

    //        body, #bodyTable {
    //            background-color: #E1E1E1;
    //        }

    //        #emailHeader {
    //            background-color: #fff;
    //        }

    //        #emailBody {
    //            background-color: #FFFFFF;
    //        }

    //        #emailFooter {
    //            background-color: #E1E1E1;
    //        }

    //        .nestedContainer {
    //            background-color: #F8F8F8;
    //            border: 1px solid #CCCCCC;
    //        }

    //        .emailButton {
    //            background-color: #205478;
    //            border-collapse: separate;
    //        }

    //        .buttonContent {
    //            color: #FFFFFF;
    //            font-family: Helvetica;
    //            font-size: 18px;
    //            font-weight: bold;
    //            line-height: 100%;
    //            padding: 15px;
    //            text-align: center;
    //        }

    //            .buttonContent a {
    //                color: #FFFFFF;
    //                display: block;
    //                text-decoration: none !important;
    //                border: 0 !important;
    //            }

    //        .emailCalendar {
    //            background-color: #FFFFFF;
    //            border: 1px solid #CCCCCC;
    //        }

    //        .emailCalendarMonth {
    //            background-color: #205478;
    //            color: #FFFFFF;
    //            font-size: 16px;
    //            font-weight: bold;
    //            padding-top: 10px;
    //            padding-bottom: 10px;
    //            text-align: center;
    //        }

    //        .emailCalendarDay {
    //            color: #205478;
    //            font-size: 60px;
    //            font-weight: bold;
    //            line-height: 100%;
    //            padding-top: 20px;
    //            padding-bottom: 20px;
    //            text-align: center;
    //        }

    //        .imageContentText {
    //            margin-top: 10px;
    //            line-height: 0;
    //        }

    //            .imageContentText a {
    //                line-height: 0;
    //            }

    //        #invisibleIntroduction {
    //            display: none !important;
    //        }

    //        span[class=ios-color-hack] a {
    //            color: #275100 !important;
    //            text-decoration: none !important;
    //        }

    //        span[class=ios-color-hack2] a {
    //            color: #205478 !important;
    //            text-decoration: none !important;
    //        }

    //        span[class=ios-color-hack3] a {
    //            color: #8B8B8B !important;
    //            text-decoration: none !important;
    //        }

    //        .a[href^='tel'], a[href^='sms'] {
    //            text-decoration: none !important;
    //            color: #606060 !important;
    //            pointer-events: none !important;
    //            cursor: default !important;
    //        }

    //        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
    //            text-decoration: none !important;
    //            color: #606060 !important;
    //            pointer-events: auto !important;
    //            cursor: default !important;
    //        }

    //        @media only screen and (max-width: 480px) {
    //            body {
    //                width: 100% !important;
    //                min-width: 100% !important;
    //            }

    //            table[id='emailHeader'],
    //            table[id='emailBody'],
    //            table[id='emailFooter'],
    //            table[class='flexibleContainer'],
    //            td[class='flexibleContainerCell'] {
    //                width: 100% !important;
    //            }

    //            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
    //                display: block;
    //                width: 100%;
    //                text-align: left;
    //            }

    //            td[class='imageContent'] img {
    //                height: auto !important;
    //                width: 100% !important;
    //                max-width: 100% !important;
    //            }

    //            img[class='flexibleImage'] {
    //                height: auto !important;
    //                width: 100% !important;
    //                max-width: 100% !important;
    //            }

    //            img[class='flexibleImageSmall'] {
    //                height: auto !important;
    //                width: auto !important;
    //            }

    //            table[class='flexibleContainerBoxNext'] {
    //                padding-top: 10px !important;
    //            }

    //            table[class='emailButton'] {
    //                width: 100% !important;
    //            }

    //            td[class='buttonContent'] {
    //                padding: 0 !important;
    //            }

    //                td[class='buttonContent'] a {
    //                    padding: 15px !important;
    //                }
    //        }

    //        @media only screen and (-webkit-device-pixel-ratio:.75) {
    //        }

    //        @media only screen and (-webkit-device-pixel-ratio:1) {
    //        }

    //        @media only screen and (-webkit-device-pixel-ratio:1.5) {
    //        }

    //        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
    //        }

    //        .blink_text {
    //            -webkit-animation-name: blinker;
    //            -webkit-animation-duration: 2s;
    //            -webkit-animation-timing-function: linear;
    //            -webkit-animation-iteration-count: infinite;
    //            -moz-animation-name: blinker;
    //            -moz-animation-duration: 2s;
    //            -moz-animation-timing-function: linear;
    //            -moz-animation-iteration-count: infinite;
    //            animation-name: blinker;
    //            animation-duration: 2s;
    //            animation-timing-function: linear;
    //            animation-iteration-count: infinite;
    //            color: white;
    //        }

    //        @-moz-keyframes blinker {
    //            0% {
    //                opacity: 1.0;
    //            }

    //            50% {
    //                opacity: 0.0;
    //            }

    //            100% {
    //                opacity: 1.0;
    //            }
    //        }

    //        @-webkit-keyframes blinker {
    //            0% {
    //                opacity: 1.0;
    //            }

    //            50% {
    //                opacity: 0.0;
    //            }

    //            100% {
    //                opacity: 1.0;
    //            }
    //        }

    //        @keyframes blinker {
    //            0% {
    //                opacity: 1.0;
    //            }

    //            50% {
    //                opacity: 0.0;
    //            }

    //            100% {
    //                opacity: 1.0;
    //            }
    //        }
    //    </style>
    //</head>
    //<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    //    <center style='background-color:#E1E1E1;'>
    //        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
    //            <tr>
    //                <td align='center' valign='top' id='bodyCell'>
    //                    <!--<table bgcolor='#fff' border='0' cellpadding='0' cellspacing='0' width='600' id='emailHeader' style='margin-top:20px;'>
    //                        <tr>
    //                            <td align='center' valign='top'>
    //                                <table border='0' cellpadding='0' cellspacing='0' width='100%'>
    //                                    <tr>
    //                                        <td align='center' valign='top' style='padding:0px'>
    //                                            <table border='0' cellpadding='10' cellspacing='0' width='500' class='flexibleContainer'>
    //                                                <tr>
    //                                                    <td valign='top' width='500' class='flexibleContainerCell' style='padding:0px;'>
    //                                                        <table align='left' border='0' cellpadding='0' cellspacing='0' width='100%'>
    //                                                            <tr>
    //                                                                <td align='center' valign='top' style='padding:0px 0px;'>
    //                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
    //                                                                        <tr>
    //                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>

    //                                                                            </td>
    //                                                                        </tr>

    //                                                                    </table>
    //                                                                </td>
    //                                                            </tr>
    //                                                        </table>
    //                                                    </td>
    //                                                </tr>
    //                                            </table>
    //                                        </td>
    //                                    </tr>
    //                                </table>
    //                            </td>
    //                        </tr>
    //                    </table>-->
    //                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>


    //                        <tr>
    //                            <td align='center' valign='top' style=''>
    //                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
    //                                    <tr>
    //                                        <td align='center' valign='top'>
    //                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
    //                                                <tr>
    //                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
    //                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>



    //                                                            <tr>
    //                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
    //                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
    //                                                                        <tr>
    //                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
    //                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
    //                                                                            </td>
    //                                                                        </tr>
    //                                                                    </table>
    //                                                                </td>
    //                                                            </tr>

    //                                                        </table>




    //                                                    </td>
    //                                                </tr>


    //                                                <tr>
    //                                                    <td align='left' valign='top' style='padding:0px;background:#fff'>
    //                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
    //                                                            <tr>
    //                                                                <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
    //                                                                    <p style='font-size:22px;line-height:28px!important;text-align:center;color:#573e40;font-weight:bold!important'>
    //                                                                        Your order is on the way! 
    //                                                                    </p>

    //                                                                    <p style='font-size:18px;color:#573e40;line-height:22px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
    //                                                                        Hi " + name + @",
    //                                                                    </p>
    //                                                                    <p style='line-height:22px!important;text-align:left;margin-top:0px;color:#573e40;margin-bottom:30px;padding:0px 50px;'>
    //                                                                       Your order is on the way. Track your shipment to see the delivery status.
    //                                                                       Your tracking details are mentioned below.
    //                                                                    </p>
    //                                                                    <p style='line-height:22px!important;text-align:left;margin-top:0px;color:#573e40;margin-bottom:30px;padding:0px 50px;'>
    //                                                                        Courier Name : " + delprname + @" <br>Tracking Code : " + delprnumber + @" <br>Tracking Link : " + link + @"
    //                                                                    </p>

    //                                                                </td>
    //                                                            </tr>

    //                                                        </table>
    //                                                    </td>
    //                                                </tr>
    //                                                <tr>
    //                                                    <td align='left' valign='top' style='padding:0px 50px 20px 50px;background:#fff'>
    //                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>


    //                                                            <tr>
    //                                                                <td align='left' valign='top' style='height:200px;float:left;width:46%;margin-bottom:10px;margin-top:20px;color:#fff;padding:10px; font-size:18px;margin-right:0.5%' bgcolor='#8cc540' class='flexibleContainerCell'>
    //                                                                    <p style='color:#fff;text-align:left;font-size:16px;margin-bottom:5px;line-height:20px;'>
    //                                                                        Order Number: " + oid + @"
    //                                                                    </p>
    //                                                                    Payment Method
    //                                                                    <p style='color:#fff;text-align:left;font-size:12px;margin-bottom:25px;line-height:20px;'>
    //                                                                        " + pType + @"
    //                                                                    </p>
    //                                                                    <p style='color:#fff;text-align:left;font-size:12px;line-height:20px;margin-bottom:5px;'>
    //                                                                       " + amt + @"
    //                                                                    </p> 
    //                                                                </td>
    //                                                                <td align='left' valign='top' style='height:200px;float:left;width:46%;margin-bottom:10px;margin-top:20px;text-align:left;padding:10px;color:#fff;font-size:18px;margin-left:0.5%;' bgcolor='#8cc540' class='flexibleContainerCell'>
    //                                                                    Shipped To:
    //                                                                    <p style='color:#fff;text-align:left;font-size:12px;line-height:20px;'>
    //                                                                       " + address + @"
    //                                                                    </p>

    //                                                                </td>

    //                                                                <td align='left' valign='top' style='float:left;width:100%' class='flexibleContainerCell'>
    //                                                                    <p style='text-align:left;font-weight:600!important;margin-bottom:20px !important;font-size:16px;'>Stuff You Picked:</p>

    //                                                                </td>
    //                                                            </tr>
    //                                                            " + productTable + @"

    //                                                        </table>
    //                                                    </td>
    //                                                </tr>
    //                                                <tr>
    //                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
    //                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
    //                                                  <tr>
    //                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
    //                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
    //                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
    //                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/facebook.png' /></a>
    //                                                                                        </li>
    //                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
    //                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/instagram.png' /></a>
    //                                                                                        </li>
    //                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
    //                                                                                            <a href='https://twitter.com/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/twitter.png' /></a>
    //                                                                                        </li>
    //                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
    //                                                                                            <a href='https://www.youtube.com/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/youtube.png' /></a>
    //                                                                                        </li>
    //                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
    //                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/mail.png' /></a>
    //                                                                                        </li>

    //                                                                                    </ul>
    //                                                                                </td>

    //                                                                            </tr> 
    //                                            </table>
    //                                        </td>
    //                                    </tr> 
    //                                            </table>
    //                                        </td>
    //                                    </tr>
    //                                </table>
    //                            </td>
    //                        </tr>
    //                    </table>
    //                </td>
    //            </tr>
    //        </table>

    //    </center>
    //</body>
    //</html>";
    //            #endregion
    //            MailMessage mail = new MailMessage();
    //            mail.To.Add(email);
    //            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
    //            mail.Subject = "Tracking Information for Order#" + oid + "";
    //            mail.Body = mailbody;
    //            mail.IsBodyHtml = true;
    //            SmtpClient smtp = new SmtpClient();
    //            smtp.Host = ConfigurationManager.AppSettings["host"];
    //            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
    //            smtp.Credentials = new System.Net.NetworkCredential
    //                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
    //            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
    //            smtp.Send(mail);
    //            return 0;
    //        }
    //        catch (Exception exx)
    //        {
    //            return 0;
    //        }
    //    }
    //    public static int OrderDelivered(string oid, string productTable, string name, string email, string mobile, string pType, string address, string paidAmount)
    //    {
    //        try
    //        {
    //            #region mailbody
    //            string mailbody = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
    //<html xmlns='http://www.w3.org/1999/xhtml'>
    //<head>
    //    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    //    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    //    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    //    <meta name='format-detection' content='telephone=no' />
    //    <title>Archidply</title>
    //    <link rel='shortcut icon' href='" + ConfigurationManager.AppSettings["domain"] + @"/emailtemplate/focuslay/logo.png' />
    //    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    //    <style type='text/css'>
    //        html {
    //            background-color: #FFF;
    //            margin: 0;
    //            font-family: 'Lato', sans-serif;
    //            padding: 0;
    //        }

    //        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

    //        body, #bodyTable, #bodyCell, #bodyCell {
    //            height: 100% !important;
    //            margin: 0;
    //            padding: 0;
    //            width: 100% !important;
    //        }

    //        table {
    //            border-collapse: collapse;
    //        }

    //            table[id=bodyTable] {
    //                width: 100% !important;
    //                margin: auto;
    //                max-width: 500px !important;
    //                color: #212121;
    //                font-weight: normal;
    //            }

    //        img, a img {
    //            border: 0;
    //            outline: none;
    //            text-decoration: none;
    //            height: auto;
    //            line-height: 100%;
    //        }

    //        /*a {
    //            text-decoration: none !important;
    //            border-bottom: 1px solid;
    //        }*/

    //        h1, h2, h3, h4, h5, h6 {
    //            color: #5F5F5F;
    //            font-weight: normal;
    //            font-size: 20px;
    //            line-height: 125%;
    //            text-align: Left;
    //            letter-spacing: normal;
    //            margin-top: 0;
    //            margin-right: 0;
    //            margin-bottom: 10px;
    //            margin-left: 0;
    //            padding-top: 0;
    //            padding-bottom: 0;
    //            padding-left: 0;
    //            padding-right: 0;
    //        }

    //        .ReadMsgBody {
    //            width: 100%;
    //        }

    //        .ExternalClass {
    //            width: 100%;
    //        }

    //            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
    //                line-height: 100%;
    //            }

    //        table, td {
    //            mso-table-lspace: 0pt;
    //            mso-table-rspace: 0pt;
    //        }

    //        #outlook a {
    //            padding: 0;
    //        }

    //        img {
    //            -ms-interpolation-mode: bicubic;
    //            display: block;
    //            outline: none;
    //            text-decoration: none;
    //        }

    //        body, table, td, p, a, li, blockquote {
    //            -ms-text-size-adjust: 100%;
    //            -webkit-text-size-adjust: 100%;
    //            font-weight: 500 !important;
    //        }

    //        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
    //            padding-top: 10px !important;
    //        }

    //        h1 {
    //            display: block;
    //            font-size: 26px;
    //            font-style: normal;
    //            font-weight: normal;
    //            line-height: 100%;
    //        }

    //        h2 {
    //            display: block;
    //            font-size: 20px;
    //            font-style: normal;
    //            font-weight: normal;
    //            line-height: 120%;
    //        }

    //        h3 {
    //            display: block;
    //            font-size: 17px;
    //            font-style: normal;
    //            font-weight: normal;
    //            line-height: 110%;
    //        }

    //        h4 {
    //            display: block;
    //            font-size: 18px;
    //            font-style: italic;
    //            font-weight: normal;
    //            line-height: 100%;
    //        }

    //        .flexibleImage {
    //            height: auto;
    //        }

    //        .linkRemoveBorder {
    //            border-bottom: 0 !important;
    //        }

    //        table[class=flexibleContainerCellDivider] {
    //            padding-bottom: 0 !important;
    //            padding-top: 0 !important;
    //        }

    //        body, #bodyTable {
    //            background-color: #E1E1E1;
    //        }

    //        #emailHeader {
    //            background-color: #fff;
    //        }

    //        #emailBody {
    //            background-color: #FFFFFF;
    //        }

    //        #emailFooter {
    //            background-color: #E1E1E1;
    //        }

    //        .nestedContainer {
    //            background-color: #F8F8F8;
    //            border: 1px solid #CCCCCC;
    //        }

    //        .emailButton {
    //            background-color: #205478;
    //            border-collapse: separate;
    //        }

    //        .buttonContent {
    //            color: #FFFFFF;
    //            font-family: Helvetica;
    //            font-size: 18px;
    //            font-weight: bold;
    //            line-height: 100%;
    //            padding: 15px;
    //            text-align: center;
    //        }

    //            .buttonContent a {
    //                color: #FFFFFF;
    //                display: block;
    //                text-decoration: none !important;
    //                border: 0 !important;
    //            }

    //        .emailCalendar {
    //            background-color: #FFFFFF;
    //            border: 1px solid #CCCCCC;
    //        }

    //        .emailCalendarMonth {
    //            background-color: #205478;
    //            color: #FFFFFF;
    //            font-size: 16px;
    //            font-weight: bold;
    //            padding-top: 10px;
    //            padding-bottom: 10px;
    //            text-align: center;
    //        }

    //        .emailCalendarDay {
    //            color: #205478;
    //            font-size: 60px;
    //            font-weight: bold;
    //            line-height: 100%;
    //            padding-top: 20px;
    //            padding-bottom: 20px;
    //            text-align: center;
    //        }

    //        .imageContentText {
    //            margin-top: 10px;
    //            line-height: 0;
    //        }

    //            .imageContentText a {
    //                line-height: 0;
    //            }

    //        #invisibleIntroduction {
    //            display: none !important;
    //        }

    //        span[class=ios-color-hack] a {
    //            color: #275100 !important;
    //            text-decoration: none !important;
    //        }

    //        span[class=ios-color-hack2] a {
    //            color: #205478 !important;
    //            text-decoration: none !important;
    //        }

    //        span[class=ios-color-hack3] a {
    //            color: #8B8B8B !important;
    //            text-decoration: none !important;
    //        }

    //        .a[href^='tel'], a[href^='sms'] {
    //            text-decoration: none !important;
    //            color: #606060 !important;
    //            pointer-events: none !important;
    //            cursor: default !important;
    //        }

    //        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
    //            text-decoration: none !important;
    //            color: #606060 !important;
    //            pointer-events: auto !important;
    //            cursor: default !important;
    //        }

    //        @media only screen and (max-width: 480px) {
    //            body {
    //                width: 100% !important;
    //                min-width: 100% !important;
    //            }

    //            table[id='emailHeader'],
    //            table[id='emailBody'],
    //            table[id='emailFooter'],
    //            table[class='flexibleContainer'],
    //            td[class='flexibleContainerCell'] {
    //                width: 100% !important;
    //            }

    //            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
    //                display: block;
    //                width: 100%;
    //                text-align: left;
    //            }

    //            td[class='imageContent'] img {
    //                height: auto !important;
    //                width: 100% !important;
    //                max-width: 100% !important;
    //            }

    //            img[class='flexibleImage'] {
    //                height: auto !important;
    //                width: 100% !important;
    //                max-width: 100% !important;
    //            }

    //            img[class='flexibleImageSmall'] {
    //                height: auto !important;
    //                width: auto !important;
    //            }

    //            table[class='flexibleContainerBoxNext'] {
    //                padding-top: 10px !important;
    //            }

    //            table[class='emailButton'] {
    //                width: 100% !important;
    //            }

    //            td[class='buttonContent'] {
    //                padding: 0 !important;
    //            }

    //                td[class='buttonContent'] a {
    //                    padding: 15px !important;
    //                }
    //        }

    //        @media only screen and (-webkit-device-pixel-ratio:.75) {
    //        }

    //        @media only screen and (-webkit-device-pixel-ratio:1) {
    //        }

    //        @media only screen and (-webkit-device-pixel-ratio:1.5) {
    //        }

    //        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
    //        }

    //        .blink_text {
    //            -webkit-animation-name: blinker;
    //            -webkit-animation-duration: 2s;
    //            -webkit-animation-timing-function: linear;
    //            -webkit-animation-iteration-count: infinite;
    //            -moz-animation-name: blinker;
    //            -moz-animation-duration: 2s;
    //            -moz-animation-timing-function: linear;
    //            -moz-animation-iteration-count: infinite;
    //            animation-name: blinker;
    //            animation-duration: 2s;
    //            animation-timing-function: linear;
    //            animation-iteration-count: infinite;
    //            color: white;
    //        }

    //        @-moz-keyframes blinker {
    //            0% {
    //                opacity: 1.0;
    //            }

    //            50% {
    //                opacity: 0.0;
    //            }

    //            100% {
    //                opacity: 1.0;
    //            }
    //        }

    //        @-webkit-keyframes blinker {
    //            0% {
    //                opacity: 1.0;
    //            }

    //            50% {
    //                opacity: 0.0;
    //            }

    //            100% {
    //                opacity: 1.0;
    //            }
    //        }

    //        @keyframes blinker {
    //            0% {
    //                opacity: 1.0;
    //            }

    //            50% {
    //                opacity: 0.0;
    //            }

    //            100% {
    //                opacity: 1.0;
    //            }
    //        }
    //    </style>
    //</head>
    //<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    //    <center style='background-color:#E1E1E1;'>
    //        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
    //            <tr>
    //                <td align='center' valign='top' id='bodyCell'>
    //                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>

    //                        <tr>
    //                            <td align='center' valign='top' style=''>
    //                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
    //                                    <tr>
    //                                        <td align='center' valign='top'>
    //                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
    //                                                <tr>
    //                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
    //                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>

    //                                                            <tr>
    //                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
    //                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
    //                                                                        <tr>
    //                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
    //                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
    //                                                                            </td>
    //                                                                        </tr>
    //                                                                    </table>
    //                                                                </td>
    //                                                            </tr>

    //                                                        </table>

    //                                                    </td>
    //                                                </tr>

    //                                                <tr>
    //                                                    <td align='left' valign='top' style='padding:0px;background:#fff'>
    //                                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
    //                                                            <tr>
    //                                                                <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
    //                                                                    <p style='font-size:22px;line-height:28px!important;text-align:center;color:#573e40;font-weight:bold!important'>
    //                                                                        Your Order has been delivered! 
    //                                                                    </p>

    //                                                                    <p style='font-size:18px;color:#573e40;line-height:22px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
    //                                                                        Hi " + name + @",
    //                                                                    </p>
    //                                                                    <p style='font-size:13px;line-height:23px!important;text-align:left;margin-top:0px;color:#573e40;margin-bottom:30px;padding:0px 50px;'>
    //                                                                        Greetings from Archidply, your order " + oid + @" is delivered successfully on " + CommonModel.UTCTime().ToString("dd-MMM-yyyy") + @", thank you for shopping with us.
    //                                                                    </p>
    //                                                                   <p style='font-size:13px;line-height:23px!important;text-align:left;margin-top:0px;color:#573e40;margin-bottom:30px;padding:0px 50px;'>Your feedback is important for us, Kindly write to feedback " + ConfigurationManager.AppSettings["from"] + @"<br><br>Team Archidply<br>" + ConfigurationManager.AppSettings["domain"] + @"</p>
    //                                                                </td>
    //                                                            </tr>

    //                                                        </table>
    //                                                    </td>
    //                                                </tr>

    //                                                 <tr>
    //                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
    //                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
    //                                                  <tr>
    //                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
    //                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
    //                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
    //                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/facebook.png' /></a>
    //                                                                                        </li>
    //                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
    //                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/instagram.png' /></a>
    //                                                                                        </li>
    //                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
    //                                                                                            <a href='https://twitter.com/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/twitter.png' /></a>
    //                                                                                        </li>
    //                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
    //                                                                                            <a href='https://www.youtube.com/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/youtube.png' /></a>
    //                                                                                        </li>
    //                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
    //                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/images_/mail.png' /></a>
    //                                                                                        </li>

    //                                                                                    </ul>
    //                                                                                </td>

    //                                                                            </tr> 
    //                                            </table>
    //                                        </td>
    //                                    </tr> 
    //                                            </table>
    //                                        </td>
    //                                    </tr>
    //                                </table>
    //                            </td>
    //                        </tr>
    //                    </table>
    //                </td>
    //            </tr>
    //        </table>

    //    </center>
    //</body>
    //</html>";
    //            #endregion
    //            MailMessage mail = new MailMessage();
    //            mail.To.Add(email);
    //            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
    //            mail.Subject = "Archidply - " + oid + " Delivered";
    //            mail.Body = mailbody;
    //            mail.IsBodyHtml = true;
    //            SmtpClient smtp = new SmtpClient();
    //            smtp.Host = ConfigurationManager.AppSettings["host"];
    //            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
    //            smtp.Credentials = new System.Net.NetworkCredential
    //                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);

    //            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
    //            smtp.Send(mail);
    //            return 0;
    //        }
    //        catch (Exception exx)
    //        {
    //            return 0;
    //        }
    //    }
    //    public static int CancellationMail(string oid, string productTable, string name, string email, string mobile, string pType, string address, string orderedOn)
    //    {
    //        try
    //        {
    //            #region mailbody
    //            string mailbody = @" <!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
    //<html xmlns='http://www.w3.org/1999/xhtml'>
    //<head>
    //    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    //    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    //    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    //    <meta name='format-detection' content='telephone=no' />
    //    <title>Archidply</title>
    //    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    //    <style type='text/css'>
    //        html {
    //            background-color: #FFF;
    //            margin: 0;
    //            font-family: 'Lato', sans-serif;
    //            padding: 0;
    //        }

    //        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

    //        body, #bodyTable, #bodyCell, #bodyCell {
    //            height: 100% !important;
    //            margin: 0;
    //            padding: 0;
    //            width: 100% !important;
    //        }

    //        table {
    //            border-collapse: collapse;
    //        }

    //            table[id=bodyTable] {
    //                width: 100% !important;
    //                margin: auto;
    //                max-width: 500px !important;
    //                color: #212121;
    //                font-weight: normal;
    //            }

    //        img, a img {
    //            border: 0;
    //            outline: none;
    //            text-decoration: none;
    //            height: auto;
    //            line-height: 100%;
    //        }

    //        /*a {
    //            text-decoration: none !important;
    //            border-bottom: 1px solid;
    //        }*/

    //        h1, h2, h3, h4, h5, h6 {
    //            color: #5F5F5F;
    //            font-weight: normal;
    //            font-size: 20px;
    //            line-height: 125%;
    //            text-align: Left;
    //            letter-spacing: normal;
    //            margin-top: 0;
    //            margin-right: 0;
    //            margin-bottom: 10px;
    //            margin-left: 0;
    //            padding-top: 0;
    //            padding-bottom: 0;
    //            padding-left: 0;
    //            padding-right: 0;
    //        }

    //        .ReadMsgBody {
    //            width: 100%;
    //        }

    //        .ExternalClass {
    //            width: 100%;
    //        }

    //            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
    //                line-height: 100%;
    //            }

    //        table, td {
    //            mso-table-lspace: 0pt;
    //            mso-table-rspace: 0pt;
    //        }

    //        #outlook a {
    //            padding: 0;
    //        }

    //        img {
    //            -ms-interpolation-mode: bicubic;
    //            display: block;
    //            outline: none;
    //            text-decoration: none;
    //        }

    //        body, table, td, p, a, li, blockquote {
    //            -ms-text-size-adjust: 100%;
    //            -webkit-text-size-adjust: 100%;
    //            font-weight: 500 !important;
    //        }

    //        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
    //            padding-top: 10px !important;
    //        }

    //        h1 {
    //            display: block;
    //            font-size: 26px;
    //            font-style: normal;
    //            font-weight: normal;
    //            line-height: 100%;
    //        }

    //        h2 {
    //            display: block;
    //            font-size: 20px;
    //            font-style: normal;
    //            font-weight: normal;
    //            line-height: 120%;
    //        }

    //        h3 {
    //            display: block;
    //            font-size: 17px;
    //            font-style: normal;
    //            font-weight: normal;
    //            line-height: 110%;
    //        }

    //        h4 {
    //            display: block;
    //            font-size: 18px;
    //            font-style: italic;
    //            font-weight: normal;
    //            line-height: 100%;
    //        }

    //        .flexibleImage {
    //            height: auto;
    //        }

    //        .linkRemoveBorder {
    //            border-bottom: 0 !important;
    //        }

    //        table[class=flexibleContainerCellDivider] {
    //            padding-bottom: 0 !important;
    //            padding-top: 0 !important;
    //        }

    //        body, #bodyTable {
    //            background-color: #E1E1E1;
    //        }

    //        #emailHeader {
    //            background-color: #fff;
    //        }

    //        #emailBody {
    //            background-color: #FFFFFF;
    //        }

    //        #emailFooter {
    //            background-color: #E1E1E1;
    //        }

    //        .nestedContainer {
    //            background-color: #F8F8F8;
    //            border: 1px solid #CCCCCC;
    //        }

    //        .emailButton {
    //            background-color: #205478;
    //            border-collapse: separate;
    //        }

    //        .buttonContent {
    //            color: #FFFFFF;
    //            font-family: Helvetica;
    //            font-size: 18px;
    //            font-weight: bold;
    //            line-height: 100%;
    //            padding: 15px;
    //            text-align: center;
    //        }

    //            .buttonContent a {
    //                color: #FFFFFF;
    //                display: block;
    //                text-decoration: none !important;
    //                border: 0 !important;
    //            }

    //        .emailCalendar {
    //            background-color: #FFFFFF;
    //            border: 1px solid #CCCCCC;
    //        }

    //        .emailCalendarMonth {
    //            background-color: #205478;
    //            color: #FFFFFF;
    //            font-size: 16px;
    //            font-weight: bold;
    //            padding-top: 10px;
    //            padding-bottom: 10px;
    //            text-align: center;
    //        }

    //        .emailCalendarDay {
    //            color: #205478;
    //            font-size: 60px;
    //            font-weight: bold;
    //            line-height: 100%;
    //            padding-top: 20px;
    //            padding-bottom: 20px;
    //            text-align: center;
    //        }

    //        .imageContentText {
    //            margin-top: 10px;
    //            line-height: 0;
    //        }

    //            .imageContentText a {
    //                line-height: 0;
    //            }

    //        #invisibleIntroduction {
    //            display: none !important;
    //        }

    //        span[class=ios-color-hack] a {
    //            color: #275100 !important;
    //            text-decoration: none !important;
    //        }

    //        span[class=ios-color-hack2] a {
    //            color: #205478 !important;
    //            text-decoration: none !important;
    //        }

    //        span[class=ios-color-hack3] a {
    //            color: #8B8B8B !important;
    //            text-decoration: none !important;
    //        }

    //        .a[href^='tel'], a[href^='sms'] {
    //            text-decoration: none !important;
    //            color: #606060 !important;
    //            pointer-events: none !important;
    //            cursor: default !important;
    //        }

    //        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
    //            text-decoration: none !important;
    //            color: #606060 !important;
    //            pointer-events: auto !important;
    //            cursor: default !important;
    //        }

    //        @media only screen and (max-width: 480px) {
    //            body {
    //                width: 100% !important;
    //                min-width: 100% !important;
    //            }

    //            table[id='emailHeader'],
    //            table[id='emailBody'],
    //            table[id='emailFooter'],
    //            table[class='flexibleContainer'],
    //            td[class='flexibleContainerCell'] {
    //                width: 100% !important;
    //            }

    //            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
    //                display: block;
    //                width: 100%;
    //                text-align: left;
    //            }

    //            td[class='imageContent'] img {
    //                height: auto !important;
    //                width: 100% !important;
    //                max-width: 100% !important;
    //            }

    //            img[class='flexibleImage'] {
    //                height: auto !important;
    //                width: 100% !important;
    //                max-width: 100% !important;
    //            }

    //            img[class='flexibleImageSmall'] {
    //                height: auto !important;
    //                width: auto !important;
    //            }

    //            table[class='flexibleContainerBoxNext'] {
    //                padding-top: 10px !important;
    //            }

    //            table[class='emailButton'] {
    //                width: 100% !important;
    //            }

    //            td[class='buttonContent'] {
    //                padding: 0 !important;
    //            }

    //                td[class='buttonContent'] a {
    //                    padding: 15px !important;
    //                }
    //        }

    //        @media only screen and (-webkit-device-pixel-ratio:.75) {
    //        }

    //        @media only screen and (-webkit-device-pixel-ratio:1) {
    //        }

    //        @media only screen and (-webkit-device-pixel-ratio:1.5) {
    //        }

    //        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
    //        }

    //        .blink_text {
    //            -webkit-animation-name: blinker;
    //            -webkit-animation-duration: 2s;
    //            -webkit-animation-timing-function: linear;
    //            -webkit-animation-iteration-count: infinite;
    //            -moz-animation-name: blinker;
    //            -moz-animation-duration: 2s;
    //            -moz-animation-timing-function: linear;
    //            -moz-animation-iteration-count: infinite;
    //            animation-name: blinker;
    //            animation-duration: 2s;
    //            animation-timing-function: linear;
    //            animation-iteration-count: infinite;
    //            color: white;
    //        }

    //        @-moz-keyframes blinker {
    //            0% {
    //                opacity: 1.0;
    //            }

    //            50% {
    //                opacity: 0.0;
    //            }

    //            100% {
    //                opacity: 1.0;
    //            }
    //        }

    //        @-webkit-keyframes blinker {
    //            0% {
    //                opacity: 1.0;
    //            }

    //            50% {
    //                opacity: 0.0;
    //            }

    //            100% {
    //                opacity: 1.0;
    //            }
    //        }

    //        @keyframes blinker {
    //            0% {
    //                opacity: 1.0;
    //            }

    //            50% {
    //                opacity: 0.0;
    //            }

    //            100% {
    //                opacity: 1.0;
    //            }
    //        }
    //    </style>
    //</head>
    //<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    //    <center style='background-color:#E1E1E1;'>
    //        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
    //            <tr>
    //                <td align='center' valign='top' id='bodyCell'>

    //                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>


    //                        <tr>
    //                            <td align='center' valign='top' style=''>
    //                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
    //                                    <tr>
    //                                        <td align='center' valign='top'>
    //                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
    //                                                <tr>
    //                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
    //                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>



    //                                                            <tr>
    //                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
    //                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
    //                                                                        <tr>
    //                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
    //                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
    //                                                                            </td>
    //                                                                        </tr>
    //                                                                    </table>
    //                                                                </td>
    //                                                            </tr>


    //                                                            <tr>
    //                                                                <td align='left' valign='top' style='padding:0px;'>
    //                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
    //                                                                        <tr>
    //                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>

    //                                                                                <p style='font-size:22px;line-height:28px!important;text-align:center;color:#111;font-weight:bold!important'>
    //                                                                                    Order cancelled!
    //                                                                                </p>

    //                                                                                <p style='font-size:16px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
    //                                                                                    Hi " + name + @"
    //                                                                                </p>
    //                                                                                <p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
    //                                                                                    Greetings from Archidply, we have received your request for order cancellation, please note we are checking the best possibility as per cancellation terms & will intimate you.                                                                                 </p>
    //                                                                                <p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:center;padding:0px 50px;'>
    //                                                                                    Thank you for shopping with us.<br>Your feedback is important for us, Kindly write to feedback " + ConfigurationManager.AppSettings["from"] + @"
    //                                                                                </p>


    //                                                                            </td>


    //                                                                        </tr>

    //                                                                    </table>
    //                                                                </td>
    //                                                            </tr>  
    //                                                             <tr>
    //                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
    //                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
    //                                                  <tr>
    //                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
    //                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
    //                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
    //                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/facebook.png' /></a>
    //                                                                                        </li>
    //                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
    //                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/instagram.png' /></a>
    //                                                                                        </li>
    //                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
    //                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/mail.png' /></a>
    //                                                                                        </li>

    //                                                                                    </ul>
    //                                                                                </td>

    //                                                                            </tr> 
    //                                            </table>
    //                                        </td>
    //                                    </tr> 
    //                                            </table>
    //                                        </td>
    //                                    </tr>
    //                                </table>
    //                            </td>
    //                        </tr>
    //                    </table>
    //                </td>
    //            </tr>
    //        </table>

    //    </center>
    //</body>
    //</html>";
    //            #endregion
    //            MailMessage mail = new MailMessage();
    //            mail.To.Add(email);
    //            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
    //            mail.Subject = "Your " + ConfigurationManager.AppSettings["domain"] + @" order has been cancelled";
    //            mail.Body = mailbody;
    //            mail.IsBodyHtml = true;
    //            SmtpClient smtp = new SmtpClient();
    //            smtp.Host = ConfigurationManager.AppSettings["host"];
    //            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
    //            smtp.Credentials = new System.Net.NetworkCredential
    //                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
    //            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
    //            smtp.Send(mail);
    //            return 0;
    //        }
    //        catch (Exception exx)
    //        {
    //            return 0;
    //        }
    //    }
    //    public static int ReturnMail(string oid, string productTable, string name, string email, string mobile, string pType, string address, string orderedOn)
    //    {
    //        try
    //        {
    //            #region mailbody
    //            string mailbody = @" <!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
    //<html xmlns='http://www.w3.org/1999/xhtml'>
    //<head>
    //    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    //    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    //    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    //    <meta name='format-detection' content='telephone=no' />
    //    <title>Archidply</title>
    //    <link href='https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i&display=swap' rel='stylesheet' />
    //    <style type='text/css'>
    //        html {
    //            background-color: #FFF;
    //            margin: 0;
    //            font-family: 'Lato', sans-serif;
    //            padding: 0;
    //        }

    //        /*@import url('https://fonts.googleapis.com/css?family=Roboto');*/

    //        body, #bodyTable, #bodyCell, #bodyCell {
    //            height: 100% !important;
    //            margin: 0;
    //            padding: 0;
    //            width: 100% !important;
    //        }

    //        table {
    //            border-collapse: collapse;
    //        }

    //            table[id=bodyTable] {
    //                width: 100% !important;
    //                margin: auto;
    //                max-width: 500px !important;
    //                color: #212121;
    //                font-weight: normal;
    //            }

    //        img, a img {
    //            border: 0;
    //            outline: none;
    //            text-decoration: none;
    //            height: auto;
    //            line-height: 100%;
    //        }

    //        /*a {
    //            text-decoration: none !important;
    //            border-bottom: 1px solid;
    //        }*/

    //        h1, h2, h3, h4, h5, h6 {
    //            color: #5F5F5F;
    //            font-weight: normal;
    //            font-size: 20px;
    //            line-height: 125%;
    //            text-align: Left;
    //            letter-spacing: normal;
    //            margin-top: 0;
    //            margin-right: 0;
    //            margin-bottom: 10px;
    //            margin-left: 0;
    //            padding-top: 0;
    //            padding-bottom: 0;
    //            padding-left: 0;
    //            padding-right: 0;
    //        }

    //        .ReadMsgBody {
    //            width: 100%;
    //        }

    //        .ExternalClass {
    //            width: 100%;
    //        }

    //            .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {
    //                line-height: 100%;
    //            }

    //        table, td {
    //            mso-table-lspace: 0pt;
    //            mso-table-rspace: 0pt;
    //        }

    //        #outlook a {
    //            padding: 0;
    //        }

    //        img {
    //            -ms-interpolation-mode: bicubic;
    //            display: block;
    //            outline: none;
    //            text-decoration: none;
    //        }

    //        body, table, td, p, a, li, blockquote {
    //            -ms-text-size-adjust: 100%;
    //            -webkit-text-size-adjust: 100%;
    //            font-weight: 500 !important;
    //        }

    //        .ExternalClass td[class='ecxflexibleContainerBox'] h3 {
    //            padding-top: 10px !important;
    //        }

    //        h1 {
    //            display: block;
    //            font-size: 26px;
    //            font-style: normal;
    //            font-weight: normal;
    //            line-height: 100%;
    //        }

    //        h2 {
    //            display: block;
    //            font-size: 20px;
    //            font-style: normal;
    //            font-weight: normal;
    //            line-height: 120%;
    //        }

    //        h3 {
    //            display: block;
    //            font-size: 17px;
    //            font-style: normal;
    //            font-weight: normal;
    //            line-height: 110%;
    //        }

    //        h4 {
    //            display: block;
    //            font-size: 18px;
    //            font-style: italic;
    //            font-weight: normal;
    //            line-height: 100%;
    //        }

    //        .flexibleImage {
    //            height: auto;
    //        }

    //        .linkRemoveBorder {
    //            border-bottom: 0 !important;
    //        }

    //        table[class=flexibleContainerCellDivider] {
    //            padding-bottom: 0 !important;
    //            padding-top: 0 !important;
    //        }

    //        body, #bodyTable {
    //            background-color: #E1E1E1;
    //        }

    //        #emailHeader {
    //            background-color: #fff;
    //        }

    //        #emailBody {
    //            background-color: #FFFFFF;
    //        }

    //        #emailFooter {
    //            background-color: #E1E1E1;
    //        }

    //        .nestedContainer {
    //            background-color: #F8F8F8;
    //            border: 1px solid #CCCCCC;
    //        }

    //        .emailButton {
    //            background-color: #205478;
    //            border-collapse: separate;
    //        }

    //        .buttonContent {
    //            color: #FFFFFF;
    //            font-family: Helvetica;
    //            font-size: 18px;
    //            font-weight: bold;
    //            line-height: 100%;
    //            padding: 15px;
    //            text-align: center;
    //        }

    //            .buttonContent a {
    //                color: #FFFFFF;
    //                display: block;
    //                text-decoration: none !important;
    //                border: 0 !important;
    //            }

    //        .emailCalendar {
    //            background-color: #FFFFFF;
    //            border: 1px solid #CCCCCC;
    //        }

    //        .emailCalendarMonth {
    //            background-color: #205478;
    //            color: #FFFFFF;
    //            font-size: 16px;
    //            font-weight: bold;
    //            padding-top: 10px;
    //            padding-bottom: 10px;
    //            text-align: center;
    //        }

    //        .emailCalendarDay {
    //            color: #205478;
    //            font-size: 60px;
    //            font-weight: bold;
    //            line-height: 100%;
    //            padding-top: 20px;
    //            padding-bottom: 20px;
    //            text-align: center;
    //        }

    //        .imageContentText {
    //            margin-top: 10px;
    //            line-height: 0;
    //        }

    //            .imageContentText a {
    //                line-height: 0;
    //            }

    //        #invisibleIntroduction {
    //            display: none !important;
    //        }

    //        span[class=ios-color-hack] a {
    //            color: #275100 !important;
    //            text-decoration: none !important;
    //        }

    //        span[class=ios-color-hack2] a {
    //            color: #205478 !important;
    //            text-decoration: none !important;
    //        }

    //        span[class=ios-color-hack3] a {
    //            color: #8B8B8B !important;
    //            text-decoration: none !important;
    //        }

    //        .a[href^='tel'], a[href^='sms'] {
    //            text-decoration: none !important;
    //            color: #606060 !important;
    //            pointer-events: none !important;
    //            cursor: default !important;
    //        }

    //        .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {
    //            text-decoration: none !important;
    //            color: #606060 !important;
    //            pointer-events: auto !important;
    //            cursor: default !important;
    //        }

    //        @media only screen and (max-width: 480px) {
    //            body {
    //                width: 100% !important;
    //                min-width: 100% !important;
    //            }

    //            table[id='emailHeader'],
    //            table[id='emailBody'],
    //            table[id='emailFooter'],
    //            table[class='flexibleContainer'],
    //            td[class='flexibleContainerCell'] {
    //                width: 100% !important;
    //            }

    //            td[class='flexibleContainerBox'], td[class='flexibleContainerBox'] table {
    //                display: block;
    //                width: 100%;
    //                text-align: left;
    //            }

    //            td[class='imageContent'] img {
    //                height: auto !important;
    //                width: 100% !important;
    //                max-width: 100% !important;
    //            }

    //            img[class='flexibleImage'] {
    //                height: auto !important;
    //                width: 100% !important;
    //                max-width: 100% !important;
    //            }

    //            img[class='flexibleImageSmall'] {
    //                height: auto !important;
    //                width: auto !important;
    //            }

    //            table[class='flexibleContainerBoxNext'] {
    //                padding-top: 10px !important;
    //            }

    //            table[class='emailButton'] {
    //                width: 100% !important;
    //            }

    //            td[class='buttonContent'] {
    //                padding: 0 !important;
    //            }

    //                td[class='buttonContent'] a {
    //                    padding: 15px !important;
    //                }
    //        }

    //        @media only screen and (-webkit-device-pixel-ratio:.75) {
    //        }

    //        @media only screen and (-webkit-device-pixel-ratio:1) {
    //        }

    //        @media only screen and (-webkit-device-pixel-ratio:1.5) {
    //        }

    //        @media only screen and (min-device-width : 320px) and (max-device-width:568px) {
    //        }

    //        .blink_text {
    //            -webkit-animation-name: blinker;
    //            -webkit-animation-duration: 2s;
    //            -webkit-animation-timing-function: linear;
    //            -webkit-animation-iteration-count: infinite;
    //            -moz-animation-name: blinker;
    //            -moz-animation-duration: 2s;
    //            -moz-animation-timing-function: linear;
    //            -moz-animation-iteration-count: infinite;
    //            animation-name: blinker;
    //            animation-duration: 2s;
    //            animation-timing-function: linear;
    //            animation-iteration-count: infinite;
    //            color: white;
    //        }

    //        @-moz-keyframes blinker {
    //            0% {
    //                opacity: 1.0;
    //            }

    //            50% {
    //                opacity: 0.0;
    //            }

    //            100% {
    //                opacity: 1.0;
    //            }
    //        }

    //        @-webkit-keyframes blinker {
    //            0% {
    //                opacity: 1.0;
    //            }

    //            50% {
    //                opacity: 0.0;
    //            }

    //            100% {
    //                opacity: 1.0;
    //            }
    //        }

    //        @keyframes blinker {
    //            0% {
    //                opacity: 1.0;
    //            }

    //            50% {
    //                opacity: 0.0;
    //            }

    //            100% {
    //                opacity: 1.0;
    //            }
    //        }
    //    </style>
    //</head>
    //<body bgcolor='#E1E1E1' leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    //    <center style='background-color:#E1E1E1;'>
    //        <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' style=' table-layout: fixed; max-width:100% !important;width: 100% !important;min-width: 100% !important;'>
    //            <tr>
    //                <td align='center' valign='top' id='bodyCell'>

    //                    <table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='700' id='emailBody' style='margin-top:20px;margin-bottom:20px;'>


    //                        <tr>
    //                            <td align='center' valign='top' style=''>
    //                                <table border='0' cellpadding='0' cellspacing='0' width='100%' bgcolor='#fff'>
    //                                    <tr>
    //                                        <td align='center' valign='top'>
    //                                            <table border='0' cellpadding='0' cellspacing='0' width='700' class='flexibleContainer'>
    //                                                <tr>
    //                                                    <td align='center' valign='top' width='700' class='flexibleContainerCell'>
    //                                                        <table border='0' cellpadding='30' cellspacing='0' width='100%'>



    //                                                            <tr>
    //                                                                <td align='left' valign='top' style='padding:10px;background:#fcfcfc'>
    //                                                                    <table border='0' cellpadding='0' cellspacing='0' style='float:left;float: left; margin-right: 0; width: 100%;'>
    //                                                                        <tr>
    //                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>
    //                                                                                <center><img style='height:50px;margin-bottom:0px;' src='" + ConfigurationManager.AppSettings["domain"] + @"/assets/imgs/logo.png' /></center>
    //                                                                            </td>
    //                                                                        </tr>
    //                                                                    </table>
    //                                                                </td>
    //                                                            </tr>


    //                                                            <tr>
    //                                                                <td align='left' valign='top' style='padding:0px;'>
    //                                                                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
    //                                                                        <tr>
    //                                                                            <td align='left' valign='top' width='100%' class='flexibleContainerCell'>

    //                                                                                <p style='font-size:22px;line-height:28px!important;text-align:center;color:#111;font-weight:bold!important'>
    //                                                                                    Order cancelled!
    //                                                                                </p>

    //                                                                                <p style='font-size:16px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
    //                                                                                    Hi " + name + @",
    //                                                                                </p>
    //                                                                                <p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:left;padding:0px 50px;'>
    //Greetings from Archidply, we have successfully created return of
    //your order, Please keep the products in its original shape with
    //its Price tag, original packing, our logistics team with shortly
    //contact with you to take return. Once we receive the products,
    //will initiate refunds to your original payment mode &amp; will be
    //intimated.
    //                                                                                 </p>
    //                                                                                <p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:30px;text-align:center;padding:0px 50px;'>
    //                                                                                      Your feedback is important for us, Kindly write to feedback " + ConfigurationManager.AppSettings["from"] + @"
    //                                                                                </p><p style='font-size:13px;color:#573e40;line-height:24px!important;margin-bottom:30px;padding:0px 50px;'>
    //                                                                                      Team Archidply<br>" + ConfigurationManager.AppSettings["domain"] + @"
    //                                                                                </p>


    //                                                                            </td>


    //                                                                        </tr>

    //                                                                    </table>
    //                                                                </td>
    //                                                            </tr>  
    //                                                             <tr>
    //                                        <td align='left' valign='top' style='padding:10px 20px;background:#f2f2f2'>
    //                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
    //                                                  <tr>
    //                                                                               <td align='center' valign='middle' width='100%' class='flexibleContainerCell'>
    //                                                                                    <ul style='list-style:none;width:390px;margin:0px auto;'>
    //                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
    //                                                                                            <a href='https://www.facebook.com/Archidplydecor.bonvivant' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/facebook.png' /></a>
    //                                                                                        </li>
    //                                                                                        <li style='display:none;margin-right:5px;padding-right:5px;'>
    //                                                                                            <a href='https://www.instagram.com/archidplydecor_bonvivant/' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/instagram.png' /></a>
    //                                                                                        </li>
    //                                                                                        <li style='display:none;margin-right:0px;padding-right:5px;'>
    //                                                                                            <a href='mailto:bangalore@archidply.com' target='_blank'><img width='25' src='"" + ConfigurationManager.AppSettings[""domain""] + @""/img/email-icons/mail.png' /></a>
    //                                                                                        </li>

    //                                                                                    </ul>
    //                                                                                </td>

    //                                                                            </tr> 
    //                                            </table>
    //                                        </td>
    //                                    </tr> 
    //                                                        </table>
    //                                                    </td>
    //                                                </tr>
    //                                            </table>
    //                                        </td>
    //                                    </tr>
    //                                </table>
    //                            </td>
    //                        </tr>
    //                    </table>
    //                </td>
    //            </tr>
    //        </table>

    //    </center>
    //</body>
    //</html>";
    //            #endregion
    //            MailMessage mail = new MailMessage();
    //            mail.To.Add(email);
    //            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
    //            mail.Subject = "Your " + ConfigurationManager.AppSettings["domain"] + @" order has been cancelled";
    //            mail.Body = mailbody;
    //            mail.IsBodyHtml = true;
    //            SmtpClient smtp = new SmtpClient();
    //            smtp.Host = ConfigurationManager.AppSettings["host"];
    //            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
    //            smtp.Credentials = new System.Net.NetworkCredential
    //                   (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
    //            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
    //            smtp.Send(mail);
    //            return 0;
    //        }
    //        catch (Exception exx)
    //        {
    //            return 0;
    //        }
    //    }


    #endregion
}