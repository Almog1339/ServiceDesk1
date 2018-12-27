/// <reference path="jquery.min.js" />


$("#Btn-login").on("click", function () {
    var userData = {
        LoginID: $('#username').val(),
        Password: $('#password').val()
    };

    $.post("api/Login", userData).done(
        function (data) {
            if (data === true) {
                localStorage.setItem('username',JSON.stringify(userData.LoginID));
                window.location.href = "index.html";
            } else {
                alert("Something went wrong!");
            }
        });
});
