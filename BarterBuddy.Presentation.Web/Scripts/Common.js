var Messages = {
    SuccessMessage: function (title, message) {
        window.BootstrapDialog.show({
            title: title,
            message: message
        });
    },
    ErrorMessage: function (title, message) {
        if (message.length > 0) {
            window.BootstrapDialog.show({
                title: title,
                message: message
            });
        }
    },
    InformativeMessage: function (title, message) {
        window.BootstrapDialog.show({
            title: title,
            message: message
        });
    },
    AlertMessage: function (title, message, callback) {
        if (message.length > 0) {
            window.BootstrapDialog.show({
                title: title,
                message: message,
                buttons:
                [{
                    label: 'Ok',
                    cssClass: 'btn-primary',
                    action: function (dialog) {
                        dialog.close();
                        callback(true);
                    }

                }]
            });
        }
    },

    ConfirmationMessage: function (title, message, callback) {
        window.BootstrapDialog.show({
            title: title,
            message: message,
            buttons:
            [{
                label: 'Continue.',
                cssClass: 'btn-primary',
                action: function (dialog) {
                    dialog.close();
                    callback(true);
                }

            }]
        });

    },
    ConfirmationMessagetYesNo: function (title, message, callback) {
        window.BootstrapDialog.show({
            title: title,
            message: message,
            buttons:
            [{
                label: 'Yes',
                cssClass: 'btn-primary',
                action: function (dialog) {
                    dialog.close();
                    callback(true);
                }

            },
            {
                label: 'No',
                cssClass: 'btn-primary',
                action: function (dialog) {
                    dialog.close();
                    callback(false);
                }
            }
            ]
        });
    },
    ConfirmationMessagetYesNoCancle: function (title, message, callback) {
        window.BootstrapDialog.show({
            title: title,
            message: message,
            buttons:
            [{
                label: 'Yes',
                cssClass: 'btn-primary',
                action: function (dialog) {
                    dialog.close();
                    callback(1);
                }

            },
            {
                label: 'No',
                cssClass: 'btn-primary',
                action: function (dialog) {
                    callback(0);
                    dialog.close();

                }
            },
             {
                 label: 'cancel',
                 cssClass: 'btn-primary',
                 action: function (dialog) {
                     dialog.close();
                 }
             }
            ]
        });
    }

};




function ValidateControls() {
    $("#div").attr("required:true");
}

$.fn.ValidateAllControls = function (errorTitle) {
    var errorMessages = "";
    $(this[0]).find(" [Required=true]").each(function () {
        if ($(this).is("input")) {
            if ($.trim($(this).val()) === "") {
                errorMessages = errorMessages + " <li> " + $(this).attr("RequiredMessage") + "</li>";
            }
        }
        else if ($(this).is("select")) {
            if ($.trim($(this).val()) === "" || $.trim($(this).val()) === -1 || $.trim($(this).val()) === "-1") {
                errorMessages = errorMessages + " <li> " + $(this).attr("RequiredMessage") + "</li>";
            }
        }
        else if ($(this).is("textarea")) {
            if ($.trim($(this).val()) === "" || $.trim($(this).val()) === -1 || $.trim($(this).val()) === "-1") {
                errorMessages = errorMessages + " <li> " + $(this).attr("RequiredMessage") + "</li>";
                return false;
            }
        }
    });
    $(this[0]).find(" [RegularExpression]").each(function () {
        if ($(this).is("input")) {
            if ($.trim($(this).val()) !== "") {
                var regType = $(this).attr("RegExpType");
                var regularExpression = "";
                switch (regType.toUpperCase()) {
                    case "EMAIL":
                        regularExpression = "^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$";
                        break;
                    case "MOBILE":
                        regularExpression = "\\(?([0-9]{3})\\)?([ .-]?)([0-9]{3})\\2([0-9]{4})";
                        break;
                    case "PERSONPIN":
                        regularExpression = "^[1-9][0-9]{4,10}$";
                        break;
                }
                if (!new RegExp(regularExpression).test($.trim($(this).val()))) {
                    errorMessages = errorMessages + " <li> " + $(this).attr("RegExpMessage") + "</li>";
                }
                //if ($.trim($(this).val()) != "" && !new RegExp($(this).attr("RegularExpression")).test($.trim($(this).val())))
                //{
                //    errorMessages = errorMessages + " <li> " + $(this).attr("RegExpMessage") + "</li>";
                //}
            }
        }
    });

    if (errorMessages.length > 0) {
        if (errorTitle !== "")
            Messages.ErrorMessage(errorTitle, "<ul>" + errorMessages + "</ul>");
        else
            Messages.ErrorMessage("Error", "<ul>" + errorMessages + "</ul>");
        return false;
    }
    return true;
};
$(document).ready(function () {
    $("form").bind("submit", function (event) {
        if (!$(this).ValidateAllControls()) {
            event.preventDefault();
        }
    });

});

function uppercase(val) {
    return val.toString().trim().toUpperCase();
}

