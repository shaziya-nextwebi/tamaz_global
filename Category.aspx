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

                const badge = document.getElementById('cartCount');
                if (badge && count != '0') {
                    badge.innerText = count;
                    badge.style.display = 'inline';
                }

                btn.innerText = 'View Cart';
                btn.style.background = '#162e7d';
                btn.disabled = false;
            })
            .catch(() => {
                btn.innerText = 'Error';
                btn.disabled = false;
            });
    }
</script>
</asp:Content>