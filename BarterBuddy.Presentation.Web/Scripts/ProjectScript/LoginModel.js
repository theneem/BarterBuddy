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
        if ($('#c1:checked').length == 0) {
            Messages.ErrorMessage("Login", "Please Confirm Terms & Policy");
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
                    else {
                        window.location.href = "/Home/Index";
                    }
                }, function (ex) {
                    Messages.ErrorMessage("Error", ex);
                }
           );
        }
    });

    $("#btnReset").bind("click", function () {
        $("#resetPasswordError").removeClass("alert-danger");
        $("#resetPasswordError")[0].innerHTML = "";;
        var validateresetPassword = $("#frmReset").valid();
        if (validateresetPassword) {

            var resetPasswordModal = {
                userName: $("#txForgetEmailUser").val(),
                password: null
            };
            BarterBuddyDataServices.PostData(window.resetUrlPath, { userModel: resetPasswordModal },
                function (responseMessage) {
                    if (responseMessage.StatusCode == ResponseCode.Error) {
                        $("#resetPasswordError").append(responseMessage.Message);
                        $("#resetPasswordError").addClass('alert alert-danger');
                        //Messages.ErrorMessage("Registration", responseMessage.Message);
                        return false;
                    }
                    else {
                        $("#resetPasswordError").append(responseMessage.Message);
                        $("#resetPasswordError").addClass('alert alert-success');
                    }
                }, function (ex) {
                    Messages.ErrorMessage("Error", ex);
                }
           );
        }
    });
}
