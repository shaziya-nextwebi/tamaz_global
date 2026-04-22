using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_view_productenq : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strEnquiries = "";
    public string strPagination = "";
    public string strSuccessMsg = "";
    public string strErrorMsg = "";
    public int intResultCount = 0;
    public int intTotalPages = 0;

    private const int PageSize = 15;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BuildEnquiryTable(1);
        }
        else
        {
            // Handle bulk delete postback triggered from JS
            string target = Request.Form["__EVENTTARGET"];
            if (target == "btnBulkDeleteServer")
                DoBulkDelete();
        }
    }

    private List<ProductEnquiry> GetFilteredList()
    {
        List<ProductEnquiry> list = ProductEnquiry.GetAllContact(conT)
            .OrderByDescending(s => s.Id)
            .ToList();

        string search = txtSearch.Text.Trim().ToLower();
        string from = txtFrom.Text.Trim();
        string to = txtTo.Text.Trim();

        if (!string.IsNullOrEmpty(search))
            list = list.Where(s =>
                s.ProductName.ToLower().Contains(search) ||
                s.TypeofEnquiry.ToLower().Contains(search) ||
                s.Name.ToLower().Contains(search)
            ).ToList();

        if (!string.IsNullOrEmpty(from))
        {
            DateTime dtFrom = Convert.ToDateTime(from);
            list = list.Where(s => s.AddedOn.Date >= dtFrom.Date).ToList();
        }

        if (!string.IsNullOrEmpty(to))
        {
            DateTime dtTo = Convert.ToDateTime(to);
            list = list.Where(s => s.AddedOn.Date <= dtTo.Date).ToList();
        }

        return list;
    }

    private void BuildEnquiryTable(int pageIndex)
    {
        try
        {
            strEnquiries = "";
            List<ProductEnquiry> list = GetFilteredList();

            intResultCount = list.Count;
            intTotalPages = (int)Math.Ceiling((double)intResultCount / PageSize);

            // Clamp page index
            if (pageIndex < 1) pageIndex = 1;
            if (pageIndex > intTotalPages && intTotalPages > 0) pageIndex = intTotalPages;

            List<ProductEnquiry> paged = list
                .Skip((pageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            int startNo = (pageIndex - 1) * PageSize + 1;

            foreach (ProductEnquiry enq in paged)
            {
                string attrMsg = HttpUtility.HtmlAttributeEncode(enq.Message ?? "");

                string preview = enq.Message ?? "";
                if (preview.Length > 50)
                    preview = preview.Substring(0, 50) + "...";

                strEnquiries += @"<tr>
                    <td class='text-center'>
                        <input type='checkbox' class='form-check-input rowChk'
                               data-id='" + enq.Id + @"' />
                    </td>
                    <td>" + startNo + @"</td>
                    <td>" + HttpUtility.HtmlEncode(enq.TypeofEnquiry ?? "") + @"</td>
                    <td>
                        <a href='" + HttpUtility.HtmlAttributeEncode(enq.SourcePage ?? "#") + @"' target='_blank'>
                            " + HttpUtility.HtmlEncode(enq.ProductName ?? "") + @"
                        </a>
                    </td>
                    <td>" + HttpUtility.HtmlEncode(enq.Name ?? "") + @"</td>
                    <td>" + HttpUtility.HtmlEncode(enq.Email ?? "") + @"</td>
                    <td>" + HttpUtility.HtmlEncode(enq.Mobile ?? "") + @"</td>
                    <td>" + HttpUtility.HtmlEncode(enq.City ?? "") + @"</td>
<td>
  <div class='msg-cell'>
    <span class='message-preview'>" + HttpUtility.HtmlEncode(preview) + @"</span>
    <a href='javascript:void(0);' class='viewMsg ms-1 link-info'
       data-name='" + HttpUtility.HtmlAttributeEncode(enq.Name ?? "") + @"'
       data-email='" + HttpUtility.HtmlAttributeEncode(enq.Email ?? "") + @"'
       data-mobile='" + HttpUtility.HtmlAttributeEncode(enq.Mobile ?? "") + @"'
       data-city='" + HttpUtility.HtmlAttributeEncode(enq.City ?? "") + @"'
       data-product='" + HttpUtility.HtmlAttributeEncode(enq.ProductName ?? "") + @"'
       data-type='" + HttpUtility.HtmlAttributeEncode(enq.TypeofEnquiry ?? "") + @"'
       data-source='" + HttpUtility.HtmlAttributeEncode(enq.SourcePage ?? "") + @"'
       data-date='" + enq.AddedOn.ToString("dd/MMM/yyyy hh:mm tt") + @"'
       data-message='" + attrMsg + @"'
       title='View Message'>
      <i class='mdi mdi-eye-outline fs-18'></i>
    </a>
  </div>
</td>
                    <td>
                        <a href='" + HttpUtility.HtmlAttributeEncode(enq.SourcePage ?? "#") + @"' target='_blank'>
                            " + HttpUtility.HtmlEncode(enq.SourcePage ?? "") + @"
                        </a>
                    </td>
                    <td>" + enq.AddedOn.ToString("dd/MMM/yyyy HH:mm") + @"</td>
                </tr>";
                startNo++;
            }

            if (intResultCount == 0)
                strEnquiries = "<tr><td colspan='11' class='text-center text-muted py-3'>No records found.</td></tr>";

            BuildPagination(pageIndex);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "BuildEnquiryTable", ex.Message);
        }
    }
    private void BuildPagination(int currentPage)
    {
        strPagination = "";
        if (intTotalPages <= 1) return;

        Func<int, string, string, string> item = (page, label, extraClass) =>
            "<li class='page-item" + extraClass + "'>" +
            "<a class='page-link pageNav' data-page='" + page +
            "' href='javascript:void(0);'>" + label + "</a></li>";

        if (currentPage > 1)
            strPagination += item(currentPage - 1, "«", "");

        int delta = 2; 
        var pages = new List<int>();

        for (int p = 1; p <= intTotalPages; p++)
        {
            if (p == 1 || p == intTotalPages ||
                (p >= currentPage - delta && p <= currentPage + delta))
                pages.Add(p);
        }

        int prev = 0;
        foreach (int p in pages)
        {
            if (prev > 0 && p - prev > 1)
                strPagination += "<li class='page-item disabled'>" +
                                 "<span class='page-link'>…</span></li>";

            string active = p == currentPage ? " active" : "";
            strPagination += item(p, p.ToString(), active);
            prev = p;
        }

        // Next
        if (currentPage < intTotalPages)
            strPagination += item(currentPage + 1, "»", "");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BuildEnquiryTable(1);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        txtFrom.Text = "";
        txtTo.Text = "";
        BuildEnquiryTable(1);
    }
    private void DoBulkDelete()
    {
        try
        {
            string raw = hdnDeleteIds.Value;
            if (string.IsNullOrEmpty(raw))
            {
                strErrorMsg = "No records selected.";
                BuildEnquiryTable(1);
                return;
            }

            string[] ids = raw.Split(',');
            int deleted = 0;
            foreach (string id in ids)
            {
                if (!string.IsNullOrWhiteSpace(id))
                    deleted += ProductEnquiry.DeleteEnq(conT, id.Trim());
            }

            hdnDeleteIds.Value = "";
            strSuccessMsg = deleted + " record(s) deleted successfully.";
            BuildEnquiryTable(1);
        }
        catch (Exception ex)
        {
            strErrorMsg = "Something went wrong. Please try again.";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "DoBulkDelete", ex.Message);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            List<ProductEnquiry> list = GetFilteredList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Enquiry Type\tProduct Name\tName\tEmail\tMobile\tCity\tMessage\tSource Page\tAdded On");

            foreach (ProductEnquiry enq in list)
            {
                sb.AppendLine(
                    (enq.TypeofEnquiry ?? "") + "\t" +
                    (enq.ProductName ?? "") + "\t" +
                    (enq.Name ?? "") + "\t" +
                    (enq.Email ?? "") + "\t" +
                    (enq.Mobile ?? "") + "\t" +
                    (enq.City ?? "") + "\t" +
                    (enq.Message ?? "").Replace("\n", " ").Replace("\r", "") + "\t" +
                    (enq.SourcePage ?? "") + "\t" +
                    enq.AddedOn.ToString("dd/MMM/yyyy HH:mm")
                );
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
                "attachment;filename=ProductEnquiries_" +
                DateTime.Now.ToString("ddMMMyyyy") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "btnExport_Click", ex.Message);
        }
    }
}