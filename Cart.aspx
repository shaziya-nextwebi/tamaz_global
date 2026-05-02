<%@ Page Title="Cart Enquiry | TamazGlobal" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="cart.aspx.cs" Inherits="Cart" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="canonical" href="<%= Request.Url.AbsoluteUri.ToLower() %>" />
    <meta name="description" content="Tamaz Global Cart Enquiry Page" />
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
                padding: 0px 4px;
            }

                .price-quantity-for-mobile .price-mobile-block {
                    text-align: start !important;
                }
        }

        .checkout {
            font-weight: 800;
            font-size: 16px;
 background: #0a1b50;
            padding: 8px 36px;
            border-radius: 5px;
            border: 1px solid #ececec;
            color: #fff;
            margin-left: auto;
            display:inline-flex;
            gap:6px;
            align-items:center;
        }
         .checkout svg{
             width:20px;
         }

        .fs-13 {
            font-size: 13px !important;
        }

        .disply-none-desktop {
            display: none !important;
        }

        @media(max-width:576px) {
            .disply-none-mobile {
                display: none !important;
            }

            .c-product-name {
                font-size: 14px !important;
            }

            .c-product-desc {
                font-size: 10px !important;
            }

            .disply-none-desktop {
                display: block !important;
            }

            .c-cart-product-price {
                font-size: 12px !important;
            }

            .sm-fs-12 {
                font-size: 10px !important;
            }

            .cart-count-btn {
                width: 20px !important;
                height: 20px !important;
            }

            .cart-qty {
                font-size: 14px !important;
            }

            .mobile-sub-total {
                font-size: 14px !important;
            }

            .cart-items-box {
                border: none !important;
                box-shadow: none !important;
            }

            .checkout {
                font-weight: 600;
                font-size: 14px;
             
                padding: 8px 36px;
                border-radius: 5px;
                border: 1px solid #ececec;
         
                margin-left: auto;
            }
             .checkout svg
             
             {
                 width:17px;
             }

            .bill-summary {
                border-radius: 8px;
                overflow: hidden;
                margin-bottom: 20px;
            }

            .check-out-page-header {
                background: #ededff;
                padding: 8px;
            }

            .check-out-details {
                padding: 0px 8px !important;
            }

            .mobile-cart-form {
                border: 1px solid #e7e7e7;
                border-top: 4px solid #0a1b50;
                border-radius: 12px;
            }

            .cart-product-img {
                width: 60px !important;
                height: 60px !important;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Breadcrumb -->
    <div class="bg-[#f8fafc] py-1 md:py-3 border-b border-gray-200">
        <div class="max-w-7xl mx-auto px-4">
            <div class="flex items-center gap-2 text-sm text-slate-500 text-sm text-[#64748B] breadcrumb breadCrumb-text">
                <a href="Default.aspx" class="hover:text-red-700 transition-colors">Home</a>
                <span class="iconify w-4 h-4" data-icon="lucide:chevron-right"></span>
                <span class="text-slate-900 font-medium">Shopping Cart</span>
            </div>
        </div>
    </div>

    <!-- Empty Cart Panel -->
    <asp:Panel ID="pnlEmpty" runat="server" Visible="false">
        <div class="max-w-7xl mx-auto px-3 md:px-4 py-24 text-center">
            <span class="iconify w-16 h-16 text-slate-300 mx-auto mb-4" data-icon="lucide:shopping-cart"></span>
            <h2 class="text-2xl font-bold text-slate-700 mb-2">Your cart is empty</h2>
            <p class="text-slate-400 mb-6">Browse our products and add items to your cart.</p>
            <a href="javascript:void(0)" onclick="goBack()" class="btn-primary inline-flex items-center gap-2">
                <span class="iconify w-5 h-5" data-icon="lucide:arrow-left"></span>
                Continue Shopping
            </a>
        </div>
    </asp:Panel>

    <!-- Cart Panel -->
    <asp:Panel ID="pnlCart" runat="server">
        <section class="section-padding bg-white">
            <div class="max-w-7xl mx-auto px-3 md:px-4">
                <div class="grid lg:grid-cols-3 gap-8">

                    <!-- Cart Items (Left) -->
                    <div class="lg:col-span-2 reveal">
                        <div class="bg-white br-12 border border-slate-100 overflow-hidden shadow-lg cart-items-box">

                            <!-- Table Header -->
                            <div class="hidden md:grid grid-cols-12 gap-4 p-5 bg-slate-50 border-b border-slate-100 text-sm font-semibold text-slate-600">
                                <div class="col-span-5">Product</div>
                                <div class="col-span-2 text-center">Price</div>
                                <div class="col-span-3 text-center">Quantity</div>
                                <div class="col-span-2 text-right">Subtotal</div>
                            </div>

                            <asp:Repeater ID="rptCart" runat="server" OnItemCommand="rptCart_ItemCommand">
                                <ItemTemplate>
                                    <div class="p-0 pb-3 md:p-5 border-b border-slate-100 hover:bg-slate-50/50 transition-colors">
                                        <div class="grid grid-cols-1 md:grid-cols-12 gap-2 md:gap-4 items-center">

                                            <!-- Product Info (col 5) -->
                                            <div class="md:col-span-5 flex items-start gap-4 mt-2">
                                                <a href='<%# ResolveUrl("~/Product/") + Eval("ProductUrl") %>'>
                                                    <img src='<%# ResolveUrl(Eval("SmallImage").ToString()) %>'
                                                        onerror="this.onerror=null; this.src='/assets/Images/no-image.jpg';"
                                                        alt='<%# Eval("ProductName") %>'
                                                        class="w-24 h-24 object-cover rounded-lg border border-slate-100 flex-shrink-0 cart-product-img" />
                                                </a>
                                                <div>
                                                    <a href='<%# ResolveUrl("~/Product/") + Eval("ProductUrl") %>'
                                                        class="font-semibold text-slate-900 mb-1 leading-tight hover:text-blue-600 transition block c-product-name">
                                                        <%# Eval("ProductName") %>
                                                    </a>
                                                    <%-- <p class="text-sm text-slate-500 mb-2 c-product-desc"><%# Eval("Category") %></p>--%>

                                                    <!-- Price (col 2) for mobile-->
                                                    <div class="md:col-span-2  price-mobile-block disply-none-desktop">
                                                        <%--   <p class="text-xs text-slate-400 mb-1 md:hidden">Price</p>--%>
                                                        <p class="text-sm mt-1 c-cart-product-price">Price: <%# FormatCartPrice(Eval("RetailPrice")) %></p>
                                                    </div>
                                                    <asp:LinkButton runat="server"
                                                        CommandName="RemoveItem"
                                                        CommandArgument='<%# Eval("ProductId") %>'
                                                        CausesValidation="false"
                                                        CssClass="text-red-700 hover:text-red-800 text-sm font-medium inline-flex items-center gap-1 transition-colors disply-none-mobile remove-cart">
                            <span class="iconify w-4 h-4" data-icon="lucide:trash-2"></span> Remove
                                                    </asp:LinkButton>
                                                </div>
                                                <asp:LinkButton runat="server"
                                                    CommandName="RemoveItem"
                                                    CommandArgument='<%# Eval("ProductId") %>'
                                                    CausesValidation="false"
                                                    CssClass="text-red-700 hover:text-red-800 text-sm font-medium inline-flex items-center gap-1 transition-colors disply-none-desktop">
<span class="iconify w-4 h-4" data-icon="lucide:trash-2"></span>
                                                </asp:LinkButton>
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
                                                    class="w-8 h-8 flex items-center justify-center bg-gray-100 rounded-full">
                                                    −</button>

                                                <span id="qty_<%# Eval("ProductId") %>">
                                                    <%# Eval("Qty") %>
                                                </span>

                                                <button type="button"
                                                    onclick="updateQty(<%# Eval("ProductId") %>, 'inc'); return false;"
                                                    class="w-8 h-8 flex items-center justify-center bg-gray-100 rounded-full">
                                                    +</button>
                                            </div>
                                            <div class="price-quantity-for-mobile">
                                                <!-- Qty (mobile) — uses same JS as desktop, no postback -->
                                                <div class="md:col-span-3 flex items-center justify-center gap-1 flex-col">
                                                    <p class="text-xs text-black mb-0 md:hidden">Quantity</p>
                                                    <div class="flex gap-1 items-center cart-qty-wrap">
                                                        <button type="button"
                                                            onclick="updateQty(<%# Eval("ProductId") %>, 'dec'); return false;"
                                                            class="w-8 h-8 flex items-center justify-center bg-gray-100 hover:bg-gray-200 rounded-full font-bold text-lg transition-colors cursor-pointer cart-count-btn">
                                                            −</button>
                                                        <%-- qty span is already rendered in the desktop block; JS will update it --%>
                                                        <span class="font-semibold w-6 text-center cart-qty" id="qty_mob_<%# Eval("ProductId") %>"><%# Eval("Qty") %></span>
                                                        <button type="button"
                                                            onclick="updateQty(<%# Eval("ProductId") %>, 'inc'); return false;"
                                                            class="w-8 h-8 flex items-center justify-center bg-gray-100 hover:bg-gray-200 rounded-full font-bold text-lg transition-colors cursor-pointer cart-count-btn">
                                                            +</button>
                                                    </div>
                                                </div>

                                                <!-- Subtotal (mobile) — unique ID with _m suffix -->
                                                <div class="md:col-span-2 text-right">
                                                    <p class="text-xs text-black mb-0 md:hidden">Subtotal</p>
                                                    <span id="subtotal_<%# Eval("ProductId") %>_m" class="font-semibold text-slate-900 mobile-sub-total">
                                                        <%# FormatSubtotal(Eval("RetailPrice"), Eval("Qty")) %>
                                                    </span>
                                                </div>
                                            </div>
                                            <%-- <div class="price-quantity-for-mobile">
                                                                                          <!-- Qty (col 3) -->
                                                <div class="md:col-span-3 flex items-center justify-center gap-1 flex-col">
                                                    <p class="text-xs text-black mb-0 md:hidden">Quantity</p>
                                                    <div class="flex gap-1 items-center cart-qty-wrap">
                                                        <asp:LinkButton runat="server"
                                                            CommandName="DecQty"
                                                            CommandArgument='<%# Eval("ProductId") %>'
                                                            CssClass="w-8 h-8 flex items-center justify-center bg-gray-100 hover:bg-gray-200 rounded-full font-bold text-lg transition-colors cursor-pointer cart-count-btn">−</asp:LinkButton>
                                                        <span class="font-semibold w-6 text-center cart-qty"><%# Eval("Qty") %></span>
                                                        <asp:LinkButton runat="server"
                                                            CommandName="IncQty"
                                                            CommandArgument='<%# Eval("ProductId") %>'
                                                            CssClass="w-8 h-8 flex items-center justify-center bg-gray-100 hover:bg-gray-200 rounded-full font-bold text-lg transition-colors cursor-pointer cart-count-btn">+</asp:LinkButton>
                                                    </div>
                                                </div>

                                                  <!-- Subtotal (col 2) -->
  <div class="md:col-span-2 text-right">
      <p class="text-xs text-black mb-0 md:hidden ">Subtotal</p>
      <span id="subtotal_<%# Eval("ProductId") %>" class="font-semibold text-slate-900 mobile-sub-total"><%# FormatSubtotal(Eval("RetailPrice"), Eval("Qty")) %></span>
  </div>
                                            </div>

                                                                                          <!-- Subtotal (col 2) -->
<div class="md:col-span-2 text-right disply-none-mobile">
    <p class="text-xs text-black mb-1 md:hidden">Subtotal</p>
    <span id="subtotal_<%# Eval("ProductId") %>" class="font-semibold text-slate-900"><%# FormatSubtotal(Eval("RetailPrice"), Eval("Qty")) %></span>
</div>

                                        </div>
                                    </div>--%>
                                            <div class="md:col-span-2 text-right disply-none-mobile">
                                                <p class="text-xs text-black mb-1 md:hidden">Subtotal</p>
                                                <span id="subtotal_<%# Eval("ProductId") %>" class="font-semibold text-slate-900"><%# FormatSubtotal(Eval("RetailPrice"), Eval("Qty")) %></span>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>

                        <div class="mt-6 flex justify-between items-center countinue-shopping">
                            <a href="javascript:void(0)" onclick="goBack()" class="inline-flex items-center gap-2 text-blue-primary hover:text-red-700 font-medium transition-colors">
                                <span class="iconify w-5 h-5" data-icon="lucide:arrow-left"></span>
                                Continue Shopping
                            </a>
                            <% if (!lblTotal.Text.Contains("Price On Request"))
                                { %>
                            <a href="/checkout.aspx" class="checkout disply-none-mobile">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-shopping-cart-icon lucide-shopping-cart"><circle cx="8" cy="21" r="1"/><circle cx="19" cy="21" r="1"/><path d="M2.05 2.05h2l2.66 12.42a2 2 0 0 0 2 1.58h9.78a2 2 0 0 0 1.95-1.57l1.65-7.43H5.12"/></svg>
                                CheckOut</a>
                            <% } %>
                        </div>
                    </div>
                    <div class="disply-none-desktop">

                        <div class="bill-summary border  border-slate-100 shadow">
                            <div class="check-out-page-header    mb-2">
                                <h2 class="text-sm font-bold text-slate-900 mb-0 flex items-center gap-2">
                                    <span class="iconify w-4 h-4 text-blue-primary" data-icon="lucide:clipboard-list"></span>
                                    Bill Summary
                                </h2>
                            </div>
                            <div class="check-out-details">
                                <div class="flex justify-between text-sm border-b pb-2 ">
                                    <span class="text-black-500 ">Subtotal</span>
                                    <asp:Label ID="Label1" runat="server" CssClass="font-semibold" />
                                </div>

                                <div class="flex justify-between text-sm mt-2  ">
                                    <span class="text-black-500">Shipping</span>
                                    <span class="font-semibold">Free</span>
                                </div>

                                <div class="flex justify-between text-lg mt-2 border-t pt-2 pb-2 ">
                                    <span class="font-bold">Total</span>
                                    <asp:Label ID="Label2" runat="server" CssClass="font-bold" />
                                </div>
                            </div>
                        </div>

                        <div class="flex justify-end">
                            <%--        <button class="checkout" onclick="window.location.href='/checkout.aspx'">
                                CheckOut
                            </button>--%>
                            <% if (!lblTotal.Text.Contains("Price On Request"))
                                { %>
                            <a href="/checkout.aspx" class="checkout">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-shopping-cart-icon lucide-shopping-cart"><circle cx="8" cy="21" r="1"/><circle cx="19" cy="21" r="1"/><path d="M2.05 2.05h2l2.66 12.42a2 2 0 0 0 2 1.58h9.78a2 2 0 0 0 1.95-1.57l1.65-7.43H5.12"/></svg>
                                CheckOut</a>
                            <% } %>
                        </div>

                    </div>

                    <!-- Quick Enquiry (Right) -->
                    <div class="lg:col-span-1 reveal mobile-cart-form">



                        <div class="bg-white rounded-2xl shadow-lg border border-slate-100 p-6 sticky top-2 add-cart-form">
                            <h2 class="text-xl font-bold text-slate-900 mb-5 flex items-center gap-2">
                                <span class="iconify w-6 h-6 text-blue-primary disply-none-mobile" data-icon="lucide:clipboard-list"></span>
                                Quick Enquiry
                            </h2>

                            <div class="flex justify-between text-sm disply-none-mobile">
                                <span class="text-slate-500">Subtotal</span>
                                <asp:Label ID="lblSubtotal" runat="server" CssClass="font-semibold" />
                            </div>

                            <div class="flex justify-between text-sm mt-2 disply-none-mobile">
                                <span class="text-slate-500">Shipping</span>
                                <span class="font-semibold">Free</span>
                            </div>

                            <div class="flex justify-between text-lg mt-3 border-t pt-3 pb-2 disply-none-mobile">
                                <span class="font-bold">Total</span>
                                <asp:Label ID="lblTotal" runat="server" CssClass="font-bold" />
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

                                <div class="grid grid-cols-1 gap-3">
                                    <div>
                                        <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" placeholder="Phone *" MaxLength="15" minLength="6" oninput="this.value = this.value.replace(/[^0-9+]/g, '')"
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
                                    ValidationGroup="CartEnquiry" OnClientClick="if (Page_ClientValidate('CartEnquiry')) { var btn=this; setTimeout(function(){ btn.disabled=true; btn.value='Please Wait...'; }, 0); }" />
                            </div>

                           <%-- <p class="text-xs text-slate-400 text-center mt-4">
                                We will get back to you within 24 hours.
                           
                            </p>--%>
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
        window.addEventListener('DOMContentLoaded', function () {
            var params = new URLSearchParams(window.location.search);
            if (params.get('msg') === 'priceOnRequest') {
                tgShowSnackbar('One or more items require a price quote. Please contact us or remove those items to proceed.');
            }
        });
        var _tgSnackbarTimer = null;
        function tgShowSnackbar(message) {
            var existing = document.getElementById('tg-snackbar-cart');
            if (!existing) {
                var el = document.createElement('div');
                el.id = 'tg-snackbar-cart';
                el.style.cssText = 'position:fixed;top:24px;right:24px;z-index:999999;background:#ea1c1c;color:#fff;padding:14px 18px;border-radius:8px;font-size:14px;font-weight:500;box-shadow:0 4px 16px rgba(0,0,0,0.18);max-width:340px;display:none;';
                document.body.appendChild(el);
                existing = el;
            }
            existing.textContent = message;
            existing.style.display = 'block';
            clearTimeout(_tgSnackbarTimer);
            _tgSnackbarTimer = setTimeout(function () {
                existing.style.display = 'none';
            }, 6000);
        }
    </script>
    <script>
        function goBack() {
            if (document.referrer && document.referrer !== window.location.href) {
                window.location.href = document.referrer;
            } else {
                window.location.href = '/Category/glutathione-injections';
            }
        }
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
            fetch('cart.aspx/UpdateQty', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ productId: productId, action: action })
            })
                .then(res => res.json())
                .then(data => {
                    if (data.d.success) {
                        const qtyEl = document.getElementById("qty_" + productId);
                        if (qtyEl) qtyEl.innerText = data.d.qty;
                        const qtyMobEl = document.getElementById("qty_mob_" + productId);
                        if (qtyMobEl) qtyMobEl.innerText = data.d.qty;
                        if (!data.d.itemIsPriceOnRequest) {
                            const subtotalEl = document.getElementById("subtotal_" + productId);
                            if (subtotalEl) subtotalEl.innerText = "₹ " + data.d.itemSubtotal;

                            const subtotalMEl = document.getElementById("subtotal_" + productId + "_m");
                            if (subtotalMEl) subtotalMEl.innerText = "₹ " + data.d.itemSubtotal;
                        }

                        const contactUsHtml = '<a href="/contact-us.aspx" style="font-weight:600;color:#333333;text-decoration:none;">Price On Request</a>';
                        const subtotalVal = parseFloat(data.d.subtotal);
                        const totalVal = parseFloat(data.d.total);

                        const formatted = {
                            sub: subtotalVal <= 0 ? contactUsHtml : "₹ " + data.d.subtotal,
                            tot: totalVal <= 0 ? contactUsHtml : "₹ " + data.d.total
                        };

                        document.getElementById('<%= lblSubtotal.ClientID %>').innerHTML = formatted.sub;
                        document.getElementById('<%= lblTotal.ClientID %>').innerHTML = formatted.tot;

                        const mSub = document.getElementById('<%= Label1.ClientID %>');
                        const mTot = document.getElementById('<%= Label2.ClientID %>');
                        if (mSub) mSub.innerHTML = formatted.sub;
                        if (mTot) mTot.innerHTML = formatted.tot;
                    }
                });
            return false;
        }
    </script>
</asp:Content>
