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

public partial class Admin_page_master : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    public string strPage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", " this.disabled = 'true';this.value='Please Wait...'; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        GetAllPageMaster();
        if (!IsPostBack)
        {
            BindPageGroup();
            if (Request.QueryString["id"] != null)
            {
                GetPageMaster();
            }
            else
            {
                btnClear.Visible = false;
            }
        }
    }


    public void BindPageGroup()
    {
        try
        {
            List<PageGroup> pg = PageGroup.GetAllPageGroup(conT);
            if (pg.Count > 0)
            {
                ddlGroup.DataSource = pg;
                ddlGroup.DataTextField = "GroupName";
                ddlGroup.DataValueField = "Id";
                ddlGroup.DataBind();
            }
            ddlGroup.Items.Insert(0, new ListItem("Select Group", ""));
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindPageGroup", ex.Message);
        }
    }


    public void GetPageMaster()
    {
        try
        {
            List<PageMaster> comps = PageMaster.GetAllPageMaster(conT).Where(x => x.Id == Convert.ToInt32(Request.QueryString["id"])).ToList();
            if (comps.Count > 0)
            {
                btnSave.Text = "Update";
                btnClear.Visible = true;
                txtName.Text = comps[0].PageName;
                txtDesc.Text = comps[0].PageDesc;
                txtLink.Text = comps[0].PageLink;
                txtIcon.Text = comps[0].Icon;
                ddlShow.SelectedIndex = ddlShow.Items.IndexOf(ddlShow.Items.FindByValue(comps[0].ShowInMenu));
                ddlGroup.SelectedIndex = ddlGroup.Items.IndexOf(ddlGroup.Items.FindByValue(comps[0].PageGroup));
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetPageMaster", ex.Message);
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
                PageMaster ca = new PageMaster();
                if (btnSave.Text == "Update")
                {
                    ca.Id = Convert.ToInt32(Request.QueryString["id"]);
                    ca.PageName = txtName.Text;
                    ca.PageLink = txtLink.Text;
                    ca.PageDesc = txtDesc.Text;
                    ca.Icon = txtIcon.Text.Replace("\"", "'").Trim();
                    ca.PageGroup = ddlGroup.SelectedItem.Value;
                    ca.ShowInMenu = ddlShow.SelectedItem.Value;
                    ca.UpdatedOn = TimeStamps.UTCTime();
                    ca.Status = "Active";
                    int result = PageMaster.UpdatePageMaster(conT, ca);
                    if (result > 0)
                    {
                        GetAllPageMaster();
                        GetPageMaster();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Details updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'There is some problem now. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);

                    }

                }
                else
                {
                    ca.Icon = txtIcon.Text.Replace("\"", "'").Trim();
                    ca.PageName = txtName.Text;
                    ca.PageLink = txtLink.Text;
                    ca.PageDesc = txtDesc.Text;
                    ca.ShowInMenu = ddlShow.SelectedItem.Value;
                    ca.PageGroup = ddlGroup.SelectedItem.Value;
                    ca.UpdatedOn = TimeStamps.UTCTime();
                    ca.Status = "Active";
                    int result = PageMaster.InsertPageMaster(conT, ca);
                    if (result > 0)
                    {
                        GetAllPageMaster();
                        txtName.Text = txtDesc.Text = txtLink.Text = "";
                        ddlGroup.ClearSelection();
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


    public void GetAllPageMaster()
    {
        try
        {
            strPage = "";
            List<PageMaster> cas = PageMaster.GetAllPageMaster(conT).OrderByDescending(s => Convert.ToDateTime(s.UpdatedOn)).ToList();
            int i = 0;
            foreach (PageMaster ca in cas)
            {
                string show = ca.ShowInMenu == "1" ? "Yes" : "No";
                strPage += @"<tr>
<td>" + (i + 1) + @"</td>   
<td>" + ca.GroupName + @"</td>
<td>" + show + @"</td>
<td>" + ca.PageName + @"</td>
<td>" + ca.PageLink + @"</td>
<td><a href='javascript:void();' class='bs-tooltip' data-id='" + ca.Id + @"' data-bs-toggle='tooltip' data-placement='top' title=''>" + ca.UpdatedOn.ToString("dd-MMM-yyyy") + @"</a></td>  
                                                <td class='text-center'>
                                                    <a href='page-master.aspx?id=" + ca.Id + @"' class='bs-tooltip fs-18' data-id='" + ca.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Edit'>
                                                        <i class='mdi mdi-pencil'></i></a>
                                                    <a href='javascript:void(0);' class='bs-tooltip deleteItem link-danger fs-18'  data-id='" + ca.Id + @"' data-bs-toggle='tooltip' data-placement='top' title='Delete'>
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

            PageMaster cat = new PageMaster();
            cat.Id = Convert.ToInt32(id);
            cat.UpdatedOn = TimeStamps.UTCTime();
            int exec = PageMaster.DeletePageMaster(conT, cat);
            if (exec > 0)
            {
                x = "Success";
            }
            else
            {
                x = "W";
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
        Response.Redirect("page-master.aspx");
    }

}