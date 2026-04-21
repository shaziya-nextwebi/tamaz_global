<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="blog-tags.aspx.cs" Inherits="Admin_blog_tags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        sup {
            color: red !important;
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
                        <h4 class="mb-sm-0">Blog Tags</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item active">Blog Tags</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <%-- Add / Edit Form --%>
            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <div class="col-lg-6 mb-3">
                            <label class="form-label">Tag Name <sup>*</sup></label>
                            <asp:TextBox runat="server" ID="txtName" MaxLength="100"
                                CssClass="form-control"
                                placeholder="Enter Tag Name" />
                            <asp:RequiredFieldValidator ID="req1" runat="server"
                                ControlToValidate="txtName" Display="Dynamic"
                                ForeColor="Red" SetFocusOnError="true"
                                ValidationGroup="Save" ErrorMessage="Field can't be empty" />
                            <asp:RegularExpressionValidator ID="rev1" runat="server"
                                ControlToValidate="txtName" Display="Dynamic"
                                ForeColor="Red" ValidationGroup="Save"
                                ValidationExpression="^[\s\S]{2,100}$"
                                ErrorMessage="Min 2, Max 100 characters required" />
                        </div>

                        <%-- Hidden fields kept for model binding --%>
                        <div style="display: none;">
                            <asp:TextBox runat="server" ID="txtUrl" CssClass="form-control" />
                            <asp:TextBox runat="server" ID="txtOrder" CssClass="form-control" />
                            <asp:TextBox runat="server" ID="txtPageTitle" CssClass="form-control" />
                            <asp:TextBox runat="server" ID="txtMetaDesc" TextMode="MultiLine" CssClass="form-control" />
                            <asp:TextBox runat="server" ID="txtMetaKeys" TextMode="MultiLine" CssClass="form-control" />
                        </div>

                        <div class="col-lg-12 mt-2">
                            <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
                            <asp:Button runat="server" ID="btnSave"
                                CssClass="btn btn-primary waves-effect waves-light"
                                Text="Save" ValidationGroup="Save"
                                OnClick="btnSave_Click" />
                            <asp:Button runat="server" ID="btnClear"
                                CssClass="btn btn-outline-secondary ms-2"
                                Text="Clear" CausesValidation="false"
                                OnClick="btnClear_Click" />
                        </div>

                    </div>
                </div>
            </div>

            <%-- Tags Table --%>
            <div class="card mt-3">
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-hover table-striped table-bordered dt-responsive align-middle" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Tag Name</th>
                                    <th>Added On</th>
                                    <th class="text-center">Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=strCategory %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/blog-tags.js"></script>
    <script>
        <%if (!string.IsNullOrEmpty(strSuccessMsg))
        { %>
        Swal.fire({ icon: 'success', title: '<%=strSuccessMsg %>', timer: 2000, showConfirmButton: false });
        <%} %>
        <%if (!string.IsNullOrEmpty(strErrorMsg))
        { %>
        Swal.fire({ icon: 'error', title: '<%=strErrorMsg %>' });
        <%} %>
    </script>
</asp:Content>
