<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="Admin_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        :root {
            --primary-dark: #162e7d;
            --secondary-dark: #0d1d50;
        }

        .card-animate {
            transition: all 0.3s ease-in-out;
            border: 1px solid rgba(22, 46, 125, 0.1);
            border-radius: 15px;
            overflow: hidden;
            background: #ffffff;
        }

        .card-animate:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 20px rgba(13, 29, 80, 0.15) !important;
        }

        .avatar-title {
            width: 48px;
            height: 48px;
            display: flex;
            align-items: center;
            justify-content: center;
            border-radius: 12px !important;
            background-color: #eef2ff !important;
            color: var(--primary-dark) !important;
        }

        .view-link {
            font-size: 0.85rem;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            color: var(--primary-dark);
            text-decoration: none !important;
        }

        .view-link:hover {
            color: var(--secondary-dark);
        }

        .card-products:hover    { border-bottom: 4px solid var(--primary-dark); }
        .card-categories:hover  { border-bottom: 4px solid var(--secondary-dark); }
        .card-brands:hover      { border-bottom: 4px solid var(--primary-dark); }
        .card-blogs:hover       { border-bottom: 4px solid var(--secondary-dark); }
        .card-cartenq:hover     { border-bottom: 4px solid var(--primary-dark); }
        .card-contact:hover     { border-bottom: 4px solid var(--secondary-dark); }
        .card-productenq:hover  { border-bottom: 4px solid var(--primary-dark); }

        .counter-value {
            font-size: 28px;
            display: block;
            color: var(--secondary-dark);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">

            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0" style="color: var(--secondary-dark);">Dashboard</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Dashboards</a></li>
                                <li class="breadcrumb-item active">Dashboard</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row mb-4">
                <div class="col-12">
                    <h4 class="fs-16 mb-1">Hello, <%=Strusername %>!</h4>
                    <p class="text-muted mb-0">Here's what's happening with your store today.</p>
                </div>
            </div>

            <div class="row">

                <%-- Products --%>
                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card card-animate card-products h-80 shadow-sm">
                        <div class="card-body p-3">
                            <div class="d-flex align-items-center mb-3">
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title fs-2"><i class="bx bx-package"></i></span>
                                </div>
                                <div class="flex-grow-1 ms-3">
                                    <p class="text-uppercase fw-bold text-muted mb-0" style="font-size:11px;">Products</p>
                                </div>
                            </div>
                            <h2 class="mb-2 fw-bold"><span class="counter-value"><%=strTotalProduct %></span></h2>
                            <a href="/Admin/view-products.aspx" class="view-link">View Details <i class="ri-arrow-right-s-line align-middle"></i></a>
                        </div>
                    </div>
                </div>

                <%-- Categories --%>
                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card card-animate card-categories h-80 shadow-sm">
                        <div class="card-body p-3">
                            <div class="d-flex align-items-center mb-3">
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title fs-2"><i class="bx bx-category"></i></span>
                                </div>
                                <div class="flex-grow-1 ms-3">
                                    <p class="text-uppercase fw-bold text-muted mb-0" style="font-size:11px;">Categories</p>
                                </div>
                            </div>
                            <h2 class="mb-2 fw-bold"><span class="counter-value"><%=strTotalOrder %></span></h2>
                            <a href="/Admin/category.aspx" class="view-link">View Details <i class="ri-arrow-right-s-line align-middle"></i></a>
                        </div>
                    </div>
                </div>

                <%-- Brands --%>
                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card card-animate card-brands h-80 shadow-sm">
                        <div class="card-body p-3">
                            <div class="d-flex align-items-center mb-3">
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title fs-2"><i class="bx bx-badge"></i></span>
                                </div>
                                <div class="flex-grow-1 ms-3">
                                    <p class="text-uppercase fw-bold text-muted mb-0" style="font-size:11px;">Brands</p>
                                </div>
                            </div>
                            <h2 class="mb-2 fw-bold"><span class="counter-value"><%=strBrand %></span></h2>
                            <a href="/Admin/Brand.aspx" class="view-link">View Details <i class="ri-arrow-right-s-line align-middle"></i></a>
                        </div>
                    </div>
                </div>

                <%-- Blogs --%>
                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card card-animate card-blogs h-80 shadow-sm">
                        <div class="card-body p-3">
                            <div class="d-flex align-items-center mb-3">
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title fs-2"><i class="bx bx-book-content"></i></span>
                                </div>
                                <div class="flex-grow-1 ms-3">
                                    <p class="text-uppercase fw-bold text-muted mb-0" style="font-size:11px;">Blogs</p>
                                </div>
                            </div>
                            <h2 class="mb-2 fw-bold"><span class="counter-value"><%=strBlogs %></span></h2>
                            <a href="/Admin/view-blogs.aspx" class="view-link">View Details <i class="ri-arrow-right-s-line align-middle"></i></a>
                        </div>
                    </div>
                </div>

                <%-- Cart Enquiry --%>
                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card card-animate card-cartenq h-80 shadow-sm">
                        <div class="card-body p-3">
                            <div class="d-flex align-items-center mb-3">
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title fs-2"><i class="bx bx-cart"></i></span>
                                </div>
                                <div class="flex-grow-1 ms-3">
                                    <p class="text-uppercase fw-bold text-muted mb-0" style="font-size:11px;">Cart Enquiry</p>
                                </div>
                            </div>
                            <h2 class="mb-2 fw-bold"><span class="counter-value"><%=strTotalCustomer %></span></h2>
                            <a href="/Admin/cart-enquiry.aspx" class="view-link">View Details <i class="ri-arrow-right-s-line align-middle"></i></a>
                        </div>
                    </div>
                </div>

                <%-- Contact Request --%>
                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card card-animate card-contact h-80 shadow-sm">
                        <div class="card-body p-3">
                            <div class="d-flex align-items-center mb-3">
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title fs-2"><i class="bx bx-envelope"></i></span>
                                </div>
                                <div class="flex-grow-1 ms-3">
                                    <p class="text-uppercase fw-bold text-muted mb-0" style="font-size:11px;">Contact Request</p>
                                </div>
                            </div>
                            <h2 class="mb-2 fw-bold"><span class="counter-value"><%=strContact %></span></h2>
                            <a href="/Admin/contact-request.aspx" class="view-link">View Details <i class="ri-arrow-right-s-line align-middle"></i></a>
                        </div>
                    </div>
                </div>

                <%-- Product Enquiry --%>
                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card card-animate card-productenq h-80 shadow-sm">
                        <div class="card-body p-3">
                            <div class="d-flex align-items-center mb-3">
                                <div class="avatar-sm flex-shrink-0">
                                    <span class="avatar-title fs-2"><i class="bx bx-message-square-detail"></i></span>
                                </div>
                                <div class="flex-grow-1 ms-3">
                                    <p class="text-uppercase fw-bold text-muted mb-0" style="font-size:11px;">Product Enquiry</p>
                                </div>
                            </div>
                            <h2 class="mb-2 fw-bold"><span class="counter-value"><%=strToday %></span></h2>
                            <a href="/Admin/view-productenq.aspx" class="view-link">View Details <i class="ri-arrow-right-s-line align-middle"></i></a>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
