// JavaScript Document

/* Thanks to CSS Tricks for pointing out this bit of jQuery
http://css-tricks.com/equal-height-blocks-in-rows/
It"s been modified into a function called at page load and then each time the page is resized. One large modification was to remove the set height before each new calculation. */

var equalheight = function(container) {
    var currentTallest = 0,
        currentRowStart = 0,
        rowDivs = new Array(),
        $el,
        topPosition = 0;

    $(container).each(function() {
        $el = $(this);
        $($el).height("auto");
        topPosition = $el.position().top;

        if (currentRowStart !== topPosition) {
            for (var currentDiv = 0; currentDiv < rowDivs.length; currentDiv++) {
                rowDivs[currentDiv].height(currentTallest);
            }
            rowDivs.length = 0; // empty the array
            currentRowStart = topPosition;
            currentTallest = $el.height();
            rowDivs.push($el);
        } else {
            rowDivs.push($el);
            currentTallest = (currentTallest < $el.height()) ? ($el.height()) : (currentTallest);
        }

        for (var currentDiv = 0; currentDiv < rowDivs.length; currentDiv++) {
            rowDivs[currentDiv].height(currentTallest);
        }
    });
}

$(window).load(function() {
    equalheight(".featureVideoBox");
});

$(window).resize(function() {
    equalheight(".featureVideoBox");
});

$(window).load(function() {
    equalheight(".table-list-group li");
});

$(window).resize(function() {
    equalheight(".table-list-group li");
});

/*--------------------*/

$(window).load(function() {
    equalheight(".main-content");
});

$(window).resize(function() {
    equalheight(".main-content");
});

$(window).load(function() {
    equalheight(".slide-out-div");
});

$(window).resize(function() {
    equalheight(".slide-out-div");
});