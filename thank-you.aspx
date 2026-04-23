<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="thank-you.aspx.cs" Inherits="thank_you" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        @keyframes scaleIn {
            0% {
                transform: scale(0);
                opacity: 0;
            }

            50% {
                transform: scale(1.1);
            }

            100% {
                transform: scale(1);
                opacity: 1;
            }
        }

        @keyframes checkDraw {
            0% {
                stroke-dashoffset: 100;
            }

            100% {
                stroke-dashoffset: 0;
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

        .success-icon {
            animation: scaleIn 0.6s cubic-bezier(0.175, 0.885, 0.32, 1.275) forwards;
        }

        .check-mark {
            stroke-dasharray: 100;
            stroke-dashoffset: 100;
            animation: checkDraw 0.6s ease-out 0.3s forwards;
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
            animation: pulse-ring 1.5s cubic-bezier(0.215, 0.61, 0.355, 1) infinite;
        }

        .pulse-ring-2 {
            animation: pulse-ring 1.5s cubic-bezier(0.215, 0.61, 0.355, 1) 0.3s infinite;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <main class="flex-1 flex items-center justify-center px-6 py-16 relative overflow-hidden">



        <!-- Grid Pattern -->
        <div class="absolute inset-0 opacity-[0.02]" style="background-image: linear-gradient(to right, rgba(255,255,255,0.1) 1px, transparent 1px), linear-gradient(to bottom, rgba(255,255,255,0.1) 1px, transparent 1px); background-size: 40px 40px;"></div>

        <div class="relative max-w-2xl mx-auto text-center">

            <!-- Success Icon -->
            <div class="relative inline-flex items-center justify-center mb-8">
                <!-- Pulse Rings -->
                <div class="absolute w-32 h-32 rounded-full bg-orange-500/20 pulse-ring"></div>
                <div class="absolute w-32 h-32 rounded-full bg-orange-500/10 pulse-ring-2"></div>

                <!-- Icon Container -->
                <div class="success-icon relative w-28 h-28 bg-gradient-to-br from-orange-500 to-orange-600 rounded-full flex items-center justify-center shadow-2xl shadow-orange-500/30">
                    <svg class="w-14 h-14 text-white" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
                        <polyline class="check-mark" points="20 6 9 17 4 12"></polyline>
                    </svg>
                </div>
            </div>

            <!-- Thank You Text -->
            <h1 class="fade-in-up delay-1 text-4xl lg:text-5xl font-semibold tracking-tight mb-4">Thank <span class="text[#0a1b50]">You!</span>
            </h1>

            <p class="fade-in-up delay-2 text-xl text-black mb-4">
                Your message has been successfully sent.
           
            </p>

            <p class="fade-in-up delay-3 text-black max-w-md mx-auto mb-10 leading-relaxed">
                We appreciate you reaching out to Thamaz Global. Our team will review your inquiry and get back to you within 24-48 hours.
           
            </p>



            <!-- Action Buttons -->
            <div class="fade-in-up delay-4 flex flex-col sm:flex-row items-center justify-center gap-4">
                <a href="/" class="btn-primary">
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

