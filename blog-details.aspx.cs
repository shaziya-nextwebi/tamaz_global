using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public partial class BlogDetail : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string BlogTitle = "";
    public string CategoryName = "";
    public string ImageUrl = "";
    public string Author = "";
    public string PublishDate = "";
    public string BlogContent = "";
    public string BlogTags = "";
    public string RecentPosts = "";
    public string RelatedPosts = "";
    public string Categories = "";
    private string strCurrentId = "";
    private string strBlogURL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        strBlogURL = Convert.ToString(RouteData.Values["BUrl"]);

        if (string.IsNullOrEmpty(strBlogURL))
            strBlogURL = Request.QueryString["url"];

        if (!string.IsNullOrEmpty(strBlogURL))
        {
            BindBlogDetails();
        }
        else if (Request.QueryString["id"] != null)
        {
            int blogId = Convert.ToInt32(Request.QueryString["id"]);
            LoadBlogById(blogId);
        }
    }

    private void BindBlogDetails()
    {
        try
        {
            List<Blogs> blogs = Blogs.GetAllBlogDetailsById_Url(conT, strBlogURL);
            if (blogs != null && blogs.Count > 0)
            {
                SetBlogProperties(blogs[0]);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "BindBlogDetails", ex.Message);
        }
    }

    private void LoadBlogById(int blogId)
    {
        try
        {
            List<Blogs> blogs = Blogs.GetBlog(conT, blogId);
            if (blogs != null && blogs.Count > 0)
            {
                SetBlogProperties(blogs[0]);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "LoadBlogById", ex.Message);
        }
    }

    private void SetBlogProperties(Blogs blog)
    {
        strCurrentId = blog.Id.ToString();
        BlogTitle = blog.BlogName;
        BlogContent = blog.FullDesc;
        ImageUrl = blog.BigImg;
        Author = blog.PostedBy;
        PublishDate = blog.PostedOn.ToString("MMMM dd, yyyy");
        CategoryName = blog.BlogTags;

        // SEO
        if (!string.IsNullOrEmpty(blog.PageTitle))
            Page.Title = blog.PageTitle + " - TAMAZ Global";
        else
            Page.Title = blog.BlogName + " - TAMAZ Global";

        if (!string.IsNullOrEmpty(blog.MetaDesc))
            Page.MetaDescription = blog.MetaDesc;

        if (!string.IsNullOrEmpty(blog.MetaKeys))
            Page.MetaKeywords = blog.MetaKeys;

        BindTags(blog.BlogTags);
        BindRecentPosts();
        BindRelatedPosts(blog.Id, blog.BlogTags);
        BindCategories();
    }

    private void BindTags(string blogTags)
    {
        if (string.IsNullOrWhiteSpace(blogTags)) return;

        string[] tags = blogTags.Split(new char[] { ',', ';' },
                                       StringSplitOptions.RemoveEmptyEntries);
        foreach (string tag in tags)
        {
            string t = tag.Trim();
            BlogTags += "<a href='Blog.aspx?tag=" + HttpUtility.UrlEncode(t) + "' " +
                        "class='px-3 py-1 bg-white border border-slate-200 rounded-full " +
                        "text-xs text-slate-600 hover:bg-slate-100 transition-colors'>" +
                        HttpUtility.HtmlEncode(t) +
                        "</a>";
        }
    }
    private void BindRecentPosts()
    {
        try
        {
            List<Blogs> all = Blogs.GetAllBlog(conT);
            int count = 0;

            foreach (Blogs b in all)
            {
                if (b.Id.ToString() == strCurrentId) continue;
                if (count >= 2) break;

                string img = string.IsNullOrEmpty(b.SmallImg) ? b.BigImg : b.SmallImg;
                string date = b.PostedOn.ToString("MMMM dd, yyyy");
                string url = GetRouteUrl("BlogDetail", new { BUrl = b.BlogUrl });

                RecentPosts += "<a href='" + url + "' class='flex gap-3 group'>";
                RecentPosts += "<img src='/" + HttpUtility.HtmlEncode(img) + "' " +
                               "alt='" + HttpUtility.HtmlEncode(b.BlogName) + "' " +
                               "class='w-16 h-16 rounded-lg object-cover flex-shrink-0' />";
                RecentPosts += "<div>";
                RecentPosts += "<h4 class='text-sm font-semibold text-slate-900 " +
                               "group-hover:text-red-700 transition-colors leading-tight'>" +
                               HttpUtility.HtmlEncode(b.BlogName) + "</h4>";
                RecentPosts += "<p class='text-xs text-slate-500 mt-1'>" + date + "</p>";
                RecentPosts += "</div>";
                RecentPosts += "</a>";

                count++;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "BindRecentPosts", ex.Message);
        }
    }
    private void BindRelatedPosts(int currentId, string currentTags)
    {
        try
        {
            List<Blogs> all = Blogs.GetAllBlog(conT);
            List<Blogs> related = new List<Blogs>();

            string[] currentTagArr = (currentTags ?? "")
                                     .Split(new char[] { ',', ';' },
                                            StringSplitOptions.RemoveEmptyEntries);

            // First pass: match by shared tags
            foreach (Blogs b in all)
            {
                if (b.Id == currentId) continue;
                if (related.Count >= 2) break;

                bool hasMatch = false;
                foreach (string t in currentTagArr)
                {
                    if (!string.IsNullOrWhiteSpace(t) &&
                        (b.BlogTags ?? "").IndexOf(t.Trim(),
                         StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        hasMatch = true;
                        break;
                    }
                }
                if (hasMatch || currentTagArr.Length == 0)
                    related.Add(b);
            }

            // Fallback: fill remaining slots with latest posts
            if (related.Count < 2)
            {
                foreach (Blogs b in all)
                {
                    if (b.Id == currentId) continue;
                    if (related.Exists(delegate (Blogs x) { return x.Id == b.Id; })) continue;
                    related.Add(b);
                    if (related.Count >= 2) break;
                }
            }

            foreach (Blogs b in related)
            {
                string img = string.IsNullOrEmpty(b.SmallImg) ? b.BigImg : b.SmallImg;
                string date = b.PostedOn.ToString("MMM dd, yyyy");
                string url = GetRouteUrl("BlogDetail", new { BUrl = b.BlogUrl });
                string tag = !string.IsNullOrEmpty(b.BlogTags)
                              ? b.BlogTags.Split(',')[0].Trim()
                              : "Blog";

                RelatedPosts += "<div class='reveal blog-card group bg-white br-12 shadow-sm " +
                                "border border-slate-100 overflow-hidden hover:shadow-xl " +
                                "transition-all duration-500'>";

                RelatedPosts += "<a href='" + url + "'>";

                RelatedPosts += "<div class='relative overflow-hidden'>";
                RelatedPosts += "<img src='/" + HttpUtility.HtmlEncode(img) + "' " +
                                "alt='" + HttpUtility.HtmlEncode(b.BlogName) + "' " +
                                "class='w-full h-56 object-cover card-img' />";
                RelatedPosts += "<div class='absolute top-4 left-4'>";
                RelatedPosts += "<span class='px-3 py-1 rounded-full bg-red-700 " +
                                "text-white text-xs font-semibold'>" +
                                HttpUtility.HtmlEncode(tag) + "</span>";
                RelatedPosts += "</div>";
                RelatedPosts += "</div>";

                RelatedPosts += "<div class='p-6'>";
                RelatedPosts += "<div class='flex items-center gap-4 text-xs text-slate-500 mb-3'>";
                RelatedPosts += "<span class='flex items-center gap-1'>" +
                                "<span class='iconify w-4 h-4' data-icon='lucide:calendar'></span>" +
                                date + "</span>";
                RelatedPosts += "<span class='flex items-center gap-1'>" +
                                "<span class='iconify w-4 h-4' data-icon='lucide:user'></span>" +
                                HttpUtility.HtmlEncode(b.PostedBy) + "</span>";
                RelatedPosts += "</div>";

                RelatedPosts += "<h3 class='text-xl font-bold mb-3 blog-title transition-colors'>" +
                                HttpUtility.HtmlEncode(b.BlogName) + "</h3>";

                RelatedPosts += "<span class='inline-flex items-center gap-2 text-red-700 " +
                                "font-semibold text-sm group-hover:gap-3 transition-all'>" +
                                "Read More " +
                                "<span class='iconify w-4 h-4' data-icon='lucide:arrow-right'></span>" +
                                "</span>";

                RelatedPosts += "</div>";
                RelatedPosts += "</a>";
                RelatedPosts += "</div>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "BindRelatedPosts", ex.Message);
        }
    }
    private void BindCategories()
    {
        try
        {
            List<Blogs> all = Blogs.GetAllBlog(conT);

            // Build a dictionary of tag -> count
            System.Collections.Generic.Dictionary<string, int> tagCount =
                new System.Collections.Generic.Dictionary<string, int>(
                    StringComparer.OrdinalIgnoreCase);

            foreach (Blogs b in all)
            {
                if (string.IsNullOrWhiteSpace(b.BlogTags)) continue;

                string[] tags = b.BlogTags.Split(new char[] { ',', ';' },
                                                  StringSplitOptions.RemoveEmptyEntries);
                foreach (string tag in tags)
                {
                    string t = tag.Trim();
                    if (string.IsNullOrEmpty(t)) continue;

                    if (tagCount.ContainsKey(t))
                        tagCount[t] = tagCount[t] + 1;
                    else
                        tagCount.Add(t, 1);
                }
            }

            foreach (var kv in tagCount
                      .OrderByDescending(x => x.Value)
                      .Take(3))
            {
                Categories += "<li>";
                Categories += "<div class='flex justify-between items-center text-slate-600 text-sm'>";
                Categories += "<span>" + HttpUtility.HtmlEncode(kv.Key) + "</span>";
                Categories += "<span class='bg-slate-100 px-2 py-0.5 rounded text-xs text-slate-500'>" +
                              kv.Value.ToString("D2") + "</span>";
                Categories += "</div>";
                Categories += "</li>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "BindCategories", ex.Message);
        }
    }
}