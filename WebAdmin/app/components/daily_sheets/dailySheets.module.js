(function () {
    angular.module('tk.daily_sheets', ['tk.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('daily_sheets', {
            url: "/daily_sheets",
            parent: 'base',
            templateUrl: "/app/components/daily_sheets/dailySheetListView.html",
            controller: "dailySheetListController"
        }).state('add_daily_sheet', {
            url: "/add_daily_sheet",
            parent: 'base',
            templateUrl: "/app/components/daily_sheets/dailySheetAddView.html",
            controller: "dailySheetAddController"
        }).state('edit_daily_sheet', {
            url: "/edit_daily_sheet/:id",
            parent: 'base',
            templateUrl: "/app/components/daily_sheets/dailySheetEditView.html",
            controller: "dailySheetEditController"
        });
    }
})();