(function (app) {
    app.controller('orderDetailController', orderDetailController);

    orderDetailController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function orderDetailController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.order = {};
        $scope.update = update;
        $scope.totalPrice = 0;

        function loadOrderDetail() {
            apiService.get('/api/orderdetail/getbyid/' + $stateParams.id, null, function (result) {
                $scope.order = result.data;

                if ($scope.order && $scope.order.OrderDetails) {
                    for (var i = 0; i < $scope.order.OrderDetails.length; i++) {
                        var orderDetail = $scope.order.OrderDetails[i];
                        $scope.totalPrice += orderDetail.Quantity * orderDetail.CurrentPrice;
                    }
                }
                console.log($scope.order)
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function update() {
            apiService.put('/api/orderdetail/' + $stateParams.id, $scope.order,
                function (result) {
                    notificationService.displaySuccess('Hóa đơn có mã ' + result.data.ID + ' đã được cập nhật.');
                    $state.go('order_detail');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
       
        loadOrderDetail();
    }

})(angular.module('oganishop.order_detail'));