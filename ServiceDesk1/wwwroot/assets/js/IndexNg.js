var myApp = angular.module('myApp', ["ngRoute"]);

myApp.config(function ($routeProvider) {
    $routeProvider
        .when("/", { templateUrl: "/Pages/Global/Self-Service.html" })
        .when("/HR", { templateUrl: "/Pages/Hr/HR.html" })
        .when("/Self-Service", { templateUrl: "/Pages/Global/Self-Service.html" })
        .when("/Organization", { templateUrl: "/Pages/Global/Organization.html" })
        .when("/Service Desk", { templateUrl: "/Pages/IT/Service Desk.html" })
        .when("/Inventory", { templateUrl: "/pages/Inventory/Inventory.html" })
        .when("/Location", { templateUrl: "/Pages/Global/Location.html", controller: "LocationCtrl" })
        .when("/Profile", { templateUrl: "/Pages/Global/Profile.html", controller: "navCtrl" })
        .when("/UsersList", { templateUrl: "/Pages/Global/UsersList.html", controller: "OrganizationCtrl" })
        .when("/JobCandidate", { templateUrl: "/pages/HR/JobCandidate.html", controller: "HrCtrl" })
        .when("/NewPosition", { templateUrl: "/pages/HR/NewPosition.html", controller: "HrCtrl" })
        .when("/AssignToMe", { templateUrl: "/pages/IT/AssignToMe.html", controller: "AssignToMeCtrl" })
        .when("/DepartmentList", { templateUrl: "/Pages/Global/DepartmentList.html", controller: "OrganizationCtrl" })
        .when("/KnowledgeBase", { templateUrl: "/Pages/IT/KnowledgeBase.html", controller: "KnowledgeCtrl" })
        .when("/NewArticle", { templateUrl: "/pages/IT/NewArticle.html", controller: "ArticleCtrl" })
        .when("/Unassigned", { templateUrl: " /pages/IT/Unassigned.html", controller: "UnassignedCtrl" })
        .when("/OpenTicket", { templateUrl: "/pages/IT/OpenTicket.html", controller: "OpenTicket" })
        .when("/ResolvedInc", { templateUrl: "/pages/IT/ResolveInc.html", controller: "ResolveIncCtrl" })
        .when("/Suppliers", { templateUrl: "/pages/Inventory/Suppliers.html", controller: "SuppliersCtrl" })
        .when("/Orders", { templateUrl: "/pages/Inventory/Orders.html", controller:"OrdersCtrl" })
        .when("/NewInc", { templateUrl: "/pages/IT/NewInc.html", controller: "IncCtrl" });


});
myApp.controller('IncCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {
    $scope.LoginID = JSON.parse($window.localStorage.getItem('username'));
    $http.get('api/IT/GetNewInc').then(function (response) {
        if (response.data === -1) {
            alert("Please try again later or content your local help desk");
        } else {
            $scope.NewIncNum = response.data;
        }
    });

    $scope.SubmitNewTicket = function () {
        $http.post("api/IT/SubmitNewTicket?LoginID=" + $scope.LoginID + "&shortDescription=" + $scope.Short_Description + "&description=" + $scope.description + "&category=" + $scope.category).then(function (response) {
            if (response.data === -1 | response.data === false) {
                alert("we encouter some issues please try again alter or content your local help desk");
            } else {
                alert("done");
            }
        });
    };
}]);

myApp.controller('OrdersCtrl', ['$scope', '$http', function ($scope, $http) {
    $http.get("api/Inventory/GetOrders").then(function (response) {
        $scope.orders = response.data;
    });
    $scope.colToSortBy = "orderId";
    $scope.sortBy = function (col) {
        $scope.colToSortBy = col;
    };
    $scope.createOrder = function () {
        $http.post("api/invetory/NewOrder?businessEntityID=" + $scope.businessEntityID + "&OrderDate="+ $scope.OrderDate + "&Required_Date="+ $scope.Required_Date + "&Shipped_Date=" + $scope.Shipped_Date + "&Freight=" + $scope.Freight + "&ShipName=" + $scope.ShipName + "&ShipAddress=" + $scope.ShipAddress + "&ShipCity=" + $scope.ShipCity +"&ShipCountry=" + $scope.ShipCountry).then(function (response) {
            if (response.data === -1) {
                alert("we have encounter some issues /nPlease try again later");
            } else {
                alert("done");
            }
        });
    };
}]);
myApp.controller('SuppliersCtrl', ['$scope', '$http', function ($scope, $http) {
    $http.get("api/Inventory/GetSuppliers").then(function (response) {
        $scope.suppliers = response.data;
    });
    $scope.colToSortBy = "supplierID";
    $scope.sortBy = function (col) {
        $scope.colToSortBy = col;
    };
}]);

