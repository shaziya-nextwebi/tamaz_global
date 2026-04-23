<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="terms-and-conditions.aspx.cs" Inherits="terms_and_conditions" %>

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
        /* Enhanced text shadow for even better readability on the banner */
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

            <!-- Breadcrumb -->
            <nav class="text-sm text-gray-500 flex flex-wrap justify-center items-center gap-2 mb-4">
                <a href="Default.aspx" class="hover:text-primary transition whitespace-nowrap">Home</a>
                <span class="text-gray-400">></span>
                <span class="text-primary font-medium whitespace-nowrap">Terms & Conditions</span>
            </nav>

            <!-- Title -->
            <h1 class="text-4xl md:text-5xl font-bold text-primary mb-4 tracking-tight">
                Terms & Conditions
            </h1>

            <!-- Paragraph -->
            <p class="text-gray-500 text-xs md:text-sm max-w-xl mx-auto leading-relaxed">
                Understanding your rights and obligations when using our services.
            </p>

        </div>
    </div>
</section>

<!-- CONTENT WITHOUT BOX - New Terms and Conditions content -->
<section class="py-16">
    <div class="max-w-5xl mx-auto px-4 text-gray-700 leading-relaxed space-y-6">

        <p><strong>TamazGlobal.com ('Website')</strong> is an online service operated and managed by TamazGlobal Trading Company. In using the TamazGlobal service, you are deemed to have accepted the terms and conditions of the agreement listed below or as may be revised from time to time ('User Agreement'), which is, for an indefinite period and you understand and agree that you are bound by such terms till the time you access this Website..</p>

        <p>If you have any queries about the terms and conditions of this User Agreement or have any comments or complaints on or about the Website, please email us at <a href="mailto:sales@tamazglobal.com">sales@tamazglobal.com</a>. We reserve the right to change the terms and conditions of this User Agreement from time to time without any obligation to inform you and it is your responsibility to look through them as often as possible.</p>

        <h2 class="content-h2">Ownership of rights:</h2>
        <p>Any use of this Website or its contents, including copying or storing it or them in whole or part, other than for your own personal, non-commercial use is prohibited without the explicit permission of TamazGlobal.</p>
        <p>All information displayed, transmitted or carried on the Website is protected by copyright and other intellectual property laws. Copyrights and other intellectual property in respect of the some of the content on the Website may be owned by the third parties.</p>
        <p>This site is designed, updated and maintained by Tamaz or its licensors. You shall not modify, publish, transmit, transfer, sell, reproduce, create derivative work from, distribute, repost, perform, display or in any way commercially exploit any of the content available on the Website.</p>

        <h2 class="content-h2">Accuracy of content and invitation to offer:</h2>
        <p>We have taken all care and precaution to try and provide accurate data and information. In the preparation of the content of this website, in particular to ensure that prices quoted are correct at time of publishing and all products have been fairly described.</p>
        <p>All prices are displayed inclusive of GST. Services however are listed exclusive of service tax as rules for service tax vary with different services. Packaging may vary from that shown.</p>
        <p>The weights, dimensions and capacities given are approximate only.</p>
        <p>We have made every effort to display as accurately as possible the colours of our products that appear on the Website. However, as the actual colours you see will depend on your monitor, we cannot guarantee that your monitor's display of any colour will accurately reflect the colour of the product on delivery. All products/services and information displayed on the Website constitute an invitation to offer.</p>
        <p>Your order for purchase constitutes your offer which shall be subject to the terms and conditions of this User Agreement. We reserve the right to accept or reject your offer in part or in full.</p>
        <p>Our acceptance of your order will take place upon dispatch of the product(s) ordered. Dispatch of all the product(s) ordered, may or may not happen at the same time, in such a scenario that portion of the order which has been dispatched will be deemed to have been accepted by us and the balance would continue to be on offer to us and we reserve the right to accept or reject such balance orders.</p>

        <h2 class="content-h2">Usage Restrictions:</h2>
        <p>You shall not use the Website for any of the following purposes:</p>
        <ul class="content-ul">
            <li>Disseminating any unlawful, harassing, libelous, abusive, threatening, harmful, vulgar, obscene, or otherwise objectionable material.</li>
            <li>Transmitting material that encourages conduct that constitutes a criminal offence, results in civil liability or otherwise breaches any relevant laws, regulations or code of practice.</li>
            <li>Gaining unauthorised access to other computer / network systems.</li>
            <li>Interfering with any other person's use or enjoyment of the Website.</li>
            <li>Breaching any applicable laws.</li>
            <li>Interfering or disrupting networks or websites connected to the Website.</li>
            <li>Making, transmitting or storing electronic copies of materials protected by copyright without the permission of the owner.</li>
        </ul>
        <p>You are not permitted to host, display, upload, modify, publish, transmit, update or share any information on the Website that</p>
        <ul class="content-ul">
            <li>belongs to another person and to which you do not have any right to;</li>
            <li>is grossly harmful, harassing, blasphemous, defamatory, obscene, pornographic, paedophilic, libellous, invasive of another's privacy, hateful, or racially, ethnically objectionable, disparaging, relating or encouraging money laundering or gambling, or otherwise unlawful in any manner whatever;</li>
            <li>harm minors in any way;</li>
            <li>infringes any patent, trademark, copyright or other proprietary rights;</li>
            <li>violates any law for the time being in force;</li>
            <li>deceives or misleads the addressee about the origin of such messages or communicates any information which is grossly offensive or menacing in nature;</li>
            <li>impersonate another person;</li>
            <li>contains software viruses or any other computer code, files or programs designed to interrupt, destroy or limit the functionality of any computer resource including the Website;</li>
            <li>threatens the unity, integrity, defence, security or sovereignty of India, friendly relations with foreign states, or public order or causes incitement to the commission of any cognisable offence or prevents investigation of any offence or is insulting any other nation.</li>
        </ul>
        <p>You are also prohibited from:</p>
        <ul class="content-ul">
            <li>violating or attempting to violate the integrity or security of the Website or its content;</li>
            <li>transmitting any information (including job posts, messages and hyperlinks) on or through the Website that is disruptive or competitive to the provision of services by us;</li>
            <li>intentionally submitting on the Website any incomplete, false or inaccurate information;</li>
            <li>making any unsolicited communications to other users of the Website;</li>
            <li>using any engine, software, tool, agent or other device or mechanism (such as spiders, robots, avatars or intelligent agents) to navigate or search the Website;</li>
            <li>attempting to decipher, decompile, disassemble or reverse engineer any part of the Website;</li>
            <li>copying or duplicating in any manner any of the content on the Website or other information available from the Website;</li>
            <li>framing or hotlinking or deeplinking any content on the Website.</li>
        </ul>

        <h2 class="content-h2">Quantity Restrictions:</h2>
        <p>We reserve the right, at our sole discretion, to limit the quantity of items purchased per person, per household or per order. These restrictions may be applicable to orders placed by the same account, the same credit / debit card, and also to orders that use the same billing and/or shipping address. We will provide notification to the customer should such limits be applied. We also reserve the right, at our sole discretion, to prohibit sales to any one as we may deem fit.</p>

        <h2 class="content-h2">Pricing Information:</h2>
        <p>While we strive to provide accurate product and pricing information, pricing or typographical errors may occur. We cannot confirm the price of a product until after you order. In the event that a product is listed at an incorrect price or with incorrect information due to an error in pricing or product information, we shall have the right, at our sole discretion, to refuse or cancel any orders placed for that product, unless the product has already been dispatched. In the event that an item is mis-priced, we may, at our sole discretion, either contact you for instructions or cancel your order and notify you of such cancellation. Unless the product ordered by you has been dispatched, your offer will not be deemed accepted and we will have the right to modify the price of the product and contact you for further instructions using the e-mail address or the contact number provided by you during the time of registration, or cancel the order and notify you of such cancellation. In the event we accept your order the same shall be debited to your credit / debit card account and duly notified to you by email or the contact number, as the case may be, that the payment has been processed. The payment may be processed prior to dispatch of the product that you have ordered. If we have to cancel the order after we have processed the payment, the said amount will be reversed back to your credit / debit card account. We strive to provide you with best value, however prices and availability are subject to change without notice.</p>
        <p>Our promotional offers/discounts are not sitewide and are limited to selected categories. Coupon codes may not be applicable on categories like diapers, baby food etc. or such other product or service as may be determined by us in our sole discretion.</p>

        <h2 class="content-h2">Indemnity:</h2>
        <p>We disclaim all warranties or conditions, whether expressed or implied, (including without limitation implied, warranties or conditions of information and context). We shall not be liable to any person for any loss or damage which may arise from the use of any of the information contained in any of the materials on this Website. This User Agreement and any contractual obligation between us and you will be governed by the laws of India, subject to the exclusive jurisdiction of Courts in New Delhi. All disputes will be subject to arbitration in New Delhi in English by a single arbitrator appointed by us under the Arbitration and Conciliation Act, 1996. Each party to arbitration shall bear its own cost.You agree to defend, indemnify and hold harmless Tamaz Global, its employees, directors, officers, agents and their successors and assigns from and against any and all claims, liabilities, damages, losses, costs and expenses, including attorney's fees, caused by or arising out of claims based upon your actions or inactions, which may result in any loss or liability to TamazGlobal or any third party including but not limited to breach of any warranties, representations or undertakings or in relation to the non-fulfillment of any of your obligations under this User Agreement or arising out of your violation of any applicable laws, regulations including but not limited to intellectual property rights, payment of statutory dues and taxes, claim of libel, defamation, violation of rights of privacy or publicity, loss of service by other subscribers and infringement of intellectual property or other rights. This clause shall survive the expiry or termination of this User Agreement</p>

        <h2 class="content-h2">Website Security:</h2>
        <p>You are prohibited from violating or attempting to violate the security of the Website, including, without limitation:</p>
        <ul class="content-ul">
            <li>Accessing data not intended for you or logging onto a server or an account which you are not authorized to access;</li>
            <li>Attempting to probe, scan or test the vulnerability of a system or network or to breach security or authentication measures without proper authorization;</li>
            <li>Attempting to interfere with service to any other user, host or network, including, without limitation, via means of submitting a virus to the Website, overloading, 'flooding,' 'spamming', 'mail bombing' or 'crashing;</li>
            <li>Sending unsolicited email, including promotions and/or advertising of products or services; or</li>
            <li>Forging any TCP/IP packet header or any part of the header information in any email or newsgroup posting. Violations of system or network security may result in civil or criminal liability. We will investigate occurrences that may involve such violations and may involve, and cooperate with, law enforcement authorities in prosecuting users who are involved in such violations. You agree not to use any device, software or routine to interfere or attempt to interfere with the proper working of this Website or any activity being conducted on this Website. You agree, further, not to use or attempt to use any engine, software, tool, agent or other device or mechanism (including without limitation browsers, spiders, robots, avatars or intelligent agents) to navigate or search this Website other than the search engine and search agents available from TamazGlobal on this Website and other than generally available third party web browsers (e.g., Google Chrome, Firefox, Microsoft Internet Explorer).</li>
        </ul>

        <h2 class="content-h2">Entire Agreement:</h2>
        <p>If any part of this User Agreement is determined to be invalid or unenforceable pursuant to applicable law including, but not limited to, the warranty disclaimers and liability limitations set forth above, then the invalid or unenforceable provision will be deemed to be superseded by a valid, enforceable provision that most closely matches the intent of the original provision and the remainder of the User Agreement shall continue in effect. Unless otherwise specified herein, this User Agreement constitutes the entire agreement between you and us with respect to the Websites/services and it supersedes all prior or contemporaneous communications and proposals, whether electronic, oral or written, between you and us with respect to the Websites/services. Our failure to act with respect to a breach by you or others does not waive its right to act with respect to subsequent or similar breaches.</p>

        <h3 class="content-h3">For any queries :<a href="mailto:sales@tamazglobal.com">sales@tamazglobal.com</a></h3>

    </div>
</section>
</asp:Content>