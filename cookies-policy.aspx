<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="cookies-policy.aspx.cs" Inherits="cookies_policy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
           <section class="relative bg-bg-light py-20 overflow-hidden">
    <div class="py-10 bg-bg-light">
        <div class="max-w-7xl mx-auto px-4 text-center">

            <!-- Breadcrumb -->
            <nav class="text-sm text-gray-500 flex flex-wrap justify-center items-center gap-2 mb-4">
                <a href="/" class="hover:text-primary transition whitespace-nowrap">Home</a>
                <span class="text-gray-400">></span>
                <span class="text-primary font-medium whitespace-nowrap">Cookies Policy</span>
            </nav>

            <!-- Title -->
            <h1 class="text-4xl md:text-5xl font-bold text-primary mb-4 tracking-tight">
                Cookies Policy
            </h1>

            <!-- Small paragraph -->
            <p class="text-gray-500 text-xs md:text-sm max-w-xl mx-auto leading-relaxed">
                This Cookies Policy explains how we use cookies to improve your browsing experience and website performance.
            </p>

        </div>
    </div>
</section>

<!-- CONTENT WITHOUT BOX - Cookies Policy content -->
<section class="py-16">
    <div class="max-w-5xl mx-auto px-4 text-gray-700 leading-relaxed space-y-6">

        <p>Tamaz Global gathers details regarding the users at several stages on our web site.</p>
        <p>A cookie is a segment of data saved money on the tool's hard disk drive and comprises details concerning the individual.</p>
        <p>Several websites, including this one, employ cookie innovation to make it simpler for our customers to discover.</p>
        <p>Our site will regularly seek the cookie from your system to retrieve the saved information.</p>
        <p>Recognizing exactly how you utilize the site enables us much better to tailor our info as well as solution to your demands.</p>
        <p>As an example, by positioning a cookie on our site, the individual will certainly not need to enter their password more than when, conserving time while on our site.</p>
        <p>The cookie automatically runs out when the individual closes their browser. Even if site visitors reject the cookie, they can still access our internet site.</p>
        <p>While on our website, making use of a cookie is not associated with any individual identifiers.</p>
        <p>Cookies might be utilized on our websites by several of our company companions (such as advertising and marketing).</p>
        <p>We do not, nevertheless, have straight authority over these cookies.</p>
        <p>In most internet browsers, a customer might set up the alternatives to show a notice each time a cookie request is detected, as well as to avoid the internet browser from accepting cookies.</p>
        <p>However, to make your check out to our site as very easy as well as comfy as feasible, we advise that you enable your browser to approve cookies.</p>

    </div>
</section>
</asp:Content>