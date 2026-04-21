<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="manage-product-label.aspx.cs" Inherits="Admin_manage_product_label" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">

            <%-- Page Title --%>
            <div class="row mb-3">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Product Label</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Master Settings</a></li>
                                <li class="breadcrumb-item active">Product Label</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <%-- Add / Edit Card --%>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title mb-0">Add Product Label</h5>
                        </div>
                        <div class="card-body">
                            <div class="row align-items-end">
                                <div class="col-lg-6 mb-3">
                                    <label class="form-label">
                                        Product Label <sup style="color:red;">*</sup>
                                    </label>
                                    <asp:TextBox runat="server" ID="txtlable" MaxLength="100"
                                        CssClass="form-control"
                                        placeholder="Enter Product Label" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server"
                                        ControlToValidate="txtlable" Display="Dynamic"
                                        ForeColor="Red" SetFocusOnError="true"
                                        ValidationGroup="Save"
                                        ErrorMessage="Field can't be empty" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                        ControlToValidate="txtlable" Display="Dynamic"
                                        ValidationGroup="Save"
                                        ValidationExpression="^[\s\S]{2,100}$"
                                        ForeColor="Red"
                                        ErrorMessage="Min 2 Max 100 characters Required" />
                                </div>
                                <div class="col-lg-2 mb-3">
                                    <asp:Button runat="server" ID="btnSave"
                                        CssClass="btn btn-primary waves-effect waves-light"
                                        Text="Save"
                                        ValidationGroup="Save"
                                        OnClick="btnSave_Click" />
                                    <asp:Button runat="server" ID="btnClear"
                                        CssClass="btn btn-outline-secondary ms-2"
                                        Text="Clear"
                                        CausesValidation="false"
                                        OnClick="btnClear_Click" />
                                </div>
                                <asp:Label ID="lblHiddenId" runat="server" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%-- Manage Table Card --%>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title mb-0">Manage Product Labels</h5>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination"
                                class="table table-nowrap align-middle table-striped table-bordered myTable"
                                style="width:100%;">
                                <thead class="table-light">
                                    <tr>
                                        <th>#</th>
                                        <th>Product Label</th>
                                        <th>Added On</th>
                                        <th class="text-center">Action</th>
                                    </tr>
                                </thead>
                                <tbody id="tbdy">
                                    <%=strLabels %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>

    <script src="assets/js/pages/manage-product-label.js"></script>
</asp:Content>