/* Created by gang.yang, at 2015-04-27 */
/* reference jquery.js, bootstrap 3.0+, jquery.form.js, jquery.unobtrusive-ajax.js */

/*
	tag eg:
	<element data-dialog="true" data-dialog-href="" data-dialog-title="" data-dialog-size=""
        data-dialog-loading="callback" data-dialog-loaded="callback"
        data-ajax-success="callback" data-ajax-failure="callback" />

	form eg:
	<form data-ajax-form="true" data-ajax-begin="function" data-ajax-success="callback" data-ajax-failure="callback">
	</form>
*/

(function ($) {
    function getFunction(code, argNames) {
        var fn = window, parts = (code || "").split(".");
        while (fn && parts.length) {
            fn = fn[parts.shift()];
        }
        if (typeof (fn) === "function") {
            return fn;
        }
        argNames.push(code);
        return Function.constructor.apply(null, argNames);
    }

    var modalDialog = {
        title: null,
        id: null,
        href: null,
        modalId: null,
        size: "", // lg, sm
        init: function(element) {
            this.title = typeof (element.attr("data-dialog-title")) === "undefined" ? "Message" : element.attr("data-dialog-title");
            this.id = typeof (element.attr("id")) === "undefined" ? "modal_dialog" : element.attr("id");
            this.href = element.attr("data-dialog-href");
            this.modalId = "#dialog_" + this.id;

            if (typeof (element.attr("data-dialog-size")) !== "undefined") {
                var m_size = element.attr("data-dialog-size");
                if ($.inArray(m_size, ["lg", "sm"]) !== -1) {
                    this.size = "modal-" + m_size;
                }
            }

            if (!$(this.modalId).is("div")) {
                $("body").append(this.getHtml());
            } else {
                $(this.modalId).find(".modal-title").text(this.title);
            }
        },
        getHtml: function() {
            var html = "<div class=\"modal fade\" id=\"dialog_" + this.id + "\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"dialogModalLabel\" aria-hidden=\"true\">";
            html += "       <div class=\"modal-dialog " + this.size + "\">";
            html += "           <div class=\"modal-content\">";
            html += "                <div class=\"modal-header\">";
            html += "                   <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>";
            html += "                   <h4 class=\"modal-title\" id=\"dialogModalLabel\">" + this.title + "</h4>";
            html += "                </div>";
            html += "                <div class=\"modal-body\"></div>";
            html += "                <div class=\"modal-footer\">";
            html += "                   <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Close</button>";
            html += "               </div>";
            html += "           </div>";
            html += "       </div>";
            html += "   </div>";

            return html;
        },
        loading: function(element) {
            var attr = "data-dialog-loading";
            if (typeof (element.attr(attr)) !== "undefined")
                getFunction(element.attr(attr), null).apply(element, arguments);
        },
        loaded: function(element) {
            var attr = "data-dialog-loaded";
            if (typeof (element.attr(attr)) !== "undefined")
                getFunction(element.attr(attr), null).apply(element, arguments);
        },
        show: function(element) {
            // loading
            this.loading(element);

            this.init(element);
            element.attr({ "data-toggle": "modal", "data-target": this.modalId, "data-backdrop": "static" });

            var $body = $(this.modalId + " .modal-body");
            $body.children().remove();
            // replace $.load, the $.get is more flexible.
            $.get(this.href, function(data, status, xhr) {
                    if (status === "error") {
                        $body.text("Remote request fail, please try again.");
                    } else {
                        $body.html(data);

                        var attach = element.attr("data-attachment");
                        if (attach) {
                            $body.find("form input:submit").attr({ "data-attachment": attach });
                        }
                        if (element.attr("data-ajax-form") === "true") {
                            var form = $body.find("form");
                            form.attr({ "data-ajax-form": "true" });
                            form.attr({ "data-ajax-begin": element.attr("data-ajax-begin") });
                            form.attr({ "data-ajax-success": element.attr("data-ajax-success") });
                            form.attr({ "data-ajax-failure": element.attr("data-ajax-failure") });
                        }
                    }
                }).fail(function() {
                    $body.text("Remote request fail, please try again.");
                })
                .always(function() {
                    modalDialog.loaded(element);
                });

            $(this.modalId).modal("show").css({
                width: "auto"
            });; // 加载后立即显示 dialog
        }
    };

    $.fn.dialog = function () {
        modalDialog.show($(this));
    };

    $(document).on("click", "[data-dialog=true]", function (evt) {
        $(this).dialog();
    });

    // form ajax submit
    $(document).on("submit", "form[data-ajax-form=true]", function(evt) {
        var element = $(this);
        var option = {
            dataType: "json",
            beforeSubmit: function(arr, form, options) {
                getFunction(element.attr("data-ajax-begin"), ["arr", "form", "options"]).apply(element, arguments);
            },
            success: function(data) {
                getFunction(element.attr("data-ajax-success"), ["data"]).apply(element, arguments);
            },
            error: function() {
                getFunction(element.attr("data-ajax-failure"), null).apply(element, arguments);
            }
        };

        element.ajaxSubmit(option);
        $(element.parents("div[role=dialog]")[0]).modal("hide");

        return false;
    });

    // alert
    var alertDialog = {
        id: null,
        model: null,
        message: null,
        modalId: null,
        index: 0,
        getAlertHtml: function () {
            var topSpan = this.index * 60 + "px";
            var style = "style=\"position: absolute; top: " + topSpan + "; width: 100%; text-align: center; z-index: 100";

            var html = "<div class=\"alert alert-" + this.model + "\" id=\"dialog_alert_" + this.id + "\" " + style + " role=\"alert\">";
            html += "       <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>";
            html +=         "<strong>" + this.message + "</strong>";
            html += "   </div>";

            return html;
        },
        init: function(id, model, message) {
            this.id = id + "_" + (this.index++);
            this.model = model;
            this.message = message;
            this.modalId = "#" + this.id;

            $("body").prepend(this.getAlertHtml());
        },
        show: function(id, model, message) {
            this.init(id, model, message);
        }
    };

    $(document).on("click", "button.close[data-dismiss=alert]", function (evt) {
        alertDialog.index--;
        $.format();
    });

    $.alertSuccess = function(message) {
        var m = "success";
        alertDialog.show(m, m, message);
    }
    $.alertInfo = function(message) {
        var m = "info";
        alertDialog.show(m, m, message);
    }
    $.alertWarning = function(message) {
        var m = "warning";
        alertDialog.show(m, m, message);
    }
    $.alertDanger = function(message) {
        var m = "danger";
        alertDialog.show(m, m, message);
    }
})(jQuery);