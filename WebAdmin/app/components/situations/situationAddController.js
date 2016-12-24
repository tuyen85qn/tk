(function (app) {
    app.controller('situationAddController', situationAddController);
    situationAddController.$inject = ['$scope', 'apiService', 'notificationService', 'commonService', '$state'];

    function situationAddController($scope, apiService, notificationService, commonService, $state) {
        $scope.situation = {          
            Status: true
        };
        $scope.situation = [];
        $scope.provinces = [];
        $scope.districts = [];
        $scope.wards = [];       

        $scope.addSituation = addSituation;
        $scope.GetSeoTitle = GetSeoTitle;
        
        function getDistrictByProvinceId(id) {
            var config = {
                params:{
                    id:id
                }
            }
            apiService.get('/api/province/getbyid',config,
                function (result) {
                    $scope.districts = result.data;
                }, function (error) {
                    notificationService.displayError('Không load được danh sách các quận, huyện, thị xã.');
                });
        }
        function getWardByDistrictId(id) {
            var config = {
                params: {
                    id: id
                }
            }
            apiService.get('/api/district/getbyid', config,
                function (result) {
                    $scope.wards = result.data;
                }, function (error) {
                    notificationService.displayError('Không load được danh sách các xã, phường, thị trấn.');
                });
        }

        function addSituation() {
            apiService.post('/api/situation/create', $scope.situation,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('situations');

                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }

        function GetSeoTitle() {
            $scope.situation.Alias= commonService.getSeoTitle($scope.situation.Name);
        }

        function loadProvinces() {
            apiService.get('/api/province/getall',null,
                function (result) {
                    $scope.provinces = result.data;
                }, function (error) {
                    notificationService.displayError('Không load được danh sách các tỉnh, thành phố.');
                });
        }

        function loadSituationCategories() {
            apiService.get('/api/situation_categories/getallparents', null,
                function (result) {
                    $scope.parentCategories = result.data;
                }, function (error) {
                    notificationSerivce.displayError('Không thể lấy các danh mục.')
                });
        }

        loadSituationCategories();
        loadProvinces();
    }
})(angular.module('tk.situations'));