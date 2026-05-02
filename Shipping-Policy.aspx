<%@ Page Title="Shipping & Delivery Policy Of Product | TamazGlobal" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="shipping-policy.aspx.cs" Inherits="Shipping_Policy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <link rel="canonical" href="<%= Request.Url.AbsoluteUri.ToLower() %>" />
     <meta name="description" content="Explore Tamaz Global’s Shipping Policy to learn about delivery timelines, shipping charges, order tracking, and hassle-free shipping options for your purchases." />
    <script src="https://cdn.tailwindcss.com"></script>
    <script>
        tailwind.config = {
            theme: {
                extend: {
                    colors: {
                        primary: '#0a1b50',
                        accent: '#B91C1C',
                        secondary: '#1E3A8A',
                        'bg-light': '#F8FAFC',
                        'text-muted': '#64748B',
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
        .content-h1{
         font-size: 1.875rem; /* text-3xl */
font-weight: 700;    /* font-bold */
color: #0a1b50;      /* primary color */
margin-top: 2.5rem;  /* mt-10 */
margin-bottom: 1rem;
        }
        
        /* Custom styles for H2, H3, and LI to ensure they apply */
        .content-h2 {
            font-size: 1.5rem; /* text-3xl */
            font-weight: 700;    /* font-bold */
            color: #0a1b50;      /* primary color */
            margin-top: 2.5rem;  /* mt-10 */
            margin-bottom: 1rem; /* mb-4 */
        }
        .content-h3 {
            font-size: 1.3rem;   /* text-2xl */
            font-weight: 600;    /* font-semibold */
            color: #1E3A8A;      /* secondary color */
            margin-top: 2rem;    /* mt-8 */
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <section class="relative bg-bg-light py-0 overflow-hidden">
    <div class="py-10 bg-bg-light">
        <div class="max-w-7xl mx-auto px-4 text-center">

            <!-- Breadcrumb (Above Title) -->
            <nav class="text-sm text-gray-500 flex flex-wrap justify-center items-center gap-2 mb-4">
                <a href="/" class="hover:text-primary transition whitespace-nowrap">
                    Home
                </a>

                <span class="text-gray-400">></span>

                <span class="text-primary font-medium whitespace-nowrap">
                    Shipping Policy
                </span>
            </nav>

            <!-- Title -->
            <h1 class="text-4xl md:text-5xl font-bold text-primary mb-4 tracking-tight">
                Shipping Policy
            </h1>

            <!-- Paragraph -->
            <p class="text-gray-600 text-sm md:text-base max-w-2xl mx-auto leading-relaxed">
                We are committed to delivering your orders safely and on time. Please review our shipping policy to understand delivery timelines, charges, and process details.
            </p>

        </div>
    </div>
</section>

<!-- CONTENT WITHOUT BOX - Shipping & Delivery Policy content -->
<section class="py-16">
    <div class="max-w-5xl mx-auto px-4 text-gray-700 leading-relaxed space-y-6">

        <p>Please carefully review our Shipping & Delivery Policy when purchasing our products. This policy will apply to any order you place with us.</p>

        <h2 class="content-h2">What Are My Shipping Delivery Options?</h2>
        <p>We offer various shipping options. In some cases, a third-party supplier may be managing our inventory and will be responsible for shipping your products.</p>

        <h3 class="content-h3">Free Shipping</h3>
        <p>We offer free three-day shipping on all orders.</p>

        <h2 class="content-h2">Do You Deliver Internationally?</h2>
        <p>We do not offer international shipping.</p>

        <h2 class="content-h2">What Happens If My Order Is Delayed?</h2>
        <p>If delivery is delayed for any reason, we will let you know as soon as possible and will advise you of a revised estimated date for delivery.</p>

        <h2 class="content-h2">Questions About Returns?</h2>
        <p>If you have questions about returns, please review our <a href="return-policy.aspx" class="text-blue-600 hover:underline">Return Policy</a>.</p>

        <h2 class="content-h2">How Can You Contact Us About This Policy?</h2>
        <p>If you have any further questions or comments, you may contact us by:</p>
        <ul class="content-ul">
            <li>Email: <a href="mailto:tamazglobal@gmail.com">tamazglobal@gmail.com</a></li>
            <li>Website: <a href="https://tamazglobal.com" target="_blank" rel="noopener noreferrer">tamazglobal.com</a></li>
        </ul>

    </div>
</section>
</asp:Content>