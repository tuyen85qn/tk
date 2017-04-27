﻿(function (app) {
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
            apiService.post('/api/situationCategory/create', $scope.situationCategory,
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
            apiService.get('/api/situationCategory/getall', null,
                function (result) {
                    var tempData = commonService.getTree(result.data, "ID", "ParentID");
                    tempData.forEach(function (item) {
                        commonService.recur(item, 0, $scope.parentCategories);
                    });
                  
                }, function (error) {
                    notificationSerivce.displayError('Không thể load danh mục.')
                });
        }
        loadParentCategories();
    }
})(angular.module('tk.situation_categories'));