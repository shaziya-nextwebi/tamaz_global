<%@ Page Title="Checkout | TamazGlobal" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="checkout.aspx.cs" Inherits="Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="canonical" href="<%= Request.Url.AbsoluteUri.ToLower() %>" />
    <meta name="description" content="Tamaz Global Checkout Page" />
    <style>
        /* Custom Scrollbar */
        .order-summary-section::-webkit-scrollbar {
            width: 4px;
        }

        .order-summary-section::-webkit-scrollbar-thumb {
            background: #cbd5e1;
            border-radius: 4px;
        }

        /* GST toggle */
        .gst-fields-wrapper {
            max-height: 0;
            overflow: hidden;
            transition: max-height 0.3s ease-out, opacity 0.3s ease-out;
            opacity: 0;
        }

            .gst-fields-wrapper.active {
                max-height: 200px;
                opacity: 1;
            }

        .place-order-btn {
            padding: 12px !important;
            font-size: 14px !important;
            background: #226C22 !important;
        }

        .order-summary-section {
           box-shadow: 0px 0px 4px rgb(188 188 188 / 55%) !important;
            border: 1px solid #DADADA;
        }

        .field-error {
            color: #dc2626;
            font-size: 11px;
            margin-top: 3px;
            display: block;
        }

        /* Price rows */
        .sum-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 6px 0;
            font-size: 14px;
        }

            .sum-row.fw7 .sum-label,
            .sum-row.fw7 .sum-val {
                font-weight: 700;
            }

            .sum-row.grand {
                font-size: 15px;
                padding: 11px 0;
                border-top: 1px solid #e2e8f0;
                margin-top: 2px;
            }

        /* Partial-only rows */
        .partial-only {
            display: none;
        }

        #orderSummaryPanel.partial-active .partial-only {
            display: flex;
        }

        /* Payment option — Doc 3 styled cards */
        .pay-opt-label {
            display: flex;
            align-items: center;
            gap: 12px;
            padding: 12px;
            border: 1px solid #e5e7eb;
            border-radius: 8px;
            cursor: pointer;
            font-size: 14px;
            color: #1e293b;
            transition: background 0.15s, border-color 0.15s;
            margin-bottom: 10px;
        }

            .pay-opt-label:last-child {
                margin-bottom: 0;
            }

            .pay-opt-label:hover {
                background: #f9fafb;
            }

            .pay-opt-label input[type="radio"] {
                width: 17px;
                height: 17px;
                accent-color: #226C22;
                cursor: pointer;
                flex-shrink: 0;
            }

            .pay-opt-label.selected {
                border-color: #dbdbdb;
                background: #f0fdf4;
            }

        .pay-opt-content {
            display: flex;
            flex-direction: column;
            gap: 2px;
        }

        .pay-opt-name {
            font-weight: 600;
            font-size: 14px;
            color: #1f2937;
        }

        .pay-opt-desc {
            font-size: 12px;
            color: #6b7280;
        }

        /* Partial detail rows (inside summary) */
        .partial-detail-row {
            display: flex;
            justify-content: space-between;
            font-size: 13px;
            padding: 5px 0;
            color: #374151;
        }

        /* Savings banner */
        .savings-banner {
            background: #f0fdf4;
            border: 1px solid #bbf7d0;
            border-radius: 8px;
            padding: 10px 14px;
            text-align: center;
            font-size: 13px;
            color: #15803d;
            font-weight: 500;
        }

            .savings-banner strong {
                font-weight: 700;
            }

        /* Mobile btn */
        .place-order-btn.display-none-desktop {
            display: none !important;
        }

        @media(max-width: 576px) {
            .place-order-btn.display-none-desktop {
                display: inline-flex !important;
                align-items: center;
                justify-content: center;
                gap: 8px;
                padding: 8px 16px;
                font-size: 14px;
                background: #226C22 !important;
            }

            .fs-12 {
                font-size: 12px !important;
            }

            .fs-14 {
                font-size: 14px !important;
            }

            .fs-18 {
                font-size: 18px !important;
            }

            .form-label {
                font-size: 12px !important;
            }

            .form-input {
                font-size: 14px !important;
            }

                .form-input:focus {
                    box-shadow: none !important;
                    border: 1px solid #666565 !important;
                }

            .delivery-form-section {
                box-shadow: 0px 0px 4px rgb(188 188 188);
                padding: 16px 12px;
                border-radius: 12px;
                border-top: 3px solid #0a1b50;
            }

            .order-summary-section {
                box-shadow: 0px 0px 4px rgb(188 188 188 / 55%) !important;
            }
            .display-none-mobile
            {
                display:none;
            }
        }

        /* Snackbar */
        #tg-snackbar {
            visibility: hidden;
            min-width: 280px;
            max-width: 360px;
            background: #ea1c1c;
            color: #fff;
            text-align: left;
            border-radius: 8px;
            padding: 14px 18px;
            position: fixed;
            top: 24px;
            right: 24px;
            z-index: 99999;
            font-size: 14px;
            font-weight: 500;
            box-shadow: 0 4px 16px rgba(0,0,0,0.18);
            display: flex;
            align-items: center;
            gap: 10px;
            opacity: 0;
            transform: translateY(-12px);
            transition: opacity 0.3s ease, transform 0.3s ease;
        }

            #tg-snackbar.tg-snackbar--show {
                visibility: visible;
                opacity: 1;
                transform: translateY(0);
            }

            #tg-snackbar .tg-snackbar__icon {
                flex-shrink: 0;
                width: 20px;
                height: 20px;
            }

            #tg-snackbar .tg-snackbar__close {
                margin-left: auto;
                background: none;
                border: none;
                color: #fff;
                font-size: 18px;
                cursor: pointer;
                line-height: 1;
                padding: 0 0 0 8px;
                opacity: 0.8;
            }

                #tg-snackbar .tg-snackbar__close:hover {
                    opacity: 1;
                }

                .terms-checkbox input
                {
                    width:100%;
                    height:100%;
                }

        @media(max-width: 480px) {
            #tg-snackbar {
                right: 12px;
                left: 12px;
                min-width: unset;
                max-width: unset;
                top: 16px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Breadcrumb -->
    <div class="bg-[#f8fafc] py-1 md:py-3 border-b border-gray-200">
        <div class="max-w-7xl mx-auto px-3 md:px-4">
            <div class="flex items-center gap-2 text-sm text-slate-500 text-[#64748B] breadcrumb breadCrumb-text">
                <a href="/Default.aspx" class="hover:text-red-700 transition-colors">Home</a>
                <span class="iconify w-4 h-4" data-icon="lucide:chevron-right"></span>
                <a href="/cart.aspx" class="hover:text-red-700 transition-colors">Cart</a>
                <span class="iconify w-4 h-4" data-icon="lucide:chevron-right"></span>
                <span class="text-slate-900 font-medium">CheckOut</span>
            </div>
        </div>
    </div>

    <!-- Main Checkout Content -->
    <main class="checkout-main-container py-8 md:py-12">
        <div class="max-w-7xl mx-auto px-3 md:px-4">
            <div class="checkout-grid grid lg:grid-cols-3 gap-8">

                <!-- LEFT: Delivery Address Form -->
                <div class="delivery-form-section lg:col-span-2 order-1">
                    <%-- <div class="bg-white rounded-xl shadow-sm border border-gray-100 p-6 md:p-8">--%>

                    <div class="mb-3 md:mb-6">
                        <h2 class="text-xl md:text-2xl font-bold text-[#0F172A] fs-18 flex gap-1">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"
                                fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                                <path d="M14 18V6a2 2 0 0 0-2-2H4a2 2 0 0 0-2 2v11a1 1 0 0 0 1 1h2" />
                                <path d="M15 18H9" />
                                <path d="M19 18h2a1 1 0 0 0 1-1v-3.65a1 1 0 0 0-.22-.624l-3.48-4.35A1 1 0 0 0 17.52 8H14" />
                                <circle cx="17" cy="18" r="2" />
                                <circle cx="7" cy="18" r="2" />
                            </svg>
                            Delivery Address
                            </h2>
                        <p class="text-sm text-gray-500 mt-1 fs-12">Fields marked with * are mandatory</p>
                    </div>

                    <!-- Name Row -->
                    <div class="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
                        <div>
                            <label class="form-label block text-sm font-medium text-black mb-1" for="<%= txtFirstName.ClientID %>">First Name *</label>
                            <asp:TextBox ID="txtFirstName" runat="server" placeholder="Enter First name"
                                CssClass="form-input w-full px-3 md:px-4 py-2 md:py-3 border border-gray-200 rounded-lg focus:border-[#333333] outline-none transition-all" />
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
                                ControlToValidate="txtFirstName" ErrorMessage="First name is required."
                                CssClass="field-error" ValidationGroup="CheckoutForm" Display="Dynamic" />
                        </div>
                        <div>
                            <label class="form-label block text-sm font-medium text-gray-700 mb-1" for="<%= txtLastName.ClientID %>">Last Name *</label>
                            <asp:TextBox ID="txtLastName" runat="server" placeholder="Enter Last name"
                                CssClass="form-input w-full px-3 md:px-4 py-2 md:py-3 border border-gray-200 rounded-lg focus:border-[#333333] outline-none transition-all" />
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server"
                                ControlToValidate="txtLastName" ErrorMessage="Last name is required."
                                CssClass="field-error" ValidationGroup="CheckoutForm" Display="Dynamic" />
                        </div>
                    </div>

                    <!-- Contact Row -->
                    <div class="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
                        <div>
                            <label class="form-label block text-sm font-medium text-gray-700 mb-1" for="<%= txtMobile.ClientID %>">Mobile No *</label>
                            <asp:TextBox ID="txtMobile" runat="server" TextMode="Phone" placeholder="+91 XXXXXXXXXX" MaxLength="15"
                                oninput="this.value = this.value.replace(/[^0-9+]/g, '')"
                                CssClass="form-input w-full px-3 md:px-4 py-2 md:py-3 border border-gray-200 rounded-lg focus:border-[#333333] outline-none transition-all" />
                            <asp:RequiredFieldValidator ID="rfvMobile" runat="server"
                                ControlToValidate="txtMobile" ErrorMessage="Mobile number is required."
                                CssClass="field-error" ValidationGroup="CheckoutForm" Display="Dynamic" />
                        </div>
                        <div>
                            <label class="form-label block text-sm font-medium text-gray-700 mb-1" for="<%= txtEmail.ClientID %>">Email Address *</label>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Enter Email Address"
                                CssClass="form-input w-full px-3 md:px-4 py-2 md:py-3 border border-gray-200 rounded-lg focus:border-[#333333] outline-none transition-all" />
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                ControlToValidate="txtEmail" ErrorMessage="Email is required."
                                CssClass="field-error" ValidationGroup="CheckoutForm" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                ControlToValidate="txtEmail"
                                ValidationExpression="^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$"
                                ErrorMessage="Enter a valid email."
                                CssClass="field-error" ValidationGroup="CheckoutForm" Display="Dynamic" />
                        </div>
                    </div>

                    <!-- Address -->
                    <div class="mb-4">
                        <label class="form-label block text-sm font-medium text-gray-700 mb-1" for="<%= txtAddress.ClientID %>">Address (House No, Street, Area) *</label>
                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="3"
                            placeholder="Enter Address" MaxLength="500"
                            CssClass="form-input w-full px-4 py-3 border border-gray-200 rounded-lg focus:border-[#333333] outline-none transition-all resize-none" />
                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server"
                            ControlToValidate="txtAddress" ErrorMessage="Address is required."
                            CssClass="field-error" ValidationGroup="CheckoutForm" Display="Dynamic" />
                    </div>

                    <!-- Pin / City / State -->
                    <div class="grid grid-cols-5 md:grid-cols-5 gap-4 mb-4">
                        <div class="col-span-2 md:col-span-1">
                            <label class="form-label block text-sm font-medium text-gray-700 mb-1" for="<%= txtPincode.ClientID %>">Pin Code *</label>
                            <asp:TextBox ID="txtPincode" runat="server" placeholder="Pin Code" MaxLength="10"
                                CssClass="form-input w-full px-3 md:px-4 py-2 md:py-3 border border-gray-200 rounded-lg focus:border-[#333333] outline-none transition-all" />
                            <asp:RequiredFieldValidator ID="rfvPincode" runat="server"
                                ControlToValidate="txtPincode" ErrorMessage="Pin code is required."
                                CssClass="field-error" ValidationGroup="CheckoutForm" Display="Dynamic" />
                        </div>
                        <div class="col-span-3 md:col-span-2">
                            <label class="form-label block text-sm font-medium text-gray-700 mb-1" for="<%= txtCity.ClientID %>">City *</label>
                            <asp:TextBox ID="txtCity" runat="server" placeholder="Enter City" MaxLength="100"
                                CssClass="form-input w-full px-3 md:px-4 py-2 md:py-3 border border-gray-200 rounded-lg focus:border-[#333333] outline-none transition-all" />
                            <asp:RequiredFieldValidator ID="rfvCity" runat="server"
                                ControlToValidate="txtCity" ErrorMessage="City is required."
                                CssClass="field-error" ValidationGroup="CheckoutForm" Display="Dynamic" />
                        </div>
                        <div class="col-span-5 md:col-span-2">
                            <label class="form-label block text-sm font-medium text-gray-700 mb-1" for="<%= txtState.ClientID %>">State *</label>
                            <asp:TextBox ID="txtState" runat="server" placeholder="Enter State" MaxLength="100"
                                CssClass="form-input w-full px-3 md:px-4 py-2 md:py-3 border border-gray-200 rounded-lg focus:border-[#333333] outline-none transition-all bg-white" />
                            <asp:RequiredFieldValidator ID="rfvState" runat="server"
                                ControlToValidate="txtState" ErrorMessage="State is required."
                                CssClass="field-error" ValidationGroup="CheckoutForm" Display="Dynamic" />
                        </div>
                    </div>

                    <!-- Country -->
                    <div class="mb-6">
                        <label class="form-label block text-sm font-medium text-gray-700 mb-1" for="<%= txtCountry.ClientID %>">Country *</label>
                        <asp:TextBox ID="txtCountry" runat="server" placeholder="Enter Country" Text="India"
                            CssClass="form-input w-full px-3 md:px-4 py-2 md:py-3 border border-gray-200 rounded-lg focus:border-[#333333] outline-none transition-all bg-white" />
                        <asp:RequiredFieldValidator ID="rfvCountry" runat="server"
                            ControlToValidate="txtCountry" ErrorMessage="Country is required."
                            CssClass="field-error" ValidationGroup="CheckoutForm" Display="Dynamic" />
                    </div>

                    <!-- GST Section -->
                    <div class="border-t border-gray-100 pt-6 mt-2">
                        <div class="flex items-center gap-3 mb-4">
                            <input type="checkbox" id="gstCheckbox" class="w-5 h-5 rounded border-gray-300 cursor-pointer" />
                            <label for="gstCheckbox" class="text-sm font-semibold text-gray-700 cursor-pointer">
                                Add Business Details (For GST Invoice)
                               
                            </label>
                        </div>
                        <div id="gstFields" class="gst-fields-wrapper">
                            <div class="grid grid-cols-1 md:grid-cols-2 gap-4 pt-2">
                                <div>
                                    <label class="form-label block text-sm font-medium text-gray-700 mb-1">Company Name</label>
                                    <asp:TextBox ID="txtCompanyName" runat="server" placeholder="Company Inc."
                                        CssClass="form-input w-full px-3 md:px-4 py-2 md:py-3 border border-gray-200 rounded-lg focus:border-[#333333] outline-none transition-all bg-gray-50" />
                                </div>
                                <div>
                                    <label class="form-label block text-sm font-medium text-gray-700 mb-1">GST Number</label>
                                    <asp:TextBox ID="txtGSTNumber" runat="server" placeholder="29ABCDE1234F1Z5"
                                        CssClass="form-input w-full px-3 md:px-4 py-2 md:py-3 border border-gray-200 rounded-lg focus:border-[#333333] outline-none transition-all bg-gray-50" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- </div>--%>
                </div>
                <%-- end left --%>

                <!-- RIGHT: Order Summary -->
                <div class="order-summary-section bg-white rounded-xl p-4 md:p-6 h-fit lg:sticky lg:top-24 order-2" id="orderSummaryPanel">

                    <h3 class="text-lg font-bold text-[#0F172A] border-b border-gray-100 pb-3 mb-3 flex gap-1 items-center">
                        <svg xmlns="http://www.w3.org/2000/svg" width="22" height="22" viewBox="0 0 24 24"
                            fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                            <path d="M13 16H8" />
                            <path d="M14 8H8" />
                            <path d="M16 12H8" />
                            <path d="M4 3a1 1 0 0 1 1-1 1.3 1.3 0 0 1 .7.2l.933.6a1.3 1.3 0 0 0 1.4 0l.934-.6a1.3 1.3 0 0 1 1.4 0l.933.6a1.3 1.3 0 0 0 1.4 0l.933-.6a1.3 1.3 0 0 1 1.4 0l.934.6a1.3 1.3 0 0 0 1.4 0l.933-.6A1.3 1.3 0 0 1 19 2a1 1 0 0 1 1 1v18a1 1 0 0 1-1 1 1.3 1.3 0 0 1-.7-.2l-.933-.6a1.3 1.3 0 0 0-1.4 0l-.934.6a1.3 1.3 0 0 1-1.4 0l-.933-.6a1.3 1.3 0 0 0-1.4 0l-.933.6a1.3 1.3 0 0 1-1.4 0l-.934-.6a1.3 1.3 0 0 0-1.4 0l-.933.6a1.3 1.3 0 0 1-.7.2 1 1 0 0 1-1-1z" />
                        </svg>
                        Order Summary
                    </h3>

                    <%-- Item count --%>
                    <p class="text-sm text-gray-500 mb-3">
                        Order contains <strong class="text-gray-800"><%= intCartItemCount %></strong>
                        <%= intCartItemCount == 1 ? "item" : "items" %>
                    </p>

                    <!-- Price breakdown -->
                    <div class="border-b border-gray-100 pb-3 mb-3">
                        <div class="sum-row">
                            <span class="sum-label text-black-600">Subtotal (Excl. Tax)</span>
                            <asp:Label ID="lblExclTax" runat="server" CssClass="sum-val font-medium" />
                        </div>
                        <div class="sum-row">
                            <span class="sum-label text-black-600">Tax (18%)</span>
                            <asp:Label ID="lblTaxAmt" runat="server" CssClass="sum-val text-black-600" />
                        </div>
                        <div class="sum-row">
                            <span class="sum-label text-black-600">Subtotal (Incl. Tax)</span>
                            <asp:Label ID="lblSubtotal" runat="server" CssClass="sum-val font-medium" />
                        </div>
                        <div class="sum-row">
                            <span class="sum-label text-black-600">Shipment Charge</span>
                            <span class="sum-val font-medium text-green-600">Free</span>
                        </div>
                    </div>

                    <!-- Grand Total -->
                    <div class="flex justify-between items-center mb-4">
                        <span class="text-lg font-bold text-[#0F172A]">Grand Total</span>
                        <asp:Label ID="lblTotal" runat="server" CssClass="text-xl font-bold text-[#000]" />
                    </div>

                    <%-- Partial-only rows --%>
                    <div id="partialRows" style="display: none;" class="border-t border-dashed border-gray-200 pt-3 mb-3">
                        <div class="partial-detail-row">
                            <span class="text-black-600">Pay now (20%)</span>
                            <span class="font-medium sum-val text-black-600"><span class='text-rupee'>₹</span><span id="lblPayNow"></span></span>
                            
                            
                        </div>
                        <div class="partial-detail-row">
                            <span class="text-black-600">Pay on Delivery (80%)</span>
                            <span  class="font-medium sum-val text-black-600" ><span class='text-rupee'>₹</span><span id="lblPayCOD"></span></span>
                           <%-- <span class="font-medium text-gray-800"></span>--%>
                        </div>
                    </div>

                    <!-- Payment Options — Doc 3 card style -->
                    <div class="border-t border-gray-100 pt-4 mb-4">
                        <p class="text-sm font-bold text-gray-700 mb-3 uppercase tracking-wider">Payment Options</p>

                        <label class="pay-opt-label" id="labelFull">
                            <asp:RadioButton ID="rbFullPayment" runat="server" GroupName="paymentMethod" Checked="true" />
                            <div class="pay-opt-content">
                                <span class="pay-opt-name">Full Payment</span>
                                <span class="pay-opt-desc">Pay full amount now</span>
                            </div>
                        </label>

                        <label class="pay-opt-label" id="labelPartial">
                            <asp:RadioButton ID="rbPartialPayment" runat="server" GroupName="paymentMethod" />
                            <div class="pay-opt-content">
                                <span class="pay-opt-name">Partial Payment</span>
                                <span class="pay-opt-desc">Pay 20% now and rest COD</span>
                            </div>
                        </label>
                    </div>

                    <!-- Savings Banner -->
                    <%--   <div class="savings-banner mb-4">
                        <asp:Literal ID="litSavings" runat="server" />
                    </div>--%>

                    <!-- Terms -->
                    <div class="terms-checkbox-group flex items-start gap-2 mb-6">
                        <asp:CheckBox ID="chkTerms" runat="server" CssClass="terms-checkbox w-4 h-4 mt-0.5 rounded border-gray-300 text-[#B91C1C] focus:ring-red-200 cursor-pointer" />
                        <label for="<%= chkTerms.ClientID %>" class="text-xs text-gray-500 cursor-pointer" style="padding-top: 4px;">
                            I agree to the <a href="/tos.aspx" class="text-[#000] hover:underline">Terms and Conditions</a>
                            and <a href="/privacy-policy.aspx" class="text-[#000] hover:underline">Privacy Policy</a>
                        </label>
                    </div>

                    <!-- Mobile button -->
                    <asp:Button ID="btnPlaceOrderMobile" runat="server"
                        Text="Save Address"
                        CssClass="place-order-btn display-none-desktop w-full text-white rounded-lg font-semibold shadow-lg mb-3"
                        ValidationGroup="CheckoutForm"
                        OnClick="btnPlaceOrder_Click"
                        OnClientClick="return ValidateCheckout();" />

                    <!-- Desktop button -->
                    <div class="display-none-mobile">
                        <asp:Button ID="btnPlaceOrder" runat="server"
                            Text="Save Address"
                            CssClass="place-order-btn w-full text-white rounded-lg font-semibold shadow-lg"
                            ValidationGroup="CheckoutForm"
                            OnClick="btnPlaceOrder_Click"
                            OnClientClick="return ValidateCheckout();" />
                    </div>

                </div>
                <%-- end right --%>
            </div>
        </div>
    </main>

    <script src="https://code.iconify.design/3/3.1.0/iconify.min.js"></script>

    <!-- Snackbar -->
    <div id="tg-snackbar" role="alert" aria-live="assertive">
        <svg class="tg-snackbar__icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"
            fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
            <circle cx="12" cy="12" r="10" />
            <line x1="12" y1="8" x2="12" y2="12" />
            <line x1="12" y1="16" x2="12.01" y2="16" />
        </svg>
        <span id="tg-snackbar-text"></span>
        <button class="tg-snackbar__close" onclick="tgHideSnackbar()" aria-label="Close">&times;</button>
    </div>

    <script>

        var _tgSnackbarTimer = null;
        function tgShowSnackbar(message, bgColor) {
            var el = document.getElementById('tg-snackbar');
            document.getElementById('tg-snackbar-text').textContent = message;
            el.style.background = bgColor || '#ea1c1c';
            el.classList.add('tg-snackbar--show');
            clearTimeout(_tgSnackbarTimer);
            _tgSnackbarTimer = setTimeout(tgHideSnackbar, 4000);
        }
        function tgHideSnackbar() {
            document.getElementById('tg-snackbar').classList.remove('tg-snackbar--show');
        }


        document.getElementById('gstCheckbox').addEventListener('change', function () {
            var f = document.getElementById('gstFields');
            if (this.checked) { f.classList.add('active'); }
            else {
                f.classList.remove('active');
                document.getElementById('<%= txtCompanyName.ClientID %>').value = '';
                document.getElementById('<%= txtGSTNumber.ClientID %>').value = '';
            }
        });


        (function () {
            var partialRows = document.getElementById('partialRows');
            var payNowEl = document.getElementById('lblPayNow');
            var payCODEl = document.getElementById('lblPayCOD');
            var totalEl = document.getElementById('<%= lblTotal.ClientID %>');
            var radios = document.querySelectorAll('input[type="radio"][name$="paymentMethod"]');
            var labelFull = document.getElementById('labelFull');
            var labelPartial = document.getElementById('labelPartial');

            function parseTotal() {
                if (!totalEl) return 0;
                return parseFloat((totalEl.innerText || totalEl.textContent || '').replace(/[^\d.]/g, '')) || 0;
            }
            function fmtINR(n) {
                return  n.toLocaleString('en-IN', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
            }
            function update() {
                var isPartial = false;
                radios.forEach(function (r) {
                    if (r.checked && r.id && r.id.indexOf('rbPartialPayment') !== -1) isPartial = true;
                });

                // Card highlight
                if (labelFull) labelFull.classList.toggle('selected', !isPartial);
                if (labelPartial) labelPartial.classList.toggle('selected', isPartial);

                // Partial detail rows
                if (isPartial) {
                    var t = parseTotal();
                    payNowEl.textContent = fmtINR(t * 0.20);
                    payCODEl.textContent = fmtINR(t * 0.80);
                    partialRows.style.display = 'block';
                } else {
                    partialRows.style.display = 'none';
                }
            }
            radios.forEach(function (r) { r.addEventListener('change', update); });
            update();
        })();


        function ValidateCheckout() {

           
            if (typeof Page_ClientValidate === 'function') {
                if (!Page_ClientValidate('CheckoutForm')) {

               
                    tgShowSnackbar('Please enter all the details.');

                    var invalid = document.querySelector('.input-validation-error');
                    if (invalid) invalid.focus();

                    return false;
                }
            }

            var chk = document.getElementById('<%= chkTerms.ClientID %>');
          if (!chk || !chk.checked) {
              tgShowSnackbar('Please accept the Terms and Conditions and Privacy Policy.');
              return false;
          }

          var btn = document.activeElement;
          if (btn && btn.tagName === 'INPUT') {
              btn.value = 'Please wait...';
              btn.style.opacity = '0.7';
              btn.style.cursor = 'not-allowed';
              setTimeout(function () { btn.disabled = true; }, 100);
          }

          return true;
      }
    </script>

</asp:Content>
