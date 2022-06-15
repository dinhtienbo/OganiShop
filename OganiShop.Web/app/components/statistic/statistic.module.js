/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('oganishop.statistics', ['oganishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('statistic_revenue',
                {
                    url: "/statistic_revenue",
                    parent: 'base',
                    templateUrl: "/app/components/statistic/statisticRevenueView.html",
                    controller: "statisticRevenueController"
                })
            .state('statistic_best_seller',
                {
                    url: "/statistic_best_seller",
                    parent: 'base',
                    templateUrl: "/app/components/statistic/statisticBestSellerView.html",
                    controller: "statisticBestSellerController"
                })
            .state('statistic_stock',
                {
                    url: "/statistic_stock",
                    parent: 'base',
                    templateUrl: "/app/components/statistic/statisticStockView.html",
                    controller: "statisticStockController"
                });
    }
})();