//Added By Parimal to validate Control which have common message.
$.fn.ValidateAllControlsForCommonMessage = function (errorTitle) {
    var errorMessages = "";
    $(this[0]).find(" [Required=true]").each(function () {
        if ($(this).is("input")) {
            if ($.trim($(this).val()) === "") {
                errorMessages = errorMessages + " <li> " + $(this).attr("RequiredMessage") + "</li>";
                return false;
            }
        }
        else if ($(this).is("select")) {
            if ($.trim($(this).val()) === "" || $.trim($(this).val()) === -1 || $.trim($(this).val()) === "-1") {
                errorMessages = errorMessages + " <li> " + $(this).attr("RequiredMessage") + "</li>";
                return false;
            }
        }
        else if ($(this).is("textarea")) {
            if ($.trim($(this).val()) === "" || $.trim($(this).val()) === -1 || $.trim($(this).val()) === "-1") {
                errorMessages = errorMessages + " <li> " + $(this).attr("RequiredMessage") + "</li>";
                return false;
            }
        }
        return true;
    });
    $(this[0]).find(" [RegularExpression]").each(function () {
        if ($(this).is("input")) {
            if ($.trim($(this).val()) !== "") {
                var regType = $(this).attr("RegExpType");
                var regularExpression = "";
                switch (regType.toUpperCase()) {
                    case "EMAIL":
                        regularExpression = "^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$";
                        break;
                    case "MOBILE":
                        regularExpression = "\\(?([0-9]{3})\\)?([ .-]?)([0-9]{3})\\2([0-9]{4})";
                        break;
                }
                if (!new RegExp(regularExpression).test($.trim($(this).val()))) {
                    errorMessages = errorMessages + " <li> " + $(this).attr("RegExpMessage") + "</li>";
                }
            }
        }
    });

    if (errorMessages.length > 0) {
        if (errorTitle !== "")
            Messages.ErrorMessage(errorTitle, "<ul>" + errorMessages + "</ul>");
        else
            Messages.ErrorMessage("Error", "<ul>" + errorMessages + "</ul>");
        return false;
    }
    return true;
};
var AccessLevelEnum;
(function (accessLevelEnum) {
    accessLevelEnum[accessLevelEnum["FullAccess"] = 0] = "vollen Zugriff"; // All CRUD
    accessLevelEnum[accessLevelEnum["ReadOnly"] = 1] = "Nur lesen keine Änderungen"; // Only View
    accessLevelEnum[accessLevelEnum["Not_Visible"] = 2] = "Nicht sichtbar"; // No rights

    accessLevelEnum[accessLevelEnum["Visible_Not_Executable"] = 3] = "sichtbar aber nicht ausführbar"; // Only View
    accessLevelEnum[accessLevelEnum["No_Delete_or_Recreating"] = 4] = "Kein Löschen oder Neuanlegen"; // Only View Edit
    accessLevelEnum[accessLevelEnum["No_Delete"] = 5] = "Kein Löschen"; // Only Add view edit
    accessLevelEnum[accessLevelEnum["No_Rebuilding"] = 6] = "Kein Neuanlegen"; // Only View edit Delete

})(AccessLevelEnum || (AccessLevelEnum = {}));
var AccessLevelEnum;

$.fn.SetAccessLevel = function () {
    $(this[0]).find(" [AccessLevel]").each(function () {
        var accessLevel = $(this).attr("AccessLevel");
        switch (parseInt(accessLevel)) {
            case AccessLevelEnum.FullAccess:
                $(this).addClass('fieldAccess0');
                break;
            case AccessLevelEnum.ReadOnly:
                $(this).find("input, select, button, a, textarea,span,div,img,table,li").attr("disabled", true);
                $(this).css("pointer-events", "auto");
                $(this).css("cursor", "pointer !important");
                $(this).addClass('fieldAccess1');
                //$(this).children().attr("disabled", true);
                break;
            case AccessLevelEnum.Not_Visible:
                $(this).addClass('fieldAccess2');
                $(this).css("display", "none !important");

                break;
        }
    });

    $(this[0]).find(" [ModuleAccessLevel]").each(function () {
        var accessLevel = $(this).attr("ModuleAccessLevel");
        var actionType = $(this).attr("ActionType");
        switch (parseInt(accessLevel)) {

            case AccessLevelEnum.FullAccess:
                break;
            case AccessLevelEnum.ReadOnly:
                if (actionType.toUpperCase() !== "VIEW") {
                    $(this).addClass("moduleAccess");
                }
                break;
            case AccessLevelEnum.Not_Visible:
                $(this).addClass("moduleAccess");
                break;
            case AccessLevelEnum.Visible_Not_Executable:
                $(this).addClass("moduleAccess");
                break;

            case AccessLevelEnum.No_Delete_or_Recreating:
                if (actionType.toUpperCase() !== "VIEW" && actionType.toUpperCase() !== "EDIT") {
                    $(this).addClass("moduleAccess");
                }
                break;

            case AccessLevelEnum.No_Delete:
                if (actionType.toUpperCase() === "DELETE") {
                    $(this).addClass("moduleAccess");
                }
                break;

            case AccessLevelEnum.No_Rebuilding:
                if (actionType.toUpperCase() === "ADD") {
                    $(this).addClass("moduleAccess");
                }
                break;
        }
    });

    return true;
};

function Gridheight() {
    //$("#employeeDetailForm").outerHeight(true)
    var windowheight = $(window).height();
    var topElemHeight = $("header").outerHeight(true) + $("footer").outerHeight(true) + $("#tabbutton").outerHeight(true) + $("#NavTabheader").outerHeight(true) + $(".ui-search-toolbar").outerHeight(true) + $(".ui-jqgrid-hdiv").outerHeight(true) + $(".ui-jqgrid-pager").outerHeight(true) + 70;
    if ($("#bodyfooter").css("display") === "block") {
        topElemHeight = topElemHeight + $("#bodyfooter").outerHeight(true) - 15;
    }
    return (windowheight - topElemHeight - 150);
}

function leftPanelHeight() {
    setTimeout(function () {
        var windowheight = $(window).height();
        var topElemHeight = $("header").outerHeight(true) + $("footer").outerHeight(true) + $(".blue-bg-title").outerHeight(true) + 70; // + $(".themeeditforminput").outerHeight(true) + $(".theme-cusomize-footer").outerHeight(true);
        if ($("#bodyfooterLeft").css("display") === "block") {
            topElemHeight = topElemHeight + $("#bodyfooterLeft").outerHeight(true) - 15;
        }
        $(".recently-update-list").css("height", windowheight - topElemHeight - 1);
        $(".ui-jqgrid-bdiv").niceScroll({ touchbehavior: false, cursorcolor: "#084e9d", cursoropacitymax: 0.9, cursorwidth: 8, cursorborder: "1px solid #084e9d", cursorborderradius: "8px", background: "#ccc" });
    }, 200);
}

