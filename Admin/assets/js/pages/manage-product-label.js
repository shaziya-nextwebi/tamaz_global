$(document).on('click', '.deleteItem', function () {
    var elem = $(this), id = $(this).attr('data-id');
    Swal.fire({
        title: 'Are you sure?',
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
                url: 'manage-product-label.aspx/Delete',
                data: JSON.stringify({ id: id }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    if (data.d === 'Success') {
                        elem.closest('tr').remove();
                        Swal.fire('Deleted!', 'Label removed.', 'success');
                    } else Swal.fire('Error!', 'Something went wrong.', 'error');
                },
                error: function () { Swal.fire('Error!', 'Something went wrong.', 'error'); }
            });
        }
    });
});