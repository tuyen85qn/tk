(function () {
    angular.module('tk.situation_categories', ['tk.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('situation_categories', {
            url: "/situation_categories",
            parent: 'base',
            templateUrl: "/app/components/situation_categories/situationCategoryListView.html",
            controller: "situationCategoryListController"
        }).state('add_situation_category', {
            url: "/add_situation_category",
            parent: 'base',
            templateUrl: "/app/components/situation_categories/situationCategoryAddView.html",
            controller: "situationCategoryAddController"
        }).state('edit_situation_category', {
            url: "/edit_situation_category/:id",
            parent: 'base',
            templateUrl: "/app/components/situation_categories/situationCategoryEditView.html",
            controller: "situationCategoryEditController"
        });
    }
})();