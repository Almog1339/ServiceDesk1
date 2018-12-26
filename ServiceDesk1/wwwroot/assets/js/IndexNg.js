var myApp = angular.module('myApp', ["ngRoute"]);


//myApp.config(function ($routeProvider) {
//    $routeProvider
//        .when("/", { templateUrl: "home.html" })
//        .when("/home", { templateUrl: "home.html" })
//        .when("/chat", { templateUrl: "chat.html" })
//        .when("/NewITRequest", { templateUrl: "NewITRequest.html" });

//});

myApp.controller('IndexMainUl',[$scope,$http,function($scope,$http) {
    $scope.LoginID = localStorage.getItem(username);
    $scope.lists='';

    $http.get("/api/Login", $scope.LoginID)
        .success(function(result) {
            $scope.lists = result;
        });
}]);