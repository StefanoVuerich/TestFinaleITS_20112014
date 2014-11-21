$('#loginButton').on('click', function (e) {

    e.preventDefault();

    $('#feedback').empty();

    var email = $('#username').val();
    var password = $('#password').val();

    var loginString = "grant_type=password&username=" + email + "&password=" + password;

    $.ajax({
        type: 'POST',
        url: '../Token',
        contentType: 'application/x-www-form-urlencoded',
        data: loginString,
        success: function (data) {
            if (data != null) {
                sessionStorage.setItem("token", data.access_token);
                window.location.href = "/Home/ShowData?username=" + data.userName;  
            } else
                $('#feedback').html('Username o password errati di preghiamo di riprovare');
        },
        error: function () {
            $('#feedback').html('Username o password errati di preghiamo di riprovare');
        }
    });

});