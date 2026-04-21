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
    public string strBannerHtml = "";
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        BindBannerSlides();
    }

    private void BindBannerSlides()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            var banners = BannerImages.GetBannerImage(con);

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

                <img src='" + item.DesktopImage + @"' alt='" + item.BannerTitle + @"' class='banner-desktop-img' />

                <img src='" + item.MobImage + @"' alt='" + item.BannerTitle + @"' class='banner-mob-img' />
            </div>";
            }
        }
        catch (Exception ex)
        {
            // handle silently
        }
    }
}