$(document).ready(function () {

    // Numbers only for display order
    $("#<%=txtOrder.ClientID %>").on('keypress', function (e) {
        var c = e.which || e.keyCode;
        if (c < 48 || c > 57) return false;
    });

    // Delete
    $(document).on('click', '.deleteItem', function () {
        var elem = $(this);
        var id = $(this).attr('data-id');
        Swal.fire({
            title: 'Are you sure to delete?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6'
        }).then(function (result) {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: 'banner-images.aspx/Delete',
                    data: JSON.stringify({ id: id }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        if (data.d === 'Success') {
                            elem.closest('tr').remove();
                            if ($('#tbdy').children().length === 0) {
                                $('#tbdy').append("<tr><td colspan='7' class='dataTables_empty text-center'>No data available</td></tr>");
                            }
                            Swal.fire('Deleted!', 'Banner has been deleted.', 'success');
                        } else {
                            Swal.fire('Error!', 'Something went wrong. Please try again.', 'error');
                        }
                    },
                    error: function () {
                        Swal.fire('Error!', 'Something went wrong. Please try again.', 'error');
                    }
                });
            }
        });
    });

});