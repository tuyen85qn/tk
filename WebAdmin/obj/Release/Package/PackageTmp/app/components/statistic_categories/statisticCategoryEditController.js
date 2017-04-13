(function (app) {
    app.controller('statisticCategoryEditController', statisticCategoryEditController);
    statisticCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService', 'commonService','$stateParams', '$state'];

    function statisticCategoryEditController($scope, apiService, notificationService, commonService, $stateParams, $state) {
        $scope.statisticCategory = null;
        $scope.parentCategories = [];
        $scope.updateStatisticCategory = updateStatisticCategory;
        $scope.GetSeoTitle = GetSeoTitle;

        function loadStatisticCategoryDetail() {
            apiService.get('/api/statisticCategory/getbyid/' + $stateParams.id, null,
                function (result) {
                    $scope.statisticCategory = result.data;
                },
                function (error) {
                    notificationService.displayError(error.Data);
                });
        }
        function updateStatisticCategory() {
            apiService.put('/api/statisticCategory/update', $scope.statisticCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('statistic_categories');

                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function GetSeoTitle() {
            $scope.statisticCategory.Alias = commonService.getSeoTitle($scope.statisticCategory.Name);
        }

        function loadParentCategories() {
            apiService.get('/api/statisticCategory/getall', null,
                function (result) {
                    $scope.parentCategories = result.data;
                }, function (error) {
                    notificationSerivce.displayError('Không thể load danh sách danh mục.')
                });
        }

        loadStatisticCategoryDetail();
        loadParentCategories();
    }
})(angular.module('tk.statistic_categories'));