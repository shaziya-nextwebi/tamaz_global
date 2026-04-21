$(document).ready(function () {

    // Auto URL
    $(".txtCategory").on("keyup", function () {
        $(".txtURL").val($(this).val().toLowerCase()
            .replace(/[\.\/\\\*\?\~]/g, '')
            .replace(/\&/g, 'and')
            .replace(/\s+/g, '-'));
    });

    // Character counter
    $(".textcount1").on('keyup', function () {
        var elem = $(this), tps = elem.attr("data-id"), len = elem.val().length;
        elem.siblings('span').text("Character count: " + len);
        elem.siblings('span').css("color",
            (tps === "Title" ? len > 60 : len > 160) ? "red" : "green");
    });

    // Delete
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
                    url: 'category.aspx/Delete',
                    data: JSON.stringify({ id: id }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        if (data.d === 'Success') {
                            elem.closest('tr').remove();
                            Swal.fire('Deleted!', 'Category removed.', 'success');
                        } else Swal.fire('Error!', 'Something went wrong.', 'error');
                    },
                    error: function () { Swal.fire('Error!', 'Something went wrong.', 'error'); }
                });
            }
        });
    });
});