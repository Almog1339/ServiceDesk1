var myApp = angular.module('myApp', ["ngRoute"]);

myApp.config(function ($routeProvider) {
    $routeProvider
        .when("/", { templateUrl: "/Pages/Global/Self-Service.html" })
        .when("/HR", { templateUrl: "/Pages/Hr/HR.html"})
        .when("/Self-Service", { templateUrl: "/Pages/Global/Self-Service.html" })
        .when("/Organization", {templateUrl: "/Pages/Global/Organization.html" })
        .when("/Incident", { templateUrl: "/Pages/IT/Incident.html" })
        .when("/Service Desk", { templateUrl: "/Pages/IT/Service Desk.html" })
        .when("/Chat", { templateUrl: "/Pages/Global/Chat.html" })
        .when("/Profile", { templateUrl: "/Pages/Global/Profile.html" })
        .when("/Settings", { templateUrl: "/Pages/Global/Settings.html" })
        .when("/UsersList", { templateUrl: "/Pages/Global/UsersList.html", controller: "OrganizationCtrl"});

});

myApp.controller('navCtrl', ['$scope', '$window', function ($scope, $window) {
    $scope.LoginID = JSON.parse($window.localStorage.getItem('username'));
    //when the chat function will work need to write a get request.
    //and use $scope.Notificaciones
    
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

myApp.controller('OrganizationCtrl', ['$scope', '$http', function($scope,$http) {
    $http.get("/api/Global".then(function(response) {
        $scope.td = response.data;
    }));
}]);