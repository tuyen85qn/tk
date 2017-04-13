(function (app) {
    app.controller('situationEditController', situationEditController);
    situationEditController.$inject = ['$scope', 'apiService', 'notificationService', 'commonService', '$state', '$stateParams'];

    function situationEditController($scope, apiService, notificationService, commonService, $state, $stateParams) {
        $scope.situation = null;     

        $scope.situationCategories = [];
        $scope.provinces = [];
        $scope.districts = [];
        $scope.wards = [];
        $scope.resolvedSituations = [];
        $scope.policeOrganizations = [];

        $scope.updateSituation = updateSituation;
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.loadDetailSituation = loadDetailSituation;

        function loadDetailSituation()
        {
            apiService.get('/api/situation/getbyid/'+ $stateParams.id,null,
                function (result) {
                    $scope.situation = result.data;
                },
                function (error) {
                    notificationService('Không thể load chi tiết tình hình');
                });
        }
        $scope.getDistrictByProvinceId = function getDistrictByProvinceId(id) {
            $scope.districtid = false;
            apiService.get('/api/district/getbyprovinceid/' + id, null,
                function (result) {
                    $scope.districts = result.data;
                }, function (error) {
                    notificationService.displayError('Không thể load được danh sách các quận, huyện, thị xã.');
                });
        }

        $scope.getWardByDistrictId = function getWardByDistrictId(id) {
            $scope.wardid = false;
            apiService.get('/api/ward/getbydistrictid/' + id, null,
                function (result) {
                    $scope.wards = result.data;
                }, function (error) {
                    notificationService.displayError('Không load được danh sách các xã, phường, thị trấn.');
                });
        }


        function updateSituation() {
            apiService.put('/api/situation/update', $scope.situation,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('situations');

                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function GetSeoTitle() {
            $scope.situation.Alias = commonService.getSeoTitle($scope.situation.Name);
        }

        function loadProvinces() {
            apiService.get('/api/province/getall', null,
                function (result) {
                    $scope.provinces = result.data;
                }, function (error) {
                    notificationService.displayError('Không load được danh sách các tỉnh, thành phố.');
                });
        }

        function loadSituationCategories() {
            apiService.get('/api/situationCategory/getall', null,
                function (result) {
                    $scope.situationCategories = result.data;
                },
                function (error) {
                    notificationService.displayError("Không thể load danh sách các mục lục tình hình");
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

        function loadResolvedSituations() {
            apiService.get('/api/resolvedSituation/getall', null,
                function (result) {
                    $scope.resolvedSituations = result.data;
                },
                function (error) {
                    notificationService.displayError("Không thể load tình trạng giải quyết");
                });
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        }

        $scope.moreImages = [];

        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })

            }
            finder.popup();
        }

        loadDetailSituation();
        loadPoliceOrganizations();
        loadResolvedSituations();
        loadSituationCategories();
        loadProvinces();
    }
})(angular.module('tk.situations'));