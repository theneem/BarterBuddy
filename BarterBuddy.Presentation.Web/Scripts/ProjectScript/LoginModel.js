$(document).ready(function () {
    $("#btnRegister").bind("click", function () {
        var userData = {
            UserName: 'Parimal',
            Password:'Test@123'
        }
        $.ajax({
            type: "POST",
            url: urlPath + "ValidateUserLogin",
            data: { userModel: userData }
    ,
            success: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response);
            }
        });
    });
})