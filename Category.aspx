<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="Category.aspx.cs" Inherits="CategoryPage" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <title><%=strPageTitle %></title>
    <meta name="description" content="<%=strMetaDesc %>" />
    <meta name="keywords"    content="<%=strMetaKeys %>" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Breadcrumb -->
    <section class="py-3 bg-[#f8fafc] border-b border-gray-200">
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
                <h1 class="text-3xl font-bold text-[#0F172A]"><%=strCategoryName %></h1>
                <p class="text-[#64748B] mt-2"><%=strShortDesc %></p>
            </div>

            <!-- Product Grid -->
            <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-3 md:gap-6 mb-12">
                <%=strProducts %>
            </div>

            <!-- Pagination -->
            <nav class="flex justify-center items-center gap-2 mb-16">
                <%=strPagination %>
            </nav>
        </div>
    </section>

    <!-- Full Description (SEO Content) -->
    <% if (strFullDesc != "") { %>
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
            window.location.href = '<%= ResolveUrl("~/Cart.aspx") %>';
        return;
    }

    btn.disabled = true;
    btn.innerText = 'Adding...';

    fetch('<%= ResolveUrl("~/Category.aspx/AddToCart") %>', {
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

            btn.innerText = 'View Cart';
            btn.style.background = '#162e7d';
            btn.disabled = false;

            var newCount = parseInt(count) || 0;
            if (newCount <= 0) return;

            // ✅ Desktop badge
            var countEl = document.getElementById('cartCount');
            if (countEl) {
                countEl.innerText = newCount;
                countEl.style.display = 'flex';
            } else {
                var cartIcon = document.querySelector('.relative.p-3');
                if (cartIcon) {
                    var span = document.createElement('span');
                    span.id = 'cartCount';
                    span.className = 'absolute -top-1 -right-1 w-5 h-5 added-cart-count text-white text-xs rounded-full flex items-center justify-center';
                    span.style.textAlign = 'center';
                    span.innerText = newCount;
                    cartIcon.appendChild(span);
                }
            }

            // ✅ Mobile badge
            var countElMobile = document.getElementById('cartCountMobile');
            if (countElMobile) {
                countElMobile.innerText = newCount;
                countElMobile.style.display = 'flex';
            } else {
                var mobileCartIcon = document.querySelector('.mobile-cart-icon');
                if (mobileCartIcon) {
                    var spanM = document.createElement('span');
                    spanM.id = 'cartCountMobile';
                    spanM.className = 'absolute -top-1 -right-1 w-5 h-5 added-cart-count text-white text-xs rounded-full flex items-center justify-center';
                    spanM.innerText = newCount;
                    mobileCartIcon.appendChild(spanM);
                }
            }
        })
        .catch(() => {
            btn.innerText = 'Error';
            btn.disabled = false;
        });
}
</script>
</asp:Content>