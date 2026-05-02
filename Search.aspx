<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Search Results</title>
    <style>
        .search-title {
            font-size: 22px;
            margin-bottom: 20px;
        }
        .product-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(220px, 1fr));
            gap: 20px;
        }
        .product-card {
            border: 1px solid #eee;
            padding: 12px;
            border-radius: 10px;
            transition: 0.2s;
        }
        .product-card:hover {
            box-shadow: 0 6px 18px rgba(0,0,0,0.1);
        }
        .product-img {
            width: 100%;
            height: 180px;
            object-fit: cover;
        }
        .product-name {
            font-size: 14px;
            font-weight: 600;
            margin-top: 10px;
        }
        .no-result {
            text-align: center;
            margin-top: 50px;
            color: gray;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">

    <div style="max-width:1200px;margin:auto;padding:20px;">

        <asp:Literal ID="litTitle" runat="server"></asp:Literal>

        <div class="product-grid">
            <asp:Repeater ID="rptProducts" runat="server">
                <ItemTemplate>
                    <div class="product-card">
                        <a href='<%# Eval("Url") %>'>
                            <img src='<%# Eval("Image") %>' class="product-img" />
                            <div class="product-name"><%# Eval("Name") %></div>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <asp:Literal ID="litNoResult" runat="server"></asp:Literal>

    </div>

</form>
</body>
</html>