using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_create_role : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    public string strUserRoless = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        GetAllRoles();
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                GetRoles();
            }
            else
            {
                btnClear.Visible = false;
            }
        }
    }
    public void GetRoles()
    {
        try
        {
            List<UserRoles> comps = UserRoles.GetAllUserRoles(conT).Where(x => x.Id == Convert.ToInt32(Request.QueryString["id"])).ToList();
            if (comps.Count > 0)
            {
                btnSave.Text = "Update";
                txtName.Text = comps[0].RoleName;
                btnClear.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetRoles", ex.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblStatus.Visible = true;
        try
        {
            if (Page.IsValid)
            {
                string pageName = Path.GetFileName(Request.Path);
                UserRoles ca = new UserRoles();
                if (btnSave.Text == "Update")
                {
                    if (CreateUser.CheckAccess(conT, pageName, "Edit", Request.Cookies["t_aid"].Value))
                    {
                        ca.RoleName = txtName.Text;
                        ca.Id = Convert.ToInt32(Request.QueryString["id"]);
                        ca.UpdatedOn = TimeStamps.UTCTime();
                        ca.Status = "Active";
                        int result = UserRoles.UpdateUserRoles(conT, ca);
                        if (result > 0)
                        {
                            GetAllRoles();
                            GetRoles();
                            lblStatus.Text = "Details Updated successfully.";
                            lblStatus.Attributes.Add("class", "alert alert-success");
                        }
                        else
                        {
                            lblStatus.Text = "There is some problem now. Please try after some time";
                            lblStatus.Attributes.Add("class", "alert alert-danger");
                        }
                    }
                    else
                    {
                        lblStatus.Text = "Access denied. Contact to your administrator";
                        lblStatus.Attributes.Add("class", "alert alert-danger");
                    }
                }
                else
                {
                    if (CreateUser.CheckAccess(conT, pageName, "Add", Request.Cookies["t_aid"].Value))
                    {
                        ca.RoleName = txtName.Text;
                        ca.UpdatedOn = TimeStamps.UTCTime();
                        ca.Status = "Active";
                        int result = UserRoles.InsertUserRoles(conT, ca);
                        if (result > 0)
                        {
                            txtName.Text = "";
                            GetAllRoles();
                            lblStatus.Text = "Details added successfully.";
                            lblStatus.Attributes.Add("class", "alert alert-success");
                        }
                        else
                        {
                            lblStatus.Text = "There is some problem now. Please try after some time";
                            lblStatus.Attributes.Add("class", "alert alert-danger");
                        }
                    }
                    else
                    {
                        lblStatus.Text = "Access denied. Contact to your administrator";
                        lblStatus.Attributes.Add("class", "alert alert-danger");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "There is some problem now. Please try after some time";
            lblStatus.Attributes.Add("class", "alert alert-danger");
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    } 
    public void GetAllRoles()
    {
        try
        {
            strUserRoless = "";
            List<UserRoles> cas = UserRoles.GetAllUserRoles(conT).OrderByDescending(s => Convert.ToDateTime(s.UpdatedOn)).ToList();
            int i = 0;
            foreach (UserRoles ca in cas)
            {
                strUserRoless += @"<tr>
                                                <td>" + (i + 1) + @"</td>  
<td>" + ca.RoleName + @"</td>
<td><a href='manage-role-access.aspx?id=" + ca.Id + @"' class='bs-tooltip fs-18' data-id='" + ca.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Manage Role Access'><i class='mdi mdi-arrow-top-right-bold-box-outline'></i></a></td>  
<td>" + ca.UpdatedOn.ToString("dd-MMM-yyyy") + @"</td>  
                                                <td class='text-center'>
                                                    <a href='create-role.aspx?id=" + ca.Id + @"' class='bs-tooltip fs-18' data-id='" + ca.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger deleteItem' data-id='" + ca.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a>
                                                </td> 
                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllRoles", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            if (CreateUser.CheckAccess(conT, "create-role.aspx", "Delete", HttpContext.Current.Request.Cookies["t_aid"].Value))
            {
                UserRoles cat = new UserRoles();
                cat.Id = Convert.ToInt32(id);
                cat.UpdatedOn = TimeStamps.UTCTime();
                int exec = UserRoles.DeleteUserRoles(conT, cat);
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteP", ex.Message);
        }
        return x;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("create-role.aspx");
    }

}