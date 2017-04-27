(function (app) {
    'use strict';

    app.controller('applicationUserAddController', applicationUserAddController);

    applicationUserAddController.$inject = ['$scope', 'apiService', 'notificationService', '$location', 'commonService'];

    function applicationUserAddController($scope, apiService, notificationService, $location, commonService) {
        $scope.account = {          
            Groups: []           
        }

        $scope.addAccount = addAccount;

        function addAccount() {
            $scope.account.BirthDay = $scope.account.BirthDay.toISOString().slice(0, 10);
            apiService.post('/api/applicationUser/add', $scope.account, addSuccessed, addFailed);
        }

        function addSuccessed(result) {
            notificationService.displaySuccess(result.data.UserName + ' đã được thêm mới.');

            $location.url('application_users');
        }
        function addFailed(response) {
            notificationService.displayError(response.data.Message);
           
        }

        function loadGroups() {
            apiService.get('/api/applicationGroup/getlistall',
                null,
                function (response) {
                    $scope.groups = response.data;
                }, function (response) {
                    notificationService.displayError('Không tải được danh sách nhóm.');
                });

        }

        loadGroups();

    }
})(angular.module('tk.application_users'));