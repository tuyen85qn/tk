(function () {
    angular.module('tk.daily_sheets', ['tk.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('daily_sheets', {
            url: "/daily_sheets",
            parent: 'base',
            templateUrl: "/app/components/daily_sheets/dailySheetListView.html",
            controller: "dailySheetListController"
        })
        .state('daily_sheet_statistic', {
            url: "/daily_sheet_statistic",
            parent: 'base',
            templateUrl:"/app/components/daily_sheets/dailySheetStatisticView.html",
            controller:"dailySheetStatisticController"
        });
    }
})();