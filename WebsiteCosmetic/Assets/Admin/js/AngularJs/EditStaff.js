
var staffApp = angular.module("staffApp", []);
staffApp.controller("StaffController", StaffController);

// Lấy danh sách Staff
let id = $("#main").attr("data-id")
function StaffController($scope, $http) {
    $http.get("/Admin/Staff/getById/" + id).then(function (res) {
        let result = JSON.parse(res.data)
        $scope.staff = {
            StaffName: result.StaffName,
            StaffId: result.StaffId,
            Address: result.Address,
            PhoneNumber: result.PhoneNumber,
            ContractWork: result.ContractWork,
            Email: result.Email,
            BasicSalary: JSON.stringify(result.BasicSalary),
            Post: result.Post,
            StartDate: new Date(result.StartDate),
            TypeWork: result.TypeWork
        }
    }, function (error) {
        alert("failed")
    })

    
    $scope.Edit = function () {
        $http({
            method: "POST",
            url: "/Admin/Staff/Edit",
            datatype: 'Json',
            data: { s: $scope.staff }
        }).then(function (res) {
            window.location.href = "/Admin/Staff/Index";
        })
    }
}

