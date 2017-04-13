(function(app) {
    app.controller('homeController', homeController);
    homeController.$inject = ['$scope', 'ngDialog', 'apiService', 'notificationService'];
    function homeController($scope, ngDialog, apiService) {

        $scope.listNationalSecurities = [];
        $scope.listSocialSecurities = [];
        $scope.listTrafficSafeties = [];
        $scope.listWorkResults =[]
        $scope.listOtherIncidents = [];
        function loadListNationalSecurities() {
            var config = {
                params: {
                    page: 0,
                    pageSize: 5,
                    filter: "An ninh quốc gia"
                }
            }

            apiService.get('/api/situation/getlistbycategory', config, function (result) {
                $scope.listNationalSecurities = result.data.Items;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function loadListTrafficSafeties() {
            var config = {
                params: {
                    page: 0,
                    pageSize: 5,
                    filter: "An toàn giao thông"
                }
            }

            apiService.get('/api/situation/getlistbycategory', config, function (result) {
                $scope.listTrafficSafeties = result.data.Items;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function loadListSocialSecurities() {
            var config = {
                params: {
                    page: 0,
                    pageSize: 5,
                    filter: "Xâm phạm TTXH"
                }
            }

            apiService.get('/api/situation/getlistbycategory', config, function (result) {
                $scope.listSocialSecurities = result.data.Items;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function loadListOtherIncidents() {
            var config = {
                params: {
                    page: 0,
                    pageSize: 5,
                    filter: "Vụ việc khác"
                }
            }

            apiService.get('/api/situation/getlistbycategory', config, function (result) {
                $scope.listOtherIncidents = result.data.Items;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function loadlistWorkResults() {
            var config = {
                params: {
                    page: 0,
                    pageSize: 5,
                    filter: "Kết quả công tác"
                }
            }

            apiService.get('/api/situation/getlistbycategory', config, function (result) {
                $scope.listWorkResults = result.data.Items;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
       
     
        loadListOtherIncidents();
       loadListSocialSecurities();
        loadListTrafficSafeties();
       
    }
})(angular.module('tk'));