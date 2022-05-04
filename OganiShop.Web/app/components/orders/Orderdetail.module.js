/// <reference path="/Assets/admin/libs/angular/angular.js" />


(function () {
    angular.module('oganishop.order_detail', ['oganishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('order_detail', {
            url: "/order_detail",
            templateUrl: "/app/components/orders/orderDetailListView.html",
            parent: 'base',
            controller: "orderDetailListController"
        }).state('order_info', {
            url: "/orders/:id",
            parent: 'base',
            templateUrl: "/app/components/orders/orderDetailView.html",
            controller: "orderDetailController"
        });
    }
})();
