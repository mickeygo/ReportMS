/* ReportTransfer
 */

/**
 * @summary     ReportTransfer
 * @description 
 * @version     1.0.0
 * @file        ReportTransfer.js
 * @author      gang.yang
 * @requires    jquery.js;
 * @copyright   Advantech.com
 */

// field ==> condition

function ReportTransfer(element, destContainer) {

    function checkArgs() {
        if (element === undefined || destContainer === undefined) {
            throw "'element' is null or 'destContainer' is null.";
        }
    }

    function getElementDataFormat() {
        checkArgs();

        var target = $(element).attr("data-field-target");
        return $("#" + target).attr("data-field-format");
    }

    function getElementDataArgs() {
        checkArgs();

        var target = $(element).attr("data-field-target");
        return $("#" + target).attr("data-field-key");
    }

    function getElementName() {
        checkArgs();

        var target = $(element).attr("data-field-target");
        return $("#" + target).attr("data-field-name");
    }

    function getElementText() {
        checkArgs();

        var target = $(element).attr("data-field-target");
        return $("#" + target).attr("data-field-text");
    }

    this.lableElement = function () {
        var $span = $("<span data-condition-key=\"true\"></span>");
        var args = getElementDataArgs();
        var name = getElementName();
        $span.attr({ "data-condition-args": args, "data-condition-name": name });
        var text = getElementText();
        $span.text(text);

        return $span;
    }
    
    this.operatorElement = function () {

        var $select = $("<select data-condition-decision=\"true\"></select>");

        var $operatorEqual = $("<option value=\"=\">=</option>");
        var $operatorMore = $("<option value=\">\">></option>");
        var $operatorMoreOrEqual = $("<option value=\">=\">>=</option>");
        var $operatorLess = $("<option value=\"<\"><</option>");
        var $operatorLessOrEqual = $("<option value=\"<=\"><=</option>");
        var $operatorNotEqual = $("<option value=\"<>\"><></option>");

        $operatorEqual.appendTo($select);
        $operatorMore.appendTo($select);
        $operatorMoreOrEqual.appendTo($select);
        $operatorLess.appendTo($select);
        $operatorLessOrEqual.appendTo($select);
        $operatorNotEqual.appendTo($select);

        return $select;
    }

    this.inputElement = function() {
        var $input = $("<input type=\"text\" data-condition-value=\"true\" />");
        var format = getElementDataFormat();
        $input.attr("data-format", format);
        if (format === "date") {
            $input.attr("readonly", "readonly");
        }

        return $input;
    }

    this.removeElement = function () {
        var $delContianerSpan = $("<span class=\"badge\"></span>");
        var $delSpan = $("<span class=\"glyphicon glyphicon-remove\" href=\"#\" data-condition-del=\"true\" role=\"button\"></span>");
        $delSpan.appendTo($delContianerSpan);

        return $delContianerSpan;
    }

    this.wrapperElement = function () {
        return $("<ul class=\"list-group\"></ul>");
    }

    this.generateItem = function () {
        var $wrapperLi = $("<li class=\"list-group-item\"></li>"); // parent

        var $wrapperDiv = $("<div class=\"row\" data-condition=\"true\"></div>"); 

        var $wrapperLabel = $("<div class=\"col-md-2 text-right\"></div>");
        var $wrapperOperator = $("<div class=\"col-md-1 text-right\"></div>");
        var $wrapperInput = $("<div class=\"col-md-5\"></div>");
        var $wrapperDel = $("<div class=\"col-md-2\"></div>");

        var $lable = this.lableElement();
        var $operator = this.operatorElement();
        var $input = this.inputElement();
        var $remove = this.removeElement();

        $lable.appendTo($wrapperLabel);
        $operator.appendTo($wrapperOperator);
        $input.appendTo($wrapperInput);
        $remove.appendTo($wrapperDel);

        $wrapperLabel.appendTo($wrapperDiv);
        $wrapperOperator.appendTo($wrapperDiv);
        $wrapperInput.appendTo($wrapperDiv);
        $wrapperDel.appendTo($wrapperDiv);

        $wrapperDiv.appendTo($wrapperLi);

        return $wrapperLi;
    }

    this.generate = function () {
        var $container = $(destContainer);
        var $wrapper = this.wrapperElement();
        var $item = this.generateItem();

        if ($container.has("ul").length === 0) {
            $item.appendTo($wrapper);
            $wrapper.appendTo($container);
        } else {
            $item.appendTo($container.find("ul"));
        }
    }

    this.delete = function(elem) {
        var $element = $(elem);
        var $li = $($element.parents("li")[0]);
        var $ul = $($element.parents("ul")[0]);
        $li.remove();

        if ($ul.has("li").length === 0) {
            $ul.remove();
        }
    }

    this.destoryConditions = function() {
        var $container = $($("[data-condition=true]").parents("ul")[0]);
        if ($container.length)
            $container.remove();
    }
}