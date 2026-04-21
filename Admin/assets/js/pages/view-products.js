$(document).ready(function () {

    // Delete
    $(document).on('click', '.deleteItem', function () {
        var elem = $(this);
        var id = $(this).attr('data-id');
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            confirmButtonText: 'Delete'
        }).then(function (result) {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: 'view-products.aspx/Delete',
                    data: JSON.stringify({ id: id }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        if (data.d === 'Success') {
                            elem.closest('tr').remove();
                            Swal.fire({ icon: 'success', title: 'Deleted!', timer: 1500, showConfirmButton: false });
                        } else if (data.d === 'Permission') {
                            Swal.fire('Permission Denied', 'Contact your administrator.', 'error');
                        } else {
                            Swal.fire('Error!', 'Something went wrong.', 'error');
                        }
                    }
                });
            }
        });
    });

    // Publish toggle
    $(document).on('change', '.publishProduct', function () {
        var id = $(this).attr('data-id');
        var ftr = $(this).prop('checked') ? 'Yes' : 'No';
        $.ajax({
            type: 'POST',
            url: 'view-products.aspx/PublishProduct',
            data: JSON.stringify({ id: id, ftr: ftr }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if (data.d === 'Success') {
                    var badge = $('#sts_' + id);
                    if (ftr === 'Yes') {
                        badge.attr('class', 'badge bg-success').text('Active');
                    } else {
                        badge.attr('class', 'badge bg-warning text-dark').text('Draft');
                    }
                    Swal.fire({ icon: 'success', title: ftr === 'Yes' ? 'Published!' : 'Set to Draft', timer: 1200, showConfirmButton: false });
                } else if (data.d === 'Permission') {
                    Swal.fire('Permission Denied', 'Contact your administrator.', 'error');
                } else {
                    Swal.fire('Error!', 'Something went wrong.', 'error');
                }
            }
        });
    });

});