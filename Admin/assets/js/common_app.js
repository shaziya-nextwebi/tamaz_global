$(document).ready(function () {
    $('.nospecial').keypress(function (e) {
        var charCode = (e.which) ? e.which : event.keyCode
        if (charCode != 32)
            if (!String.fromCharCode(charCode).match(/^[a-zA-Z0-9]*$/g))
                return false;
    });

    $('.onlyNum').keypress(function (e) {
        var charCode = (e.which) ? e.which : event.keyCode
        if (String.fromCharCode(charCode).match(/[^0-9]/g))
            return false;

    });

    $('.numWPts').keypress(function (e) {
        var charCode = (e.which) ? e.which : event.keyCode
        if (String.fromCharCode(charCode).match(/[^0-9\.]/g))
            return false;

    });

    $('.onlyAlpha').keypress(function (e) {
        var charCode = (e.which) ? e.which : event.keyCode
        if (!String.fromCharCode(charCode).match(/^[a-zA-Z\s]*$/g))
            return false;
    });

    //$(window).on('load', function () {
    //    var dashboardMenuText = "Dashboard";
    //    var activeMenuText = $('.menu-link.active span').text().trim();
    //    var activePageText = $('.nav .nav-item .nav-link.active').html().trim();

    //    // Create breadcrumb items array
    //    var breadcrumbItems = [
    //        { text: dashboardMenuText, href: 'dashboard.aspx', active: false },
    //        { text: activeMenuText, href: 'javascript:void(0);', active: false },
    //        { text: activePageText, href: 'javascript:void(0);', active: true }
    //    ].filter(item => item.text); // Filter out items with empty text

    //    updateBreadcrumb(breadcrumbItems);
    //});
});

function updateBreadcrumb(items) {
    var breadcrumb = $('.breadcrumb');
    breadcrumb.empty();

    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        var li = "";
        if (item.active) {
            li += "<li class='breadcrumb-item active'>" + item.text + "</li>";
        } else {
            li += "<li class='breadcrumb-item'><a href='" + item.href + "'>" + item.text + "</a></li>";
        }
        breadcrumb.append(li);
    }
}