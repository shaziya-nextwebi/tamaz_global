<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="create-admin-user.aspx.cs" Inherits="Admin_my_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Admin Users</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Admin Settings</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"] != null ? "Update" : "Add" %> Admin User</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end page title -->

            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1"><%=Request.QueryString["id"] != null ? "Update" : "Add" %> Admin User</h4>
                        </div>
                        <!-- end card header -->
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                                </div>
                            </div>
                            <div class="row gy-4">
                                <div class="col-md-3">
                                    <label>User Role <sup>*</sup></label>
                                    <asp:DropDownList runat="server" class="form-control mb-2 mr-sm-2" data-choices data-choices-search-true ID="ddlRole">
                                        <asp:ListItem Value="">User Role</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRole" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-3">
                                    <label>User Name <sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtName" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtName" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label>Email <sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtEmail" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-3">
                                    <label>Contact No</label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtContactNo" />
                                </div>

                                <div class="col-md-3">
                                    <label>Login Id <sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtUserId" />
                                </div>

                                <div class="col-md-3">
                                    <label>Password <sup>*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtPassword" />
                                </div>

                                <div class="col-md-6">
                                    <label>Profile Image</label>
                                    <asp:FileUpload runat="server" class="form-control mb-2 mr-sm-2" ID="fuProfile" />
                                    <small style="color: red">Image format .png, .jpeg, .jpg, .webp, .gif with 128px X 128px</small>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <%=strThumbImage %>
                                    </div>
                                </div>
                                <div class="col-xxl-3 col-md-6">
                                    <div>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save" CssClass="btn btn-primary waves-effect waves-light m-t-25" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-primary waves-effect waves-light m-t-25" OnClick="btnClear_Click" />
                                        <asp:Label ID="lblThumb" runat="server" Visible="false"></asp:Label>
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
</asp:Content>

