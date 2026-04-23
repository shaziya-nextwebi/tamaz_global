<%@ Page Title="TAMAZ Global - Premium Wellness Products" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
        .view-cart-btn {
            background: #e97c13 !important;
          
        }

            .view-cart-btn:hover {
                background: #0f1f57;
            }
        /* Desktop: show desktop image, hide mobile */
        .banner-desktop-img {
            display: block;
            position: absolute;
            inset: 0;
            width: 100%;
            height: 100%;
            object-fit: cover;
            z-index: 0;
        }

        .banner-mob-img {
            display: none;
            position: absolute;
            inset: 0;
            width: 100%;
            height: 100%;
            object-fit: cover;
            z-index: 0;
        }

        /* Mobile: flip visibility */
        @media (max-width: 768px) {
            .banner-desktop-img {
                display: none;
            }

            .banner-mob-img {
                display: block;
            }
        }

        /* Keep slide content above the background image */
        .swiper-slide {
            position: relative;
        }

            .swiper-slide .max-w-7xl {
                position: relative;
                z-index: 1;
                width:100%;
            }
      


            .MobileHeroSlider {
    width: 100%;
    height: auto; 
   
    display:none;
}

.MobileHeroSlider .swiper-slide {
    display: flex;
    align-items: center;
    justify-content: center;
}

/* 👇 Important */
.MobileHeroSlider img {
    width: 100%;
    height: 100%;
    object-fit: contain;   /* ✅ FULL image visible */
}

/* Pagination */
.mobile-hero-pagination {
    bottom: 0px !important;
}
      @media(max-width:576px)
      {
          .desktop-hero-banner
          {
              display:none;
          }
          .MobileHeroSlider{
              display:block;
          }

      }





    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- ===================== HERO BANNER ===================== -->
    <%--    <section class="hero-banner" id="hero">
        <div class="swiper myHeroSlider">
            <div class="swiper-wrapper">

                <!-- Slide 1 -->
                <div class="swiper-slide slide-1">
                    <div class="max-w-7xl mx-auto px-4 py-12 slide-banner">
                        <div class="grid lg:grid-cols-2 gap-8 items-center">
                            <div class="hero-content fade-in">
                                <div class="hero-badge">
                                    <svg width="14" height="14" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2" /></svg>
                                    Premium Quality
                                </div>
                                <h1 class="hero-title"><span class="tag-text">Best Source Of</span><br />Skin Whitening Capsule</h1>
                                <p class="hero-desc">Discover premium health and wellness products.</p>
                                <a href="Category.aspx" class="btn-primary">Shop Now <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="5" y1="12" x2="19" y2="12" /><polyline points="12 5 19 12 12 19" /></svg></a>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Slide 2 -->
                <div class="swiper-slide slide-2">
                    <div class="max-w-7xl mx-auto px-4 py-12 slide-banner">
                        <div class="grid lg:grid-cols-2 gap-8 items-center">
                            <div class="hero-content fade-in">
                                <div class="hero-badge">
                                    <svg width="14" height="14" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2" /></svg>
                                    Premium Quality
                                </div>
                                <h1 class="hero-title"><span class="tag-text">Best Source Of</span><br />Skin Whitening Products</h1>
                                <p class="hero-desc">Discover premium health and wellness products.</p>
                                <a href="Category.aspx" class="btn-primary">Shop Now <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="5" y1="12" x2="19" y2="12" /><polyline points="12 5 19 12 12 19" /></svg></a>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Slide 3 -->
                <div class="swiper-slide slide-3">
                    <div class="max-w-7xl mx-auto px-4 py-12 slide-banner">
                        <div class="grid lg:grid-cols-2 gap-8 items-center">
                            <div class="hero-content fade-in">
                                <div class="hero-badge">
                                    <svg width="14" height="14" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2" /></svg>
                                    Premium Quality
                                </div>
                                <h1 class="hero-title"><span class="tag-text">Best Source Of</span><br />Skin Whitening Injections</h1>
                                <p class="hero-desc">Discover premium health and wellness products.</p>
                                <a href="Category.aspx" class="btn-primary">Shop Now <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="5" y1="12" x2="19" y2="12" /><polyline points="12 5 19 12 12 19" /></svg></a>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Slide 4 -->
                <div class="swiper-slide slide-4">
                    <div class="max-w-7xl mx-auto px-4 py-12 slide-banner">
                        <div class="grid lg:grid-cols-2 gap-8 items-center">
                            <div class="hero-content fade-in">
                                <div class="hero-badge">
                                    <svg width="14" height="14" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2" /></svg>
                                    Premium Quality
                                </div>
                                <h1 class="hero-title"><span class="tag-text">Best Source Of</span><br />Skin Whitening Soaps</h1>
                                <p class="hero-desc">Discover premium health and wellness products.</p>
                                <a href="Category.aspx" class="btn-primary">Shop Now <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="5" y1="12" x2="19" y2="12" /><polyline points="12 5 19 12 12 19" /></svg></a>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Slide 5 -->
                <div class="swiper-slide slide-5">
                    <div class="max-w-7xl mx-auto px-4 py-12 slide-banner">
                        <div class="grid lg:grid-cols-2 gap-8 items-center">
                            <div class="hero-content fade-in">
                                <div class="hero-badge">
                                    <svg width="14" height="14" viewBox="0 0 24 24" fill="currentColor"><polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2" /></svg>
                                    Premium Quality
                                </div>
                                <h1 class="hero-title"><span class="tag-text">Best Source Of</span><br />Weight Gain Capsules</h1>
                                <p class="hero-desc">Discover premium health and wellness products.</p>
                                <a href="Category.aspx" class="btn-primary">Shop Now <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="5" y1="12" x2="19" y2="12" /><polyline points="12 5 19 12 12 19" /></svg></a>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="swiper-button-next"></div>
            <div class="swiper-button-prev"></div>
        </div>
    </section>--%>
    <section class="hero-banner desktop-hero-banner" id="hero">
        <div class="swiper myHeroSlider">
            <div class="swiper-wrapper">
                <%=strBannerHtml %>
            </div>
            <div class="swiper-button-next"></div>
            <div class="swiper-button-prev"></div>
        </div>
    </section>

 <%--   //this is the banner section for Mobile--%>
