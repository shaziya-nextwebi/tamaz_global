// Tab switching
function switchTab(name, btn) {
    document.querySelectorAll('.tab-pane').forEach(p => p.classList.remove('active'));
    document.querySelectorAll('.tab-btn').forEach(b => b.classList.remove('active'));
    document.getElementById('tab-' + name).classList.add('active');
    btn.classList.add('active');
}

$(document).ready(function () {

    // Auto URL
    $(".txtProdName").on("keyup", function () {
        $(".txtURL").val($(this).val().toLowerCase()
            .replace(/[\.\/\\\*\?\~]/g, '')
            .replace(/\&/g, 'and')
            .replace(/\s+/g, '-'));
    });

    // Brand autocomplete
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

    // Init gallery sortable
    var grid = document.getElementById('galleryGrid');
    if (grid) {
        Sortable.create(grid, { animation: 150, ghostClass: 'bg-light' });
        BindGalleryImages($("#TourId").val());
    }

    // Gallery upload
    $("#btnSaveGallery").on("click", function () {
        var pid = $("#pid").val();
        if (!pid) { Swal.fire('Error', 'No product selected.', 'error'); return; }

        var fileUpload = $("#fileUp").get(0);
        var files = fileUpload.files;
        if (files.length === 0) { Swal.fire('Warning', 'Please select files to upload.', 'warning'); return; }

        var btn = $(this);
        btn.text("Please wait...");

        var data = new FormData();
        for (var i = 0; i < files.length; i++) { data.append(files[i].name, files[i]); }
        data.append("TId", pid);
        data.append("GType", "Image");

        $.ajax({
            url: "package-images.ashx",
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                btn.text("Upload");
                if (result.split('|')[0] === "Success") {
                    BindGalleryImages(pid);
                    Swal.fire({ icon: 'success', title: 'Uploaded!', timer: 1500, showConfirmButton: false });
                } else {
                    Swal.fire('Error!', 'Something went wrong.', 'error');
                }
            },
            error: function () { btn.text("Upload"); Swal.fire('Error!', 'Upload failed.', 'error'); }
        });
    });

    // Update image order
    $("#UpdateImgOrder").on("click", function () {
        var ids = [];
        $("#galleryGrid .gallery-item").each(function () { ids.push($(this).attr("data-id")); });

        $.ajax({
            type: 'POST',
            url: 'add-products.aspx/ImageOrderUpdate',
            data: JSON.stringify({ ids: ids }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if (data.d === 'Success')
                    Swal.fire({ icon: 'success', title: 'Order updated!', timer: 1200, showConfirmButton: false });
                else Swal.fire('Error!', 'Something went wrong.', 'error');
            }
        });
    });

    // Delete gallery image
    $(document).on('click', '.deleteGalleryItem', function () {
        var elem = $(this), id = $(this).attr('data-id');
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
                            Swal.fire('Deleted!', '', 'success');
                        } else Swal.fire('Error!', 'Something went wrong.', 'error');
                    }
                });
            }
        });
    });

    // Delete FAQ
    $(document).on('click', '.deletepfaqItem', function () {
        var elem = $(this), id = $(this).attr('data-id');
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
                            Swal.fire('Deleted!', '', 'success');
                        } else Swal.fire('Error!', 'Something went wrong.', 'error');
                    }
                });
            }
        });
    });

    // Edit FAQ inline
    $(document).on('click', '.editFaqItem', function () {
        $('#<%=txtQues.ClientID %>').val($(this).attr('data-question'));
        $('#<%=txtAnswer.ClientID %>').val($(this).attr('data-answer'));
        $('#<%=lblFaqId.ClientID %>').text($(this).attr('data-id'));
        $('#<%=btnFAQ.ClientID %>').val('Update FAQ');
        switchTab('faqs', document.querySelectorAll('.tab-btn')[3]);
    });
});

function BindGalleryImages(id) {
    if (!id) return;
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
                    "<a href='/" + data.d[i].Images + "' target='_blank'>" +
                    "<img src='/" + data.d[i].Images + "' /></a>" +
                    "<div class='del-btn'><a href='javascript:void(0);' class='deleteGalleryItem' data-id='" + data.d[i].Id + "'>" +
                    "<i class='mdi mdi-trash-can-outline'></i></a></div></div>";
            }
            $("#galleryGrid").html(str);
        }
    });
}
