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

public partial class Admin_page_group : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    public string strPageGroups = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        GetAllGroup();
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                GetGroups();
            }
            else
            {
                btnClear.Visible = false;
            }
        }
    }


    public void GetGroups()
    {
        try
        {
            List<PageGroup> comps = PageGroup.GetAllPageGroup(conT).Where(x => x.Id == Convert.ToInt32(Request.QueryString["id"])).ToList();
            if (comps.Count > 0)
            {
                btnSave.Text = "Update";
                txtName.Text = comps[0].GroupName;
                txtIcon.Text = comps[0].Icon;
                txtOrder.Text = comps[0].GroupOrder;
                btnClear.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetGroups", ex.Message);
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
                PageGroup ca = new PageGroup();
                if (btnSave.Text == "Update")
                {
                    ca.GroupOrder = txtOrder.Text.Trim() == "" ? "1000" : txtOrder.Text.Trim();
                    ca.GroupName = txtName.Text.Trim();
                    ca.Id = Convert.ToInt32(Request.QueryString["id"]);
                    ca.UpdatedOn = TimeStamps.UTCTime();
                    ca.Icon = txtIcon.Text.Replace("\"", "'").Trim();
                    ca.Status = "Active";
                    int result = PageGroup.UpdatePageGroup(conT, ca);
                    if (result > 0)
                    {
                        GetAllGroup();
                        GetGroups();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Details updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                    }
                }
                else
                {
                    ca.GroupOrder = txtOrder.Text.Trim() == "" ? "1000" : txtOrder.Text.Trim();
                    ca.GroupName = txtName.Text.Trim();
                    ca.Icon = txtIcon.Text.Replace("\"", "'").Trim();
                    ca.UpdatedOn = TimeStamps.UTCTime();
                    ca.Status = "Active";
                    int result = PageGroup.InsertPageGroup(conT, ca);
                    if (result > 0)
                    {
                        txtName.Text = "";txtOrder.Text = ""; txtIcon.Text = "";
                        GetAllGroup();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Details added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnSave_Click", ex.Message);
        }
    }


    public void GetAllGroup()
    {
        try
        {
            strPageGroups = "";
            List<PageGroup> cas = PageGroup.GetAllPageGroup(conT).OrderByDescending(s => Convert.ToDateTime(s.UpdatedOn)).ToList();
            int i = 0;
            foreach (PageGroup ca in cas)
            {

                strPageGroups += @"<tr>
                                                <td>" + (i + 1) + @"</td>  
<td>" + ca.Icon + @"</td>
<td>" + ca.GroupOrder + @"</td>
<td>" + ca.GroupName + @"</td>
<td><a href='javascript:void();' class='bs-tooltip' data-id='" + ca.Id + @"' data-bs-toggle='tooltip' data-bs-placement='top' title=''>" + ca.UpdatedOn.ToString("dd-MMM-yyyy") + @"</a></td>  
                                                <td class='text-center'>
                                                    <a href='page-group.aspx?id=" + ca.Id + @"' class='bs-tooltip fs-18 link-info' data-id='" + ca.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip fs-18 link-danger deleteItem' data-id='" + ca.Id + @"' data-bs-toggle='tooltip' data-bs-placement='top' title='Delete'>
                                                        <i class='mdi mdi-trash-can-outline'></i></a>
                                                </td>

                                            </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllGroup", ex.Message);
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            if (CreateUser.CheckAccess(conT, "page-group.aspx", "Delete", HttpContext.Current.Request.Cookies["t_aid"].Value))
            {
                PageGroup cat = new PageGroup();
                cat.Id = Convert.ToInt32(id);
                cat.UpdatedOn = TimeStamps.UTCTime();
                int exec = PageGroup.DeletePageGroup(conT, cat);
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
        Response.Redirect("page-group.aspx");
    }
}