<%@ Page Title="Pay Now | TamazGlobal" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="paynow.aspx.cs" Inherits="PayNow" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="canonical" href="<%= Request.Url.AbsoluteUri.ToLower() %>" />
    <meta name="description" content="Tamaz Global - Complete your payment securely." />
    <style>
        .pay-card {
            background: #fff;
            border-radius: 14px;
            border: 1px solid #e2e8f0;
            box-shadow: 0 4px 24px rgba(0,0,0,0.07);
            overflow: hidden;
        }

        .pay-card-header {
            background: #0a1b50;
            padding: 12px 16px;
        }

            .pay-card-header.order-summary-header {
                background: #fafafa;
                border-bottom: 1px solid #e2e8f0;
            }

            .pay-card-header h2 {
                color: #fff;
                margin: 0;
                font-size: 18px;
                font-weight: 700;
                display: flex;
                align-items: center;
                gap: 8px;
            }

            .pay-card-header.order-summary-header h2 {
                color: #000;
            }

        .pay-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 8px 0;
            border-bottom: 1px solid #f1f5f9;
            font-size: 14px;
        }

            .pay-row:last-child {
                border-bottom: none !important;
            }

            .pay-row .label {
                color: #131314;
            }

            .pay-row .value {
                font-weight: 600;
                color: #000;
            }

            .pay-row.total-row .label {
                font-size: 16px;
                font-weight: 700;
                color: #0f172a;
            }

            .pay-row.total-row .value {
                font-size: 18px;
                font-weight: 700;
                color: #000;
            }

        .delivery-block {
            background: #f8fafc;
            border-radius: 10px;
            padding: 8px 16px;
            margin-top: 20px;
            border: 1px solid #dfdfdf;
        }

            .delivery-block p {
                margin: 0 0 6px;
                font-size: 13px;
                color: #000;
                line-height: 1.6;
            }

                .delivery-block p:last-child {
                    margin-bottom: 0;
                }

        .product-mini-item {
            display: flex;
            align-items: center;
            gap: 12px;
            padding: 10px 0;
            border-bottom: 1px solid #f1f5f9;
        }

            .product-mini-item:last-child {
                border-bottom: none;
            }

            .product-mini-item img {
                width: 52px;
                height: 52px;
                object-fit: cover;
                border-radius: 8px;
                border: 1px solid #e2e8f0;
                flex-shrink: 0;
            }

            .product-mini-item .name {
                font-size: 13px;
                font-weight: 600;
                color: #1e293b;
            }

            .product-mini-item .meta {
                font-size: 12px;
                color: #64748b;
                margin-top: 2px;
            }

        /* Payment loader overlay */
        #paymentLoader {
            display: none;
            position: fixed;
            inset: 0;
            background: rgba(255,255,255,0.95);
            z-index: 99999;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            gap: 16px;
        }

            #paymentLoader.show {
                display: flex;
            }

            #paymentLoader .loader-spinner {
                width: 48px;
                height: 48px;
                border: 4px solid #e2e8f0;
                border-top-color: #0a1b50;
                border-radius: 50%;
                animation: spin 0.8s linear infinite;
            }

        @keyframes spin {
            to {
                transform: rotate(360deg);
            }
        }

        .razorpay-payment-button {
            display: none !important; /* hidden — we trigger it programmatically */
        }

        .pay-now-btn {
            width: 100%;
            background: #226C22;
            color: #fff;
            border: none;
            border-radius: 10px;
            padding: 14px;
            font-size: 15px;
            font-weight: 700;
            cursor: pointer;
            letter-spacing: 0.3px;
            transition: background 0.2s;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
        }

            .pay-now-btn:hover {
                background: #1a5219;
            }

        .payment-type-badge {
            display: inline-flex;
            align-items: center;
            gap: 6px;
            background: #eff6ff;
            color: #1d4ed8;
            font-size: 12px;
            font-weight: 600;
            padding: 4px 10px;
            border-radius: 20px;
            border: 1px solid #bfdbfe;
        }

        .secure-badges {
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
            margin-top: 12px;
            font-size: 11px;
            color: #94a3b8;
        }

        .pay-row.total-row {
            margin-top: 4px;
            padding-top: 8px;
            border-bottom: none;
        }

        .address-head {
            font-size: 16px !important;
            font-weight: 700 !important;
            color: #000000 !important;
            letter-spacing: 0.5px !important;
            margin-bottom: 12px !important;
            border-bottom: 1px solid #eaeaea;
            letter-spacing: 0;
            line-height: 1.5;
        }

        .back-to-checkout {
            font-size: 14px;
            color: #1a1a6e;
            text-decoration: none;
            display: inline-flex;
            align-items: center;
            gap: 4px;
            font-weight: 600;
        }

        .pay-card-body {
            padding: 20px;
        }

        @media(max-width:567px) {
            .pay-card-body {
                padding: 12px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Breadcrumb -->
    <div class="bg-[#f8fafc] py-1 md:py-3 border-b border-gray-200">
        <div class="max-w-7xl mx-auto px-3 md:px-4">
            <div class="flex items-center gap-2 text-sm text-slate-500 text-[#64748B] breadcrumb breadCrumb-text">
                <a href="/Default.aspx" class="hover:text-red-700 transition-colors">Home</a>
                <span class="iconify w-4 h-4" data-icon="lucide:chevron-right"></span>
                <a href="/cart.aspx" class="hover:text-red-700 transition-colors">Cart</a>
                <span class="iconify w-4 h-4" data-icon="lucide:chevron-right"></span>
                <a href="/checkout.aspx" class="hover:text-red-700 transition-colors">Checkout</a>
                <span class="iconify w-4 h-4" data-icon="lucide:chevron-right"></span>
                <span class="text-slate-900 font-medium">Pay Now</span>
            </div>
        </div>
    </div>

    <!-- Payment loader overlay -->
    <div id="paymentLoader">
        <div class="loader-spinner"></div>
        <p style="font-size: 15px; font-weight: 600; color: #0a1b50; margin: 0;">Processing your payment&hellip;</p>
        <p style="font-size: 13px; color: #64748b; margin: 0;">Please do not refresh or go back.</p>
    </div>

    <section class="section-padding bg-white">
        <div class="max-w-7xl mx-auto px-3 md:px-4">

            <div class="grid lg:grid-cols-5 gap-8">

                <!-- ── LEFT: Order Summary ── -->
                <div class="lg:col-span-3">
                    <div class="pay-card">
                        <div class="pay-card-header order-summary-header">
                            <h2>
                                <span class="iconify w-5 h-5" data-icon="lucide:receipt-text"></span>
                                Order Summary
                            </h2>
                        </div>
                        <div class="pay-card-body">

                            <!-- Product list -->
                            <div style="margin-bottom: 16px;">
                                <%= strProductListHtml %>
                            </div>

                            <!-- Totals -->
                            <div class="pay-row">
                                <span class="label">Subtotal (Excl. Tax)</span>
                                <span class="value">
                                    <asp:Literal ID="litSubtotal" runat="server" /></span>
                            </div>
                            <div class="pay-row">
                                <span class="label">Tax (18%)</span>
                                <span class="value"><span class='text-rupee'>₹</span><%= strTaxAmount %></span>
                            </div>
                            <div class="pay-row">
                                <span class="label">Shipping</span>
                                <span class="value" style="color: #2b2b2b;">Free</span>
                            </div>
                            <div class="pay-row total-row">
                                <span class="label">Total</span>
                                <span class="value">
                                    <asp:Literal ID="litTotal" runat="server" /></span>
                            </div>

                            <!-- Delivery address block -->
                            <div class="delivery-block">
                                <p class="address-head">Delivery Address</p>
                                <p><strong><%= strBuyerName %></strong></p>
                                <p><%= strDeliveryAddress %></p>
                                <p>
                                    <span class="iconify w-3 h-3" data-icon="lucide:phone" style="display: inline; vertical-align: middle;"></span>
                                    &nbsp;<%= strBuyerMobile %>
                                    &nbsp;&nbsp;
                                   
                                    <span class="iconify w-3 h-3" data-icon="lucide:mail" style="display: inline; vertical-align: middle;"></span>
                                    &nbsp;<%= strBuyerEmail %>
                                </p>
                                <% if (!string.IsNullOrEmpty(strCompanyName))
                                    { %>
                                <p>Company: <strong><%= strCompanyName %></strong> &nbsp;|&nbsp; GST: <strong><%= strGSTNumber %></strong></p>
                                <% } %>
                            </div>

                        </div>
                    </div>
                </div>

                <!-- ── RIGHT: Payment Panel ── -->
                <div class="lg:col-span-2">
                    <div class="pay-card">
                        <div class="pay-card-header">
                            <h2>
                                <span class="iconify w-5 h-5" data-icon="lucide:credit-card"></span>
                                Payment
                            </h2>
                        </div>
                        <div class="pay-card-body">

                            <p style="font-size: 13px; color: #000; margin: 0 0 16px;">
                                You are about to pay for your order at <strong>TAMAZ Global</strong>.
                                Complete the payment securely via Razorpay.
                           
                            </p>

                            <!-- Payment type badge -->
                            <div style="margin-bottom: 20px;">
                                <span class="payment-type-badge">
                                    <span class="iconify w-3 h-3" data-icon="lucide:tag"></span>
                                    <%= strPaymentTypeBadge %>
                                </span>
                            </div>

                            <!-- Amount to pay -->
                            <div style="background: #f0fdf4; border: 1px solid #bbf7d0; border-radius: 10px; padding: 16px 20px; margin-bottom: 24px; text-align: center;">
                                <p style="font-size: 12px; color: #16a34a; font-weight: 600; margin: 0 0 4px; letter-spacing: 0.5px;">AMOUNT TO PAY</p>
                                <p style="font-size: 28px; font-weight: 800; color: #000; margin: 0;">
                                    <asp:Literal ID="litPayableAmount" runat="server" />
                                </p>
                            </div>

                            <%-- Hidden fields outside any form tag --%>
                            <input type="hidden" id="razorpay_payment_id" />
                            <input type="hidden" id="razorpay_order_id" value="<%= strRazorOrderId %>" />
                            <input type="hidden" id="razorpay_signature" />

                            <button type="button" class="pay-now-btn" id="btnPayNow" onclick="openRazorpay()">
                                <span class="iconify w-5 h-5" data-icon="lucide:shield-check"></span>
                                Pay &#8377;<%= strPayableAmountDisplay %> Securely
                            </button>

                            <%--     <div class="secure-badges">
                                <span class="iconify w-3 h-3" data-icon="lucide:lock"></span>
                                256-bit SSL secured &nbsp;&bull;&nbsp; Powered by Razorpay
                           
                            </div>--%>
                        </div>
                    </div>

                    <!-- Back to checkout -->
                    <div style="margin-top: 14px; text-align: center;">
                        <a href="/checkout.aspx" class="back-to-checkout">
                            <span class="iconify w-4 h-4" data-icon="lucide:arrow-left"></span>
                            Back to Checkout
</a>
                    </div>

                </div>

            </div>
        </div>
    </section>

</asp:Content>

<asp:Content ID="ScriptsContent" ContentPlaceHolderID="scripts" runat="server">
    <script src="https://code.iconify.design/3/3.1.0/iconify.min.js"></script>
    <script src="https://checkout.razorpay.com/v1/checkout.js"></script>
    <script>
        function openRazorpay() {
            var options = {
                key:         '<%= strRazorKey %>',
                amount:      '<%= strBuyerAmountPaise %>',
                currency: 'INR',
                name: 'TAMAZ Global',
                description: 'Order Payment',
                order_id:    '<%= strRazorOrderId %>',
                image: '/assests/Images/logo.png',
                prefill: {
                    name:    '<%= strBuyerName %>',
            email:   '<%= strBuyerEmail %>',
            contact: '<%= strBuyerMobile %>'
                },
                theme: { color: '#0a1b50' },


                handler: function (response) {
                    document.getElementById('paymentLoader').classList.add('show');

                    fetch('PaymentCallback.ashx', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        body: 'razorpay_payment_id=' + encodeURIComponent(response.razorpay_payment_id) +
                            '&razorpay_order_id=' + encodeURIComponent(response.razorpay_order_id) +
                            '&razorpay_signature=' + encodeURIComponent(response.razorpay_signature)
                    }).then(function (res) {
                        res.text().then(function (txt) {
                            console.log('Callback response:', txt); // ← check browser console
                            if (res.ok) {
                                window.location.href = '/payment-success.aspx';
                            } else {
                                document.getElementById('paymentLoader').classList.remove('show');
                                tgShowSnackbar('Error: ' + txt);
                            }
                        });
                    })
                        .catch(function () {
                            document.getElementById('paymentLoader').classList.remove('show');
                            tgShowSnackbar('Something went wrong. Please contact support.');
                        });
                },
                // ↑ end of handler ↑

                modal: {
                    ondismiss: function () {
                        // User closed Razorpay without paying — do nothing
                    }
                }
            };

            var rzp = new Razorpay(options);
            rzp.on('payment.failed', function (response) {
                tgShowSnackbar('Payment failed: ' + response.error.description);
            });
            rzp.open();
        }


        var _tgSnackbarTimer = null;
        function tgShowSnackbar(message) {
            var existing = document.getElementById('tg-snackbar-pay');
            if (!existing) {
                var el = document.createElement('div');
                el.id = 'tg-snackbar-pay';
                el.style.cssText = 'position:fixed;top:24px;right:24px;z-index:999999;background:#ea1c1c;color:#fff;padding:14px 18px;border-radius:8px;font-size:14px;font-weight:500;box-shadow:0 4px 16px rgba(0,0,0,0.18);max-width:340px;';
                document.body.appendChild(el);
                existing = el;
            }
            existing.textContent = message;
            existing.style.display = 'block';
            clearTimeout(_tgSnackbarTimer);
            _tgSnackbarTimer = setTimeout(function () { existing.style.display = 'none'; }, 5000);
        }
    </script>
</asp:Content>
