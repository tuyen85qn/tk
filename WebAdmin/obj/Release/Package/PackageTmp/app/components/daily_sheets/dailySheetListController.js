(function (app) {
    app.controller('dailySheetListController', dailySheetListController);
    dailySheetListController.$inject = ['$rootScope','$scope', 'notificationService', 'apiService', '$filter', '$ngBootbox', 'ngDialog']
    function dailySheetListController($rootScope,$scope, notificationService, apiService, $filter, $ngBootbox, ngDialog) {
        $scope.loading = true;       
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.search = search;        
        $scope.dailySheets = [];

        $scope.delDailySheet = function(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?')
                .then(function () {
                    var config = {
                        params: {
                            id: id
                        }
                    }
                    apiService.del('/api/dailySheet/delete', config, function () {
                        notificationService.displaySuccess('Đã xóa thành công.');
                        search();
                    },
                    function () {
                        notificationService.displayError('Xóa không thành công.');
                    });
                });
        }

        $scope.addDailySheet = function () {
            var opts = {
                backdrop: true,
                backdropClick: true,
                dialogFade: false,
                keyboard: false,
                controller: 'dailySheetAddController',
                closeByEscape: false,
                closeByDocument: false,
                closeByNavigation: true,
                $scope: $scope,
                templateUrl: 'app/components/daily_sheets/dailySheetAddView.html',
                width: '60%',
                resolve: {
                    
                   
                }
            }
           ngDialog.open(opts);
         
        }

        $scope.updateDailySheet = function (id) {
            $rootScope.editId = id;
            var opts = {
                backdrop: true,
                backdropClick: true,
                dialogFade: false,
                keyboard: false,
                controller:'dailySheetEditController',
                closeByEscape: false,
                closeByDocument: false,
                closeByNavigation: true,
                $scope: $scope,               
                templateUrl: 'app/components/daily_sheets/dailySheetEditView.html',
                width: '60%',
                resolve: {
                   
                }
            }
           ngDialog.open(opts);           
        }

        function search(page) {
            page = page || 0;
            var filter = "";
            if ($scope.filterDate != null)
            {
                $scope.filterDate = $scope.filterDate.toISOString().slice(0, 10);
                filter = $scope.filterDate;
            }
            $scope.loading = true;            
            var config = {
                params: {
                    page: page,
                    pageSize:10,
                    filterDate: filter
                }
            }

            apiService.get('/api/dailySheet/getlistpaging', config, function (result) {
                $scope.dailySheets = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;                               
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }    

        search();
    }
})(angular.module('tk.daily_sheets'));