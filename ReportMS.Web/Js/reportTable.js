/* ReportTable */

/**
 * @summary     ReportTable
 * @description 
 * @version     1.0.0
 * @file        ReportTable.js
 * @author      gang.yang
 * @requires    jquery.js; jquery.dataTables.js
 * @copyright   Advantech.com
 */

function ReportTable(container, table) {

    this.container = container;
    this.table = table == undefined || $.trim(table) === "" ? "#datatable" : table;
    this.scrollYHeight = 400;

    function getPostData(datas) {

        // Conditions
        $("[data-condition=true]").each(function (i) {
            var name = $(this).find("[data-condition-key=true]").attr("data-condition-name");
            var alias = $(this).find("[data-condition-key=true]").attr("data-condition-args");
            var decision = $(this).find("[data-condition-decision=true]").val();
            var value = $(this).find("[data-condition-value=true]").val();

            var key = alias;
            // name,decision,value;name,decision,value
            if (datas[key] === undefined) {
                datas[key] = name + "," + decision + "," + value;
            } else {
                var val = datas[key];
                datas[key] = val + ";" + name + "," + decision + "," + value;
            }
        });

        // TableOrView Id
        datas["T0"] = $("[data-report-option=table] option:selected").attr("data-report-id");

        // Fields
        $("[data-field=true] input:checked[data-field-key]").each(function (i) {
            var key = "F" + (i + 1);
            var param = $(this).attr("data-field-name"); // field
            var alias = $(this).attr("data-field-key"); // alias
            datas[key] = param + "," + alias;
        });

        return datas;
    }

    this.getPostData = function() {
        var data = {};
        return getPostData(data);
    }

    this.setPostData = function (data) {
        getPostData(data);
    }

    this.setColumnData = function () {
        var columnsData = [];
        $("[data-field=true] input:checked[data-field-key]").each(function (i) {
            var args = $(this).attr("data-field-key");
            columnsData.push({ "data": args });
        });

        return columnsData;
    }

    function getColumnNames() {
        var columns = [];
        $("[data-field=true] input:checked[data-field-key]").each(function (i) {
            var name = $(this).attr("data-field-name");
            columns.push(name);
        });

        return columns;
    }

    function getElementId(element) {
        if (element.indexOf("#") === 0) {
            return element.substring(1);
        }
        return element;
    }

    this.setDataTable = function () {
        var $container = jQuery(this.container);
        $container.children().remove();

        var $table = $("<table style=\"width: 100%;\"></table>");
        $table.attr("id", getElementId(this.table));
        $table.addClass("display");

        var $thead = $("<thead></thead>");
        var $tr = $("<tr></tr>");

        var columns = getColumnNames();
        for (var i in columns) {
            var $th = $("<th></th>");
            $th.text(columns[i]);
            $th.appendTo($tr);
        }

        $tr.appendTo($thead);
        $thead.appendTo($table);
        $table.appendTo($container);
    }

    function checkColumnsData(columnsData) {
        return columnsData != undefined && columnsData.length > 0;
    }

    this.addCssForTable = function(id) {
        var dtId = (id === undefined ? this.table : id) + "_wrapper";
        var css = "table table-bordered table-striped table-hover";
        $(dtId + " table[role=grid]").addClass(function (index, currentClass) {
            $(this).addClass(css);
        });
    }

    var scrollY = this.scrollYHeight;
    this.showTableOption = function (id, url, postData, columnsData, scrollYHeight) {
        if (!checkColumnsData(columnsData)) {
            return;
        }
        if (scrollYHeight !== undefined) {
            scrollY = scrollYHeight;
        }

        $(id).DataTable({
            responsive: true,
            lengthChange: false,
            searching: false,
            destroy: true,
            processing: true,
            serverSide: true,
            scrollY: scrollY,
            scrollCollapse: true,
            scrollX: true,
            paging: true,
            paginationType: "full_numbers",
            sort: false,
            //stateSave: true,
            ajax: {
                url: url,
                type: "POST",
                data: postData
            },
            columns: columnsData
        });

        this.addCssForTable(id);
    }

    this.showTable = function (url, scrollYHeight) {
        this.setDataTable(this.container);
        var columnData = this.setColumnData();
        this.showTableOption(this.table, url, this.setPostData, columnData, scrollYHeight);
    }

    this.destoryTable = function (id) {
        var dtId = (id === undefined ? this.table : id) + "_wrapper";
        //$("#datatable").dataTable().fnDestroy();
        $(dtId).remove();
    }
}

