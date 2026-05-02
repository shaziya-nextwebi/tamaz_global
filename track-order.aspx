<%@ Page Title="Track Your Order - Tamaz Global" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="track-order.aspx.cs" Inherits="track_order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://fonts.googleapis.com/css2?family=Cormorant+Garamond:wght@300;400;600;700&family=DM+Sans:wght@300;400;500;600&display=swap" rel="stylesheet">
    <style>

        /* ── Page Shell ── */
        .tg-track-page {
         
           /* background: linear-gradient(135deg, #0d0d1a 0%, #1a1a2e 40%, #16213e 100%);*/
            padding: 60px 20px 80px;
            position: relative;
            overflow: hidden;
        }


        .tg-track-page::after {
            content: '';
            position: fixed;
            bottom: -150px; left: -150px;
            width: 500px; height: 500px;
            border-radius: 50%;
            background: radial-gradient(circle, rgba(233,69,96,0.04) 0%, transparent 70%);
            pointer-events: none;
        }

        /* ── Header ── */
        .tg-header {
            text-align: center;
            margin-bottom: 50px;
        }

        .tg-logo-mark {
            display: inline-block;
            width: 60px; height: 60px;
            background: linear-gradient(135deg, var(--gold-dark), var(--gold-light));
            clip-path: polygon(50% 0%,100% 25%,100% 75%,50% 100%,0% 75%,0% 25%);
            margin-bottom: 20px;
            animation: spin-slow 20s linear infinite;
            display:none;
        }

        @keyframes spin-slow { to { transform: rotate(360deg); } }

        .tg-header h1 {
         
            font-size: 28px;
            font-weight:600;
            color: var(--text-main);
            
            margin-bottom: 10px;
        }
            
        .tg-header h1 span { color: var(--gold); }

        .tg-header p {
            color: var(--text-muted);
            font-size: 1rem;
          
            
        }

        /* ── Search Bar ── */
        .tg-search-wrap {
            max-width: 620px;
            margin: 0 auto 55px;
        }

        .tg-search-box {
            display: flex;
            gap: 0;
            background: rgba(255,255,255,0.04);
            border: 1px solid var(--border);
            border-radius: 60px;
            padding: 6px 6px 6px 24px;
            backdrop-filter: blur(10px);
            transition: border-color .3s, box-shadow .3s;
        }

        .tg-search-box:focus-within {
      box-shadow: 0 0 0 4px rgb(183 182 182 / 14%);
    border-color: black;
        }

        .tg-search-input {
            flex: 1;
            background: transparent;
            border: none;
            outline: none;
            color: var(--text-main);
            font-family: 'DM Sans', sans-serif;
            font-size: 1rem;
           
        }

        .tg-search-input::placeholder { color: var(--text-muted); }

        .tg-search-btn {
            padding: 14px 30px;
            background: linear-gradient(135deg, var(--gold-dark) 0%, var(--gold-light) 100%);
            color: var(--deep);
            border: none;
            border-radius: 50px;
       
            font-size: .95rem;
            font-weight: 600;
            cursor: pointer;
            transition: transform .2s, box-shadow .2s;
          
            white-space: nowrap;
                background: #0a1b50;
    color: #fff
        }

     .tg-search-btn:hover{
         transform:translateY(-2px)
     }

        .tg-validation-msg {
            color: var(--accent);
            font-size: .85rem;
            margin-top: 8px;
            padding-left: 24px;
        }

        /* ── Two-Column Layout ── */
        .tg-result-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 28px;
            max-width: 1100px;
            margin: 0 auto;
        }

        @media (max-width: 900px) {
            .tg-result-grid { grid-template-columns: 1fr; }
        }

        /* ── Glass Card ── */
        .tg-card {
            background: rgba(255,255,255,0.04);
            border: 1px solid var(--border);
            border-radius: 12px;
            padding: 30px;
            backdrop-filter: blur(12px);
        }

        .tg-card-title {
            font-size: 1.3rem;
            font-weight: 600;
            color: var(--gold);
            margin-bottom: 22px;
          
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .tg-card-title::after {
            content: '';
            flex: 1;
            height: 1px;
            background: var(--border);
        }

        /* ── Order Info ── */
        .tg-info-row {
            display: flex;
            justify-content: space-between;
            align-items: flex-start;
            padding: 11px 0;
            border-bottom: 1px solid rgba(255,255,255,0.05);
            gap: 10px;
        }

        .tg-info-row:last-child { border-bottom: none; }

        .tg-info-label {
            font-size: .82rem;
            color: var(--text-muted);
            font-weight: 600;
        
            text-transform: uppercase;
            min-width: 130px;
        }

        .tg-info-value {
            font-size: .92rem;
            color: var(--text-main);
            font-weight: 500;
            text-align: right;
        }

        .tg-status-badge {
            display: inline-block;
            padding: 4px 14px;
            border-radius: 20px;
            font-size: .78rem;
            font-weight: 600;
           
            text-transform: uppercase;
        }

        .badge-delivered  { background: rgba(76,175,130,.15); color: #4CAF82; border: 1px solid rgba(76,175,130,.3); }
        .badge-transit    { background: rgba(201,168,76,.15);  color: var(--gold); border: 1px solid rgba(201,168,76,.3); }
        .badge-pending    { background: rgba(233,69,96,.12);   color: #E94560; border: 1px solid rgba(233,69,96,.3); }
        .badge-dispatched { background: rgba(100,160,255,.12); color: #64A0FF; border: 1px solid rgba(100,160,255,.3); }

        /* ── Vertical Timeline ── */
        .tg-timeline {
            position: relative;
            padding: 10px 0 10px 0;
          
            overflow-y: auto;
            scrollbar-width: thin;
            scrollbar-color: var(--gold-dark) transparent;
        }

        .tg-timeline::-webkit-scrollbar { width: 4px; }
        .tg-timeline::-webkit-scrollbar-track { background: transparent; }
        .tg-timeline::-webkit-scrollbar-thumb { background: var(--gold-dark); border-radius: 4px; }

        /* vertical spine */
        .tg-timeline-spine {
            position: absolute;
            left: 22px;
            top: 30px;
            bottom: 30px;
            width: 2px;
           background: rgb(24 136 0 / 73%);
            z-index: 0;
            height:90%;
        }

        .tg-step {
            position: relative;
            z-index: 1;
            display: flex;
            align-items: flex-start;
            gap: 18px;
            margin-bottom: 32px;
            animation: fadeSlideIn .45s ease both;
        }

        .tg-step:last-child { margin-bottom: 0; }

        @keyframes fadeSlideIn {
            from { opacity: 0; transform: translateX(-12px); }
            to   { opacity: 1; transform: translateX(0); }
        }

        .tg-step:nth-child(1)  { animation-delay: .05s; }
        .tg-step:nth-child(2)  { animation-delay: .12s; }
        .tg-step:nth-child(3)  { animation-delay: .19s; }
        .tg-step:nth-child(4)  { animation-delay: .26s; }
        .tg-step:nth-child(5)  { animation-delay: .33s; }
        .tg-step:nth-child(6)  { animation-delay: .40s; }

        /* circle node */
        .tg-node {
            flex-shrink: 0;
            width: 46px; height: 46px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 1.15rem;
            border: 2px solid rgba(201,168,76,0.25);
            background: rgba(255,255,255,0.04);
            transition: all .3s;
        }

        .tg-node.completed {
            border-color: rgb(24 136 0 / 73%);
                background: rgb(255 255 255);
        }

        .tg-node.active {
               border-color: orange;
    background: rgb(255 255 255);

    animation: pulse-gold 2.2s infinite;
        }

        @keyframes pulse-gold {
            0%   { box-shadow: 0 0 0 0   rgba(201,168,76,.4); }
            70%  { box-shadow: 0 0 0 12px rgba(201,168,76,0); }
            100% { box-shadow: 0 0 0 0   rgba(201,168,76,0); }
        }

        .tg-node.cancelled {
            border-color: rgba(233,69,96,.5);
            background: rgba(233,69,96,.08);
        }

        /* step content */
        .tg-step-body { flex: 1; padding-top: 5px; }

        .tg-step-label {
            font-size: 1rem;
            font-weight: 600;
            color: var(--text-main);
            margin-bottom: 4px;
           
        }

        .tg-step.active   .tg-step-label { color: var(--gold); }
        .tg-step.cancelled .tg-step-label { color: var(--accent); }

        .tg-step-desc {
            font-size: .82rem;
            color: var(--text-muted);
            line-height: 1.5;
            margin-bottom: 4px;
        }

        .tg-step-date {
          font-size: .75rem;
    color: rgb(2 2 2 / 90%);
 
        }

        .tg-map-link {
            display: inline-block;
            margin-top: 6px;
            font-size: .78rem;
            color: var(--gold);
            text-decoration: none;
            border: 1px solid rgba(201,168,76,.3);
            border-radius: 20px;
            padding: 3px 10px;
            transition: background .2s;
        }
        .tg-map-link:hover { background: rgba(201,168,76,.1); }

        /* ── Order Items Table ── */
        .tg-items-card {
            grid-column: 1 / -1;
        }

        .tg-table {
            width: 100%;
            border-collapse: collapse;
            font-size: .88rem;
        }

        .tg-table thead tr {
            background: rgba(201,168,76,.08);
        }

        .tg-table th {
            padding: 12px 14px;
            text-align: left;
            font-size: .75rem;
            font-weight: 600;
           
            text-transform: uppercase;
            color: var(--gold);
            border-bottom: 1px solid var(--border);
        }

        .tg-table td {
            padding: 12px 14px;
            border-bottom: 1px solid rgba(255,255,255,0.05);
            color: var(--text-main);
            vertical-align: middle;
        }

        .tg-table tbody tr:hover { background: rgba(255,255,255,.03); }
        .tg-table tbody tr:last-child td { border-bottom: none; }

        /* ── Alert ── */
        .tg-alert {
            max-width: 520px;
            margin: 0 auto;
            background: rgba(233,69,96,.08);
            border: 1px solid rgba(233,69,96,.3);
            border-radius: 16px;
            padding: 24px 30px;
            text-align: center;
        }

        .tg-alert-icon { font-size: 2rem; margin-bottom: 10px; }
        .tg-alert strong { color: var(--accent); display: block; margin-bottom: 6px; }
        .tg-alert p { color: var(--text-muted); font-size: .9rem; }

        /* ── Courier Link ── */
        .tg-courier-link {
            display: inline-flex;
            align-items: center;
            gap: 8px;
            margin-top: 18px;
            padding: 10px 22px;
            background: rgba(201,168,76,.08);
            border: 1px solid rgba(201,168,76,.3);
            border-radius: 30px;
            color: var(--gold);
            text-decoration: none;
            font-size: .88rem;
            font-weight: 500;
            transition: background .2s, box-shadow .2s;
        }

        .tg-courier-link:hover {
            background: rgba(201,168,76,.15);
            box-shadow: 0 4px 16px rgba(201,168,76,.2);
            color: var(--gold-light);
        }

        /* ── Responsive ── */
        @media (max-width: 768px) {
            .tg-search-box {
                padding: 5px 5px 5px 16px;
            }

            .tg-search-btn {
                padding: 12px 18px;
                font-size: .85rem;
            }

            .tg-card {
                padding: 20px 12px;
                width: 94vw;
            }

            .tg-info-label {
                min-width: 100px;
                font-size: .76rem;
            }

            .tg-track-page {
                padding: 60px 12px 80px;
            }

            .tg-info-row {
                padding: 8px 0;
            }
            .tg-header
            {
                    margin-bottom: 24px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="tg-track-page">

        <!-- Header -->
        <div class="tg-header">
            <div class="tg-logo-mark"></div>
            <h1>Track Your <span>Order</span></h1>
            <p>Enter your tracking code to follow your order's journey</p>
        </div>

        <!-- Search -->
        <div class="tg-search-wrap">
            <div class="tg-search-box">
                <asp:TextBox ID="txtOrderId" runat="server"
                    CssClass="tg-search-input"
                    Placeholder="Enter tracking code"
                    MaxLength="50" />
                <asp:Button ID="btnFindOrder" runat="server"
                    CssClass="tg-search-btn"
                    Text="Track Order"
                    ValidationGroup="FindOrder"
                    OnClick="btnFindOrder_Click" />
            </div>
            <asp:RequiredFieldValidator ID="rfv1" runat="server"
                ControlToValidate="txtOrderId"
                Display="Dynamic"
                CssClass="tg-validation-msg"
                ValidationGroup="FindOrder"
                ErrorMessage="⚠ Please enter a tracking code." />
        </div>

        <!-- Results Grid -->
        <div class="tg-result-grid">

            <!-- ── Left: Order Details ── -->
            <div runat="server" id="orderDetailsDiv" visible="false">
                <div class="tg-card">
                    <div class="tg-card-title">📦 Order Details</div>

                    <div class="tg-info-row">
                        <span class="tg-info-label">Booked On</span>
                        <span class="tg-info-value"><%= strBookedDate %></span>
                    </div>
                    <div class="tg-info-row">
                        <span class="tg-info-label">Tracking No</span>
                        <span class="tg-info-value"><%= strShipmentNo %></span>
                    </div>
                   <%-- <div class="tg-info-row">
                        <span class="tg-info-label">Origin</span>
                        <span class="tg-info-value"><%= strOrigin %></span>
                    </div>
                    <div class="tg-info-row">
                        <span class="tg-info-label">Destination</span>
                        <span class="tg-info-value"><%= strDestination %></span>
                    </div>--%>
                    <div class="tg-info-row">
                        <span class="tg-info-label">Items</span>
                        <span class="tg-info-value"><%= strPieces %></span>
                    </div>
                    <div class="tg-info-row">
                        <span class="tg-info-label">Status</span>
                        <span class="tg-info-value"><%= strStatusBadge %></span>
                    </div>
                    <div class="tg-info-row">
                        <span class="tg-info-label">Expected Delivery</span>
                        <span class="tg-info-value"><%= strExpectedDeliveryDate %></span>
                    </div>
                    <div class="tg-info-row">
                        <span class="tg-info-label">Payment</span>
                        <span class="tg-info-value"><%= strPaymentType %></span>
                    </div>
                    <div class="tg-info-row">
                        <span class="tg-info-label">Payment Status</span>
                        <span class="tg-info-value"><%= strPaymentStatus %></span>
                    </div>
                    <div class="tg-info-row">
                        <span class="tg-info-label">Total</span>
                        <span class="tg-info-value"><%= strGrandTotal %></span>
                    </div>
                </div>
            </div>

            <!-- ── Right: Timeline ── -->
            <div runat="server" id="detailDiv" visible="false">
                <div class="tg-card">
                    <div class="tg-card-title">🗺 Tracking Timeline</div>
                    <div class="tg-timeline">
                        <div class="tg-timeline-spine"></div>
                        <%= strTrackDetails %>
                    </div>
                    <%= strCourierLink %>
                </div>
            </div>

            <!-- ── Full Width: Order Items ── -->
            <div runat="server" id="orderItemDetailsDiv" visible="false" class="tg-items-card">
                <div class="tg-card">
                    <div class="tg-card-title">🛍 Order Items</div>
                    <div style="overflow-x:auto;">
                        <table class="tg-table">
                            <thead>
                                <tr>
                                    <th>Order ID</th>
                                    <th>Product</th>
                                    <th>Qty</th>
                                    <th>Unit Price</th>
                                    <th>Line Total</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%= strOrderItems %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <!-- ── Not Found ── -->
            <div runat="server" id="noDetailsDiv" visible="false" style="grid-column:1/-1;">
                <div class="tg-alert">
                    <div class="tg-alert-icon">🔍</div>
                    <strong>No order found</strong>
                    <p>We couldn't find any order matching that tracking code.<br>Please double-check and try again.</p>
                </div>
            </div>

        </div>
    </section>

</asp:Content>
