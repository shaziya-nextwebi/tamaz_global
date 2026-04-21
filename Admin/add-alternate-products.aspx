<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="add-alternate-products.aspx.cs" Inherits="add_alternate_products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
<style>
    sup { color: red !important; }
    .select2-container--default .select2-selection--multiple {
        border: 1px solid #ced4da;
        border-radius: 4px;
        min-height: 38px;
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
                        <h4 class="mb-sm-0">Alternative Products</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item active">Alternative Products</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <div class="col-lg-6 mb-3">
                            <label class="form-label">Select Product <sup>*</sup></label>
                            <asp:DropDownList runat="server" ID="ddlProduct"
                                CssClass="form-select"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Product</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvProduct" runat="server"
                                ControlToValidate="ddlProduct" InitialValue="0"
                                Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                ValidationGroup="Save" ErrorMessage="Please select a product" />
                        </div>

                        <%--                <div class="col-lg-6 mb-3">
                            <label class="form-label">Map Products <sup>*</sup></label>
                            <asp:ListBox ID="chkMapProducts" runat="server"
                                CssClass="form-control"
                                SelectionMode="Multiple"
                                Rows="8" />
                            <small class="text-muted">Hold Ctrl / Cmd to select multiple</small>
                            <asp:RequiredFieldValidator ID="rfvMapProducts" runat="server"
                                ControlToValidate="chkMapProducts" InitialValue="0"
                                Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                ValidationGroup="Save" ErrorMessage="Please select at least one product" />
                        </div>--%>
                        <div class="col-lg-6 mb-3">
                            <label class="form-label">Map Products <sup>*</sup></label>
                            <select id="chkMapProductsJs" name="chkMapProductsJs" class="form-control select2-multi" multiple="multiple" style="width: 100%;">
                                <%=strMapProductOptions %>
                            </select>
                            <%-- Hidden field to carry selected values on postback --%>
                            <asp:HiddenField ID="hdnMappedProducts" runat="server" />
                            <div id="rfvMap" class="text-danger" style="display: none; font-size: 13px;">Please select at least one product</div>
                        </div>

                        <div class="col-lg-12 mt-2">
                            <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
                            <asp:Button runat="server" ID="btnSave"
                                CssClass="btn btn-primary waves-effect waves-light"
                                Text="Save Details"
                                OnClientClick="return prepareAndValidate();"
                                OnClick="btnSave_Click" />
                        </div>

                    </div>
                </div>
            </div>

        </div>
    </div>

<script>
    $(document).ready(function () {
        $('.select2-multi').select2({
            placeholder: "Select products to map",
            allowClear: true
        });

        // Pre-select already mapped values from hidden field
        var existing = $('#<%=hdnMappedProducts.ClientID %>').val();
        if (existing) {
            var vals = existing.split('|').filter(Boolean);
            $('.select2-multi').val(vals).trigger('change');
        }
    });

    function prepareAndValidate() {
        var selected = $('.select2-multi').val();
        if (!selected || selected.length === 0) {
            $('#rfvMap').show();
            return false;
        }
        $('#rfvMap').hide();
        // Pack selected IDs pipe-separated into hidden field
        $('#<%=hdnMappedProducts.ClientID %>').val(selected.join('|'));
        return true;
    }

    <%if (!string.IsNullOrEmpty(strSuccessMsg)) { %>
    Swal.fire({ icon: 'success', title: '<%=strSuccessMsg %>', timer: 2000, showConfirmButton: false });
    <%} %>
    <%if (!string.IsNullOrEmpty(strErrorMsg)) { %>
    Swal.fire({ icon: 'error', title: '<%=strErrorMsg %>' });
    <%} %>
</script>
</asp:Content>
