$(document).ready(function () {
    BindClickEvent();
});

function BindClickEvent() {
    $("#btnLogin").bind("click", function () {
        var validateLogin = $("#login").valid();
        if (validateLogin) {
            BarterBuddyDataServices.PostData(window.loginUrlPath, $("#login").serialize(),
                function (responseMessage) {
                    if (responseMessage.StatusCode == ResponseCode.Error) {
                        Messages.ErrorMessage("Login", responseMessage.Message);
                        return false;
                    }
                    else {
                        window.location.href = "/Home/Index";
                    }
                }, function (ex) {
                    Messages.ErrorMessage("Error", ex);
                }
                );
        }
    });
    $("#btnRegister").bind("click", function () {
        if ($("#c1").ischecked == false) {
            Messages.ErrorMessage("Registration", "Select Terms");
            return false;
        }
        var validateregis = $("#regis").valid();
        if (validateregis) {
             BarterBuddyDataServices.PostData(window.registerUrlPath, $("#regis").serialize(),
                 function (responseMessage) {
                     if (responseMessage.StatusCode == ResponseCode.Error) {
                         Messages.ErrorMessage("Registration", responseMessage.Message);
                         return false;
                     }
                 }, function (ex) {
                     Messages.ErrorMessage("Error", ex);
                 }
            );
        }
    });
}
