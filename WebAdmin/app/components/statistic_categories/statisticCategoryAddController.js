(function (app) {
    app.controller('statisticCategoryAddController', statisticCategoryAddController);
    statisticCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', 'commonService', '$state'];

    function statisticCategoryAddController($scope, apiService, notificationService, commonService, $state) {
        $scope.statisticCategory = {          
            Status: true
        };
        $scope.parentCategories = [];
        $scope.addStatisticCategory = addStatisticCategory;
        $scope.GetSeoTitle = GetSeoTitle;


        function addStatisticCategory() {
            apiService.post('/api/statisticCategory/create', $scope.statisticCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('statistic_categories');

                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }

        function GetSeoTitle() {
            $scope.statisticCategory.Alias= commonService.getSeoTitle($scope.statisticCategory.Name);
        }

        function loadParentCategories() {
            apiService.get('/api/statisticCategory/getall', null,
                function (result) {
                    $scope.parentCategories = result.data;
                }, function (error) {
                    notificationSerivce.displayError('Can not get parent category.')
                });
        }
        loadParentCategories();
    }
})(angular.module('tk.statistic_categories'));