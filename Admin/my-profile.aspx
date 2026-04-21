<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="my-profile.aspx.cs" Inherits="Admin_my_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">My Profile</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Dashboard</a></li>
                                <li class="breadcrumb-item active">My Profile</li>
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
                            <h4 class="card-title mb-0 flex-grow-1">My Profile</h4>
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
                                    <label>User Role</label>
                                    <asp:DropDownList runat="server" class="form-control mb-2 mr-sm-2" ID="ddlRole">
                                        <asp:ListItem Value="">User Role</asp:ListItem>
                                        <asp:ListItem Value="1">Admin</asp:ListItem>
                                        <asp:ListItem Value="2">User</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label>User Name</label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtName" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtName" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label>Login Id</label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtUserId" />
                                </div>
                                <div class="col-md-3">
                                    <label>Email</label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtEmail" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <label>Contact No</label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtContactNo" />

                                </div>
                                <div class="col-md-3">
                                    <label>Profile Image</label>
                                    <asp:FileUpload runat="server" class="form-control mb-2 mr-sm-2" ID="fuProfile" />
                                    <small>Image format .png, .jpeg, .jpg, .webp, .gif with 128px X 128px</small>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <%=strThumbImage %>
                                    </div>
                                </div>

                                <div class="col-xxl-3 col-md-6">
                                    <div>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save" CssClass="btn btn-primary waves-effect waves-light m-t-25" />
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

