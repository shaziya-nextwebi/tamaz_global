<%@ Page Title="Glutathione Injection Sharing - TAMAZ Global" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BlogDetail.aspx.cs" Inherits="BlogDetail" %>

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

    <!-- Breadcrumb -->
    <div class="bg-[#f8fafc] py-3 border-b border-gray-200">
        <div class="max-w-7xl mx-auto px-4">
            <div class="flex items-center gap-2 text-sm text-slate-500">
                <a href="Default.aspx" class="hover:text-red-700 transition-colors">Home</a>
                <span class="iconify w-4 h-4" data-icon="lucide:chevron-right"></span>
                <a href="/Blog.aspx" class="hover:text-red-700 transition-colors">Blog</a>
                <span class="iconify w-4 h-4" data-icon="lucide:chevron-right"></span>
                <span class="text-slate-900 font-medium blog-tittle-breadCrumb"><%= BlogTitle %></span>
            </div>
        </div>
    </div>

    <!-- Main Content -->
    <section class="section-padding bg-white">
        <div class="max-w-7xl mx-auto px-4">
            <div class="grid lg:grid-cols-3 gap-10">

                <!-- Article Content (Left Side) -->
                <div class="lg:col-span-2 reveal">
                    <article class="bg-white br-12 shadow-sm border border-slate-100 overflow-hidden">
                        <!-- Featured Image -->
                        <div class="relative h-64 md:h-96 w-full">
                            <img src="/<%= ImageUrl %>" alt="image" class="w-full h-full object-cover" />
                            <div class="absolute inset-0 bg-gradient-to-t from-slate-900/50 to-transparent blog-Detailcard-overlay"></div>
                            <div class="absolute bottom-0 left-0 right-0 p-6">
                                <span class="px-3 py-1 rounded-full bg-red-700 text-white text-xs font-semibold mb-3 inline-block"><%= CategoryName %></span>
                                <h1 class="text-2xl md:text-3xl font-bold text-[#fff] mb-4 blog-detail-title" ><%= BlogTitle %></h1>
                            </div>
                        </div>

                        <!-- Meta Info -->
                        <div class="flex flex-wrap items-center gap-4 px-6 py-4 border-b border-slate-100 bg-slate-50/50">
                            <div class="flex items-center gap-2">
                                <img src="/assests/Images/avathar.png" alt="Author" class="w-8 h-8 rounded-full object-cover" />
                                <span class="text-sm font-medium text-slate-700"><%= Author %></span>
                            </div>
                            <span class="text-slate-300">|</span>
                            <div class="flex items-center gap-1 text-sm text-slate-500">
                                <span class="iconify w-4 h-4" data-icon="lucide:calendar"></span>
                                <%= PublishDate %>
                            </div>
                        </div>

                        <!-- Article Body -->
                        <div class="p-6 md:p-8 blog-detail max-w-none pt-0 md:pt-0">
                            <%= BlogContent %>
                            <%-- <h2>Introduction</h2>
                            <p>
                                In the pursuit of flawless, radiant skin, many individuals turn to various skincare treatments. Among these, <strong>Glutathione injections</strong> have gained immense popularity for their skin brightening and antioxidant properties. In this article, we will delve into the details of Glutathione injections, sharing personal experiences and insights to help you make an informed decision.
                           
                            </p>

                            <h2>What is Glutathione Injection?</h2>
                            <p>
                                Glutathione is a powerful antioxidant naturally produced in the body. It plays a crucial role in neutralizing free radicals, detoxifying the liver, and boosting the immune system. When administered via injection, Glutathione works by inhibiting the enzyme tyrosinase, which is responsible for the production of melanin (skin pigment). This process leads to a gradual lightening of the skin tone and a reduction in hyperpigmentation.
                           
                            </p>
                            <p>
                                Unlike oral supplements, injections deliver the antioxidant directly into the bloodstream, ensuring maximum absorption and faster results. This method is often preferred by those seeking significant changes in their complexion.
                           
                            </p>

                            <h2>Personal Skin Condition</h2>
                            <p>Before starting the treatment, my skin was dealing with several common issues:</p>
                            <ul>
                                <li>Uneven skin tone with visible dark spots.</li>
                                <li>Frequent breakouts and acne marks.</li>
                                <li>Dullness caused by sun exposure and pollution.</li>
                                <li>Hyperpigmentation around the mouth and forehead.</li>
                            </ul>
                            <p>
                                Despite trying various topical creams and serums, the results were often temporary or minimal. This led me to explore clinical treatments like Glutathione injections.
                           
                            </p>

                            <h2>Treatment Effect &amp; Results</h2>
                            <p>The treatment usually involves a series of sessions, spaced a week apart. Here is a breakdown of the results observed over time:</p>

                            <div class="bg-blue-50 border-l-4 border-blue-900 p-4 my-6 rounded-r-lg">
                                <h3 class="text-blue-900 font-bold mt-0 mb-2">Weeks 1-3:</h3>
                                <p class="text-blue-900 mb-0">I noticed a subtle glow. My skin felt more hydrated, and the texture began to improve. The dullness started to fade.</p>
                            </div>

                            <div class="bg-blue-50 border-l-4 border-blue-900 p-4 my-6 rounded-r-lg">
                                <h3 class="text-blue-900 font-bold mt-0 mb-2">Weeks 4-8:</h3>
                                <p class="text-blue-900 mb-0">Significant changes were visible. The dark spots started to lighten, and my overall skin tone became more even. Friends and family started complimenting the "natural glow."</p>
                            </div>

                            <div class="bg-blue-50 border-l-4 border-blue-900 p-4 my-6 rounded-r-lg">
                                <h3 class="text-blue-900 font-bold mt-0 mb-2">After 3 Months:</h3>
                                <p class="text-blue-900 mb-0">My skin was visibly brighter and smoother. The hyperpigmentation had reduced by about 70-80%. The acne marks were barely noticeable.</p>
                            </div>

                            <h2>Conclusion</h2>
                            <p>
                                Glutathione injections can be a game-changer for those struggling with skin pigmentation and texture issues. However, it is essential to consult with a certified dermatologist before starting any treatment. Authentic products and proper dosage are key to achieving safe and effective results. At <strong>Tamaz Global Trading Co.</strong>, we ensure you get genuine products for your wellness journey.
                           
                            </p>--%>
                        </div>

                        <!-- Tags & Share -->
                        <div class="px-6 md:px-8 py-5 border-t border-slate-100 flex flex-col sm:flex-row justify-between items-center gap-4 bg-slate-50">
                            <div class="flex items-center gap-2 flex-wrap">
                                <span class="text-sm font-semibold text-slate-600">Tags:</span>
                                <%= BlogTags %>
                                <%-- <a href="#" class="px-3 py-1 bg-white border border-slate-200 rounded-full text-xs text-slate-600 hover:bg-slate-100 transition-colors">Glutathione</a>
                                <a href="#" class="px-3 py-1 bg-white border border-slate-200 rounded-full text-xs text-slate-600 hover:bg-slate-100 transition-colors">Skin Whitening</a>
                                <a href="#" class="px-3 py-1 bg-white border border-slate-200 rounded-full text-xs text-slate-600 hover:bg-slate-100 transition-colors">Injection</a>
                                --%>
                            </div>
                            <div class="flex items-center gap-2">
                                <span class="text-sm font-semibold text-slate-600">Share:</span>
                                <a href="#" class="w-8 h-8 rounded-full bg-white border border-slate-200 flex items-center justify-center hover:bg-blue-600 hover:text-white hover:border-blue-600 transition-colors text-slate-500">
                                    <span class="iconify w-4 h-4" data-icon="lucide:facebook"></span>
                                </a>
                                <a href="#" class="w-8 h-8 rounded-full bg-white border border-slate-200 flex items-center justify-center hover:bg-sky-500 hover:text-white hover:border-sky-500 transition-colors text-slate-500">
                                    <span class="iconify w-4 h-4" data-icon="lucide:twitter"></span>
                                </a>
                                <a href="#" class="w-8 h-8 rounded-full bg-white border border-slate-200 flex items-center justify-center hover:bg-green-600 hover:text-white hover:border-green-600 transition-colors text-slate-500">
                                    <span class="iconify w-4 h-4" data-icon="lucide:share"></span>
                                </a>
                            </div>
                        </div>
                    </article>

                    <!-- Related Posts -->
                    <div class="mt-12 reveal">
                        <h2 class="text-2xl font-bold text-slate-900 mb-6">Related Posts</h2>
                        <div class="grid sm:grid-cols-2 gap-6">
                            <%= RelatedPosts %>
                            <%--     <div class="reveal blog-card group bg-white br-12 shadow-sm border border-slate-100 overflow-hidden hover:shadow-xl transition-all duration-500">
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
                            --%>
                        </div>
                    </div>
                </div>

                <!-- Sidebar (Right Side) -->
                <div class="lg:col-span-1">
                    <div class="sticky top-2 blog-right-side space-y-6">

                        <!-- Categories Widget -->
                        <div class="bg-white p-6 br-12 shadow-sm border border-slate-100 reveal">
                            <h3 class="text-lg font-bold text-slate-900 mb-4">Categories</h3>
                            <ul class="space-y-3">
                                  <%= Categories %>
                               <%-- <li>
                                    <a href="#" class="flex justify-between items-center text-slate-600 hover:text-red-700 transition-colors text-sm">
                                        <span>Skin Care</span>
                                        <span class="bg-slate-100 px-2 py-0.5 rounded text-xs text-slate-500">12</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#" class="flex justify-between items-center text-slate-600 hover:text-red-700 transition-colors text-sm">
                                        <span>Health Supplements</span>
                                        <span class="bg-slate-100 px-2 py-0.5 rounded text-xs text-slate-500">08</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#" class="flex justify-between items-center text-slate-600 hover:text-red-700 transition-colors text-sm">
                                        <span>Anti-Aging</span>
                                        <span class="bg-slate-100 px-2 py-0.5 rounded text-xs text-slate-500">05</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#" class="flex justify-between items-center text-slate-600 hover:text-red-700 transition-colors text-sm">
                                        <span>Weight Loss</span>
                                        <span class="bg-slate-100 px-2 py-0.5 rounded text-xs text-slate-500">04</span>
                                    </a>
                                </li>--%>
                            </ul>
                        </div>

                        <!-- Recent Posts Widget -->
                        <div class="bg-white p-6 br-12 shadow-sm border border-slate-100 reveal">
                            <h3 class="text-lg font-bold text-slate-900 mb-4">Recent Posts</h3>
                            <div class="space-y-4">
                                 <%= RecentPosts %>
                               <%-- <a href="#" class="flex gap-3 group">
                                    <img src="https://picsum.photos/seed/recent1/100/100" alt="Recent Post" class="w-16 h-16 rounded-lg object-cover flex-shrink-0" />
                                    <div>
                                        <h4 class="text-sm font-semibold text-slate-900 group-hover:text-red-700 transition-colors leading-tight">Top 5 Benefits of NAD+ Therapy</h4>
                                        <p class="text-xs text-slate-500 mt-1">March 20, 2024</p>
                                    </div>
                                </a>
                                <a href="#" class="flex gap-3 group">
                                    <img src="https://picsum.photos/seed/recent2/100/100" alt="Recent Post" class="w-16 h-16 rounded-lg object-cover flex-shrink-0" />
                                    <div>
                                        <h4 class="text-sm font-semibold text-slate-900 group-hover:text-red-700 transition-colors leading-tight">Choosing the Right Supplements</h4>
                                        <p class="text-xs text-slate-500 mt-1">March 19, 2024</p>
                                    </div>
                                </a>--%>
                            </div>
                        </div>

                        <!-- CTA Widget -->
                        <div class="bg-gradient-to-br from-slate-900 to-blue-900 p-6 br-12 shadow-lg text-white reveal">
                            <h3 class="text-lg font-bold mb-2">Want Better Skin?</h3>
                            <p class="text-sm text-slate-300 mb-4">Discover personalized skincare tips and expert recommendations today.</p>
                            <a href="/Category/glutathione-injections" class="inline-flex items-center gap-2 bg-red-700 text-white px-4 py-2 rounded-lg text-sm font-semibold hover:bg-red-800 transition-colors w-full justify-center">
                                <span class="iconify w-4 h-4" data-icon="lucide:shopping-cart"></span>
                                Shop Now
                            </a>
                        </div>
                    </div>
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
                if (el.getBoundingClientRect().top < window.innerHeight - 50) {
                    el.classList.add('active');
                }
            });
        };
        window.addEventListener('scroll', revealOnScroll);
        revealOnScroll();
    </script>
</asp:Content>
