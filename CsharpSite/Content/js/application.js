$(function () { function o() { return $(window).width() - ($('[data-toggle="popover"]').offset().left + $('[data-toggle="popover"]').outerWidth()) } $(window).on("resize", function () { var t = $('[data-toggle="popover"]').data("bs.popover"); t && (t.options.viewport.padding = o()) }), $('[data-toggle="popover"]').popover({ template: '<div class="popover" role="tooltip"><div class="arrow"></div><div class="popover-content p-x-0"></div></div>', title: "", html: !0, trigger: "manual", placement: "bottom", viewport: { selector: "body", padding: o() }, content: function () { var o = $(".app-navbar .navbar-nav:last-child").clone(); return '<div class="nav nav-stacked" style="width: 200px">' + o.html() + "</div>" } }), $('[data-toggle="popover"]').on("click", function (o) { o.stopPropagation(), $('[data-toggle="popover"]').data("bs.popover").tip().hasClass("in") ? ($('[data-toggle="popover"]').popover("hide"), $(document).off("click.app.popover")) : ($('[data-toggle="popover"]').popover("show"), setTimeout(function () { $(document).one("click.app.popover", function () { $('[data-toggle="popover"]').popover("hide") }) }, 1)) }) }),
$(document).on("click", ".js-gotoMsgs", function () {
    $("#msgfrm-container").addClass("hide"), $(".js-conversation").addClass("hide"), $(".js-msgGroup, .js-newMsg").removeClass("hide"), $(".modal-title").html("Messages")
}), $(document).on("click", "[data-action=growl]", function (o) { o.preventDefault(), $("#app-growl").append('<div class="alert alert-dark alert-dismissible fade in" role="alert"><button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button><p>Click the x on the upper right to dismiss this little thing. Or click growl again to show more growls.</p></div>') }), $(document).on("focus", '[data-action="grow"]', function () { $(window).width() > 1e3 && $(this).animate({ width: 300 }) }), $(document).on("blur", '[data-action="grow"]', function () { if ($(window).width() > 1e3) { $(this).animate({ width: 180 }) } }), $(function () { function o() { $(window).scrollTop() > $(window).height() ? $(".docs-top").fadeIn() : $(".docs-top").fadeOut() } $(".docs-top").length && (o(), $(window).on("scroll", o)) }), $(function () { function o() { i.width() > 768 ? e() : t() } function t() { i.off("resize.theme.nav"), i.off("scroll.theme.nav"), n.css({ position: "", left: "", top: "" }) } function e() { function o() { e.containerTop = $(".docs-content").offset().top - 40, e.containerRight = $(".docs-content").offset().left + $(".docs-content").width() + 45, t() } function t() { var o = i.scrollTop(), t = Math.max(o - e.containerTop, 0); return t ? void n.css({ position: "fixed", left: e.containerRight, top: 40 }) : ($(n.find("li")[1]).addClass("active"), n.css({ position: "", left: "", top: "" })) } var e = {}; o(), $(window).on("resize.theme.nav", o).on("scroll.theme.nav", t), $("body").scrollspy({ target: "#markdown-toc", selector: "li > a" }), setTimeout(function () { $("body").scrollspy("refresh") }, 1e3) } var n = $("#markdown-toc"), i = $(window); n[0] && (o(), i.on("resize", o)) });

$(document).on("click", ".js-msgGroup a", function () {
    var target = $(this).data("targetid");
    console.log(target);
    $(".js-msgGroup, .js-newMsg").addClass("hide");
    $(".js-conversation").removeClass("hide");
    $(".modal-title").html('<a href="#" class="js-gotoMsgs">Back</a>');
    $("#msgfrm-container").removeClass("hide");
    $("#msgfrm").data("targetid", target);
    $("#msgfrm input[name='targetID']").val(target);
    GetChat(target)
});
refreshActions();

function refreshActions() {
    $('.btn-follow').on('click', function (e) {
        var target = $(this).data("targetid");
        $.ajax({
            url: "/Follow/Follow/" + target,
            method: "post",
            success: function (resp) {
                location.reload();
            }
        });
    });
    $('.btn-unfollow').on('click', function (e) {
        var target = $(this).data("targetid");
        $.ajax({
            url: "/Follow/Unfollow/" + target,
            method: "post",
            success: function (resp) {
                location.reload();
            }
        });
    });
    $('.btn-react').on('click', function (e) {
        var reaction = $(this).data("reactionid");
        var target = $(this).data("postid");
        $.ajax({
            url: "/Posts/React",
            method: "post",
            format: 'text/json',
            data: {
                ReactionId: reaction,
                PostID: target
            },
            success: function (resp) {
                feedAutoRefresh();
            }
        });
    });
}

function feedAutoRefresh() {
    $.ajax({
        url: '/Feed/GetFeed',
        method: 'post',
        success: function (resp) {

            displayFeed(resp);

        }
    });
}
function displayFeed(feedsjson) {
    $("#feed").empty();
    $("#feed").append('<li class="qf b aml">'
        + '<div class="input-group">'
            + '<input type="text" class="form-control" placeholder="Write a post">'
            + '<div class="fj">'
                + '<button type="button" class="cg fm">'
                    + '<i class="fa fa-file-text" aria-hidden="true"></i>'
                + '</button>'
                + '<button type="button" class="cg fm">'
                    + '<i class="fa fa-camera" aria-hidden="true"></i>'
                + '</button>'
            + '</div>'
        + '</div>'
    + '</li>');
    var element = "";
    for (var i = 0; i < feedsjson['data'].length; i++) {
        var post = feedsjson['data'][i];
        var usr = post['User'];
        element +=
        '<li class="qf b aml">'
            + '<a class="qj" href="#">'
                + '<img class="qh cu"'
                     + 'src="/Content/images/' + usr['UserId'] + '.jpg">'
            + '</a>'
            + '<div class="qg">'
                + '<div class="aoc">'
                    + '<div class="qn">'
                        + '<small class="eg dp">' + post["Publication_date"] + '</small>'
                        + '<h5>' + usr["Username"] + '</h5>'
                    + '</div>'
                    + '<p>'
                        + post["Contents"]
                    + '</p>'
                    + '<div>';
        for (var k = 0; k < post['Reactions'].length; k++) {
            var react = post['Reactions'][k];
            element += '<span style="margin-right: 0.5em;"><i class="btn-react fa ' + react['icon'] + '" '
                + 'data-postid="' + post['PostId'] + '" data-reactionid="' + react['reactionid'] + '" title="' + react['name']
                + '" style="color: gray; cursor: pointer;" aria-hidden="true"></i>' + react['count'] + '</span>';

        }
        element += '</div>'
                    + '<ul class="qo alm">';
        for (var j = 0; j < post['Comments'].length; j++) {
            var comment = post['Comments'][j];
            var cusr = comment['User'];

            element += '<li class="qf">'
                        + '<a class="qj" href="#">'
                            + '<img class="qh cu" src="/Content/images/' + cusr["UserId"] + '.jpg">'
                        + '</a>'
                        + '<div class="qg">'
                            + '<strong>' + cusr["Username"] + ': </strong>'
                            + comment['Contents']
                        + '</div>'
                    + '</li>';
        }

        element += '</ul>'
            + '</div>'
        + '</div>'
    + '</li>';


    }
    $("#feed").append(element);
    refreshActions();
}