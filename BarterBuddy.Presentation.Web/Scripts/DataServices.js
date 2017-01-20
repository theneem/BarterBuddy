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

        dataServices.prototype.GetDataProgress = function (url, dataObject, callBack, progresscallBack, errorCallback, isglobal) {
            if (isglobal == undefined) {
                isglobal = true;
            }
            $.ajax({
                url: url,
                type: 'get',
                data: dataObject,
                cache: false,
                global: isglobal,
                xhr: function () {
                    var xhr = new window.XMLHttpRequest();
                    // ReSharper disable Html.EventNotResolved
                    xhr.upload.addEventListener("progress", function (evt) {
                        if (evt.lengthComputable) {
                            //var percentComplete = evt.loaded / evt.total;
                            progresscallBack(evt.loaded / evt.total);
                        }
                    }, false);
                    xhr.addEventListener("progress", function (evt) {
                        if (evt.lengthComputable) {
                            progresscallBack(evt.loaded / evt.total);
                            //var percentComplete = evt.loaded / evt.total;
                            //$('#ImportProgresslabel').html(percentComplete * 100 + '%')
                            //$('.progressbarSlide').css({
                            //    width: percentComplete * 100 + '%'
                            //});
                        }
                    }, false);
                    return xhr;
                },

                success: function (x) {
                    if (x !== undefined && typeof (x) === "object" && x !== null) {

                        if (x.hasOwnProperty("IsSuccess")) {

                        }
                    }
                    callBack(x);
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
            $.ajax({
                url: url,
                type: 'post',
                data: dataObject,
                cache: false,
                headers: headers,
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
                        window.location = window.ChangepasswordLoginURL;
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