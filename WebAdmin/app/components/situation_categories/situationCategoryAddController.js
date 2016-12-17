(function (app) {
    app.controller('situationCategoryAddController', situationCategoryAddController);
    situationCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', 'commonService', '$state'];

    function situationCategoryAddController($scope, apiService, notificationService, commonService, $state) {
        $scope.situationCategory = {          
            Status: true
        };
        $scope.parentCategories = [];
        $scope.addSituationCategory = addSituationCategory;
        $scope.GetSeoTitle = GetSeoTitle;


        function addSituationCategory() {
            apiService.post('/api/situationcategory/create', $scope.situationCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('situation_categories');

                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }

        function GetSeoTitle() {
            $scope.situationCategory.Alias= commonService.getSeoTitle($scope.situationCategory.Name);
        }

        function loadParentCategories() {
            apiService.get('/api/situationcategory/getallparents',null,
                function (result) {
                    $scope.parentCategories = result.data;
                }, function (error) {
                    notificationSerivce.displayError('Can not get parent category.')
                });
        }
        loadParentCategories();
    }
})(angular.module('tk.situation_categories'));