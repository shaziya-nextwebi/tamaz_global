$(document).ready(function () {

    // View message modal
    $(document).on('click', '.viewMsg', function () {
        var btn = $(this);
        $('#mdName').text(btn.attr('data-name'));
        $('#mdPhone').text(btn.attr('data-phone'));
        $('#mdCity').text(btn.attr('data-city'));
        $('#mdDate').text(btn.attr('data-date'));
        $('#modalSenderName').text(btn.attr('data-name'));
        // Use html() to preserve any line breaks / formatting
        $('#mdMessage').html(btn.attr('data-message'));
        var modal = new bootstrap.Modal(document.getElementById('msgModal'));
        modal.show();
    });

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
                    url: 'contact-request.aspx/Delete',
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

});