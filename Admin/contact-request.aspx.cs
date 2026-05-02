using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class Admin_view_contact : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strContact = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetAllContactRequest();
    }

    private void GetAllContactRequest()
    {
        try
        {
            strContact = "";
            List<Contact> contacts = Contact.GetAllContact(conT)
                .Where(x => x.Status == "Active")
                .OrderByDescending(o => o.Id)
                .ToList();

            int i = 1;
            foreach (Contact con in contacts)
            {
                // Convert newlines to <br> so modal renders them correctly
                string formattedMsg = System.Web.HttpUtility.HtmlEncode(con.Comments)
                    .Replace("&#13;&#10;", "<br/>")
                    .Replace("&#10;", "<br/>")
                    .Replace("&#13;", "<br/>");

                // Safe attribute value — encode for data-* attribute
                string attrMsg = System.Web.HttpUtility.HtmlAttributeEncode(
                    con.Comments ?? "");

                // Short preview in the table cell
                string preview = con.Comments ?? "";
                if (preview.Length > 60)
                    preview = preview.Substring(0, 60) + "...";

                strContact += @"<tr>
                    <td>" + i + @"</td>
                    <td>" + con.UserName + @"</td>
                    <td>" + con.PhoneNo + @"</td>
                    <td>" + con.City + @"</td>
                    <td class='text-center'>
                      
                        <a href='javascript:void(0);' class='viewMsg ms-1 link-info'
                           data-name='" + System.Web.HttpUtility.HtmlAttributeEncode(con.UserName ?? "") + @"'
                           data-phone='" + System.Web.HttpUtility.HtmlAttributeEncode(con.PhoneNo ?? "") + @"'
                           data-city='" + System.Web.HttpUtility.HtmlAttributeEncode(con.City ?? "") + @"'
                           data-date='" + con.AddedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"'
                           data-message='" + attrMsg + @"'
                           title='View Message'>
                            View Message
                        </a>
                    </td>
                    <td>" + con.AddedOn.ToString("dd/MMM/yyyy") + @"</td>
                    <td class='text-center'>
                        <a href='javascript:void(0);' class='deleteItem link-danger fs-18'
                           data-id='" + con.Id + @"' title='Delete'>
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
                "GetAllContactRequest", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            Contact cat = new Contact();
            cat.Id = Convert.ToInt32(id);
            cat.AddedOn = CommonModel.UTCTime();
            cat.AddedIp = CommonModel.IPAddress();
            cat.Status = "Deleted";

            return Contact.DeleteContact(conT, cat) > 0 ? "Success" : "W";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "Delete", ex.Message);
            return "W";
        }
    }
}