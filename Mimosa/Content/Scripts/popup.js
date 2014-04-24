
$.BigLoading = function (popupdiv) {
    var overlayElement = $('#overlay');
    if (overlayElement.length == 0) {
        overlayElement = $("<div id='overlay'></div>");
        overlayElement.css({
            "position": "absolute",
            "background-color": "#000000",
            "opacity": 0.9,
            "top": 0,
            "left": 0,
            "width": $(document).width(),
            "height": $(document).height(),
            "z-index": 999
        });
        overlayElement.appendTo("body");
    }

    $(popupdiv).css({
        "position": "absolute",
        //"top": (screen.height - $(popupdiv).outerHeight()) / 2,
        "top": 5, //75
        "left": (screen.width - $(popupdiv).outerWidth()) / 2,
        "z-index": 9999
    });

    overlayElement.show();
    $(popupdiv + " input[type='text']").removeClass("input-validation-error");
    $(popupdiv).show();
    $(popupdiv).css("position", "fixed");

    var closeButton = $(popupdiv).find('.popup-close');
    if (closeButton.length > 0) {
        closeButton.bind("click", function () {
            hidePopup(popupdiv);
        });
    }

    $("#overlay").bind("click", function () {
        hidePopup(popupdiv);
    });

};

function showPopup(popupid) {
    $.BigLoading(popupid);
    return false;
}

function hidePopup(popupid) {
    $("#overlay").remove();
    $(popupid).hide();
}
