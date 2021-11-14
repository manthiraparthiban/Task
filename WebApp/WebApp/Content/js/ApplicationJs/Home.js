$("#btnSignIn").click(function () {
    if ($('#txtusername').val() == "") {
        $('#Ptxtusername').show();
        $('#Ptxtusername').fadeOut(8000);
        $('#Ptxtusername').html('Enter Your registered email address');
        $('#txtusername').focus();
        return false;
    }
    if ($('#txtPassword').val() == "") {
        $('#PtxtPassword').show();
        $('#PtxtPassword').fadeOut(8000);
        $('#PtxtPassword').html('Enter Password');
        $('#txtPassword').focus();
        return false;
    }
    if ($.trim($('#txtusername').val()).length > 0 && $.trim($('#txtPassword').val()).length > 0) {
        var LoginData = {
            user_name: $("#txtusername").val(),
            password: $("#txtPassword").val(),
        }
        $.ajax({
            async: true,
            type: 'POST',
            url: '/Home/LoginVerification',
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify(LoginData),
            success: function (data) {
                if (jQuery.isEmptyObject((data))) {
                    $('#PbtnSignIn').show();
                    $('#PbtnSignIn').fadeOut(8000);
                    $('#PbtnSignIn').html('Password or email address entered is incorrect');
                }
                else {
                    var data = JSON.parse(data);
                    if (data != '') {
                        if (data["msg"] == "Success") {
                            if (data["data"][0].msg == "1")
                            {
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
                                $('#PbtnSignIn').show();
                                $('#PbtnSignIn').fadeOut(8000);
                                $('#PbtnSignIn').html('Password or email address entered is incorrect');
                            }
                            else {
                                $('#PbtnSignIn').show();
                                $('#PbtnSignIn').fadeOut(8000);
                                $('#PbtnSignIn').html('Kindly Register');
                            }
                        }
                        else{
                            $('#PbtnSignIn').show();
                            $('#PbtnSignIn').fadeOut(8000);
                            $('#PbtnSignIn').html('Kindly Register');
                        }
                    }
                    else {
                        $('#PbtnSignIn').show();
                        $('#PbtnSignIn').fadeOut(8000);
                        $('#PbtnSignIn').html('Kindly Register');
                    }
                }
            }
        });
    }
});


$('#txtPassword').keypress(function (e) {
    var key = e.which;
    if (key == 13)  // the enter key code
    {
        $('#btnSignIn').click();
    }
});

$('#txtusername').keypress(function (e) {
    var key = e.which;
    if (key == 13)  // the enter key code
    {
        $('#btnSignIn').click();
    }
});
