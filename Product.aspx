<%@ Page Title="Product Detail - TAMAZ Global" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Product" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
        .scrollbar-hide::-webkit-scrollbar { display: none; }
        .scrollbar-hide { -ms-overflow-style: none; scrollbar-width: none; }
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Breadcrumb -->
    <section class="bg-[#f8fafc] py-3 border-b border-gray-200">
        <div class="max-w-7xl mx-auto px-4">
            <nav class="text-sm text-[#64748B] breadcrumb">
                <a href="Default.aspx" class="hover:text-[#B91C1C]">Home</a>
                <span class="mx-2">/</span>
                <a href="Category.aspx" class="hover:text-[#B91C1C]">Injections</a>
                <span class="mx-2">/</span>
                <span class="text-[#0F172A] font-medium">Vesco Pharma Gluta C 1000</span>
            </nav>
        </div>
    </section>

    <!-- Product Top Section -->
    <section class="py-8 md:py-12 bg-white">
        <div class="max-w-7xl mx-auto px-4">
            <div class="grid grid-cols-1 md:grid-cols-2 gap-10 lg:gap-10">

                <!-- Left: Product Images -->
                <div class="flex flex-col gap-3">
                    <div class="relative w-full aspect-square bg-gray-50 rounded-2xl overflow-hidden border border-gray-100 group main-product-image-wrap">
                        <img id="mainProductImage" src="assests/Images/old-product/p-5.jpg" alt="Product Main"
                            onclick="openModal(currentIndex)"
                            class="object-contain transition-transform duration-500 group-hover:scale-105 mainProductImage cursor-pointer" />
                        <span class="absolute top-4 left-4 bg-[#B91C1C] text-white text-xs font-bold px-3 py-1 rounded-full">Best Seller</span>
                    </div>
                    <div class="grid grid-cols-4 gap-3">
                        <button onclick="changeImage(0)" class="thumb-btn active rounded-xl overflow-hidden border-2 border-transparent hover:border-[#B91C1C] transition-all">
                            <img src="assests/Images/old-product/p-1.jpg" class="w-full h-20 object-cover" />
                        </button>
                        <button onclick="changeImage(1)" class="thumb-btn rounded-xl overflow-hidden border-2 border-transparent hover:border-[#B91C1C] transition-all">
                            <img src="assests/Images/old-product/p-2.jpg" class="w-full h-20 object-cover" />
                        </button>
                        <button onclick="changeImage(2)" class="thumb-btn rounded-xl overflow-hidden border-2 border-transparent hover:border-[#B91C1C] transition-all">
                            <img src="assests/Images/old-product/p-3.jpg" class="w-full h-20 object-cover" />
                        </button>
                        <button onclick="changeImage(3)" class="thumb-btn rounded-xl overflow-hidden border-2 border-transparent hover:border-[#B91C1C] transition-all">
                            <img src="assests/Images/old-product/p-4.jpg" class="w-full h-20 object-cover" />
                        </button>
                    </div>
                </div>

                <!-- Right: Product Info -->
                <div class="flex flex-col">
                    <div class="flex items-center mb-4 flex-wrap gap-3">
                        <span class="text-xs font-bold text-[#1E3A8A] uppercase tracking-wider">Vesco Pharma</span>
                        <span class="inline-flex items-center gap-2 bg-[#2E7D32] text-white text-xs font-bold px-4 py-1.5 rounded-full shadow-sm">
                            <svg width="14" height="14" viewBox="0 0 23 17" fill="none" xmlns="http://www.w3.org/2000/svg" class="flex-shrink-0">
                                <path d="M19.0312 4.03125H16.0312V0H2.01562C0.9375 0 0 0.9375 0 2.01562V13.0312H2.01562C2.01562 14.6719 3.375 16.0312 5.01562 16.0312C6.65625 16.0312 8.01562 14.6719 8.01562 13.0312H14.0156C14.0156 14.6719 15.375 16.0312 17.0156 16.0312C18.6562 16.0312 20.0156 14.6719 20.0156 13.0312H22.0312V8.01562L19.0312 4.03125ZM18.5156 5.53125L20.4844 8.01562H16.0312V5.53125H18.5156ZM5.01562 14.0156C4.45312 14.0156 4.03125 13.5469 4.03125 13.0312C4.03125 12.4688 4.45312 12 5.01562 12C5.57812 12 6 12.4688 6 13.0312C6 13.5469 5.57812 14.0156 5.01562 14.0156ZM7.21875 11.0156C6.70312 10.4062 5.90625 10.0312 5.01562 10.0312C4.125 10.0312 3.32812 10.4062 2.8125 11.0156H2.01562V2.01562H14.0156V11.0156H7.21875ZM17.0156 14.0156C16.4531 14.0156 16.0312 13.5469 16.0312 13.0312C16.0312 12.4688 16.4531 12 17.0156 12C17.5781 12 18 12.4688 18 13.0312C18 13.5469 17.5781 14.0156 17.0156 14.0156Z" fill="currentColor" />
                            </svg>
                            Available
                        </span>
                    </div>

                    <h1 class="text-2xl section-title md:text-3xl font-bold text-[#0F172A] mb-3">
                        Vesco Pharma Gluta C 1000 Glutathione Skin Whitening Injection
                    </h1>
                    <p class="text-sm md:text-base text-[#64748B] mb-6 leading-relaxed">
                        High concentration Glutathione formula for effective skin whitening and anti-aging benefits. Contains Vitamin C for enhanced absorption and radiance.
                    </p>

                    <!-- Key Highlights -->
                    <div class="grid grid-cols-1 sm:grid-cols-3 gap-4 mb-6">
                        <div class="flex items-center gap-3 bg-gray-50 p-3 rounded-lg border border-gray-100">
                            <div class="flex-shrink-0 w-10 h-10 bg-blue-100 rounded-full flex items-center justify-center text-blue-600">
                                <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                    <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path>
                                    <circle cx="9" cy="7" r="4"></circle>
                                    <path d="M23 21v-2a4 4 0 0 0-3-3.87"></path>
                                    <path d="M16 3.13a4 4 0 0 1 0 7.75"></path>
                                </svg>
                            </div>
                            <div>
                                <h4 class="text-xs font-bold text-gray-800">Designed by</h4>
                                <p class="text-xs text-gray-500">Dermatologists</p>
                            </div>
                        </div>
                        <div class="flex items-center gap-3 bg-gray-50 p-3 rounded-lg border border-gray-100">
                            <div class="flex-shrink-0 w-10 h-10 bg-green-100 rounded-full flex items-center justify-center text-green-600">
                                <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                    <path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"></path>
                                    <polyline points="14 2 14 8 20 8"></polyline>
                                </svg>
                            </div>
                            <div>
                                <h4 class="text-xs font-bold text-gray-800">Honest Disclosure</h4>
                                <p class="text-xs text-gray-500">Active Percentage</p>
                            </div>
                        </div>
                        <div class="flex items-center gap-3 bg-gray-50 p-3 rounded-lg border border-gray-100">
                            <div class="flex-shrink-0 w-10 h-10 bg-orange-100 rounded-full flex items-center justify-center text-orange-600">
                                <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                    <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"></path>
                                </svg>
                            </div>
                            <div>
                                <h4 class="text-xs font-bold text-gray-800">Safe &amp;</h4>
                                <p class="text-xs text-gray-500">Effective</p>
                            </div>
                        </div>
                    </div>

                    <!-- Meta Data -->
                    <div class="mb-6">
                        <div class="grid grid-cols-2 sm:grid-cols-3 gap-2 sm:gap-3">
                            <div class="flex items-center gap-2 p-3 rounded-lg hover:bg-red-50/30 transition-colors cursor-pointer group">
                                <div class="w-8 h-8 rounded-md bg-gray-100 flex items-center justify-center text-gray-500 group-hover:text-[#B91C1C] group-hover:bg-red-100 transition-colors">
                                    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                        <path d="M22 19a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h5l2 3h9a2 2 0 0 1 2 2z"></path>
                                    </svg>
                                </div>
                                <div>
                                    <span class="block text-[10px] text-gray-400 uppercase tracking-wider">Category</span>
                                    <span class="block text-xs font-semibold text-gray-700">Glutathione Injections</span>
                                </div>
                            </div>
                            <div class="flex items-center gap-2 p-3 rounded-lg hover:bg-red-50/30 transition-colors cursor-pointer group">
                                <div class="w-8 h-8 rounded-md bg-gray-100 flex items-center justify-center text-gray-500 group-hover:text-[#B91C1C] group-hover:bg-red-100 transition-colors">
                                    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                        <circle cx="12" cy="12" r="10"></circle>
                                        <line x1="2" y1="12" x2="22" y2="12"></line>
                                        <path d="M12 2a15.3 15.3 0 0 1 4 10 15.3 15.3 0 0 1-4 10 15.3 15.3 0 0 1-4-10 15.3 15.3 0 0 1 4-10z"></path>
                                    </svg>
                                </div>
                                <div>
                                    <span class="block text-[10px] text-gray-400 uppercase tracking-wider">Place of Origin:</span>
                                    <span class="block text-xs font-semibold text-gray-700">Thailand</span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Key Ingredients -->
                    <div class="mb-6">
                        <h4 class="text-xs font-bold text-gray-500 uppercase tracking-wider mb-2">Key Ingredients</h4>
                        <div class="flex flex-wrap gap-2">
                            <span class="px-3 py-1.5 bg-[#dbeafe] text-sky-800 text-xs font-semibold rounded-lg border border-sky-200">Liquid Nano Glutathione</span>
                            <span class="px-3 py-1.5 bg-green-100 text-green-800 text-xs font-semibold rounded-lg border border-green-200">Vitamin C</span>
                        </div>
                    </div>

                    <!-- Price -->
                    <div class="flex items-end gap-3 mb-6 items-center">
                        <span class="text-3xl font-bold text-[#000]">Rs. 4,400</span>
                    </div>

                    <!-- Add to Cart & Wholesale Enquiry -->
                    <div class="flex flex-col sm:flex-row gap-4 mb-6">
                        <div class="flex items-center border border-gray-200 rounded-lg overflow-hidden">
                            <button class="px-4 py-3 text-gray-600 hover:bg-gray-100 text-xl font-bold">&#8722;</button>
                            <input type="number" value="1" class="w-6 text-center border-0 focus:ring-0 text-lg font-semibold ps-1" />
                            <button class="px-4 py-3 text-gray-600 hover:bg-gray-100 text-xl font-bold">+</button>
                        </div>
                        <button class="flex-1 flex items-center justify-center gap-2 bg-[#B91C1C] text-white py-4 px-2 rounded-lg font-semibold hover:bg-red-700 transition-colors shadow-lg">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                <circle cx="9" cy="21" r="1" />
                                <circle cx="20" cy="21" r="1" />
                                <path d="M1 1h4l2.68 13.39a2 2 0 0 0 2 1.61h9.72a2 2 0 0 0 2-1.61L23 6H6" />
                            </svg>
                            Add to Cart
                        </button>
                        <button onclick="openWholesaleModal()" class="flex-1 flex items-center justify-center gap-2 bg-[#0F172A] text-white py-3 px-6 rounded-lg font-semibold hover:bg-[#1E293B] transition-colors border border-gray-800 shadow-lg">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                <path d="M22 16.92v3a2 2 0 0 1-2.18 2 19.79 19.79 0 0 1-8.63-3.07 19.5 19.5 0 0 1-6-6 19.79 19.79 0 0 1-3.07-8.67A2 2 0 0 1 4.11 2h3a2 2 0 0 1 2 1.72 12.84 12.84 0 0 0 .7 2.81 2 2 0 0 1-.45 2.11L8.09 9.91a16 16 0 0 0 6 6l1.27-1.27a2 2 0 0 1 2.11-.45 12.84 12.84 0 0 0 2.81.7A2 2 0 0 1 22 16.92z" />
                            </svg>
                            Wholesale Enquiry
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Product Tabs Section -->
    <section class="pb-10 product-tab">
        <div class="max-w-7xl mx-auto px-4">
            <!-- Tab Navigation -->
            <div class="sticky top-[120px] z-20 p-2 mb-8 overflow-x-auto scrollbar-hide border-b bg-white">
                <div class="flex gap-1 min-w-max">
                    <button class="tab-btn active" data-tab="description">Product Description</button>
                    <button class="tab-btn" data-tab="benefits">Benefits</button>
                    <button class="tab-btn" data-tab="ingredients">Ingredients</button>
                    <button class="tab-btn" data-tab="usage">Usage</button>
                    <button class="tab-btn" data-tab="faq">FAQs</button>
                    <button class="tab-btn" data-tab="enquiry">Quick Enquiry</button>
                </div>
            </div>

            <div class="bg-white rounded-xl shadow-sm border border-gray-100 px-0 md:px-6 py-8 pt-0">

                <!-- 1. Description -->
                <div id="description" class="tab-pane pt-0">
                    <div class="editor-content">
                        <h2>Vesco Pharma Gluta C 1000 Glutathione Skin Whitening Injection</h2>
                        <p>Vesco Pharma's Gluta C 1000 Glutathione Skin Whitening Injections are enriched with high-concentration Glutathione and Vitamin C. This powerful formula is designed to brighten skin, reduce pigmentation, and promote a youthful glow. It works by inhibiting melanin production, resulting in a lighter and more even skin tone.</p>
                        <p>Manufactured under strict quality control standards, these injections ensure maximum efficacy and safety. Suitable for all skin types.</p>
                        <h3>Key Highlights</h3>
                        <ul>
                            <li>High Quality Glutathione (1000mg)</li>
                            <li>Enriched with Vitamin C for better absorption</li>
                            <li>Visible results within weeks</li>
                        </ul>
                        <h3>What Makes Vesco Pharma Gluta C 1000 Special?</h3>
                        <p>Vesco Pharma's Gluta C 1000 is a powerful blend of Liquid Nano Glutathione and Vitamin C, designed to improve your skin's health and appearance. This product is available in a package containing 10 ampoules, each with 1000mg of Liquid Nano Glutathione and 1000mg of Vitamin C.</p>
                        <p>The injections can be administered intravenously (IV) or intramuscularly (IM), depending on your preference or medical advice. The combination of Glutathione and Vitamin C works synergistically to deliver impressive skin whitening results.</p>
                    </div>
                </div>

                <div class="border-t border-gray-100 my-8 mb-0"></div>

                <!-- 2. Benefits -->
                <div id="benefits" class="tab-pane pt-0">
                    <div class="editor-content">
                        <h2>Key Benefits</h2>
                        <ul>
                            <li><strong>Skin Whitening:</strong> The antioxidant properties of Glutathione detoxify your body, eliminating free radicals.</li>
                            <li><strong>Anti-Aging:</strong> Reduces fine lines and wrinkles by boosting collagen production.</li>
                            <li><strong>Detoxification:</strong> Glutathione acts as a powerful antioxidant, detoxifying the liver and body.</li>
                            <li><strong>UV Protection:</strong> Helps in repairing skin damage caused by sun exposure.</li>
                            <li><strong>Even Skin Tone:</strong> Reduces dark spots, acne scars, and hyperpigmentation.</li>
                        </ul>
                    </div>
                </div>

                <div class="border-t border-gray-100 my-8 mb-0"></div>

                <!-- 3. Ingredients -->
                <div id="ingredients" class="tab-pane pt-0">
                    <div class="editor-content">
                        <h2>Ingredients List</h2>
                        <ol>
                            <li>Glutathione 1000mg</li>
                            <li>Vitamin C (Ascorbic Acid) 500mg</li>
                            <li>Thioctic Acid</li>
                            <li>Epidermal Growth Factor (EGF)</li>
                            <li>Alpha Lipoic Acid</li>
                        </ol>
                        <p>* Please consult the packaging or your healthcare provider for the complete list of excipients.</p>
                    </div>
                </div>

                <div class="border-t border-gray-100 my-8 mb-0"></div>

                <!-- 4. Usage -->
                <div id="usage" class="tab-pane pt-0">
                    <div class="editor-content">
                        <h2>How to Use</h2>
                        <ol>
                            <li>Consult a dermatologist or healthcare professional before use.</li>
                            <li>Administer via intravenous (IV) or intramuscular (IM) injection.</li>
                            <li>Recommended frequency is once or twice a week for best results.</li>
                            <li>Maintain a healthy diet and hydration levels during the course.</li>
                            <li>Avoid sun exposure immediately after the session; use sunscreen.</li>
                        </ol>
                    </div>
                </div>

                <div class="border-t border-gray-100 my-8 mb-0"></div>

                <!-- 5. FAQ -->
                <div id="faq" class="tab-pane pt-0">
                    <h2 class="text-2xl section-title font-bold text-[#0F172A] mb-6">Frequently Asked Questions</h2>
                    <div class="space-y-4">
                        <div class="faq-item border border-gray-200 rounded-xl overflow-hidden">
                            <button class="faq-question w-full flex items-center justify-between p-5 text-left font-semibold text-[#0F172A] hover:bg-gray-50 transition-colors">
                                <span>Is it safe to use Glutathione injections?</span>
                                <svg class="faq-icon w-5 h-5 transform transition-transform" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
                                </svg>
                            </button>
                            <div class="faq-answer hidden px-5 pb-5 text-[#64748B]">
                                Yes, Glutathione is a naturally occurring antioxidant in the body. However, it is crucial to use high-quality products from reputable sellers like Tamaz Global and consult a doctor before starting any treatment.
                            </div>
                        </div>
                        <div class="faq-item border border-gray-200 rounded-xl overflow-hidden">
                            <button class="faq-question w-full flex items-center justify-between p-5 text-left font-semibold text-[#0F172A] hover:bg-gray-50 transition-colors">
                                <span>How many sessions are required to see results?</span>
                                <svg class="faq-icon w-5 h-5 transform transition-transform" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
                                </svg>
                            </button>
                            <div class="faq-answer hidden px-5 pb-5 text-[#64748B]">
                                Results vary based on individual metabolism and skin type. Generally, visible changes are noticed after 4 to 8 sessions. Consistency and proper dosage are key.
                            </div>
                        </div>
                        <div class="faq-item border border-gray-200 rounded-xl overflow-hidden">
                            <button class="faq-question w-full flex items-center justify-between p-5 text-left font-semibold text-[#0F172A] hover:bg-gray-50 transition-colors">
                                <span>Are there any side effects?</span>
                                <svg class="faq-icon w-5 h-5 transform transition-transform" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
                                </svg>
                            </button>
                            <div class="faq-answer hidden px-5 pb-5 text-[#64748B]">
                                When administered correctly, side effects are rare. Some individuals might experience mild digestive issues or allergic reactions. Always get tested for allergies first.
                            </div>
                        </div>
                        <div class="faq-item border border-gray-200 rounded-xl overflow-hidden">
                            <button class="faq-question w-full flex items-center justify-between p-5 text-left font-semibold text-[#0F172A] hover:bg-gray-50 transition-colors">
                                <span>Do Vesco Pharma Gluta C 1000 Injections affect aging?</span>
                                <svg class="faq-icon w-5 h-5 transform transition-transform" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
                                </svg>
                            </button>
                            <div class="faq-answer hidden px-5 pb-5 text-[#64748B]">
                                As a powerful antioxidant, Glutathione helps reduce signs of aging like wrinkles and fine lines.
                            </div>
                        </div>
                    </div>
                </div>

                <div class="border-t border-gray-100 my-8 mb-0"></div>

                <!-- 6. Enquiry Form -->
                <div id="enquiry" class="tab-pane pt-4">
                    <div class="grid lg:grid-cols-2 gap-10">
                        <div>
                            <h2 class="text-2xl section-title font-bold text-[#0F172A] mb-2">Quick Enquiry</h2>
                            <p class="text-[#64748B] mb-6">Fill out the form below and we will get back to you shortly.</p>
                            <div class="space-y-4">
                                <div class="grid sm:grid-cols-2 gap-4">
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">Name *</label>
                                        <asp:TextBox ID="txtEnqName" runat="server" placeholder="Your Name" CssClass="w-full border border-gray-200 rounded-lg px-4 py-3 focus:ring-2 focus:ring-red-100 focus:border-[#B91C1C] outline-none transition-all" />
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">City</label>
                                        <asp:TextBox ID="txtEnqCity" runat="server" placeholder="Your City" CssClass="w-full border border-gray-200 rounded-lg px-4 py-3 focus:ring-2 focus:ring-red-100 focus:border-[#B91C1C] outline-none transition-all" />
                                    </div>
                                </div>
                                <div class="grid sm:grid-cols-2 gap-4">
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">Phone *</label>
                                        <asp:TextBox ID="txtEnqPhone" runat="server" TextMode="Phone" placeholder="+91 XXXXX XXXXX" CssClass="w-full border border-gray-200 rounded-lg px-4 py-3 focus:ring-2 focus:ring-red-100 focus:border-[#B91C1C] outline-none transition-all" />
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">Email</label>
                                        <asp:TextBox ID="txtEnqEmail" runat="server" TextMode="Email" placeholder="email@example.com" CssClass="w-full border border-gray-200 rounded-lg px-4 py-3 focus:ring-2 focus:ring-red-100 focus:border-[#B91C1C] outline-none transition-all" />
                                    </div>
                                </div>
                                <div>
                                    <label class="block text-sm font-medium text-gray-700 mb-1">Message</label>
                                    <asp:TextBox ID="txtEnqMessage" runat="server" TextMode="MultiLine" Rows="4" placeholder="Write your message..." CssClass="w-full border border-gray-200 rounded-lg px-4 py-3 focus:ring-2 focus:ring-red-100 focus:border-[#B91C1C] outline-none transition-all resize-none" />
                                </div>
                                <div class="flex items-center gap-3">
                                    <span class="text-gray-600 font-medium">6 + 1 = ?</span>
                                    <asp:TextBox ID="txtCaptcha" runat="server" CssClass="w-24 border border-gray-200 rounded-lg px-4 py-2 focus:ring-2 focus:ring-red-100 focus:border-[#B91C1C] outline-none" placeholder="?" />
                                </div>
                                <asp:Button ID="btnEnquiry" runat="server" Text="Send Now" CssClass="w-full sm:w-auto bg-[#0F172A] text-white font-semibold px-10 py-3 rounded-lg hover:bg-[#1E293B] transition-colors shadow-lg" OnClick="btnEnquiry_Click" />
                            </div>
                        </div>

                        <!-- Contact Info Sidebar -->
                        <div class="rounded-2xl p-8 border border-gray-100 flex flex-col contact-info-block">
                            <h3 class="text-xl font-bold text-[#0F172A] mb-6">Contact Information</h3>
                            <div class="space-y-6">
                                <div class="flex items-start gap-4">
                                    <div class="w-12 h-12 bg-[#EFF6FF] br-12 flex items-center justify-center flex-shrink-0">
                                        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#1E3A8A" stroke-width="2">
                                            <path d="M22 16.92v3a2 2 0 0 1-2.18 2 19.79 19.79 0 0 1-8.63-3.07 19.5 19.5 0 0 1-6-6 19.79 19.79 0 0 1-3.07-8.67A2 2 0 0 1 4.11 2h3a2 2 0 0 1 2 1.72 12.84 12.84 0 0 0 .7 2.81 2 2 0 0 1-.45 2.11L8.09 9.91a16 16 0 0 0 6 6l1.27-1.27a2 2 0 0 1 2.11-.45 12.84 12.84 0 0 0 2.81.7A2 2 0 0 1 22 16.92z" />
                                        </svg>
                                    </div>
                                    <div>
                                        <h4 class="font-semibold text-[#0F172A]">Retail Orders</h4>
                                        <p class="text-sm text-gray-500">Dr. Aliya</p>
                                        <a href="tel:+919988227622" class="font-semibold hover:underline">+91 99882 27622</a>
                                    </div>
                                </div>
                                <div class="flex items-start gap-4">
                                    <div class="w-12 h-12 bg-[#EFF6FF] br-12 flex items-center justify-center flex-shrink-0">
                                        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#1E3A8A" stroke-width="2">
                                            <path d="M22 16.92v3a2 2 0 0 1-2.18 2 19.79 19.79 0 0 1-8.63-3.07 19.5 19.5 0 0 1-6-6 19.79 19.79 0 0 1-3.07-8.67A2 2 0 0 1 4.11 2h3a2 2 0 0 1 2 1.72 12.84 12.84 0 0 0 .7 2.81 2 2 0 0 1-.45 2.11L8.09 9.91a16 16 0 0 0 6 6l1.27-1.27a2 2 0 0 1 2.11-.45 12.84 12.84 0 0 0 2.81.7A2 2 0 0 1 22 16.92z" />
                                        </svg>
                                    </div>
                                    <div>
                                        <h4 class="font-semibold text-[#0F172A]">Wholesale Orders</h4>
                                        <p class="text-sm text-gray-500">Mr. Aby</p>
                                        <a href="tel:+919900746748" class="font-semibold hover:underline">+91 99007 46748</a>
                                    </div>
                                </div>
                                <div class="flex items-start gap-4">
                                    <div class="w-12 h-12 bg-[#EFF6FF] br-12 flex items-center justify-center flex-shrink-0">
                                        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#1E3A8A" stroke-width="2">
                                            <path d="M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z" />
                                            <polyline points="22,6 12,13 2,6" />
                                        </svg>
                                    </div>
                                    <div>
                                        <h4 class="font-semibold text-[#0F172A]">Email Us</h4>
                                        <a href="mailto:sales@tamazglobal.com" class="font-semibold hover:underline">sales@tamazglobal.com</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Wholesale Enquiry Modal -->
    <div id="wholesaleModal" class="fixed inset-0 z-50 hidden items-center justify-center bg-black/50 backdrop-blur-sm transition-opacity duration-300">
        <div class="bg-white rounded-2xl shadow-2xl max-w-md w-full mx-4 relative transform transition-all duration-300 scale-95 opacity-0" id="modalContent">
            <button onclick="closeWholesaleModal()" class="absolute top-4 right-4 text-gray-400 hover:text-gray-600 transition-colors">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                    <line x1="18" y1="6" x2="6" y2="18"></line>
                    <line x1="6" y1="6" x2="18" y2="18"></line>
                </svg>
            </button>
            <div class="p-8 pb-0 text-center">
                <h2 class="text-2xl section-title font-bold text-[#0F172A]">Get Whole Sale Enquiry</h2>
                <p class="text-sm text-gray-500 mt-2">Fill in the details below and we will get back to you shortly.</p>
            </div>
            <div class="p-8 pt-6 space-y-4">
                <asp:TextBox ID="txtWsName" runat="server" placeholder="Your Name" CssClass="w-full px-4 py-3 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-red-100 focus:border-[#B91C1C] transition-all text-sm" />
                <asp:TextBox ID="txtWsCity" runat="server" placeholder="City" CssClass="w-full px-4 py-3 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-red-100 focus:border-[#B91C1C] transition-all text-sm" />
                <asp:TextBox ID="txtWsPhone" runat="server" TextMode="Phone" placeholder="Phone" CssClass="w-full px-4 py-3 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-red-100 focus:border-[#B91C1C] transition-all text-sm" />
                <asp:TextBox ID="txtWsEmail" runat="server" TextMode="Email" placeholder="Your Email" CssClass="w-full px-4 py-3 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-red-100 focus:border-[#B91C1C] transition-all text-sm" />
                <asp:TextBox ID="txtWsMessage" runat="server" TextMode="MultiLine" Rows="3" placeholder="Your Message" CssClass="w-full px-4 py-3 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-red-100 focus:border-[#B91C1C] transition-all text-sm resize-none" />
                <asp:Button ID="btnWsSubmit" runat="server" Text="Send Now" CssClass="w-full btn-primary text-center hover:bg-blue-700 text-white font-semibold py-3 rounded-lg transition-colors shadow-lg shadow-blue-200 justify-center" OnClick="btnWsSubmit_Click" />
            </div>
        </div>
    </div>

    <!-- Image Popup Modal -->
    <div id="imageModal" class="fixed inset-0 bg-black/80 hidden items-center justify-center z-100 image-modal-container">
        <button onclick="closeModal()" class="absolute top-5 right-5 text-white text-3xl">&times;</button>
        <button onclick="prevImage()" class="absolute left-5 text-white text-3xl">&#10094;</button>
        <img id="modalImage" class="max-w-[90%] max-h-[90%] rounded-lg shadow-lg" />
        <button onclick="nextImage()" class="absolute right-5 text-white text-3xl">&#10095;</button>
    </div>

    <!-- WhatsApp Floating Button -->
    <a href="https://wa.me/92300123456" target="_blank" class="fixed bottom-6 right-6 w-14 h-14 bg-green-500 rounded-full flex items-center justify-center shadow-2xl shadow-green-500/30 hover:scale-110 transition-transform z-50">
        <span class="iconify w-7 h-7 text-white" data-icon="logos:whatsapp-icon"></span>
    </a>

