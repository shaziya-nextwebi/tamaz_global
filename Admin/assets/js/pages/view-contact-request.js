$(document).ready(function () {

    $(document).on("click", ".viewMessageBtn", function () {

        $("#modalMessage").text($(this).data("message"));

        var myModal = new bootstrap.Modal(document.getElementById('messageModal'));
        myModal.show();
    });
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
                    url: "view-contact-request.aspx/Delete",
                    data: JSON.stringify({ id: id }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (data2) {
                        if (data2.d == "Success") {
                            Swal.fire({
                                title: "Deleted!",
                                text: "Contact has been deleted.",
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