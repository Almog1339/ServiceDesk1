var myApp = angular.module('myApp', ["ngRoute"]);

myApp.config(function ($routeProvider) {
    $routeProvider
        .when("/", { templateUrl: "Self-Service.html" })
        .when("/hr", { templateUrl: "HR.html" })
        .when("/Self-Service", { templateUrl: "Self-Service.html" })
        .when("/Organization", { templateUrl: "Organization.html" })
        .when("/Incident", { templateUrl: "Incident.html" })
        .when("/Service Desk", { templateUrl: "Service Desk.html" });

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