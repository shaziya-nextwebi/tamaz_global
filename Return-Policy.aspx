<%@ Page Title="Return Policy Of Product | TamazGlobal" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Return-Policy.aspx.cs" Inherits="Return_Policy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="canonical" href="<%= Request.Url.AbsoluteUri.ToLower() %>" />

    <meta name="description" content="Learn about TamazGlobal’s return and exchange policy: products must be unused, in original packaging, and returned within 15 days; no refunds, only exchanges. Terms & conditions included." />

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

        /* Custom styles for H2, H3, and LI to ensure they apply */
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

        .content-ul {
            list-style: disc; /* Use a disk for list items */
            padding-left: 1.5rem; /* Add left padding for bullet points */
            margin-bottom: 1rem; /* Space after the list */
        }

            .content-ul li {
                margin-bottom: 0.5rem; /* Space between list items */
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
                    <span class="text-primary font-medium whitespace-nowrap">Return Policy</span>
                </nav>

                <!-- Title -->
                <h1 class="text-4xl md:text-5xl font-bold text-primary mb-4 tracking-tight">Return Policy
                </h1>

                <!-- Small paragraph -->
                <p class="text-gray-500 text-xs md:text-sm max-w-xl mx-auto leading-relaxed">
                    We aim to ensure your satisfaction. Please review our return policy for eligibility, timelines, and refund process details.
                </p>

            </div>
        </div>
    </section>


    <!-- CONTENT WITHOUT BOX - Return Policy content -->
    <section class="py-16">
        <div class="max-w-5xl mx-auto px-4 text-gray-700 leading-relaxed space-y-6">

            <p>We will issue a product exchange for any type of product that is returned in an unused, sellable and intact condition, in its original packaging. The product has to be returned within 15 days of the delivery date. Missing out on items, wrong order or excess charges must be reported within 24 to 48 hours of delivery. We only offer exchanges; we do not offer any refunds or any type of credits.</p>

            <p>Only products bought exclusively from the Tamaz Global site can be returned back to us. We will certainly not accept returns of products bought from any other third party websites or physical stores. You can email us at <a href="mailto:tamazglobal@gmail.com">tamazglobal@gmail.com</a>. Please allow one to 2 weeks for your return to be processed, it will need to be vetted by our customer service team. You will receive a tele call or an email once your return is processed.</p>

            <h2 class="content-h2">Terms & Conditions:</h2>
            <ul class="content-ul">
                <li>All returned products have to be accompanied with a copy of the original invoice.</li>
                <li>Each item can only be exchanged once.</li>
                <li>Shipping costs will not be refunded.</li>
                <li>All gifts or promotional products received with your purchase must be returned with your order.</li>
                <li>All items bought on sale are final - we cannot offer any returns or exchanges on sale products.</li>
                <li>If the return complies with our return and exchange policy, you will be eligible to exchange the product for a product of the same or lesser value. We will not refund the difference. In the case that the items exceed the original value, you will be required to pay the difference.</li>
            </ul>
            <p><strong>Please note:</strong> Although we have tried to precisely show the actual color of the products, there may be a minor variation based on the operating system you are using.</p>

        </div>
    </section>
</asp:Content>
