(function (app) {
    app.controller('statisticBestSellerController', statisticBestSellerController);

    statisticBestSellerController.$inject = ['$scope', 'apiService', 'notificationService','$filter'];

    function statisticBestSellerController($scope, apiService, notificationService,$filter) {
        $scope.tabledata = [];
        $scope.labels = [];
        $scope.series = ['Doanh số', 'Lợi nhuận'];
        $scope.startDate = '01/01/2022';
        $scope.endDate = '01/01/2023';
        $scope.getStatistic = getStatistic;

        $scope.byDay = byDay;
        $scope.byMonth = byMonth;
        $scope.byQuarter = byQuarter;
        $scope.byYear = byYear;

        today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //As January is 0.
        var yyyy = today.getFullYear();

        function getValue(dd, mm, yyyy) {
            return mm + '/' + dd + '/' + yyyy;
        }

        function byDay() {
            $scope.startDate = getValue(dd, mm, yyyy);
            $scope.endDate = getValue(dd, mm, yyyy);
            getStatistic();
        }
        function byMonth() {
            $scope.startDate = getValue(1, mm, yyyy);
            $scope.endDate = getValue(dd, mm, yyyy);
            getStatistic();
        }
        function byQuarter() {
            var startQuarter = 1;
            if (mm >= 10) {
                startQuarter = 10;
            } else if (mm >= 7) {
                startQuarter = 7;
            } else if (mm >= 4) {
                startQuarter = 4;
            } else if (mm >= 1) {
                startQuarter = 1;
            }
            $scope.startDate = getValue(1, startQuarter, yyyy);
            $scope.endDate = getValue(dd, mm, yyyy);
            getStatistic();
        }
        function byYear() {
            $scope.startDate = getValue(1, 1, yyyy);
            $scope.endDate = getValue(dd, mm, yyyy);
            getStatistic();
        }


        $scope.chartdata = [];
        function getStatistic() {
            var config = {
                param: {
                    //mm/dd/yyyy
                    fromDate: $scope.startDate,
                    toDate: $scope.endDate
                }
            }
            apiService.get('/api/statistic/getbestseller?fromDate=' + config.param.fromDate + "&toDate=" + config.param.toDate, null, function (response) {
                $scope.tabledata = response.data;
            }, function (response) {
                notificationService.displayError('Không thể tải dữ liệu');
            });
        }

        getStatistic();
    }

})(angular.module('oganishop.statistics'));