<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="_404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="Page not found" />
    <link rel="canonical" href="<%= Request.Url.AbsoluteUri.ToLower() %>" />
    <style>
        /* Float Animation for 404 Text */
        @keyframes float {
            0%, 100% {
                transform: translateY(0px);
            }

            50% {
                transform: translateY(-20px);
            }
        }

        @keyframes fadeInUp {
            0% {
                opacity: 0;
                transform: translateY(30px);
            }

            100% {
                opacity: 1;
                transform: translateY(0);
            }
        }

        @keyframes pulse-ring {
            0% {
                transform: scale(0.8);
                opacity: 0.8;
            }

            100% {
                transform: scale(2);
                opacity: 0;
            }
        }

        .float-animation {
            animation: float 6s ease-in-out infinite;
        }

        .fade-in-up {
            opacity: 0;
            animation: fadeInUp 0.6s ease-out forwards;
        }

        .delay-1 {
            animation-delay: 0.2s;
        }

        .delay-2 {
            animation-delay: 0.4s;
        }

        .delay-3 {
            animation-delay: 0.6s;
        }

        .delay-4 {
            animation-delay: 0.8s;
        }

        .pulse-ring {
            animation: pulse-ring 2s cubic-bezier(0.215, 0.61, 0.355, 1) infinite;
        }

        .pulse-ring-2 {
            animation: pulse-ring 2s cubic-bezier(0.215, 0.61, 0.355, 1) 0.5s infinite;
        }

        /* Dashed circle rotation */
        @keyframes spin-slow {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }

        .spin-slow {
            animation: spin-slow 20s linear infinite;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main class="flex-1 flex items-center justify-center px-6 py-16 relative overflow-hidden">



        <!-- Grid Pattern -->
        <div class="absolute inset-0 opacity-[0.03]" style="background-image: linear-gradient(to right, rgba(0,0,0,0.1) 1px, transparent 1px), linear-gradient(to bottom, rgba(0,0,0,0.1) 1px, transparent 1px); background-size: 40px 40px;"></div>

        <div class="relative max-w-2xl mx-auto text-center">

            <!-- 404 Graphic -->
            <div class="relative inline-flex items-center justify-center mb-8">
                <!-- Pulse Rings -->
                <div class="absolute w-40 h-40 rounded-full bg-orange-100 pulse-ring"></div>
                <div class="absolute w-40 h-40 rounded-full bg-orange-50 pulse-ring-2"></div>

                <!-- 404 Text -->
                <div class="float-animation relative">
                    <h1 class="text-[150px] sm:text-[180px] font-bold tracking-tighter text-slate-900 leading-none select-none" style="text-shadow: 0 10px 30px rgba(0,0,0,0.05);">4<span class="text-orange-500">0</span>4
                    </h1>
                </div>
            </div>

            <!-- Error Text -->
            <h2 class="fade-in-up delay-1 text-2xl sm:text-3xl font-semibold tracking-tight mb-4 text-slate-900">Oops! Page Not Found
            </h2>

            <p class="fade-in-up delay-2 text-lg text-slate-500 mb-8">
                The page you're looking for doesn't exist or has been moved.
           
            </p>

            <p class="fade-in-up delay-3 text-slate-500 max-w-md mx-auto mb-10 leading-relaxed">
                Don't worry, you can return to our homepage or use the navigation to find what you need.
           
            </p>



            <!-- Action Buttons -->
            <div class="fade-in-up delay-4 flex flex-col sm:flex-row items-center justify-center gap-4">
                <a href="/" class="inline-flex items-center gap-2 btn-primary">
                    <svg style="transform: scaleX(-1);" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                        <line x1="5" y1="12" x2="19" y2="12"></line>
                        <polyline points="12 5 19 12 12 19"></polyline>
                    </svg>
                    Back to Homepage
                </a>

            </div>


        </div>
    </main>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <script>
        lucide.createIcons();
    </script>
</asp:Content>

