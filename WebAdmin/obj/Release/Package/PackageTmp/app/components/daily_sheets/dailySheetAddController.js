(function (app) {
    app.controller('dailySheetAddController', dailySheetAddController);
    dailySheetAddController.$inject = ['$scope','$injector', 'apiService', 'notificationService', 'commonService', '$filter', '$ngBootbox', 'ngDialog'];

    function dailySheetAddController($scope,$injector, apiService, notificationService, commonService, $filter, $ngBootbox, ngDialog) {

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }

        $scope.dailySheet = {
            Status:false
        }

        function loadTypeReport() {
            apiService.get('/api/typeReport/getall', null,
                function (result) {
                    $scope.typeReports = result.data;
                }, function (error) {
                    notificationService.displayError('Không load danh sach các kiểu báo cáo.');
                });
        }
        function loadPoliceOrganizations() {
            apiService.get('/api/policeOrganization/getall', null,
                function (result) {
                    $scope.policeOrganizations = result.data;
                },
                function (error) {
                    notificationService.displayError("Không thể load danh sách các cơ quan giải quyết");
                });
        }


        $scope.SaveSheet = function () {
            $scope.dailySheet.DayReport = $scope.dailySheet.DayReport.toISOString().slice(0, 10);           
            apiService.post('/api/dailySheet/create', $scope.dailySheet,
                function (result) {
                    notificationService.displaySuccess('Thêm mới thành công.');
                    $scope.closeThisDialog();
                    var stateService = $injector.get('$state');
                    stateService.reload();

                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                    $scope.closeThisDialog();
                    var stateService = $injector.get('$state');
                    stateService.reload();
                });          
                      
           
        }

        $scope.CancelSheet = function () {
            $scope.dailySheet = null;
            $scope.closeThisDialog();
        }

        loadPoliceOrganizations();
        loadTypeReport();
    }

})(angular.module('tk.daily_sheets'));