function rightContainerHeight() {
    //$("#employeeDetailForm").outerHeight(true)
    var windowheight = $(window).height();
    var topElemHeight = $("header").outerHeight(true) + $("footer").outerHeight(true) + $("#tabbutton").outerHeight(true) + $("#NavTabheader").outerHeight(true) + 70; // + $(".themeeditforminput").outerHeight(true) + $(".theme-cusomize-footer").outerHeight(true);
    if ($("#bodyfooterRight").css("display") === "block") {
        topElemHeight = topElemHeight + $("#bodyfooterRight").outerHeight(true) - 15;
    } else {
        if ($("#bodyfooterLeft").css("display") === "block") {
            topElemHeight = topElemHeight - 10;
        }

    }

    Pageheight = windowheight - topElemHeight - 1;
    $(".tab-content-wrapper").css("height", windowheight - topElemHeight - 1);
    //$("#InnerTab .tab-content-wrapper").css("height", windowheight - topElemHeight - 120);

}

function rightSubTabContainerHeight(tabheight) {
    var windowheight = $(window).height();
    var topElemHeight = $("header").outerHeight(true) + $("footer").outerHeight(true) + $("#tabbutton").outerHeight(true) + $("#NavTabheader").outerHeight(true) + 70; // + $(".themeeditforminput").outerHeight(true) + $(".theme-cusomize-footer").outerHeight(true);
    if ($("#bodyfooterRight").css("display") === "block") {
        topElemHeight = topElemHeight + $("#bodyfooterRight").outerHeight(true) - 15;
    } else {
        if ($("#bodyfooterLeft").css("display") === "block") {
            topElemHeight = topElemHeight - 10;
        }
    }
    Pageheight = windowheight - topElemHeight - 1;
    $(".tab-content-wrapper").css("height", windowheight - topElemHeight - 1);
    $("#InnerTab .tab-content-wrapper").css("height", windowheight - topElemHeight - tabheight - $("#NavTabheaderInner").outerHeight(true));
}

$.fn.ModelContainerHeight = function () {
    var windowheight = $(window).height();
    var modelHeight = windowheight - 200;
    var model = $(this).find(".modal-body");
    model.css("max-height", modelHeight);
    model.css("height", modelHeight);
    $(this).find(".modal-body").niceScroll({ horizrailenabled: false, touchbehavior: false, cursorcolor: "#084e9d", cursoropacitymax: 0.9, cursorwidth: 8, zindex: 100000000000000000, cursorborder: "1px solid #084e9d", cursorborderradius: "8px", background: "#ccc" });
};
$.fn.ModelTabHeight = function (height) {
    var windowheight = $(window).height();
    var modelHeight = windowheight - 300 - height - $("#NavTabheader").outerHeight(true);
    $(this).css("height", modelHeight);
}


function ApplyScrollBar() {
    setTimeout(function () {
        $(".tab-content-wrapper").niceScroll({ touchbehavior: false, cursorcolor: "#084e9d", cursoropacitymax: 0.9, cursorwidth: 8, cursorborder: "1px solid #084e9d", cursorborderradius: "8px", background: "#ccc" });
        $(".ui-jqgrid-btable").niceScroll({ touchbehavior: false, cursorcolor: "#084e9d", cursoropacitymax: 0.9, cursorwidth: 8, cursorborder: "1px solid #084e9d", cursorborderradius: "8px", background: "#ccc" });
        $(".recently-update-list").ApplyCustomScrollBar(false);
    }, 1000);
}

$.fn.ApplyCustomScrollBar = function (horizontalScrollbar) {
    if (horizontalScrollbar == undefined) {
        horizontalScrollbar = false;
    }
    $(this).niceScroll({ horizrailenabled: horizontalScrollbar, touchbehavior: false, cursorcolor: "#084e9d", cursoropacitymax: 0.9, cursorwidth: 8, cursorborder: "1px solid #084e9d", cursorborderradius: "8px", background: "#ccc" });
};


$(window).resize(function () {
    rightContainerHeight();
    leftPanelHeight();
});

/////Grid Related Function
var StringSortOpt = ["cn", "bw", "bn", "nc"];
var NumericSortOpt = ["eq", "gt", "lt", "ne", "le", "ge"];
var EqualSortOpt = ['eq'];
var JGGridDateformat = 'd.m.Y H:i';

$.fn.JQGridViewLayout = function () {
    $(this).closest('.ui-jqgrid-view').find('.ui-search-toolbar').find('input, select').focus(function (e) {
        var $header = $(e.target).closest('.ui-jqgrid-hdiv'),
            $body = $header.siblings('.ui-jqgrid-bdiv');
        setTimeout(function () {
            // we syncronize the scroll in the separate thread
            // to be sure that the new scrolling value
            // already set in the grid header
            $body[0].scrollLeft = $header[0].scrollLeft;
        }, 0);
    });
};
$.fn.NumericTextBox = function () {
    $(this).keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
};
$.fn.GridFooterRefreshButton = function (pagerId, url) {
    $(this).jqGrid('navButtonAdd', pagerId, {
        caption: "",
        id: "refreshGrid",
        title: window.grdbtnRefreshTooltip,
        buttonicon: 'fa fa-refresh fa-2x',
        onClickButton:
            function () {
                $('.sort_order').remove();
                sortingOrder = [];
                var grid = $(this);
                grid.jqGrid('setGridParam', {
                    search: true, url: url + "?sortOrder=" + " ", sortname: " ", sortOrder: " "
                });
                var postData = grid.jqGrid('getGridParam', 'postData');
                $.extend(postData, {
                    filters: JSON.stringify({
                        groupOp: "AND", rules: []
                    })
                });
                $('input[id*="gs_"]').val("");
                grid.trigger("reloadGrid", [{
                    page: 1
                }]);
            }
    });
};

