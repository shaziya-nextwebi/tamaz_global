<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="cart-enquiry.aspx.cs" Inherits="Admin_product_enquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        sup {
            color: red !important;
        }

        .message-preview {
            max-width: 200px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
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
                        <h4 class="mb-sm-0">Cart Enquiry Requests</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item active">Cart Enquiry</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-hover table-striped table-bordered dt-responsive align-middle myTable" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Product Name &amp; Quantity</th>
                                    <th>Name</th>
                                    <th>Mobile</th>
                                    <th>City</th>
                                    <th>Message</th>
                                    <th>Added On</th>
                                    <th class="text-center">Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=strContact %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <%-- View Message Modal --%>
    <div class="modal fade" id="msgModal" tabindex="-1" aria-labelledby="msgModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="msgModalLabel">Enquiry from <span id="modalSenderName"></span></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">Name</div>
                        <div class="col-sm-8" id="mdName"></div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">Mobile</div>
                        <div class="col-sm-8" id="mdMobile"></div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">City</div>
                        <div class="col-sm-8" id="mdCity"></div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">Product Name &amp; Quantity</div>
                        <div class="col-sm-8" id="mdProduct"></div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">Received On</div>
                        <div class="col-sm-8" id="mdDate"></div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-sm-4 text-muted fw-bold">Message</div>
                        <div class="col-sm-8">
                            <div id="mdMessage"
                                style="white-space: pre-wrap; background: #f8f9fa; border: 1px solid #e0e0e0; border-radius: 6px; padding: 14px; min-height: 80px; font-size: 14px; line-height: 1.7;">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/cart-enquiry.js"></script>
</asp:Content>
