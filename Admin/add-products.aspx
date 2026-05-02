<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="add-products.aspx.cs" Inherits="Admin_add_products" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        sup {
            color: red !important;
        }

        .tab-nav {
            display: flex;
            gap: 4px;
            border-bottom: 2px solid #ddd;
            margin-bottom: 24px;
            justify-content: center;
        }

        .tab-btn {
            padding: 10px 28px;
            border: none;
            background: #f1f1f1;
            border-radius: 6px 6px 0 0;
            cursor: pointer;
            font-weight: 500;
            color: #555;
            font-size: 15px;
        }

            .tab-btn.active {
                background: #0b3ab8;
                color: #fff;
            }

        .tab-pane {
            display: none;
        }

            .tab-pane.active {
                display: block;
            }

        .gallery-grid {
            display: flex;
            flex-wrap: wrap;
            gap: 16px;
            margin-top: 20px;
        }

        .gallery-item {
            position: relative;
            width: 220px;
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            overflow: hidden;
            background: #fff;
            cursor: grab;
        }

            .gallery-item img {
                width: 100%;
                height: 160px;
                object-fit: cover;
                display: block;
            }

            .gallery-item .del-btn {
                display: flex;
                justify-content: center;
                align-items: center;
                padding: 6px;
                background: #f8f8f8;
                border-top: 1px solid #eee;
            }

                .gallery-item .del-btn a {
                    color: #e53935;
                    font-size: 20px;
                }

                    .gallery-item .del-btn a:hover {
                        color: #b71c1c;
                    }

        .clean-html-btn {
            font-size: 12px;
            padding: 3px 10px;
            margin-left: 8px;
            vertical-align: middle;
            cursor: pointer;
            background: #f1f1f1;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

            .clean-html-btn:hover {
                background: #e0e0e0;
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
                        <h4 class="mb-sm-0"><%=Request.QueryString["id"] != null ? "Update" : "Add New" %> Product</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="view-products.aspx">Products</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"] != null ? "Update" : "Add" %> Product</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">

                    <%-- Tabs — only show when editing --%>
                    <%if (Request.QueryString["id"] != null)
                        { %>
                    <div class="tab-nav">
                        <button type="button" class="tab-btn active" onclick="switchTab('general',this)">General Info</button>
                        <button type="button" class="tab-btn" onclick="switchTab('seo',this)">SEO</button>
                        <button type="button" class="tab-btn" onclick="switchTab('gallery',this)">Gallery</button>
                        <button type="button" class="tab-btn" onclick="switchTab('faqs',this)">FAQ's</button>
                    </div>
                    <%} %>

                    <%-- ===== GENERAL INFO ===== --%>
                    <div id="tab-general" class="tab-pane active">
                        <div class="row">

                            <div class="col-lg-3 mb-3">
                                <label class="form-label">Category <sup>*</sup></label>
                                <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-select">
                                    <asp:ListItem Value="0">Select Category</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCategory" runat="server"
                                    ControlToValidate="ddlCategory" InitialValue="0"
                                    Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                    ValidationGroup="Save" ErrorMessage="Please select a category" />
                            </div>

                            <div class="col-lg-3 mb-3">
                                <label class="form-label">Brand</label>
                                <asp:TextBox runat="server" ID="txtBrandName"
                                    CssClass="form-control txtBrandName"
                                    placeholder="Enter Brand Name" />
                            </div>

                            <div class="col-lg-3 mb-3">
                                <label class="form-label">Product Availability <sup>*</sup></label>
                                <asp:DropDownList runat="server" ID="ddlavalibality" CssClass="form-select">
                                    <asp:ListItem Value="0">Select Availability</asp:ListItem>
                                    <asp:ListItem>Available</asp:ListItem>
                                    <asp:ListItem>Coming Soon</asp:ListItem>
                                    <asp:ListItem>Out of Stock</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAvail" runat="server"
                                    ControlToValidate="ddlavalibality" InitialValue="0"
                                    Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                    ValidationGroup="Save" ErrorMessage="Please select availability" />
                            </div>

                            <div class="col-lg-3 mb-3">
                                <label class="form-label">Product Label</label>
                                <asp:DropDownList runat="server" ID="ddllabel" CssClass="form-select">
                                    <asp:ListItem Value="0">Select Label</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-6 mb-3">
                                <label class="form-label">Product Name <sup>*</sup></label>
                                <asp:TextBox runat="server" ID="txtProdName" MaxLength="150"
                                    CssClass="form-control txtProdName"
                                    placeholder="Enter Product Name" />
                                <asp:RequiredFieldValidator ID="rfvProdName" runat="server"
                                    ControlToValidate="txtProdName" Display="Dynamic"
                                    ForeColor="Red" ValidationGroup="Save"
                                    ErrorMessage="Field can't be empty" />
                            </div>

                            <div class="col-lg-6 mb-3">
                                <label class="form-label">Product URL <sup>*</sup></label>
                                <asp:TextBox runat="server" ID="txtURL" MaxLength="150"
                                    CssClass="form-control txtURL"
                                    placeholder="Auto-Generated" />
                                <asp:RequiredFieldValidator ID="rfvURL" runat="server"
                                    ControlToValidate="txtURL" Display="Dynamic"
                                    ForeColor="Red" ValidationGroup="Save"
                                    ErrorMessage="Field can't be empty" />
                            </div>

                            <div class="col-lg-4 mb-3">
                                <label class="form-label">Place Of Origin</label>
                                <asp:TextBox runat="server" ID="txtPlaceOrigin" MaxLength="30"
                                    CssClass="form-control"
                                    placeholder="e.g. Kerala, India" />
                            </div>

                            <div class="col-lg-4 mb-3">
                                <label class="form-label">Retail Price</label>
                                <asp:TextBox runat="server" ID="txtRetailPrice" MaxLength="8"
                                    CssClass="form-control"
                                    onkeypress="return isNumber(event)"
                                    placeholder="Enter Retail Price" />
                            </div>

                            <div class="col-lg-4 mb-3">
                                <label class="form-label">Key Ingredients</label>
                                <asp:TextBox runat="server" ID="txtKeyIndred"
                                    CssClass="form-control"
                                    placeholder="Comma separated" />
                            </div>

                            <div class="col-lg-12 mb-3">
                                <label class="form-label">Product Description</label>
                                <button type="button" class="clean-html-btn"
                                    data-editor="<%=txtFullDesc.ClientID%>">
                                    ✦ Clean HTML
                                </button>
                                <asp:TextBox runat="server" ID="txtFullDesc" TextMode="MultiLine"
                                    CssClass="form-control summernote" />
                            </div>

                            <div class="col-lg-12 mb-3">
                                <label class="form-label">Benefits Description</label>
                                <button type="button" class="clean-html-btn"
                                    data-editor="<%=txtBrnfdesc.ClientID%>">
                                    ✦ Clean HTML
                                </button>
                                <asp:TextBox runat="server" ID="txtBrnfdesc" TextMode="MultiLine"
                                    CssClass="form-control summernote" />
                            </div>

                            <div class="col-lg-6 mb-3">
                                <label class="form-label">Ingredients Description</label>
                                <button type="button" class="clean-html-btn"
                                    data-editor="<%=txtIngDesc.ClientID%>">
                                    ✦ Clean HTML
                                </button>
                                <asp:TextBox runat="server" ID="txtIngDesc" TextMode="MultiLine"
                                    CssClass="form-control summernote" />
                            </div>

                            <div class="col-lg-6 mb-3">
                                <label class="form-label">Usage Description</label>
                                <button type="button" class="clean-html-btn"
                                    data-editor="<%=txtUsgDesc.ClientID%>">
                                    ✦ Clean HTML
                                </button>
                                <asp:TextBox runat="server" ID="txtUsgDesc" TextMode="MultiLine"
                                    CssClass="form-control summernote" />
                            </div>

                            <div class="col-lg-6 mb-3">
                                <label class="form-label">Thumb Image <sup>*</sup></label>
                                <asp:FileUpload ID="fuIndSamll" runat="server" CssClass="form-control" />
                                <small class="text-danger">.png .jpeg .jpg .gif .webp — 1000px × 1000px</small>
                                <asp:RequiredFieldValidator ID="ReqFileupload1" runat="server"
                                    ControlToValidate="fuIndSamll" Display="Dynamic"
                                    ForeColor="Red" SetFocusOnError="true"
                                    ValidationGroup="Save" ErrorMessage="Please upload thumb image" />
                                <div class="mt-2"><%=strBannerImage %></div>
                            </div>

                            <div class="col-lg-3 mb-3 d-flex align-items-end">
                                <div class="form-check form-switch form-switch-md mb-2">
                                    <asp:CheckBox runat="server" ID="chbDispHome" CssClass="form-check-input" />
                                    <label class="form-check-label" for="<%=chbDispHome.ClientID %>">Active / Publish</label>
                                </div>
                            </div>

                            <div class="col-lg-12 mt-2">
                                <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
                                <asp:Button runat="server" ID="btnSave"
                                    CssClass="btn btn-primary waves-effect waves-light"
                                    Text="Save" ValidationGroup="Save"
                                    OnClientClick="tinyMCE.triggerSave(false,true);"
                                    OnClick="btnSave_Click" />
                                <asp:Label ID="lblIndustrialInternImage1" runat="server" Visible="false" />
                                <input type="hidden" id="idPid" runat="server" />
                            </div>

                        </div>
                    </div>
                    <%-- end General --%>

                    <%if (Request.QueryString["id"] != null)
                        { %>

                    <%-- ===== SEO ===== --%>
                    <div id="tab-seo" class="tab-pane">
                        <div class="row">
                            <div class="col-lg-12 mb-3">
                                <label class="form-label">Page Title</label>
                                <asp:TextBox runat="server" ID="txtPTitle"
                                    CssClass="form-control"
                                    placeholder="Page Title" />
                            </div>
                            <div class="col-lg-12 mb-3">
                                <label class="form-label">Meta Keys</label>
                                <asp:TextBox runat="server" ID="txtMKeys"
                                    CssClass="form-control"
                                    placeholder="Meta Keywords" />
                            </div>
                            <div class="col-lg-12 mb-3">
                                <label class="form-label">Meta Description</label>
                                <asp:TextBox runat="server" ID="txtMetaDesc" TextMode="MultiLine"
                                    CssClass="form-control" Rows="3"
                                    placeholder="Meta Description" />
                            </div>
                            <div class="col-lg-12">
                                <asp:Button runat="server" ID="btnSeo"
                                    CssClass="btn btn-primary waves-effect waves-light"
                                    Text="Save SEO" CausesValidation="false"
                                    OnClick="btnSeo_Click" />
                            </div>
                        </div>
                    </div>
                    <%-- end SEO --%>

                    <%-- ===== GALLERY ===== --%>
                    <div id="tab-gallery" class="tab-pane">
                        <div class="d-flex align-items-center gap-3 flex-wrap mb-2">
                            <input type="file" id="fileUp" class="form-control" style="max-width: 400px;" multiple />
                            <button type="button" id="btnSaveGallery" class="btn btn-primary">Upload</button>
                            <input type="hidden" id="pid" value="<%=Request.QueryString["id"] %>" />
                        </div>
                        <small class="text-danger">.png .jpeg .jpg .webp — 1000px × 1000px</small>

                        <div class="gallery-grid" id="galleryGrid">
                        </div>

                        <div class="mt-3">
                            <button type="button" class="btn btn-success" id="UpdateImgOrder">
                                Update Image Order
                            </button>
                        </div>
                    </div>
                    <%-- end Gallery --%>

                    <%-- ===== FAQs ===== --%>
                    <div id="tab-faqs" class="tab-pane">
                        <div class="row">
                            <div class="col-lg-12 mb-3">
                                <label class="form-label">Question <sup>*</sup></label>
                                <asp:TextBox runat="server" ID="txtQues" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvQues" runat="server"
                                    ControlToValidate="txtQues" Display="Dynamic"
                                    ForeColor="Red" ValidationGroup="SaveFAQ"
                                    ErrorMessage="Field can't be empty" />
                            </div>
                            <div class="col-lg-12 mb-3">
                                <label class="form-label">Answer <sup>*</sup></label>
                                <button type="button" class="clean-html-btn"
                                    data-editor="<%=txtAnswer.ClientID%>">
                                    ✦ Clean HTML
                                </button>
                                <asp:TextBox runat="server" ID="txtAnswer" TextMode="MultiLine"
                                    CssClass="form-control summernote" />
                                <asp:RequiredFieldValidator ID="rfvAnswer" runat="server"
                                    ControlToValidate="txtAnswer" Display="Dynamic"
                                    ForeColor="Red" ValidationGroup="SaveFAQ"
                                    ErrorMessage="Field can't be empty" />
                            </div>
                            <div class="col-lg-12 mb-3">
                                <asp:Label ID="lblFaqId" runat="server" Visible="false" />
                                <asp:Button runat="server" ID="btnFAQ"
                                    CssClass="btn btn-primary me-2"
                                    Text="Save FAQ" ValidationGroup="SaveFAQ"
                                    OnClick="btnFAQ_Click" />
                                <asp:Button runat="server" ID="btnClearFAQ"
                                    CssClass="btn btn-outline-danger"
                                    Text="Clear" CausesValidation="false"
                                    OnClick="btnClearFAQ_Click" />
                            </div>
                            <div class="col-lg-12 mt-3">
                                <table class="table dt-responsive align-middle table-striped table-bordered"
                                    style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Question</th>
                                            <th>Answer</th>
                                            <th>Added On</th>
                                            <th class="text-center">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyFAQ">
                                        <%=strProdFaqs %>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <%-- end FAQs --%>

                    <%} %>
                </div>
            </div>

        </div>
    </div>

    <input type="hidden" id="TourId" value="<%=strTourId %>" />
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/add-products.js"></script>

</asp:Content>
