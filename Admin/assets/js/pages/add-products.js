// Tab switching
function switchTab(name, btn) {
    document.querySelectorAll('.tab-pane').forEach(p => p.classList.remove('active'));
    document.querySelectorAll('.tab-btn').forEach(b => b.classList.remove('active'));
    document.getElementById('tab-' + name).classList.add('active');
    btn.classList.add('active');
}

$(document).ready(function () {
    $(document).on('click', '.clean-html-btn', function () {
        var editorId = $(this).data('editor');
        cleanEditor(editorId);
    });
    // ------------------------------------------------------------------
    // Restore active tab from URL ?tab= param (e.g. after FAQ save)
    // ------------------------------------------------------------------
    var urlParams = new URLSearchParams(window.location.search);
    var tabParam = urlParams.get('tab');
    if (tabParam) {
        var tabBtn = document.querySelector('.tab-btn[onclick*="' + tabParam + '"]');
        if (tabBtn) switchTab(tabParam, tabBtn);
    }

    // ------------------------------------------------------------------
    // Auto URL slug from product name
    // ------------------------------------------------------------------
    $(".txtProdName").on("keyup", function () {
        $(".txtURL").val($(this).val().toLowerCase()
            .replace(/[\.\/\\\*\?\~]/g, '')
            .replace(/\&/g, 'and')
            .replace(/\s+/g, '-'));
    });

    // ------------------------------------------------------------------
    // Brand autocomplete
    // Safe-guarded: only runs if jQuery UI is loaded
    // ------------------------------------------------------------------
    if ($.fn.autocomplete) {
        $(".txtBrandName").autocomplete({
            minLength: 1,
            source: function (request, response) {
                $.ajax({
                    type: 'POST',
                    url: 'add-products.aspx/GetBrandAutoComplete',
                    data: JSON.stringify({ sName: request.term }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) { response(data.d); }
                });
            },
            focus: function (e, ui) { $(".txtBrandName").val(ui.item.BrandName); return false; },
            select: function (e, ui) { $(".txtBrandName").val(ui.item.BrandName); return false; }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>").append("<a>" + item.BrandName + "</a>").appendTo(ul);
        };
    }

    // ------------------------------------------------------------------
    // Gallery — init SortableJS + load existing images
    // Safe-guarded: only runs if Sortable library is loaded
    // ------------------------------------------------------------------
    var grid = document.getElementById('galleryGrid');
    if (grid) {
        if (typeof Sortable !== 'undefined') {
            Sortable.create(grid, { animation: 150, ghostClass: 'bg-light' });
        }
        // Load images regardless of whether Sortable loaded
        var tourId = $("#TourId").val();
        if (tourId && tourId !== "") {
            BindGalleryImages(tourId);
        }
    }

    // ------------------------------------------------------------------
    // Gallery upload
    // url  -> product_images.ashx
    // key  -> "pid"  (matches Request["pid"] in the handler)
    // ------------------------------------------------------------------
    $("#btnSaveGallery").on("click", function () {
        var pid = $("#pid").val();
        if (!pid || pid === "") {
            Swal.fire('Error', 'No product selected.', 'error');
            return;
        }
        var files = $("#fileUp").get(0).files;
        if (files.length === 0) {
            Swal.fire('Warning', 'Please select at least one file.', 'warning');
            return;
        }

        var btn = $(this);
        btn.text("Please wait...");

        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        data.append("pid", pid);
        data.append("GType", "Image");

        $.ajax({
            url: "product_images.ashx",
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                btn.text("Upload");
                if (result.split('|')[0] === "Success") {
                    BindGalleryImages(pid);
                    Swal.fire({ icon: 'success', title: 'Uploaded!', timer: 1500, showConfirmButton: false });
                } else if (result.split('|')[0] === "Permission") {
                    Swal.fire('Error!', 'Permission denied.', 'error');
                } else {
                    Swal.fire('Error!', 'Something went wrong during upload.', 'error');
                }
            },
            error: function () {
                btn.text("Upload");
                Swal.fire('Error!', 'Upload request failed.', 'error');
            }
        });
    });

    // ------------------------------------------------------------------
    // Update image drag/drop order
    // ------------------------------------------------------------------
    $("#UpdateImgOrder").on("click", function () {
        var ids = [];
        $("#galleryGrid .gallery-item").each(function () {
            ids.push($(this).attr("data-id"));
        });
        if (ids.length === 0) {
            Swal.fire('Warning', 'No images to reorder.', 'warning');
            return;
        }
        $.ajax({
            type: 'POST',
            url: 'add-products.aspx/ImageOrderUpdate',
            data: JSON.stringify({ ids: ids }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if (data.d === 'Success')
                    Swal.fire({ icon: 'success', title: 'Order updated!', timer: 1200, showConfirmButton: false });
                else
                    Swal.fire('Error!', 'Something went wrong saving order.', 'error');
            },
            error: function () {
                Swal.fire('Error!', 'Order update request failed.', 'error');
            }
        });
    });

    // ------------------------------------------------------------------
    // Delete gallery image
    // ------------------------------------------------------------------
    $(document).on('click', '.deleteGalleryItem', function () {
        var elem = $(this);
        var id = $(this).attr('data-id');
        Swal.fire({
            title: 'Delete this image?', icon: 'warning',
            showCancelButton: true, confirmButtonColor: '#d33',
            confirmButtonText: 'Delete'
        }).then(function (r) {
            if (r.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: 'add-products.aspx/DeleteGallery',
                    data: JSON.stringify({ id: id }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        if (data.d === 'Success') {
                            elem.closest('.gallery-item').remove();
                            Swal.fire({ icon: 'success', title: 'Deleted!', timer: 1200, showConfirmButton: false });
                        } else {
                            Swal.fire('Error!', 'Could not delete image.', 'error');
                        }
                    },
                    error: function () { Swal.fire('Error!', 'Delete request failed.', 'error'); }
                });
            }
        });
    });

    // ------------------------------------------------------------------
    // Delete FAQ
    // ------------------------------------------------------------------
    $(document).on('click', '.deletepfaqItem', function () {
        var elem = $(this);
        var id = $(this).attr('data-id');
        Swal.fire({
            title: 'Delete this FAQ?', icon: 'warning',
            showCancelButton: true, confirmButtonColor: '#d33',
            confirmButtonText: 'Delete'
        }).then(function (r) {
            if (r.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: 'add-products.aspx/DeleteProductFaqs',
                    data: JSON.stringify({ id: id }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        if (data.d === 'Success') {
                            elem.closest('tr').remove();
                            Swal.fire({ icon: 'success', title: 'Deleted!', timer: 1200, showConfirmButton: false });
                        } else {
                            Swal.fire('Error!', 'Could not delete FAQ.', 'error');
                        }
                    },
                    error: function () { Swal.fire('Error!', 'Delete request failed.', 'error'); }
                });
            }
        });
    });

    // ------------------------------------------------------------------
    // Edit FAQ inline — populate form and switch to FAQ tab
    // ------------------------------------------------------------------
    $(document).on('click', '.editFaqItem', function () {
        var question = $(this).attr('data-question');
        var answer = $(this).attr('data-answer');
        var faqId = $(this).attr('data-id');

        $('#<%=txtQues.ClientID%>').val(question);
        $('#<%=lblFaqId.ClientID%>').text(faqId);  // .text() — Label renders as <span>
        $('#<%=btnFAQ.ClientID%>').val('Update FAQ');

        // Set TinyMCE content — must use setContent(), not .val()
        var answerId = '<%=txtAnswer.ClientID%>';
        var editor = (typeof tinymce !== 'undefined') ? tinymce.get(answerId) : null;
        if (editor) {
            editor.setContent(answer);
        } else {
            $('#' + answerId).val(answer);
        }

        // Switch to FAQ tab (4th tab button = index 3)
        var faqBtn = document.querySelectorAll('.tab-btn')[3];
        if (faqBtn) switchTab('faqs', faqBtn);
    });

});

function BindGalleryImages(id) {
    if (!id || id === "") return;
    $.ajax({
        type: 'POST',
        url: 'add-products.aspx/GetGalleryImage',
        data: JSON.stringify({ id: id }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            var str = "";
            for (var i = 0; i < data.d.length; i++) {
                str += "<div class='gallery-item' data-id='" + data.d[i].Id + "'>" +
                    "  <a href='/" + data.d[i].Images + "' target='_blank'>" +
                    "    <img src='/" + data.d[i].Images + "' alt='Gallery image' />" +
                    "  </a>" +
                    "  <div class='del-btn'>" +
                    "    <a href='javascript:void(0);' class='deleteGalleryItem' data-id='" + data.d[i].Id + "' title='Delete'>" +
                    "      <i class='mdi mdi-trash-can-outline'></i>" +
                    "    </a>" +
                    "  </div>" +
                    "</div>";
            }
            $("#galleryGrid").html(str);
        },
        error: function () {
            console.error("BindGalleryImages: AJAX failed for id=" + id);
        }
    });
}
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