$.fn.GridFooterExportDataButton = function (pagerId, url) {
    $(this).jqGrid('navButtonAdd', pagerId, {
        caption: "",
        title: window.grdbtnExportTooltip,
        buttonicon: 'fa fa-wpforms fa-2x',
        onClickButton: function () {

            window.ExportJQData(url, $(this).jqGrid("getGridParam", "postData").filters, $(this));
        }
    });
};



$.fn.AppendGridFooter = function (gridId) {
    $(this).append("<td class='ui-pg-button ui-corner-all' title='" + window.grdbtnSaveSettingTooltip + "'><div class='pull-left gridfooterIcon'><span class='fa saveSeting fa-2x'></div></td>");
    $(this).append("<td class='ui-pg-button ui-corner-all' title='" + window.grdbtnPrintSettingTooltip + "'><div class='pull-left gridfooterIcon'><span class='gridPrintButton fa print fa-2x'></span></div></td>");
    //$(this).append("<td class='ui-pg-button ui-corner-all' title='" + window.grdbtnResetSettingTooltip + "'><div class='pull-left gridfooterIcon'><span class='fa resetSeting fa-2x'></span></div></td>");
    $(this).append("<td class='ui-pg-button ui-corner-all' title='Save fillter'><div class='pull-left' style='padding-top:5px'><div class='new-switch'><input id='cmn-toggle-4' class='cmn-toggle cmn-toggle-round-flat' type='checkbox' checked><label for='cmn-toggle-4'></label></div></div></td>");
    $(this).append("<td class='ui-pg-button ui-corner-all' title='" + window.grdbtnZoomInTooltip + "'><div class='pull-left gridfooterIcon' ><span class='fa arrow_up_ase' onclick= 'window.GridZoomIn();'></div></td>");
    $(this).append("<td class='ui-pg-button ui-corner-all' title='" + window.grdbtnZoomOutTooltip + "'><div class='pull-left gridfooterIcon'><span class='fa arrow_dow_dec' onclick= 'window.GridZoomOut();'></span></div></td>");


    //<label class='switch-original'> <input type='Checkbox' id ='gridchkSwitch' checked='false'> <span class='check'></span></label>
    $(".saveSeting").bind("click", function () {
        saveGridSettings(gridId, window.finalOrderingText, window.moduleName);
    });

    $(".gridPrintButton").bind("click", function () {
        $("#PrintDataModal").find("#ModalBody").html($("#PrintDailogContents").html());
        $("#PrintDataModal").modal("show");
        console.log("click From Common");
    });

    $("#cmn-toggle-4").bind("click", function () {
        if ($(this).is(":checked")) {
            clearGridSettings(gridId);
        }
        else {
            $(".fa-refresh").click();
            //For future reference 
            //$.each($("#" + gridId).jqGrid("getGridParam", "colModel"), function () {
            //    if (!$(this)[0].hidedlg) {
            //        if ($(this)[0].hidden) {
            //            $("#" + gridId).jqGrid('showCol', $(this)[0].name);
            //        }
            //    }
            //});
            //    var defaultcolumnMap = [];
            //$.each( , function (ind,val) {

            //    defaultcolumnMap.push($(this)[ind].value);
            //});
            // var columnMap =;
            //$("#" + gridId).jqGrid('remapColumns',[0, 1, 2, 3, 4, 5, 6, 7, 8, 9], true);
            //  var count = $("#" + gridId).jqGrid("getGridParam", "colModel").length;
            //   var defaultcolumnMap = [];
            //  for (i = 1; i <6; i++) {
            //$("#" + gridId).jqGrid('remapColumns', $("#" + gridId).jqGrid('getGridParam', 'remapColumns'), true);
            // defaultcolumnMap.push(i);
            //  }
            //   $("#" + gridId).jqGrid('remapColumns', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9], true);
            //  $("#" + gridId).jqGrid('remapColumns', columnMap, true);
            //defaultcolumnMap
            //$("#" + gridId).jqGrid('remapColumns', columnMap, true);


        }
    });
};

$.fn.ApplySortNotification = function (index) {
    var arr = index.split(',');
    $('.sort_order').remove();
    for (var i = 0; i < arr.length; i++) {
        var valAndSort = arr[i].split(' ');
        var sortNotification = $("<div>").attr({
            "class": "sort_order"
        }).html(i + 1);
        if ((valAndSort[0] !== ""))
            $('#' + $(this)[0].id + '_' + valAndSort[0]).append(sortNotification);
        else
            $('#' + $(this)[0].id + '_' + valAndSort[1]).append(sortNotification);
    }
};


$.fn.JQGridHideColMenu = function (columnName) {
    var id = '#jqgh_' + $(this)[0].id + '_' + columnName;
    $(id + " .colmenu").remove();
};

$.fn.JQGridIsNumeric = function (inputVal) {
    if (!numericReg.test(inputVal)) {
        return false;
    }
    else {
        return true;
    }
};
$.fn.JQGridDataTemplate = function (self) {
    $(this).datepicker({
        changeYear: true,
        changeMonth: true,
        showButtonPanel: false,
        showOn: 'focus',
        onSelect: function () {
            if (this.id.substr(0, 3) === "gs_") {
                // in case of searching toolbar
                setTimeout(function () {
                    self.triggerToolbar();
                }, 50);
            } else {
                // refresh the filter in case of
                // searching dialog
                $(this).trigger('change');
            }
        }, attr: {
            title: 'Select Date'
        }
    });
};

