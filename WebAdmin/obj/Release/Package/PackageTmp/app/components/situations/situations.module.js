(function () {
    angular.module('tk.situations', ['tk.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('situations', {
            url: "/situations",
            parent: 'base',
            templateUrl: "/app/components/situations/situationListView.html",
            controller: "situationListController"
        }).state('add_situation', {
            url: "/add_situation",
            parent: 'base',
            templateUrl: "/app/components/situations/situationAddView.html",
            controller: "situationAddController"
        }).state('edit_situation', {
            url: "/edit_situation/:id",
            parent: 'base',
            templateUrl: "/app/components/situations/situationEditView.html",
            controller: "situationEditController"
        });
    }
})();