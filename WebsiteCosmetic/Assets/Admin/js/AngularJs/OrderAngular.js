var orderApp = angular.module("orderApp", []);
orderApp.controller("OrderController", OrderController);

function OrderController($scope, $http) {
    $http.get("/Admin/Order/getAllOrder").then(function (res) {
        $scope.datas = JSON.parse(res.data)
    }, function (error) {
        alert("failed")
    })

    $(document).on('click', '#btnDetail', function () {
        $('#bigModal').modal('show');
    })
}