using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class Admin_view_blogs : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strBlogs = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetAllBlogs();
    }

    private void GetAllBlogs()
    {
        try
        {
            strBlogs = "";
            List<Blogs> list = Blogs.GetAllBlog(conT)
                .OrderByDescending(s => s.Id)
                .ToList();

            int i = 1;
            foreach (Blogs pro in list)
            {
                string thumb = !string.IsNullOrEmpty(pro.SmallImg)
                    ? "<img src='/" + pro.SmallImg + "' style='height:50px; border-radius:4px;' />"
                    : "<span class='text-muted'>No image</span>";

                strBlogs += @"<tr>
                    <td>" + i + @"</td>
                    <td>
                        <a href='/blog/" + pro.BlogUrl + @"' target='_blank'>" + pro.BlogName + @"</a>
                    </td>
                    <td>" + thumb + @"</td>
                    <td title='Written By: " + pro.AddedBy + @"'>" + pro.AddedOn.ToString("dd/MMM/yyyy") + @"</td>
                    <td class='text-center'>
                        <a href='write-blog.aspx?id=" + pro.Id + @"' class='link-info me-2 fs-18' title='Edit'>
                            <i class='mdi mdi-pencil'></i>
                        </a>
                        <a href='javascript:void(0);' class='deleteItem link-danger fs-18'
                           data-id='" + pro.Id + @"' title='Delete'>
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
                "GetAllBlogs", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            Blogs pro = new Blogs();
            pro.Id = Convert.ToInt32(id);
            pro.AddedOn = CommonModel.UTCTime();
            pro.AddedIP = CommonModel.IPAddress();
            pro.AddedBy = HttpContext.Current.Request.Cookies["t_aid"].Value;

            return Blogs.DeleteBlog(conT, pro) > 0 ? "Success" : "W";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "Delete", ex.Message);
            return "W";
        }
    }
}