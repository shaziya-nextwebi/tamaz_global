<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="category.aspx.cs" Inherits="Admin_category" ValidateRequest="false" %>

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
                        <h4 class="mb-sm-0"><%=Request.QueryString["id"] != null ? "Update" : "Add New" %> Category</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Master Settings</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"] != null ? "Update" : "Add" %> Category</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">

                <%-- Left: Main Fields --%>
                <div class="col-lg-8">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title mb-0">Category Details</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">

                                <div class="col-lg-6 mb-3">
                                    <label class="form-label">Category Name <sup>*</sup></label>
                                    <asp:TextBox runat="server" ID="txtCategory" MaxLength="100"
                                        CssClass="form-control txtCategory"
                                        placeholder="Enter Category Name" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server"
                                        ControlToValidate="txtCategory" Display="Dynamic"
                                        ForeColor="Red" SetFocusOnError="true"
                                        ValidationGroup="Save" ErrorMessage="Field can't be empty" />
                                </div>

                                <div class="col-lg-6 mb-3">
                                    <label class="form-label">Category URL <sup>*</sup></label>
                                    <asp:TextBox runat="server" ID="txtUrl"
                                        CssClass="form-control txtURL"
                                        placeholder="Auto-Generated" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtUrl" Display="Dynamic"
                                        ForeColor="Red" SetFocusOnError="true"
                                        ValidationGroup="Save" ErrorMessage="Field can't be empty" />
                                </div>

                                <div class="col-lg-6 mb-3">
                                    <label class="form-label">Category Order <sup>*</sup></label>
                                    <asp:TextBox runat="server" ID="txtOrder" MaxLength="10"
                                        CssClass="form-control"
                                        placeholder="Display Order"
                                        onkeypress="return isNumber(event)" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtOrder" Display="Dynamic"
                                        ForeColor="Red" SetFocusOnError="true"
                                        ValidationGroup="Save" ErrorMessage="Field can't be empty" />
                                </div>

                                <div class="col-lg-6 mb-3 d-flex align-items-end">
                                    <div class="form-check form-switch form-switch-md mb-2">
                                        <asp:CheckBox runat="server" ID="chkHome" CssClass="form-check-input" />
                                        <label class="form-check-label" for="<%=chkHome.ClientID %>">Display on Home?</label>
                                    </div>
                                </div>

                                <div class="col-lg-12 mb-3" style="display: none;">
                                    <label class="form-label">Short Description</label>
                                    <asp:TextBox runat="server" ID="txtShortDesc" TextMode="MultiLine"
                                        CssClass="form-control" Rows="3" />
                                </div>

                                <div class="col-lg-12 mb-3">
                                    <label class="form-label">Full Description</label>
                                    <button type="button" class="clean-html-btn"
                                        data-editor="<%=txtFullDesc.ClientID%>">
                                        ✦ Clean HTML
                                    </button>
                                    <asp:TextBox runat="server" ID="txtFullDesc" TextMode="MultiLine"
                                        CssClass="form-control summernote" Rows="6" />
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <%-- Right: SEO + Image --%>
                <div class="col-lg-4">

                    <%-- SEO Card --%>
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title mb-0">SEO Details</h5>
                        </div>
                        <div class="card-body">
                            <div class="mb-3">
                                <label class="form-label">Page Title</label>
                                <asp:TextBox runat="server" ID="txtPageTitle"
                                    data-id="Title"
                                    CssClass="form-control textcount1"
                                    placeholder="Page Title" />
                                <span></span>
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Meta Keys</label>
                                <asp:TextBox runat="server" ID="txtMetaKeys"
                                    CssClass="form-control"
                                    placeholder="Meta Keywords" />
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Meta Description</label>
                                <asp:TextBox runat="server" ID="txtMetaDesc"
                                    data-id="MetaDesc" TextMode="MultiLine"
                                    CssClass="form-control textcount1"
                                    Rows="3" placeholder="Meta Description" />
                                <span></span>
                            </div>
                        </div>
                    </div>

                    <%-- Image Card --%>
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title mb-0">Category Image</h5>
                        </div>
                        <div class="card-body">
                            <div class="mb-3">
                                <label class="form-label">Thumb Image <sup>*</sup></label>
                                <asp:FileUpload ID="fuIndSamll" runat="server" CssClass="form-control" />
                                <small class="text-danger">.png .jpeg .jpg .gif .webp — 500px × 500px</small>
                                <asp:RequiredFieldValidator ID="ReqFileupload1" runat="server"
                                    ControlToValidate="fuIndSamll" Display="Dynamic"
                                    ForeColor="Red" SetFocusOnError="true"
                                    ValidationGroup="Save" ErrorMessage="Please upload thumb image" />
                                <div class="mt-2"><%=strBannerImage %></div>
                            </div>

                            <%-- Mobile image hidden --%>
                            <div style="display: none;">
                                <asp:FileUpload ID="fuIndThumb" runat="server" CssClass="form-control" />
                                <div class="mt-2"><%=strMobileImage %></div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>

            <%-- Save Buttons --%>
            <div class="row mb-4">
                <div class="col-lg-12">
                    <asp:Button runat="server" ID="btnSave"
                        CssClass="btn btn-primary waves-effect waves-light"
                        Text="Save" ValidationGroup="Save"
                        OnClick="btnSave_Click" />
                    <asp:Button runat="server" ID="btnNew"
                        CssClass="btn btn-info ms-2" Visible="false"
                        Text="Add New Category"
                        OnClick="btnNew_Click"
                        CausesValidation="false" />
                    <asp:Label ID="lblIndustrialInternImage1" runat="server" Visible="false" />
                    <asp:Label ID="lblIndustrialInternImage2" runat="server" Visible="false" />
                </div>
            </div>

            <%-- Manage Table --%>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header d-flex align-items-center justify-content-between">
                            <h5 class="card-title mb-0">Manage Categories</h5>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination"
                                class="table table-nowrap align-middle table-striped table-bordered myTable"
                                style="width: 100%;">
                                <thead class="table-light">
                                    <tr>
                                        <th>#</th>
                                        <th>Category</th>
                                        <th>Order</th>
                                        <th>Display Home</th>
                                        <th>Thumb Image</th>
                                        <th>Page Title</th>
                                        <th>Added On</th>
                                        <th class="text-center">Action</th>
                                    </tr>
                                </thead>
                                <tbody id="tbdy">
                                    <%=strCategory %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/category.js"></script>
</asp:Content>
