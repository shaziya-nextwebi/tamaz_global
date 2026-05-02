$(document).ready(function () {
    $(document).on('click', '.clean-html-btn', function () {
        var editorId = $(this).data('editor');
        cleanEditor(editorId);
    });
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
function cleanEditor(editorId) {

    var editor = null;
    if (typeof tinymce !== 'undefined') {
        editor = tinymce.get(editorId);
        if (!editor) {
            tinymce.editors.forEach(function (ed) {
                if (ed.id === editorId || ed.id.indexOf(editorId) !== -1) {
                    editor = ed;
                }
            });
        }
    }

    var raw = editor ? editor.getContent() : $('#' + editorId).val();

    if (!raw || raw.trim() === '') {
        Swal.fire('Info', 'Editor is already empty.', 'info');
        return;
    }

    Swal.fire({
        title: 'Cleaning HTML...',
        text: 'AI is cleaning your content, please wait.',
        allowOutsideClick: false,
        allowEscapeKey: false,
        didOpen: function () { Swal.showLoading(); }
    });

    fetch('/clean-html.ashx', {
        method: 'POST',
        headers: { 'Content-Type': 'text/plain; charset=utf-8' },
        body: raw
    })
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            if (data.error) {
                throw new Error(data.error);
            }

            var cleanedHtml = data.html.trim();

            if (editor) {
                editor.setContent(cleanedHtml);
            } else {
                $('#' + editorId).val(cleanedHtml);
            }

            Swal.fire({
                icon: 'success',
                title: 'HTML cleaned!',
                text: 'AI has cleaned your content successfully.',
                timer: 1800,
                showConfirmButton: false
            });
        })
        .catch(function (err) {
            console.error('cleanEditor error:', err);
            Swal.fire('Error!', 'AI cleaning failed: ' + err.message, 'error');
        });
}