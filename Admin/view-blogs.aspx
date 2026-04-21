<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="view-blogs.aspx.cs" Inherits="Admin_view_blogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">

            <div class="row mb-3">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Manage Blogs</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item active">Blogs</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <%--<div class="d-flex justify-content-end mb-3">
                        <a href="write-blog.aspx" class="btn btn-primary waves-effect waves-light">
                            <i class="mdi mdi-plus me-1"></i> Write Blog
                        </a>
                    </div>--%>
                    <div class="table-responsive">
                        <table class="table table-hover table-striped table-bordered dt-responsive align-middle myTable" style="width:100%;">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Blog Name</th>
                                    <th>Thumb Image</th>
                                    <th>Added On</th>
                                    <th class="text-center">Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=strBlogs %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/view-blogs.js"></script>
</asp:Content>