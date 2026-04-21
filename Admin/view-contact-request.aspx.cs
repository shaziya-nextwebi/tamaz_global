using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_view_contact_request : System.Web.UI.Page
{
    public string strContactRequests = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindContactRequests();
        }
    }

    private void BindContactRequests()
    {
        SqlConnection conT = new SqlConnection(
            ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

        List<ContactUs> list = ContactUs.GetAllContactRequests(conT);

        int i = 1;

        foreach (var item in list)
        {
            strContactRequests += @"
                <tr>
                    <td>" + i + @"</td>
                    <td>" + item.Name + @"</td>
                    <td>" + item.EmailId + @"</td>
                    <td>" + item.Phone + @"</td>
                   <td>
    <a href='javascript:void(0);' 
       class='viewMessageBtn text-primary'
       data-message='" + Server.HtmlEncode(item.Message) + @"'>
       View Message
    </a>
</td>
                    <td>" + item.CreatedOn + @"</td>
                    <td class='text-center'>
                        <a href='javascript:void(0);'
                          class='fs-18 link-danger deleteItem' data-id='" + item.Id + @"'><i class='mdi mdi-trash-can-outline'></i></a>
                    </td>
                </tr>";

            i++;
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "W";

        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            ContactUs rev = new ContactUs();
            rev.Id = Convert.ToInt32(id);
            rev.CreatedOn = CommonModel.UTCTime();

            int exec = ContactUs.Delete(conT, rev);

            if (exec > 0)
                x = "Success";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "Delete", ex.Message);
        }

        return x;
    }
}