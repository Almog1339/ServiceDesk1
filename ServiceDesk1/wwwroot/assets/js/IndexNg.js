﻿var myApp = angular.module('myApp', ["ngRoute"]);

myApp.config(function ($routeProvider) {
    $routeProvider
        .when("/", { templateUrl: "/Pages/Global/Self-Service.html" })
        .when("/HR", { templateUrl: "/Pages/Hr/HR.html" })
        .when("/Self-Service", { templateUrl: "/Pages/Global/Self-Service.html" })
        .when("/Organization", { templateUrl: "/Pages/Global/Organization.html" })
        .when("/Incident", { templateUrl: "/Pages/IT/Incident.html" })
        .when("/Service Desk", { templateUrl: "/Pages/IT/Service Desk.html" })
        .when("/Chat", { templateUrl: "/Pages/Global/Chat.html" })
        .when("/LocationMap", { templateUrl: "/Pages/Global/LocationMap.html", controller: "SimpleMapController" })
        .when("/Profile", { templateUrl: "/Pages/Global/Profile.html" })
        .when("/Settings", { templateUrl: "/Pages/Global/Settings.html",controller:"SettingsCtrl" })
        .when("/UsersList", { templateUrl: "/Pages/Global/UsersList.html", controller: "OrganizationCtrl" })
        .when("/DepartmentList", { templateUrl: "/Pages/Global/DepartmentList.html", controller: "OrganizationCtrl" })
        .when("/KnowledgeBase", { templateUrl: "/Pages/IT/KnowledgeBase.html", controller: "KnowledgeCtrl" })
        .when("/NewArticle", { templateUrl: "/pages/IT/NewArticle.html", controller: "ArticleCtrl" });


});

myApp.controller('navCtrl', ['$scope','$http', '$window', function ($scope,$http, $window) {
    $scope.LoginID = JSON.parse($window.localStorage.getItem('username'));

    //$http.get("api/index/image?LoginID=" + $scope.LoginID).then(function(response) {
    //    $scope.img = response.data;
    //});
    //when the chat function will work need to write a get request.
    //and use $scope.Notificaciones

}]);
myApp.controller('SettingsCtrl', ['$scope', function($scope) {
    $scope.Settings = [{
        "name": "Themes"
    }, {
        "name": "Profile Settings"
    }]
}]);

myApp.controller('IndexMainUl', ['$scope', '$http', '$window', function ($scope, $http, $window) {
    $scope.lists = '';
    $scope.LoginID = JSON.parse($window.localStorage.getItem('username'));

    $http.get("api/Login?LoginID=" + $scope.LoginID).then(function (response) {
        if (response.data === -1) {
            alert("Please try to login again later we have encounter some problems...");
            console.log("Please try to login again later we have encounter some problems...");

        } else if (response.data >= 1) {
            $window.localStorage.setItem('departmentId', JSON.stringify(response.data));

        } else {
            alert("Please try to login again later we have encounter some problems...");
        }
    });
    $scope.departmentID = JSON.parse($window.localStorage.getItem('departmentId'));

    $http.get("api/index?departmentId=" + $scope.departmentID).then(function (response) {
        $window.localStorage.setItem('option', JSON.stringify(response.data));
        $scope.option = response.data;
    });
}]);

myApp.controller('OrganizationCtrl', ['$scope', '$http', function ($scope, $http) {
    $http.get("/api/Global/UsersList").then(function (response) {
        $scope.td = response.data;
    });
    $http.get("/api/Global/Department").then(function (response) {
        $scope.DepartmentList = response.data;
    });
}]);

myApp.controller('KnowledgeCtrl', ['$scope', '$http', function ($scope, $http) {
    $http.get("/api/Knowledge/GetArticles").then(function (response) {
        $scope.items = response.data;
    });
}]);

myApp.controller('ArticleCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {
    $scope.LoginID = JSON.parse($window.localStorage.getItem('username'));

    $http.get("api/Knowledge/EntityID?userName=" + $scope.LoginID).then(function (response) {
        $scope.BusinessEntityID = response.data;
    });
    $http.get("/api/Knowledge").then(function (response) {
        if (response.data === -1) {
            alert("Please try again later");
        } else {
            console.log(response.data);
            $scope.ID = response.data;
        }
    });

    $scope.submitArticle = function () {

        $http.post("api/Knowledge?ID=" +
            $scope.ID +
            "&Title=" +
            $scope.title +
            "&Content=" +
            $scope.content +
            "&BusinessEntityID=" +
            $scope.BusinessEntityID +
            "&PostedByLoginID=" +
            $scope.LoginID
        ).then(function (response) {
            if (response.status === -1) {
                alert("We have encounter some problem please try again later");
            } else {
                alert("Done... we will redirect you shortly back to home page");
                $window.location.href("/KnowledgeBase.html");
            }
        });
    };
}
]);