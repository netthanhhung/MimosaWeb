$('.row-warpper.select').live('click', function () {
    var price_bigin = $('.left-contact-content .price').attr('price');
    var price_global = $('.left-contact-content .price').attr('price_global');
    var heso = $('label.ui-state-active').attr('heso');
    //alert(price);

    $("#BookRoomForm input[name='service']").remove();
    $("#BookRoomForm input[name='equipment']").remove();
    if ($(this).find('span.price-equipment').hasClass('active') || $(this).find('span.price-service').hasClass('active')) {
        $(this).find('span.price-equipment').removeClass('active');
        $(this).find('span.price-service').removeClass('active');
        $(this).removeClass('active');
        var price_ = 0;
        var limit = $('.row-warpper.select.active').length;
        //alert(limit);

        for (var i = 0; i < limit; i++) {
            var item = $('.row-warpper.select.active').eq(i);

            if (item.attr("tableName") == "service") {
                input = "<input type='hidden' name='service' value='" + item.attr("idTable") + "' />";
            } else {
                input = "<input type='hidden' name='equipment' value='" + item.attr("idTable") + "' />";
            }
            $("#BookRoomForm").append(input);

            price_ = parseInt(price_) + parseInt(item.attr('price'));

        }
        //alert(price_);
        var price_tiennghi = parseInt(price_);
        total_price = (parseInt(price_global) + parseInt(price_tiennghi)) * heso;
        total_price_6 = (parseInt(price_global) + parseInt(price_tiennghi)) * 1;
        total_price_3 = (parseInt(price_global) + parseInt(price_tiennghi)) * 1.1;
        total_price_1 = (parseInt(price_global) + parseInt(price_tiennghi)) * 1.2;
        $('.month.one-month').attr('price-default', total_price_1);
        $('.month.three-month').attr('price-default', total_price_3);
        $('.month.six-month').attr('price-default', total_price_6);
        total_prices = accounting.formatNumber(total_price, 0, ",");
        $('.left-contact-content .price').attr('price', total_price);
        //$('.month.six-month').attr('price-default', total_price);
        $('.left-contact-content .price').empty().html('Giá thuê: ' + total_prices + ' đ/tháng');
    } else {
        $(this).find('span.price-equipment').addClass('active');
        $(this).find('span.price-service').addClass('active');
        $(this).addClass('active');
        var price_ = 0;
        var limit = $('.row-warpper.select.active').length;
        //alert(limit);
        var input = "";
        for (var i = 0; i < limit; i++) {
            var item = $('.row-warpper.select.active').eq(i);

            if (item.attr("tableName") == "service") {
                input = "<input type='hidden' name='service' value='" + item.attr("idTable") + "' />";
            } else {
                input = "<input type='hidden' name='equipment' value='" + item.attr("idTable") + "' />";
            }
            $("#BookRoomForm").append(input);
            price_ = parseInt(price_) + parseInt(item.attr('price'));
        }
        //alert(price_);
        var price_tiennghi = parseInt(price_);
        total_price = (parseInt(price_global) + parseInt(price_tiennghi)) * heso;
        total_price_6 = (parseInt(price_global) + parseInt(price_tiennghi)) * 1;
        total_price_3 = (parseInt(price_global) + parseInt(price_tiennghi)) * 1.1;
        total_price_1 = (parseInt(price_global) + parseInt(price_tiennghi)) * 1.2;
        $('.month.one-month').attr('price-default', total_price_1);
        $('.month.three-month').attr('price-default', total_price_3);
        $('.month.six-month').attr('price-default', total_price_6);
        total_prices = accounting.formatNumber(total_price, 0, ",");
        $('.left-contact-content .price').attr('price', total_price);
        //$('.month.six-month').attr('price-default', total_price);
        $('.left-contact-content .price').empty().html('Giá thuê: ' + total_prices + ' đ/tháng');
        //alert(total_prices);
    }
});
if ($('.month').length) {
    $('.month').click(function () {
        var price_mac_dich = $(this).attr('price-default');
        price_three_month = parseInt(price_mac_dich);
        $('.left-contact-content .price').attr('price', price_three_month);
        total_prices = accounting.formatNumber(price_three_month, 0, ",");
        $('.left-contact-content .price').empty().html('Giá thuê: ' + total_prices + ' đ/tháng');
    });
}