(function (app) {
    app.controller('situationCategoryEditController', situationCategoryEditController);
    situationCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService', 'commonService','$stateParams', '$state'];

    function situationCategoryEditController($scope, apiService, notificationService, commonService, $stateParams, $state) {
        $scope.situationCategory = null;
        $scope.parentCategories = [];
        $scope.updateSituationCategory = updateSituationCategory;
        $scope.GetSeoTitle = GetSeoTitle;

        function loadSituationCategoryDetail() {
            apiService.get('/api/situationCategory/getbyid/' + $stateParams.id, null,
                function (result) {
                    $scope.situationCategory = result.data;
                },
                function (error) {
                    notificationService.displayError(error.Data);
                });
        }
        function updateSituationCategory() {
            apiService.put('/api/situationCategory/update', $scope.situationCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('situation_categories');

                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function GetSeoTitle() {
            $scope.situationCategory.Alias = commonService.getSeoTitle($scope.situationCategory.Name);
        }

        function loadParentCategories() {
            apiService.get('/api/situationCategory/getallparents', null,
                function (result) {
                    $scope.parentCategories = result.data;
                }, function (error) {
                    notificationSerivce.displayError('Can not get parent category.')
                });
        }

        loadSituationCategoryDetail();
        loadParentCategories();
    }
})(angular.module('tk.situation_categories'));