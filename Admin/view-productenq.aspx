<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="view-productenq.aspx.cs" Inherits="Admin_view_productenq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        sup {
            color: red !important;
        }

        .msg-cell {
            display: flex;
            align-items: center;
            gap: 6px;
            min-width: 160px;
        }

        .message-preview {
            max-width: 130px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            flex: 1;
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
                        <h4 class="mb-sm-0">Product Enquiries</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item active">Product Enquiries</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">

                    <%-- Search / Filter Bar --%>
                    <div class="row mb-3 g-2 align-items-end">
                        <div class="col-lg-4 col-md-6">
                            <label class="form-label">Search</label>
                            <asp:TextBox runat="server" ID="txtSearch" MaxLength="100"
                                CssClass="form-control"
                                placeholder="Search by Product / Type / Name" />
                        </div>
                        <div class="col-lg-2 col-md-4">
                            <label class="form-label">From Date</label>
                            <asp:TextBox runat="server" ID="txtFrom"
                                CssClass="form-control"
                                placeholder="dd/MMM/yyyy" />
                        </div>
                        <div class="col-lg-2 col-md-4">
                            <label class="form-label">To Date</label>
                            <asp:TextBox runat="server" ID="txtTo"
                                CssClass="form-control"
                                placeholder="dd/MMM/yyyy" />
                        </div>
                        <div class="col-lg-auto">
                            <asp:Button runat="server" ID="btnSearch"
                                CssClass="btn btn-primary waves-effect waves-light"
                                Text="Search" CausesValidation="false"
                                OnClick="btnSearch_Click" />
                            <asp:Button runat="server" ID="btnReset"
                                CssClass="btn btn-outline-secondary ms-1"
                                Text="Reset" CausesValidation="false"
                                OnClick="btnReset_Click" />
                        </div>
                        <div class="col-lg-auto ms-auto">
                            <asp:Button runat="server" ID="btnExport"
                                CssClass="btn btn-success waves-effect waves-light"
                                Text="Export to Excel" CausesValidation="false"
                                OnClick="btnExport_Click" />
                        </div>
                    </div>

                    <%-- Bulk Delete + Count --%>
                    <div class="d-flex align-items-center justify-content-between mb-2">
                        <p class="text-muted mb-0">
                            Showing <strong><%=intResultCount %></strong> record(s)
                        </p>
                        <button type="button" id="btnBulkDelete"
                            class="btn btn-danger btn-sm waves-effect waves-light">
                            <i class="mdi mdi-trash-can-outline me-1"></i>Delete Selected
                        </button>
                    </div>

                    <%-- Table --%>
                    <div class="table-responsive">
                        <table class="table table-hover table-striped table-bordered dt-responsive align-middle" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th style="width: 40px;">
                                        <input type="checkbox" id="chkAll" class="form-check-input" />
                                    </th>
                                    <th>#</th>
                                    <th>Enquiry Type</th>
                                    <th>Product Name</th>
                                    <th>Name</th>
                                    <th>Email</th>
                                    <th>Mobile</th>
                                    <th>City</th>
                                    <th>Message</th>
                                    <th>Source Page</th>
                                    <th>Added On</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=strEnquiries %>
                            </tbody>
                        </table>
                    </div>

                    <%-- Pagination --%>
                    <%if (intTotalPages > 1)
                        { %>
                    <nav class="mt-3">
                        <ul class="pagination pagination-sm justify-content-end flex-wrap">
                            <%=strPagination %>
                        </ul>
                    </nav>
                    <%} %>
                </div>
            </div>

        </div>
    </div>

    <%-- View Message Modal --%>
    <div class="modal fade" id="msgModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Enquiry from <span id="modalSenderName"></span></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">Name</div>
                        <div class="col-sm-8" id="mdName"></div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">Email</div>
                        <div class="col-sm-8" id="mdEmail"></div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">Mobile</div>
                        <div class="col-sm-8" id="mdMobile"></div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">City</div>
                        <div class="col-sm-8" id="mdCity"></div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">Product</div>
                        <div class="col-sm-8" id="mdProduct"></div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">Enquiry Type</div>
                        <div class="col-sm-8" id="mdType"></div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">Source Page</div>
                        <div class="col-sm-8">
                            <a id="mdSource" href="#" target="_blank"></a>
                        </div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-sm-4 text-muted fw-bold">Received On</div>
                        <div class="col-sm-8" id="mdDate"></div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-sm-4 text-muted fw-bold">Message</div>
                        <div class="col-sm-8">
                            <div id="mdMessage"
                                style="white-space: pre-wrap; background: #f8f9fa; border: 1px solid #e0e0e0; border-radius: 6px; padding: 14px; min-height: 80px; font-size: 14px; line-height: 1.7;">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <%-- Hidden field to carry checked IDs for bulk delete --%>
    <asp:HiddenField ID="hdnDeleteIds" runat="server" />
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {

            // Initialize date pickers
            flatpickr("#<%= txtFrom.ClientID %>", {
                dateFormat: "d/M/Y",
                allowInput: true
            });

            flatpickr("#<%= txtTo.ClientID %>", {
                dateFormat: "d/M/Y",
                allowInput: true
            });

        });
        $(document).ready(function () {

            // Select All checkbox
            $('#chkAll').on('change', function () {
                $('.rowChk').prop('checked', $(this).prop('checked'));
            });

            // View message modal
            $(document).on('click', '.viewMsg', function () {
                var btn = $(this);
                $('#mdName').text(btn.attr('data-name'));
                $('#mdEmail').text(btn.attr('data-email'));
                $('#mdMobile').text(btn.attr('data-mobile'));
                $('#mdCity').text(btn.attr('data-city'));
                $('#mdProduct').text(btn.attr('data-product'));
                $('#mdType').text(btn.attr('data-type'));
                $('#mdDate').text(btn.attr('data-date'));
                $('#mdSource').text(btn.attr('data-source')).attr('href', btn.attr('data-source'));
                $('#mdMessage').html(btn.attr('data-message'));
                $('#modalSenderName').text(btn.attr('data-name'));
                new bootstrap.Modal(document.getElementById('msgModal')).show();
            });

            // Bulk delete
            $('#btnBulkDelete').on('click', function () {
                var ids = [];
                $('.rowChk:checked').each(function () {
                    ids.push($(this).attr('data-id'));
                });
                if (ids.length === 0) {
                    Swal.fire('Warning', 'Please select at least one record to delete.', 'warning');
                    return;
                }
                Swal.fire({
                    title: 'Delete ' + ids.length + ' record(s)?',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#d33',
                    confirmButtonText: 'Delete'
                }).then(function (result) {
                    if (result.isConfirmed) {
                        $('#<%=hdnDeleteIds.ClientID %>').val(ids.join(','));
                        __doPostBack('btnBulkDeleteServer', '');
                    }
                });
            });

        });

        <%if (!string.IsNullOrEmpty(strSuccessMsg))
        { %>
        Swal.fire({ icon: 'success', title: '<%=strSuccessMsg %>', timer: 2000, showConfirmButton: false });
        <%} %>
        <%if (!string.IsNullOrEmpty(strErrorMsg))
        { %>
        Swal.fire({ icon: 'error', title: '<%=strErrorMsg %>' });
        <%} %>
    </script>
</asp:Content>
