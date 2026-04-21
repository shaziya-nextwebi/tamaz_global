<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reset-password.aspx.cs" Inherits="Admin_reset_password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Tamaz Global| Reset Password</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- App favicon -->
    <link rel="shortcut icon" href="assets/images/favicon.ico" />
    <!-- Bootstrap Css -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Icons Css -->
    <link href="assets/css/icons.min.css" rel="stylesheet" type="text/css" />
    <!-- App Css-->
    <link href="assets/css/app.min.css" rel="stylesheet" type="text/css" />
    <!-- custom Css-->
    <link href="assets/css/custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="auth-page-wrapper">

            <!-- auth page content -->
            <div class="auth-page-content">
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-md-8 col-lg-6 col-xl-5">
                            <div class="card mt-4">

                                <div class="card-body p-4">
                                    <div class="text-center mt-2">
                                        <a href="/" class="d-inline-block auth-logo">
                                            <img src="assets/images/logo-head.png" alt="" height="70" />
                                        </a>
                                    </div>
                                    <div class="text-center mt-2">
                                        <h5 class="text-primary">Create new password</h5>
                                        <p class="text-muted">Your new password must be different from previous used password.</p>
                                    </div>

                                    <div class="p-2">
                                        <asp:Label ID="lblStatus" runat="server" Style="width: 100%;" Visible="false"></asp:Label>
                                        <div class="mb-3">
                                            <label class="form-label">Password</label>
                                            <div class="position-relative auth-pass-inputgroup">
                                                <asp:TextBox runat="server" TextMode="Password" class="form-control pe-5 password-input" onpaste="return false" placeholder="Enter password" ID="txtPassword" />
                                                <button class="btn btn-link position-absolute end-0 top-0 text-decoration-none text-muted password-addon" type="button" id="password-addon"><i class="ri-eye-fill align-middle"></i></button>
                                            </div>
                                            <div id="passwordInput" class="form-text">Must be at least 8 characters.</div>
                                            <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Reset" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ValidationGroup="Reset" SetFocusOnError="true" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Password" ValidationExpression="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[~!@#$%^&*(),.?:{}|<>]).{8,}"></asp:RegularExpressionValidator>

                                        </div>

                                        <div class="mb-3">
                                            <label class="form-label" for="confirm-password-input">Confirm Password</label>
                                            <div class="position-relative auth-pass-inputgroup mb-3">
                                                <asp:TextBox TextMode="Password" runat="server" class="form-control pe-5" onpaste="return false" placeholder="Confirm password" ID="txtConfirmPassword" />
                                                <button class="btn btn-link position-absolute end-0 top-0 text-decoration-none text-muted password-addon" type="button"><i class="ri-eye-fill align-middle"></i></button>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Reset" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="comp1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Reset" ErrorMessage="Password doesn't match"></asp:CompareValidator>
                                            <asp:RegularExpressionValidator runat="server" ValidationGroup="Reset" SetFocusOnError="true" ControlToValidate="txtConfirmPassword" Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Password" ValidationExpression="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[~!@#$%^&*(),.?:{}|<>]).{8,}"></asp:RegularExpressionValidator>
                                        </div>

                                        <div id="password-contain" class="p-3 bg-light mb-2 password-contain rounded">
                                            <h5 class="fs-13">Password must contain:</h5>
                                            <p id="pass-length" class="invalid fs-12 mb-2">Minimum <b>8 characters</b></p>
                                            <p id="pass-lower" class="invalid fs-12 mb-2">At <b>lowercase</b> letter (a-z)</p>
                                            <p id="pass-upper" class="invalid fs-12 mb-2">At least <b>uppercase</b> letter (A-Z)</p>
                                            <p id="pass-number" class="invalid fs-12 mb-2">At least <b>number</b> (0-9)</p>
                                            <p id="pass-special" class="invalid fs-12 mb-0">At least <b>Special character</b> (~!@#$%^&*(),.?:{}|<>)</p>
                                        </div>


                                        <div class="mt-4">
                                            <asp:Button runat="server" ID="btnLogin" OnClick="btnReset_Click" class="btn btn-primary btn-primary-login w-100" ValidationGroup="Reset" Text="Reset Password" />

                                        </div>


                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                        </div>
                    </div>
                    <!-- end row -->
                </div>
                <!-- end container -->
            </div>
            <!-- end auth page content -->

            <!-- footer -->
            <footer class="footer">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="text-center">
                                <p class="mb-0 text-muted">
                                    Designed & Developed by Nextwebi
                           
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </footer>
            <!-- end Footer -->
        </div>
        <!-- end auth-page-wrapper -->

        <!-- JAVASCRIPT -->
        <script src="assets/libs/bootstrap/js/bootstrap.bundle.min.js"></script>
        <script src="assets/js/pages/plugins/lord-icon-2.1.0.js"></script>
        <!-- password-addon init -->
        <script src="assets/js/pages/passowrd-create.init.js"></script>
    </form>
</body>
</html>
