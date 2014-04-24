/* giaidieu_custom.js file */
(function ($) {
    // Handle custom phòng đẹp events
    //Drupal.behaviors.custom_phong_dep = {
    //    attach: function (context, settings) {
    //var $test = $("select#edit-field-status-room-wrapper").val()
    //if($test == undefined){
    //alert('ok');
    //}
    $(window).load(function () {
        if ($('#slider').length) {
            $('#slider').nivoSlider({
                effect: 'random',               // Specify sets like: 'fold,fade,sliceDown'
                slices: 15,                     // For slice animations
                boxCols: 8,                     // For box animations
                boxRows: 4,                     // For box animations
                animSpeed: 1000,                 // Slide transition speed
                pauseTime: 3500,                // How long each slide will show
                startSlide: 0,                  // Set starting Slide (0 index)
                directionNav: false,             // Next & Prev navigation
                controlNav: true,               // 1,2,3... navigation
                controlNavThumbs: false,        // Use thumbnails for Control Nav
                pauseOnHover: true,             // Stop animation while hovering
                manualAdvance: false,           // Force manual transitions
                prevText: 'Prev',               // Prev directionNav text
                nextText: 'Next',               // Next directionNav text
                randomStart: true,             // Start on a random slide
                beforeChange: function () { },     // Triggers before a slide transition
                afterChange: function () { },      // Triggers after a slide transition
                slideshowEnd: function () { },     // Triggers after all slides have been shown
                lastSlide: function () { },        // Triggers when last slide is shown
                afterLoad: function () { }         // Triggers when slider has loaded

            });
        }

        if ($('#slider2').length) {
            $('#slider2').nivoSlider({
                effect: 'random',               // Specify sets like: 'fold,fade,sliceDown'
                slices: 15,                     // For slice animations
                boxCols: 8,                     // For box animations
                boxRows: 4,                     // For box animations
                animSpeed: 1000,                 // Slide transition speed
                pauseTime: 3500,                // How long each slide will show
                startSlide: 0,                  // Set starting Slide (0 index)
                directionNav: true,             // Next & Prev navigation
                controlNav: false,               // 1,2,3... navigation
                controlNavThumbs: false,        // Use thumbnails for Control Nav
                pauseOnHover: true,             // Stop animation while hovering
                manualAdvance: false,           // Force manual transitions
                prevText: 'Prev',               // Prev directionNav text
                nextText: 'Next',               // Next directionNav text
                randomStart: true,             // Start on a random slide
                beforeChange: function () { },     // Triggers before a slide transition
                afterChange: function () { },      // Triggers after a slide transition
                slideshowEnd: function () { },     // Triggers after all slides have been shown
                lastSlide: function () { },        // Triggers when last slide is shown
                afterLoad: function () { }         // Triggers when slider has loaded

            });
        }

    });
    if ($(".page-node-162 select#edit-field-house-city-tid").length) {
        $(".page-node-162 select#edit-field-house-city-tid").val('1');
    }
    if ($(".page-nha-cho-thue select#edit-field-house-city-tid").length) {
        $(".page-nha-cho-thue select#edit-field-house-city-tid").val('1');
    }
    $("select#edit-field-house-city-tid").change(function () {
        var tid = $(this).val();
        //init_2();
        $("select#edit-field-house-district-tid").val('All');
        $.post('/select-city/' + tid, {}, function (output) {
            $("select#edit-field-location-child").empty().append(output);
            //Drupal.attachBehaviors("#output-ajax-" + nid);
        });
        var tid_2 = $("select#edit-field-location-child").val();
        var tid_3 = $("select#edit-field-khoang-gia-tid").val();
        var tid_4 = $("select#edit-field-status-room").val();
        var request = jQuery.ajax({
            url: "ajax-info",
            type: "POST",
            data: { city_id: tid, quan_id: tid_2, price_id: tid_3, status_id: tid_4 },
            success: function (msg) {
                if (msg == '') {
                    alert('no result');
                } else {
                    $('.ajax-header').empty().append(msg)
                }
            }
        });
    });
    $("select#edit-field-location-child").change(function () {
        var tid_2 = $(this).val();
        var tid = $("select#edit-field-house-city-tid").val();
        var tid_3 = $("select#edit-field-khoang-gia-tid").val();
        var tid_4 = $("select#edit-field-status-room").val();
        $("select#edit-field-house-district-tid").val(tid_2).change();
        var request = jQuery.ajax({
            url: "ajax-info",
            type: "POST",
            data: { city_id: tid, quan_id: tid_2, price_id: tid_3, status_id: tid_4 },
            success: function (msg) {
                if (msg == '') {
                    alert('no result');
                } else {
                    $('.ajax-header').empty().append(msg)
                }
            }
        });
    });
    $("select#edit-field-status-room").change(function () {
        var status = $(this).val();
        if (status == 1) {
            $("select#edit-field-status-room-value-op").val('empty').change();
        } else {
            if (status == 'All') {
                $("select#edit-field-status-room-value-op").val('<=');
                $("input#edit-field-status-room-value-value").val('').change();
            } else {
                $("select#edit-field-status-room-value-op").val('<=');
                $("input#edit-field-status-room-value-value").val(status).change();
            }
        }
        var tid_4 = $(this).val();
        var tid_2 = $("select#edit-field-location-child").val();
        var tid_3 = $("select#edit-field-khoang-gia-tid").val();
        var tid = $("select#edit-field-house-city-tid").val();
        $("select#edit-field-house-district-tid").val(tid_2).change();
        var request = jQuery.ajax({
            url: "ajax-info",
            type: "POST",
            data: { city_id: tid, quan_id: tid_2, price_id: tid_3, status_id: tid_4 },
            success: function (msg) {
                if (msg == '') {
                    alert('no result');
                } else {
                    $('.ajax-header').empty().append(msg)
                }
            }
        });
    });
    $("select#edit-field-khoang-gia-tid").change(function () {
        var tid_3 = $(this).val();
        var tid_2 = $("select#edit-field-location-child").val();
        var tid_4 = $("select#edit-field-status-room").val();
        var tid = $("select#edit-field-house-city-tid").val();
        $("select#edit-field-house-district-tid").val(tid_2).change();
        var request = jQuery.ajax({
            url: "ajax-info",
            type: "POST",
            data: { city_id: tid, quan_id: tid_2, price_id: tid_3, status_id: tid_4 },
            success: function (msg) {
                if (msg == '') {
                    alert('no result');
                } else {
                    $('.ajax-header').empty().append(msg)
                }
            }
        });
    });
    //Build slideshow image room
    if ($('#slideshow').length) {
        $('.slideshow-image-room #slideshow').cycle({
            fx: 'fade',
            speed: 1000,
            timeout: 0,
            pager: '#nav',
            pagerAnchorBuilder: function (idx, slide) {
                // return sel string for existing anchor
                return '#nav li:eq(' + (idx) + ') a';
            }
        });
        $('.slideshow-image-house #slideshow').cycle({
            fx: 'fade',
            speed: 1000,
            timeout: 0,
            pager: '#house',
            pagerAnchorBuilder: function (idx, slide) {
                // return sel string for existing anchor
                return '#house li:eq(' + (idx) + ') a';
            }
        });
    }
    //Jquery update price form action 
    
    //Build ajax auto load change location in slideshow sales off
    $(".option-city select").change(function () {
        var tid_location = $(this).val();
        $.post('/build-ajax-slideshow/' + tid_location, {}, function (output) {
            $(".content-poster-warpper").empty().append(output);
            Drupal.attachBehaviors(".content-poster-warpper");
        });
    });
    if ($('#slideshowHolder').length) {
        $('#slideshowHolder').jqFancyTransitions({
            effect: '', // wave, zipper, curtain
            width: 495, // width of panel
            height: 166, // height of panel
            strips: 20, // number of strips
            delay: 5000, // delay between images in ms
            stripDelay: 50, // delay beetwen strips in ms
            titleOpacity: 0, // opacity of title
            titleSpeed: 1000, // speed of title appereance in ms
            position: 'alternate', // top, bottom, alternate, curtain
            direction: 'fountainAlternate', // left, right, alternate, random, fountain, fountainAlternate
            navigation: true, // prev and next navigation buttons
            links: false // show images as links
        });
    }
    //    }
    //};
})(jQuery);


