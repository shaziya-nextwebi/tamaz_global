<%@ WebHandler Language="C#" Class="product_images" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public class product_images : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if(context.Request.Files.Count > 0)
        {
           SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ToString());

            string strPid = context.Request["pid"];
            if (CreateUser.CheckAccess(conT, "add-products.aspx", "Add", HttpContext.Current.Request.Cookies["t_id"].Value))
            {
                HttpFileCollection files = context.Request.Files;
                int x = 0;
                for(int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string fileExtension = Path.GetExtension(file.FileName.ToLower()), ImageGuid1 = Guid.NewGuid().ToString();
                    string iconPath = context.Server.MapPath(".") + "\\../UploadImages\\" + ImageGuid1 + "_big" + fileExtension;
                    if ((fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".webp"))
                    {
                       

                        if (fileExtension == ".png")
                        {
                            System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(file.InputStream);
                            System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                             CommonModel.SavePNG(iconPath, objImagesmallBig, 80);
                        }
                        else if (fileExtension == ".webp")
                        {
                            file.SaveAs(iconPath);
                        }
                        else
                        {
                            System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(file.InputStream);
                            System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                            CommonModel.SaveJpeg(iconPath, objImagesmallBig, 90);
                        }

                        string bImage = "UploadImages/" + ImageGuid1 + "_big" + fileExtension;
                        ProductGallery pi = new ProductGallery();
                        pi.AddedBy = HttpContext.Current.Request.Cookies["t_id"].Value;
                        pi.Pid =Convert.ToString(strPid);
                        pi.Images = bImage;
                        pi.AddedOn = TimeStamps.UTCTime();
                        pi.AddedIp = CommonModel.IPAddress();
                        pi.Status = "Active";
                        x += ProductGallery.InsertProductGallery(conT, pi);
                    }
                }
                if (x > 0)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Success|" + x);
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Error|");
                }
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Permission|");
            }
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}