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

// data format

// Date
$(document).on("focusin", "input[data-format=date]", function (e) {
    $(this).datepicker({
        changeMonth: true,
        changeYear: true
    });
});

// String
$(document).on("keypress", "input[data-format=string]", function (e) {
    return e.keyCode !== keycodeExpression.comma
            && e.keyCode !== keycodeExpression.semicolon
            && e.keyCode !== keycodeExpression.whitespace
            && e.keyCode !== keycodeExpression.enter
            && e.keyCode !== keycodeExpression.newline;
});

// Integer
$(document).on("keypress", "input[data-format=integer]", function (e) {
    return (keycodeExpression.zero <= e.keyCode && e.keyCode <= keycodeExpression.nine)
            || e.keyCode === keycodeExpression.minus;
});

// Number
$(document).on("keypress", "input[data-format=number]", function (e) {
    return (keycodeExpression.zero <= e.keyCode && e.keyCode <= keycodeExpression.nine)
            || e.keyCode === keycodeExpression.minus
            || e.keyCode === keycodeExpression.dot;
});