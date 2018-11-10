$(document).ready(function () {
    $('#btn-search').click();
    $('#txtMsg').keyup(CheckMsg);
    $(".dropdown").hover(
        function () {
            $('.dropdown-menu', this).stop(true, true).slideDown("fast");
            $(this).toggleClass('open');
        },
        function () {
            $('.dropdown-menu', this).stop(true, true).slideUp("fast");
            $(this).toggleClass('open');
        }
    );

    $(window).scroll(function () {
        var position = $('#view-scroll').offset().top;
        var scrollVal = $(this).scrollTop();
        if (scrollVal > position) {
            $('.movie').attr("class", "scrollmovie");
            $('.movie-main').attr("class", "scrollmovie-main");
            $('.movie-main-body').attr("class", "scrollmovie-main-body");

        } else {
            $('.scrollmovie').attr("class", "movie");
            $('.scrollmovie-main').attr("class", "movie-main");
            $('.scrollmovie-main-body').attr("class", "movie-main-body");

        };
    });
   // $('#discussDiv').bind('mousewheel DOMMouseScroll',mouseScroll);   
});


function CheckMsg() {
    if ($('#txtMsg').val() != "") {
        $('#btnSendMsg').removeAttr("disabled");
    } else {
        $('#btnSendMsg').attr("disabled", "disabled");
    }
}

function mouseScroll(e) {
    alert(e.originalEvent.wheelDelta); 
}
function ClearCart() {
    //$.ajax({
    //    type: "GET",
    //    url: '/Cart/ClearCartID',
    //    data: {}
    //}).done(function (result) {

    //    var ids = result.split('/');
    //    for (i = 0; i < ids.length - 1; i++) {
    //        var cart = "#CurrentCart" + ids[i];
    //        $.ajax({
    //            type: 'GET',
    //            url: '/Cart/CurrentCart',
    //            data: { Id: ids[i] }
    //        }).done(function (model) {
    //            $(cart).html(model);
    //        });
    //    }
    //    $.ajax({
    //        type: 'POST',
    //        url: '/Cart/ClearCart',
    //        data: {}
    //    }).done(function (msg) {
    //        $("li#Cart").html(msg);
    //        });
    //    });

    $.ajax({
        type: 'POST',
        url: '/Cart/ClearCart',
        data: {}
    }).done(function (msg) {
        $("li#Cart").html(msg);
        });
    var carts = $('.CartAdded');
    $.each(carts, function (index, value) {
        cartid = this.id;
        $.ajax({
            type: 'GET',
            url: '/Cart/CurrentCart',
            data: { Id: this.id }
        }).done(function (model) {
            var cart = "#CurrentCart" + value.id;
            $(cart).html(model);
        });
    });
}


