var myApp = angular.module('myApp', ["ngRoute"]);

myApp.controller('IndexMainUl', ['$scope', '$http', function ($scope, $http) {
    $scope.lists = '';
    $scope.LoginID = localStorage.username;
    var data = {
        LoginID: $scope.LoginID
    };

    $http.get('api/Login', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (result) {
        if (result === -1) {
            $scope.lists = ["Please try to login again later we have encounter some problems...", 1];
            return console.log(result);
        } else if (data >= 1) {
            localStorage.setItem("department", data);
            $scope.department = localStorage.department;
            $http.get("api/index", $scope.department).then(function () {
                if (result === -1) {
                    $scope.lists = "Please try to login again later we have encounter some problems...";
                    return console.log(data + " from the second get request");

                } else {
                    $scope.lists = data;
                    return console.log($scope.lists);
                }
            });
        } else {
            $scope.lists = "Please try to login again later we have encounter some problems...";
            console.log(data + " only god know why it fail at this point...");

               }
    });
}]);


//myApp.config(function ($routeProvider) {
//    $routeProvider
//        .when("/", { templateUrl: "home.html" })
//        .when("/home", { templateUrl: "home.html" })
//        .when("/chat", { templateUrl: "chat.html" })
//        .when("/NewITRequest", { templateUrl: "NewITRequest.html" });

//});
