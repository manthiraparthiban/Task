

$("#btnchangePwd").click(function () {
    var Password = $("#txtcurrent_password").val();
    var NewPassword = $("#txtnew_password").val();
    var ConfirmPassword = $("#txtconfirm_password").val();
    if ($.trim(Password) == "") {
        $('#Ptxtcurrent_password').show();
        $('#Ptxtcurrent_password').fadeOut(8000);
        $('#Ptxtcurrent_password').html('Enter Current Password');
        $("#txtcurrent_password").val('');
        $("#txtcurrent_password").focus();
        return false;
    }
    if ($.trim(NewPassword) == "") {
        $('#Ptxtnew_password').show();
        $('#Ptxtnew_password').fadeOut(8000);
        $('#Ptxtnew_password').html('Enter New Password');
        $("#txtnew_password").val('');
        $("#txtnew_password").focus();
        return false;
    }
    if ($.trim(ConfirmPassword) == "") {
        $('#Ptxtconfirm_password').show();
        $('#Ptxtconfirm_password').fadeOut(8000);
        $('#Ptxtconfirm_password').html('Enter Confirm Password');
        $("#txtconfirm_password").val('');
        $("#txtconfirm_password").focus();
        return false;
    }
    if ($.trim(NewPassword).length < 8) {
        $('#Ptxtnew_password').show();
        $('#Ptxtnew_password').fadeOut(8000);
        $('#Ptxtnew_password').html('New Password Length must be in Minimum of 8 Characters');
        $("#txtnew_password").val('');
        $("#txtnew_password").focus();
        return false;
    }
    if ($.trim(ConfirmPassword).length < 8) {
        $('#Ptxtconfirm_password').show();
        $('#Ptxtconfirm_password').fadeOut(8000);
        $('#Ptxtconfirm_password').html('Confirm Password Length must be in Minimum of 8 Characters');
        $("#txtconfirm_password").val('');
        $("#txtconfirm_password").focus();
        return false;
    }
    if (NewPassword.length > 0 && ConfirmPassword.length > 0 && NewPassword != ConfirmPassword) {
        $('#Ptxtconfirm_password').show();
        $('#Ptxtconfirm_password').fadeOut(8000);
        $('#Ptxtconfirm_password').html('Your Password and Confirmation Password does not match');
        $("#txtnew_password").val('');
        $("#txtconfirm_password").val('');
        $("#txtnew_password").focus();
        return false;
    }
    var PasswordUpdationData = {
        old_password: $.trim($('#txtcurrent_password').val()),
        new_password: $.trim($('#txtnew_password').val())
    }
    if (Password.length > 0 && NewPassword.length > 0 && ConfirmPassword.length > 0) {
        $.ajax({
            async: true,
            type: 'POST',
            url: 'ChangePassword/PasswordUpdation',
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify(PasswordUpdationData),
            success: function (data) {
                if (jQuery.isEmptyObject((data))) {
                    alert("Something Went Wrong, Plase try again later")
                }
                else {
                    var data = JSON.parse(data);
                    if (data != '') {
                        if (data["msg"] == "Success") {
                            if (data["data"][0].msg == "1") {
                                var LoginData = {
                                    usercode: data["data"][0].user_code,
                                    emailid: data["data"][0].email_id,
                                    username: data["data"][0].name,
                                    password: data["data"][0].password,
                                    token: data["token"]
                                }
                                $.ajax({
                                    async: true,
                                    type: 'POST',
                                    url: '/Home/LoginSession',
                                    contentType: 'application/json; charset=UTF-8',
                                    data: JSON.stringify(LoginData),
                                    success: function (data) {
                                        window.location.href = "/Dashboard/index";
                                    }
                                });
                            }
                            else if (data["data"][0].msg == "0") {
                                alert("Something Went Wrong, Plase try again later")
                            }
                            else {
                                alert("Something Went Wrong, Plase try again later")
                            }
                        }
                        else {
                              alert("Something Went Wrong, Plase try again later")
                        }
                    }
                    else {
                       alert("Something Went Wrong, Plase try again later")
                    }
                }
            }
        });
    }
});



function TestOnTextChange() {
    var NewPassword = $("#txtnew_password").val();
    var ConfirmPassword = $("#txtconfirm_password").val();
    if ($.trim(NewPassword).length < 8) {
        $('#Ptxtnew_password').html('New Password Length must be in 8 Characters');
        $("#txtnew_password").focus();
        return false;
    }
    if ($.trim(ConfirmPassword).length < 8) {
        $('#Ptxtconfirm_password').html('Confirm Password Length must be in Minimum of 8 Characters');

        $("#txtconfirm_password").focus();
        return false;
    }
}