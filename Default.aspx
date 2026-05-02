<%@ Page Title="Tamaz Global Trading Company" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
     <meta name="description" content="Tamaz Global Trading Company is a wholesale distributor of beauty, personal care, and Fitness products." />
 <link rel="canonical" href="https://www.tamazglobal.com/" hreflang="en"/>
    <style>
        .view-cart-btn {
            background: #e51c4c !important;
          
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
                display: block;
            }

         /*   .banner-mob-img {
                display: block;
            }*/
        }

        /* Keep slide content above the background image */
        .swiper-slide {
            position: relative;
        }

            .swiper-slide .max-w-7xl {
                position: relative;
                z-index: 1;
                width: 100%;
            }



        .MobileHeroSlider {
            width: 100%;
            height: auto;
            display: none;
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
                object-fit: contain; /* ✅ FULL image visible */
            }

        /* Pagination */
        .mobile-hero-pagination {
            bottom: 0px !important;
        }


        @media(max-width:576px) {
            .desktop-hero-banner {
                display: none;
            }

            .MobileHeroSlider {
                display: block;
            }

           
            .category-name
            {
                font-size:10px !important;
                line-height:12px !important;
            }
            .margin-bottom-20
            {
                margin-bottom:14px !important;
            }
            .section-title
            {
                        margin-bottom: 0px !important;
            }
                .main-header .logo-image {
        width: 80px;
    }
        }
        .category-swiper {
    width: 100%;
    overflow: hidden;
}
.category-swiper .swiper-slide {
    height: auto;
    min-height:max-content;
    width:max-content;
/*    margin:0px 10px !important;*/
}
        .fs-9 {
            font-size: 9px !important;
        }

        .myFeatureSlider .swiper-slide {
            min-height: auto !important;
            /*width: max-content !important;*/
        }
        .spotlight-button.sp-desktop {
            padding: 4px 8px !important;
            font-size: 12px !important;
        }

        /* ===== SWIPER SLIDE ===== */
.brand-col {
    display: flex !important;
    flex-direction: column !important;
    gap: 12px !important;
}
.swiper-slide.brand-col
{
    min-height:0px !important;
}
/* ===== BRAND CARD ===== */
.brand-wrapper {
    position: relative;
    overflow: hidden;
    border: 1px solid #e8e8e8;
    border-radius: 8px;
    background: #fff;
    transition: box-shadow 0.3s ease, transform 0.3s ease;
}

.brand-wrapper:hover {
    box-shadow: 0 6px 24px rgba(0, 0, 0, 0.10);
    transform: translateY(-3px);
}

.brand-wrapper a {
    display: block;
    padding: 4px 12px;
    text-align: center;
}

.brand-wrapper img {
    max-height: 80px;
    width: auto;
    max-width: 100%;
    object-fit: contain;
    display: block;
    margin: 0 auto;
    transition: transform 0.3s ease;
}

.brand-wrapper:hover img {
    transform: scale(1.05);
}

/* ===== BRAND CAPTION ===== */
.brand-caption {
    background: #f5f5f5;
    padding: 8px 12px;
    text-align: center;
    border-top: 1px solid #e8e8e8;
}

.partner-link {
    font-size: 13px;
    font-weight: 600;
    color: #1a4fba;
    text-decoration: none;
    display: block;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
}

.partner-link:hover {
    color: #e53935;
    text-decoration: none;
}

/* ===== SHINE EFFECT ===== */
.shine-overlay {
    position: relative;
}

.shine {
    position: absolute;
    top: 0;
    left: -75%;
    width: 50%;
    height: 100%;
    background: linear-gradient(
        to right,
        rgba(255,255,255,0) 0%,
        rgba(255,255,255,0.4) 50%,
        rgba(255,255,255,0) 100%
    );
    transform: skewX(-20deg);
    pointer-events: none;
    opacity: 0;
    transition: opacity 0.1s;
}

.brand-wrapper:hover .shine {
    animation: shineAnim 0.6s ease forwards;
}

@keyframes shineAnim {
    0%   { left: -75%; opacity: 1; }
    100% { left: 125%; opacity: 1; }
}

/* ===== SWIPER PAGINATION DOTS ===== */
.brand-pagination {
    margin-top: 20px;
    position: relative !important;
    bottom: auto !important;
}

.brand-pagination .swiper-pagination-bullet {
    width: 10px;
    height: 10px;
    background: #ccc;
    opacity: 1;
}

.brand-pagination .swiper-pagination-bullet-active {
    background: #e53935;
    width: 24px;
    border-radius: 5px;
}

