(function (app) {
    app.controller('statisticStockController', statisticStockController);

    statisticStockController.$inject = ['$scope', 'apiService', 'notificationService','$filter'];

    function statisticStockController($scope, apiService, notificationService,$filter) {
        $scope.tabledata = [];
        $scope.labels = [];
        $scope.series = ['Doanh số', 'Lợi nhuận'];

        $scope.chartdata = [];
        function getStatistic() {
            apiService.get('/api/statistic/getstock', null, function (response) {
                $scope.tabledata = response.data;
            }, function (response) {
                notificationService.displayError('Không thể tải dữ liệu');
            });
        }

        getStatistic();
    }

})(angular.module('oganishop.statistics'));