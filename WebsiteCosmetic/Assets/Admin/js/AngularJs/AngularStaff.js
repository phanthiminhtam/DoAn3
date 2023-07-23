var staffApp = angular.module("staffApp", []);
staffApp.controller("StaffController", StaffController);

// Lấy danh sách Staff
function StaffController($scope, $http) {
    $http.get("/Admin/Staff/getStaff").then(function (res) {
        $scope.datas = JSON.parse(res.data)
    }, function (error) {
        alert("failed")
    })

    //Thêm nhân viên
    $scope.Save = function () {
        $http({
            method: 'POST',
            url: '/Admin/Staff/Create',
            datatype: 'Json',
            data: { staff: $scope.staff}
        }).then(function (res) {
            if (res.data.message) {
                alert("Thêm nhân viên thành công!");
                window.location.href = "/Admin/Staff/Index";
            }
            else {
                alert("Thêm nhân viên không thành công!");
            }
        })
    }

    
}
