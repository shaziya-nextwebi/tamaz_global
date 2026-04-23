using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
public partial class CategoryPage : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strCategoryName = "";
    public string strPageTitle = "";
    public string strMetaDesc = "";
    public string strMetaKeys = "";
    public string strFullDesc = "";
    public string strShortDesc = "";
    public string strBreadcrumb = "";
    public string strProducts = "";
    public string strPagination = "";
    public string strCategoryUrl = "";
    public int currentPage = 1;
    public int totalProducts = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Read categoryurl from route
        string categoryUrl = Page.RouteData.Values["categoryurl"] != null
                             ? Page.RouteData.Values["categoryurl"].ToString()
                             : "";

        if (categoryUrl == "")
        {
            Response.Redirect("/404");
            return;
        }

        strCategoryUrl = categoryUrl;

        // Get page number from querystring
        int.TryParse(Request.QueryString["page"], out currentPage);
        if (currentPage < 1) currentPage = 1;

        if (!IsPostBack)
        {
            LoadCategoryDetails(categoryUrl);
            LoadProducts(categoryUrl, currentPage);
        }
    }

    public void LoadCategoryDetails(string categoryUrl)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            List<global::Category> cats = global::Category.GetAllCategory(con);
            global::Category cat = cats.FirstOrDefault(x => x.CategoryUrl.ToLower() == categoryUrl.ToLower());

            if (cat == null)
            {
                Response.Redirect("/404");
                return;
            }

            strCategoryName = cat.CategoryName;
            strPageTitle = cat.PageTitle != "" ? cat.PageTitle : cat.CategoryName + " - TAMAZ Global";
            strMetaDesc = cat.MetaDesc;
            strMetaKeys = cat.MetaKeys;
            strFullDesc = cat.FullDesc;
            strShortDesc = cat.ShortDesc != "" ? cat.ShortDesc : "Browse our premium collection.";

            // Breadcrumb
            strBreadcrumb = "<a href='/' class='hover:text-[#B91C1C] transition-colors'>Home</a>" +
                            "<span class='mx-2'>/</span>" +
                            "<a href='' class='hover:text-[#B91C1C] transition-colors'>Category</a>" +
                            "<span class='mx-2'>/</span>" +
                            "<span class='text-[#0F172A] font-medium'>" + cat.CategoryName + "</span>";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "LoadCategoryDetails", ex.Message);
        }
    }

    public void LoadProducts(string categoryUrl, int pageNo)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            List<ProductDetails> products = ProductDetails.GetAllProductsBasedOnCategory(con, categoryUrl, pageNo);

            strProducts = "";

            if (products.Count == 0)
            {
                strProducts = "<p class='text-gray-400 col-span-4 text-center py-10'>No products found in this category.</p>";
                return;
            }

            totalProducts = products[0].ttlProduct;

            foreach (ProductDetails p in products)
            {
                string image = p.SmallImage != "" ? "/" + p.SmallImage : "/assests/Images/placeholder.png";
                string name = p.ProductName.Replace("'", "&#39;").Replace("\"", "&quot;");
                string url = "/Product/" + p.ProductUrl;
                string retailPrice = p.RetailPrice != null ? p.RetailPrice.Trim() : "";
                string label = p.ProductLabelName != "" ? p.ProductLabelName : "";

                string badge = label != ""
                    ? "<span class='product-badge'>" + label + "</span>"
                    : "";

                string price = (retailPrice == "" || retailPrice == "0" || retailPrice == "0.00")
                    ? "<a href='/ContactUs.aspx' class='contact-us-link' style='color:#162e7d; font-weight:600; font-size:15px; text-decoration:none;' onclick='event.stopPropagation();'>Contact Us</a>"
                    : "<span class='current-price'><i class='fa-solid fa-indian-rupee-sign pe-1 fs-16'></i>" + retailPrice + "</span>";

                strProducts +=
                    "<div class='product-card' style='cursor:pointer;' onclick=\"window.location='" + url + "'\">" +
                        "<div class='product-image'>" +
                            badge +
                            "<button class='wishlist-btn' onclick='event.stopPropagation();'>" +
                                "<svg width='18' height='18' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2'>" +
                                    "<path d='M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z' />" +
                                "</svg>" +
                            "</button>" +
                            "<img src='" + image + "' alt='" + name + "' loading='lazy' />" +
                        "</div>" +
                        "<div class='product-info'>" +
                            "<h3 class='product-name'>" + p.ProductName + "</h3>" +
                            "<div class='product-price'>" + price + "</div>" +
                           "<button class='add-cart-btn' onclick='event.stopPropagation(); event.preventDefault(); addToCart(" + p.Id + ", this); return false;'>Add to Cart</button>" +
                        "</div>" +
                    "</div>";
            }

            // Pagination
            BuildPagination(categoryUrl, pageNo, totalProducts);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "LoadProducts", ex.Message);
        }
    }

    public void BuildPagination(string categoryUrl, int currentPg, int total)
    {
        int totalPages = (int)Math.Ceiling((double)total / 20);
        if (totalPages <= 1)
        {
            strPagination = "";
            return;
        }

        string baseUrl = "/Category/" + categoryUrl + "?page=";
        strPagination = "";

        // Prev button
        string prevDisabled = currentPg == 1 ? "opacity-50 pointer-events-none" : "";
        strPagination += "<a href='" + baseUrl + (currentPg - 1) + "' class='pagination-btn pagination-prev " + prevDisabled + "'>" +
                         "<svg width='16' height='16' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2'><polyline points='15 18 9 12 15 6'/></svg>Prev</a>";

        strPagination += "<div class='flex items-center gap-2'>";

        for (int i = 1; i <= totalPages; i++)
        {
            if (i == 1 || i == totalPages || (i >= currentPg - 1 && i <= currentPg + 1))
            {
                string active = i == currentPg ? "active" : "";
                strPagination += "<a href='" + baseUrl + i + "' class='pagination-num " + active + "'>" + i + "</a>";
            }
            else if (i == currentPg - 2 || i == currentPg + 2)
            {
                strPagination += "<span class='pagination-dots'>...</span>";
            }
        }

        strPagination += "</div>";

        // Next button
        string nextDisabled = currentPg == totalPages ? "opacity-50 pointer-events-none" : "";
        strPagination += "<a href='" + baseUrl + (currentPg + 1) + "' class='pagination-btn pagination-next " + nextDisabled + "'>" +
                         "Next<svg width='16' height='16' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2'><polyline points='9 18 15 12 9 6'/></svg></a>";
    }
    [WebMethod]
    public static string AddToCart(int productId)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            string uid = HttpContext.Current.Request.Cookies["t_new_vi"] != null
                         ? HttpContext.Current.Request.Cookies["t_new_vi"].Value : "";

            if (uid == "")
            {
                // Generate a new guest guid and set cookie
                uid = Guid.NewGuid().ToString();
                string encUid = CommonModel.Encrypt(uid);
                HttpCookie cookie = new HttpCookie("t_new_vi", encUid);
                cookie.Expires = DateTime.Now.AddDays(30);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                uid = CommonModel.Decrypt(uid);
            }

            // Check if already in cart — update qty, else insert
            List<AddtoCart> existing = AddtoCart.GetUserArpcartByUid(conT, uid, productId.ToString());

            if (existing.Count > 0)
            {
                AddtoCart update = new AddtoCart
                {
                    ProductId = productId,
                    Userguid = uid,
                    Qty = existing[0].Qty + 1
                };
                AddtoCart.Updatecartdetails(conT, update);
            }
            else
            {
                AddtoCart insert = new AddtoCart
                {
                    ProductId = productId,
                    Userguid = uid,
                    Qty = 1
                };
                AddtoCart.Insertcartdetails(conT, insert);
            }

            // Return updated cart count
            string count = AddtoCart.GetcartlistQunatity(conT);
            return count == "" ? "0" : count;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "AddToCart", ex.Message);
            return "-1";
        }
    }
}