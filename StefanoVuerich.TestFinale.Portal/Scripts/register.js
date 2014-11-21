$('#registerButton').on('click', function (e) {

    e.preventDefault();

    $('#feedback').empty();

    var email = $('#email').val();
    var password = $('#password').val();
    var confirmPassword = $('#confirmPassord').val();

    var userModel = {
        Email: email,
        Password: password,
        ConfirmPassword : confirmPassword
    };

    $.ajax({
        type: 'POST',
        url: '../api/Account/Register',
        contentType: 'application/json',
        data: JSON.stringify(userModel),
        success: function (data) {

            var loginString = "grant_type=password&username=" + userModel.Email + "&password=" + userModel.Password;

            $.ajax({
                type: 'POST',
                url: '../Token',
                contentType: 'application/x-www-form-urlencoded',
                data: loginString,
                success: function (data) {
                    if (data != null) {
                        sessionStorage.setItem("token", data.access_token);
                        window.location.href = "/Home/ShowData?username=" + userModel.Email;
                    } else
                        $('#feedback').html('Username o password errati di preghiamo di riprovare');
                },
                error: function () {
                    $('#feedback').html('Username o password errati di preghiamo di riprovare');
                }
            });
        },
        error: function (xhr, textStatus) {
            $('#feedback').html('Richiesta non valida, la password deve essere di almeno sei caratteri');
        }
    });

});