function GridZoomIn() {

    $('.jqgrow').height($('.jqgrow').height() + 10);
    $(".tab-content-wrapper").height(window.Pageheight + 20);
    window.rightContainerHeight();
    $(".tab-content-wrapper").height(window.Pageheight + 21);
    $(".ui-jqgrid-bdiv").ApplyCustomScrollBar(false);
}
function GridZoomOut() {
    if ($('.jqgrow').height() <= 35) {
        $(".jqgrow td").css("padding", "0px 6px");
    }
    $('.jqgrow').height($('.jqgrow').height() - 10);
    $(".tab-content-wrapper").height(window.Pageheight + 20);
    window.rightContainerHeight();
    $(".tab-content-wrapper").height(window.Pageheight + 21);
    $(".ui-jqgrid-bdiv").ApplyCustomScrollBar(false);
}

////Custom DataView ddl Method
$.fn.GetCustomDDLSelectedValue = function () {
    return $("#" + $(this)[0].id + " .customddlselectedValue").attr('selectedValue');
};
$.fn.GetCustomDDLSelectedText = function () {
    return $("#" + $(this)[0].id + " .customddlselectedValue").attr('selectedText');
};
$.fn.SetCustomDDLSelectedValue = function (value) {
    $("#" + $(this)[0].id + " li .row").removeClass('selection');
    $("#" + $(this)[0].id + " li[value='" + value + "']").find('.row').addClass('selection');
    $("#" + $(this)[0].id + " .customddlselectedValue").attr('selectedValue', value);

    var selectedText = $("#" + $(this)[0].id + " li[value='" + value + "']").attr('text');
    $("#" + $(this)[0].id + " .customddlselectedValue").attr('selectedText', selectedText);
    $("#" + $(this)[0].id + " .customddlselectedValue").text(selectedText);


};
$.fn.SetCustomDDLSelectedText = function (value) {
    $("#" + $(this)[0].id + " .customddlselectedValue").attr('selectedText', value);
    $("#" + $(this)[0].id + " .customddlselectedValue").text(value);
    $("#" + $(this)[0].id + " .customddlselectedValue").attr('selectedValue', $("#" + $(this)[0].id + " li[value='" + value + "']").attr('value'));
    $("#" + $(this)[0].id + " li .row").removeClass('selection');
    $("#" + $(this)[0].id + " li[value='" + value + "']").find('.row').addClass('selection');
};

$.fn.bindCustomDDLChangeEvent = function () {
    $("#" + $(this)[0].id + " .dropdown-menu li").click(function () {
        var ddl = $(this).parent().parent().parent()[0].id;
        if ($("#" + ddl + " .dropdown-group").attr("disabled") !== "disabled") {
            $("#" + ddl).find('.row').removeClass('selection');
            $(this).find('.row').addClass('selection');
            $("#" + ddl + " .customddlselectedValue").attr('selectedValue', $(this).attr('value'));
            $("#" + ddl + " .customddlselectedValue").attr('selectedText', $(this).attr('text'));
            $("#" + ddl + " .customddlselectedValue").text($(this).attr('text'));
            if (reloadAllConfiguredEvent != undefined) {

                reloadAllConfiguredEvent(ddl);
            }
        }
    });
};

$.fn.bindFooterDDLChangeEvent = function () {

    $("#" + $(this)[0].id + " .dropdown-menu li").click(function () {
        var ddl = $(this).parent().parent().parent()[0].id;
        $("#" + ddl).find('.row').removeClass('selection');
        $(this).find('.row').addClass('selection');
        $("#" + ddl + " .customddlselectedValue").attr('selectedValue', $(this).attr('value'));
        $("#" + ddl + " .customddlselectedValue").attr('selectedText', $(this).attr('text'));
        $("#" + ddl + " .customddlselectedValue").text($(this).attr('text'));
        var gridSetting = {
            UserDataView: $(this).attr('value')
        };
        window.geLocDataServices.PostData(window.saveFooterSettingsUrl, gridSetting, function () {
        });
    });
};
var numericReg = /^\d+(\.\d{1})??$/;


// For convenience...
// ReSharper disable once NativeTypePrototypeExtending
Date.prototype.format = function () {
    return new Date(this).toLocaleDateString(window.currentCultureName);
};

function saveGridSettings(gridId, sortOrder, moduleName) {
    var hiddenFields = [];
    $.each($("#" + gridId).jqGrid("getGridParam", "colModel"), function () {
        if (!$(this)[0].hidedlg) {
            if ($(this)[0].hidden) {
                hiddenFields.push($(this)[0].name);
            }
        }
    });

    var gridSetting = {
        Sord: sortOrder,
        Rows: $("#" + gridId).getGridParam("postData").rows,
        _search: $("#" + gridId).getGridParam("postData")._search,
        WhereCondition: $("#" + gridId).getGridParam("postData").filters,
        FilterCondition: JSON.stringify($("#" + gridId).jqGrid('getGridParam', 'colFilters')),
        module: moduleName,
        HiddenColumns: hiddenFields,
        Ordering: $("#" + gridId).jqGrid("getGridParam", "remapColumns")
    };

    //// window.geLocDataServices.PostData(window.saveSettingsUrl, gridSetting, function (data) { });

    $.ajax({
        url: window.saveSettingsUrl,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        success: function () {
        },
        data: JSON.stringify(gridSetting),
        async: false
    });
}

function clearGridSettings(gridId) {
    window.isfirstLoad = true;
    $("#" + gridId).trigger('reloadGrid');
}

