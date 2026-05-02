<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="write-blog.aspx.cs" ValidateRequest="false" Inherits="write_blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <style>
        sup { color: red !important; }
        .select2-container--default .select2-selection--multiple {
            border: 1px solid #ced4da;
            border-radius: 4px;
            min-height: 38px;
        }
        .char-count { font-size: 12px; margin-top: 3px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">

            <%-- Page Title --%>
            <div class="row mb-3">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0"><%=Request.QueryString["id"] != null ? "Update" : "Write" %> Blog</h4>
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="view-blogs.aspx">Blogs</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"] != null ? "Update" : "Write" %> Blog</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <%-- Blog Name --%>
                        <div class="col-lg-6 mb-3">
                            <label class="form-label">Blog Name <sup>*</sup></label>
                            <asp:TextBox runat="server" ID="txtBlogName" MaxLength="100"
                                CssClass="form-control txtName"
                                placeholder="Enter Blog Name" />
                            <asp:RequiredFieldValidator ID="req1" runat="server"
                                ControlToValidate="txtBlogName" Display="Dynamic"
                                ForeColor="Red" SetFocusOnError="true"
                                ValidationGroup="Save" ErrorMessage="Field can't be empty" />
                        </div>

                        <%-- Blog URL --%>
                        <div class="col-lg-6 mb-3">
                            <label class="form-label">Blog URL <sup>*</sup></label>
                            <asp:TextBox runat="server" ID="txtURL" MaxLength="100"
                                CssClass="form-control txtURL"
                                placeholder="Auto-Generated" />
                            <asp:RequiredFieldValidator ID="rfvURL" runat="server"
                                ControlToValidate="txtURL" Display="Dynamic"
                                ForeColor="Red" SetFocusOnError="true"
                                ValidationGroup="Save" ErrorMessage="Field can't be empty" />
                        </div>

                        <%-- Blog Tags --%>
                        <div class="col-lg-4 mb-3">
                            <label class="form-label">Blog Tags</label>
                            <select id="lstTagsJs" name="lstTagsJs" class="form-control select2-multi" multiple="multiple" style="width:100%;">
                                <%=strTagOptions %>
                            </select>
                            <asp:HiddenField ID="hdnBlogTags" runat="server" />
                        </div>

                        <%-- Posted By --%>
                        <div class="col-lg-4 mb-3">
                            <label class="form-label">Posted By</label>
                            <asp:TextBox runat="server" ID="txtPostedBy" MaxLength="30"
                                CssClass="form-control"
                                placeholder="Author name" />
                        </div>

                        <%-- Posted On --%>
                        <div class="col-lg-4 mb-3">
                            <label class="form-label">Posted On</label>
                            <asp:TextBox runat="server" ID="txtDate" MaxLength="100"
                                CssClass="form-control"
                                placeholder="dd/MMM/yyyy" />
                        </div>

                        <%-- Page Title --%>
                        <div class="col-lg-4 mb-3">
                            <label class="form-label">Page Title</label>
                            <asp:TextBox runat="server" ID="txtPageTitle" TextMode="MultiLine"
                                CssClass="form-control textcount1" Rows="2"
                                data-id="Title" data-max="60"
                                placeholder="Page Title" />
                            <small class="char-count" id="spTitle"></small>
                        </div>

                        <%-- Meta Description --%>
                        <div class="col-lg-4 mb-3">
                            <label class="form-label">Meta Description</label>
                            <asp:TextBox runat="server" ID="txtMetaDesc" TextMode="MultiLine"
                                CssClass="form-control textcount1" Rows="2"
                                data-id="MetaDesc" data-max="160"
                                placeholder="Meta Description" />
                            <small class="char-count" id="spMetaDesc"></small>
                        </div>

                        <%-- Meta Keys --%>
                        <div class="col-lg-4 mb-3">
                            <label class="form-label">Meta Keywords</label>
                            <asp:TextBox runat="server" ID="txtMetaKeys" TextMode="MultiLine"
                                CssClass="form-control" Rows="2"
                                placeholder="Meta Keywords" />
                        </div>

                        <%-- Short Desc (hidden) --%>
                        <div style="display:none;">
                            <asp:TextBox runat="server" ID="txtShortDesc" TextMode="MultiLine" CssClass="form-control" />
                        </div>

                        <%-- Full Description --%>
                        <div class="col-lg-12 mb-3">
                            <label class="form-label">Full Description <sup>*</sup></label>
                                <button type="button" class="clean-html-btn"
        data-editor="<%=txtDesc.ClientID%>">
        ✦ Clean HTML
    </button>
                            <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine"
                                CssClass="form-control summernote" />
                            <asp:RequiredFieldValidator ID="rfvDesc" runat="server"
                                ControlToValidate="txtDesc" Display="Dynamic"
                                ForeColor="Red" SetFocusOnError="true"
                                ValidationGroup="Save" ErrorMessage="Field can't be empty" />
                        </div>

                        <%-- Thumb Image --%>
                        <div class="col-lg-4 mb-3">
                            <label class="form-label">Thumb Image <sup>*</sup></label>
                            <asp:FileUpload ID="fuIndSamll" runat="server" CssClass="form-control" />
                            <small class="text-danger">.png .jpeg .jpg .gif .webp — 580px × 390px</small>
                            <div class="mt-2"><%=strBannerImage %></div>
                            <asp:Label ID="lblIndustrialInternImage1" runat="server" Visible="false" />
                        </div>

                        <%-- Detail Image --%>
                        <div class="col-lg-4 mb-3">
                            <label class="form-label">Detail Image</label>
                            <asp:FileUpload ID="fuIndThumb" runat="server" CssClass="form-control" />
                            <small class="text-danger">.png .jpeg .jpg .gif .webp — 930px × 500px</small>
                            <div class="mt-2"><%=strMobileImage %></div>
                            <asp:Label ID="lblIndustrialInternImage2" runat="server" Visible="false" />
                        </div>

                        <%-- Save Button --%>
                        <div class="col-lg-12 mt-2">
                            <p style="color:red; font-weight:bold;">Note : <sup>*</sup> are required fields</p>
                            <asp:Button runat="server" ID="btnSave"
                                CssClass="btn btn-primary waves-effect waves-light"
                                Text="Save"
                                ValidationGroup="Save"
                                OnClientClick="tinyMCE.triggerSave(false,true); prepareTags();"
                                OnClick="btnSave_Click" />
                        </div>

                    </div>
                </div>
            </div>

        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        $(document).ready(function () {
            $(document).on('click', '.clean-html-btn', function () {
                var editorId = $(this).data('editor');
                cleanEditor(editorId);
            });
            // Select2 for tags
            $('.select2-multi').select2({
                placeholder: "Select blog tags",
                allowClear: true
            });

            // Pre-select existing tags from hidden field
            var existing = $('#<%=hdnBlogTags.ClientID %>').val();
            if (existing) {
                var vals = existing.split('|').filter(Boolean);
                $('.select2-multi').val(vals).trigger('change');
            }

            // Auto-generate URL from blog name
            $(".txtName").on("keyup", function () {
                $(".txtURL").val($(this).val().toLowerCase()
                    .replace(/[\.\/\\\*\?\~]/g, '')
                    .replace(/\&/g, 'and')
                    .replace(/\s+/g, '-'));
            });

            // Character counters
            $(".textcount1").on("keyup", function () {
                var elem = $(this);
                var type = elem.attr("data-id");
                var max  = parseInt(elem.attr("data-max"));
                var len  = elem.val().length;
                var spId = type === "Title" ? "#spTitle" : "#spMetaDesc";
                $(spId).text("Characters: " + len + " / " + max);
                $(spId).css("color", len > max ? "red" : "green");
            });
        });
        function cleanEditor(editorId) {

            var editor = null;
            if (typeof tinymce !== 'undefined') {
                editor = tinymce.get(editorId);
                if (!editor) {
                    tinymce.editors.forEach(function (ed) {
                        if (ed.id === editorId || ed.id.indexOf(editorId) !== -1) {
                            editor = ed;
                        }
                    });
                }
            }

            var raw = editor ? editor.getContent() : $('#' + editorId).val();

            if (!raw || raw.trim() === '') {
                Swal.fire('Info', 'Editor is already empty.', 'info');
                return;
            }

            Swal.fire({
                title: 'Cleaning HTML...',
                text: 'AI is cleaning your content, please wait.',
                allowOutsideClick: false,
                allowEscapeKey: false,
                didOpen: function () { Swal.showLoading(); }
            });

            fetch('/clean-html.ashx', {
                method: 'POST',
                headers: { 'Content-Type': 'text/plain; charset=utf-8' },
                body: raw
            })
                .then(function (response) {
                    return response.json();
                })
                .then(function (data) {
                    if (data.error) {
                        throw new Error(data.error);
                    }

                    var cleanedHtml = data.html.trim();

                    if (editor) {
                        editor.setContent(cleanedHtml);
                    } else {
                        $('#' + editorId).val(cleanedHtml);
                    }

                    Swal.fire({
                        icon: 'success',
                        title: 'HTML cleaned!',
                        text: 'AI has cleaned your content successfully.',
                        timer: 1800,
                        showConfirmButton: false
                    });
                })
                .catch(function (err) {
                    console.error('cleanEditor error:', err);
                    Swal.fire('Error!', 'AI cleaning failed: ' + err.message, 'error');
                });
        }
        function prepareTags() {
            var selected = $('.select2-multi').val();
            if (selected && selected.length > 0)
                $('#<%=hdnBlogTags.ClientID %>').val(selected.join('|'));
        }

        <%if (!string.IsNullOrEmpty(strSuccessMsg)) { %>
        Swal.fire({ icon: 'success', title: '<%=strSuccessMsg %>', timer: 2000, showConfirmButton: false });
        <%} %>
        <%if (!string.IsNullOrEmpty(strErrorMsg)) { %>
        Swal.fire({ icon: 'error', title: '<%=strErrorMsg %>' });
        <%} %>
    </script>
</asp:Content>