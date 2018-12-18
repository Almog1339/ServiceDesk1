$("#BtnLogin").on("click", function () {

    var userData = {
        UserName: $('#username').val(),
        Pass: $('#password').val()
    };

    $.ajax({
        type: "POST",
        url: "api/Login",
        data: userData,
        dataType:JSON,
        success:
            function(data) {
                switch (data) {
                    //Engineering 
                case 1:
                case 2:
                case 6:
                    console.log("1,2,6");
                    break;
                //Sales And marketing
                case 3:
                case 4:
                    console.log("3,4");
                    break;
                //Hr
                case 9:
                    console.log("9");
                    break;
                //IT
                case 17:
                    console.log("17");
                    break;
                //Manufacturing
                case 5:
                case 7:
                case 8:
                case 12:
                case 13:
                case 15:
                    console.log("5,7,8,12,13,15");
                    break;
                //Executive General and Administration
                case 10:
                case 11:
                case 16:
                case 14:
                    console.log("10,11,16,14");
                    break;
                default:
                    console.log("Something went wrong!");
                    break;
                }
            }

    });
});
