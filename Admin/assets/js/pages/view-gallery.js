$(document).ready(function () {

    // Delete News
    $(document.body).on('click', '.deleteItem', function () {
        var elem = $(this);
        var id = elem.attr('data-id');
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn btn-primary w-xs me-2 mt-2",
            cancelButtonClass: "btn btn-danger w-xs mt-2",
            confirmButtonText: "Yes, delete it!",
            buttonsStyling: false,
            showCloseButton: true
        }).then(function (result) {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: "view-gallery.aspx/Delete",
                    data: JSON.stringify({ id: id }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (data2) {
                        if (data2.d == "Success") {
                            Swal.fire({
                                title: "Deleted!",
                                text: "Gallery has been deleted.",
                                icon: "success",
                                confirmButtonClass: "btn btn-primary w-xs mt-2",
                                buttonsStyling: false
                            });
                            elem.closest('tr').remove();
                        } else if (data2.d == "Permission") {
                            Swal.fire({
                                title: "Oops...",
                                text: "Permission denied! Contact admin.",
                                icon: "error",
                                confirmButtonClass: "btn btn-primary w-xs mt-2",
                                buttonsStyling: false
                            });
                        } else {
                            Swal.fire({
                                title: "Oops...",
                                text: "Something went wrong!",
                                icon: "error",
                                confirmButtonClass: "btn btn-primary w-xs mt-2",
                                buttonsStyling: false
                            });
                        }
                    }
                });
            }
        })
    });
});
















//function initSortable(container) {

//    if (container.hasClass("ui-sortable")) return;

//    container.sortable({
//        items: "> .galleryItem",     // 👈 IMPORTANT
//        cursor: "move",
//        tolerance: "pointer",
//        placeholder: "sortable-placeholder",
//        forcePlaceholderSize: true,
//        cancel: ".deleteItem",       // prevent drag on delete
//        update: function () {

//            var order = [];

//            container.find(".galleryItem").each(function (index) {
//                order.push({
//                    Id: $(this).data("id"),
//                    DisplayOrder: index + 1
//                });
//            });

//            $.ajax({
//                type: "POST",
//                url: "view-gallery.aspx/UpdateOrder",
//                data: JSON.stringify({ items: order }),
//                contentType: "application/json; charset=utf-8"
//            });
//        }
//    });
//}


//$(document).ready(function () {
//    $(".sortableArea").each(function () {
//        initSortable($(this));
//    });

    
//    $('a[data-bs-toggle="tab"]').on('shown.bs.tab', function (e) {
//        var target = $($(e.target).attr("href")).find(".sortableArea");
//        initSortable(target);
//    });

    
//    $(document).on('click', '.deleteItem', function () {
//        var elem = $(this);
//        var id = elem.attr('data-id');

//        Swal.fire({
//            title: "Are you sure?",
//            text: "You won't be able to revert this!",
//            icon: "warning",
//            showCancelButton: true,
//            confirmButtonText: "Yes, delete it!",
//        }).then(function (result) {
//            if (result.isConfirmed) {
//                $.ajax({
//                    type: 'POST',
//                    url: "view-gallery.aspx/Delete",
//                    data: JSON.stringify({ id: id }),
//                    contentType: 'application/json; charset=utf-8',
//                    dataType: 'json',
//                    success: function (res) {
//                        if (res.d === "Success") {
                   
//                            elem.closest('.galleryItem').fadeOut(200, function () { $(this).remove(); });
//                            Swal.fire("Deleted!", "Gallery item deleted successfully.", "success");
//                        } else {
//                            Swal.fire("Oops...", "Something went wrong!", "error");
//                        }
//                    }
//                });
//            }
//        });
//    });
//});
