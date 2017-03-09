(function(app) {
    app.controller('homeController', homeController);
    homeController.$inject = ['$rootScope', '$scope', '$uibModal', 'ngDialog', 'apiService', 'notificationService'];
    function homeController($rootScope, $scope, $uibModal, ngDialog, apiService) {

        //$scope.detail = function () {
        //    $rootScope.modalInstance = $uibModal.open({
        //        animation: true,
        //        templateUrl: '/app/components/home/test.html',
        //        backdrop: 'static',
        //        windowClass: 'center-modal',               
        //        keyboard: false,
        //        size: 'lg'
        //    });
                
        //}
        

        $scope.detail = function () {
            $rootScope.situation = null;
            apiService.get('/api/situation/getbyid/' + 49, null,
                           function (result) {
                               $rootScope.situation = result.data;
                               $rootScope.situation.Content = $rootScope.situation.Content.replace(/(<p[^>]+?>|<p>|<\/p>)/img,"").replace(/<br\s*[\/]?>/gi, "\n");
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
                templateUrl: 'app/components/home/test.html',
                width:'50%',
                resolve: {
                   
                }
            }
            ngDialog.open(opts);
        }
    }
})(angular.module('tk'));