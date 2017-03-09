(function () {
    angular.module('tk.statistics', ['tk.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('statistics', {
            url: "/statistics",
            parent: 'base',
            templateUrl: "/app/components/statistics/statisticListView.html",
            controller: "statisticListController"
        }).state('add_statistic', {
            url: "/add_statistic",
            parent: 'base',
            templateUrl: "/app/components/statistics/statisticAddView.html",
            controller: "statisticAddController"
        }).state('edit_statistic', {
            url: "/edit_statistic/:id",
            parent: 'base',
            templateUrl: "/app/components/statistics/statisticEditView.html",
            controller: "statisticEditController"
        });
    }
})();