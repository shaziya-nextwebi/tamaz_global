<%@ Page Title="Contact Us - TAMAZ Global" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
        .reveal { opacity: 0; transform: translateY(20px); transition: all 0.8s cubic-bezier(0.16, 1, 0.3, 1); }
        .reveal.active { opacity: 1; transform: translateY(0); }
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Hero Section -->
    <section class="relative bg-bg-light py-20 overflow-hidden">
        <div class="absolute inset-0">
            <img src="assests/Images/conatct-us-banner.png" alt="Background" class="w-full h-full object-cover" />
        </div>
        <div class="max-w-7xl mx-auto px-4 relative z-10 text-center">
            <div class="reveal">
                <div class="flex items-center justify-center gap-2 text-sm text-text-muted mb-6">
                    <a href="Default.aspx" class="hover:text-accent transition-colors">Home</a>
                    <span class="iconify w-4 h-4" data-icon="lucide:chevron-right"></span>
                    <span class="text-primary font-medium">Contact Us</span>
                </div>
                <h1 class="text-4xl md:text-5xl font-bold text-black mb-4 tracking-tight">Contact Us</h1>
                <p class="text-lg text-text-muted max-w-2xl mx-auto">
                    We are here to help. Reach out to us for inquiries, support, or partnership opportunities.
                </p>
            </div>
        </div>
    </section>

    <!-- Main Content -->
    <section class="section-padding">
        <div class="max-w-7xl mx-auto px-4">
            <div class="grid lg:grid-cols-2 gap-12">

                <!-- Left Side: Contact Info -->
                <div class="reveal">
                    <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] mb-2">Get In Touch</h2>
                    <p class="text-muted-500 mb-8">Fill up the form and our Team will get back to you within 24 hours.</p>

                    <div class="space-y-6 mb-10">
                        <!-- Phone -->
                        <div class="flex items-start gap-4 p-5 bg-white rounded-xl border border-slate-100 shadow-sm hover:shadow-md transition-shadow">
                            <div class="w-12 h-12 rounded-lg bg-red-50 flex items-center justify-center flex-shrink-0">
                                <span class="iconify w-6 h-6 text-red-700" data-icon="lucide:phone"></span>
                            </div>
                            <div>
                                <h4 class="font-bold text-slate-900 mb-1">Phone</h4>
                                <a href="tel:+919988227622" class="text-slate-600 hover:text-red-700 transition-colors block">+91 99882 27622</a>
                                <a href="tel:+919900746748" class="text-slate-600 hover:text-red-700 transition-colors block text-sm">+91 990074 6748 (Wholesale)</a>
                            </div>
                        </div>

                        <!-- Email -->
                        <div class="flex items-start gap-4 p-5 bg-white rounded-xl border border-slate-100 shadow-sm hover:shadow-md transition-shadow">
                            <div class="w-12 h-12 rounded-lg bg-blue-50 flex items-center justify-center flex-shrink-0">
                                <span class="iconify w-6 h-6 text-blue-900" data-icon="lucide:mail"></span>
                            </div>
                            <div>
                                <h4 class="font-bold text-slate-900 mb-1">Email</h4>
                                <a href="mailto:sales@tamazglobal.com" class="text-slate-600 hover:text-red-700 transition-colors block">sales@tamazglobal.com</a>
                            </div>
                        </div>

                        <!-- Location -->
                        <div class="flex items-start gap-4 p-5 bg-white rounded-xl border border-slate-100 shadow-sm hover:shadow-md transition-shadow">
                            <div class="w-12 h-12 rounded-lg bg-slate-100 flex items-center justify-center flex-shrink-0">
                                <span class="iconify w-6 h-6 text-slate-700" data-icon="lucide:map-pin"></span>
                            </div>
                            <div>
                                <h4 class="font-bold text-slate-900 mb-1">Address</h4>
                                <p class="text-slate-600">Tamaz Global Trading Co.<br />
                                    No 40, Unit no 104, 1st Floor, Promenade Road,<br />
                                    Frazer Town Bangalore - 560005</p>
                            </div>
                        </div>
                    </div>

                    <!-- Social Media -->
                    <div>
                        <h4 class="font-bold text-slate-900 mb-4">Follow Us</h4>
                        <div class="flex gap-3">
                            <a href="#" class="w-10 h-10 rounded-full bg-slate-100 flex items-center justify-center text-slate-600 hover:bg-[#0a1b50] hover:text-white transition-colors">
                                <span class="iconify w-5 h-5" data-icon="lucide:facebook"></span>
                            </a>
                            <a href="#" class="w-10 h-10 rounded-full bg-slate-100 flex items-center justify-center text-slate-600 hover:bg-[#0a1b50] hover:text-white transition-colors">
                                <span class="iconify w-5 h-5" data-icon="lucide:instagram"></span>
                            </a>
                            <a href="#" class="w-10 h-10 rounded-full bg-slate-100 flex items-center justify-center text-slate-600 hover:bg-[#0a1b50] hover:text-white transition-colors">
                                <span class="iconify w-5 h-5" data-icon="lucide:twitter"></span>
                            </a>
                            <a href="#" class="w-10 h-10 rounded-full bg-slate-100 flex items-center justify-center text-slate-600 hover:bg-[#0a1b50] hover:text-white transition-colors">
                                <span class="iconify w-5 h-5" data-icon="lucide:linkedin"></span>
                            </a>
                        </div>
                    </div>
                </div>

                <!-- Right Side: Form -->
                <div class="reveal">
                    <div class="bg-white br-12 shadow-lg border border-slate-100 p-8 relative overflow-hidden">
                        <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] mb-4">Send Us a Message</h2>

                        <div class="space-y-4">
                            <div class="grid sm:grid-cols-2 gap-4">
                                <div>
                                    <label class="block text-sm font-medium text-slate-700 mb-1">Name *</label>
                                    <asp:TextBox ID="txtName" runat="server" placeholder="Your Name" CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-red-500 bg-slate-50 text-sm" />
                                </div>
                                <div>
                                    <label class="block text-sm font-medium text-slate-700 mb-1">City</label>
                                    <asp:TextBox ID="txtCity" runat="server" placeholder="Your City" CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-red-500 bg-slate-50 text-sm" />
                                </div>
                            </div>

                            <div class="grid sm:grid-cols-2 gap-4">
                                <div>
                                    <label class="block text-sm font-medium text-slate-700 mb-1">Phone *</label>
                                    <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" placeholder="Phone Number" CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-red-500 bg-slate-50 text-sm" />
                                </div>
                                <div>
                                    <label class="block text-sm font-medium text-slate-700 mb-1">Email *</label>
                                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Email Address" CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-red-500 bg-slate-50 text-sm" />
                                </div>
                            </div>

                            <div>
                                <label class="block text-sm font-medium text-slate-700 mb-1">Message</label>
                                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="4" placeholder="Write your message here..." CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-red-500 bg-slate-50 text-sm resize-none" />
                            </div>

                            <!-- Verification Code -->
                            <div>
                                <label class="block text-sm font-medium text-slate-700 mb-1">Verification Code *</label>
                                <div class="flex gap-3 items-center">
                                    <asp:TextBox ID="txtVerify" runat="server" placeholder="Enter code" CssClass="w-32 px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-red-500 bg-slate-50 text-sm" />
                                    <div class="bg-slate-200 px-4 py-2 rounded font-mono tracking-widest text-slate-600 select-none text-sm">
                                        8K2B9
                                    </div>
                                    <button type="button" class="text-slate-500 hover:text-red-700 transition-colors">
                                        <span class="iconify w-5 h-5" data-icon="lucide:refresh-cw"></span>
                                    </button>
                                </div>
                            </div>

                            <asp:Button ID="btnSubmit" runat="server" Text="Send Message" CssClass="w-full btn-primary flex items-center justify-center gap-2 mt-6" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- WhatsApp Floating Button -->
    <a href="https://wa.me/92300123456" target="_blank" class="fixed bottom-6 right-6 w-14 h-14 bg-green-500 rounded-full flex items-center justify-center shadow-2xl shadow-green-500/30 hover:scale-110 transition-transform z-50">
        <span class="iconify w-7 h-7 text-white" data-icon="logos:whatsapp-icon"></span>
    </a>

</asp:Content>

<asp:Content ID="ScriptsContent" ContentPlaceHolderID="scripts" runat="server">
    <script src="https://code.iconify.design/3/3.1.0/iconify.min.js"></script>
    <script>
        const reveals = document.querySelectorAll('.reveal');
        const revealOnScroll = () => {
            reveals.forEach(el => {
                if (el.getBoundingClientRect().top < window.innerHeight - 50) {
                    el.classList.add('active');
                }
            });
        };
        window.addEventListener('scroll', revealOnScroll);
        revealOnScroll();
    </script>
</asp:Content>
