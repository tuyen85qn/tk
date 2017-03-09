(function () {
    angular.module('tk.statistic_categories', ['tk.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('statistic_categories', {
            url: "/statistic_categories",
            parent: 'base',
            templateUrl: "/app/components/statistic_categories/statisticCategoryListView.html",
            controller: "statisticCategoryListController"
        }).state('add_statistic_category', {
            url: "/add_statistic_category",
            parent: 'base',
            templateUrl: "/app/components/statistic_categories/statisticCategoryAddView.html",
            controller: "statisticCategoryAddController"
        }).state('edit_statistic_category', {
            url: "/edit_statistic_category/:id",
            parent: 'base',
            templateUrl: "/app/components/statistic_categories/statisticCategoryEditView.html",
            controller: "statisticCategoryEditController"
        });
    }
})();