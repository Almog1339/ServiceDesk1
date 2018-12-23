var app = angular.module("app", ["ngRoute"])
    .config(function($routeProvider,$locationProvider) {
        $routeProvider
            .when("/home",
                {
                    templateUrl: "/wwwroot/home.html"
                })
            .when("/Chat",
                {
                    templateUrl: "/wwwroot/Chat.html"
                })
                $locationProvider.html5Mode(true);
    });