function loadSavedSettings(gridId, jqgridData, isfirstLoad, moduleName) {
    if (isfirstLoad) {

        //window.geLocDataServices.PostData(window.getSettingsUrl, {}, function (model) {

        //    if (model.WhereCondition !== null || model.Sord !== null) {
        //        var newData = "_search=" + model._search + "&" + jqgridData.data.split('&')[1] + "&rows=" + model.Rows + "&page=1&sidx=&sord=asc&filters=" + model.WhereCondition;
        //        if (model.Sord != null) {
        //            newData = newData + "&sortOrder=" + model.Sord;
        //            window.sortingOrder = model.Sord.split(",");
        //            $("#" + gridId).ApplySortNotification(model.Sord);
        //        }
        //        jqgridData.data = newData;
        //        $("#" + gridId).jqGrid('setGridParam', { postData: { _search: model._search, rows: model.Rows, filters: model.WhereCondition } });

        //    }
        //});
        var gridSetting = {
            module: moduleName
        };
        $.ajax({
            url: window.getSettingsUrl,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            data: JSON.stringify(gridSetting),
            success: function (model) {
                if (model.WhereCondition !== null || model.Sord !== null || model.Ordering !== null || model.HiddenColumns !== null) {
                    var newData = "_search=" + model._search + "&" + jqgridData.data.split('&')[1] + "&rows=" + model.Rows + "&page=1&sidx=&sord=asc&filters=" + model.WhereCondition;
                    if (model.Sord != null) {
                        newData = newData + "&sortOrder=" + model.Sord;
                        window.sortingOrder = model.Sord.split(",");
                        $("#" + gridId).ApplySortNotification(model.Sord);
                    }
                    if (window.isReorderingRequired) {
                        if (model.Ordering != null) {
                            // if ($("#" + gridId).jqGrid("getGridParam", "remapColumns").toString() != model.Ordering.toString()) {
                            $("#" + gridId).jqGrid('remapColumns', model.Ordering, true);
                            window.isReorderingRequired = false;
                            // }
                        }
                    }

                    if (model.HiddenColumns != null) {
                        $.each(model.HiddenColumns, function (ind, val) {
                            $("#" + gridId).jqGrid('hideCol', val);
                        });
                    }
                    jqgridData.data = newData;
                    $("#" + gridId).jqGrid('setGridParam', {
                        search: model._search
                    });
                    $("#" + gridId).jqGrid('setGridParam', {
                        postData: {
                            _search: model._search, rows: 50, filters: model.WhereCondition
                        }
                    });
                    window.ts.p.colFilters = JSON.parse(model.FilterCondition);
                    console.log(JSON.parse(model.WhereCondition));
                    $("#" + gridId).jqGrid('setGridParam', 'colFilters', JSON.parse(model.WhereCondition));
                    if (model.WhereCondition !== null) {
                        if (model.FilterCondition === '{"Name":{}}' || model.FilterCondition === '{}') {
                            var lastIndex = 0;
                            var count = 0;

                            while (lastIndex !== -1) {
                                lastIndex = model.WhereCondition.indexOf("field", lastIndex);
                                if (lastIndex !== -1) {
                                    count++;
                                    lastIndex += 5;
                                }
                            }
                            var i = 00;
                            lastIndex = 0;
                            var lastIndex1 = 0;
                            for (; i < count ; i++) {
                                var fieldName;
                                var fieldData;
                                lastIndex = model.WhereCondition.indexOf("field", lastIndex);
                                fieldName = model.WhereCondition.substring(lastIndex, model.WhereCondition.length);
                                fieldName = fieldName.substring(8, model.WhereCondition.length);
                                fieldName = fieldName.substring(0, fieldName.indexOf(","));
                                fieldName = fieldName.substring(0, fieldName.length - 1);
                                window.fieldName.push(fieldName);
                                lastIndex1 = model.WhereCondition.indexOf("data", lastIndex1);
                                fieldData = model.WhereCondition.substring(lastIndex1, model.WhereCondition.length);
                                fieldData = fieldData.substring(7, model.WhereCondition.length);
                                fieldData = fieldData.substring(0, fieldData.indexOf("}"));
                                fieldData = fieldData.substring(0, fieldData.length - 1);
                                window.fieldValue.push(fieldData);
                                if (lastIndex !== -1) {
                                    lastIndex += 5;
                                }
                                if (lastIndex1 !== -1) {
                                    lastIndex1 += 4;
                                }
                            }
                            if (window.fieldName.length <= 0) {
                                $("#gridchkSwitch").attr('checked', false);
                                $("#gridchkSwitch").attr('disabled', 'disabled');
                            }
                        }
                        else {
                            $("#gridchkSwitch").attr('checked', false);
                            $("#gridchkSwitch").attr('disabled', 'disabled');
                        }
                    }
                }
            },
            async: false
        });
    }
}

function setSearchValue(isfirstLoad) {
    if (isfirstLoad) {
        var j = 00;
        var allInput = $(".ui-search-toolbar").find('input');
        var allselect = $(".ui-search-toolbar").find('select');
        $.each(allInput, function () {
            allInput[j].value = "";
            j++;
        });
        j = 00;
        $.each(allselect, function () {
            allselect[j].value = "";
            j++;
        });
        j = 00;
        $.each(window.fieldName, function () {
            $("#gs_" + window.fieldName[j]).val(window.fieldValue[j]);
            j++;
        });
    }
}

var ActionType;
// ReSharper disable once InconsistentNaming
(function (ActionType) {
    ActionType[ActionType["Create"] = 0] = "Create";
    ActionType[ActionType["DefaultLoad"] = 1] = "DefaultLoad";
    ActionType[ActionType["Edit"] = 2] = "Edit";
    ActionType[ActionType["Cancel"] = 2] = "Cancel";
})(ActionType || (ActionType = {}));
var ActionType;

function hex2rgb($colour) {
    if ($colour[0] === '#') {
        $colour = $colour.substring(1, $colour.length);
    }
    return parseInt($colour, 16);
}

function ShowPagePopup(url, module) {
    if (url.length > 0) {
        var property = "height=500,width=1200,top=100,left=100,resizable=yes";
        window.open(url, module, property);
    }
};

function SubPagePopup(element, url) {
    var module = element.attr("nav-module");
    if (module !== "undefined" && module.length > 0) {
        url = url + module;
        var property = "height=500,width=1200,top=100,left=100,resizable=yes";
        window.open(url, module, property);
    }
};

function LeftMenuToolTip(tooltip) {
    $("#recentlyUL").attr('title', tooltip);
}

$.fn.SetToolTip = function (tooltip) {
    $(this).attr('title', tooltip);
};

