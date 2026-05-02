<%@ Page Title="Privacy Policy | TamazGlobal" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="privacy-policy.aspx.cs" Inherits="privacy_policy" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="canonical" href="<%= Request.Url.AbsoluteUri.ToLower() %>" />
    <meta name="description" content="By accessing this web site, you expressly approve the policies as well as conditions of this Privacy Plan."/>
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

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="relative bg-bg-light py-0 overflow-hidden">
    <div class="py-10 bg-bg-light">
        <div class="max-w-7xl mx-auto px-4 text-center">

            <!-- Breadcrumb -->
            <nav class="text-sm text-gray-500 flex flex-wrap justify-center items-center gap-2 mb-4">
                <a href="/" class="hover:text-primary transition whitespace-nowrap">Home</a>
                <span class="text-gray-400">></span>
                <span class="text-primary font-medium whitespace-nowrap">Privacy Policy</span>
            </nav>

            <!-- Title -->
            <h1 class="text-4xl md:text-5xl font-bold text-primary mb-4 tracking-tight">
                Privacy Policy
            </h1>

            <!-- Small paragraph -->
            <p class="text-gray-500 text-xs md:text-sm max-w-xl mx-auto leading-relaxed">
                This Privacy Policy explains how we collect, use, and protect your information when you use our website.
            </p>

        </div>
    </div>
</section>

<!-- CONTENT WITHOUT BOX -->
<section class="py-16">
    <div class="max-w-5xl mx-auto px-4 text-gray-700 leading-relaxed space-y-6">
        <div class="editor-content">
        <h2 class="content-h2">Privacy Policy</h2>

        <p>We value your self-confidence in us. That is why we stress the strenuous high quality for risk-free transactions as well as the security of consumer info.</p>

        <p>Please read the complying with disclosure to recognize more regarding our procedures for getting as well as sharing info.</p>

        <h3 class="content-h3">Note:</h3>
        <p>Please keep in mind that our privacy legislations might change any time and without warning.<br/>
        Please review this policy on a frequent basis to guarantee that you recognize any adjustments.<br/>
        By accessing this web site, you expressly approve the policies as well as conditions of this Privacy Plan.<br/>
        If you do not agree, please do not utilize, or access our website.<br/>
        You concur as well as acknowledge our usage and circulation of your personal details in line with this Privacy Policy by merely utilizing the website.</p>

        <h2 class="content-h2">Collection of Directly Identifiable Info and other Information</h2>

        <p>We collect and retain your individual info sent by you periodically when you visit our site.</p>

        <p>Our major objective is to offer you a comfortable, reliable, pleasurable, and customized experience.</p>

        <p>This allows us to deliver services and attributes that are likely to match your demands, as well as to adjust our internet site to ensure easier interaction.</p>

        <p>You can visit the Website without revealing your identity or personal details.</p>

        <p>Once you provide personal information, you are no longer anonymous. We clearly define required and optional fields wherever possible.</p>

        <p>We may collect information automatically based on your website usage.</p>

        <p>We use cookies to improve browsing experience, analyze traffic, and enhance security and advertising effectiveness.</p>

        <p>A cookie is a small data file stored on your device that helps recognize repeat users and improve website performance.</p>

        <p>Cookies may be used by business partners such as advertising providers, but we do not control them directly.</p>

        <p>You may disable cookies in your browser settings, but some website features may not work properly.</p>

        <p>When you make purchases or interact with us, we may collect details such as address, payment information, and transaction history.</p>

        <p>If you send messages, reviews, or feedback, we may store that information for support and legal purposes.</p>

        <p>When you create an account, we may collect name, email, phone number, and payment details as required.</p>

        <p>We may use your information to provide offers based on your previous interactions and preferences.</p>

        <h2 class="content-h2">Questions?</h2>

        <p>If you have any questions regarding this statement, please Email: 
            <a href="mailto:tamazglobal@gmail.com">tamazglobal@gmail.com</a>
        </p>

    </div>
        </div>
</section>
</asp:Content>