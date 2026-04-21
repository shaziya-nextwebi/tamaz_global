<%@ Page Title="Shopping Cart - TAMAZ Global" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
        .reveal { opacity: 0; transform: translateY(20px); transition: all 0.8s cubic-bezier(0.16, 1, 0.3, 1); }
        .reveal.active { opacity: 1; transform: translateY(0); }
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Breadcrumb -->
    <div class="bg-[#f8fafc] py-3 border-b border-gray-200">
        <div class="max-w-7xl mx-auto px-4">
            <div class="flex items-center gap-2 text-sm text-slate-500">
                <a href="Default.aspx" class="hover:text-red-700 transition-colors">Home</a>
                <span class="iconify w-4 h-4" data-icon="lucide:chevron-right"></span>
                <span class="text-slate-900 font-medium">Shopping Cart</span>
            </div>
        </div>
    </div>

    <!-- Main Content -->
    <section class="section-padding bg-white">
        <div class="max-w-7xl mx-auto px-4">
            <div class="grid lg:grid-cols-3 gap-8">

                <!-- Cart Items List (Left Side) -->
                <div class="lg:col-span-2 reveal">
                    <div class="bg-white br-12 border border-slate-100 overflow-hidden shadow-lg">
                        <!-- Table Header -->
                        <div class="hidden md:grid grid-cols-12 gap-4 p-5 bg-slate-50 border-b border-slate-100 text-sm font-semibold text-slate-600">
                            <div class="col-span-6">Product</div>
                            <div class="col-span-2 text-center">Price</div>
                            <div class="col-span-2 text-center">Quantity</div>
                            <div class="col-span-2 text-right">Subtotal</div>
                        </div>

                        <!-- Cart Item 1 -->
                        <div class="p-5 border-b border-slate-100 hover:bg-slate-50/50 transition-colors">
                            <div class="grid grid-cols-1 md:grid-cols-12 gap-4 items-center">
                                <div class="md:col-span-6 flex items-center gap-4">
                                    <img src="assests/Images/product-img-1.jpg" alt="Product" class="w-24 h-24 object-cover rounded-lg border border-slate-100" />
                                    <div>
                                        <h3 class="font-semibold text-slate-900 mb-1 leading-tight">Miracle NAD - XEL NAD+ Injection</h3>
                                        <p class="text-sm text-slate-500 mb-2">Advanced NAD Plus Cellular Enhancement Therapy</p>
                                        <button class="text-red-700 hover:text-red-800 text-sm font-medium flex items-center gap-1 transition-colors">
                                            <span class="iconify w-4 h-4" data-icon="lucide:trash-2"></span>
                                            Remove
                                        </button>
                                    </div>
                                </div>
                                <div class="md:col-span-2 text-center">
                                    <div class="md:hidden text-sm text-slate-500 mb-1">Price:</div>
                                    <span class="font-semibold text-slate-900">4500.00</span>
                                </div>
                                <div class="md:col-span-2 flex justify-center">
                                    <div class="flex items-center border border-slate-200 rounded-lg overflow-hidden">
                                        <button class="p-2 hover:bg-slate-100 transition-colors">
                                            <span class="iconify w-4 h-4" data-icon="lucide:minus"></span>
                                        </button>
                                        <input type="text" value="1" class="w-12 text-center border-x border-slate-200 py-2 text-sm font-medium text-slate-900 focus:outline-none" />
                                        <button class="p-2 hover:bg-slate-100 transition-colors">
                                            <span class="iconify w-4 h-4" data-icon="lucide:plus"></span>
                                        </button>
                                    </div>
                                </div>
                                <div class="md:col-span-2 text-right">
                                    <div class="md:hidden text-sm text-slate-500 mb-1">Subtotal:</div>
                                    <span class="font-bold text-slate-900 text-lg">4500.00</span>
                                </div>
                            </div>
                        </div>

                        <!-- Cart Item 2 -->
                        <div class="p-5 border-b border-slate-100 hover:bg-slate-50/50 transition-colors">
                            <div class="grid grid-cols-1 md:grid-cols-12 gap-4 items-center">
                                <div class="md:col-span-6 flex items-center gap-4">
                                    <img src="assests/Images/product-img-2.jpg" alt="Product" class="w-24 h-24 object-cover rounded-lg border border-slate-100" />
                                    <div>
                                        <h3 class="font-semibold text-slate-900 mb-1 leading-tight">Vesco Pharma Gluta C 1000 Glutathione Capsules</h3>
                                        <p class="text-sm text-slate-500 mb-2">Advanced NAD Plus Cellular Enhancement Therapy</p>
                                        <button class="text-red-700 hover:text-red-800 text-sm font-medium flex items-center gap-1 transition-colors">
                                            <span class="iconify w-4 h-4" data-icon="lucide:trash-2"></span>
                                            Remove
                                        </button>
                                    </div>
                                </div>
                                <div class="md:col-span-2 text-center">
                                    <div class="md:hidden text-sm text-slate-500 mb-1">Price:</div>
                                    <span class="font-semibold text-slate-900">2500.00</span>
                                </div>
                                <div class="md:col-span-2 flex justify-center">
                                    <div class="flex items-center border border-slate-200 rounded-lg overflow-hidden">
                                        <button class="p-2 hover:bg-slate-100 transition-colors">
                                            <span class="iconify w-4 h-4" data-icon="lucide:minus"></span>
                                        </button>
                                        <input type="text" value="2" class="w-12 text-center border-x border-slate-200 py-2 text-sm font-medium text-slate-900 focus:outline-none" />
                                        <button class="p-2 hover:bg-slate-100 transition-colors">
                                            <span class="iconify w-4 h-4" data-icon="lucide:plus"></span>
                                        </button>
                                    </div>
                                </div>
                                <div class="md:col-span-2 text-right">
                                    <div class="md:hidden text-sm text-slate-500 mb-1">Subtotal:</div>
                                    <span class="font-bold text-slate-900 text-lg">5000.00</span>
                                </div>
                            </div>
                        </div>

                        <!-- Cart Item 3 -->
                        <div class="p-5 border-b border-slate-100 hover:bg-slate-50/50 transition-colors">
                            <div class="grid grid-cols-1 md:grid-cols-12 gap-4 items-center">
                                <div class="md:col-span-6 flex items-center gap-4">
                                    <img src="assests/Images/product-img-3.jpg" alt="Product" class="w-24 h-24 object-cover rounded-lg border border-slate-100" />
                                    <div>
                                        <h3 class="font-semibold text-slate-900 mb-1 leading-tight">Dr James Vitamin C 1000mg Skin Whitening Capsules</h3>
                                        <p class="text-sm text-slate-500 mb-2">Advanced NAD Plus Cellular Enhancement Therapy</p>
                                        <button class="text-red-700 hover:text-red-800 text-sm font-medium flex items-center gap-1 transition-colors">
                                            <span class="iconify w-4 h-4" data-icon="lucide:trash-2"></span>
                                            Remove
                                        </button>
                                    </div>
                                </div>
                                <div class="md:col-span-2 text-center">
                                    <div class="md:hidden text-sm text-slate-500 mb-1">Price:</div>
                                    <span class="font-semibold text-slate-900">6000.00</span>
                                </div>
                                <div class="md:col-span-2 flex justify-center">
                                    <div class="flex items-center border border-slate-200 rounded-lg overflow-hidden">
                                        <button class="p-2 hover:bg-slate-100 transition-colors">
                                            <span class="iconify w-4 h-4" data-icon="lucide:minus"></span>
                                        </button>
                                        <input type="text" value="1" class="w-12 text-center border-x border-slate-200 py-2 text-sm font-medium text-slate-900 focus:outline-none" />
                                        <button class="p-2 hover:bg-slate-100 transition-colors">
                                            <span class="iconify w-4 h-4" data-icon="lucide:plus"></span>
                                        </button>
                                    </div>
                                </div>
                                <div class="md:col-span-2 text-right">
                                    <div class="md:hidden text-sm text-slate-500 mb-1">Subtotal:</div>
                                    <span class="font-bold text-slate-900 text-lg">6000.00</span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="mt-6 flex justify-between items-center">
                        <a href="Category.aspx" class="inline-flex items-center gap-2 text-blue-primary hover:text-red-700 font-medium transition-colors">
                            <span class="iconify w-5 h-5" data-icon="lucide:arrow-left"></span>
                            Continue Shopping
                        </a>
                    </div>
                </div>

                <!-- Quick Enquiry / Cart Summary (Right Side) -->
                <div class="lg:col-span-1 reveal">
                    <div class="bg-white rounded-2xl shadow-lg border border-slate-100 p-6 sticky top-2">
                        <h2 class="text-xl font-bold text-slate-900 mb-5 flex items-center gap-2">
                            <span class="iconify w-6 h-6 text-blue-primary" data-icon="lucide:clipboard-list"></span>
                            Quick Enquiry
                        </h2>

                        <div class="space-y-4 mb-6">
                            <div class="flex justify-between text-sm border-b border-slate-100 pb-3">
                                <span class="text-slate-500">Subtotal</span>
                                <span class="font-semibold text-slate-900">&#8377;250.00</span>
                            </div>
                            <div class="flex justify-between text-sm border-b border-slate-100 pb-3">
                                <span class="text-slate-500">Shipping</span>
                                <span class="font-semibold text-slate-900">Calculated later</span>
                            </div>
                            <div class="flex justify-between text-lg pt-2">
                                <span class="font-bold text-slate-900">Total</span>
                                <span class="font-bold text-black-700">&#8377;250.00</span>
                            </div>
                        </div>

                        <!-- Enquiry Form -->
                        <div class="space-y-3">
                            <div>
                                <asp:TextBox ID="txtName" runat="server" placeholder="Your Name *" CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-blue-900 text-sm bg-slate-50" />
                            </div>
                            <div>
                                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Email Address *" CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-blue-900 text-sm bg-slate-50" />
                            </div>
                            <div class="grid grid-cols-2 gap-3">
                                <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" placeholder="Phone *" CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-blue-900 text-sm bg-slate-50" />
                                <asp:TextBox ID="txtCity" runat="server" placeholder="City" CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-blue-900 text-sm bg-slate-50" />
                            </div>
                            <div>
                                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="3" placeholder="Your Message (Optional)" CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-blue-900 text-sm bg-slate-50 resize-none" />
                            </div>
                            <asp:Button ID="btnEnquiry" runat="server" Text="Send Enquiry" CssClass="w-full btn-primary text-center flex justify-center" OnClick="btnEnquiry_Click" />
                        </div>

                        <p class="text-xs text-slate-400 text-center mt-4">
                            We will get back to you within 24 hours.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- WhatsApp Floating Button -->
    <a href="https://wa.me/92300123456" target="_blank" class="fixed bottom-6 right-6 w-14 h-14 bg-green-500 rounded-full flex items-center justify-center shadow-2xl shadow-green-500/30 hover:scale-110 transition-transform z-50">
        <span class="iconify w-7 h-7 text-white" data-icon="logos:whatsapp-icon"></span>
    </a>

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
