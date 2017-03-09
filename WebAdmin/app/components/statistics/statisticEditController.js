(function (app) {
    app.controller('statisticEditController', statisticEditController);
    statisticEditController.$inject = ['$scope', 'apiService', 'notificationService', 'commonService', '$state', '$stateParams'];

    function statisticEditController($scope, apiService, notificationService, commonService, $state, $stateParams) {
        $scope.statistic = null;
        $scope.page = 0;
        $scope.pageCount = 0;       
        $scope.districtid = true;
        $scope.listSituations = [];
       
        $scope.statisticCategories = [];
        $scope.provinces = [];
        $scope.districts = [];
        $scope.resolvedSituations = [];

        $scope.updateStatistic = updateStatistic;
        $scope.GetSeoTitle = GetSeoTitle;

        function loadDetailStatistic() {
            $scope.hideResult = true;
            apiService.get('/api/statistic/getbyid/' + $stateParams.id, null,
                function (result) {
                    $scope.statistic = result.data;
                },
                function (error) {
                    notificationService('Không thể load chi tiết thống kê');
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

        function updateStatistic() {
            $scope.statistic.FromDate = $scope.statistic.FromDate.toISOString().slice(0, 10);
            $scope.statistic.ToDate = $scope.statistic.ToDate.toISOString().slice(0, 10);
            apiService.put('/api/statistic/update', $scope.statistic,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' được cập nhật thành công.');
                    $state.go('statistics');

                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function GetSeoTitle() {
            $scope.statistic.Alias = commonService.getSeoTitle($scope.statistic.Name);
        }

        function loadProvinces() {
            apiService.get('/api/province/getall', null,
                function (result) {
                    $scope.provinces = result.data;
                }, function (error) {
                    notificationService.displayError('Không load được danh sách các tỉnh, thành phố.');
                });
        }

        function loadStatisticCategories() {
            apiService.get('/api/statisticCategory/getall', null,
                function (result) {
                    $scope.statisticCategories = result.data;
                },
                function (error) {
                    notificationService.displayError("Không thể load danh sách các mục lục tình hình");
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

        $scope.search = function search(page) {
            $scope.hideResult = false;
            page = page || 0;
            var config = {
                params: {
                    fromDate: $scope.statistic.FromDate.toISOString().slice(0, 10),
                    toDate: $scope.statistic.ToDate.toISOString().slice(0, 10),
                    provinceID: $scope.statistic.ProvinceID,
                    page: page,
                    pageSize: 5,
                    districID: $scope.statistic.DistrictID,
                    resolvedSituationID: $scope.statistic.ResolvedSituationID,
                    filter: ""
                }
            };
            apiService.get('/api/situation/getlistpagingbydate', config,
                function (result) {
                    $scope.listSituations = result.data.Items;
                    $scope.statistic.TotalSituationCount = result.data.TotalCount;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                    CalculatingDamages();
                }, function (error) {
                    notificationService.displayError(error.data);
                });
        }
        function CalculatingDamages() {
            $.each($scope.listSituations, function (key, value) {
                $scope.statistic.TheDead += value.TheDead;
                $scope.statistic.TheInjured += value.TheInjured;
                $scope.statistic.PropertyDamage += value.PropertyDamage;
            })
        }

        loadDetailStatistic();
        loadResolvedSituations();
        loadStatisticCategories();
        loadProvinces();
        
    }
})(angular.module('tk.statistics'));