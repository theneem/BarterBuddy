var revapi;
$(document).ready(function () {
    $('.txt').focus(function () {
        $('.txtMain').removeClass('spanAnimate');
        $(this).parent().addClass('spanAnimate');
    });
    $('.txt').blur(function () {
        var $this = $(this);
        if ($this.val().length == 0) {
            $(this).parent().removeClass('spanAnimate');
        }
        else if ($(this).val() != "") {
            $(this).parent().addClass('topSpan');
        }
        $(this).parent().removeClass('spanAnimate');
    });

    $('.txt').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code === 9) {
            if ($(this).val() != "") {
                $(this).parent().removeClass('spanAnimate').addClass('topSpan');
            }
        }
    });

    $.each($('form').find('.txt'), function (k, v) {
        if ($(this).val() != "") {
            $(this).parent().addClass('topSpan');
        }
    });
    var revapi = jQuery('.tp-banner').show().revolution({
        waitForInit: true,
        delay: 15000,
        autowidth: "on",
        autoHeight: "on",
        hideThumbs: 10,
        fullWidth: "on",
        forceFullWidth: "on",
        fullScreenOffsetContainer: "",
        navigationType: "none",
        navigationArrows: "none",
        navigationStyle: "round",
        onHoverStop: true,
        stopAfterLoops: 0,
        stopAtSlide: 1,
    });
    $('#slider').slick({
        // options: option - value,
        infinite: false,
        speed: 1500,
        autoplay: false,
        autoplaySpeed: 15000,
        dots: false,
        dragging: true,
        useCSS: false,
        arrows: false,
        adaptiveHeight: true,
        vertical: true,
        verticalSwiping: true,
        pauseOnHover: true,
    });
    $(".dot").click(function (e) {
        e.preventDefault();
        var slide = $(this).index();
        revapi.revshowslide(slide + 1);
        $('#slider').slick("slickGoTo", slide);
        if ($('.dot').hasClass('active')) {
            $('.dot').removeClass('active');
            $(this).addClass('active');
        }
        else {
            $('.dot').addClass('active');
        }
    });
    //    revapi = jQuery('.tp-banner').revolution(
    //            {
    //                delay: 15000,
    //                autowidth: "on",
    //                autoHeight: "on",
    //                hideThumbs: 10,
    //                fullWidth: "on",
    //                forceFullWidth: "on",
    //                fullScreenOffsetContainer: "",
    //                navigationType: "bullet",
    //                navigationArrows: "none",
    //                navigationStyle: "round",
    //                onHoverStop: true
    //            });
    //    $("#slider").slick({
    //        infinite: true,
    //        speed: 1500,
    //        autoplay: false,
    //        autoplaySpeed: 17000,
    //        dots: true,
    //        dragging: true,
    //        useCSS: false,
    //        arrows: false,
    //        adaptiveHeight: true,
    //        vertical: true,
    //        verticalSwiping: true,
    //        pauseOnHover: true,
    //    });



    $(".blueCon").mCustomScrollbar();
    $(".menuIcon").click(function () {
        if ($('.menuIcon').hasClass('open')) {
            $('.menuIcon').removeClass('open');
            $(this).next().removeClass('openMenu');
        }
        else {
            $(this).addClass('open');
            $(this).next().addClass('openMenu');
        }
    });

    $(".logBtn").click(function () {
        $(".bg").fadeIn("slow");
        $(".popup").addClass("cl-popup");
        $(".popup, .close").show();
    });
    $(".close").click(function () {
        $(".popup").removeClass("cl-popup");
        $('.bg,.close').fadeOut("fast");
    });

    //tabing start here
    $(".detailTab li").click(function () {
        var tabId = $(this).attr('data-tab');
        $('.detailTab li').removeClass("current");
        $(".accoContain").removeClass("current in");
        $(this).addClass("current");
        $("#" + tabId).addClass("current");
        setTimeout(function () {
            $("#" + tabId).addClass("in");
        }, 100);
    });

    // fatching data and make accordian
    $(".accoContain").before("<h2 class='resp-accordion' role='tab'><span class='resp-arrow'></span></h2>")

    var itemCount = 0;
    $('.resp-accordion').each(function () {
        var innertext = $('.tabing:eq(' + itemCount + ')').html();
        $('.resp-accordion:eq(' + itemCount + ')').append(innertext);
        itemCount++;
    });

    if ($(window).width() < 767) {
        $(".accoContain").removeClass("current in");
        $(".resp-accordion").click(function () {
            if ($(this).hasClass("minus")) {
                $(this).removeClass("minus").next().slideUp();
            }
            else {
                $(".resp-accordion").removeClass("minus");
                $(".accoContain").slideUp();
                $(this).addClass("minus");
                $(this).next().slideDown();
            }
        });
    }
    ;
});
$(window).on("resize", function () {
    $(".blueCon").mCustomScrollbar();
});
