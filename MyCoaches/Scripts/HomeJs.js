$(document).ready(function () {
    function clickActiveNav(val,object) {
        var value = val;
        if (value == "prs") {
            $("#navbarNum > li").each(function () {
                if ($(this).hasClass("active")) {
                    value = $(this).children().attr("val") - 1;
                    if (value > 0) {
                        $(this).removeClass("active");
                        $(this).prev().addClass("active");
                    } else
                        value = 1;
                }
            });
        }
        else
            if (value == "suv") {
                var o;
                $("#navbarNum > li").each(function () {
                    if ($(this).hasClass("active")) {
                        value = Number($(this).children().attr("val")) + 1;
                        var lastVal = $("#navbarNum > li").last().prev().children().attr("val");
                        if (lastVal >= value) {
                            $(this).removeClass("active");
                            o = $(this).next();
                        } else {
                            value = lastVal;
                            o = $("#navbarNum > li").last().prev();
                        }

                    }
                });
                o.addClass("active");
            }
            else {
                $("#navbarNum > li").each(function () {
                    $(this).removeClass("active");
                });
                object.parent().addClass("active");
            }
        return value;
    }
    $(".clickNavPost").click(function () {
        var value = clickActiveNav($(this).attr("val"), $(this));
        value = value * 9;
        var i = 0;
        $(".count_item").each(function () {
            if (i >= (value - 9) && i <= value - 1) {
                $(this).css("display", "block");
            }
            else {
                $(this).css("display", "none");
            }
            i++;
        });
    });
    $(".clickNavCoach").click(function () {
        var value = clickActiveNav($(this).attr("val"), $(this));
        value = value * 6;
        var i = 0;
        $(".count_item_coache").each(function () {
            if (i >= (value - 6) && i <= value - 1) {
                $(this).attr("style", "display: block!important");
            }
            else {
                $(this).attr("style", "display: none!important"); 
            }
            i++;
        });
    });
    $(".clickNavChoix").click(function () {
        var value = clickActiveNav($(this).attr("val"), $(this));
        value = value * 9;
        var i = 0;
        $(".count_item_Choix").each(function () {
            if (i >= (value - 9) && i <= value - 1) {
                $(this).attr("style", "display: block!important");
            }
            else {
                $(this).attr("style", "display: none!important");
            }
            i++;
        });
    });
    $("#btncomments").click(function () {
        $("#comment").toggle();
    });
    $(".reply").click(function () {
        if ($(this).next().css("display") == "none") {
            $(".reply").next().css({ "display": "none" });
            $(this).next().css({ "display": "block" });
        }
        else {
            checkReplay = false;
            $(this).next().css({ "display": "none" });
        }
    });
    $("#action_menu_btn").on("click",function () {
        $(".action_menu").toggle();
    });
});

