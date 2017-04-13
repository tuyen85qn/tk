(function (app) {
    app.controller('situationCategoryListController', situationCategoryListController);
    situationCategoryListController.$inject = ['$scope', 'notificationService', 'apiService', '$filter','$ngBootbox']
    function situationCategoryListController($scope, notificationService, apiService, $filter, $ngBootbox) {
        $scope.situationCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.filterExpression = '';
        $scope.loading = true;
        $scope.search = search;     
        $scope.delSituationCategory = delSituationCategory;

        function delSituationCategory(id) {
            $ngBootbox.confirm('Bạn có muốn xóa không?').then(function(){
                var config ={
                    params:{
                        id:id
                    }
                };
                apiService.del('/api/situationCategory/delete', config, function (result) {
                    notificationService.displaySuccess(result.data.Name + 'đã xóa thành công.');
                    search();
                }, function (error) {
                    notificationService.displayError(error.data.Message);
                });
            });
            
        }
        function search(page) {
            page = page || 0;

            $scope.loading = true;
            var config = {
                params: {
                    page: page,
                    pageSize: 5,
                    filter: $scope.filterExpression
                }
            }

            apiService.get('/api/situationCategory/getlistpaging', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không tìm thấy bản ghi nào.');
                }
                $scope.situationCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                if ($scope.filterExpression && $scope.filterExpression.length) {
                    notificationService.displayInfo(result.data.Items.length + ' items found');
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        $scope.search();
    }
})(angular.module('tk.situation_categories'));