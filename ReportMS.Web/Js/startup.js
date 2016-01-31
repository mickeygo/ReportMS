/*===== data format =====*/
// keycode
var keycodeExpression = {
    enter: 13,
    newline: 10,
    zero: 48,   
    nine: 57,   
    whitespace: 32,
    comma: 44,
    minus: 45,
    dot: 46,
    semicolon: 59
};

// Date
$(document).on("focusin", "input[data-format=date]", function (e) {
    $(this).datepicker({
        changeMonth: true,
        changeYear: true
    });
});

// String [0-9a-zA-Z_]
$(document).on("keypress", "input[data-format=string]", function (e) {
    return e.keyCode !== keycodeExpression.comma
            && e.keyCode !== keycodeExpression.semicolon
            && e.keyCode !== keycodeExpression.whitespace
            && e.keyCode !== keycodeExpression.enter
            && e.keyCode !== keycodeExpression.newline;
});

// Integer [0-9-]
$(document).on("keypress", "input[data-format=integer]", function (e) {
    return (keycodeExpression.zero <= e.keyCode && e.keyCode <= keycodeExpression.nine)
            || e.keyCode === keycodeExpression.minus;
});

// Number [0-9.-]
$(document).on("keypress", "input[data-format=number]", function (e) {
    return (keycodeExpression.zero <= e.keyCode && e.keyCode <= keycodeExpression.nine)
            || e.keyCode === keycodeExpression.minus
            || e.keyCode === keycodeExpression.dot;
});

/*===== dataTable =====*/
$(document).one("load", "table[data-datatable=true]", function (e) {
    // data-datatable-paging true/false
    // data-datatable-sort true/false
    // data-datatable-searching true/false
    // more options, see http://datatables.net/reference/option/
    $(this).DataTable({
        responsive: true,
        lengthChange: false,
        searching: false,
        processing: true,
        paging: true,
        sort: false
    });
});

/*===== Menu =====*/
$(function () {
    $("#accordion").accordion({
        heightStyle: "content",
        active: false,
        collapsible: true
    });

    $(".sub-menu-link").on("click", function (e) {
        e.preventDefault();
        var link = $(this).attr("href");
        location.href = link;
    });

    /* Contact Panel */
    $("#accordion").accordion({
        heightStyle: "content",
        active: false,
        collapsible: true,
        targetClass: "main-content-wrapper",
        tabSlideOutClass: "slide-out-div",
        menuTitleClass: "main-menu-title",
        footerClass: "footer-bg"
    });

    $(".slide-out-div").tabSlideOut({
        tabHandle: ".handle",                                           //class of the element that will become your tab
        pathToTabImage: "/Content/themes/main-menu.png",                //path to the image for the tab //Optionally can be set using css
        imageHeight: "170px",                                           //height of tab image           //Optionally can be set using css
        imageWidth: "36px",                                             //width of tab image            //Optionally can be set using css
        tabLocation: "left",                                            //side of screen where tab lives, top, right, bottom, or left
        speed: 300,                                                     //speed of animation
        action: "click",                                                //options: 'click' or 'hover', action to trigger animation
        topPos: "20px",                                                 //position from the top/ use if tabLocation is left or right
        leftPos: "0px",                                                 //position from left/ use if tabLocation is bottom or top
        fixedPosition: false                                            //options: true makes it stick(fixed position) on scroll
    });
    /* End Contact Panel */
});

/* ===== Global ===== */
$(function () {
    $("[data-toggle='tooltip']").tooltip();
});

/* ===== End Global ===== */