/* ===== RESPONSIVE ===== */
@media (max-width: 767px) {
    .clients-section .section-title {
        font-size: 22px;
    }
    .brand-wrapper img {
        max-height: 80px;
    }
    .partner-link {
        font-size: 12px;
    }
    .brand-slider-outer
    {
        padding:0px 12px 12px ;
    }
        .brand-wrapper:hover {
            box-shadow: 0 6px 24px rgba(0, 0, 0, 0.10);
            transform: translateY(0px);
        }
}

    </style>

</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   
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
            <%=strMobileBannerHtml %>

        </div>

        <div class="swiper-pagination mobile-hero-pagination"></div>

    </section>

    <!-- ===================== FEATURES ===================== -->
  <section class="features-section">
    <div class="max-w-7xl mx-auto px-4">

        <div class="swiper myFeatureSlider">
            <div class="swiper-wrapper">

                <!-- Slide 1 -->
                <div class="swiper-slide">
                    <div class="feature-item">
                       <%-- bg-linear-gradient--%>
                        <div class="feature-icon ">

                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24"
                                viewBox="0 0 24 24" fill="none" class="text-white"
                                stroke="currentColor" stroke-width="2" stroke-linecap="round"
                                stroke-linejoin="round"
                                class="lucide lucide-badge-check-icon lucide-badge-check">
                                <path d="M3.85 8.62a4 4 0 0 1 4.78-4.77 4 4 0 0 1 6.74 0 4 4 0 0 1 4.78 4.78 4 4 0 0 1 0 6.74 4 4 0 0 1-4.77 4.78 4 4 0 0 1-6.75 0 4 4 0 0 1-4.78-4.77 4 4 0 0 1 0-6.76Z" />
                                <path d="m9 12 2 2 4-4" />
                            </svg>

                        </div>
                        <div>
                            <div class="feature-title">Genuine Products</div>
                            <div class="feature-desc">Trusted Original Product</div>
                        </div>
                    </div>
                </div>

                <!-- Slide 2 -->
                <div class="swiper-slide">
                    <div class="feature-item">
                        <div class="feature-icon ">

                            <svg width="24" height="24" viewBox="0 0 24 24" fill="none"
                                stroke="currentColor" stroke-width="2" class="text-white">
                                <rect x="1" y="3" width="15" height="13" />
                                <polygon points="16 8 20 8 23 11 23 16 16 16 16 8" />
                                <circle cx="5.5" cy="18.5" r="2.5" />
                                <circle cx="18.5" cy="18.5" r="2.5" />
                            </svg>

                        </div>
                        <div>
                            <div class="feature-title">Free Shipping</div>
                            <div class="feature-desc">
                                On Order Above ₹2000
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Slide 3 -->
                <div class="swiper-slide">
                    <div class="feature-item">
                        <div class="feature-icon ">

                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24"
                                viewBox="0 0 24 24" fill="none" class="text-white"
                                stroke="currentColor" stroke-width="2" stroke-linecap="round"
                                stroke-linejoin="round"
                                class="lucide lucide-headset-icon lucide-headset">
                                <path d="M3 11h3a2 2 0 0 1 2 2v3a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-5Zm0 0a9 9 0 1 1 18 0m0 0v5a2 2 0 0 1-2 2h-1a2 2 0 0 1-2-2v-3a2 2 0 0 1 2-2h3Z" />
                                <path d="M21 16v2a4 4 0 0 1-4 4h-5" />
                            </svg>

                        </div>
                        <div>
                            <div class="feature-title">Expert Support</div>
                            <div class="feature-desc">Talk to specialists</div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </div>
</section>
    <!-- ===================== CATEGORIES ===================== -->
    <section class="w-full bg-white py-12 border-t border-gray-100 section-padding">
        <div class="w-full px-4 md:px-8">
            <div class="flex items-start md:items-center justify-start md:justify-center mb-4 md:mb-8 px-2 margin-bottom-20">
                <div class="text-start md:text-center">
                    <h1 class="text-2xl md:text-3xl font-bold text-[#0F172A] section-title">Our Top Categories</h1>
                    <%--<p class="text-[#64748B] mt-1 section-description">Browse our premium wellness collection</p>--%>
                </div>
            </div>
           
            <div class="swiper category-swiper">
    <div class="swiper-wrapper">
        <%=strTopCategories %>
    </div>
</div>
        </div>
    </section>
    <!-- ===================== CLIENT SLIDER ===================== -->
<section class="clients-section">
    <div class="max-w-7xl mx-auto px-4 mb-2 md:mb-4">
        <div class="text-center fade-in">
            <h2 class="section-title">
                Top Brands on Tamaz Global

            </h2>
            <p class="section-subtitle mt-1">Working with leading wellness brands worldwide</p>
        </div>
    </div>

    <div class="brand-slider-outer max-w-7xl  mx-auto py-3 pb-2">
        <div class="swiper brandSwiper">
            <div class="swiper-wrapper" id="clientSlider">
                <%=strBrandSlider %>
            </div>
      
        </div>
    </div>
