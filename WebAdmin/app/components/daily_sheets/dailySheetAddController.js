(function (app) {
    app.controller('dailySheetAddController', dailySheetAddController);
    dailySheetAddController.$inject = ['$rootScope','$scope', 'apiService', 'notificationService', 'commonService', '$state', '$filter', '$ngBootbox','ngDialog'];

    function dailySheetAddController($rootScope, $scope, apiService, notificationService, commonService, $state, $filter, $ngBootbox, ngDialog) {
        $rootScope.dailySheets = [];

        $scope.page = 0;
        $scope.pageCount = 0;

        $scope.addSheet = function () {
            $scope.sheet = {
                Status: false
            }
            var opts = {               
                backdrop: true,
                backdropClick: true,
                dialogFade: false,
                keyboard: false,
                controller: ['$scope',function($scope){
                    function loadTypeReport() {
                        apiService.get('/api/typeReport/getall', null,
                            function (result) {
                                $scope.provinces = result.data;
                            }, function (error) {
                                notificationService.displayError('Không load được danh sách các tỉnh, thành phố.');
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
                    loadPoliceOrganizations();
                    loadTypeReport()
                }],
                closeByEscape: false,
                $scope: $scope,
                templateUrl: 'app/components/daily_sheets/sheetAddView.html',
                width: '50%',
                resolve: {

                }
            }
            ngDialog.open(opts);
            $rootScope.dailySheets.push($scope.sheet);
        }
        $scope.addDailySheet = function () {

        }
        $scope.search = function search(page) {
            $scope.hideResult = false;
            page = page || 0;
            var config = {
                params: {
                    fromDate: $scope.dailySheet.FromDate.toISOString().slice(0, 10),
                    toDate: $scope.dailySheet.ToDate.toISOString().slice(0, 10),
                    provinceID: $scope.dailySheet.ProvinceID,
                    page: page,
                    pageSize: 5,
                    districID: $scope.dailySheet.DistrictID,
                    resolvedSituationID: $scope.dailySheet.ResolvedSituationID,
                    filter: ""
                }
            };
            apiService.get('/api/situation/getlistpagingbydate', config,
                function (result) {
                    $scope.listSituations = result.data.Items;
                    $scope.dailySheet.TotalSituationCount = result.data.TotalCount;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                    CalculatingDamages();
                }, function (error) {
                    notificationService.displayError(error.data);
                });
        }

    }
})(angular.module('tk.daily_sheets'));