function OnSuccess() {

}
function OnBegin() {
    $.BigLoadingWaiting();
}
function OnComplete() {
    $.RemoveBigLoading();
}
function OnFailure() {
    if ($('#overlay').length > 0) {
        $.RemoveBigLoading();
    }
    alert('Lỗi hệ thống. Vui lòng thông báo cho bộ phận quản trị.');
}

$(function () {
    jQuery.fn.extend({

        outerHtml: function (replacement) {
            // We just want to replace the entire node and contents with
            // some new html value
            if (replacement) {
                return this.each(function () { $(this).replaceWith(replacement); });
            }

            /*
            * Now, clone the node, we want a duplicate so we don't remove
            * the contents from the DOM. Then append the cloned node to
            * an anonymous div.
            * Once you have the anonymous div, you can get the innerHtml,
            * which includes the original tag.
            */
            var tmp_node = $("<div></div>").append($(this).clone());
            var markup = tmp_node.html();

            // Don't forget to clean up or we will leak memory.
            tmp_node.remove();
            return markup;
        }
    });

    $.fn.vCenter = function (options) {
        var pos = {
            sTop: function () {
                return window.pageYOffset
        || document.documentElement && document.documentElement.scrollTop
        || document.body.scrollTop;
            },
            wHeight: function () {
                return window.innerHeight
        || document.documentElement && document.documentElement.clientHeight
        || document.body.clientHeight;
            }
        };
        return this.each(function (index) {
            if (index == 0) {
                var $this = $(this);
                var elHeight = $this.height();
                var elTop = pos.sTop() + (pos.wHeight() / 2) - (elHeight / 2) - options.offsetTop;
                $this.css({
                    position: 'absolute',
                    marginTop: '0',
                    top: elTop
                });
            }
        });
    };

    $.BigLoadingWaiting = function () {

        var overlayElement = $("<div id='overlay' class='ui-widget-overlay'></div>");
        /*
        var loadingtag = '<object width="200" height="100">'
        + '<param name="movie" value="' + swfFile + '">'
        + '<embed src="' + BOSettings.Url.Base.ImageRoot + '/loading/loading10.swf' + '" width="200" height="100"></embed>'
        + '</object>'
        */
        var urlImage = window.location.origin;
        if (window.location.pathname != '/') {
            urlImage += '/Content/Images/loading/loading.gif';
        } else {
            urlImage += 'Content/Images/loading/loading.gif';
        }
        var loadingtag = '<a href="javascript:$.RemoveBigLoading();"><img src="' + urlImage + '" /></a>';

        var loadingElement = $('<div id="bigLoadingElement">'
            + loadingtag
            + '</div>'
            );
        var windowWidth = $(window).width();
        var windowHeight = $(window).height();
        var popupWidth = 100;
        loadingElement.css({
            "position": "absolute",
            "left": windowWidth / 2 - popupWidth / 2,
            "z-index": 2000
        });
        overlayElement.css({
            "width": $(document).width(),
            "height": $(document).height(),
            "z-index": 1500,
            "opacity": 0.5,
        });
        loadingElement.vCenter({ offsetTop: 100 });
        overlayElement.appendTo("body");
        loadingElement.appendTo("body");

    };

    $.RemoveBigLoading = function () {
        $("#bigLoadingElement").remove();
        $("#overlay").remove();
    };

    
});