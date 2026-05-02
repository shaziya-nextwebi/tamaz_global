<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="report.aspx.cs" Inherits="Admin_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        sup {
            color: red !important;
        }

      
        .table td, .table th {
            white-space: nowrap;
            vertical-align: middle;
            font-size: 13px;
        }

  
        .badge-status {
            font-size: 11px;
            padding: 3px 8px;
            border-radius: 4px;
            font-weight: 600;
            display: inline-block;
        }

        .badge-confirmed {
            background: #d1fae5;
            color: #065f46;
        }

        .badge-pending {
            background: #fef9c3;
            color: #92400e;
        }

        .badge-cancelled {
            background: #fee2e2;
            color: #991b1b;
        }

        .badge-shipped {
            background: #e0f2fe;
            color: #075985;
        }

        .badge-delivered {
            background: #ede9fe;
            color: #5b21b6;
        }

        .badge-paid {
            background: #d1fae5;
            color: #065f46;
        }

        .badge-unpaid {
            background: #fee2e2;
            color: #991b1b;
        }

        .badge-partial {
            background: #e0f2fe;
            color: #075985;
        }

       
        .amount-cell {
            font-weight: 600;
            color: #0f172a;
        }

   
        .ship-name {
            font-weight: 600;
            color: #1e293b;
            font-size: 13px;
        }

        .ship-addr {
            font-size: 11px;
            color: #64748b;
            margin-top: 1px;
        }

        .ship-phone {
            font-size: 11px;
            color: #2563eb;
            margin-top: 2px;
        }

            .ship-phone i {
                font-size: 11px;
            }

 
        .item-count-badge {
            display: inline-block;
            background: #f1f5f9;
            color: #475569;
            border-radius: 12px;
            padding: 2px 10px;
            font-size: 11px;
            font-weight: 600;
            border: 1px solid #e2e8f0;
        }

        .action-col {
            min-width: 110px;
        }

        .action-btn-group {
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 4px;
        }

        .btn-action {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            width: 30px;
            height: 30px;
            border-radius: 6px;
            font-size: 16px;
            border: 1px solid transparent;
            transition: all .15s ease;
            text-decoration: none;
            cursor: pointer;
        }

        .btn-view {
            background: #eff6ff;
            color: #2563eb;
            border-color: #bfdbfe;
                margin-right: 3px;
        }

            .btn-view:hover {
                background: #2563eb;
                color: #fff;
            }

        .btn-status {
            background: #f0fdf4;
            color: #16a34a;
            border-color: #bbf7d0;
        }

            .btn-status:hover {
                background: #16a34a;
                color: #fff;
            }

        .btn-delete {
            background: #fff1f2;
            color: #e11d48;
            border-color: #fecdd3;
        }

            .btn-delete:hover {
                background: #e11d48;
                color: #fff;
            }

          .btn-deliver {
    background: #f0fdf4;
    color: #16a34a;
    border-color: #bbf7d0;
}

    .btn-deliver:hover {
        background: #16a34a;
        color: #fff;
    }
        /* ── Unified Order Detail Modal ──────────────────────────── */
        #orderDetailModal .modal-header {
            background: linear-gradient(135deg, #1e40af 0%, #1d4ed8 100%);
            color: #fff;
            border-bottom: none;
            padding: 16px 20px;
        }

        #orderDetailModal .modal-title {
            font-size: 16px;
            font-weight: 700;
        }

        #orderDetailModal .btn-close {
            filter: brightness(0) invert(1);
        }

        .od-section {
            border: 1px solid #e2e8f0;
            border-radius: 8px;
            overflow: hidden;
            margin-bottom: 16px;
        }

        .od-section-title {
            background: #f8fafc;
            border-bottom: 1px solid #e2e8f0;
            padding: 9px 14px;
            font-size: 12px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: .5px;
            color: #64748b;
            display: flex;
            align-items: center;
            gap: 6px;
        }

            .od-section-title i {
                font-size: 15px;
                color: #2563eb;
            }

        .od-body {
            padding: 14px;
        }

        .od-field {
            margin-bottom: 10px;
        }

            .od-field:last-child {
                margin-bottom: 0;
            }

        .od-label {
            font-size: 10px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: .5px;
            color: #94a3b8;
            margin-bottom: 2px;
        }

        .od-value {
            font-size: 13px;
            color: #1e293b;
            font-weight: 500;
        }

            .od-value.mono {
                font-family: 'Courier New', monospace;
                font-size: 12px;
                color: #475569;
            }

            .od-value a {
                color: #2563eb;
                text-decoration: none;
            }

                .od-value a:hover {
                    text-decoration: underline;
                }

        .od-amount-grid {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 10px;
        }

        .od-amount-box {
            background: #f8fafc;
            border: 1px solid #e2e8f0;
            border-radius: 6px;
            padding: 10px 12px;
            text-align: center;
        }

            .od-amount-box .lbl {
                font-size: 10px;
                color: #94a3b8;
                font-weight: 700;
                text-transform: uppercase;
                letter-spacing: .4px;
            }

            .od-amount-box .val {
                font-size: 15px;
                font-weight: 700;
                color: #1e293b;
                margin-top: 3px;
            }

            .od-amount-box.highlight {
                background: #eff6ff;
                border-color: #bfdbfe;
            }

                .od-amount-box.highlight .val {
                    color: #1d4ed8;
                }

        /* Items table inside modal */
        .od-items-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 12px;
        }

            .od-items-table th {
                background: #f1f5f9;
                padding: 8px 10px;
                font-weight: 700;
                color: #475569;
                border-bottom: 2px solid #e2e8f0;
                text-align: left;
            }

            .od-items-table td {
                padding: 8px 10px;
                border-bottom: 1px solid #f1f5f9;
                color: #334155;
                vertical-align: middle;
            }

            .od-items-table tr:last-child td {
                border-bottom: none;
            }

            .od-items-table tr:hover td {
                background: #f8fafc;
            }

        .od-item-img {
            width: 40px;
            height: 40px;
            object-fit: contain;
            border-radius: 4px;
            border: 1px solid #e2e8f0;
            background: #f8fafc;
        }

        .od-item-img-placeholder {
            width: 40px;
            height: 40px;
            border-radius: 4px;
            background: #f1f5f9;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            color: #94a3b8;
            font-size: 18px;
        }

        .od-items-table td.text-end {
            text-align: right;
        }

        .od-items-table td.text-center {
            text-align: center;
        }

        .table-responsive {
            overflow-x: auto;
            position: relative;
        }

        /* IMPORTANT: apply to specific class, NOT last-child */
        .action-col {
            position: sticky !important;
            right: 0;
            z-index: 5;
            background: #fff;
        }

        .table thead th {
            background: #f8fafc;
        }

        .table td.action-col,
        .table th.action-col {
            position: sticky;
            right: 0;
            z-index: 5;
            background: #fff;
        }
        /* header fix */
        .table thead th.action-col {
            z-index: 6;
            background: #f8fafc;
        }

        /* add separator shadow */
        .action-col::before {
            content: "";
            position: absolute;
            left: -5px;
            top: 0;
            width: 5px;
            height: 100%;
            box-shadow: -2px 0 5px rgba(0,0,0,0.08);
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
                        <h4 class="mb-sm-0">Orders Report</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item active">Orders Report</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-hover table-striped table-bordered  align-middle myTable" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Order</th>
                                    <th>Ship To</th>
                                    <th>Payment Type</th>
                                    <th>Grand Total</th>
                                    <th>Excl. Tax</th>
                                    <th>Tax Amt</th>
                                    <th>Payable Now</th>
                                    <th>COD Amt</th>
                                    <th>Order Status</th>
                                    <th>Payment Status</th>
                                    <th>Paid On</th>
                                    <%-- <th class="text-center">Items</th>--%>
                                    <th class="text-center action-col">Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%= strReport %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="modal fade" id="DispatchModal" data-orderguid="" tabindex="-1" aria-labelledby="exampleModalgridLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Update Dispatch Info</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <div class="row g-3">
                        <!--end col-->
                        <div class="col-xxl-12">
                            <div>
                                <label for="txtCourierName" class="form-label">Courier Name <sup>*</sup></label>
                                <input type="text" maxlength="100" class="form-control" id="txtCourierName">
                                <span class="error"></span>
                            </div>
                        </div>
                        <!--end col-->

                        <div class="col-xxl-12">
                            <label for="txtTrackingCode" class="form-label">Tracking Code <sup>*</sup></label>
                            <input type="text" maxlength="20" class="form-control" id="txtTrackingCode">
                            <span class="error"></span>

                        </div>

                        <!--end col-->
                        <div class="col-xxl-12">
                            <label for="txtTrackingLink" class="form-label">Tracking Link <sup>*</sup></label>
                            <input type="text" maxlength="20" class="form-control" id="txtTrackingLink">
                            <span class="error"></span>
                        </div>
                        <div class="col-xxl-12">
                            <div>
                                <label for="txtDate" class="form-label">Delivery Date<sup>*</sup></label>
                                <input type="date" class="form-control today_datepicker_with_time" id="txtDate">
                                <span class="error"></span>
                            </div>
                        </div>
                        <!--end col-->

                        <div class="col-lg-12">

                            <div class="hstack gap-2 justify-content-end">
                                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                                <button type="button" id="btnDispatch" class="btn btn-primary">Submit</button>
                            </div>
                        </div>
                    </div>
                    <!--end col-->
                </div>
                <!--end row-->

            </div>
        </div>
    </div>

    <div class="modal fade" id="shippingModal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Shipping Details</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body" id="shippingBody"></div>
            </div>
        </div>
    </div>

    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/Reports.js"></script>
</asp:Content>
