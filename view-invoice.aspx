<%@ Page Language="C#" AutoEventWireup="true" CodeFile="view-invoice.aspx.cs" Inherits="Admin_view_invoice" %>

<!doctype html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Invoice #<%=strInvoiceNo %> | Thamaz Global</title>
    
    <!-- Modern Font -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap" rel="stylesheet">

    <style>
        /* ── CSS Variables for Branding ── */
        :root {
            --primary: #0C447C;      /* Deep Blue */
            --primary-light: #EBF3FA;
            --accent: #f97316;       /* Orange for highlights */
            --text-dark: #000;
            --text-gray: #64748b;
            --text-light: #94a3b8;
            --border: #e2e8f0;
            --bg-body: #f8fafc;
            --success: #10b981;
            --danger: #ef4444;
        }

        /* ── Reset & Base ── */
        *, *::before, *::after { box-sizing: border-box; margin: 0; padding: 0; }

        body {
            background: var(--bg-body);
            font-family: 'Inter', -apple-system, BlinkMacSystemFont, sans-serif;
            color: var(--text-dark);
            min-height: 100vh;
            padding: 2rem 1rem;
            -webkit-print-color-adjust: exact; /* Force print colors */
            print-color-adjust: exact;
        }

        /* ── Page Container ── */
        .invoice-wrapper {
            max-width: 900px;
            margin: 0 auto;
            background: #ffffff;
            box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.1);
            border-radius: 16px;
            overflow: hidden;
            position: relative;
        }

        /* ── Header Section ── */
        .invoice-header {
    
    background: #ffffff;
    padding: 20px 24px;
    color: #fff;
    display: flex;
    justify-content: space-between;
    align-items: flex-start;
    position: relative;
    border-bottom: 1px solid #efefef;
        }

        /* Decorative background circle */
        .invoice-header::after {
            content: '';
            position: absolute;
            top: -50px;
            right: -50px;
            width: 200px;
            height: 200px;
            background: rgba(255, 255, 255, 0.05);
            border-radius: 50%;
            pointer-events: none;
        }

        .brand-details h1 {
            font-size: 1.5rem;
            font-weight: 700;
            margin-bottom: 0.5rem;
            letter-spacing: -0.025em;
        }

        .brand-details p {
            font-size: 0.875rem;
            color: rgba(255, 255, 255);
            line-height: 1.6;
        }

        .invoice-meta {
            text-align: right;
            position: relative;
            z-index: 2;
        }

        .invoice-number-badge {
            background: rgba(255, 255, 255, 0.15);
            backdrop-filter: blur(10px);
            padding: 0.5rem 1rem;
            border-radius: 8px;
            font-size: 0.75rem;
            font-weight: 600;
            letter-spacing: 0.05em;
            text-transform: uppercase;
            display: inline-block;
            margin-bottom: 1rem;
            display:none;
        }

        .invoice-total-display {
    font-size: 1.5rem;
    font-weight: 700;
    line-height: 1;
    margin-bottom: 0.5rem;
    color: black;
        }

        .invoice-date {
            font-size: 0.875rem;
color: #000;
        }
        .invoice-date.mt-8{
            margin-top:8px;
        }

        /* ── Body Section ── */
        .invoice-body {
            padding: 1.5rem;
        }

        /* ── Parties Grid ── */
        .parties-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 3rem;
            
            padding-bottom: 1.5rem;
            
        }

        .party-block h4 {
            font-size: 0.75rem;
           
           
            color: #000000;
            margin-bottom: 0.75rem;
            font-weight: 600;
        }

        .party-block h3 {
            font-size: 1.1rem;
            font-weight: 700;
            margin-bottom: 0.5rem;
            color: var(--text-dark);
        }

        .party-block p {
            font-size: 0.9rem;
            color: #000000;
            line-height: 1.7;
        }

        /* ── Items Table ── */
        .table-container {
            margin-bottom: 1rem;
            overflow-x: auto;
                    border-radius: 6px;
    border: 1px solid #cccccc;
        }

        .modern-table {
            width: 100%;
            border-collapse: collapse;
        }

        .modern-table thead th {
       text-align: left;
    font-size: 0.75rem;

    letter-spacing: 0.05em;
    color: #333333;
    padding: 0.5rem;
    border-bottom: 2px solid #e2e8f0;
    font-weight: 600;
    background: #e2e8f0;
        }

        .modern-table thead th.text-right { text-align: right; }
        .modern-table thead th.text-center { text-align: center; }

        .modern-table tbody tr {
            border-bottom: 1px solid #cccccc;
            transition: background 0.2s;
        }
        
        .modern-table tbody tr:last-child { border-bottom: none; }

        .modern-table td {
            padding: 1.25rem 1rem;
            vertical-align: middle;
            color: #000;
            font-size: 0.95rem;
        }

        .modern-table td.text-right { text-align: right; font-weight: 600; }
        .modern-table td.text-center { text-align: center; }

        /* Product styling */
        .product-name-main {
            font-weight: 600;
            color: var(--text-dark);
            display: block;
        }
        
        /* Quantity Badge */
        .qty-pill {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            min-width: 32px;
            height: 28px;
            background: var(--primary-light);
            color: var(--primary);
            border-radius: 6px;
            font-size: 0.85rem;
            font-weight: 600;
            padding: 0 8px;
        }

        /* ── Totals Summary ── */
        .totals-summary {
            display: flex;
            justify-content: flex-end;
        }

        .summary-card {
            width: 100%;
            max-width: 380px;
            background: #f8fafc;
            border-radius: 12px;
            padding: 1.5rem;
                box-shadow: 0px 2px 3px rgb(0 0 0 / 24%);
        }

        .summary-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 0.75rem;
            font-size: 0.8rem;
            color: black;
        }

        .summary-row.divider {
            margin-top: 0.7rem;
            padding-top: 0.7rem;
            border-top: 1px solid var(--border);
            margin-bottom: 0;
        }

        .summary-label { font-weight: 500; }
        .summary-value { font-weight: 600; color: var(--text-dark); }

        .grand-total-row {
            margin-top: 1rem;
            padding-top: 1rem;
           border-top: 2px solid #0a1b50;
        }

        .grand-total-label {
            font-size: 1rem;
            font-weight: 700;
            color:#000;
        }

        .grand-total-value {
            font-size: 1.5rem;
            font-weight: 700;
        color: #000000;
        }

        .paid-text { color: #109a09; }
        .remain-text { color: var(--danger); }

        /* ── Payment Info Bar ── */
        .payment-bar {
            display: flex;
            align-items: center;
            gap: 1rem;
            background: linear-gradient(135deg, #f0fdf4 0%, #dcfce7 100%);
            border: 1px solid #bbf7d0;
            padding: 1rem 1.5rem;
            border-radius: 10px;
            margin-top: 2rem;
        }

        .pay-icon {
            width: 40px;
            height: 40px;
            background: #ffffff;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            color: var(--success);
            box-shadow: 0 2px 4px rgba(0,0,0,0.05);
        }

        .pay-details {
            flex: 1;
        }

        .pay-label {
            font-size: 0.75rem;
            color: var(--text-gray);
            text-transform: uppercase;
            letter-spacing: 0.05em;
        }

        .pay-id {
            font-weight: 600;
            color: var(--text-dark);
            font-family: monospace;
            letter-spacing: 0.02em;
        }

        /* ── Footer ── */
        .invoice-footer {
            background: #f8fafc;
               padding: 0.5rem 1rem;
            border-top: 1px solid var(--border);
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .footer-note {
            font-size: 0.8rem;
            color: #333333;
        }

        .print-btn {
            display: inline-flex;
            align-items: center;
            gap: 0.5rem;
            background: var(--primary);
            color: #fff;
            border: none;
            padding: 0.75rem 1.5rem;
            border-radius: 8px;
            font-weight: 600;
            font-size: 0.9rem;
            cursor: pointer;
            transition: all 0.2s;
            box-shadow: 0 4px 6px -1px rgba(12, 68, 124, 0.2);
        }

        .print-btn:hover {
            background: #0a3563;
            transform: translateY(-1px);
            box-shadow: 0 6px 10px -1px rgba(12, 68, 124, 0.3);
        }

        .print-btn svg {
            width: 18px;
            height: 18px;
        }

        /* ── Status Pill Colors ── */
        .status-paid { background: #dcfce7; color: #166534; border: 1px solid #bbf7d0; }
        .status-pending { background: #fff7ed; color: #9a3412; border: 1px solid #ffedd5; }
        .invoice-logo img{
            width:80px !important;
        }
        /* ── Responsive ── */
        @media (max-width: 768px) {
            .invoice-header {
                
                text-align: left;
                padding: 12px;
            }
            .invoice-meta {
                text-align: left;
                
            }
            .invoice-total-display
            {
                font-size:20px;
                text-align:end;
            }
            .invoice-date
            {
                font-size:0.7rem;
                text-align:end;
            }
            body
            {
                    padding: 2rem 0.5rem;
            }
            .parties-grid {
                grid-template-columns: 1fr;
                gap: 1rem;
            }
            .invoice-body { padding: 0.75rem; }
            .invoice-footer { 
                flex-direction: column; 
                gap: 1rem;
                text-align: center;
            }
              .invoice-logo img{
      width:60px !important;
  }
        }

        /* ── Print Specific ── */
        @media print {
            body { background: #fff; padding: 0; }
            .invoice-wrapper { box-shadow: none; border-radius: 0; max-width: 100%; }
            .print-btn { display: none; }
            .invoice-header, .modern-table thead th, .qty-pill, .payment-bar {
                -webkit-print-color-adjust: exact; 
                print-color-adjust: exact; 
            }
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="invoice-wrapper">

            <!-- ═══ HEADER ═══ -->
            <div class="invoice-header">
                <div class="brand-details">
           <%--         <h1>Thamaz Global Trading Co.</h1>--%>
                    <div class="invoice-logo">
                                <img src="Admin/assets/images/logo.png" />

                        </div>
                  
                    <%--<p>
                        No 40, Unit no 104, 1st Floor, Promenade Road,<br />
                        Frazer Town, Bangalore – 560005, India<br />
                        <span style="font-size:0.8rem; opacity:0.7;">GST: XXXXXXXX</span>
                    </p>--%>
                </div>

                <div class="invoice-meta">
                    <div class="invoice-number-badge">INVOICE #<%=strInvoiceNo %></div>
                    <div class="invoice-total-display">₹<%=strFinalAmount.Replace("₹", "") %></div>
                    <div class="invoice-date">
                        Date: <%=strOrderDate %> 
                       
                    </div>
                    <div class="invoice-date mt-8">
                         Status: <span class="status-paid" style="padding:0px 8px; border-radius:4px; font-size:0.7rem; font-weight:600;"><%=strPaymentStatus %></span>
                    </div>
                </div>
            </div>

            <!-- ═══ BODY ═══ -->
            <div class="invoice-body">

                <!-- Parties Section -->
                <div class="parties-grid">
                    <div class="party-block">
                        <h4>Billed To</h4>
                        <h3><%=strName %></h3>
                        <p>
                            <%=strAddressLine1 %><br />
                            <%=strAddressLine3 %><br />
                            <%=strAddressLine4 %>
                        </p>
                    </div>
                    <div class="party-block" style="text-align:right;">
                        <h4>Pay To</h4>
                        <h3>Tamaz Global Trading Co.</h3>
                        <p>
                            No 40, Unit 104, 1st Floor<br />
                            Promenade Road, Frazer Town<br />
                            Bangalore – 560005
                        </p>
                    </div>
                </div>

                <!-- Items Table -->
                <div class="table-container">
                    <table class="modern-table">
                        <thead>
                            <tr>
                                <th style="width:40px;">#</th>
                                <th>Product Description</th>
                                <th>Unit Price</th>
                                <th class="text-center">Qty</th>
                                <th class="text-right">Line Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            <!-- Backend generated rows will go here. 
                                 We need to slightly adjust backend classes to match new design if needed, 
                                 but I have styled the existing classes below. -->
                            <%=strItems %>
                        </tbody>
                    </table>
                </div>

                <!-- Totals -->
                <div class="totals-summary">
                    <div class="summary-card">
                        <div class="summary-row">
                            <span class="summary-label">Subtotal (excl. tax)</span>
                            <span class="summary-value"><%=strSunTotalWithoutTax %></span>
                        </div>
                        <div class="summary-row">
                            <span class="summary-label">GST (18%)</span>
                            <span class="summary-value"><%=strTax %></span>
                        </div>
                        <div class="summary-row">
                            <span class="summary-label">Shipping</span>
                            <span class="summary-value" style="color:#109a09;">Free</span>
                        </div>

                        <div class="summary-row grand-total-row">
                            <span class="grand-total-label">Total Amount</span>
                            <span class="grand-total-value"><%=strFinalAmount %></span>
                        </div>

                        <div class="summary-row divider">
                            <span class="summary-label">Amount Paid</span>
                            <span class="summary-value paid-text"><%=strPaidAmount %></span>
                        </div>

                        <% if (isPartial) { %>
                        <div class="summary-row" style="margin-top:0.5rem;">
                            <span class="summary-label">Balance Due (COD)</span>
                            <span class="summary-value remain-text"><%=strRemainingAmount %></span>
                        </div>
                        <% } %>
                    </div>
                </div>

                <!-- Payment ID -->
                <% if (!string.IsNullOrEmpty(strPaymentId)) { %>
                <div class="payment-bar">
                    <div class="pay-icon">
                        <svg viewBox="0 0 24 24" width="20" height="20" stroke="currentColor" stroke-width="2" fill="none" stroke-linecap="round" stroke-linejoin="round">
                            <rect x="1" y="4" width="22" height="16" rx="2" ry="2"></rect>
                            <line x1="1" y1="10" x2="23" y2="10"></line>
                        </svg>
                    </div>
                    <div class="pay-details">
                        <div class="pay-label">Transaction ID</div>
                        <div class="pay-id"><%=strPaymentId %></div>
                    </div>
                </div>
                <% } %>

            </div>

            <!-- ═══ FOOTER ═══ -->
            <div class="invoice-footer">
                <span class="footer-note">This is a system generated invoice. Thank you for your business.</span>
                <button type="button" class="print-btn" onclick="window.print()">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                        <polyline points="6 9 6 2 18 2 18 9"></polyline>
                        <path d="M6 18H4a2 2 0 0 1-2-2v-5a2 2 0 0 1 2-2h16a2 2 0 0 1 2 2v5a2 2 0 0 1-2 2h-2"></path>
                        <rect x="6" y="14" width="12" height="8"></rect>
                    </svg>
                    Print Invoice
                </button>
            </div>

        </div>
    </form>

    <!-- Small script to map backend classes to frontend design logic -->
    <!-- This allows your backend code to remain EXACTLY as is, while we visually restyle the output -->
    <script>
        // We will run a small script on load to fix the generated table rows classes
        // so they match the new design without changing your C# code.
        document.addEventListener("DOMContentLoaded", function() {
            const rows = document.querySelectorAll(".modern-table tbody tr");
            rows.forEach((row, index) => {
                // Remove old inline styles if any (or just rely on CSS cascade)
                // Add utility classes to cells for alignment
                const cells = row.querySelectorAll("td");
                if(cells.length >= 5) {
                    // Map: [#, Product, Price, Qty, Total]
                    cells[2].classList.add("text-right"); // Price
                    cells[3].classList.add("text-center"); // Qty
                    cells[4].classList.add("text-right"); // Total
                    
                    // Make product name nicer
                    const productSpan = cells[1].querySelector(".inv-product-name");
                    if(productSpan) productSpan.classList.add("product-name-main");
                    
                    // Make qty pill nicer
                    const qtySpan = cells[3].querySelector(".inv-qty-badge");
                    if(qtySpan) qtySpan.classList.add("qty-pill");
                }
            });
        });
    </script>
</body>
</html>