$(document).ready(function () {
    // Section Post Ajax
    if ($("#DivDetailes").length>0) {
        var id = $("#DivDetailes").attr("idPost");
        var checkReplay = false;
        setInterval(function () {
            $.ajax({
                type: "GET",
                url: "/Posts/getEvaluation",
                data: "id=" + id,
                success: function (data) {
                    $("#ParentReact").html(data);
                    $("#like").click(function () {
                        likePost();
                    });
                    $("#dislike").click(function () {
                        dislikePost();
                    });
                }
            });
        }, 5000);
        setInterval(function () {
            checkReplay = false;
            $(".reply").each(function () {
                if ($(this).next().css("display") == "block") {
                    checkReplay = true;
                }
            }); 
            if (!checkReplay) {

                $.ajax({
                    type: "GET",
                    url: "/Posts/getComment",
                    data: "id=" + id,
                    success: function (data) {
                        $("#comment").html(data);
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
                        $(".submitReply").click(function () {
                            addReponde($(this));
                        });
                    }
                });

            }
        }, 5000);
        $("#like").click(function () {
            likePost();
        });
        function likePost() {
            $.ajax({
                type: "POST",
                url: "/Posts/Like",
                data: "id="+ id ,
                success: function () {
                    $.ajax({
                        type: "GET",
                        url: "/Posts/getEvaluation",
                        data: "id=" + id,
                        success: function (data) {
                            $("#ParentReact").html(data);
                            $("#like").click(function () {
                                likePost();
                            });
                        }
                    });
                }
            });
        }
        $("#dislike").click(function () {
            dislikePost();
        });
        function dislikePost() {
            $.ajax({
                type: "POST",
                url: "/Posts/Dislike",
                data: "id=" + id ,
                success: function () {
                    $.ajax({
                        type: "GET",
                        url: "/Posts/getEvaluation",
                        data: "id=" + id,
                        success: function (data) {
                            $("#ParentReact").html(data);
                            $("#like").click(function () {
                                likePost();
                            });
                        }
                    });
                }
            });
        }
        $(".submitReply").click(function () {
            addReponde($(this));
        });
        function addReponde(e) {
            var repond = e.parent().prev().children().eq(1).val();
            var forUser = e.parent().parent().parent().parent().children().eq(0).val();
            console.log(e.parent().parent());
            var idComment = e.attr("idComment");
            $.ajax({
                url: "/Posts/addRepondes",
                type: "POST",
                data: "id=" + idComment + "&message=" + repond + "&foruser=" + forUser
            });
            e.parent().parent().parent().css("display", "none");
        }
        $("#btnPostComment").click(function () {
            var comment = $(this).parent().prev().children().eq(1).val();
            $(this).parent().prev().children().eq(1).val("");
            console.log(comment);
            $.ajax({
                url: "/Posts/addComment",
                type: "POST",
                data: "idPost=" + id + "&message=" + comment
            });
            $("#comment").css("display", "block");
        });
        $("#searchMinPost").bind('input propertychange', function () {
            var search = $(this).val();
            $.ajax({
                url: "/Posts/getminPost",
                type: "GET",
                data: "search=" + search,
                success: function (data) {
                    $("#minPost").html(data)
                }
            });
        });
    }
    //Section Ajax Coach 
    if ($("#coachesSection").length>0) {
        $("#searchfiltercoaches").bind('input propertychange', function () {
            var search = $(this).val();
            $.ajax({
                url: "/Coaches/filter",
                type: "GET",
                data: "search=" + search,
                success: function (data) {
                    $("#coaches").html(data);
                    if (data.length < 6) {
                        $("#navbarNum").css("display", "none");
                    } else {
                        $("#navbarNum").css("display", "block");
                    }
                }
            });
        });
        $(".click_activ").click(function () {
            var search = $("#searchfiltercoaches").val();
            if ($(this).hasClass("btn-outline-danger")) {
                $(this).removeClass("btn-outline-danger");
                $(this).addClass("btn-danger");
            }
            else {
                $(this).removeClass("btn-danger");
                $(this).addClass("btn-outline-danger");
            }
            if ($(this).val() == "Men" && $(this).next().hasClass("btn-danger")) {
                $(this).next().removeClass("btn-danger");
                $(this).next().addClass("btn-outline-danger");
            }
            if ($(this).val() == "Women" && $(this).prev().hasClass("btn-danger")) {
                $(this).prev().removeClass("btn-danger");
                $(this).prev().addClass("btn-outline-danger");
            }
            var tag = "&";
            var cpt = 1;
            $(".click_activ").each(function () {
                if ($(this).hasClass("btn-danger")) {
                    tage += "tag" + cpt + "=" + $(this).val()+"&";
                    cpt++;
                }
            });
            console.log(tage);
            $.ajax({
                url: "/Coaches/filter",
                type: "GET",
                data: "search=" + search + tage,
                success: function (data) {
                    $("#coaches").html(data);
                }
            });
        });
    }
    // Section Ajax Choix 
    if ($("#SectionChoix").length>0) {
        $("#searchfilterchoix").bind('input propertychange', function () {
            var search = $(this).val();
            $.ajax({
                url: "/Sports/filter",
                type: "GET",
                data: "search=" + search,
                success: function (data) {
                    $("#choix").html(data);
                    if (data.length < 6) {
                        $("#navbarNum").css("display", "none");
                    } else {
                        $("#navbarNum").css("display", "block");
                    }
                }
            });
        });
        $(".click_activ").click(function () {
            var search = $("#searchfiltercoaches").val();
            if ($(this).hasClass("btn-outline-danger")) {
                $(this).removeClass("btn-outline-danger");
                $(this).addClass("btn-danger");
            }
            else {
                $(this).removeClass("btn-danger");
                $(this).addClass("btn-outline-danger");
            }
            if ($(this).val() == "Men" && $(this).next().hasClass("btn-danger")) {
                $(this).next().removeClass("btn-danger");
                $(this).next().addClass("btn-outline-danger");
            }
            if ($(this).val() == "Women" && $(this).prev().hasClass("btn-danger")) {
                $(this).prev().removeClass("btn-danger");
                $(this).prev().addClass("btn-outline-danger");
            }
            var tag = "&";
            var cpt = 1;
            $(".click_activ").each(function () {
                if ($(this).hasClass("btn-danger")) {
                    tag += "tag" + cpt + "=" + $(this).val() + "&";
                    cpt++;
                }
            });
            console.log(tage);
            $.ajax({
                url: "/Sports/filter",
                type: "GET",
                data: "search=" + search + tag,
                success: function (data) {
                    $("#choix").html(data);
                }
            });
        });
    }
    // Session Ajax Chats
    if ($("#section-chat").length > 0) {
        $("#contactChats").children().on("click",function () {
            $("#contactChats").children().each(function () {
                $(this).removeClass("active_li");
            });
            $(this).addClass("active_li");
            var id = $(this).attr("idContact");
            $.ajax({
                url: "/Chats/getMessages",
                type: "GET",
                data: "id=" + id,
                success: function (data) {
                    $(".msgAjax").remove();
                    $("#content-msg").prepend(data);
                    scrollDown();
                    $("#action_menu_btn").on("click", function () {
                        $(".action_menu").toggle();
                    });
                }
            });
        });
        $("#search_chat").bind('input propertychange', function () {
            var search = $(this).val(); 
            var id = 0;
            $.ajax({
                url: "/Chats/searchChat",
                type: "GET",
                data: "search=" + search,
                success: function (data) {
                    $("#contactChats").html(data);
                    $("#contactChats").children().each(function () {
                        if ($(this).hasClass("active_li")) {
                            id = $(this).attr("idContact");
                        }
                    });
                    $.ajax({
                        url: "/Chats/getMessages",
                        type: "GET",
                        data: "id=" + id,
                        success: function (data) {
                            $(".msgAjax").remove();
                            $("#content-msg").prepend(data);
                            scrollDown();
                            $("#action_menu_btn").on("click", function () {
                                $(".action_menu").toggle();
                            });
                        }
                    });
                    $("#contactChats").children().click(function () {
                        $("#contactChats").children().each(function () {
                            $(this).removeClass("active_li");
                        });
                        $(this).addClass("active_li");
                        id = $(this).attr("idContact");
                        $.ajax({
                            url: "/Chats/getMessages",
                            type: "GET",
                            data: "id=" + id,
                            success: function (data) {
                                $(".msgAjax").remove();
                                $("#content-msg").prepend(data);
                                scrollDown();
                                $("#action_menu_btn").on("click", function () {
                                    $(".action_menu").toggle();
                                });
                            }
                        });
                    });
                }
            });
        });
        setInterval(function () {
            var id = 0;
            $("#contactChats").children().each(function () {
                if ($(this).hasClass("active_li")) {
                    id = $(this).attr("idContact");
                }
            });
            $.ajax({
                url: "/Chats/checkMessage",
                type: "GET",
                data: "id=" + id + "&nbr=" + $("#nbr_msg").attr("id_nbr_msg"),
                success: function (data) {
                    if (data == "1") {
                        $.ajax({
                            url: "/Chats/getMessages",
                            type: "GET",
                            data: "id=" + id,
                            success: function (data) {
                                $(".msgAjax").remove();
                                $("#content-msg").prepend(data);
                                scrollDown();
                                $("#action_menu_btn").on("click", function () {
                                    $(".action_menu").toggle();
                                });
                            }
                        });
                    }
                }
            });
        }, 1000);
        setInterval(function () {
            var id;
            var cpt=0;
            $("#contactChats").children().each(function () {
                if ($(this).hasClass("active_li")) {
                    id = $(this).attr("idContact");
                }
                cpt++;
            });
            $.ajax({
                url: "/Chats/checkContact",
                type: "GET",
                data: "nbr=" + cpt,
                success: function (data) {
                    if (data == "1" && $("#search_chat").val()== "" ) {
                        $.ajax({
                            url: "/Chats/getContact",
                            type: "GET",
                            success: function (data) {
                                $("#contactChats").html(data);
                                $("#contactChats").children().each(function () {
                                    $(this).removeClass("active_li");
                                    if (id == $(this).attr("idContact")) {
                                        $(this).addClass("active_li");
                                    }
                                });
                                $("#contactChats").children().click(function () {
                                    $("#contactChats").children().each(function () {
                                        $(this).removeClass("active_li");
                                    });
                                    $(this).addClass("active_li");
                                    id = $(this).attr("idContact");
                                    $.ajax({
                                        url: "/Chats/getMessages",
                                        type: "GET",
                                        data: "id=" + id,
                                        success: function (data) {
                                            $(".msgAjax").remove();
                                            $("#content-msg").prepend(data);
                                            scrollDown();
                                            $("#action_menu_btn").on("click", function () {
                                                $(".action_menu").toggle();
                                            });
                                        }
                                    });
                                });
                            }
                        });
                    }
                }
            });
        }, 1000);
        $("#send_msg").click(function () {
            var id = $("#infoUser").attr("id_userInfo");
            var message = $("#message_ev").val();
            $("#message_ev").val("");
            if (message != "") {
                $.ajax({
                    url: "/Chats/sendMessages",
                    type: "POST",
                    data: "id=" + id + "&message=" + message
                });
            }
        });
        function scrollDown() {
            var element = document.getElementById("msgs");
            element.scrollTop = element.scrollHeight;
        }
        scrollDown();
    }
});