myApp.controller('LocationCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.countryList = ["Alaska", "Alabama", "Arkansas", "American Samoa", "Arizona", "California", "Colorado", "Connecticut", "District of Columbia", "Delaware", "Florida", "Georgia", "Guam", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", " North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Virgin Islands", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming"];
    $scope.complete = function (string) {
        var output = [];
        angular.forEach($scope.countryList, function (country) {
            if (country.toLowerCase().indexOf(string.toLowerCase()) >= 0) {
                output.push(country);
            }
        });
        $scope.filterCountry = output;
    };
    $scope.fillTextbox = function (string) {
        $scope.country = string;
        $scope.hideList = true;
        setTimeout(function () {
            $http.get("api/Global/GetStores?State=" + $scope.country).then(function (resoponse) {
                $scope.storeLists = resoponse.data;
            });
        }, 1000);

    };
}]);

myApp.controller('UnassignedCtrl', ['$scope', '$http', function ($scope, $http) {
    $http.get("api/IT/GetUnassigned").then(function (response) {
        $scope.td = response.data;
    });
}]);
myApp.controller('ResolveIncCtrl', ['$scope', '$http', function ($scope, $http) {
    $http.get("api/IT/ResolveInc").then(function (response) {
        $scope.td = response.data;
    });
}]);

myApp.controller("OpenTicket", ['$scope', '$http', function ($scope, $http) {
    $http.get("api/IT/GetOpenTicket").then(function (response) {
        $scope.td = response.data;
    });
}]);
myApp.controller("AssignToMeCtrl", ['$scope', '$http', function ($scope, $http) {
    $http.get("api/IT/AssignToMe?userName=" + $scope.LoginID).then(function (response) {
        $scope.td = response.data;
    });
}]);
myApp.controller('navCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window, ) {

    $scope.LoginID = JSON.parse($window.localStorage.getItem('username'));


    $http.get("api/Global/GetImg?loginID=" + $scope.LoginID).then(function (response) {
        $scope.ImgData = response.data;
    });

    $http.get("api/Global/GetAllImg").then(function (response) {
        $scope.images = response.data;
    });

    $http.get("api/Global/info?LoginID=" + $scope.LoginID).then(function (response) {
        $scope.information = response.data;
        $window.localStorage.setItem('information', JSON.stringify(response.data));
        var i = JSON.parse($window.localStorage.getItem("information"));
        $scope.firstName = i[0].firstName;
        $scope.lastName = i[0].lastName;
        $scope.BusinessEntityID = i[0].businessEntityID;
    });

    $scope.UpdateProfile = function () {
        $http.post("api/Global/info?firstName=" +
            $scope.firstName +
            "&lastName=" +
            $scope.lastName +
            "&BusinessEntityID=" +
            $scope.BusinessEntityID).then(function (response) {
                if (response.data === -1 | response.data === false) {
                    alert("We could not update your information please contact your system administrator or the HR");
                } else {
                    alert("Done you are now can continue your work");
                }
            });
    };
    //$scope.Password = '';

    //$scope.Reset = function () {
    //    console.log($scope.Password);
    //    console.log($scope.newPassword);

    //    $http.post("api/Global/passwordReset?LoginID=" + $scope.LoginID + "&Password=" + $scope.Password + "&NewPassword=" + $scope.newPassword + "&BusinessEntityID=" + $scope.BusinessEntityID).then(function (response) {
    //        if (response.data === -1 | response.data === false) {
    //            alert("We could not update your password please content your local administrator.");
    //        } else {
    //            alert("your password has been update successfuly");
    //        }
    //    });
    //};

}]);

myApp.controller('HrCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.LoginID = JSON.parse(window.localStorage.getItem('username'));
    $http.get("api/HR").then(function (response) {
        $scope.OpenJobs = response.data;
        console.log(response.data);
    });
    $http.get("api/HR/Candidate").then(function (response) {
        $scope.Candidates = response.data;
    });
    $http.get("api/HR/GetPositionID").then(function (response) {
        if (response.data === -1 | response.data === false) {
            alert("Please try again later");
        } else {
            $scope.NewPositionID = response.data;
        }
    });
    $scope.OpenNewPosition = function () {
        $http.post("api/HR?JobTitle=" + $scope.JobTitle + "&JobDescription=" + $scope.JobDescription + "&Department=" + $scope.Hiring_Department).then(function (response) {
            if (response.data === -1 | response.data === false) {
                alert("Please try again later");
            } else {
                alert("Done");
            }
        });
    };

}
]);
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
        $scope.option = response.data;
    });
}]);

myApp.controller('OrganizationCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.colToSortBy = "supplierID";
    $scope.sortBy = function (col) {
        $scope.colToSortBy = col;
    };
    $http.get("/api/Global/UsersList").then(function (response) {
        $scope.td = response.data;
    });
    $http.get("/api/Global/Department").then(function (response) {
        $scope.DepartmentList = response.data;
    });
}]);

myApp.controller('KnowledgeCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.subject;
    console.log($scope.subject);
    $http.get("/api/Knowledge/GetArticles").then(function (response) {
        $scope.items = response.data;
        console.log(response.data);
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
            "&Subject=" +
            $scope.Subject +
            "&Content=" +
            $scope.content +
            "&BusinessEntityID=" +
            $scope.BusinessEntityID +
            "&PostedByLoginID=" +
            $scope.LoginID +
            "&title=" +
            $scope.title
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

