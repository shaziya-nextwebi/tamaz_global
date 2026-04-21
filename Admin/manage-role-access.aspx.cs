using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_manage_role_access : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    public string strUserName = "", strRole = "", strUserAccess = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetUserAccess();
        GetRoleDetails();
    }

    public void GetRoleDetails()
    {
        try
        {
            List<UserRoles> comps = UserRoles.GetAllUserRoles(conT).Where(x => x.Id == Convert.ToInt32(Request.QueryString["id"])).ToList();
            if (comps.Count > 0)
            {
                strRole = comps[0].RoleName;
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void GetUserAccess()
    {
        try
        {
            List<UserRolesAccess> users = UserRoles.GetAllUserRoles(conT, Request.QueryString["id"]).OrderBy(x => x.GOrderBy).ToList();
            int i = 0;
            foreach (var rl in users)
            {

                string ft1 = rl.ViewAccess == "1" ? "checked" : "";
                string ft2 = rl.AddAccess == "1" ? "checked" : "";
                string ft3 = rl.EditAccess == "1" ? "checked" : "";
                string ft4 = rl.DeleteAccess == "1" ? "checked" : "";


                strUserAccess += @"<tr>
                                                <td>" + (i + 1) + @"</td>  
<td>" + rl.PageGroupName + @"</td> 
<td><a href='javascript:void();' class='bs-tooltip' data-bs-toggle='tooltip' data-bs-placement='top' title='" + rl.PageDesc + @"'>" + rl.PageName + @"</a></td>
<td>" + rl.PageLink + @"</td> 
<td>
<div class='form-check form-switch changeAccess' id='v_" + rl.PageId + @"' data-id='" + rl.PageId + @"'>
    <input class='form-check-input' type='checkbox' data-id='" + rl.PageId + @"' id='chk_vi_" + rl.PageId + @"' role='switch' " + ft1 + @">
    <label class='form-check-label' for='chk_vi_" + rl.PageId + @"'></label>
</div>
</td>  
<td>
<div class='form-check form-switch changeAccess' id='v_" + rl.PageId + @"'  data-id='" + rl.PageId + @"'>
    <input class='form-check-input' type='checkbox' data-id='" + rl.PageId + @"' class='' id='chk_ad_" + rl.PageId + @"' role='switch' " + ft2 + @">
    <label class='form-check-label' for='chk_ad_" + rl.PageId + @"'></label>
</div>
</td>  
<td>
<div class='form-check form-switch changeAccess' id='v_" + rl.PageId + @"'  data-id='" + rl.PageId + @"'>
    <input class='form-check-input' type='checkbox' data-id='" + rl.PageId + @"' class='' id='chk_ed_" + rl.PageId + @"' role='switch' " + ft3 + @">
    <label class='form-check-label' for='chk_ed_" + rl.PageId + @"'></label>
</div>
 </td>  
<td>
<div class='form-check form-switch changeAccess' id='v_" + rl.PageId + @"'  data-id='" + rl.PageId + @"'>
    <input class='form-check-input' type='checkbox' data-id='" + rl.PageId + @"' id='chk_del_" + rl.PageId + @"' class='' role='switch' " + ft4 + @">
    <label class='form-check-label' for='chk_del_" + rl.PageId + @"'></label>
</div>
</td>  
</tr>";
                i++;
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteP", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string ChangeStatus(string pid, string rid, string v, string a, string e, string d)
    {
        string x = "";
        try
        {

            SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            List<UserRolesAccess> users = UserRoles.GetAllUserRolesCheck(conT, rid, pid);
            UserRolesAccess ua = new UserRolesAccess();
            if (users.Count > 0)
            {
                if (CreateUser.CheckAccess(conT, "manage-role-access.aspx", "Edit", HttpContext.Current.Request.Cookies["t_aid"].Value))
                {
                    ua.PageId = pid;
                    ua.RoleId = rid;
                    ua.AddAccess = a == "Y" ? "1" : "0";
                    ua.EditAccess = e == "Y" ? "1" : "0";
                    ua.ViewAccess = v == "Y" ? "1" : "0";
                    ua.DeleteAccess = d == "Y" ? "1" : "0";
                    ua.PageGroupId = "";
                    ua.UpdatedOn = TimeStamps.UTCTime();
                    int exec = UserRoles.UpdateRoleAccess(conT, ua);
                    if (exec > 0)
                    {
                        x = "Success";
                    }
                }
                else
                {
                    x = "Permission";
                }
            }
            else
            {
                if (CreateUser.CheckAccess(conT, "manage-role-access.aspx", "Add", HttpContext.Current.Request.Cookies["t_aid"].Value))
                {
                    ua.PageId = pid;
                    ua.RoleId = rid;
                    ua.AddAccess = a == "Y" ? "1" : "0";
                    ua.EditAccess = e == "Y" ? "1" : "0";
                    ua.ViewAccess = v == "Y" ? "1" : "0";
                    ua.DeleteAccess = d == "Y" ? "1" : "0";
                    ua.UpdatedOn = TimeStamps.UTCTime();
                    ua.Status = "Active";
                    ua.PageGroupId = "";
                    int exec = UserRoles.InsertToRoleAccess(conT, ua);
                    if (exec > 0)
                    {
                        x = "Success";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteP", ex.Message);
        }
        return x;
    }
}