<section class="hero-banner mobile-hero-banner swiper-container MobileHeroSlider">

    <div class="swiper-wrapper">

        <div class="swiper-slide">
            <a href="Product.aspx">
                <img src="assests/Images/mobile-banner-images/1.png" />
            </a>
        </div>

        <div class="swiper-slide">
            <a href="Product.aspx">
                <img src="assests/Images/mobile-banner-images/2.png" />
            </a>
        </div>

        <div class="swiper-slide">
            <a href="Product.aspx">
                <img src="assests/Images/mobile-banner-images/3.png" />
            </a>
        </div>

        <div class="swiper-slide">
            <a href="Product.aspx">
                <img src="assests/Images/mobile-banner-images/4.png" />
            </a>
        </div>

        <div class="swiper-slide">
            <a href="Product.aspx">
                <img src="assests/Images/mobile-banner-images/5.png" />
            </a>
        </div>

    </div>

    <div class="swiper-pagination mobile-hero-pagination"></div>

</section>

    <!-- ===================== FEATURES ===================== -->
    <section class="features-section">
        <div class="max-w-7xl mx-auto px-4">
            <div class="grid grid-cols-2 md:grid-cols-4 gap-4">
                <div class="feature-item fade-in">
                    <div class="feature-icon bg-linear-gradient">
                        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" class="text-white">
                            <rect x="1" y="3" width="15" height="13" />
                            <polygon points="16 8 20 8 23 11 23 16 16 16 16 8" />
                            <circle cx="5.5" cy="18.5" r="2.5" />
                            <circle cx="18.5" cy="18.5" r="2.5" />
                        </svg>
                    </div>
                    <div>
                        <div class="feature-title">Free Shipping</div>
                        <div class="feature-desc">On orders over $150</div>
                    </div>
                </div>
                <div class="feature-item fade-in">
                    <div class="feature-icon bg-linear-gradient">
                        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" class="text-white">
                            <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z" />
                        </svg>
                    </div>
                    <div>
                        <div class="feature-title">Secure Payment</div>
                        <div class="feature-desc">100% safe transactions</div>
                    </div>
                </div>
                <div class="feature-item fade-in">
                    <div class="feature-icon bg-linear-gradient">
                        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" class="text-white">
                            <circle cx="12" cy="12" r="10" />
                            <path d="M16 8h-6a2 2 0 1 0 0 4h4a2 2 0 1 1 0 4H8" />
                            <path d="M12 18V6" />
                        </svg>
                    </div>
                    <div>
                        <div class="feature-title">Money Back</div>
                        <div class="feature-desc">30-day guarantee</div>
                    </div>
                </div>
                <div class="feature-item fade-in">
                    <div class="feature-icon bg-linear-gradient">
                        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" class="text-white">
                            <path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2z" />
                        </svg>
                    </div>
                    <div>
                        <div class="feature-title">Expert Support</div>
                        <div class="feature-desc">Talk to specialists</div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- ===================== CATEGORIES ===================== -->
    <section class="w-full bg-white py-12 border-t border-gray-100 section-padding">
        <div class="w-full px-4 md:px-8">
            <div class="flex items-start md:items-center justify-start md:justify-center mb-8 sm:mb-4 px-2">
                <div class="text-start md:text-center">
                    <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] section-title">Our Top Categories</h2>
                    <p class="text-[#64748B] mt-1 section-description">Browse our premium wellness collection</p>
                </div>
            </div>
            <div class="flex justify-center gap-5 overflow-x-auto pb-4 scrollbar-hide -mx-4 px-4 md:mx-0 md:px-0 category-scroll-container">
                <%=strTopCategories %>
                <%--<a href="Category.aspx" class="flex-shrink-0 w-[140px] md:w-[160px] group block category-box">
                    <div class="relative rounded-2xl overflow-hidden mb-3 bg-gray-50 transition-all duration-300 group-hover:shadow-xl">
                        <img src="assests/Images/category-img/1.png" alt="Glutathione Injections" class="w-full h-[140px] md:h-[160px] object-cover transition-transform duration-500 group-hover:scale-110 category-img" />
                    </div>
                    <h3 class="text-sm font-semibold text-[#0F172A] text-center">Glutathione Injections</h3>
                </a>
                <a href="Category.aspx" class="flex-shrink-0 w-[140px] md:w-[160px] group block category-box">
                    <div class="relative rounded-2xl overflow-hidden mb-3 bg-gray-50 transition-all duration-300 group-hover:shadow-xl">
                        <img src="assests/Images/category-img/2.png" alt="Other Injections" class="w-full h-[140px] md:h-[160px] object-cover transition-transform duration-500 group-hover:scale-110 category-img" />
                    </div>
                    <h3 class="text-sm font-semibold text-[#0F172A] text-center">Other Injections</h3>
                </a>
                <a href="Category.aspx" class="flex-shrink-0 w-[140px] md:w-[160px] group block category-box">
                    <div class="relative rounded-2xl overflow-hidden mb-3 bg-gray-50 transition-all duration-300 group-hover:shadow-xl">
                        <img src="assests/Images/category-img/3.png" alt="Skin Whitening Pills" class="w-full h-[140px] md:h-[160px] object-cover transition-transform duration-500 group-hover:scale-110 category-img" />
                    </div>
                    <h3 class="text-sm font-semibold text-[#0F172A] text-center">Skin Whitening Pills</h3>
                </a>
                <a href="Category.aspx" class="flex-shrink-0 w-[140px] md:w-[160px] group block category-box">
                    <div class="relative rounded-2xl overflow-hidden mb-3 bg-gray-50 transition-all duration-300 group-hover:shadow-xl">
                        <img src="assests/Images/category-img/4.png" alt="Whitening Cream" class="w-full h-[140px] md:h-[160px] object-cover transition-transform duration-500 group-hover:scale-110 category-img" />
                    </div>
                    <h3 class="text-sm font-semibold text-[#0F172A] text-center">Whitening Cream</h3>
                </a>
                <a href="Category.aspx" class="flex-shrink-0 w-[140px] md:w-[160px] group block category-box">
                    <div class="relative rounded-2xl overflow-hidden mb-3 bg-gray-50 transition-all duration-300 group-hover:shadow-xl">
                        <img src="assests/Images/category-img/5.png" alt="Whitening Soap" class="w-full h-[140px] md:h-[160px] object-cover transition-transform duration-500 group-hover:scale-110 category-img" />
                    </div>
                    <h3 class="text-sm font-semibold text-[#0F172A] text-center">Whitening Soap</h3>
                </a>
                <a href="#" class="flex-shrink-0 w-[140px] md:w-[160px] group block category-box">
                    <div class="relative rounded-2xl overflow-hidden mb-3 bg-gray-50 transition-all duration-300 group-hover:shadow-xl">
                        <img src="assests/Images/category-img/6.png" alt="Whitening Lotions" class="w-full h-[140px] md:h-[160px] object-cover transition-transform duration-500 group-hover:scale-110 category-img" />
                    </div>
                    <h3 class="text-sm font-semibold text-[#0F172A] text-center">Whitening Lotions</h3>
                </a>
                <a href="Category.aspx" class="flex-shrink-0 w-[140px] md:w-[160px] group block category-box">
                    <div class="relative rounded-2xl overflow-hidden mb-3 bg-gray-50 transition-all duration-300 group-hover:shadow-xl">
                        <img src="assests/Images/category-img/7.png" alt="Skin Whitening Products" class="w-full h-[140px] md:h-[160px] object-cover transition-transform duration-500 group-hover:scale-110 category-img" />
                    </div>
                    <h3 class="text-sm font-semibold text-[#0F172A] text-center">Skin Whitening Products</h3>
                </a>
                <a href="Category.aspx" class="flex-shrink-0 w-[140px] md:w-[160px] group block category-box">
                    <div class="relative rounded-2xl overflow-hidden mb-3 bg-gray-50 transition-all duration-300 group-hover:shadow-xl">
                        <img src="assests/Images/category-img/8.png" alt="Weight Gain/Loss" class="w-full h-[140px] md:h-[160px] object-cover transition-transform duration-500 group-hover:scale-110 category-img" />
                    </div>
                    <h3 class="text-sm font-semibold text-[#0F172A] text-center">Weight Gain/Loss</h3>
                </a>--%>
            </div>
        </div>
    </section>

    <!-- ===================== ABOUT US ===================== -->
    <section class="section-padding bg-sky-50 about-section bg-white overflow-hidden">
        <div class="max-w-7xl mx-auto px-4 md:px-8">
            <div class="grid lg:grid-cols-2 gap-4 md:gap-10 lg:gap-12 items-center">
                <div class="relative">
                    <img src="assests/Images/about.png" alt="About Us Image" class="w-full rounded-lg object-cover about-us-image fade-in" />
                </div>
                <div class="lg:pl-8 about-us-content">
                    <span class="inline-block text-xs md:text-sm font-bold text-[#1E3A8A] uppercase tracking-wider mb-4 relative">DEDICATED TO QUALITY
                       
                        <span class="absolute bottom-0 left-0 w-12 h-0.5 bg-[#B91C1C] -mb-2"></span>
                    </span>
                    <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] section-title">Your Trusted Global Trading Partner in <span class="">Wellness</span>
                    </h2>
                    <p class="text-muted text-base leading-relaxed mb-4 mt-1">
                        At Tamaz Global Trading, we source premium health and wellness products from world-renowned laboratories. We are committed to providing safe, effective, and innovative solutions to enhance your well-being.
                   
                    </p>
                    <ul class="space-y-4 mb-10">
                        <li class="flex items-start gap-4">
                            <div class="feature-icon">
                                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" width="24" height="24">
                                    <path d="M9 12l2 2 4-4" />
                                    <circle cx="12" cy="12" r="10" />
                                </svg>
                            </div>
                            <div>
                                <h4 class="font-semibold text-[#0F172A] text-base mb-1">Direct collaboration with certified laboratories</h4>
                                <p class="text-sm text-[#64748B]">We work directly with top-tier certified labs globally.</p>
                            </div>
                        </li>
                        <li class="flex items-start gap-4">
                            <div class="feature-icon">
                                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                    <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z" />
                                </svg>
                            </div>
                            <div>
                                <h4 class="font-semibold text-[#0F172A] text-base mb-1">Strict quality control and verification</h4>
                                <p class="text-sm text-[#64748B]">Every product undergoes rigorous testing protocols.</p>
                            </div>
                        </li>
                        <li class="flex items-start gap-4">
                            <div class="feature-icon">
                                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                    <circle cx="12" cy="12" r="10" />
                                    <line x1="2" y1="12" x2="22" y2="12" />
                                    <path d="M12 2a15.3 15.3 0 0 1 4 10 15.3 15.3 0 0 1-4 10 15.3 15.3 0 0 1-4-10 15.3 15.3 0 0 1 4-10z" />
                                </svg>
                            </div>
                            <div>
                                <h4 class="font-semibold text-[#0F172A] text-base mb-1">Global cold chain express delivery</h4>
                                <p class="text-sm text-[#64748B]">Safe and fast delivery maintaining product integrity.</p>
                            </div>
                        </li>
                    </ul>
                    <a href="About.aspx" class="inline-flex items-center gap-2 btn-primary text-white">Learn More
                       
                        <svg class="w-4 h-4 group-hover:translate-x-1 transition-transform" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                            <line x1="5" y1="12" x2="19" y2="12" />
                            <polyline points="12 5 19 12 12 19" />
                        </svg>
                    </a>
                </div>
            </div>
        </div>
    </section>

    <section class="py-16 bg-white overflow-hidden spotlight-section show-in-desktop">
        <div class="max-w-7xl mx-auto px-4 md:px-8">
            <div class="flex items-start md:items-center justify-start md:justify-center mb-8 sm:mb-4 px-2">
                <div class="text-start md:text-center">
                    <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] section-title">In The Spotlight</h2>
                    <p class="text-[#64748B] mt-1 section-description">Discover our exclusive deals and trending collections</p>
                </div>
            </div>
            <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
                <%=strSpotlight %>
            </div>
        </div>
    </section>
    <section class="py-16 bg-white overflow-hidden spotlight-section show-in-mobile">
        <div class="max-w-7xl mx-auto px-4 md:px-8">
            <div class="flex items-start md:items-center justify-start md:justify-center mb-8 sm:mb-4 px-2">
                <div class="text-start md:text-center">
                    <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] section-title">In The Spotlight</h2>
                    <p class="text-[#64748B] mt-1 section-description">Discover our exclusive deals and trending collections</p>
                </div>
            </div>
            <div class="md:hidden">
                <div class="swiper spotlightSwiper">
                    <div class="swiper-wrapper">
                        <%=strSpotlightMobile %>
                        <%--                        <div class="swiper-slide">
                            <div class="relative h-[420px] rounded-2xl overflow-hidden shadow-lg spotlight-card">
                                <img src="assests/Images/spotlight-images/sp-1.png" class="absolute inset-0 w-full h-full object-cover" />
                                <div class="absolute inset-0 bg-gradient-to-t from-black/70 via-black/20 to-transparent overlay-gradient"></div>
                                <div class="relative z-10 h-full p-6 flex flex-col justify-end">
                                    <span class="spotlight-bridge text-[#FEE2E2] text-sm font-bold mb-2">30% Off</span>
                                    <h3 class="text-white text-2xl font-bold mb-3">Premium Skin Care</h3>
                                    <a href="Category.aspx" class="bg-white px-5 py-2 rounded-lg font-semibold text-sm w-fit spotlight-btn">Shop Now</a>
                                </div>
                            </div>
                        </div>
                        <div class="swiper-slide">
                            <div class="relative h-[420px] rounded-2xl overflow-hidden shadow-lg spotlight-card">
                                <img src="assests/Images/spotlight-images/sp-2.png" class="absolute inset-0 w-full h-full object-cover" />
                                <div class="absolute inset-0 bg-gradient-to-t from-black/70 via-black/20 to-transparent overlay-gradient"></div>
                                <div class="relative z-10 h-full p-6 flex flex-col justify-end">
                                    <span class="spotlight-bridge text-[#FEE2E2] text-sm font-bold mb-2">New Arrival</span>
                                    <h3 class="text-white text-2xl font-bold mb-3">Whitening Injections</h3>
                                    <a href="Category.aspx" class="bg-white px-5 py-2 rounded-lg font-semibold text-sm w-fit spotlight-btn">Shop Now</a>
                                </div>
                            </div>
                        </div>
                        <div class="swiper-slide">
                            <div class="relative h-[420px] rounded-2xl overflow-hidden shadow-lg spotlight-card">
                                <img src="assests/Images/spotlight-images/sp-3.png" class="absolute inset-0 w-full h-full object-cover" />
                                <div class="absolute inset-0 bg-gradient-to-t from-black/70 via-black/20 to-transparent overlay-gradient"></div>
                                <div class="relative z-10 h-full p-6 flex flex-col justify-end">
                                    <span class="spotlight-bridge text-[#FEE2E2] text-sm font-bold mb-2">Best Seller</span>
                                    <h3 class="text-white text-2xl font-bold mb-3">Health Supplements</h3>
                                    <a href="Category.aspx" class="bg-white px-5 py-2 rounded-lg font-semibold text-sm w-fit spotlight-btn">Shop Now</a>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- ===================== CLIENT SLIDER ===================== -->
    <section class="clients-section">
        <div class="max-w-7xl mx-auto px-4 mb-8 sm:mb-4">
            <div class="text-center fade-in">
                <h2 class="section-title">Our Trusted Partners</h2>
                <p class="section-subtitle section-description mt-1">Working with leading wellness brands worldwide</p>
            </div>
        </div>
        <div class="slider-container">
            <div class="slider-track" id="clientSlider">
                <%=strBrandSlider %>
                <%--                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/1.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/2.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/3.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/4.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/5.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/6.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/7.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/8.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/9.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/10.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/12.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/13.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/14.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/15.jpg" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/16.png" alt="clients" /></span>
                </div>
                <div class="client-card">
                    <span class="client-logo">
                        <img src="assests/Images/clients/17.png" alt="clients" /></span>
                </div>--%>
            </div>
        </div>
    </section>

    <!-- ===================== PRODUCTS ===================== -->
    <section class="products-section">
        <div class="max-w-7xl mx-auto px-4 md:px-8">
            <div class="flex items-center justify-between mb-10 fade-in product-title-wrapper">
                <div>
                    <h2 class="section-title">Tamaz Global Top Products</h2>
                    <p class="section-subtitle section-description mt-1">Top-rated products loved by customers</p>
                </div>
                <a href="Category/glutathione-injections" class="hidden sm:inline-flex btn-primary text-sm py-3 px-6 gap-2">View All
                   
                    <svg class="w-4 h-4" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                        <line x1="5" y1="12" x2="19" y2="12" />
                        <polyline points="12 5 19 12 12 19" />
                    </svg>
                </a>
            </div>
            <div class="grid grid-cols-2 md:grid-cols-4 gap-6 products-grid" id="productsGrid">
                <%=strHomeProducts %>
                <%-- Products rendered by JS below --%>
            </div>
        </div>
    </section>

