<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="ProductDetails_Page" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
        .scrollbar-hide::-webkit-scrollbar {
            display: none;
        }

        .scrollbar-hide {
            -ms-overflow-style: none;
            scrollbar-width: none;
        }

        /* Thumbnail active state */
        .thumb-btn.active-thumb {
            border-color: #B91C1C !important;
        }

        /* Gallery thumbnail grid */
        .thumb-grid {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 10px;
            margin-top: 12px;
        }

            .thumb-grid button {
                border-radius: 10px;
                overflow: hidden;
                border: 2px solid transparent;
                transition: border-color 0.2s;
                cursor: pointer;
                background: none;
                padding: 0;
            }

                .thumb-grid button:hover,
                .thumb-grid button.active-thumb {
                    border-color: #B91C1C;
                }

                .thumb-grid button img {
                    width: 100%;
                    height: 80px;
                    object-fit: cover;
                    display: block;
                }


        /* Wholesale modal */
        #wholesaleModal {
            display: none;
            position: fixed;
            inset: 0;
            z-index: 9999;
            background: rgba(0,0,0,0.5);
            backdrop-filter: blur(4px);
            align-items: center;
            justify-content: center;
        }

            #wholesaleModal.open {
                display: flex;
            }

            #wholesaleModal #modalContent {
                transform: scale(0.95);
                opacity: 0;
                transition: transform 0.3s ease, opacity 0.3s ease;
            }

            #wholesaleModal.open #modalContent {
                transform: scale(1);
                opacity: 1;
            }

        .thumb-grid {
            display: flex;
            gap: 10px;
            margin-top: 12px;
            overflow-x: auto;
            flex-direction: row;
            padding-bottom: 10px;
            scroll-behavior: smooth;
            scrollbar-width: thin;
            scrollbar-color: transparent transparent;
        }


            .thumb-grid::-webkit-scrollbar {
                height: 4px;
            }



            .thumb-grid:hover::-webkit-scrollbar-thumb,
            .thumb-grid:active::-webkit-scrollbar-thumb {
                background: #bbb;
            }

            .thumb-grid > * {
                flex: 0 0 100px;
            }
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input type="hidden" id="hdnProductName" value="<%=strProductName %>" />
    <input type="hidden" id="hdnProductId" value="<%=strProductId %>" />

    <!-- Breadcrumb -->
    <section class="bg-[#f8fafc] py-3 border-b border-gray-200">
        <div class="max-w-7xl mx-auto px-4">
            <nav class="text-sm text-[#64748B] breadcrumb">
                <a href="Default.aspx" class="hover:text-[#B91C1C]">Home</a>
                <span class="mx-2">/</span>
                <a href="/Category/<%=strCategoryUrl %>" class="hover:text-[#B91C1C]"><%=strCategory %></a>
                <span class="mx-2">/</span>
                <span class="text-[#0F172A] font-medium"><%=strProductName %></span>
            </nav>
        </div>
    </section>

    <!-- Product Top Section -->
    <section class="py-8 md:py-12 bg-white">
        <div class="max-w-7xl mx-auto px-4">
            <div class="grid grid-cols-1 md:grid-cols-2 gap-10">

                <!-- Left: Main Image + Thumbnails -->
                <div class="flex flex-col gap-3">
                    <!-- Main Image -->
                    <div class="relative w-full aspect-square bg-gray-50 rounded-2xl overflow-hidden border border-gray-100 group">
                        <img id="mainProductImage"
                            src="/<%=strSmallImage %>"
                            alt="<%=strProductName %>"
                            onclick="openImageModal(0)"
                            class="object-contain w-full h-full transition-transform duration-500 group-hover:scale-105 cursor-pointer" />
                        <%=strLabelBadge %>

                        <%-- Prev Arrow --%>
                        <button type="button" onclick="navigateGallery(-1)"
                            class="absolute left-3 top-1/2 -translate-y-1/2 w-9 h-9 bg-white/80 hover:bg-white rounded-full shadow-md flex items-center justify-center z-10 transition-all hover:scale-110">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="#0F172A" stroke-width="2.5">
                                <polyline points="15 18 9 12 15 6" />
                            </svg>
                        </button>

                        <%-- Next Arrow --%>
                        <button type="button" onclick="navigateGallery(1)"
                            class="absolute right-3 top-1/2 -translate-y-1/2 w-9 h-9 bg-white/80 hover:bg-white rounded-full shadow-md flex items-center justify-center z-10 transition-all hover:scale-110">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="#0F172A" stroke-width="2.5">
                                <polyline points="9 18 15 12 9 6" />
                            </svg>
                        </button>
                    </div>
                    <%--                   <div class="relative w-full aspect-square bg-gray-50 rounded-2xl overflow-hidden border border-gray-100 group">
                        <img id="mainProductImage"
                            src="/<%=strSmallImage %>"
                            alt="<%=strProductName %>"
                            onclick="openImageModal(0)"
                            class="object-contain w-full h-full transition-transform duration-500 group-hover:scale-105 cursor-pointer" />
                        <%=strLabelBadge %>
                    </div>--%>

                    <!-- Thumbnails — loaded from gallery via AJAX + thumb image as first item -->
                    <div class="thumb-grid" id="thumbGrid">
                        <%-- First thumb is always the main product image --%>
                        <button type="button" class="active-thumb thumb-btn" onclick="switchMainImage('/<%=strSmallImage %>',this)">
                            <img src="/<%=strSmallImage %>" alt="Main" />
                        </button>
                    </div>
                </div>

                <!-- Right: Info -->
                <div class="flex flex-col">
                    <div class="flex items-center mb-4 flex-wrap gap-3">
                        <span class="text-xs font-bold text-[#1E3A8A] uppercase tracking-wider"><%=strBrand %></span>
                        <%=strAvailBadge %>
                    </div>

                    <h1 class="text-2xl section-title md:text-3xl font-bold text-[#0F172A] mb-3"><%=strProductName %></h1>
                    <p class="text-sm md:text-base text-[#64748B] mb-6 leading-relaxed"><%=strShortDesc %></p>

                    <!-- Key Highlights -->
                    <div class="grid grid-cols-1 sm:grid-cols-3 gap-4 mb-6 extra-point-grid-detail ">
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

                    <!-- Meta -->
                    <div class="mb-6">
                        <div class="grid grid-cols-2 sm:grid-cols-3 gap-2 sm:gap-3 extra-point-grid-detail">
                            <div class="flex items-center gap-2 p-3 rounded-lg hover:bg-red-50/30 transition-colors group">
                                <div class="w-8 h-8 rounded-md bg-gray-100 flex items-center justify-center text-gray-500 group-hover:text-[#B91C1C] group-hover:bg-red-100 transition-colors">
                                    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                        <path d="M22 19a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h5l2 3h9a2 2 0 0 1 2 2z"></path>
                                    </svg>
                                </div>
                                <div>
                                    <span class="block text-[10px] text-gray-400 uppercase tracking-wider">Category</span>
                                    <span class="block text-xs font-semibold text-gray-700"><%=strCategory %></span>
                                </div>
                            </div>
                            <div class="flex items-center gap-2 p-3 rounded-lg hover:bg-red-50/30 transition-colors group">
                                <div class="w-8 h-8 rounded-md bg-gray-100 flex items-center justify-center text-gray-500 group-hover:text-[#B91C1C] group-hover:bg-red-100 transition-colors">
                                    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                        <circle cx="12" cy="12" r="10"></circle>
                                        <line x1="2" y1="12" x2="22" y2="12"></line>
                                        <path d="M12 2a15.3 15.3 0 0 1 4 10 15.3 15.3 0 0 1-4 10 15.3 15.3 0 0 1-4-10 15.3 15.3 0 0 1 4-10z"></path>
                                    </svg>
                                </div>
                                <div>
                                    <span class="block text-[10px] text-gray-400 uppercase tracking-wider">Place of Origin</span>
                                    <span class="block text-xs font-semibold text-gray-700"><%=strPlaceOfOrigin %></span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Key Ingredients -->
                    <div class="mb-6">
                        <h4 class="text-xs font-bold text-gray-500 uppercase tracking-wider mb-2">Key Ingredients</h4>
                        <div class="flex flex-wrap gap-2"><%=strIngredientTags %></div>
                    </div>

                    <!-- Price -->
                    <div class="flex items-center gap-3 mb-6">
                        <span class="text-3xl font-bold text-[#000] fs-24-mobile">
                            <% if (!string.IsNullOrEmpty(strRetailPrice))
                                { %>
            Rs. <%=strRetailPrice %>
                            <% }
                                else
                                { %>
                            <a href="/ContactUs.aspx" class="text-3xl font-bold text-[#000] fs-24-mobile">Contact Us
                            </a>
                            <% } %>
                        </span>
                    </div>

                    <!-- Actions -->
                    <div class="flex flex-col sm:flex-row gap-4 mb-6">
                        <div class="flex items-center border border-gray-200 rounded-lg overflow-hidden item-to-cart-count">
                            <button type="button" class="px-4 py-3 text-gray-600 hover:bg-gray-100 text-xl font-bold" onclick="decreaseQty()">&#8722;</button>
                            <input type="number" id="productQty" value="1" min="1" class="w-10 text-center border-0 focus:ring-0 text-lg font-semibold" />
                            <button type="button" class="px-4 py-3 text-gray-600 hover:bg-gray-100 text-xl font-bold" onclick="increaseQty()">+</button>
                        </div>
                        <button type="button" id="btnAddToCart"
                            onclick="addToCartProduct(this)"
                            class="flex-1 flex items-center justify-center gap-2 bg-[#B91C1C] text-white py-4 px-2 rounded-lg font-semibold hover:bg-red-700 transition-colors shadow-lg">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                <circle cx="9" cy="21" r="1" />
                                <circle cx="20" cy="21" r="1" />
                                <path d="M1 1h4l2.68 13.39a2 2 0 0 0 2 1.61h9.72a2 2 0 0 0 2-1.61L23 6H6" />
                            </svg>
                            <span id="btnAddToCartText">Add to Cart</span>
                        </button>
                        <%-- FIX: onclick calls openWholesaleModal() defined below --%>
                        <button type="button" onclick="openWholesaleModal()" class="flex-1 flex items-center justify-center gap-2 bg-[#0F172A] text-white py-3 px-6 rounded-lg font-semibold hover:bg-[#1E293B] transition-colors shadow-lg">
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

    <!-- ===== TABS ===== -->
    <section class="pb-10 product-tab">
        <div class="max-w-7xl mx-auto px-4">
            <div class="sticky top-[120px] z-20 p-2 mb-8 overflow-x-auto scrollbar-hide border-b bg-white">
                <div class="flex gap-1 min-w-max">
                    <button type="button" class="tab-btn active" data-tab="description">Product Description</button>
                    <button type="button" class="tab-btn" data-tab="benefits">Benefits</button>
                    <button type="button" class="tab-btn" data-tab="ingredients">Ingredients</button>
                    <button type="button" class="tab-btn" data-tab="usage">Usage</button>
                    <button type="button" class="tab-btn" data-tab="faq">FAQ's</button>
                    <button type="button" class="tab-btn" data-tab="enquiry">Quick Enquiry</button>
                </div>
            </div>

            <div class="bg-white rounded-xl shadow-sm border border-gray-100 px-0 md:px-6 py-8 pt-0">

                <div id="description" class="tab-pane pt-0">
                    <div class="editor-content"><%=strFullDesc %></div>
                </div>

                <div class="border-t border-gray-100 my-8 mb-0"></div>

                <div id="benefits" class="tab-pane pt-0">
                    <div class="editor-content"><%=strBenefitsDesc %></div>
                </div>

                <div class="border-t border-gray-100 my-8 mb-0"></div>

                <div id="ingredients" class="tab-pane pt-0">
                    <div class="editor-content"><%=strIngredientsDesc %></div>
                </div>

                <div class="border-t border-gray-100 my-8 mb-0"></div>

                <div id="usage" class="tab-pane pt-0">
                    <div class="editor-content"><%=strUsageDesc %></div>
                </div>

                <div class="border-t border-gray-100 my-8 mb-0"></div>

                <!-- FAQ Section — rendered server-side -->
                <div id="faq" class="tab-pane pt-4">
                    <h2 class="text-2xl section-title font-bold text-[#0F172A] mb-6">Frequently Asked Questions</h2>
                    <div class="space-y-4" id="faqContainer">
                        <%=strProdFaqs %>
                    </div>
                </div>

                <div class="border-t border-gray-100 my-8 mb-0"></div>

                <!-- Quick Enquiry -->
                <div id="enquiry" class="tab-pane pt-4">
                    <div class="grid lg:grid-cols-2 gap-10">
                        <div>
                            <h2 class="text-2xl section-title font-bold text-[#0F172A] mb-2">Quick Enquiry</h2>
                            <p class="text-[#64748B] mb-6">Fill out the form below and we will get back to you shortly.</p>
                            <div class="space-y-4">
                                <div class="grid sm:grid-cols-2 gap-4">
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">Name *</label>
                                        <input type="text" id="txtEnqName" placeholder="Your Name" class="w-full border border-gray-200 rounded-lg px-4 py-3 outline-none transition-all" />
                                        <span class="text-red-500 text-xs" id="errEnqName"></span>
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">City</label>
                                        <input type="text" id="txtEnqCity" placeholder="Your City" class="w-full border border-gray-200 rounded-lg px-4 py-3 outline-none transition-all"  />
                                        <span class="text-red-500 text-xs" id="errEnqCity"></span>
                                    </div>
                                </div>
                                <div class="grid sm:grid-cols-2 gap-4">
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">Phone *</label>
                                        <input type="tel" id="txtEnqPhone" placeholder="+91 XXXXX XXXXX" maxlength="15" class="w-full border border-gray-200 rounded-lg px-4 py-3 outline-none transition-all" oninput="this.value = this.value.replace(/[^0-9+]/g, '')"/>
                                        <span class="text-red-500 text-xs" id="errEnqPhone"></span>
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">Email</label>
                                        <input type="email" id="txtEnqEmail" placeholder="email@example.com" class="w-full border border-gray-200 rounded-lg px-4 py-3 outline-none transition-all" />
                                        <span class="text-red-500 text-xs" id="errEnqEmail"></span>
                                    </div>
                                </div>
                                <div>
                                    <label class="block text-sm font-medium text-gray-700 mb-1">Message</label>
                                    <textarea id="txtEnqMessage" rows="4" placeholder="Write your message..." class="w-full border border-gray-200 rounded-lg px-4 py-3 outline-none resize-none"></textarea>
                                    <span class="text-red-500 text-xs" id="errEnqMessage"></span>
                                </div>
                                <div class="flex items-center gap-3">
                                    <span class="text-gray-600 font-medium" id="captchaLabel"></span>
                                    <input type="text" id="txtCaptcha" class="w-24 border border-gray-200 rounded-lg px-4 py-2 outline-none" placeholder="?" />
                                    <span class="text-red-500 text-xs" id="errCaptcha"></span>
                                </div>
                                <button type="button" onclick="submitEnquiry()" class="w-full sm:w-auto bg-[#0F172A] text-white font-semibold px-10 py-3 rounded-lg hover:bg-[#1E293B] transition-colors shadow-lg">Send Now</button>
                                <div id="enqMsg" class="text-sm mt-2"></div>
                            </div>
                        </div>

                        <!-- Contact Sidebar -->
                        <div class="rounded-2xl p-8 border border-gray-100 flex flex-col contact-info-block why-points-card">
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


    <div id="wholesaleModal">
        <div class="bg-white rounded-2xl shadow-2xl max-w-md w-full mx-4 relative" id="modalContent">
            <button onclick="closeWholesaleModal()" class="absolute top-4 right-4 text-gray-400 hover:text-gray-600">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                    <line x1="18" y1="6" x2="6" y2="18"></line>
                    <line x1="6" y1="6" x2="18" y2="18"></line>
                </svg>
            </button>
            <div class="p-8 pb-0 text-center">
                <h2 class="text-2xl section-title font-bold text-[#0F172A]">Get Wholesale Enquiry</h2>
                <p class="text-sm text-gray-500 mt-2">Fill in the details below and we will get back to you shortly.</p>
            </div>
            <div class="p-8 pt-6 space-y-4">

                <div>
                    <input type="text" id="txtWsName" placeholder="Your Name"
                        class="w-full px-4 py-3 border border-gray-200 rounded-lg outline-none text-sm" />
                    <span class="text-red-500 text-xs block mt-1" id="errWsName"></span>
                </div>

                <div>
                    <input type="text" id="txtWsCity" placeholder="City"
                        class="w-full px-4 py-3 border border-gray-200 rounded-lg outline-none text-sm" />
                    <span class="text-red-500 text-xs block mt-1" id="errWsCity"></span>
                </div>

                <div>
                    <input type="tel" id="txtWsPhone" placeholder="Phone" maxlength="15" oninput="this.value = this.value.replace(/[^0-9+]/g, '')"
                        class="w-full px-4 py-3 border border-gray-200 rounded-lg outline-none text-sm" />
                    <span class="text-red-500 text-xs block mt-1" id="errWsPhone"></span>
                </div>

                <div>
                    <input type="email" id="txtWsEmail" placeholder="Your Email"
                        class="w-full px-4 py-3 border border-gray-200 rounded-lg outline-none text-sm" />
                    <span class="text-red-500 text-xs block mt-1" id="errWsEmail"></span>
                </div>

                <div>
                    <textarea id="txtWsMessage" placeholder="Your Message" rows="3"
                        class="w-full px-4 py-3 border border-gray-200 rounded-lg outline-none text-sm resize-none"></textarea>
                    <span class="text-red-500 text-xs block mt-1" id="errWsMessage"></span>
                </div>

                <button type="button" onclick="submitWholesale()"
                    class="w-full bg-[#0F172A] text-white font-semibold py-3 rounded-lg hover:bg-[#1E293B] transition-colors shadow-lg">
                    Send Now
                </button>

                <div id="wsMsg" class="text-sm mt-2"></div>
            </div>
        </div>
    </div>
    <div id="snackbar"
        class="fixed top-6 left-1/2 -translate-x-1/2 bg-black text-white px-6 py-3 rounded-lg shadow-lg opacity-0 pointer-events-none transition-all duration-300 z-[99999]">
        Thank you! Your enquiry has been submitted.
    </div>
    <!-- Image Zoom Modal -->
    <div id="imageModal" style="display: none; position: fixed; inset: 0; background: rgba(0,0,0,0.92); z-index: 99999; align-items: center; justify-content: center;">

        <%-- Close --%>
        <button type="button" onclick="closeImageModal()"
            style="position: absolute; top: 20px; right: 20px; color: #fff; font-size: 32px; background: none; border: none; cursor: pointer; z-index: 2;">
            &times;
        </button>

        <%-- Prev --%>
        <button type="button" onclick="navigateModal(-1)"
            style="position: absolute; left: 20px; top: 50%; transform: translateY(-50%); background: rgba(255,255,255,0.15); border: none; border-radius: 50%; width: 44px; height: 44px; display: flex; align-items: center; justify-content: center; cursor: pointer; z-index: 2;">
            <svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="#fff" stroke-width="2.5">
                <polyline points="15 18 9 12 15 6" />
            </svg>
        </button>

        <%-- Main Image --%>
        <img id="modalImage" src=""
            style="max-width: 88%; max-height: 88vh; border-radius: 10px; object-fit: contain; transition: opacity 0.2s;" />

        <%-- Counter --%>
        <div id="modalCounter"
            style="position: absolute; bottom: 20px; left: 50%; transform: translateX(-50%); color: #fff; font-size: 13px; background: rgba(0,0,0,0.4); padding: 4px 14px; border-radius: 20px;">
        </div>

        <%-- Next --%>
        <button type="button" onclick="navigateModal(1)"
            style="position: absolute; right: 20px; top: 50%; transform: translateY(-50%); background: rgba(255,255,255,0.15); border: none; border-radius: 50%; width: 44px; height: 44px; display: flex; align-items: center; justify-content: center; cursor: pointer; z-index: 2;">
            <svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="#fff" stroke-width="2.5">
                <polyline points="9 18 15 12 9 6" />
            </svg>
        </button>
    </div>
    <%--    <div id="imageModal" style="display: none; position: fixed; inset: 0; background: rgba(0,0,0,0.85); z-index: 9999; align-items: center; justify-content: center;">
        <button type="button" onclick="closeImageModal()" style="position: absolute; top: 20px; right: 20px; color: #fff; font-size: 32px; background: none; border: none; cursor: pointer;">&times;</button>
        <img id="modalImage" src="" style="max-width: 90%; max-height: 90%; border-radius: 8px;" />
    </div>--%>
</asp:Content>

<asp:Content ID="ScriptsContent" ContentPlaceHolderID="scripts" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.iconify.design/3/3.1.0/iconify.min.js"></script>
    <script src="/assests/js/pages/product.js"></script>
</asp:Content>
