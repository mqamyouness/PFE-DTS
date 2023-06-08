$(document).ready(function () {
    $("#nav_bar_left").children().click(function () {
        $("#nav_bar_left").children().each(function () {
            $("#nav_bar_left").children().removeClass("active");
        });
        $(this).addClass("active");
    });
});