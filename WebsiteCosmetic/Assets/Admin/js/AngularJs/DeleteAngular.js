
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
            data: { staff: $scope.staff }
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
    $scope.Delete = function (staff, index) {
        if (confirm(`Bạn có muốn xoá nhân viên này không?`)) {
            $http({
                method: 'Post',
                url: '/Admin/Staff/Delete',
                dataType: 'Json',
                data: { id: staff.StaffId }
            }).then(function (res) {
                console.log(res.data.message)
                if (res.data.message) {
                    $scope.datas.splice(index, 1);
                    alert("Xoá nhân viên thành công!");
                    
                }
                else {
                    alert("Xoá không thành công!");
                }
            })
        }
    }
}

