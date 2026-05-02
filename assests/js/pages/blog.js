$(document).ready(function () {
    BindBlogs();
});
function BindBlogs(pageNo) {

    if (!pageNo) pageNo = 1;

    $.ajax({
        type: "POST",
        url: "blog.aspx/BindBlogs",
        data: JSON.stringify({ pNo: pageNo, pSize: 6 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            var data = response.d;

            if (data.Status == "Success") {

                $("#blogContainer").html(data.Html);

                BindPagination(pageNo, data.TotalCount, 6);

                // re-trigger animation
                setTimeout(function () {
                    document.querySelectorAll('.reveal').forEach(el => {
                        el.classList.add('active');
                    });
                }, 100);
                if (window.Iconify) {
                    Iconify.scan();
                }
            }
            else {
                $("#blogContainer").html("<div class='col-span-3 text-center'>No blogs found</div>");
            }
        }
    });
}

function BindPagination(currentPage, totalCount, pageSize) {

    var totalPages = Math.ceil(totalCount / pageSize);
    var html = "";

    var start = Math.max(1, currentPage - 2);
    var end = Math.min(totalPages, currentPage + 2);

    if (start > 1) {
        html += `<li><a onclick="BindBlogs(1)" class="page-btn w-10 h-10 flex items-center justify-center rounded-lg border border-slate-200 text-slate-700 font-medium cursor-pointer">1</a></li>`;
        if (start > 2) html += `<li><span class="px-2">...</span></li>`;
    }

    for (var i = start; i <= end; i++) {

        var active = (i == currentPage)
            ? "bg-[#0a1b50] text-white"
            : "border border-slate-200 text-slate-700";

        html += `<li>
            <a onclick="BindBlogs(${i})"
               class="w-10 h-10 flex items-center justify-center rounded-lg ${active} font-medium cursor-pointer">
               ${i}
            </a>
        </li>`;
    }

    if (end < totalPages) {
        if (end < totalPages - 1) html += `<li><span class="px-2">...</span></li>`;
        html += `<li><a onclick="BindBlogs(${totalPages})" class="page-btn w-10 h-10 flex items-center justify-center rounded-lg border border-slate-200 text-slate-700 font-medium cursor-pointer">${totalPages}</a></li>`;
    }

    $("#blogPagination").html(html);
}