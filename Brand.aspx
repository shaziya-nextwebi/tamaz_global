<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Brand.aspx.cs" Inherits="BrandPage" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
     <link rel="canonical" href="https://www.tamazglobal.com/Brand/<%=strBrandUrl%>" hreflang="en" />
    <script type="application/ld+json">
         {
             "@context": "https://www.schema.org/",
             "@type": "Product",
             "name": "<%=strPageTitle %>",
             "description": "<%=strMetaDesc %>",
             "image": "https://www.tamazglobal.com/assests/Images/logo.png",
             "brand": {
                 "@type": "Thing",
                 "name": "tamazglobal"
             },
             "aggregateRating": {
                 "@type": "AggregateRating",
                 "ratingValue": "4.6",
                 "ratingCount": "742"
             } 
         }
    </script>
    <title><%=strPageTitle %></title>
    <meta name="description" content="<%=strMetaDesc %>" />
    <meta name="keywords" content="<%=strMetaKeys %>" />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumb -->
    <section class="py-1 md:py:3 bg-[#f8fafc] border-b border-gray-200">
        <div class="max-w-7xl mx-auto px-4">
            <nav class="text-sm text-[#64748B] breadcrumb">
                <%=strBreadcrumb %>
            </nav>
        </div>
    </section>
    <!-- Main Listing -->
    <section class="py-5 bg-white category-listing-section">
        <div class="max-w-7xl mx-auto px-4">
            <div class="mb-8">
                <h1 class="text-3xl font-bold text-[#0F172A]"><%=strBrandName %></h1>
                <p class="text-[#000] mt-2"><%=strShortDesc %></p>
            </div>
            <!-- Product Grid -->
            <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-3 md:gap-6 gap-6 mb-6 md:mb-12 products-grid">
                <%=strProducts %>
            </div>

            <% if (!string.IsNullOrEmpty(strPagination))
                { %>
            <nav class="flex justify-center items-center gap-2 mb-16">
                <%=strPagination %>
            </nav>
            <% } %>
        </div>
    </section>
    <!-- Full Description (SEO Content) -->
    <% if (strFullDesc != "")
        { %>
    <section class="section-padding border-t border-gray-200 pt-12">
        <div class="bg-transparent">
            <div class="editor-content max-w-7xl mx-auto px-4">
                <%=strFullDesc %>
            </div>
        </div>
    </section>
    <% } %>
</asp:Content>
<asp:Content ID="ScriptsContent" ContentPlaceHolderID="scripts" runat="server">
    <script>
        function addToCart(productId, btn) {

            if (btn.innerText.trim() === 'View Cart') {
                window.location.href = '<%= ResolveUrl("~/cart.aspx") %>';
            return;
        }

        btn.disabled = true;
        btn.innerText = 'Adding...';

        fetch('<%= ResolveUrl("~/Brand.aspx/AddToCart") %>', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ productId: productId })
            })
                .then(res => res.json())
                .then(data => {
                    const count = data.d;

                    if (count == -1) {
                        btn.innerText = 'Error';
                        btn.disabled = false;
                        return;
                    }

                    const badge = document.getElementById('cartCount');
                    if (badge && count != '0') {
                        badge.innerText = count;
                        badge.style.display = 'inline';
                    }

                    btn.innerText = 'View Cart';
                    btn.style.background = '#e51c4c';
                    btn.disabled = false;
                })
                .catch(() => {
                    btn.innerText = 'Error';
                    btn.disabled = false;
                });
        }
    </script>
</asp:Content>
