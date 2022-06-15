(function (app) {
    app.controller('statisticRevenueController', statisticRevenueController);

    statisticRevenueController.$inject = ['$scope', 'apiService', 'notificationService','$filter'];

    function statisticRevenueController($scope, apiService, notificationService,$filter) {
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
        $scope.totalRevenues = 0;
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
            console.log('get statistic')
            var config = {
                param: {
                    //mm/dd/yyyy
                    fromDate: $scope.startDate,
                    toDate: $scope.endDate
                }
            }
            apiService.get('/api/statistic/getrevenue?fromDate=' + config.param.fromDate + "&toDate=" + config.param.toDate, null, function (response) {
                $scope.tabledata = response.data;
                $scope.totalRevenues = 0;
                for (var i = 0; i < response.data.length; i++) {
                    if (response.data[i] && response.data[i].Revenues) {
                        $scope.totalRevenues += response.data[i].Revenues;
                    }
                }
                var labels = [];
                var chartData = [];
                var revenues = [];
                
                $.each(response.data, function (i, item) {
                    labels.push($filter('date')(item.Date,'dd/MM/yyyy'));
                    revenues.push(item.Revenues);
                    
                });
                chartData.push(revenues);
                
                $scope.chartdata = chartData;
                $scope.labels = labels;
            }, function (response) {
                notificationService.displayError('Không thể tải dữ liệu');
            });
        }

        getStatistic();
    }

})(angular.module('oganishop.statistics'));