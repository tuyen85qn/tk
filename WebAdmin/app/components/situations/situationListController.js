(function (app) {
    app.controller('situationCategoryListController', situationCategoryListController);
    situationCategoryListController.$inject = ['$scope', 'notificationService', 'apiService', '$filter','$ngBootbox']
    function situationCategoryListController($scope, notificationService, apiService, $filter, $ngBootbox) {
        $scope.loading = true;
        $scope.data = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.search = search;
        $scope.filterExpression ="";       

        function delSituation(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?')
                .then(function () {
                    var config = {
                        params: {
                            id: id
                        }
                    }
                    apiService.del('/api/situation/delete', config, function () {
                        notificationService.displaySuccess('Đã xóa thành công.');
                        search();
                    },
                    function () {
                        notificationService.displayError('Xóa không thành công.');
                    });
                });
        }
        function search(page) {
            page = page || 0;

            $scope.loading = true;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterExpression
                }
            }

            apiService.get('/api/situation/getlistpaging', config, function (result) {
                $scope.data = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
                if ($scope.filterExpression && $scope.filterExpression.length) {
                    notificationService.displayInfo(result.data.Items.length + ' items found');
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }    

        $scope.search();
    }
})(angular.module('tk.situations'));