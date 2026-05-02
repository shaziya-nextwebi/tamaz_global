<%@ WebHandler Language="C#" Class="product_images" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public class product_images : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            if (context.Request.Files.Count == 0)
            {
                context.Response.Write("Error|No files");
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["conT"].ConnectionString;
            SqlConnection conT = new SqlConnection(connStr);

            string strPid = context.Request["pid"];

            // Ensure upload folder exists
            string folderPath = context.Server.MapPath("~/UploadImages/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            int x = 0;

            foreach (string key in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[key];

                string ext = Path.GetExtension(file.FileName.ToLower());
                string guid = Guid.NewGuid().ToString();

                if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".webp")
                    continue;

                string filePath = Path.Combine(folderPath, guid + "_big" + ext);

                file.SaveAs(filePath);

                string dbPath = "UploadImages/" + guid + "_big" + ext;
                ProductGallery pi = new ProductGallery
                {
                    AddedBy = "Admin",
                    Pid = strPid,
                    Images = dbPath,

                    // ⭐ ADD THIS LINE
                    GalleryType = context.Request["GType"] ?? "Image",

                    AddedOn = TimeStamps.UTCTime(),
                    AddedIp = CommonModel.IPAddress(),
                    Status = "Active"
                };

                x += ProductGallery.InsertProductGallery(conT, pi);
            }

            context.Response.Write(x > 0 ? "Success|" + x : "Error|");
        }
        catch (Exception ex)
        {
            context.Response.Write("Error|" + ex.Message);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}