</section>

        <!-- ===================== PRODUCTS ===================== -->
    <section class="products-section pt-0">
        <div class="max-w-7xl mx-auto px-3  md:px-4 md:px-8">
            <div class="flex items-center justify-between mb-10 fade-in product-title-wrapper margin-bottom-20">
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

    <!-- ===================== ABOUT US ===================== -->
    <section class="section-padding about-section bg-white overflow-hidden">
        <div class="max-w-7xl mx-auto px-4 md:px-8">
            <div class="grid lg:grid-cols-2 gap-4 md:gap-10 lg:gap-12 items-center">
                <div class="relative order-2 md:order-1">
                    <img src="/assests/Images/about-deskop.jpg" alt="About Us Image" class="w-full rounded-lg object-cover about-us-image fade-in" />
                </div>
                <div class="lg:pl-8 about-us-content order-1 md:order-2">
                    <span class="inline-block text-xs md:text-sm font-bold text-[#1E3A8A] uppercase tracking-wider mb-4 relative">DEDICATED TO QUALITY
                       
                        <span class="absolute bottom-0 left-0 w-12 h-0.5 bg-[#B91C1C] -mb-2"></span>
                    </span>
                    <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] section-title">Your Trusted Global Trading Partner in <span class="">Wellness</span>
                    </h2>
                    <p class="text-muted text-base leading-relaxed mb-4 mt-1">
                        At Tamaz Global Trading, we source premium health and wellness products from world-renowned laboratories. We are committed to providing safe, effective, and innovative solutions to enhance your well-being.
                   
                    </p>
                    <ul class="space-y-4 mb-5 md:mb-10">
                        <li class="flex items-start gap-4">
                            <div class="feature-icon bg-light-gradient">
                                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" width="24" height="24">
                                    <path d="M9 12l2 2 4-4" />
                                    <circle cx="12" cy="12" r="10" />
                                </svg>
                            </div>
                            <div>
                                <h4 class="font-semibold text-[#0F172A] text-base mb-1">Direct collaboration with certified laboratories</h4>
                                <p class="text-sm text-[#000]">We work directly with top-tier certified labs globally.</p>
                            </div>
                        </li>
                        <li class="flex items-start gap-4">
                            <div class="feature-icon bg-light-gradient">
                                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                    <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z" />
                                </svg>
                            </div>
                            <div>
                                <h4 class="font-semibold text-[#0F172A] text-base mb-1">Strict quality control and verification</h4>
                                <p class="text-sm text-[#000]">Every product undergoes rigorous testing protocols.</p>
                            </div>
                        </li>
                        <li class="flex items-start gap-4">
                            <div class="feature-icon bg-light-gradient">
                                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                    <circle cx="12" cy="12" r="10" />
                                    <line x1="2" y1="12" x2="22" y2="12" />
                                    <path d="M12 2a15.3 15.3 0 0 1 4 10 15.3 15.3 0 0 1-4 10 15.3 15.3 0 0 1-4-10 15.3 15.3 0 0 1 4-10z" />
                                </svg>
                            </div>
                            <div>
                                <h4 class="font-semibold text-[#0F172A] text-base mb-1">Global cold chain express delivery</h4>
                                <p class="text-sm text-[#000]">Safe and fast delivery maintaining product integrity.</p>
                            </div>
                        </li>
                    </ul>
                    <a href="about.aspx" class="inline-flex items-center gap-2 btn-primary text-white">Learn More
                       
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
            <div class="flex items-start md:items-center justify-start md:justify-center mb-4 md:mb-8 px-2">
                <div class="text-start md:text-center">
                    <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] section-title">In The Spotlight</h2>
                    <%--<p class="text-[#64748B] mt-1 section-description">Discover our exclusive deals and trending collections</p>--%>
                </div>
            </div>
            <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
                <%=strSpotlight %>
            </div>
        </div>
    </section>
    <section class="py-16 bg-white overflow-hidden spotlight-section show-in-mobile">
        <div class="max-w-7xl mx-auto px-4 md:px-8">
            <div class="flex items-start md:items-center justify-start md:justify-center mb-4 md:mb-8 px-2">
                <div class="text-start md:text-center">
                    <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] section-title">In The Spotlight</h2>
                    <p class="text-[#000] mt-1 section-description">Discover our exclusive deals and trending collections</p>
                </div>
            </div>
            <div class="md:hidden">
                <div class="swiper spotlightSwiper">
                    <div class="swiper-wrapper">
                        <%=strSpotlightMobile %>
                       
                    </div>
                </div>
            </div>
        </div>
    </section>





