$(document).ready(function () {

    // Init SortableJS on the list
    Sortable.create(document.getElementById('left-defaults'), {
        animation: 150,
        ghostClass: 'bg-light'
    });

    // Load products when category changes
    $(".ddlCat").on("change", function () {
        PopulateProducts();
    });

    // Update order button
    $("#btnUpdate").on("click", function () {
        var category = $(".ddlCat").val();
        if (!category || category === "0") {
            Swal.fire('Warning', 'Please select a category first.', 'warning');
            return;
        }

        var ids = [];
        $("#left-defaults li").each(function () {
            ids.push($(this).attr("data-id"));
        });

        if (ids.length === 0) {
            Swal.fire('Warning', 'No products to reorder.', 'warning');
            return;
        }

        var btn = $(this);
        btn.text("Please wait...");

        $.ajax({
            type: 'POST',
            url: 'category-order.aspx/UpdateProductOrder',
            data: JSON.stringify({ ids: ids }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                btn.text("Update Order");
                if (data.d === 'Success') {
                    Swal.fire({
                        icon: 'success',
                        title: 'Order Updated!',
                        timer: 1500,
                        showConfirmButton: false
                    });
                } else {
                    Swal.fire('Error!', 'Something went wrong.', 'error');
                }
            },
            error: function () {
                btn.text("Update Order");
                Swal.fire('Error!', 'Something went wrong.', 'error');
            }
        });
    });
});

function PopulateProducts() {
    var category = $(".ddlCat").val();
    if (!category || category === "0") {
        $("#left-defaults").html("");
        return;
    }

    $.ajax({
        type: 'POST',
        url: 'category-order.aspx/GetProductsByCategory',
        data: JSON.stringify({ categoryId: category }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            var str = "";
            if (data.d.length === 0) {
                str = "<p class='text-muted p-3'>No products found in this category.</p>";
            } else {
                for (var i = 0; i < data.d.length; i++) {
                    str += "<li data-id='" + data.d[i].Id + "'>" +
                        "<div class='maindiv'>" +
                        "<img src='/" + data.d[i].Image + "' alt='" + data.d[i].ProductName + "' />" +
                        "<div><span>" + data.d[i].ProductName + "</span></div>" +
                        "</div>" +
                        "</li>";
                }
            }
            $("#left-defaults").html(str);
        },
        error: function () {
            Swal.fire('Error!', 'Failed to load products.', 'error');
        }
    });
}