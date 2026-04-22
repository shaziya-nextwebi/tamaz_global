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

    #endregion
}