<%@ Page Title="Security Poicy of TamazGlobal" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="security.aspx.cs" Inherits="security" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="canonical" href="<%= Request.Url.AbsoluteUri.ToLower() %>" />
    <meta name="description" content="Your online transaction on Tamaz Global is secure with the highest levels of transaction security currently available on the Internet." />
    <script src="https://cdn.tailwindcss.com"></script>
    <script>
        tailwind.config = {
            theme: {
                extend: {
                    colors: {
                        primary: '#0a1b50', /* Dark blue */
                        accent: '#B91C1C',  /* Red */
                        secondary: '#1E3A8A', /* Medium blue */
                        'bg-light': '#F8FAFC', /* Light off-white */
                        'text-muted': '#64748B', /* Gray for muted text */
                    }
                }
            }
        }
    </script>
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
        /* Enhanced text shadow for even better readability on the banner */
        .banner-text-shadow-strong {
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.7); /* Stronger, darker shadow */
        }
        /* Custom styles for H2 and H3 to ensure they apply */
        .content-h2 {
            font-size: 1.5rem; /* text-3xl */
            font-weight: 700; /* font-bold */
            color: #0a1b50; /* primary color */
            margin-top: 2.5rem; /* mt-10 */
            margin-bottom: 1rem; /* mb-4 */
        }

        .content-h3 {
            font-size: 1.3rem; /* text-2xl */
            font-weight: 600; /* font-semibold */
            color: #1E3A8A; /* secondary color */
            margin-top: 2rem; /* mt-8 */
            margin-bottom: 0.75rem; /* mb-3 */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="relative bg-bg-light py-0 overflow-hidden">
        <div class="py-10 bg-bg-light">
            <div class="max-w-7xl mx-auto px-4 text-center">

                <!-- Breadcrumb -->
                <nav class="text-sm text-gray-500 flex flex-wrap justify-center items-center gap-2 mb-4">
                    <a href="/" class="hover:text-primary transition whitespace-nowrap">Home</a>
                    <span class="text-gray-400">></span>
                    <span class="text-primary font-medium whitespace-nowrap">Security</span>
                </nav>

                <!-- Title -->
                <h1 class="text-4xl md:text-5xl font-bold text-primary mb-4 tracking-tight">Security
                </h1>

                <!-- Small paragraph -->
                <p class="text-gray-500 text-xs md:text-sm max-w-xl mx-auto leading-relaxed">
                    We use secure systems and industry-standard measures to protect your personal data and ensure safe transactions.
                </p>

            </div>
        </div>
    </section>

    <!-- CONTENT WITHOUT BOX - Security Policy content -->
    <section class="py-16">
        <div class="max-w-5xl mx-auto px-4 text-gray-700 leading-relaxed space-y-6">

            <h2 class="content-h2">Is it safe to use my credit/debit card on Tamaz Global?</h2>
            <p>Your online transaction on Tamaz Global is secure with the highest levels of transaction security currently available on the Internet. Tamaz Global uses 256-bit encryption technology to protect your card information while securely transmitting it to the respective banks for payment processing.</p>
            <p>All credit card and debit card payments on Tamaz Global are processed through secure and trusted payment gateways managed by leading banks. Banks now use the 3D Secure password service for online transactions, providing an additional layer of security through identity verification.</p>

            <h2 class="content-h2">Does Tamaz Global store my credit/debit card information?</h2>
            <p>No.</p>

            <h2 class="content-h2">What credit/debit cards are accepted on Tamaz Global?</h2>
            <p>We accept VISA, MasterCard, Maestro credit/debit cards.</p>

            <h2 class="content-h2">Do you accept payment made by credit/debit cards issued in other countries?</h2>
            <p>We accept VISA, MasterCard, Maestro, credit/debit cards issued by banks in India only.</p>

            <h2 class="content-h2">What other payment options are available on Tamaz Global?</h2>
            <p>Apart from Credit and Debit Cards, we accept payments by Internet Banking, Cash-on-Delivery.</p>

            <h2 class="content-h2">Privacy Policy</h2>
            <p>Tamaz Global respects your privacy and is committed to protecting it. For more details, please see our <a href="privacy-policy.aspx" class="text-blue-600 hover:underline">Privacy Policy</a>.</p>

            <h2 class="content-h2">Contact Us</h2>
            <p>Couldn't find the information you need? Please Contact Us at <a href="mailto:tamazglobal@gmail.com">tamazglobal@gmail.com</a>.</p>

        </div>
    </section>
</asp:Content>
