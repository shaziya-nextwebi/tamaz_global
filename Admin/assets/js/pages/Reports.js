function esc(str) {
    return (str || '').replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
}

function statusBadge(s) {
    var map = {
        'Confirmed': 'badge-confirmed', 'Cancelled': 'badge-cancelled',
        'Shipped': 'badge-shipped', 'Delivered': 'badge-delivered',
        'Paid': 'badge-paid', 'Unpaid': 'badge-unpaid', 'Partial': 'badge-partial'
    };
    return '<span class="badge-status ' + (map[s] || 'badge-pending') + '">' + esc(s) + '</span>';
}

$('#btnDispatch').click(function () {

    var guid = $('#DispatchModal').attr('data-orderguid');

    var courierName = $("#txtCourierName").val();
    var trackingCode = $("#txtTrackingCode").val();
    var trackingLink = $("#txtTrackingLink").val();
    var DelDate = $("#txtDate").val();

    if (!courierName || !trackingCode || !trackingLink || !DelDate) {
        Swal.fire('Missing Fields', 'All fields are required.', 'warning');
        return;
    }

    // ── Show loading state ─────────────────────────────────────
    var $btn = $(this);
    $btn.prop('disabled', true)
        .html('<span class="spinner-border spinner-border-sm me-1" role="status" aria-hidden="true"></span> Please wait...');

    var data = {
        OrderGuid: guid,
        courierName: courierName,
        trackingCode: trackingCode,
        trackingLink: trackingLink,
        oStatus: "Dispatched",
        DelDate: DelDate
    };

    $.ajax({
        type: 'POST',
        url: "report.aspx/DispatchOrder",
        data: JSON.stringify(data),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',

        success: function (res) {
            // ── Restore button ─────────────────────────────────
            $btn.prop('disabled', false).html('Submit');

            if (res.d === "Success") {
                $('#DispatchModal').modal('hide');
                Swal.fire({ icon: 'success', title: 'Dispatched!', text: 'Order dispatched successfully.', timer: 1500, showConfirmButton: false });
                setTimeout(() => location.reload(), 1600);
            }
            else if (res.d === "Dispatched") {
                Swal.fire('Already Dispatched', 'This order has already been dispatched.', 'info');
            }
            else if (res.d === "Permission") {
                Swal.fire('Permission Denied', 'Contact your administrator.', 'error');
            }
            else if (res.d === "InvalidStatus") {
                Swal.fire('Not Allowed', 'Only confirmed orders can be dispatched.', 'warning');
            }
            else {
                Swal.fire('Error!', 'Failed: ' + res.d, 'error');
            }
        },
        error: function (err) {
            // ── Restore button on error too ────────────────────
            $btn.prop('disabled', false).html('Submit');
            console.log(err);
            Swal.fire('AJAX Error', 'Could not reach the server. Try again.', 'error');
        }
    });
});
document.addEventListener('click', function (e) {
    var btn = e.target.closest('.deleteItem');
    if (!btn) return;

    var id = btn.getAttribute('data-id');

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#6b7280',
        confirmButtonText: 'Yes, Delete it!'
    }).then(function (result) {
        if (!result.isConfirmed) return;

        fetch('report.aspx/Delete', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: id })
        }).then(function (r) { return r.json(); })
            .then(function (res) {
                if (res.d === 'Success') {
                    var row = btn.closest('tr');
                    row.style.transition = 'opacity .3s';
                    row.style.opacity = '0';
                    setTimeout(function () { row.remove(); }, 320);
                    Swal.fire({ icon: 'success', title: 'Deleted!', text: 'Order deleted successfully.', timer: 1500, showConfirmButton: false });
                } else if (res.d === 'Permission') {
                    Swal.fire('Permission Denied', 'Contact your administrator.', 'error');
                } else {
                    Swal.fire('Error!', 'Could not delete. Try again.', 'error');
                }
            });
    });
});
document.addEventListener('click', function (e) {
    var btn = e.target.closest('.viewShip');
    if (!btn) return;

    var d = JSON.parse(atob(btn.getAttribute('data-b64')));

    var html = `
        <div style="background:#f1f5f9;padding:15px;border-radius:10px;">
            <p><b>Contact Person Name :</b> ${esc(d.name)}</p>
            <p><b>Email :</b> ${esc(d.email)}</p>
            <p><b>Phone :</b> ${esc(d.mobile)}</p>
            <p><b>Address :</b> ${esc(d.address)}</p>
            <p><b>City :</b> ${esc(d.city)}</p>
            <p><b>State :</b> ${esc(d.state)}</p>
            <p><b>PinCode :</b> ${esc(d.pincode)}</p>
        </div>
    `;

    document.getElementById('shippingBody').innerHTML = html;

    new bootstrap.Modal(document.getElementById('shippingModal')).show();
});
document.addEventListener('click', function (e) {
    var btn = e.target.closest('.dispatchItem');
    if (!btn) return;

    var guid = btn.getAttribute('data-id');

    $('#DispatchModal').attr('data-orderguid', guid);
    new bootstrap.Modal(document.getElementById('DispatchModal')).show();
});
document.addEventListener('click', function (e) {
    var btn = e.target.closest('.viewShipInfo');
    if (!btn) return;

    var html = `
        <div style="padding:10px">
            <p><b>Courier:</b> ${btn.dataset.courier || '-'}</p>
            <p><b>Tracking Code:</b> ${btn.dataset.tracking || '-'}</p>
            <p><b>Tracking Link:</b> <a href="${btn.dataset.link}" target="_blank">Track</a></p>
            <p><b>Delivery Date:</b> ${btn.dataset.date || '-'}</p>
        </div>
    `;

    document.getElementById('shippingBody').innerHTML = html;
    new bootstrap.Modal(document.getElementById('shippingModal')).show();
});
document.addEventListener('click', function (e) {
    var btn = e.target.closest('.deliverItem');
    if (!btn) return;

    var id = btn.getAttribute('data-id');

    Swal.fire({
        title: 'Mark as Delivered?',
        text: "This will update the order status to Delivered.",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#16a34a',
        cancelButtonColor: '#6b7280',
        confirmButtonText: 'Yes, Deliver it!'
    }).then(function (result) {
        if (!result.isConfirmed) return;

        fetch('report.aspx/MarkDelivered', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: id })
        })
            .then(function (r) { return r.json(); })
            .then(function (res) {
                if (res.d === 'Success') {
                    Swal.fire({ icon: 'success', title: 'Delivered!', text: 'Order marked as delivered successfully.', timer: 1500, showConfirmButton: false });
                    setTimeout(function () { location.reload(); }, 1600);
                } else if (res.d === 'AlreadyDelivered') {
                    Swal.fire('Already Delivered', 'This order is already marked as delivered.', 'info');
                } else if (res.d === 'InvalidStatus') {
                    Swal.fire('Not Allowed', 'Only dispatched orders can be marked as delivered.', 'warning');
                } else if (res.d === 'Permission') {
                    Swal.fire('Permission Denied', 'Contact your administrator.', 'error');
                } else if (res.d === 'NotFound') {
                    Swal.fire('Not Found', 'Order not found.', 'error');
                } else {
                    Swal.fire('Error!', 'Something went wrong. Try again.', 'error');
                }
            })
            .catch(function () {
                Swal.fire('Network Error', 'Could not reach the server. Try again.', 'error');
            });
    });
});
function showSnackbar(msg, type) {
    var bg = '#008a3d'; // success

    if (type === 'error') bg = '#ea1c1c';
    if (type === 'warning') bg = '#f59e0b';

    Snackbar.show({
        pos: 'top-right',
        text: msg,
        actionTextColor: '#fff',
        backgroundColor: bg
    });
}