</asp:Content>

<asp:Content ID="ScriptsContent" ContentPlaceHolderID="scripts" runat="server">
    <script src="https://code.iconify.design/3/3.1.0/iconify.min.js"></script>
    <script>
        // Image Gallery
        const images = [
            "assests/Images/old-product/p-1.jpg",
            "assests/Images/old-product/p-2.jpg",
            "assests/Images/old-product/p-3.jpg",
            "assests/Images/old-product/p-4.jpg"
        ];
        let currentIndex = 0;

        function changeImage(index) {
            currentIndex = index;
            document.getElementById("mainProductImage").src = images[index];
            document.querySelectorAll(".thumb-btn").forEach((btn, i) => {
                btn.classList.toggle("border-[#B91C1C]", i === index);
                btn.classList.toggle("border-transparent", i !== index);
            });
        }

        function openModal(index) {
            currentIndex = index;
            document.getElementById("imageModal").classList.remove("hidden");
            document.getElementById("imageModal").classList.add("flex");
            updateModalImage();
        }

        function closeModal() {
            document.getElementById("imageModal").classList.add("hidden");
            document.getElementById("imageModal").classList.remove("flex");
        }

        function updateModalImage() {
            document.getElementById("modalImage").src = images[currentIndex];
        }

        function nextImage() {
            currentIndex = (currentIndex + 1) % images.length;
            updateModalImage();
        }

        function prevImage() {
            currentIndex = (currentIndex - 1 + images.length) % images.length;
            updateModalImage();
        }

        // Tabs - smooth scroll
        document.querySelectorAll(".tab-btn").forEach((button) => {
            button.addEventListener("click", function () {
                const targetId = this.getAttribute("data-tab");
                const target = document.getElementById(targetId);
                if (target) {
                    const headerOffset = 190;
                    const elementPosition = target.getBoundingClientRect().top + window.pageYOffset;
                    window.scrollTo({ top: elementPosition - headerOffset, behavior: "smooth" });
                }
                document.querySelectorAll(".tab-btn").forEach(btn => btn.classList.remove("active"));
                this.classList.add("active");
            });
        });

        // FAQ Accordion
        document.querySelectorAll(".faq-question").forEach((button) => {
            button.addEventListener("click", () => {
                button.nextElementSibling.classList.toggle("hidden");
                button.querySelector(".faq-icon").classList.toggle("rotate-180");
            });
        });

        // Wholesale Modal
        const wholesaleModal = document.getElementById("wholesaleModal");
        const modalContent = document.getElementById("modalContent");

        function openWholesaleModal() {
            wholesaleModal.classList.remove("hidden");
            wholesaleModal.classList.add("flex");
            setTimeout(() => {
                wholesaleModal.classList.add("opacity-100");
                modalContent.classList.remove("scale-95", "opacity-0");
                modalContent.classList.add("scale-100", "opacity-100");
            }, 10);
            document.body.style.overflow = "hidden";
        }

        function closeWholesaleModal() {
            modalContent.classList.remove("scale-100", "opacity-100");
            modalContent.classList.add("scale-95", "opacity-0");
            setTimeout(() => {
                wholesaleModal.classList.add("hidden");
                wholesaleModal.classList.remove("flex", "opacity-100");
                document.body.style.overflow = "auto";
            }, 300);
        }

        wholesaleModal.addEventListener("click", (e) => {
            if (e.target === wholesaleModal) closeWholesaleModal();
        });
    </script>
</asp:Content>
