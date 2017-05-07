(function (app) {
    app.controller('statisticAddController', statisticAddController);
    statisticAddController.$inject = ['$rootScope','$scope', 'apiService', 'notificationService', 'commonService', '$state', '$filter', '$ngBootbox','ngDialog'];

    function statisticAddController($rootScope,$scope, apiService, notificationService, commonService, $state, $filter, $ngBootbox, ngDialog) {
        $scope.statistic = {          
            Status: true,
            TotalCount: 0,
            TheDead: 0,
            TheInjured: 0,
            PropertyDamage:0
        };
       
        $scope.page = 0;
        $scope.pageCount = 0;

        $scope.hideResult = true;
        $scope.districtid = true;
        $scope.listSituations = [];

        $scope.statisticCategories = [];
        $scope.provinces = [];
        $scope.districts = [];      
        $scope.resolvedSituations = [];        

        $scope.addStatistic = addStatistic;
        $scope.GetSeoTitle = GetSeoTitle;
        
        $scope.getDistrictByProvinceId = function getDistrictByProvinceId(id) {
            $scope.districtid = false;
            apiService.get('/api/district/getbyprovinceid/' + id ,null,
                function (result) {
                    $scope.districts = result.data;
                }, function (error) {
                    notificationService.displayError('Không thể load được danh sách các quận, huyện, thị xã.');
                });
        }

        function addStatistic() {
            $scope.statistic.FromDate = moment($scope.statistic.FromDate.toString()).format('YYYY-MM-DD');
            $scope.statistic.ToDate = moment($scope.statistic.ToDate.toString()).format('YYYY-MM-DD');
            apiService.post('/api/statistic/create', $scope.statistic,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('statistics');

                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }

        $scope.showDetailSituation = function(id) {
            $rootScope.situation = null;
            apiService.get('/api/situation/getbyid/' + id, null,
                           function (result) {
                               $rootScope.situation = result.data;
                               $rootScope.situation.Content = $rootScope.situation.Content.replace(/(<p[^>]+?>|<p>|<\/p>)/img, "").replace(/<br\s*[\/]?>/gi, "\n");
                           },
                           function (error) {
                               notificationService.displayError('Cant load detail Situation.');
                           });
            var opts = {
                backdrop: true,
                backdropClick: true,
                dialogFade: false,
                keyboard: false,
                $scope: $rootScope,
                templateUrl: 'app/components/statistics/detailSituationView.html',
                width: '50%',
                resolve: {
                }
            }
            ngDialog.open(opts);
        }
        

        function GetSeoTitle() {
            $scope.statistic.Alias= commonService.getSeoTitle($scope.statistic.Name);
        }

        function loadProvinces() {
            apiService.get('/api/province/getall',null,
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


        loadResolvedSituations();
        loadStatisticCategories();
        loadProvinces();
    }
})(angular.module('tk.statistics'));