</asp:Content>

<asp:Content ID="ScriptsContent" ContentPlaceHolderID="scripts" runat="server">
 <script>
     var mobileHeroSlider = new Swiper('.MobileHeroSlider', {
         loop: true,
         //autoplay: {
         //    delay: 3000,
         //    disableOnInteraction: false,
         //},

         pagination: {
             el: '.mobile-hero-pagination',
             clickable: true,
         },

         speed: 600
     });
</script>
    <script>
        // Hero Slider
        new Swiper(".myHeroSlider", {
            loop: true,
            speed: 800,
            effect: "fade",
            navigation: {
                nextEl: ".swiper-button-next",
                prevEl: ".swiper-button-prev",
            }
        });

        // Spotlight Mobile Swiper
        new Swiper(".spotlightSwiper", {
            slidesPerView: 1.2,
            spaceBetween: 16,
        });

        function initSlider() {
            const slider = document.getElementById("clientSlider");
            if (!slider) return;
            slider.innerHTML += slider.innerHTML;
        }

        function initAnimations() {
            if (window.matchMedia("(prefers-reduced-motion: reduce)").matches) {
                document.querySelectorAll(".fade-in").forEach(el => el.classList.add("visible"));
                return;
            }
            const observer = new IntersectionObserver((entries) => {
                entries.forEach((entry, index) => {
                    if (entry.isIntersecting) {
                        setTimeout(() => entry.target.classList.add("visible"), index * 100);
                    }
                });
            }, { threshold: 0.1 });
            document.querySelectorAll(".fade-in").forEach(el => observer.observe(el));
        }

        document.addEventListener("DOMContentLoaded", () => {
            // renderProducts();
            initSlider();
            initAnimations();
        });
        function addToCart(btn, productId, e) {
            if (e) {
                e.stopPropagation();
                e.preventDefault();
            }

            // If already in cart, go to cart page
            if (btn.classList.contains('view-cart-btn')) {
                window.location.href = '/Cart.aspx';
                return;
            }

            btn.disabled = true;
            btn.innerText = 'Adding...';

            fetch('/Default.aspx/AddToCart', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ productId: productId.toString() })
            })
                .then(function (r) { return r.json(); })
                .then(function (data) {
                    var result = data.d || data;
                    if (result.success) {
                        // Replace button with a direct link
                        var a = document.createElement('a');
                        a.href = '/Cart.aspx';
                        a.className = 'view-cart-btn add-cart-btn';
                        a.innerText = 'View Cart';
                        a.style.display = 'block';
                        a.style.textAlign = 'center';
                        a.style.textDecoration = 'none';
                        a.setAttribute('onclick', 'event.stopPropagation();');
                        btn.parentNode.replaceChild(a, btn);
                    } else {
                        btn.disabled = false;
                        btn.innerText = 'Add to Cart';
                    }
                })
                .catch(function () {
                    btn.disabled = false;
                    btn.innerText = 'Add to Cart';
                });
        }
    </script>
</asp:Content>
