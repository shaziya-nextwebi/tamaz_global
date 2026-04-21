<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Brand-order.aspx.cs" Inherits="Admin_Brand_order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .sortablev {
            list-style-type: none;
            margin: 0;
            padding: 10px;
            width: 100%;
            min-height: 200px;
            display: flex;
            flex-wrap: wrap;
            gap: 12px;
        }

        .sortablev li {
            cursor: grab;
            width: 180px;
        }

        .sortablev li .maindiv {
            background: #0b3ab8;
            color: #fff;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 2px 8px rgba(0,0,0,0.15);
            text-align: center;
        }

        .sortablev li .maindiv img {
            width: 100%;
            height: 140px;
            object-fit: cover;
            display: block;
            background: #fff;
        }

        .sortablev li .maindiv div {
            padding: 8px 6px;
            font-size: 12px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">

            <%-- Page Title --%>
            <div class="row mb-3">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Re-Arrange Brand Display Order</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Products</a></li>
                                <li class="breadcrumb-item active">Brand Order</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <h5 class="card-title mb-0">Re-Arrange Product Display Order by Brand</h5>
                </div>
                <div class="card-body">

                    <div class="row mb-4">
                        <div class="col-lg-4">
                            <label class="form-label">Select Brand</label>
                            <asp:DropDownList runat="server" ID="ddlCategory"
                                CssClass="form-select ddlBrand"
                                AutoPostBack="false" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-lg-12">
                            <ul id="left-defaults" class="sortablev"></ul>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <button type="button" id="btnUpdate"
                                class="btn btn-primary waves-effect waves-light">
                                Update Order
                            </button>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/brand-order.js"></script>
</asp:Content>