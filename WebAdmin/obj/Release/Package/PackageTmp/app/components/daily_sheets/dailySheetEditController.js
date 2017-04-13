(function (app) {
    app.controller('dailySheetEditController', dailySheetEditController);
    dailySheetEditController.$inject = ['$rootScope', '$scope', '$injector', 'apiService', 'notificationService', 'commonService', '$filter', '$ngBootbox', 'ngDialog'];

    function dailySheetEditController($rootScope,$scope,$injector, apiService, notificationService, commonService, $filter, $ngBootbox, ngDialog) {

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }

        $scope.dailySheet = {            
        };

        function loadDetailDailySheet() {
            apiService.get('/api/dailySheet/getbyid/' + $rootScope.editId, null,
                function (result) {
                    $scope.dailySheet = result.data;
                }, function (error) {
                    notificationService.displayError('Không load được báo cáo ngày');
                });
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


        $scope.UpdateSheet = function () {
            $scope.dailySheet.DayReport = $scope.dailySheet.DayReport.toISOString().slice(0, 10);           
            apiService.put('/api/dailySheet/update', $scope.dailySheet,
                function (result) {
                    notificationService.displaySuccess('Cập nhật thành công.');
                    $scope.closeThisDialog();
                    var stateService = $injector.get('$state');
                    stateService.reload();

                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                    $scope.closeThisDialog();
                    var stateService = $injector.get('$state');
                    stateService.reload();
                });           

        }

        $scope.CancelSheet = function () {
            $scope.dailySheet = null;
            $scope.closeThisDialog();
        }

        loadDetailDailySheet();
        loadPoliceOrganizations();
        loadTypeReport();
    }

})(angular.module('tk.daily_sheets'));