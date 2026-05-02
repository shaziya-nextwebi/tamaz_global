$(document).ready(function () {
    // View message modal
    $(document).on('click', '.viewMsg', function () {
        var btn = $(this);
        $('#mdName').text(btn.attr('data-name'));
        $('#mdMobile').text(btn.attr('data-mobile'));
        $('#mdCity').text(btn.attr('data-city'));
        $('#mdDate').text(btn.attr('data-date'));
        $('#modalSenderName').text(btn.attr('data-name'));
        $('#mdMessage').html(
            $('<textarea>').html(btn.attr('data-message')).text()
        );

        // Parse and render products nicely
        var rawProduct = $('<textarea>').html(btn.attr('data-product')).text();
        var items = rawProduct.split('|');
        var html = '';

        $.each(items, function (i, item) {
            item = item.trim();
            if (!item) return;

            var parts = item.split('#');
            var namePart = parts[0].trim();
            var qtyPart = parts[1] ? parts[1].trim() : '';

            var priceMatch = namePart.match(/\((₹[\d,]+(?:\.\d{1,2})?)\)$/);
            var price = priceMatch ? priceMatch[1] : '';
            var prodName = namePart.replace(/\s*\(₹[\d,]+(?:\.\d{1,2})?\)$/, '').trim();

            html += '<div class="product-item">';
            html += '  <div class="prod-name">' + $('<div>').text(prodName).html() + '</div>';
            html += '  <div class="prod-meta">';
            if (price) html += '<span class="me-3">Price: <strong>' + price + '</strong></span>';
            if (qtyPart) {
                var qtyMatch = qtyPart.match(/Qty-(\d+)\s*=\s*(₹[\d,]+(?:\.\d{1,2})?)/);
                if (qtyMatch) {
                    html += '<span class="me-3">Qty: <strong>' + qtyMatch[1] + '</strong></span>';
                    html += '<span>Total: <strong>' + qtyMatch[2] + '</strong></span>';
                } else {
                    html += '<span>' + $('<div>').text(qtyPart).html() + '</span>';
                }
            }
            html += '  </div>';
            html += '</div>';
        });

        $('#mdProduct').html(html || '<span class="text-muted">—</span>');

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
                    url: 'cart-enquiry.aspx/Delete',
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