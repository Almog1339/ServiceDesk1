
var myApp = angular.module('myApp', ["ngRoute"]);


myApp.config(function ($routeProvider) {
    $routeProvider
        .when("/", { templateUrl: "home.html" })
        .when("/home", { templateUrl: "home.html" })
        .when("/chat", { templateUrl: "chat.html" })
        .when("/NewITRequest", { templateUrl: "NewITRequest.html" });

});