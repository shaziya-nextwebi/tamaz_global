<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="change-password.aspx.cs" Inherits="Admin_change_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Change Password</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Dashboard</a></li>
                                <li class="breadcrumb-item active">Change Password</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <!-- end page title -->

            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex d-none">
                            <h4 class="card-title mb-0 flex-grow-1">Change Password</h4>
                        </div>
                        <!-- end card header -->
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                                </div>
                            </div>
                            <div class="row gy-4">
                                <div class="col-lg-4">
                                    <label>Current Password</label>
                                    <div class="position-relative auth-pass-inputgroup">
                                        <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 password" TextMode="Password" ID="txtCurrent" />
                                        <button class="btn btn-link position-absolute end-0 top-0 text-decoration-none text-muted password-addon" type="button" id="password-addon"><i class="ri-eye-fill align-middle"></i></button>
                                        <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtCurrent" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <label>New Password</label>
                                    <div class="position-relative auth-pass-inputgroup">
                                        <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 password-input password" TextMode="Password" ID="txtNew" />
                                        <button class="btn btn-link position-absolute end-0 top-0 text-decoration-none text-muted password-addon" type="button"><i class="ri-eye-fill align-middle"></i></button>
                                        <asp:RegularExpressionValidator runat="server" ValidationGroup="Save" SetFocusOnError="true" ControlToValidate="txtNew" Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Password" ValidationExpression="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[~!@#$%^&*(),.?:{}|<>]).{8,}"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNew" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <label>Confirm New Password</label>
                                    <div class="position-relative auth-pass-inputgroup">
                                        <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2 password" TextMode="Password" ID="txtConfirm" />
                                        <button class="btn btn-link position-absolute end-0 top-0 text-decoration-none text-muted password-addon" type="button"><i class="ri-eye-fill align-middle"></i></button>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtConfirm" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator runat="server" ValidationGroup="Save" SetFocusOnError="true" ControlToValidate="txtConfirm" Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Password" ValidationExpression="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[~!@#$%^&*(),.?:{}|<>]).{8,}"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="txtConfirm" ControlToCompare="txtNew" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Password doesn't match"> </asp:CompareValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-7">

                                    <div id="password-contain" class="p-3 bg-light mb-2 password-contain rounded">
                                        <h5 class="fs-13">Password must contain:</h5>
                                        <p id="pass-length" class="invalid fs-12 mb-2">Minimum <b>8 characters</b></p>
                                        <p id="pass-lower" class="invalid fs-12 mb-2">At <b>lowercase</b> letter (a-z)</p>
                                        <p id="pass-upper" class="invalid fs-12 mb-2">At least <b>uppercase</b> letter (A-Z)</p>
                                        <p id="pass-number" class="invalid fs-12 mb-2">At least <b>number</b> (0-9)</p>
                                        <p id="pass-special" class="invalid fs-12 mb-0">At Lease <b>Special character</b> (~!@#$%^&*(),.?":{}|<>)</p>
                                    </div>
                                    
                                </div>
                            </div>
                            <div class="row">


                                <div class="col-xxl-3 col-md-6">
                                    <div>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save" CssClass="btn btn-primary waves-effect waves-light m-t-25" />

                                    </div>
                                </div>
                                <!--end col-->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/change-password.js"></script>
</asp:Content>

