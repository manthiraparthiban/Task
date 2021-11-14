$("#btnregister").click(function () {
    if ($('#fname').val() == "") {
        $('#Pfname').show();
        $('#Pfname').fadeOut(8000);
        $('#Pfname').html('Enter First Name');
        $('#fname').focus();
        return false;
    }
    if ($('#lname').val() == "") {
        $('#Plname').show();
        $('#Plname').fadeOut(8000);
        $('#Plname').html('Enter Last Name');
        $('#lname').focus();
        return false;
    }
    if ($('#emailid').val() == "") {
        $('#Pemailid').show();
        $('#Pemailid').fadeOut(8000);
        $('#Pemailid').html('Enter email address');
        $('#emailid').focus();
        return false;
    }
    if ($('#password').val() == "") {
        $('#Ppassword').show();
        $('#Ppassword').fadeOut(8000);
        $('#Ppassword').html('Enter Password');
        $('#password').focus();
        return false;
    }
    if ($('#repassword').val() == "") {
        $('#Prepassword').show();
        $('#Prepassword').fadeOut(8000);
        $('#Prepassword').html('Re-Enter Password');
        $('#repassword').focus();
        return false;
    }
    if ($('#password').val() != $('#repassword').val()) {
        $('#Prepassword').show();
        $('#Prepassword').fadeOut(8000);
        $('#Prepassword').html('Please Enter Correct Password');
        $('#repassword').focus();
        return false;
    }
    var RegiterData = {
        email_id: $("#emailid").val(),
        password: $("#password").val(),
        firstname: $("#fname").val(),
        lastname: $("#lname").val(),
    }
    $.ajax({
        async: true,
        type: 'POST',
        url: '/Home/RegisterUser',
        contentType: 'application/json; charset=UTF-8',
        data: JSON.stringify(RegiterData),
        success: function (data) {
            if (jQuery.isEmptyObject((data))) {
                alert("Something Went Wrong, Plase try again later")
            }
            else {
                var data = JSON.parse(data);
                console.log(data);
                if (data != '') {
                    if (data["msg"] == "Success") {
                        if (data["data"][0].msg == "1")
                        {
                            alert("Successfully Registered");
                            window.location.href = "/Home/Index";
                        }
                        else if (data["data"][0].msg == "0") {
                            alert("Email Id Already Exists Kindly Login");
                            window.location.href = "/Home/Index";
                        }
                    }
                    else
                    {
                        alert("Something Went Wrong, Plase try again later")
                    }
                }
                else {
                    alert("Something Went Wrong, Plase try again later")
                }
            }
        }
    });
});