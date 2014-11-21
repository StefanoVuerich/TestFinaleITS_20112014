$('#changePass').on('click', function (e) {

    e.preventDefault();

    var oldPass = $('#oldPass').val();
    var newPass = $('#newPass').val();
    var confirmNewPass = $('#confirmNewPass').val();

    var passwordChanger = {
        OldPassword: oldPass,
        NewPassword: newPass,
        ConfirmPassword: confirmNewPass
    }

    $.ajax({
        type: 'POST',
        url: '../api/Account/ChangePassword',
        contentType: 'application/json',
        data: JSON.stringify(passwordChanger),
        beforeSend: function (xhr) { xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('token')) },
        success: function (data, textStatus, jqXHR) {
            if (jqXHR.status == 200) {
                $("#oldPass, #newPass, #confirmNewPass").val("");
                $('#feedback').html('Password modificata correttamente');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('#feedback').html('Error modifing password');
        }
    });

});