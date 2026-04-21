using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_view_admin_users : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    public string strUsers = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllUsers();
        }
    }
    public void GetAllUsers()
    {
        try
        {
            strUsers = "";
            List<CreateUser> instrs = CreateUser.GetAllUser(conT);
            int i = 0;
            foreach (CreateUser cat in instrs)
            {
                string lastlogged = cat.log_time == null ? "No Logs found" : "Last logged at : " + cat.log_time.Value.ToString("dd MMM yyyy hh:mm tt") + @", Last logged IP : " + cat.log_ip + @"";
                string ft1 = cat.Status == "Blocked" ? "checked" : "";

                string sts = cat.Status == "Blocked" ? "<span id='sts_" + cat.Id + @"' class='badge badge-soft-danger'>Blocked</span>" : "<span id='sts_" + cat.Id + @"' class='badge badge-soft-success'>Active</span>";

                string chk = @"
<div class='form-check form-switch markBlocked' id='chk_" + cat.Id + @"'  data-id='" + cat.Id + @"'>
    <input class='form-check-input' type='checkbox' data-id='" + cat.Id + @"' class='' id='chk_u_" + cat.Id + @"' role='switch' " + ft1 + @">
    <label class='form-check-label' for='chk_u_" + cat.Id + @"'></label>
</div>";
                strUsers += @"<tr>
                                                <td>" + (i + 1) + @"</td>
                                                <td><a target='_blank' href='/" + cat.ProfileImage + @"'><img src='/" + cat.ProfileImage + @"' style='max-height:50px;' /></a> </td>
                                                <td>" + cat.UserName + @"</td>
                                                <td>" + cat.UserRole + @"</td>
                                                <td>" + cat.EmailId + @"</td> 
  <td>" + sts + @"</td> 
  <td>" + chk + @"</td> 
                                                 <td><span class='bs-tooltip' data-bs-toggle='tooltip' data-placement='top' title='Added By : " + cat.AddedBy + @"' >" + cat.AddedOn.ToString("dd-MMM-yyyy") + @"</span></td>   
                                                                                                <td class=''>
                                                    <a href='create-admin-user.aspx?id=" + cat.Id + @"' class='bs-tooltip fs-18' data-id='" + cat.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip  fs-18 link-danger deleteItem' data-id='" + cat.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a>
                                                </td>

                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllUsers", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            if (CreateUser.CheckAccess(conT, "view-admin-users.aspx", "Delete", HttpContext.Current.Request.Cookies["t_aid"].Value))
            {
                CreateUser cat = new CreateUser();
                cat.Id = Convert.ToInt32(id);
                cat.Status = "Deleted";
                cat.AddedOn = TimeStamps.UTCTime();
                cat.AddedIP = CommonModel.IPAddress();
                cat.AddedBy = HttpContext.Current.Request.Cookies["t_aid"].Value;
                int exec = CreateUser.DeleteUser(conT, cat);
                if (exec > 0)
                {
                    x = "Success";
                }
                else
                {
                    x = "W";
                }
            }
            else
            {
                x = "Permission";
            }
        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
        }
        return x;
    }

    [WebMethod(EnableSession = true)]
    public static string BlockUsers(string id, string ftr)
    {
        string x = "";
        string vali = "";
        try
        {
            SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            if (CreateUser.CheckAccess(conT, "view-admin-users.aspx", "Edit", HttpContext.Current.Request.Cookies["t_aid"].Value))
            {
                int exec = 0;
                CreateUser pro = new CreateUser();
                if (ftr == "Yes")
                {
                    exec = CreateUser.BlockUser(conT, id, HttpContext.Current.Request.Cookies["t_aid"].Value);
                    vali = "Blocked";
                }
                else
                {
                    exec = CreateUser.UnBlockUser(conT, id, HttpContext.Current.Request.Cookies["t_aid"].Value);
                    vali = "UnBlocked";
                }

                if (exec > 0)
                {
                    x = "Success" + "-" + vali;
                }
                else
                {
                    x = "W";
                }
            }
            else
            {
                x = "Permission";
            }
        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BlockUsers", ex.Message);
        }
        return x;
    }
}