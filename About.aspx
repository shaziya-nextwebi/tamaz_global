<%@ Page Title="About Us - TAMAZ Global" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.tailwindcss.com"></script>
    <script>
        tailwind.config = {
            theme: {
                extend: {
                    colors: {
                        primary: '#0a1b50',
                        accent: '#B91C1C',
                        secondary: '#1E3A8A',
                        'bg-light': '#F8FAFC',
                        'text-muted': '#64748B',
                    }
                }
            }
        }
</script>
    <style>
        .reveal {
            opacity: 0;
            transform: translateY(20px);
            transition: all 0.8s cubic-bezier(0.16, 1, 0.3, 1);
        }
        .reveal.active {
            opacity: 1;
            transform: translateY(0);
        }
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Hero Section -->
    <section class="relative bg-bg-light py-20 overflow-hidden">
        <div class="absolute inset-0">
            <img src="assests/Images/about-us-page/about-us-banner.png" alt="Background" class="w-full h-full object-cover" />
        </div>
        <div class="max-w-7xl mx-auto px-4 relative z-10 text-center">
            <div class="reveal">
                <div class="flex items-center justify-center gap-2 text-sm text-text-muted mb-6">
                    <a href="Default.aspx" class="hover:text-accent transition-colors">Home</a>
                    <span class="iconify w-4 h-4" data-icon="lucide:chevron-right"></span>
                    <span class="text-primary font-medium">About Us</span>
                </div>
                <h1 class="text-4xl md:text-5xl font-bold text-black mb-4 tracking-tight">About Us</h1>
                <p class="text-lg text-text-muted max-w-2xl mx-auto banner-aub-text">Discover the story behind Tamaz Global Trading Co. and our commitment to quality.</p>
            </div>
        </div>
    </section>

    <!-- About Company Section -->
    <section class="section-padding bg-white">
        <div class="max-w-7xl mx-auto px-4">
            <div class="grid lg:grid-cols-2 md:grid-cols-2 gap-16 items-center">
                <div class="reveal">
                    <div class="relative">
                        <div class="absolute -inset-4 bg-bg-light br-12 -z-10"></div>
                        <img src="assests/Images/about-us-page/about-us--intro.png" alt="Our Team" class="w-full h-auto br-12 shadow-xl" />
                        <div class="absolute -bottom-6 -right-6 bg-white br-12 shadow-xl p-6 border border-gray-100 about-side-strip">
                            <div class="flex items-center gap-4">
                                <div class="w-14 h-14 feature-icon rounded-full flex items-center justify-center text-[#0a1b50] text-2xl font-bold">
                                    15+
                                </div>
                                <div>
                                    <p class="font-bold text-primary">Years of</p>
                                    <p class="text-text-muted text-sm">Excellence</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="reveal">
                    <span class="inline-block px-4 py-1.5 rounded-full bg-accent/10 text-accent text-xs font-bold tracking-wider mb-4">
                        DEDICATED TO QUALITY
                    </span>
                    <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] mb-4">
                        Your Trusted Partner in Health &amp; Wellness
                    </h2>
                    <div class="space-y-4 text-muted leading-relaxed mb-8">
                        <p>
                            <strong class="text-primary">Tamaz Global Trading Co.</strong> is a premier distributor of personal care products, specializing in wholesale marketing of authentic health and wellness solutions. Based in Pakistan, we serve clients globally.
                        </p>
                        <p>
                            We believe in empowering our customers with genuine products that enhance their well-being. Our extensive network allows us to offer competitive pricing without compromising on quality.
                        </p>
                    </div>
                    <div class="grid sm:grid-cols-2 gap-4 about-point-grid-2">
                        <div class="flex items-center gap-3">
                            <div class="w-10 h-10 rounded-lg bg-green-100 flex items-center justify-center flex-shrink-0">
                                <span class="iconify w-5 h-5 text-green-600" data-icon="lucide:check-circle"></span>
                            </div>
                            <span class="text-primary font-medium fs-sm-14">100% Authentic</span>
                        </div>
                        <div class="flex items-center gap-3">
                            <div class="w-10 h-10 rounded-lg bg-blue-100 flex items-center justify-center flex-shrink-0">
                                <span class="iconify w-5 h-5 text-secondary" data-icon="lucide:users"></span>
                            </div>
                            <span class="text-primary font-medium fs-sm-14">Wholesale Focus</span>
                        </div>
                        <div class="flex items-center gap-3">
                            <div class="w-10 h-10 rounded-lg bg-purple-100 flex items-center justify-center flex-shrink-0">
                                <span class="iconify w-5 h-5 text-purple-600" data-icon="lucide:globe"></span>
                            </div>
                            <span class="text-primary font-medium fs-sm-14">Global Shipping</span>
                        </div>
                        <div class="flex items-center gap-3 fs-sm-14">
                            <div class="w-10 h-10 rounded-lg bg-orange-100 flex items-center justify-center flex-shrink-0">
                                <span class="iconify w-5 h-5 text-orange-600" data-icon="lucide:headphones"></span>
                            </div>
                            <span class="text-primary font-medium">Expert Support</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Why Choose Us Section -->
    <section class="section-padding bg-bg-light">
        <div class="max-w-7xl mx-auto px-4">
            <div class="text-center mb-6 reveal">
                <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] mb-4">Why Choose Us</h2>
                <p class="text-text-muted max-w-xl mx-auto">We are committed to providing the best service and genuine products to our valued customers.</p>
            </div> 
            <div class="grid sm:grid-cols-2 md:grid-cols-4 lg:grid-cols-4 gap-6 reveal about-point-grid-2">
                <div class="bg-white rounded-2xl p-6 border border-gray-100 hover:shadow-xl hover:-translate-y-1 transition-all duration-300 text-center group why-points-card">
                    <div class="w-16 h-16 rounded-2xl bg-accent/5 flex items-center justify-center mx-auto mb-5 group-hover:bg-accent transition-colors">
                        <span class="iconify w-8 h-8 text-accent group-hover:text-white transition-colors" data-icon="lucide:badge-check"></span>
                    </div>
                    <h3 class="text-lg font-bold text-primary mb-2">100% Authentic</h3>
                    <p class="text-sm text-text-muted">Sourced directly from authorized manufacturers.</p>
                </div>
                <div class="bg-white rounded-2xl p-6 border border-gray-100 hover:shadow-xl hover:-translate-y-1 transition-all duration-300 text-center group why-points-card">
                    <div class="w-16 h-16 rounded-2xl bg-accent/5 flex items-center justify-center mx-auto mb-5 group-hover:bg-accent transition-colors">
                        <span class="iconify w-8 h-8 text-accent group-hover:text-white transition-colors" data-icon="lucide:truck"></span>
                    </div>
                    <h3 class="text-lg font-bold text-primary mb-2">Free Shipping</h3>
                    <p class="text-sm text-text-muted">On all orders over $150 worldwide.</p>
                </div>
                <div class="bg-white rounded-2xl p-6 border border-gray-100 hover:shadow-xl hover:-translate-y-1 transition-all duration-300 text-center group why-points-card">
                    <div class="w-16 h-16 rounded-2xl bg-accent/5 flex items-center justify-center mx-auto mb-5 group-hover:bg-accent transition-colors">
                        <span class="iconify w-8 h-8 text-accent group-hover:text-white transition-colors" data-icon="lucide:shield-check"></span>
                    </div>
                    <h3 class="text-lg font-bold text-primary mb-2">Secure Payment</h3>
                    <p class="text-sm text-text-muted">100% secure transaction guarantee.</p>
                </div>
                <div class="bg-white rounded-2xl p-6 border border-gray-100 hover:shadow-xl hover:-translate-y-1 transition-all duration-300 text-center group why-points-card">
                    <div class="w-16 h-16 rounded-2xl bg-accent/5 flex items-center justify-center mx-auto mb-5 group-hover:bg-accent transition-colors">
                        <span class="iconify w-8 h-8 text-accent group-hover:text-white transition-colors" data-icon="lucide:headphones"></span>
                    </div>
                    <h3 class="text-lg font-bold text-primary mb-2">24/7 Support</h3>
                    <p class="text-sm text-text-muted">Dedicated customer support team.</p>
                </div>
            </div>
        </div>
    </section>

    <!-- Stats Section -->
    <section class="section-padding bg-white">
        <div class="max-w-7xl mx-auto px-4">
            <div class="bg-gradient-to-r from-primary to-secondary br-12 p-10 md:p-16 relative overflow-hidden stats-point-wrap">
                <div class="absolute top-0 right-0 w-96 h-96 bg-white/5 rounded-full -translate-y-1/2 translate-x-1/2"></div>
                <div class="absolute bottom-0 left-0 w-64 h-64 bg-white/5 rounded-full translate-y-1/2 -translate-x-1/2"></div>
                <div class="relative z-10 grid sm:grid-cols-2 md:grid-cols-4 gap-8 text-center about-point-grid-2">
                    <div>
                        <p class="text-5xl font-bold text-white mb-2 fs-sm-28">15+</p>
                        <p class="text-blue-200 font-medium fs-sm-14 ">Years Experience</p>
                    </div>
                    <div>
                        <p class="text-5xl font-bold text-white mb-2 fs-sm-28">500+</p>
                        <p class="text-blue-200 font-medium fs-sm-14">Products</p>
                    </div>
                    <div>
                        <p class="text-5xl font-bold text-white mb-2 fs-sm-28">50K+</p>
                        <p class="text-blue-200 font-medium fs-sm-14">Happy Customers</p>
                    </div>
                    <div>
                        <p class="text-5xl font-bold text-white mb-2 fs-sm-28">15+</p>
                        <p class="text-blue-200 font-medium fs-sm-14">Countries Served</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Our Mission Section -->
    <section class="section-padding bg-bg-light">
        <div class="max-w-7xl mx-auto px-4">
            <div class="grid lg:grid-cols-2 gap-16 items-center mobile-gap-20">
                <div class="reveal order-2 lg:order-1">
                    <span class="inline-block text-xs md:text-sm font-bold text-[#1E3A8A] uppercase tracking-wider mb-4 relative">
                        OUR MISSION
                        <span class="absolute bottom-0 left-0 w-12 h-0.5 bg-[#B91C1C] -mb-2"></span>
                    </span>
                    <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] mb-4">
                        Building Trust Through <span class="text-accent">Quality</span>
                    </h2>
                    <p class="text-muted leading-relaxed mb-6">
                        At Tamaz Global Trading Co., our mission is simple: to provide access to premium health and wellness products with integrity. We understand the importance of authenticity in the healthcare and beauty industry, which is why every product undergoes rigorous quality checks.
                    </p>
                    <p class="text-muted leading-relaxed mb-8">
                        Our team of experts is dedicated to offering personalized support, ensuring you find the right solutions for your needs. We value long-term relationships over short-term gains.
                    </p>
                    <a href="ContactUs.aspx" class="inline-flex items-center gap-2 text-accent font-semibold hover:gap-3 transition-all">
                        Contact Us
                        <span class="iconify w-5 h-5" data-icon="lucide:arrow-right"></span>
                    </a>
                </div>
                <div class="reveal order-1 lg:order-2">
                    <div class="grid grid-cols-2 gap-4 grid-sm-1">
                        <img src="assests/Images/about-us-page/our-mission-image/1.png" alt="products" class="w-full h-64 object-cover rounded-2xl shadow-lg" />
                        <img src="assests/Images/about-us-page/our-mission-image/2.png" alt="products" class="w-full h-64 object-cover rounded-2xl shadow-lg mt-8 mobile-d-none" />
                    </div>
                </div>
            </div>
        </div>
        <div class="max-w-7xl mx-auto mt-8 our-mission-points">
            <div class="grid sm:grid-cols-3 md:grid-cols-3 gap-6 about-point-grid-2">
                <div class="flex items-center gap-4 p-4">
                    <span class="iconify w-12 h-12 text-accent" data-icon="lucide:truck"></span>
                    <div>
                        <h4 class="font-bold text-primary">Free Shipping</h4>
                        <p class="text-sm text-text-muted">On orders over $150</p>
                    </div>
                </div>
                <div class="flex items-center gap-4 p-4">
                    <span class="iconify w-12 h-12 text-accent" data-icon="lucide:shield-check"></span>
                    <div>
                        <h4 class="font-bold text-primary">Secure Payment</h4>
                        <p class="text-sm text-text-muted">100% Secure Transaction</p>
                    </div>
                </div>
                <div class="flex items-center gap-4 p-4">
                    <span class="iconify w-12 h-12 text-accent" data-icon="lucide:refresh-cw"></span>
                    <div>
                        <h4 class="font-bold text-primary">Money Back</h4>
                        <p class="text-sm text-text-muted">30-Day Guarantee</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Call To Action -->
    <section class="section-padding bg-white">
        <div class="max-w-7xl mx-auto px-4 text-center reveal">
            <div class="br-12 p-12 md:p-16 border border-gray-100 relative overflow-hidden cta-bg-image stats-point-wrap">
                <h2 class="text-2xl md:text-3xl font-bold text-[#ffffff] mb-4">Partner With Us Today</h2>
                <p class="text-white max-w-xl mx-auto mb-8">Join our network of satisfied customers and distributors. Contact us for wholesale inquiries.</p>
                <div class="flex flex-col sm:flex-row gap-4 justify-center">
                    <a href="ContactUs.aspx" class="inline-flex items-center justify-center gap-2 bg-accent text-white px-8 py-3.5 rounded-md font-semibold hover:bg-red-800 transition-colors shadow-lg shadow-accent/20">
                        <span class="iconify w-5 h-5" data-icon="lucide:mail"></span>
                        Contact Us
                    </a>
                    <a href="tel:919988227622" class="inline-flex items-center justify-center gap-2 bg-white text-primary px-8 py-3.5 rounded-md font-semibold hover:bg-primary hover:text-white transition-colors">
                        <span class="iconify w-5 h-5" data-icon="lucide:phone"></span>
                        Call Now
                    </a>
                </div>
            </div>
        </div>
    </section>

  

</asp:Content>

<asp:Content ID="ScriptsContent" ContentPlaceHolderID="scripts" runat="server">
    <script src="https://code.iconify.design/3/3.1.0/iconify.min.js"></script>
    <script>
        const reveals = document.querySelectorAll('.reveal');
        const revealOnScroll = () => {
            reveals.forEach(el => {
                const windowHeight = window.innerHeight;
                const elementTop = el.getBoundingClientRect().top;
                if (elementTop < window.innerHeight - 150) {
                    el.classList.add('active');
                }
            });
        };
        window.addEventListener('scroll', revealOnScroll);
        revealOnScroll();
    </script>
</asp:Content>