/// toggle select all fields
function toggleCheckAllField(element) {
    var isChecked = $(element).is(":checked");

    $("[data-field=true] input:checkbox[data-field-key]").each(function () {
        $(this).prop({ "checked": isChecked });
    });
}


/* ReportField */

/**
 * @summary     ReportField
 * @description 
 * @version     1.0.0
 * @file        ReportField.js
 * @author      gang.yang
 * @requires    jquery.js;
 * @copyright   Advantech.com
 */
function ReportField(container) {
    this.container = "#" + container;

    var fieldHtml = function (name, displayName, format, key) {
        var $content = $("<div class=\"col-md-9\"></div>");
        var $contentInput = $("<input type=\"checkbox\"></div>");
        $contentInput.attr({ "id": name, "data-field-key": key, "data-field-name": name, "data-field-text": displayName, "data-field-format": format });
        var $contentLabel = $("<label></label>");
        $contentLabel.attr("for", name);
        $contentLabel.text(displayName);
        $contentInput.appendTo($content);
        $contentLabel.appendTo($content);

        var $plus = $("<div class=\"col-md-1\"></div>");
        var $plusSpan = $("<span class=\"glyphicon glyphicon-plus\"></span>");
        $plusSpan.attr("data-field-target", name);
        $plusSpan.appendTo($plus);

        var $row = $("<div class=\"row\" data-field=\"true\"></div>");
        $content.appendTo($row);
        $plus.appendTo($row);

        return $row;
    }

    this.destoryFields = function () {
        var $fieldContainer = $(this.container);

        if ($fieldContainer.has("[data-option=reportfield_wrapper]").length > 0) {
            $fieldContainer.find("[data-option=reportfield_wrapper]").remove();
        }
    }

    this.generateFields = function (fields) {
        var $fieldContainer = $(this.container);

        if ($fieldContainer.has("[data-option=reportfield_wrapper]").length > 0) {
            $fieldContainer.find("[data-option=reportfield_wrapper]").remove();
        }

        var $filedContent = $("<div data-option=\"reportfield_wrapper\"></div>");
        for (var i in fields) {
            var name = fields[i].FieldName;
            var displayName = fields[i].DisplayName;
            var format = fields[i].DataType;
            var index = fields[i].Sort;
            var key = "P" + index;
            var $filedRow = fieldHtml(name, displayName, format, key);
            $filedRow.appendTo($filedContent);
        }

        $filedContent.appendTo($fieldContainer);
    }
}


/**
 * @summary     ReportDownload
 * @description 
 * @version     1.0.0
 * @file        ReportDownload.js
 * @author      gang.yang
 * @requires    jquery.js;
 * @copyright   Advantech.com
 */

/// downloadUrl, 执行下载文件的 Url
function ReportDownload(downloadUrl) {
    this.report = new ReportTable();

    this.checkColumnsBefore = function () {
        var columns = this.report.setColumnData();
        return columns != undefined && columns.length > 0;
    }

    this.downLoad = function () {
        if (!this.checkColumnsBefore()) {
            alert("There are not any fields to download.");
            return false;
        }

        var postData = this.report.getPostData();

        $.ajax({
            async: false,
            type: "POST",
            url: downloadUrl,
            data: postData,
            dataType: "json"
        }).fail(function () {
            alert("Download the excel failure.");
            return false;
        }).always(function (data) {
            if (data.status === "success")
                return true;

            if (data.status === "failure") 
                alert("Download the file failure.");
            
            return false;
        });
    }
}