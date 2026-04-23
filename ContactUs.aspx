<%@ Page Title="Contact Us - TAMAZ Global" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
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

        .field-error {
            color: #dc2626;
            font-size: 12px;
            margin-top: 4px;
            display: block;
        }
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
                <p class="text-lg text-text-muted max-w-2xl mx-auto banner-aub-text">
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
                                <p class="text-slate-600">
                                    Tamaz Global Trading Co.<br />
                                    No 40, Unit no 104, 1st Floor, Promenade Road,<br />
                                    Frazer Town Bangalore - 560005
                                </p>
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
                    <div class="bg-white rounded-xl shadow-lg border border-slate-100 contact-form-wrap p-8  relative overflow-hidden">
                        <h2 class="text-2xl md:text-3xl font-bold text-[#0F172A] mb-4">Send Us a Message</h2>

                        <!-- Status Label -->
                        <asp:Label ID="lblStatus" runat="server" Visible="false" CssClass="block mb-4 px-4 py-3 rounded-lg text-sm font-medium"></asp:Label>

                        <div class="space-y-4">
                            <div class="grid sm:grid-cols-2 gap-4">
                                <!-- Name -->
                                <div>
                                    <label class="block text-sm font-medium text-slate-700 mb-1">Name <span class="text-red-500">*</span></label>
                                    <asp:TextBox ID="txtName" runat="server" placeholder="Your Name"
                                        CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-red-500 bg-slate-50 text-sm" />
                                    <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                        ControlToValidate="txtName"
                                        ErrorMessage="Name is required."
                                        CssClass="field-error"
                                        ValidationGroup="ContactForm"
                                        Display="Dynamic" />
                                </div>

                                <!-- City -->
                                <div>
                                    <label class="block text-sm font-medium text-slate-700 mb-1">City</label>
                                    <asp:TextBox ID="txtCity" runat="server" placeholder="Your City"
                                        CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-red-500 bg-slate-50 text-sm" />
                                </div>
                            </div>

                            <div class="grid sm:grid-cols-2 gap-4">
                                <!-- Phone -->
                                <div>
                                    <label class="block text-sm font-medium text-slate-700 mb-1">Phone <span class="text-red-500">*</span></label>
                                    <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" placeholder="Phone Number" maxlength="15" oninput="this.value = this.value.replace(/[^0-9]/g, '')"
                                        CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-red-500 bg-slate-50 text-sm" />
                                    <asp:RequiredFieldValidator ID="rfvPhone" runat="server"
                                        ControlToValidate="txtPhone"
                                        ErrorMessage="Phone number is required."
                                        CssClass="field-error"
                                        ValidationGroup="ContactForm"
                                        Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="revPhone" runat="server"
                                        ControlToValidate="txtPhone"
                                        ValidationExpression="^[0-9\+\-\s\(\)]{7,15}$"
                                        ErrorMessage="Enter a valid phone number."
                                        CssClass="field-error"
                                        ValidationGroup="ContactForm"
                                        Display="Dynamic" />
                                </div>

                                <!-- Email -->
                                <div>
                                    <label class="block text-sm font-medium text-slate-700 mb-1">Email <span class="text-red-500">*</span></label>
                                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Email Address"
                                        CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-red-500 bg-slate-50 text-sm" />
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                        ControlToValidate="txtEmail"
                                        ErrorMessage="Email is required."
                                        CssClass="field-error"
                                        ValidationGroup="ContactForm"
                                        Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                        ControlToValidate="txtEmail"
                                        ValidationExpression="^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$"
                                        ErrorMessage="Enter a valid email address."
                                        CssClass="field-error"
                                        ValidationGroup="ContactForm"
                                        Display="Dynamic" />
                                </div>
                            </div>

                            <!-- Message -->
                            <div>
                                <label class="block text-sm font-medium text-slate-700 mb-1">Message</label>
                                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="4"
                                    placeholder="Write your message here..."
                                    CssClass="w-full px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-red-500 bg-slate-50 text-sm resize-none" />
                            </div>

                            <!-- Captcha / Verification -->
                            <div>
                                <label class="block text-sm font-medium text-slate-700 mb-1">Verification Code <span class="text-red-500">*</span></label>
                                <div class="flex gap-3 items-center ">
                                    <asp:TextBox ID="txtCaptcha" runat="server" placeholder="Enter code"
                                        CssClass="w-32 px-4 py-2.5 border border-slate-200 rounded-lg focus:outline-none focus:border-red-500 bg-slate-50 text-sm" />
                                    <div class="bg-slate-200 px-4 py-2 rounded font-mono tracking-widest text-slate-600 select-none text-sm">
                                        <asp:Image ID="Image1" runat="server" />
                                    </div>
                                    <asp:LinkButton ID="lnkRefresh" runat="server" OnClick="lnkRefresh_Click"
                                        CausesValidation="false"
                                        CssClass="text-slate-500 hover:text-red-700 transition-colors"
                                        ToolTip="Refresh Captcha">
                                        <span class="iconify w-5 h-5" data-icon="lucide:refresh-cw"></span>
                                    </asp:LinkButton>
                                </div>
                                <asp:RequiredFieldValidator ID="rfvCaptcha" runat="server"
                                    ControlToValidate="txtCaptcha"
                                    ErrorMessage="Please enter the verification code."
                                    CssClass="field-error"
                                    ValidationGroup="ContactForm"
                                    Display="Dynamic" />
                            </div>

                            <!-- Submit Button -->
                            <asp:Button ID="btnSubmit" runat="server" Text="Send Message"
                                CssClass="w-full btn-primary flex items-center justify-center gap-2 mt-6"
                                OnClick="btnSubmit_Click"
                                ValidationGroup="ContactForm" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>



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

        function resetButton() {
            var btn = document.getElementById('<%= btnSubmit.ClientID %>');
            if (btn) { btn.disabled = false; btn.value = 'Send Message'; }
        }
    </script>
</asp:Content>