$.fn.VisibleControl = function (flag) {
    var accessLevel = $(this).attr("AccessLevel");
    if (accessLevel === undefined || accessLevel === "0") {

        if (flag === "true" || flag === "false")
            flag = flag === "true" ? true : false;
        else if (flag === "1" || flag === "0")
            flag = flag === "1" ? true : false;

        if (flag) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    }

};

$.fn.EnableControl = function (flag) {
    if (flag) {
        $(this).attr("disabled", false);

    }
    else {
        $(this).attr("disabled", true);
    }

};

$.fn.EnableControlAll = function (flag) {

    var accessLevel = $(this).attr("AccessLevel");
    if (accessLevel === undefined || accessLevel === "0") {

        if (flag === "true" || flag === "false")
            flag = flag === "true" ? true : false;
        else if (flag === "1" || flag === "0")
            flag = flag === "1" ? true : false;

        if (flag) {
            $(this).attr("disabled", false);
            $(this).find("input, select, button, textarea,span,div,img").attr("disabled", false);

        }
        else {
            $(this).attr("disabled", true);
            $(this).find("input, select, button, textarea,span,div,img").attr("disabled", true);
        }
    }
};

function FreeZScreen() {
    $('#container').attr('style', 'pointer-events: none;opacity: 0.4;');
    $('#container').append("<div id='divCoutner' class='footerbcounter'></div>");
    inveralTime = 100;
    counter = 60;
    var footertimer = setInterval(function () {
        if (counter >= 0) {
            counter = parseFloat(counter - 0.1).toFixed(1);
            if (parseFloat(counter) === 0) {
                counter = 60;
            }
            $('#divCoutner').text(window.Counter_FooterTimerMsg.replace("%COUNTER%", counter));
        }
        else {
            $('#divCoutner').text("");
            clearInterval(footertimer);
        }
    }, inveralTime);

}

function SetFooterTimerScreenBus(url, systemId, command, tnTimeout, tnWieOft, tlAbbruch, tlImmerda, tlMitAntwort, tnMode, successMsg, errorMsg, callBack) {
    $('#container').attr('style', 'pointer-events: none;opacity: 0.4;');
    $('#container').append("<div id='divCoutner' class='footerbcounter'></div>");
    var timer = tlImmerda === true ? 1 : 0;
    var withResponse = tlMitAntwort === true ? 1 : 0;
    window.geLocDataServices.GetData(url, { systemId: systemId, command: command, timer: timer, withResponse: withResponse, mode: tnMode },
    function (data) {
        if (data.IsSuccess) {
            var n = parseInt(data.Message);
            var ref = parseInt(data.Data);
            if (ref > 0) {
                var list = [1, 2, 3, 6, 7];
                if (list.indexOf(n) >= 0) {
                    window.StatusBarMessage(GetFooterResponseMessage(n), true, true);

                }
                else {
                    inveralTime = 100;
                    counter = tnTimeout;
                    if (tlMitAntwort) {
                        url = url.replace("ReadComServer", "CheckComServerStatus");
                        footertimer = setInterval(function () {
                            if (counter > 0) {
                                counter = parseFloat(counter - 0.1).toFixed(1);
                                $('#divCoutner').text(window.Counter_FooterTimerMsg.replace("%COUNTER%", counter));
                                window.geLocDataServices.GetData(url, { offlineId: ref },
                                function (datastatus) {
                                    if (parseFloat(counter) === 0 || datastatus.Data.trim() !== "") {
                                        counter = tnTimeout;
                                        callBack(datastatus, true, true, successMsg, errorMsg);
                                    }
                                    else {
                                        callBack(datastatus, true, false, successMsg, errorMsg);
                                    }
                                },
                                function () { }, false);
                            }
                            //else {
                            //    callBack("timeout", true, true, successMsg, errorMsg);
                            //}
                        }, inveralTime);
                    }
                }

            }
            else {
                window.StatusBarMessage(GetFooterResponseMessage(-4), true, true);
            }

        }
        else {
            window.StatusBarMessage(GetFooterResponseMessage(-5), true, true);
        }
    }, false);
}

function SetFooterTimerScreen(url, systemId, command, tnTimeout, tnWieOft, tlAbbruch, tlImmerda, tlMitAntwort, tnMode, successMsg, errorMsg, callBack, deleteOnChange) {
    if (deleteOnChange === undefined) {
        deleteOnChange = true;
    }
    $('#container').attr('style', 'pointer-events: none;opacity: 0.4;');
    $('#container').append("<div id='divCoutner' class='footerbcounter'></div>");
    var timer = tlImmerda === true ? 1 : 0;
    var withResponse = tlMitAntwort === true ? 1 : 0;
    window.geLocDataServices.GetData(url, { systemId: systemId, command: command, timer: timer, withResponse: withResponse, mode: tnMode },
    function (data) {
        if (data.IsSuccess) {
            var n = parseInt(data.Message);
            var ref = parseInt(data.Data);
            BindAbortEvent(ref);
            if (ref > 0) {
                var list = [1, 2, 3, 6, 7];
                if (list.indexOf(n) > 0) {
                    window.StatusBarMessage(GetFooterResponseMessage(n), true, true);
                }
                inveralTime = 100;
                counter = tnTimeout;
                if (tlMitAntwort) {
                    //ReadComServer(url, ref, tnTimeout, callBack, successMsg, errorMsg);
                    SignalRNotifications.SubScribeOffline(ref, tnTimeout, function (data) {
                        UnBindAbortEvent();
                        callBack(data, true, true, successMsg, errorMsg);
                    }, deleteOnChange);
                }
            }
            else {
                window.StatusBarMessage(GetFooterResponseMessage(-4), true, true);
            }

        }
        else {
            window.StatusBarMessage(GetFooterResponseMessage(-5), true, true);
        }
    }, false);
}


