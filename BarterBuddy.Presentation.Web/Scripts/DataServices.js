var BarterBuddy;
(function (BarterBuddy) {
    var dataServices = (function () {
        function dataServices() {
        }
        dataServices.prototype.GetData = function (url, dataObject, callBack, errorCallback, isglobal) {
            if (isglobal == undefined) {
                isglobal = true;
            }
            $.ajax({
                url: url,
                type: 'get',
                data: dataObject,
                cache: false,
                global: isglobal,
                success: function (x) {
                    if (x !== undefined && typeof (x) === "object" && x !== null) {
                        if (x.hasOwnProperty("IsError") && x.IsError) {
                            $("#error-description").html(x.Message);
                            $("#technical-details").html(x.ErrorMessage);
                            Messages.ErrorMessage("Fehler", $($("#error-message").html()));
                        }
                        else {
                            callBack(x);
                        }
                    }
                    else if (typeof (x) == "string") {
                        try {
                            var y = JSON.parse(x);
                            if (y.hasOwnProperty("IsError") && y.IsError) {
                                $("#error-description").html(y.Message);
                                $("#technical-details").html(y.ErrorMessage);
                                Messages.ErrorMessage("Fehler", $($("#error-message").html()));
                            }
                            else {
                                callBack(x);
                            }
                        }
                        catch (err) {
                            callBack(x);
                        }
                    }
                    else {
                        callBack(x);
                    }

                    //if (x !== undefined && typeof (x) === "object") {
                    //    if (x.hasOwnProperty("IsSuccess")) {
                    //    }
                    //}
                    //callBack(x);
                },
                error: function (x, h, r) {
                    if (x.status === 401 || x.status === 302 || x.status === 403) {
                        window.location = window.ChangepasswordLoginURL;
                    }
                    else {
                        errorCallback(x, h, r);
                    }
                }
            });
        };
        dataServices.prototype.PostData = function (url, dataObject, callBack, errorCallback, headers, isglobal) {
            if (isglobal == undefined) {
                isglobal = true;
            }
            //$('#loadingDisplay').show(0);
            //$('#loadProgressBar').css('width', '50%').attr("aria-valuenow", 50);
            $.ajax({
                url: url,
                type: 'post',
                data: dataObject,
                cache: false,
                headers: headers,
                global: isglobal,
                complete: function () {

                    //$('#loadProgressBar').css('width', '100%').attr("aria-valuenow", 100);
                    //$("#loadingDisplay").delay(500).fadeOut(20).queue(function (next) {
                    //    $('#loadProgressBar').delay(1200).css('width', '0%').attr("aria-valuenow", 0);
                    //    next();
                    //});
                },
                success: function (x) {
                    $('#searchResults').delay(800).slideDown(500);
                    if (x !== undefined && typeof (x) === "object" && x !== null) {
                        if (x.hasOwnProperty("IsError") && x.IsError) {
                            $("#error-description").html(x.Message);
                            $("#technical-details").html(x.ErrorMessage);
                            Messages.ErrorMessage("Fehler", $($("#error-message").html()));
                        }
                        else {
                            callBack(x);
                        }
                    }
                    else if (typeof (x) == "string") {
                        try {
                            var y = JSON.parse(x);
                            if (y.hasOwnProperty("IsError") && y.IsError) {
                                $("#error-description").html(y.Message);
                                $("#technical-details").html(y.ErrorMessage);
                                Messages.ErrorMessage("Fehler", $($("#error-message").html()));
                            }
                            else {
                                callBack(x);
                            }
                        }
                        catch (err) {
                            callBack(x);
                        }
                    }
                    else {
                        callBack(x);
                    }
                },
                error: function (x, h, r) {
                    if (x.status === 401 || x.status === 302 || x.status === 403) {
                        window.location = window.ChangepasswordLoginURL;
                    }
                    errorCallback(x, h, r);
                }
            });
        };

        dataServices.prototype.PostFileData = function (url, dataObject, callBack, errorCallback, headers, isglobal) {
            if (isglobal == undefined) {
                isglobal = true;
            }

            $.ajax({
                url: url,
                type: 'post',
                data: dataObject,
                cache: false,
                headers: headers,
                global: isglobal,
                mimeType: "multipart/form-data",
                contentType: false,
                processData: false,
                success: function (x) {
                    if (x !== undefined && typeof (x) === "object" && x !== null) {
                        if (x.hasOwnProperty("IsError") && x.IsError) {
                            $("#error-description").html(x.Message);
                            $("#technical-details").html(x.ErrorMessage);
                            Messages.ErrorMessage("Fehler", $($("#error-message").html()));
                        }
                        else {
                            callBack(x);
                        }
                    }
                    else if (typeof (x) == "string") {
                        try {
                            var y = JSON.parse(x);
                            if (y.hasOwnProperty("IsError") && y.IsError) {
                                $("#error-description").html(y.Message);
                                $("#technical-details").html(y.ErrorMessage);
                                Messages.ErrorMessage("Fehler", $($("#error-message").html()));
                            }
                            else {
                                callBack(x);
                            }
                        }
                        catch (err) {
                            callBack(x);
                        }
                    }
                    else {
                        callBack(x);
                    }
                },
                error: function (x, h, r) {
                    if (x.status === 401 || x.status === 302 || x.status === 403) {
                        Messages.ErrorMessage("Error", x.statusCode);
                        //window.location = window.ChangepasswordLoginURL;
                    }
                    errorCallback(x, h, r);
                }
            });
        };

        return dataServices;
    })();
    BarterBuddy.DataServices = dataServices;
})(BarterBuddy || (BarterBuddy = {}));
//# sourceMappingURL=DataServices.js.map
var BarterBuddyDataServices = new BarterBuddy.DataServices();