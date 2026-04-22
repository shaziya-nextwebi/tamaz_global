<%@ Page Title="Our Blog - TAMAZ Global" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Blog.aspx.cs" Inherits="Blog" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
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
            <img src="assests/Images/blog-banner.png" alt="Background" class="w-full h-full object-cover" />
        </div>
        <div class="max-w-7xl mx-auto px-4 relative z-10 text-center">
            <div class="reveal">
                <div class="flex items-center justify-center gap-2 text-sm text-text-muted mb-6">
                    <a href="Default.aspx" class="hover:text-accent transition-colors">Home</a>
                    <span class="iconify w-4 h-4" data-icon="lucide:chevron-right"></span>
                    <span class="text-primary font-medium">Blogs</span>
                </div>
                <h1 class="text-4xl md:text-5xl font-bold text-black mb-4 tracking-tight">Our Blogs</h1>
                <p class="text-lg text-text-muted max-w-2xl mx-auto">
                    Dive into stories, tips, and trends designed to inform, inspire, and elevate your knowledge.
               
                </p>
            </div>
        </div>
    </section>

    <!-- Blog Grid Section -->
    <section class="py-16">
        <div class="max-w-7xl mx-auto px-4">
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8" id="blogContainer"></div>

            <!-- Blog Card 1 -->
            <%--      <div class="reveal blog-card group bg-white br-12 shadow-sm border border-slate-100 overflow-hidden hover:shadow-xl transition-all duration-500">
                <a href="BlogDetail.aspx">
                    <div class="relative overflow-hidden">
                        <img src="https://picsum.photos/seed/glutathione/600/400" alt="Glutathione" class="w-full h-56 object-cover card-img" />
                        <div class="absolute top-4 left-4">
                            <span class="px-3 py-1 rounded-full bg-red-700 text-white text-xs font-semibold">Skin Care</span>
                        </div>
                    </div>
                    <div class="p-6">
                        <div class="flex items-center gap-4 text-xs text-slate-500 mb-3">
                            <span class="flex items-center gap-1">
                                <span class="iconify w-4 h-4" data-icon="lucide:calendar"></span>
                                Mar 20, 2024
                                </span>
                            <span class="flex items-center gap-1">
                                <span class="iconify w-4 h-4" data-icon="lucide:user"></span>
                                Admin
                                </span>
                        </div>
                        <h3 class="text-xl font-bold mb-3 blog-title transition-colors">Glutathione Injection: Before and After Guide
                            </h3>
                        <p class="text-slate-500 text-sm mb-4 line-clamp-2">
                            Discover the real results of Glutathione injections. Learn what to expect before and after the treatment for maximum skin brightening effects.
                           
                        </p>
                        <span class="inline-flex items-center gap-2 text-red-700 font-semibold text-sm group-hover:gap-3 transition-all">Read More
                               
                            <span class="iconify w-4 h-4" data-icon="lucide:arrow-right"></span>
                        </span>
                    </div>
                </a>
            </div>

            <!-- Blog Card 2 -->
            <div class="reveal blog-card group bg-white br-12 shadow-sm border border-slate-100 overflow-hidden hover:shadow-xl transition-all duration-500">
                <div class="relative overflow-hidden">
                    <img src="https://picsum.photos/seed/supplements/600/400" alt="Supplements" class="w-full h-56 object-cover card-img" />
                    <div class="absolute top-4 left-4">
                        <span class="px-3 py-1 rounded-full bg-blue-900 text-white text-xs font-semibold">Health</span>
                    </div>
                </div>
                <div class="p-6">
                    <div class="flex items-center gap-4 text-xs text-slate-500 mb-3">
                        <span class="flex items-center gap-1">
                            <span class="iconify w-4 h-4" data-icon="lucide:calendar"></span>
                            Mar 18, 2024
                            </span>
                        <span class="flex items-center gap-1">
                            <span class="iconify w-4 h-4" data-icon="lucide:user"></span>
                            Dr. Sarah
                            </span>
                    </div>
                    <h3 class="text-xl font-bold mb-3 blog-title transition-colors">Top 5 Benefits of Vitamin C IV Therapy
                        </h3>
                    <p class="text-slate-500 text-sm mb-4 line-clamp-2">
                        High-dose Vitamin C IV therapy is gaining popularity. Explore the top benefits for immunity, skin health, and energy levels.
                       
                    </p>
                    <a href="#" class="inline-flex items-center gap-2 text-red-700 font-semibold text-sm group-hover:gap-3 transition-all">Read More
                           
                        <span class="iconify w-4 h-4" data-icon="lucide:arrow-right"></span>
                    </a>
                </div>
            </div>

            <!-- Blog Card 3 -->
            <div class="reveal blog-card group bg-white br-12 shadow-sm border border-slate-100 overflow-hidden hover:shadow-xl transition-all duration-500">
                <div class="relative overflow-hidden">
                    <img src="https://picsum.photos/seed/authentic/600/400" alt="Authentic Products" class="w-full h-56 object-cover card-img" />
                    <div class="absolute top-4 left-4">
                        <span class="px-3 py-1 rounded-full bg-slate-900 text-white text-xs font-semibold">Guide</span>
                    </div>
                </div>
                <div class="p-6">
                    <div class="flex items-center gap-4 text-xs text-slate-500 mb-3">
                        <span class="flex items-center gap-1">
                            <span class="iconify w-4 h-4" data-icon="lucide:calendar"></span>
                            Mar 15, 2024
                            </span>
                        <span class="flex items-center gap-1">
                            <span class="iconify w-4 h-4" data-icon="lucide:user"></span>
                            Admin
                            </span>
                    </div>
                    <h3 class="text-xl font-bold mb-3 blog-title transition-colors">How to Identify Authentic Whitening Products
                        </h3>
                    <p class="text-slate-500 text-sm mb-4 line-clamp-2">
                        Don't fall for fake products. Learn the key indicators of authentic skin whitening injections and supplements to ensure safety.
                       
                    </p>
                    <a href="#" class="inline-flex items-center gap-2 text-red-700 font-semibold text-sm group-hover:gap-3 transition-all">Read More
                           
                        <span class="iconify w-4 h-4" data-icon="lucide:arrow-right"></span>
                    </a>
                </div>
            </div>

            <!-- Blog Card 4 -->
            <div class="reveal blog-card group bg-white br-12 shadow-sm border border-slate-100 overflow-hidden hover:shadow-xl transition-all duration-500">
                <div class="relative overflow-hidden">
                    <img src="https://picsum.photos/seed/antiaging/600/400" alt="Anti Aging" class="w-full h-56 object-cover card-img" />
                    <div class="absolute top-4 left-4">
                        <span class="px-3 py-1 rounded-full bg-red-700 text-white text-xs font-semibold">Skin Care</span>
                    </div>
                </div>
                <div class="p-6">
                    <div class="flex items-center gap-4 text-xs text-slate-500 mb-3">
                        <span class="flex items-center gap-1">
                            <span class="iconify w-4 h-4" data-icon="lucide:calendar"></span>
                            Mar 10, 2024
                            </span>
                        <span class="flex items-center gap-1">
                            <span class="iconify w-4 h-4" data-icon="lucide:user"></span>
                            Expert
                            </span>
                    </div>
                    <h3 class="text-xl font-bold mb-3 blog-title transition-colors">Anti-Aging Secrets: What Really Works?
                        </h3>
                    <p class="text-slate-500 text-sm mb-4 line-clamp-2">
                        From collagen boosters to NAD+ therapy, we break down the science behind effective anti-aging treatments available today.
                       
                    </p>
                    <a href="#" class="inline-flex items-center gap-2 text-red-700 font-semibold text-sm group-hover:gap-3 transition-all">Read More
                           
                        <span class="iconify w-4 h-4" data-icon="lucide:arrow-right"></span>
                    </a>
                </div>
            </div>

            <!-- Blog Card 5 -->
            <div class="reveal blog-card group bg-white br-12 shadow-sm border border-slate-100 overflow-hidden hover:shadow-xl transition-all duration-500">
                <div class="relative overflow-hidden">
                    <img src="https://picsum.photos/seed/weightloss/600/400" alt="Weight Loss" class="w-full h-56 object-cover card-img" />
                    <div class="absolute top-4 left-4">
                        <span class="px-3 py-1 rounded-full bg-blue-900 text-white text-xs font-semibold">Wellness</span>
                    </div>
                </div>
                <div class="p-6">
                    <div class="flex items-center gap-4 text-xs text-slate-500 mb-3">
                        <span class="flex items-center gap-1">
                            <span class="iconify w-4 h-4" data-icon="lucide:calendar"></span>
                            Mar 05, 2024
                            </span>
                        <span class="flex items-center gap-1">
                            <span class="iconify w-4 h-4" data-icon="lucide:user"></span>
                            Admin
                            </span>
                    </div>
                    <h3 class="text-xl font-bold mb-3 blog-title transition-colors">L-Carnitine Injections for Weight Management
                        </h3>
                    <p class="text-slate-500 text-sm mb-4 line-clamp-2">
                        A comprehensive guide on using L-Carnitine injections as a supplement for your weight loss journey and energy boost.
                       
                    </p>
                    <a href="#" class="inline-flex items-center gap-2 text-red-700 font-semibold text-sm group-hover:gap-3 transition-all">Read More
                           
                        <span class="iconify w-4 h-4" data-icon="lucide:arrow-right"></span>
                    </a>
                </div>
            </div>

            <!-- Blog Card 6 -->
            <div class="reveal blog-card group bg-white br-12 shadow-sm border border-slate-100 overflow-hidden hover:shadow-xl transition-all duration-500">
                <div class="relative overflow-hidden">
                    <img src="https://picsum.photos/seed/review/600/400" alt="Reviews" class="w-full h-56 object-cover card-img" />
                    <div class="absolute top-4 left-4">
                        <span class="px-3 py-1 rounded-full bg-slate-900 text-white text-xs font-semibold">Reviews</span>
                    </div>
                </div>
                <div class="p-6">
                    <div class="flex items-center gap-4 text-xs text-slate-500 mb-3">
                        <span class="flex items-center gap-1">
                            <span class="iconify w-4 h-4" data-icon="lucide:calendar"></span>
                            Mar 01, 2024
                            </span>
                        <span class="flex items-center gap-1">
                            <span class="iconify w-4 h-4" data-icon="lucide:user"></span>
                            Customer
                            </span>
                    </div>
                    <h3 class="text-xl font-bold mb-3 blog-title transition-colors">Real Customer Experience: Skin Whitening Journey
                        </h3>
                    <p class="text-slate-500 text-sm mb-4 line-clamp-2">
                        A detailed story from one of our loyal customers about their 3-month skin whitening journey using our products.
                       
                    </p>
                    <a href="#" class="inline-flex items-center gap-2 text-red-700 font-semibold text-sm group-hover:gap-3 transition-all">Read More
                           
                        <span class="iconify w-4 h-4" data-icon="lucide:arrow-right"></span>
                    </a>
                </div>
            </div>

        </div>--%>

            <!-- Pagination -->
            <div class="flex justify-center items-center gap-2 mt-12 reveal">
                <ul id="blogPagination" class="flex gap-2"></ul>
            </div>
            <%--        <div class="flex justify-center items-center gap-2 mt-12 reveal">
            <a href="#" class="w-10 h-10 flex items-center justify-center rounded-lg border border-slate-200 text-slate-400 hover:bg-slate-100 transition-colors">
                <span class="iconify w-5 h-5" data-icon="lucide:chevron-left"></span>
            </a>
            <a href="#" class="w-10 h-10 flex items-center justify-center rounded-lg bg-[#0a1b50] text-white font-semibold shadow-md">1</a>
            <a href="#" class="w-10 h-10 flex items-center justify-center rounded-lg border border-slate-200 text-slate-700 hover:bg-slate-100 transition-colors font-medium">2</a>
            <a href="#" class="w-10 h-10 flex items-center justify-center rounded-lg border border-slate-200 text-slate-700 hover:bg-slate-100 transition-colors font-medium">3</a>
            <span class="px-2 text-slate-400">...</span>
            <a href="#" class="w-10 h-10 flex items-center justify-center rounded-lg border border-slate-200 text-slate-700 hover:bg-slate-100 transition-colors font-medium">10</a>
            <a href="#" class="w-10 h-10 flex items-center justify-center rounded-lg border border-slate-200 text-slate-400 hover:bg-slate-100 transition-colors">
                <span class="iconify w-5 h-5" data-icon="lucide:chevron-right"></span>
            </a>
        </div>--%>
        </div>

    </section>

    <!-- WhatsApp Floating Button -->
    <a href="https://wa.me/92300123456" target="_blank" class="fixed bottom-6 right-6 w-14 h-14 bg-green-500 rounded-full flex items-center justify-center shadow-2xl shadow-green-500/30 hover:scale-110 transition-transform z-50">
        <span class="iconify w-7 h-7 text-white" data-icon="logos:whatsapp-icon"></span>
    </a>
    <script src="https://code.iconify.design/3/3.1.0/iconify.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="assests/js/pages/blog.js"></script>
</asp:Content>
