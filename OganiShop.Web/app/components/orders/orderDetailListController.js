(function (app) {
    app.controller('orderDetailListController', orderDetailListController);

    orderDetailListController.$inject = ['$scope', 'apiService', 'notificationService', '$window', '$filter'];

    function orderDetailListController($scope, apiService, notificationService, $window, $filter) {
        $scope.orderDetail = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getorderDetail = getorderDetail;
        $scope.keyword = '';
        $scope.totalCount = 0;
        $scope.orderStatus = '';
        $scope.search = search;
        $scope.ShowConfirm = ShowConfirm;

        $scope.updateStatus = updateStatus;

        $scope.operatorAllow = operatorAllow;

        function operatorAllow(index) {
            var item = $scope.orderDetail[index];
            return {
                thanhtoan: item.OrderStatus != 'Hủy đơn hàng' && item.PaymentStatus == 'not',
                huydonhang: item.PaymentStatus == 'not' && item.OrderStatus == 'Chờ xử lý',
                xuly: item.OrderStatus == 'Chờ xử lý',
                giaohang: item.OrderStatus == 'Đang xử lý',
                hoanthanh: item.PaymentStatus == 'yes' && item.OrderStatus == 'Đang giao'
            }
        }

        //$scope.selectAll = selectAll;

        //$scope.deleteMultiple = deleteMultiple;

        //function deleteMultiple() {
        //    var listId = [];
        //    $.each($scope.selected, function (i, item) {
        //        listId.push(item.ID);
        //    });
        //    var config = {
        //        params: {
        //            checkedorderDetail: JSON.stringify(listId)
        //        }
        //    }
        //    apiService.del('api/productcategory/deletemulti', config, function (result) {
        //        notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
        //        search();
        //    }, function (error) {
        //        notificationService.displayError('Xóa không thành công');
        //    });
        //}

        //$scope.isAll = false;
        //function selectAll() {
        //    if ($scope.isAll === false) {
        //        angular.forEach($scope.orderDetail, function (item) {
        //            item.checked = true;
        //        });
        //        $scope.isAll = true;
        //    } else {
        //        angular.forEach($scope.orderDetail, function (item) {
        //            item.checked = false;
        //        });
        //        $scope.isAll = false;
        //    }
        //}

        //$scope.$watch("orderDetail", function (n, o) {
        //    var checked = $filter("filter")(n, { checked: true });
        //    if (checked.length) {
        //        $scope.selected = checked;
        //        $('#btnDelete').removeAttr('disabled');
        //    } else {
        //        $('#btnDelete').attr('disabled', 'disabled');
        //    }
        //}, true);
    

        function ShowConfirm(id, type) {
            var message = "";
            if (type === 'xuly') {
                message = 'Bạn có chắc xác nhận bắt đầu XỬ LÝ đơn hàng này'
            }
            else if (type === 'giaohang') {
                message = 'Bạn có chắc xác nhận bắt đầu GIAO HÀNG đơn hàng này'
            }
            else if (type === 'thanhtoan') {
                message = 'Bạn có chắc xác nhận đơn hàng này đã được THANH TOÁN'
            } else if (type === 'hoanthanh') {
                message = 'Bạn có chắc xác nhận HOÀN THÀNH đơn hàng này'
            } else if (type === 'huydonhang') {
                message = 'Bạn có chắc xác nhận HỦY đơn hàng này'
            }
            if ($window.confirm(message)) {
                $scope.updateStatus(id, type)
            } else {
                $scope.Message = "You clicked NO.";
            }
        }

        function updateStatus(id, type) {
            var config = {
                type: type
            }
            apiService.put('/api/orderdetail/updatebyid/' + id, config, function () {
                notificationService.displaySuccess('Thành công');
                search();
            }, function () {
                notificationService.displayError('Thất bại');
            })
        }

        function search() {
            getorderDetail();
        }

        function getorderDetail(orderStatus, page) {
            $scope.orderStatus = orderStatus;
            if (!orderStatus) {
                orderStatus = ''
            }
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20,
                    orderStatus: orderStatus
                }
            }
            apiService.get('/api/orderdetail/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                $scope.orderDetail = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load orderdetail failed.');
            });
        }

        $scope.getorderDetail();
    }
})(angular.module('oganishop.order_detail'));