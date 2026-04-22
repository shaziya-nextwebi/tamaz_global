using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Blog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static object BindBlogs(int pNo, int pSize)
    {
        var response = new
        {
            Status = "",
            Html = "",
            TotalCount = 0
        };

        try
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            int rowNo = (pNo - 1) * pSize;

            var list = Blogs.GetAllBlogsd(con, rowNo);

            if (list.Count > 0)
            {
                string html = "";

                foreach (var item in list)
                {
                    html += @"
<div class='reveal blog-card group bg-white br-12 shadow-sm border border-slate-100 overflow-hidden hover:shadow-xl transition-all duration-500'>
    <a href='/blog/" + item.BlogUrl + @"'>
        <div class='relative overflow-hidden'>
            <img src='" + item.SmallImg + @"' class='w-full h-56 object-cover card-img' />
            <div class='absolute top-4 left-4'>
                <span class='px-3 py-1 rounded-full bg-red-700 text-white text-xs font-semibold'>" + item.BlogTags + @"</span>
            </div>
        </div>
        <div class='p-6'>
            <div class='flex items-center gap-4 text-xs text-slate-500 mb-3'>
                <span class='flex items-center gap-1'>
                    <span class='iconify w-4 h-4' data-icon='lucide:calendar'></span>
                    " + Convert.ToDateTime(item.PostedOn).ToString("MMM dd, yyyy") + @"
                </span>
                <span class='flex items-center gap-1'>
                    <span class='iconify w-4 h-4' data-icon='lucide:user'></span>
                    " + item.PostedBy + @"
                </span>
            </div>
            <h3 class='text-xl font-bold mb-3 blog-title transition-colors'>
                " + item.BlogName + @"
            </h3>
            <p class='text-slate-500 text-sm mb-4 line-clamp-2'>
                " + item.MetaDesc + @"
            </p>
            <span class='inline-flex items-center gap-2 text-red-700 font-semibold text-sm group-hover:gap-3 transition-all'>
                Read More
                <span class='iconify w-4 h-4' data-icon='lucide:arrow-right'></span>
            </span>
        </div>
    </a>
</div>";
                }

                return new
                {
                    Status = "Success",
                    Html = html,
                    TotalCount = list[0].TotalBlogNo
                };
            }

            return new { Status = "Error", Html = "", TotalCount = 0 };
        }
        catch
        {
            return new { Status = "Error", Html = "", TotalCount = 0 };
        }
    }

}