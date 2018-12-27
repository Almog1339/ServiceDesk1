var myApp = angular.module('myApp', ["ngRoute"]);

myApp.controller('nameCtrl', ['$scope', '$window', function ($scope, $window) {
    $scope.LoginID = JSON.parse($window.localStorage.getItem('username'));

}]);

myApp.controller('IndexMainUl', ['$scope', '$http', '$window', function ($scope, $http, $window) {
    $scope.lists = '';
    $scope.LoginID = JSON.parse($window.localStorage.getItem('username'));

    $http.get("api/Login?LoginID=" + $scope.LoginID).then(function (response) {
        if (response.data === -1) {
            console.log("Please try to login again later we have encounter some problems...");

        } else if (response.data >= 1) {
            $window.localStorage.setItem('departmentId', JSON.stringify(response.data));

        } else {
            console.log("Please try to login again later we have encounter some problems...");
            console.log(response.data);

        }
    });
    setTimeout(1000);
    $scope.departmentID = JSON.parse($window.localStorage.getItem('departmentId'));

    $http.get("api/index?departmentId=" + $scope.departmentID).then(function (response) {
        $scope.tests = response.data.toString();
    });
}]);


//myApp.config(function ($routeProvider) {
//    $routeProvider
//        .when("/", { templateUrl: "home.html" })
//        .when("/home", { templateUrl: "home.html" })
//        .when("/chat", { templateUrl: "chat.html" })
//        .when("/NewITRequest", { templateUrl: "NewITRequest.html" });

//});
