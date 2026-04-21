<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="page-master.aspx.cs" Inherits="Admin_page_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">

            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Page Master</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Admin Settings</a></li>
                                <li class="breadcrumb-item active">Page Master</li>
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
                            <h4 class="card-title mb-0 flex-grow-1">Add Page Master</h4>
                        </div>
                        <!-- end card header -->
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                                </div>
                            </div>
                            <div class="row gy-4">
                                <div class="col-md-2">
                                    <label>Page Group</label>
                                    <asp:DropDownList runat="server" class="form-control mb-2 mr-sm-2 " ID="ddlGroup">
                                        <asp:ListItem Value="">Group Name</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label>Show in Menu</label>
                                    <asp:DropDownList runat="server" class="form-control mb-2 mr-sm-2 basic" ID="ddlShow">
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label>Page Name</label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtName" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtName" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label>Page Link</label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtLink" />
                                </div>
                                <div class="col-md-3">
                                    <label>Page Desc</label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtDesc" />
                                </div>
                                <div class="col-md-3" style="display: none;">
                                    <label>Icon</label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtIcon" />
                                </div>

                                <div class="col-xxl-3 col-md-6">
                                    <div>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" CssClass="btn btn-primary waves-effect waves-light m-t-25" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-primary waves-effect waves-light m-t-25" OnClick="btnClear_Click" />
                                    </div>
                                </div>
                                <!--end col-->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1">Manage Page Master</h4>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination" class="table nowrap align-middle myTable" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Group</th>
                                        <th>Show In Menu</th>
                                        <th>Page Name</th>
                                        <th>Page Link</th>
                                        <th>Added On</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%=strPage %>
                                    <%--<tr>
                                        <td>01</td>
                                        <td>Bangalore
                                        </td>
                                        <td>23-Jun-2022
                                        </td>
                                        <td>
                                            <a href="#" class="fs-15 link-info"><i class="mdi mdi-pencil"></i></a>
                                            <a href="#" class="fs-15 link-danger"><i class="mdi mdi-trash-can-outline"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>02</td>
                                        <td>Delhi
                                        </td>
                                        <td>23-Jun-2022
                                        </td>
                                        <td>
                                            <a href="#" class="fs-15 link-info"><i class="mdi mdi-pencil"></i></a>
                                            <a href="#" class="fs-15 link-danger"><i class="mdi mdi-trash-can-outline"></i></a>
                                        </td>
                                    </tr>--%>
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/page-master.js"></script>
</asp:Content>

