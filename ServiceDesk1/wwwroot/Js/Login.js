/// <reference path="jquery-3.3.1.js" />


$("#BtnLogin").on("click", function () {

    var userData = {
        UserName: $('#username').val(),
        Pass: $('#password').val()
    };

    $.post("api/Login", userData).done(
        function (data) {
            switch (data) {
                //Engineering 
                case 1:
                case 2:
                case 6:
                    window.location.href = "Engineering.html";
                    break;
                //Sales And marketing
                case 3:
                case 4:
                    window.location.href = "Sales.html";
                    break;
                //Hr
                case 9:
                    window.location.href = "Hr.html";
                    break;
                //IT
                case 17:
                    window.location.href = "IT.html";
                    break;
                //Manufacturing
                case 5:
                case 7:
                case 8:
                case 12:
                case 13:
                case 15:
                    window.location.href = "Manufacturing.html";
                    break;
                //Executive General and Administration
                case 10:
                case 11:
                case 16:
                case 14:
                    window.location.href = "Executive.html";
                    break;
                default:
                    console.log("Something went wrong!");
                    break;
            }
        });
});