</asp:Content>

<asp:Content ID="ScriptsContent" ContentPlaceHolderID="scripts" runat="server">



    <script>
        var brandSwiper = new Swiper('.brandSwiper', {
            loop: true,
            speed: 600,
            loopedSlides: 8, 
            autoplay: {
                delay: 2500,
                disableoninteraction: false,
                pauseonmouseenter: true,
            },
            //pagination: {
            //    el: '.brand-pagination',
            //    clickable: true,
            //},
            breakpoints: {
                0: {
                    slidesPerView: 2.5,    // mobile  → 1 slide = 2 logos
                    spaceBetween: 12,
                },
                768: {
                    slidesPerView: 6,    // tablet  → 2 slides = 4 logos
                    spaceBetween: 16,
                },
                1024: {
                    slidesPerView: 8,    // desktop → 3 slides = 6 logos
                    spaceBetween: 20,
                }
            }
        });
</script>
    <script>
        var swiper = new Swiper(".myFeatureSlider", {
            loop: true,
            autoplay: {
                delay: 2500,
                disableOnInteraction: false,
            },
            spaceBetween: 15,
            slidesPerView: 1.5, // mobile

            breakpoints: {
                768: {
                    slidesPerView: 3 // desktop
                }
            }
        });
</script>
    <script>
    
    new Swiper('.category-swiper', {
        loop: true,
        autoplay: {
            delay: 1500,
            disableOnInteraction: false,
            pauseOnMouseEnter: true
        },
        speed: 600,
        slidesPerView: 3.5,
        spaceBetween: 12,
        grabCursor: true,
        breakpoints: {
            0: {
                slidesPerView: 3,
            },
            380: {
                slidesPerView: 3.5,
            },
        
            768: {          // tablet
                slidesPerView: 6,
                spaceBetween: 16
            },
            1024: {         // desktop
                slidesPerView: 8,
                spaceBetween: 20
            }
        }
    });
</script>
   
    <script>
        var mobileHeroSlider = new Swiper('.MobileHeroSlider', {
            loop: true,
            autoplay: {
                delay: 3000,
                disableOnInteraction: false,
            },

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
            
            autoplay: {
                delay: 3000,
                disableOnInteraction: false,
            },

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

            if (btn.classList.contains('view-cart-btn')) {
                window.location.href = '/cart.aspx';
                return;
            }

            btn.disabled = true;
            btn.innerText = 'Adding...';

            fetch('/Default.aspx/AddToCart', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ productId: productId.toString(), qty: 1 })
            })
                .then(function (r) { return r.json(); })
                .then(function (data) {
                    var result = data.d || data;
                    if (result.success) {
                        // Replace button with View Cart link
                        var a = document.createElement('a');
                        a.href = '/cart.aspx';
                        a.className = 'view-cart-btn add-cart-btn';
                        a.innerText = 'View Cart';
                        a.style.display = 'block';
                        a.style.textAlign = 'center';
                        a.style.textDecoration = 'none';
                        a.setAttribute('onclick', 'event.stopPropagation();');
                        btn.parentNode.replaceChild(a, btn);

                        // ✅ Update cart count badges
                        var newCount = result.cartCount ? parseInt(result.cartCount) : 0;

                        if (newCount > 0) {
                            // --- Desktop badge ---
                            var countEl = document.getElementById('cartCount');
                            if (countEl) {
                                countEl.innerText = newCount;
                                countEl.style.display = 'flex';
                            } else {
                                var cartIcon = document.querySelector('.relative.p-3');
                                if (cartIcon) {
                                    var span = document.createElement('span');
                                    span.id = 'cartCount';
                                    span.className = 'absolute -top-1 -right-1 w-5 h-5 added-cart-count text-white text-xs rounded-full flex items-center justify-center';
                                    span.style.textAlign = 'center';
                                    span.innerText = newCount;
                                    cartIcon.appendChild(span);
                                }
                            }

                            // --- Mobile badge ---
                            var countElMobile = document.getElementById('cartCountMobile');
                            if (countElMobile) {
                                countElMobile.innerText = newCount;
                                countElMobile.style.display = 'flex';
                            } else {
                                var mobileCartIcon = document.querySelector('.mobile-cart-icon');
                                if (mobileCartIcon) {
                                    var spanM = document.createElement('span');
                                    spanM.id = 'cartCountMobile';
                                    spanM.className = 'absolute -top-1 -right-1 w-5 h-5 added-cart-count text-white text-xs rounded-full flex items-center justify-center';
                                    spanM.innerText = newCount;
                                    mobileCartIcon.appendChild(spanM);
                                }
                            }
                        }

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
