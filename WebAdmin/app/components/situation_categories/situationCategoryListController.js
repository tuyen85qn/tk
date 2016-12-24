(function (app) {
    app.controller('situationCategoryListController', situationCategoryListController);
    situationCategoryListController.$inject = ['$scope', 'notificationService', 'apiService', '$filter','$ngBootbox']
    function situationCategoryListController($scope, notificationService, apiService, $filter, $ngBootbox) {
        $scope.situationCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.search = search;
        $scope.getSituationCategories = getSituationCategories;
        $scope.delSituationCategory = delSituationCategory;

        function search() {
            $scope.getSituationCategories();
        }

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
        function getSituationCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 5
                }
            };

            apiService.get('/api/situationCategory/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không tìm thấy bản ghi nào.');
                }
                $scope.situationCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load situation categories failed.');
            });
        }
        $scope.getSituationCategories();
    }
})(angular.module('tk.situation_categories'));