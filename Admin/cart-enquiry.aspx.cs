using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class Admin_product_enquiry : System.Web.UI.Page
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
            List<CartlistEnq> contacts = CartlistEnq.GetAllcartEnquiry(conT)
                .Where(x => x.Status == "Active")
                .OrderByDescending(o => o.Id)
                .ToList();

            int i = 1;
            foreach (CartlistEnq con in contacts)
            {
                // Convert newlines to <br> so modal renders them correctly
                string formattedMsg = System.Web.HttpUtility.HtmlEncode(con.Message)
                    .Replace("&#13;&#10;", "<br/>")
                    .Replace("&#10;", "<br/>")
                    .Replace("&#13;", "<br/>");

                // Safe attribute value — encode for data-* attribute
                string attrMsg = System.Web.HttpUtility.HtmlAttributeEncode(
                    con.Message ?? "");

                // Short preview in the table cell
                string preview = con.Message ?? "";
                if (preview.Length > 60)
                    preview = preview.Substring(0, 60) + "...";

                strContact += @"<tr>
                    <td>" + i + @"</td>
                    <td>" + System.Web.HttpUtility.HtmlEncode(con.ProductName ?? "") + @"</td>
                    <td>" + System.Web.HttpUtility.HtmlEncode(con.Name ?? "") + @"</td>
                    <td>" + System.Web.HttpUtility.HtmlEncode(con.Mobile ?? "") + @"</td>
                    <td>" + System.Web.HttpUtility.HtmlEncode(con.City ?? "") + @"</td>
                    <td>
                        <span class='message-preview d-inline-block'>" + System.Web.HttpUtility.HtmlEncode(preview) + @"</span>
                        <a href='javascript:void(0);' class='viewMsg ms-1 link-info'
                           data-name='" + System.Web.HttpUtility.HtmlAttributeEncode(con.Name ?? "") + @"'
                           data-mobile='" + System.Web.HttpUtility.HtmlAttributeEncode(con.Mobile ?? "") + @"'
                           data-city='" + System.Web.HttpUtility.HtmlAttributeEncode(con.City ?? "") + @"'
                           data-product='" + System.Web.HttpUtility.HtmlAttributeEncode(con.ProductName ?? "") + @"'
                           data-date='" + con.AddedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"'
                           data-message='" + attrMsg + @"'
                           title='View Message'>
                            <i class='mdi mdi-eye-outline fs-18'></i>
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

            if (CreateUser.CheckAccess(conT, "cart-enquiry.aspx", "Delete",
                HttpContext.Current.Request.Cookies["tamz_new_id"].Value))
            {
                CartlistEnq cat = new CartlistEnq();
                cat.Id = Convert.ToInt32(id);
                cat.AddedOn = TimeStamps.UTCTime();
                cat.AddedIp = CommonModel.IPAddress();
                cat.Status = "Deleted";

                int exec = CartlistEnq.DeleteEnq(conT, cat);
                return exec > 0 ? "Success" : "W";
            }
            else
            {
                return "Permission";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "Delete", ex.Message);
            return "W";
        }
    }
}