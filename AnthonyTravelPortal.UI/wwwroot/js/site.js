$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
        if ($('#sidebar').hasClass('active')) {
            $('#contentWrapper').css('margin-left', 0);
            $('.main-header').css('margin-left', 0);
            $('#sidebarCollapse').css('margin-left', 250);
        } else {
            $('#contentWrapper').css('margin-left', 0);
            $('.main-header').css('margin-left', 0);
            $('#sidebarCollapse').css('margin-left', 0);
        }
    });
});

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip({ html: true });
    $('.collapse').collapse();
    $('[data-init-select2="true"]').select2();
});
