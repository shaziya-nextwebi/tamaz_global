using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public string strBannerHtml = "", strBrandSlider = "", strSpotlight = "", strHomeProducts = "", strSpotlightMobile = "", strMobileBannerHtml = "";
    public string strTopCategories = "";
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBannerSlides();
            BindBrandSlider();
            BindSpotlight();
            BindHomeProducts();
            BindTopCategories();
        }
    }
    private void BindBannerSlides()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            var banners = AddBannerImages.GetBannerImage(con);

            foreach (var item in banners)
            {
                strBannerHtml += @"
            <div class='swiper-slide'>
                <div class='max-w-7xl mx-auto px-4 py-12 slide-banner'>
                    <div class='grid lg:grid-cols-2 gap-8 items-center'>
                        <div class='hero-content fade-in'>
                            <div class='hero-badge'>
                                <svg width='14' height='14' viewBox='0 0 24 24' fill='currentColor'><polygon points='12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2' /></svg>
                                Premium Quality
                            </div>
                            <h1 class='hero-title'>" + item.BannerTitle + @"</h1>
                            <p class='hero-desc'>Discover premium health and wellness products.</p>
                            <a href='" + (item.Link != "" ? item.Link : "Category.aspx") + @"' class='btn-primary'>Shop Now 
                                <svg width='18' height='18' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2'>
                                    <line x1='5' y1='12' x2='19' y2='12' />
                                    <polyline points='12 5 19 12 12 19' />
                                </svg>
                            </a>
                        </div>
                    </div>
                </div>

                <img src='" + item.DeskImage + @"' alt='" + item.BannerTitle + @"' class='banner-desktop-img' />

                <img src='" + item.MobImage + @"' alt='" + item.BannerTitle + @"' class='banner-mob-img' />
            </div>";

                string link = item.Link != "" ? item.Link : "Product.aspx";
                strMobileBannerHtml += @"
        <div class='swiper-slide'>
            <a href='" + link + @"'>
                <img src='" + item.MobImage + @"' alt='" + item.BannerTitle + @"' />
            </a>
        </div>";
            }
        }
        catch (Exception ex)
        {
            // handle silently
        }
    }
    public void BindBrandSlider()
    {
        try
        {
            SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            List<Brand> brands = Brand.GetAllBrand(conT);
            strBrandSlider = "";
            foreach (Brand b in brands)
            {
                string img = b.BannerImage != "" ? "/" + b.BannerImage : "/assests/Images/clients/placeholder.png";
                strBrandSlider += @"
<div class='client-card'>
    <a href='/Brand/" + b.BrandUrl + @"' class='client-logo'>
        <img src='" + img + @"' alt='" + b.BrandName + @"' title='" + b.BrandName + @"' />
    </a>
</div>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindBrandSlider", ex.Message);
        }
    }
    public void BindSpotlight()
    {
        try
        {
            SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            List<ProductDetails> products = ProductDetails.GetSpotlightProducts(conT);
            strSpotlight = "";

            // label badge colors — cycle through these
            string[] badgeColors = new string[] { "#FEE2E2", "#E0F2FE", "#DCFCE7" };
            int i = 0;

            foreach (ProductDetails p in products)
            {
                string image = p.SmallImage != "" ? "/" + p.SmallImage : "/assests/Images/placeholder.png";
                string label = p.ProductLabelName != "" ? p.ProductLabelName : "New Arrival";
                string categoryLink = "/Category/" + p.CategoryUrl;
                string color = badgeColors[i % badgeColors.Length];

                strSpotlight += @"
            <div class='group relative h-[450px] br-12 overflow-hidden shadow-lg cursor-pointer spotlight-card'>
                <img src='" + image + @"' alt='" + p.Category + @"' 
                     class='absolute inset-0 w-full h-full object-cover transition-transform duration-700 group-hover:scale-110' />
                <div class='absolute inset-0 bg-gradient-to-t from-black/70 via-black/20 to-transparent overlay-gradient'></div>
                <div class='relative z-10 h-full p-8 p-4-tablet flex flex-col justify-end'>
                    <span class='text-sm font-bold uppercase tracking-wider mb-2' style='color:" + color + @";'>" + label + @"</span>
                    <h3 class='text-white text-3xl font-bold mb-3 leading-tight'>" + p.Category + @"</h3>
                    <a href='" + categoryLink + @"' class='inline-flex items-center gap-2 bg-white text-[#0F172A] px-6 py-3 rounded-lg font-semibold text-sm hover:bg-gray-100 transition-colors self-start spotlight-button'>
                        Shop Now
                        <svg width='16' height='16' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2'>
                            <line x1='5' y1='12' x2='19' y2='12' />
                            <polyline points='12 5 19 12 12 19' />
                        </svg>
                    </a>
                </div>
            </div>";
                strSpotlightMobile += @"
<div class='swiper-slide'>
    <div class='relative h-[420px] rounded-2xl overflow-hidden shadow-lg spotlight-card'>
        <img src='" + image + @"' class='absolute inset-0 w-full h-full object-cover' />
        <div class='absolute inset-0 bg-gradient-to-t from-black/70 via-black/20 to-transparent overlay-gradient'></div>
        <div class='relative z-10 h-full p-6 flex flex-col justify-end'>
            <span class='spotlight-bridge text-sm font-bold mb-2' style='color:" + color + @";'>" + label + @"</span>
            <h3 class='text-white text-2xl font-bold mb-3'>" + p.Category + @"</h3>
            <a href='" + categoryLink + @"' class='bg-white px-5 py-2 rounded-lg font-semibold text-sm w-fit spotlight-btn'>Shop Now</a>
        </div>
    </div>
</div>";
                i++;
            }

            // fallback if no products found
            if (strSpotlight == "")
            {
                strSpotlight = "<p class='text-gray-400 col-span-3 text-center'>No spotlight products found.</p>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindSpotlight", ex.Message);
        }
    }
    public void BindHomeProducts()
    {

        try
        {
            SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            List<ProductDetails> products = ProductDetails.GetAllProductstop8(conT);

            // Get current cart product IDs to check which are already in cart
            List<AddtoCart> cartItems = AddtoCart.GetAllcartproducts(conT);
            List<int> cartProductIds = new List<int>();

            foreach (var c in cartItems) cartProductIds.Add(c.ProductId);

            strHomeProducts = "";

            foreach (ProductDetails p in products)
            {
                string image = p.SmallImage != "" ? "/" + p.SmallImage : "/assests/Images/placeholder.png";
                string label = p.ProductLabelName != "" ? p.ProductLabelName : "";
                string url = p.ProductUrl != "" ? "/Product/" + p.ProductUrl : "#";
                string name = p.ProductName.Replace("'", "&#39;").Replace("\"", "&quot;");
                string retailPrice = p.RetailPrice != null ? p.RetailPrice.Trim() : "";

                string price;
                if (retailPrice == "" || retailPrice == "0" || retailPrice == "0.00")
                {
                    price = "<a href='/ContactUs.aspx' class='contact-us-link' " +
                            "style='color:#162e7d; font-weight:600; font-size:15px; text-decoration:none;' " +
                            "onclick='event.stopPropagation();'>Contact Us</a>";
                }
                else
                {
                    price = "<span class='current-price'>" +
                            "<i class='fa-solid fa-indian-rupee-sign pe-1 fs-16'></i>" +
                            retailPrice + "</span>";
                }

                string badge = label != ""
                    ? "<span class='product-badge'>" + label + "</span>"
                    : "";

                bool inCart = cartProductIds.Contains(p.Id);

                string btnHtml;
                if (inCart)
                {
                    btnHtml = "<a href='/Cart.aspx' class=' add-cart-btn view-cart-btn' " +
                              "onclick='event.stopPropagation();' " +
                              "style='display:block; text-align:center; text-decoration:none;'>View Cart</a>";
                }
                else
                {
                    btnHtml = "<button class='add-cart-btn ' " +
                              "onclick=\"event.stopPropagation(); event.preventDefault(); addToCart(this, " + p.Id + ", event);\">" +
                              "Add to Cart</button>";
                }
                strHomeProducts +=
                    "<div class='product-card fade-in' style='cursor:pointer;' " +
                        "onclick=\"window.location='" + url + "'\">" +
                        "<div class='product-image'>" +
                            badge +
                            "<img src='" + image + "' alt='" + name + "' loading='lazy' />" +
                        "</div>" +
                        "<div class='product-info'>" +
                            "<h3 class='product-name'>" + p.ProductName + "</h3>" +
                            "<div class='product-price'>" + price + "</div>" +
                        btnHtml +
                        "</div>" +
                    "</div>";
            }

            if (strHomeProducts == "")
            {
                strHomeProducts = "<p class='text-gray-400 col-span-4 text-center py-10'>No products found.</p>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindHomeProducts", ex.Message);
        }
    }
    public void BindTopCategories()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            List<global::Category> cats = global::Category.GetTopCategory(con);
            //List<global::Category> cats = global::Category.GetAllCategory(con);

            strTopCategories = "";

            foreach (var c in cats)
            {
                string img = !string.IsNullOrEmpty(c.BannerImage)
                    ? "/" + c.BannerImage
                    : "/assests/Images/category-img/placeholder.png";

                strTopCategories += @"
            <a href='/Category/" + c.CategoryUrl + @"' class='flex-shrink-0 w-[140px] md:w-[160px] group block category-box'>
                <div class='relative rounded-2xl overflow-hidden mb-3 bg-gray-50 transition-all duration-300 group-hover:shadow-xl'>
                    <img src='" + img + @"' alt='" + c.CategoryName + @"' 
                        class='w-full h-[140px] md:h-[160px] object-cover transition-transform duration-500 group-hover:scale-110 category-img' />
                </div>
                <h3 class='text-sm font-semibold text-[#0F172A] text-center'>" + c.CategoryName + @"</h3>
            </a>";
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindTopCategories", ex.Message);
        }
    }

    [WebMethod]
    public static object AddToCart(string productId, int qty = 1)
    {
        try
        {
            SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            string uid = HttpContext.Current.Request.Cookies["t_new_vi"] != null
                ? CommonModel.Decrypt(HttpContext.Current.Request.Cookies["t_new_vi"].Value)
                : "";

            if (uid == "")
            {
                uid = Guid.NewGuid().ToString();
                HttpCookie cookie = new HttpCookie("t_new_vi", CommonModel.Encrypt(uid));
                cookie.Expires = DateTime.Now.AddDays(30);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }

            int pid = Convert.ToInt32(productId);

            var existing = AddtoCart.GetUserArpcartByUid(conT, uid, productId).FirstOrDefault();

            if (existing != null)
            {
                // ✅ Update qty if already in cart
                existing.Qty = qty;
                AddtoCart.Updatecartdetails(conT, existing);
                return new { success = true, inCart = true, cartCount = AddtoCart.GetcartlistQunatity(conT) };
            }

            AddtoCart cart = new AddtoCart
            {
                ProductId = pid,
                Qty = qty,  // ✅ use passed qty
                Userguid = uid
            };

            int result = AddtoCart.Insertcartdetails(conT, cart);
            string count = AddtoCart.GetcartlistQunatity(conT);

            return new { success = result > 0, inCart = true, cartCount = count };
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException("Default.aspx", "AddToCart", ex.Message);
            return new { success = false, inCart = false };
        }
    }

    [WebMethod]
    public static object SearchProducts(string query)
    {
        try
        {
            SqlConnection conT = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            List<object> results = new List<object>();

            using (SqlCommand cmd = new SqlCommand(
                @"SELECT TOP 15 ProductName, SmallImage, ProductUrl 
              FROM ProductDetails 
              WHERE Status != 'Deleted' 
              AND ProductName LIKE @q 
              ORDER BY ProductName ASC", conT))
            {
                cmd.Parameters.AddWithValue("@q", "%" + query + "%");
                conT.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        results.Add(new
                        {
                            name = dr["ProductName"].ToString(),
                            img = "/" + dr["SmallImage"].ToString(),
                            url = "/Product/" + dr["ProductUrl"].ToString()
                        });
                    }
                }
                conT.Close();
            }
            return results;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException("Search", "SearchProducts", ex.Message);
            return new List<object>();
        }
    }
}