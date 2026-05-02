<%@ Page
    ValidateRequest="false"
    Title=""
    Language="C#"
    MasterPageFile="~/Admin/MasterPage.master"
    AutoEventWireup="true"
    CodeFile="banner-images.aspx.cs"
    Inherits="Admin_banner_images" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdn.materialdesignicons.com/7.4.47/css/materialdesignicons.min.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">

            <!-- Page Title -->
            <div class="row mb-3">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Banner Images</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item active">Banner Images</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Add / Edit Form -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header d-flex align-items-center justify-content-between">
                            <h5 class="card-title mb-0">
                                <%=Request.QueryString["id"] == null ? "Add Banner" : "Update Banner" %>
                            </h5>
                        </div>
                        <div class="card-body">
                            <div class="row">

                                <!-- Banner Title -->
                                <div class="col-lg-4 mb-3">
                                    <label class="form-label">Banner Title<sup style="color: red;">*</sup></label>
                                    <asp:TextBox runat="server" ID="txtTitle" MaxLength="100"
                                        CssClass="form-control" PlaceHolder="Enter Banner Title" />
                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server"
                                        ControlToValidate="txtTitle" Display="Dynamic" ForeColor="Red"
                                        SetFocusOnError="true" ValidationGroup="Save"
                                        ErrorMessage="Field can't be empty" />
                                    <asp:RegularExpressionValidator ID="revTitle" runat="server"
                                        ControlToValidate="txtTitle" Display="Dynamic" ForeColor="Red"
                                        ValidationExpression="^[\s\S]{2,100}$" ValidationGroup="Save"
                                        ErrorMessage="Min 2 Max 100 characters required" />
                                </div>

                                <!-- External Link -->
                                <div class="col-lg-4 mb-3">
                                    <label class="form-label">External Link<sup style="color: red;">*</sup></label>
                                    <asp:TextBox runat="server" ID="txtlink" MaxLength="150"
                                        CssClass="form-control" PlaceHolder="Enter External Link" />
                                    <asp:RequiredFieldValidator ID="rfvLink" runat="server"
                                        ControlToValidate="txtlink" Display="Dynamic" ForeColor="Red"
                                        SetFocusOnError="true" ValidationGroup="Save"
                                        ErrorMessage="Field can't be empty" />
                                    <asp:RegularExpressionValidator ID="revLink" runat="server"
                                        ControlToValidate="txtlink" Display="Dynamic" ForeColor="Red"
                                        ValidationExpression="^[\s\S]{2,150}$" ValidationGroup="Save"
                                        ErrorMessage="Min 2 Max 150 characters required" />
                                </div>

                                <!-- Display Order -->
                                <div class="col-lg-4 mb-3">
                                    <label class="form-label">Display Order<sup style="color: red;">*</sup></label>
                                    <asp:TextBox runat="server" ID="txtOrder" MaxLength="10"
                                        CssClass="form-control" PlaceHolder="Enter Display Order" />
                                    <asp:RequiredFieldValidator ID="rfvOrder" runat="server"
                                        ControlToValidate="txtOrder" Display="Dynamic" ForeColor="Red"
                                        SetFocusOnError="true" ValidationGroup="Save"
                                        ErrorMessage="Field can't be empty" />
                                </div>

                                <!-- Desktop Banner Image -->
                                <div class="col-lg-6 mb-3">
                                    <label class="form-label">Desktop Banner Image<sup style="color: red;">*</sup></label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control mb-1" />
                                    <small style="color: red;">.png, .jpeg, .jpg, .webp formats required — 1903px × 520px
                                    </small>
                                    <div class="mt-2"><%=strThumbImage %></div>
                                    <asp:RequiredFieldValidator ID="reqUpload" runat="server"
                                        ControlToValidate="FileUpload1" Display="Dynamic" ForeColor="Red"
                                        SetFocusOnError="true" ValidationGroup="Save"
                                        ErrorMessage="Please select a desktop image" />
                                </div>

                                <!-- Mobile Banner Image -->
                                <div class="col-lg-6 mb-3">
                                    <label class="form-label">Mobile Banner Image<sup style="color: red;">*</sup></label>
                                    <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control mb-1" />
                                 <small style="color: red;">.png, .jpeg, .jpg, .webp formats required — 600px × 528px</small>
                                    <div class="mt-2"><%=strThumbImageMob %></div>
                                    <asp:RequiredFieldValidator ID="reqUploadMob" runat="server"
                                        ControlToValidate="FileUpload2" Display="Dynamic" ForeColor="Red"
                                        SetFocusOnError="true" ValidationGroup="Save"
                                        ErrorMessage="Please select a mobile image" />
                                </div>

                                <!-- Buttons -->
                                <div class="col-lg-12 mt-2">
                                    <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
                                    <asp:Button ID="btnUpload" runat="server"
                                        Text="Save"
                                        CssClass="btn btn-primary waves-effect waves-light"
                                        ValidationGroup="Save"
                                        OnClick="btnUpload_Click" />
                                    <asp:Button ID="btnClear" runat="server"
                                        Text="Clear"
                                        CssClass="btn btn-outline-primary waves-effect waves-light ms-2"
                                        OnClick="btnClear_Click"
                                        CausesValidation="false" />
                                    <asp:Label ID="lblThumb" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblThumbMob" runat="server" Visible="false"></asp:Label>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Manage Table -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title mb-0">Manage Banner Images</h5>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination"
                                class="table dt-responsive align-middle table-striped table-bordered myTable"
                                style="width: 100%;">
                                <thead class="table-light">
                                    <tr>
                                        <th>#</th>
                                        <th>Banner Title</th>
                                        <th>Desktop Image</th>
                                        <th>Mobile Image</th>
                                        <th>Display Order</th>
                                        <th>Updated On</th>
                                        <th class="text-center">Action</th>
                                    </tr>
                                </thead>
                                <tbody id="tbdy">
                                    <%=strImages %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/banner-images.js"></script>
</asp:Content>
