var providerApp = angular.module("providerApp", []);
providerApp.controller("ProviderController", ProviderController);

function ProviderController($scope, $http) {
    $http.get("/Admin/Provider/getAllProvider").then(function (res) {
        $scope.datas = res.data
    }, function (error) {
        alert("failed")
    })

    //Thêm nhà cung cấp
    $scope.Save = function () {
        $http({
            method: 'POST',
            url: '/Admin/Provider/Create',
            datatype: 'Json',
            data: { prv: $scope.prv }
        }).then(function (res) {
            if (res.data.message) {
                alert("Thêm nhà cung cấp thành công!");
                $('#exampleModal').modal('hide');
            }
            else {
                alert("Thêm nhà cung cấp không thành công!");
            }
        })
    }
    $scope.Delete = function (prv, index) {
        if (confirm(`Bạn có muốn xoá nhà cung cấp này không?`)) {
            $http({
                method: 'Post',
                url: '/Admin/Provider/Delete',
                dataType: 'Json',
                data: { id: prv.PrvId }
            }).then(function (res) {
                console.log(res.data.message)
                if (res.data.message) {
                    $scope.datas.splice(index, 1);
                    alert("Xoá nhà cung cấp thành công!");
                }
                else {
                    alert("Xoá không thành công!");
                }
            })
        }
    }

    $scope.Edit = function (data) {
        $scope.provider = data;
        console.log(data)
        console.log($scope.provider)
    }
    $scope.SaveEdit = function () {
        $http({
            method: "POST",
            url: "/Admin/Provider/Edit",
            datatype: 'Json',
            data: { provider: $scope.provider }
        }).then(function (res) {
            $("#btn_close").trigger('click') 
            $(".modal-backdrop .fade .show").css({ 'display': 'none' })
        })
    }
}