using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class Admin_manage_product_label : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strLabels = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
                BindLabelForEdit();

            GetAllLabels();
        }
    }

    // ===================== BIND FOR EDIT =====================

    private void BindLabelForEdit()
    {
        try
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            var label = ProductLabelMaster.GetProductLabelById(conT, id);
            if (label != null)
            {
                txtlable.Text = label.ProductLabel;
                btnSave.Text = "Update";
                lblHiddenId.Text = label.Id.ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "BindLabelForEdit", ex.Message);
        }
    }

    // ===================== GET ALL =====================

    private void GetAllLabels()
    {
        try
        {
            strLabels = "";
            var list = ProductLabelMaster.GetAllProductlabels(conT);
            int i = 0;
            foreach (var item in list)
            {
                strLabels += @"<tr>
                    <td>" + (i + 1) + @"</td>
                    <td>" + item.ProductLabel + @"</td>
                    <td>" + item.AddedOn.ToString("dd/MMM/yyyy") + @"</td>
                    <td class='text-center'>
                        <a href='manage-product-label.aspx?id=" + item.Id + @"'
                           class='bs-tooltip link-info me-1' title='Edit'>
                            <svg xmlns='http://www.w3.org/2000/svg' width='20' height='24'
                                 viewBox='0 0 24 24' fill='none' stroke='currentColor'
                                 stroke-width='2' stroke-linecap='round' stroke-linejoin='round'
                                 class='feather feather-edit'>
                                <path d='M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7'></path>
                                <path d='M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z'></path>
                            </svg>
                        </a>
                        <a href='javascript:void(0);'
                           class='bs-tooltip deleteItem link-danger' data-id='" + item.Id + @"'
                           title='Delete'>
                            <svg xmlns='http://www.w3.org/2000/svg' width='20' height='24'
                                 viewBox='0 0 24 24' fill='none' stroke='currentColor'
                                 stroke-width='2' stroke-linecap='round' stroke-linejoin='round'
                                 class='feather feather-trash-2'>
                                <polyline points='3 6 5 6 21 6'></polyline>
                                <path d='M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2'></path>
                                <line x1='10' y1='11' x2='10' y2='17'></line>
                                <line x1='14' y1='11' x2='14' y2='17'></line>
                            </svg>
                        </a>
                    </td>
                </tr>";
                i++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetAllLabels", ex.Message);
        }
    }

    // ===================== SAVE / UPDATE =====================

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;
        try
        {
            string aid = Request.Cookies["t_aid"].Value;

            ProductLabelMaster label = new ProductLabelMaster();
            label.ProductLabel = txtlable.Text.Trim();
            label.AddedIp = CommonModel.IPAddress();
            label.AddedOn = CommonModel.UTCTime();
            label.AddedBy = aid;

            if (btnSave.Text == "Update")
            {
                label.Id = Convert.ToInt32(lblHiddenId.Text);
                int result = ProductLabelMaster.UpdateProductLabel(conT, label);
                if (result > 0)
                {
                    GetAllLabels();
                    ShowSuccess("Label updated successfully.");
                }
                else ShowError("Something went wrong. Please try again.");
            }
            else
            {
                int result = ProductLabelMaster.InsertProductLabel(conT, label);
                if (result > 0)
                {
                    txtlable.Text = "";
                    GetAllLabels();
                    ShowSuccess("Label added successfully.");
                }
                else ShowError("Something went wrong. Please try again.");
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "btnSave_Click", ex.Message);
        }
    }

    // ===================== DELETE (WebMethod) =====================

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            ProductLabelMaster label = new ProductLabelMaster
            {
                Id = Convert.ToInt32(id),
                AddedOn = CommonModel.UTCTime(),
                AddedIp = CommonModel.IPAddress(),
                AddedBy = ""
            };

            int result = ProductLabelMaster.DeleteProductLabel(conT, label);
            return result > 0 ? "Success" : "W";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "Delete", ex.Message);
            return "W";
        }
    }

    // ===================== HELPERS =====================

    private void ShowSuccess(string msg)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "msg",
            "Snackbar.show({pos:'top-right',text:'" + msg +
            "',actionTextColor:'#fff',backgroundColor:'#008a3d'});", true);
    }

    private void ShowError(string msg)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "msg",
            "Snackbar.show({pos:'top-right',text:'" + msg +
            "',actionTextColor:'#fff',backgroundColor:'#ea1c1c'});", true);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtlable.Text = "";
        lblHiddenId.Text = "";
        btnSave.Text = "Save";
    }
}