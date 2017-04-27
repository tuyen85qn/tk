(function (app) {
    app.controller('situationAddController', situationAddController);
    situationAddController.$inject = ['$scope', 'apiService', 'notificationService', 'commonService', '$state'];

    function situationAddController($scope, apiService, notificationService, commonService, $state) {
        $scope.situation = {          
            Status: true
        };

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }

        $scope.disabledDistrict = true;
        $scope.disabledWard = true;
        $scope.disabledHamlet = true;

        $scope.situationCategories = [];
        $scope.provinces = [];
        $scope.districts = [];
        $scope.wards = [];
        $scope.resolvedSituations = [];
        $scope.policeOrganizations = [];

        $scope.addSituation = addSituation;
        $scope.GetSeoTitle = GetSeoTitle;
        
        $scope.getDistrictByProvinceId = function(id) {
            $scope.disabledDistrict = false;
            apiService.get('/api/district/getbyprovinceid/' + id ,null,
                function (result) {
                    $scope.districts = result.data;
                }, function (error) {
                    notificationService.displayError('Không thể load được danh sách các quận, huyện, thị xã.');
                });
        }

        $scope.getWardByDistrictId = function(id) {
            $scope.disabledWard = false;
            apiService.get('/api/ward/getbydistrictid/'+ id, null,
                function (result) {
                    $scope.wards = result.data;
                }, function (error) {
                    notificationService.displayError('Không load được danh sách các xã, phường, thị trấn.');
                });
        }

        $scope.getHamletByWardId = function() {
            $scope.disabledHamlet = false;
        }

        function addSituation() {
            $scope.situation.OccurenceDay = moment($scope.situation.OccurenceDay.toString()).format('YYYY-MM-DD');;
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
            apiService.get('/api/situationCategory/getall', null,
                function (result) {
                    var tempData = commonService.getTree(result.data, "ID", "ParentID");
                    tempData.forEach(function (item) {
                        commonService.recur(item, 0, $scope.situationCategories);
                    });                   
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

        loadPoliceOrganizations();
        loadResolvedSituations();
        loadSituationCategories();
        loadProvinces();
    }
})(angular.module('tk.situations'));