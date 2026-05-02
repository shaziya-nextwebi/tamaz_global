<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sitemap.aspx.cs" Inherits="sitemap" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sitemap - Tamazglobal</title>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap" rel="stylesheet">
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Inter', Arial, sans-serif;
            line-height: 1.6;
            color: #333;
            background-color: #f5f5f5;
        }

        /* Top Bar */
        .top-bar {
            background-color: #b91c1c;
            padding: 8px 0;
            text-align: center;
        }

        .top-bar p {
            color: #fff;
            font-size: 13px;
            font-weight: 400;
            letter-spacing: 0.3px;
        }

        /* Header */
        .header {
            background-color: #fff;
            border-bottom: 1px solid #e5e5e5;
            padding: 16px 0;
        }

        .header-inner {
            max-width: 1200px;
            margin: 0 auto;
            padding: 0 20px;
            display: flex;
            align-items: center;
            justify-content: space-between;
        }
        .header-inner img{
               height: 75px;
        }

        .logo {
            font-size: 24px;
            font-weight: 700;
            color: #b91c1c;
            text-decoration: none;
            letter-spacing: -0.5px;
        }

        .logo span {
            color: #333;
        }

        .breadcrumb-nav {
            display: flex;
            align-items: center;
            gap: 8px;
            font-size: 13px;
            color: #888;
        }

        .breadcrumb-nav a {
            color: #666;
            text-decoration: none;
            transition: color 0.2s;
        }

        .breadcrumb-nav a:hover {
            color: #b91c1c;
        }

        .breadcrumb-nav .sep {
            color: #ccc;
            font-size: 11px;
        }

        .breadcrumb-nav .current {
            color: #333;
            font-weight: 500;
        }

        /* Main Container */
        .container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 40px 20px 60px;
        }

        /* Page Title */
        .page-header {
            margin-bottom: 40px;
        }

        .page-header h1 {
            color: #1a1a1a;
            font-size: 28px;
            font-weight: 700;
            margin-bottom: 8px;
            letter-spacing: -0.5px;
        }

        .page-header p {
            color: #888;
            font-size: 14px;
            font-weight: 400;
        }

        /* Section */
        .section {
            margin-bottom: 36px;
        }

        .section-header {
            display: flex;
            align-items: center;
            gap: 10px;
            margin-bottom: 16px;
            padding-bottom: 12px;
            border-bottom: 1px solid #eaeaea;
        }

        .section-header h2 {
            color: #1a1a1a;
            font-size: 16px;
            font-weight: 600;
            letter-spacing: -0.2px;
        }

        .section-header .count {
            background-color: #f0f0f0;
            color: #777;
            font-size: 11px;
            font-weight: 600;
            padding: 2px 8px;
            border-radius: 10px;
        }

        /* Main Page Links */
        .main-links {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
        }

        .main-link {
            display: inline-flex;
            align-items: center;
            gap: 6px;
            padding: 10px 20px;
            background-color: #fff;
            color: #333;
            text-decoration: none;
            border: 1px solid #e0e0e0;
            border-radius: 6px;
            font-size: 13px;
            font-weight: 500;
            transition: all 0.2s ease;
        }

        .main-link:hover {
            border-color: #b91c1c;
            color: #b91c1c;
            background-color: #fef2f2;
        }

        .main-link .arrow {
            font-size: 10px;
            opacity: 0;
            transform: translateX(-4px);
            transition: all 0.2s ease;
        }

        .main-link:hover .arrow {
            opacity: 1;
            transform: translateX(0);
        }

        /* Links Grid */
        .links-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
            gap: 6px;
        }

        .link-item {
            display: flex;
            align-items: center;
            gap: 10px;
            padding: 10px 14px;
            background-color: #fff;
            border: 1px solid transparent;
            border-radius: 6px;
            transition: all 0.2s ease;
        }

        .link-item:hover {
            border-color: #e0e0e0;
            background-color: #fafafa;
        }

        .link-item .dot {
            width: 5px;
            height: 5px;
            border-radius: 50%;
            background-color: #d4d4d4;
            flex-shrink: 0;
            transition: background-color 0.2s;
        }

        .link-item:hover .dot {
            background-color: #b91c1c;
        }

        .link-item a {
            color: #555;
            text-decoration: none;
            font-size: 13px;
            font-weight: 400;
            transition: color 0.2s;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .link-item:hover a {
            color: #b91c1c;
        }

        /* Footer */
        .footer {
            background-color: #1a1a1a;
            padding: 30px 20px;
            text-align: center;
        }

        .footer p {
            color: #888;
            font-size: 13px;
        }

        .footer a {
            color: #b91c1c;
            text-decoration: none;
        }

        .footer a:hover {
            text-decoration: underline;
        }

        /* Responsive */
        @media (max-width: 768px) {
            .header-inner {
                flex-direction: column;
                gap: 10px;
                text-align: center;
            }

            .container {
                padding: 24px 16px 40px;
            }

            .page-header h1 {
                font-size: 22px;
            }

            .links-grid {
                grid-template-columns: 1fr;
            }

            .main-links {
                flex-direction: column;
            }

            .main-link {
                justify-content: center;
            }
        }
    </style>
</head>
<body>

    <!-- Header -->
    <div class="header">
        <div class="header-inner">
            <a href="https://www.tamazglobal.com/" class="logo">
                <img src="https://www.tamazglobal.com/assests/Images/logo.png" alt="LOGO" /></a>
            <div class="breadcrumb-nav">
                <a href="https://www.tamazglobal.com/">Home</a>
                <span class="sep">›</span>
                <span class="current">Sitemap</span>
            </div>
        </div>
    </div>

    <!-- Main Content -->
    <div class="container">

        <div class="page-header">
            <h1>Sitemap</h1>
            <p>A complete overview of all pages on TamazGlobal</p>
        </div>

        <!-- Main Pages -->
        <div class="section">
            <div class="section-header">
                <h2>Main Pages</h2>
            </div>
            <div class="main-links">
                <a href="https://www.tamazglobal.com/" class="main-link">Home <span class="arrow">→</span></a>
                <a href="https://www.tamazglobal.com/about.aspx" class="main-link">About Us <span class="arrow">→</span></a>
                <a href="https://www.tamazglobal.com/blogs" class="main-link">Blogs <span class="arrow">→</span></a>
                <a href="https://www.tamazglobal.com/contact-us.aspx" class="main-link">Contact Us <span class="arrow">→</span></a>
            </div>
        </div>

        <!-- Policies -->
        <div class="section">
            <div class="section-header">
                <h2>Policies & Information</h2>
            </div>
            <div class="main-links">
                <a href="https://www.tamazglobal.com/return-policy.aspx" class="main-link">Return Policy <span class="arrow">→</span></a>
                <a href="https://www.tamazglobal.com/shipping-policy.aspx" class="main-link">Shipping & Delivery <span class="arrow">→</span></a>
                <a href="https://www.tamazglobal.com/privacy-policy.aspx" class="main-link">Privacy Policy <span class="arrow">→</span></a>
                <a href="https://www.tamazglobal.com/cookies-policy.aspx" class="main-link">Cookie Policy <span class="arrow">→</span></a>
                <a href="https://www.tamazglobal.com/security.aspx" class="main-link">Security <span class="arrow">→</span></a>
                <a href="https://www.tamazglobal.com/tos.aspx" class="main-link">Terms & Conditions <span class="arrow">→</span></a>
            </div>
        </div>

        <!-- Other Pages -->
        <div class="section">
            <div class="section-header">
                <h2>Other Pages</h2>
            </div>
            <div class="main-links">
                <a href="https://www.tamazglobal.com/404.aspx" class="main-link">404 Page <span class="arrow">→</span></a>
                <a href="https://www.tamazglobal.com/thank-you.aspx" class="main-link">Thank You <span class="arrow">→</span></a>
            </div>
        </div>

        <!-- Categories -->
        <div class="section">
            <div class="section-header">
                <h2>Categories</h2>
            </div>
            <div class="links-grid">
                <%=strCategories%>
            </div>
        </div>

        <!-- Products -->
        <div class="section">
            <div class="section-header">
                <h2>Products</h2>
            </div>
            <div class="links-grid">
                <%=strProducts %>
            </div>
        </div>

        <!-- Top Brands -->
        <div class="section">
            <div class="section-header">
                <h2>Top Brands</h2>
            </div>
            <div class="links-grid">
                <%=strTopBrands %>
            </div>
        </div>

        <!-- Blogs -->
        <div class="section">
            <div class="section-header">
                <h2>Our Blogs</h2>
            </div>
            <div class="links-grid">
                <%=strBlogs %>
            </div>
        </div>

    </div>

    <!-- Footer -->
    <div class="footer">
        <p>&copy; 2026 <a href="https://www.tamazglobal.com/">TamazGlobal</a>. All rights reserved.</p>
    </div>

</body>
</html>