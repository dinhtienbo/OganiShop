(function (app) {
    app.controller('orderEditController', orderEditController);

    orderEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function orderEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.order = {};
        $scope.update = update;


        function loadOrderDetail() {
            apiService.get('/api/orderdetail/getbyid/' + $stateParams.id, null, function (result) {
                $scope.order = result.data;
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