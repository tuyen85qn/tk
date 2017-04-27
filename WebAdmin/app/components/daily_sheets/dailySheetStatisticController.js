(function (app) {
    app.controller('dailySheetStatisticController', dailySheetStatisticController);
    dailySheetStatisticController.$inject = ['$scope','apiService', 'notificationService', 'commonService', '$filter', '$ngBootbox', 'ngDialog'];

    function dailySheetStatisticController($scope, apiService, notificationService, commonService, $filter, $ngBootbox, ngDialog) {
            
        $scope.dailySheetST = {};
        $scope.dailySheetStatistics = [];
        $scope.policeOrganizations = [];
        $scope.hideResult = true;
        
        function loadPoliceOrganizations() {
            apiService.get('/api/policeOrganization/getall', null,
                function (result) {
                    $scope.policeOrganizations = result.data;
                },
                function (error) {
                    notificationService.displayError("Không thể load danh sách các cơ quan giải quyết");
                });
        }


        $scope.dailySheetStatistic = function () {
            var fromDate = moment($scope.dailySheetST.FromDate.toString()).format('YYYY-MM-DD');
            var toDate = moment($scope.dailySheetST.ToDate.toString()).format('YYYY-MM-DD');;
            var config = {
                params:{
                    fromDate: fromDate,
                    toDate: toDate,
                    policeOrganizationID: $scope.policeOrganizationID
                }
                
            };
            
            apiService.get('/api/dailySheet/statistic',config,
                function (result) {
                    $scope.hideResult = false;
                    $scope.dailySheetStatistics= result.data;
                }, function (error) {
                    notificationService.displayError('Đã có lỗi xảy ra');                   
                });

        }
      
        loadPoliceOrganizations();
       
    }

})(angular.module('tk.daily_sheets'));