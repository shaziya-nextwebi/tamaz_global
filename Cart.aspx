<%@ Page Title="Shopping Cart - TAMAZ Global" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" %>

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

        .field-error {
            color: #dc2626;
            font-size: 12px;
            margin-top: 4px;
            display: block;
        }

        .cart-product-img {
            max-width: 80px !important;
            max-height: 80px !important;
        }

        .price-quantity-for-mobile {
            display: none;
        }

        @media(max-width:768px) {
            .tab-d-none {
                display: none !important;
            }

            .price-quantity-for-mobile {
                display: flex;
                justify-content: space-between;
                align-items: center;
            }

                .price-quantity-for-mobile .price-mobile-block {
                    text-align: start !important;
                }
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
                <span class="text-slate-900 font-medium">Shopping Cart</span>
            </div>
        </div>
    </div>

    <!-- Empty Cart Panel -->
    <asp:Panel ID="pnlEmpty" runat="server" Visible="false">
        <div class="max-w-7xl mx-auto px-4 py-24 text-center">
            <span class="iconify w-16 h-16 text-slate-300 mx-auto mb-4" data-icon="lucide:shopping-cart"></span>
            <h2 class="text-2xl font-bold text-slate-700 mb-2">Your cart is empty</h2>
            <p class="text-slate-400 mb-6">Browse our products and add items to your cart.</p>
            <a href="/Category/glutathione-injections" class="btn-primary inline-flex items-center gap-2">
                <span class="iconify w-5 h-5" data-icon="lucide:arrow-left"></span>
                Continue Shopping
            </a>
        </div>
    </asp:Panel>

    <!-- Cart Panel -->
    <asp:Panel ID="pnlCart" runat="server">
        <section class="section-padding bg-white">
            <div class="max-w-7xl mx-auto px-4">
                <div class="grid lg:grid-cols-3 gap-8">

                    <!-- Cart Items (Left) -->
                    <div class="lg:col-span-2 reveal">
                        <div class="bg-white br-12 border border-slate-100 overflow-hidden shadow-lg">

                            <!-- Table Header -->
                            <div class="hidden md:grid grid-cols-12 gap-4 p-5 bg-slate-50 border-b border-slate-100 text-sm font-semibold text-slate-600">
                                <div class="col-span-5">Product</div>
                                <div class="col-span-2 text-center">Price</div>
                                <div class="col-span-3 text-center">Quantity</div>
                                <div class="col-span-2 text-right">Subtotal</div>
                            </div>

                            <asp:Repeater ID="rptCart" runat="server" OnItemCommand="rptCart_ItemCommand">
                                <ItemTemplate>
                                    <div class="p-5 border-b border-slate-100 hover:bg-slate-50/50 transition-colors">
                                        <div class="grid grid-cols-1 md:grid-cols-12 gap-4 items-center">

                                            <!-- Product Info (col 5) -->
                                            <div class="md:col-span-5 flex items-start gap-4">
                                                <a href='<%# ResolveUrl("~/Product/") + Eval("ProductUrl") %>'>
                                                    <img src='<%# ResolveUrl(Eval("SmallImage").ToString()) %>'
                                                        onerror="this.onerror=null; this.src='/assets/Images/no-image.jpg';"
                                                        alt='<%# Eval("ProductName") %>'
                                                        class="w-24 h-24 object-cover rounded-lg border border-slate-100 flex-shrink-0 cart-product-img" />
                                                </a>
                                                <div>
                                                    <a href='<%# ResolveUrl("~/Product/") + Eval("ProductUrl") %>'
                                                        class="font-semibold text-slate-900 mb-1 leading-tight hover:text-blue-600 transition block">
                                                        <%# Eval("ProductName") %>
                                                    </a>
                                                    <p class="text-sm text-slate-500 mb-2"><%# Eval("Category") %></p>
                                                    <asp:LinkButton runat="server"
                                                        CommandName="RemoveItem"
                                                        CommandArgument='<%# Eval("ProductId") %>'
                                                        CausesValidation="false"
                                                        CssClass="text-red-700 hover:text-red-800 text-sm font-medium inline-flex items-center gap-1 transition-colors">
                            <span class="iconify w-4 h-4" data-icon="lucide:trash-2"></span> Remove
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <!-- Price (col 2) -->
                                            <div class="md:col-span-2 text-center tab-d-none">
                                                <p class="text-xs text-slate-400 mb-1 md:hidden">Price</p>
                                                <p class="text-sm mt-1"><%# FormatCartPrice(Eval("RetailPrice")) %></p>
                                            </div>

                                            <!-- Qty (col 3) -->
                                            <div class="md:col-span-3 flex items-center justify-center gap-2 tab-d-none">
                                                <p class="text-xs text-slate-400 mb-1 md:hidden">Quantity</p>
                                        <button type="button"
    onclick="updateQty(<%# Eval("ProductId") %>, 'dec'); return false;"
    class="w-8 h-8 flex items-center justify-center bg-gray-100 rounded-full">−</button>

<span id="qty_<%# Eval("ProductId") %>">
    <%# Eval("Qty") %>
</span>

<button type="button"
    onclick="updateQty(<%# Eval("ProductId") %>, 'inc'); return false;"
    class="w-8 h-8 flex items-center justify-center bg-gray-100 rounded-full">+</button>
                                            </div>

                                            <div class="price-quantity-for-mobile">
                                                <!-- Price (col 2) -->
                                                <div class="md:col-span-2 text-center price-mobile-block">
                                                    <p class="text-xs text-slate-400 mb-1 md:hidden">Price</p>
                                                    <p class="text-sm mt-1"><%# FormatCartPrice(Eval("RetailPrice")) %></p>
                                                </div>

                                                <!-- Qty (col 3) -->
                                                <div class="md:col-span-3 flex items-center justify-center gap-2 flex-col">
                                                    <p class="text-xs text-slate-400 mb-1 md:hidden">Quantity</p>
                                                    <div class="flex gap-1">
                                                        <asp:LinkButton runat="server"
                                                            CommandName="DecQty"
                                                            CommandArgument='<%# Eval("ProductId") %>'
                                                            CssClass="w-8 h-8 flex items-center justify-center bg-gray-100 hover:bg-gray-200 rounded-full font-bold text-lg transition-colors cursor-pointer">−</asp:LinkButton>
                                                        <span class="font-semibold w-6 text-center"><%# Eval("Qty") %></span>
                                                        <asp:LinkButton runat="server"
                                                            CommandName="IncQty"
                                                            CommandArgument='<%# Eval("ProductId") %>'
                                                            CssClass="w-8 h-8 flex items-center justify-center bg-gray-100 hover:bg-gray-200 rounded-full font-bold text-lg transition-colors cursor-pointer">+</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- Subtotal (col 2) -->
                                            <div class="md:col-span-2 text-right">
                                                <p class="text-xs text-slate-400 mb-1 md:hidden">Subtotal</p>
                                                <span class="font-semibold text-slate-900"><%# FormatSubtotal(Eval("RetailPrice"), Eval("Qty")) %></span>
                                            </div>

                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>

                        <div class="mt-6">
                            <a href="/Category/glutathione-injections" class="inline-flex items-center gap-2 text-blue-primary hover:text-red-700 font-medium transition-colors">
                                <span class="iconify w-5 h-5" data-icon="lucide:arrow-left"></span>
                                Continue Shopping
                            </a>
                        </div>
                    </div>

                    <!-- Quick Enquiry (Right) -->
                    <div class="lg:col-span-1 reveal">
                        <div class="bg-white rounded-2xl shadow-lg border border-slate-100 p-6 sticky top-2">
                            <h2 class="text-xl font-bold text-slate-900 mb-5 flex items-center gap-2">
                                <span class="iconify w-6 h-6 text-blue-primary" data-icon="lucide:clipboard-list"></span>
                                Quick Enquiry
                            </h2>

                            <div class="flex justify-between text-sm">
                                <span class="text-slate-500">Subtotal</span>
                                <asp:Label ID="lblSubtotal" runat="server" CssClass="font-semibold" />
                            </div>

                            <div class="flex justify-between text-sm mt-2">
                                <span class="text-slate-500">Shipping</span>
                                <span class="font-semibold">Calculated later</span>
                            </div>

                            <div class="flex justify-between text-lg mt-3 border-t pt-3">
                                <span class="font-bold">Total</span>
                                <asp:Label ID="lblTotal" runat="server" CssClass="font-bold text-blue-900" />
                            </div>

                            <!-- Enquiry Form -->
                            <div class="space-y-3">

                                <div>
                                    <asp:TextBox ID="txtName" runat="server" placeholder="Your Name *"
                                        CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-blue-900 text-sm bg-slate-50" />
                                    <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                        ControlToValidate="txtName" ErrorMessage="Name is required."
                                        CssClass="field-error" ValidationGroup="CartEnquiry" Display="Dynamic" />
                                </div>

                                <div>
                                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Email Address *"
                                        CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-blue-900 text-sm bg-slate-50" />
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                        ControlToValidate="txtEmail" ErrorMessage="Email is required."
                                        CssClass="field-error" ValidationGroup="CartEnquiry" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                        ControlToValidate="txtEmail"
                                        ValidationExpression="^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$"
                                        ErrorMessage="Enter a valid email."
                                        CssClass="field-error" ValidationGroup="CartEnquiry" Display="Dynamic" />
                                </div>

                                <div class="grid grid-cols-2 gap-3">
                                    <div>
                                        <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" placeholder="Phone *"
                                            CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-blue-900 text-sm bg-slate-50" />
                                        <asp:RequiredFieldValidator ID="rfvPhone" runat="server"
                                            ControlToValidate="txtPhone" ErrorMessage="Phone is required."
                                            CssClass="field-error" ValidationGroup="CartEnquiry" Display="Dynamic" />
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtCity" runat="server" placeholder="City"
                                            CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-blue-900 text-sm bg-slate-50" />
                                    </div>
                                </div>

                                <div>
                                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="3"
                                        placeholder="Your Message (Optional)"
                                        CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-blue-900 text-sm bg-slate-50 resize-none" />
                                </div>

                                <asp:Button ID="btnEnquiry" runat="server" Text="Send Enquiry"
                                    CssClass="w-full btn-primary text-center flex justify-center"
                                    OnClick="btnEnquiry_Click"
                                    ValidationGroup="CartEnquiry" />
                            </div>

                            <p class="text-xs text-slate-400 text-center mt-4">
                                We will get back to you within 24 hours.
                           
                            </p>
                        </div>
                    </div>

                </div>
            </div>
        </section>
    </asp:Panel>


</asp:Content>

<asp:Content ID="ScriptsContent" ContentPlaceHolderID="scripts" runat="server">
    <script src="https://code.iconify.design/3/3.1.0/iconify.min.js"></script>
    <script>
        const reveals = document.querySelectorAll('.reveal');
        const revealOnScroll = () => {
            reveals.forEach(el => {
                if (el.getBoundingClientRect().top < window.innerHeight - 50) el.classList.add('active');
            });
        };
        window.addEventListener('scroll', revealOnScroll);
        revealOnScroll();
    </script>
<script>
    function updateQty(productId, action) {

        fetch('Cart.aspx/UpdateQty', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ productId: productId, action: action })
        })
            .then(res => res.json())
            .then(data => {
                if (data.d.success) {
                    document.getElementById("qty_" + productId).innerText = data.d.qty;
                }
            });

        return false; // 🔥 critical
    }
</script>
</asp:Content>