function GetFooterResponseMessage(n) {

    //* Text zum Kommunikationsstatus 
    //* Rückgabewerte
    //*   -4 keine Ergebnismenge
    //*   -3 UserAbbruch
    //*   -2 Timeout
    //*   -1 ist weg
    //*    0 ok, Antwort ist in tcAntwort
    //*    1 abgemeldet
    //*    2 Pause
    //*    3 wartet auf Zeitpunkt
    //*    6 Verbindungsproblem
    //*    7 Offline
    switch (n) {
        case 1:
            return ('System abgemeldet');
        case 2:
            return ('System Pause');
        case 3:
            return ('wartet auf Zeitpunkt');
        case 6:
            return ('System Verbindungsproblem');
        case 7:
            return ('System Offline');
        case 0:
            return ('ok');
        case -4:
            return ('keine Ergebnismenge');

        case -3:
            return ('Abbruch durch Benutzer');
        case -2:
            return ('Timeout');
        case -1:
            return ('Anfrage ist gelöscht');
        default:
            return ('Unbekannter Fehler');
    }
}

function ShowDataviewPopup() {
    var url = window.GetDefaultDataViewURL;
    var id = $("#cddfooterDataview").GetCustomDDLSelectedValue();
    if (id !== "undefined" && id.length > 0) {
        url = url + "?id=" + id;
    }
    ShowPagePopup(url, "Dataview");
}

function StatusBarMessage(message, autohide, timerhide, callBack) {
    if (message === undefined) {
        message = "";
    }
    if (message.length > 0) {
        if ($('#Pagestatus').css('display') === "none") {
            $("#Pagestatus").show(100);
        }
    }
    $('#Pagestatus').html(message);
    if (autohide) {
        $("#Pagestatus").hide(10000);
    }

    if (timerhide === true) {
        counter = -1;
        inveralTime = 1000000;
        $('#container').removeAttr('style');
        $('#divCoutner').text("");
        clearInterval(footertimer);
        clearInterval(setInterval);
        clearInterval(SignalRNotifications.interval);
        if (callBack) {
            callBack();
        }
    }
}

function ShowBodyFotter(isright, isCounter) {
    $("#bodyfooterLeft").show();
    if (isright)
        $("#bodyfooterRight").show();
    else
        $("#bodyfooterRight").hide();
    if (!isCounter) {
        $("#divleftMenuSearch").removeClass("col-md-10");
        $("#divleftMenuSearch").addClass("col-md-12");
        $("#divleftMenulicount").hide();

    }
}

function numericValidateOnChange(elemVal, element) {
    var numericReg = /^\d+(\.\d{1})??$/;
    if (!numericReg.test(elemVal)) {
        element.val(0);
        return false;
    }
    return true;
}

function SaveINIConfiguration(module, section, key, value, callback) {
    var gridSetting = { module: module, section: section, key: key, value: value };
    window.geLocDataServices.PostData(window.SaveConfigurationUrl, gridSetting, function () {
        if (callback !== undefined)
            callback();
    });
}

function GetINIConfiguration(module, section, key, value, callBack) {
    var gridSetting = { module: module, section: section, key: key, value: value };
    window.geLocDataServices.PostData(window.ReadConfigurationUrl, gridSetting, function (data) {
        callBack(data);
    });
}
function GetAllINIConfiguration(module, section, key, value, callBack) {
    var gridSetting = { module: module, section: section, key: key, value: value };
    window.geLocDataServices.PostData(window.ReadAllConfigurationUrl, gridSetting, function (data) {
        callBack(data);
    });
}

function ReadComServer(url, ref, tnTimeout, callBack, successMsg, errorMsg) {

    url = url.replace("ReadComServer", "CheckComServerStatus");
    footertimer = setInterval(function () {
        if (counter > 0) {
            counter = parseFloat(counter - 0.1).toFixed(1);
            $('#divCoutner').text(window.Counter_FooterTimerMsg.replace("%COUNTER%", counter));
            window.geLocDataServices.GetData(url, { offlineId: ref, isDeleteOffline: false },
            function (datastatus) {

                //geLocDataServices.GetData(url, { offlineId: Ref, isDeleteOffline: true }, function () { }, function () { }, false);
                if (parseFloat(counter) === 0 || datastatus.Data.trim() !== "") {
                    counter = tnTimeout;
                    callBack(datastatus, true, true, successMsg, errorMsg);
                }
                else {
                    callBack(datastatus, true, false, successMsg, errorMsg);
                }
            },
            function () { }, false);
        }
        else {
            $('#divCoutner').text("");
            clearInterval(footertimer);
        }
    }, inveralTime);
}

function BindAbortEvent(commandId) {
    $(document).keypress(function (event) {
        if (event.keyCode === 65 || event.keyCode === 97) {
            StatusBarMessage(window.PageStatusAbort, true, true, function () { });
            SignalRNotifications.DeRegisterSubScription(commandId);
            UnBindAbortEvent();
        }
    });
}

function UnBindAbortEvent() {
    $(document).unbind("keypress");
}


function hexColor(colorval) {
    colorval = colorval.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);

    function hex(x) {
        return ("0" + parseInt(x).toString(16)).slice(-2);
    }

    return "#" + hex(colorval[1]) + hex(colorval[2]) + hex(colorval[3]);
}

function GetExportColumns(element) {
    var selectedColums = [];
    if (element !== undefined) {
        var colModel = $(element).jqGrid("getGridParam", "colModel");
        var colNames = $(element).jqGrid("getGridParam", "colNames");
        if (colModel.length > 0) {
            var i;
            for (i = 0; i < colModel.length; i++) {
                if (!colModel[i].hidedlg) {
                    selectedColums.push(colModel[i].name);
                }
            }
            selectedColums.push("|");
            for (i = 0; i < colModel.length; i++) {
                if (!colModel[i].hidedlg) {
                    selectedColums.push($("<div>").html(colNames[i]).text());
                }
            }
        }
    }
    return selectedColums;

}
