<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tamaz Global | Admin</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- App favicon -->
    <link rel="shortcut icon" href="assets/images/favicon.png" />
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
<%--    <%=CommonModel.Decrypt("Nzjn3MD27kZoCTT5CabktgrPxgFKUIFyARVgXaBYE58= ") %>--%>
    <form id="form1" runat="server">
        <div class="auth-page-wrapper pt-5">
            <div class="auth-page-content">
                <div class="container">
                    <!-- end row -->
                    <div class="row justify-content-center">
                        <div class="col-md-8 col-lg-6 col-xl-6">
                            <div class="card mt-4">
                                <div class="card-body p-4">
                                    <div class="text-center mt-2">
                                        <a href="javascript:void(0);" class="d-inline-block auth-logo">
                                            <img src="assets/images/logo-head.png" alt="" height="70" />
                                        </a>
                                    </div>
                                    <div class="text-center mt-2">
                                        <h5 class="text-primary">Welcome Back !</h5>
                                        <p class="text-muted">Sign in to continue to <b>Tamaz Global</b> </p>
                                        <asp:Label ID="lblStatus" runat="server" Style="width: 100%;" Visible="false"></asp:Label>
                                    </div>
                                    <div class="p-2 mt-4 mb-4">
                                        <div class="mb-3">
                                            <label for="username" class="form-label">Username</label>
                                            <asp:TextBox runat="server" class="form-control" ID="txtUserName" placeholder="Enter username" />
                                            <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Login" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="mb-3">
                                            <div class="float-end">
                                                <a href="forgot-password.aspx" class="text-muted">Forgot password?</a>
                                            </div>
                                            <label class="form-label" for="password-input">Password</label>
                                            <div class="position-relative auth-pass-inputgroup mb-3">
                                                <asp:TextBox runat="server" TextMode="Password" class="form-control pe-5 password-input" placeholder="Enter password" ID="txtPassword" />
                                                <button class="btn btn-link position-absolute end-0 top-0 text-decoration-none text-muted shadow-none password-addon" type="button" id="password-addon"><i class="mdi mdi-eye align-middle fs-16 vispass"></i></button>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Login" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                            </div>

                                        </div>

                                        <div class="form-check">
                                            <input class="form-check-input" type="checkbox" runat="server" value="" id="chkLogKeep" />
                                            <label class="form-check-label" for="<%=chkLogKeep.ClientID %>">Remember me</label>
                                        </div>

                                        <div class="mt-4">
                                            <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" class="btn btn-primary btn-primary-login w-100" ValidationGroup="Login" Text="Sign In" />
                                        </div>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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
        </div>
        <!-- JAVASCRIPT -->
        <script src="assets/libs/bootstrap/js/bootstrap.bundle.min.js"></script>
        <script src="assets/js/plugins.js"></script>
        <script src="assets/libs/prismjs/prism.js"></script>
        <script src="assets/js/pages/notifications.init.js"></script>
        <script src="assets/js/pages/password-addon.init.js"></script>
    </form>
</body>
</html>
