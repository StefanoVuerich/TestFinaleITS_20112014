$(function () {
    if (sessionStorage.getItem('token') == null)
        $('#logoutLink').remove()

    $('.registered').on('click', function (e) {
        e.preventDefault();
        var obj = $(this);
        if (sessionStorage.getItem('token') == null) {
            window.location.href = '../Home/Login';
        } else {
            var url = obj[0].href;
            window.location.href = url;
        }
    })
    if (sessionStorage.getItem('token') != null) {
        $('#loginLink').remove();
        $('#registrationLink').remove();
    }
    $('#logoutLink').on('click', function (e) {
        e.preventDefault()
        sessionStorage.removeItem('token');
        sessionStorage.removeItem('User');
        window.location.href = '../Home';
    })
})
