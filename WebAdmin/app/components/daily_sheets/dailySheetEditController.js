(function (app) {
    app.controller('dailySheetEditController', dailySheetEditController);
    dailySheetEditController.$inject = ['$rootScope', '$scope', '$injector', 'apiService',
        'notificationService', 'authenticationService', 'commonService', '$filter', '$ngBootbox', 'ngDialog', '$http'];

    function dailySheetEditController($rootScope, $scope, $injector, apiService, notificationService,
        authenticationService, commonService, $filter, $ngBootbox, ngDialog, $http) {

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }

        $scope.dailySheet = {            
        };
        $scope.files = [];

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

        //add file
        $scope.selectedFile = function (files) {

            $scope.files.push(files[0]);

        }

        $scope.UpdateSheet = function () {           
            
            $scope.dailySheet.DayReport = moment($scope.dailySheet.DayReport.toString()).format('YYYY-MM-DD');
            authenticationService.setHeader();
            $http({
                method: 'PUT',
                url: "/api/dailySheet/update",
                //IMPORTANT!!! You might think this should be set to 'multipart/form-data' 
                // but this is not true because when we are sending up files the request 
                // needs to include a 'boundary' parameter which identifies the boundary 
                // name between parts in this multi-part request and setting the Content-type 
                // manually will not set this boundary parameter. For whatever reason, 
                // setting the Content-type to 'false' will force the request to automatically
                // populate the headers properly including the boundary parameter.
                headers: { 'Content-Type': undefined },
                //This method will allow us to change how the data is sent up to the server
                // for which we'll need to encapsulate the model data in 'FormData'
                transformRequest: function (data) {
                    var formData = new FormData();
                    //need to convert our json object to a string version of json otherwise
                    // the browser will do a 'toString()' on the object which will result 
                    // in the value '[Object object]' on the server.
                    formData.append("dailySheet", angular.toJson(data.dailySheet));
                    formData.append("file", data.files[0]);
                   
                    return formData;
                },
                //Create an object that contains the model and files which will be transformed
                // in the above transformRequest method
                data: { dailySheet: $scope.dailySheet, files: $scope.files }
            }).then(function (result, status, headers, config) {
                notificationService.displaySuccess('Cập nhật thành công.');
                $scope.closeThisDialog();
                var stateService = $injector.get('$state');
                stateService.reload();
            },
            function (data, status, headers, config) {
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