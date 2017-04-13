/// <reference path="Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.factory('apiService', apiService);
    apiService.$inject = ['$http', 'authenticationService'];
    function apiService($http, authenticationService) {
        return {
            get: get,
            post: post,
            put: put,
            del:del,
           
        }

        function get(url, params, successCallback, failure) {
            authenticationService.setHeader();
            $http.get(url, params).then(function (result) {
                successCallback(result);
            },function (error) {
                failure(error);
            });
        }

        function post(url, data, successCallback, failure) {
            authenticationService.setHeader();
            $http.post(url, data).then(function (result) {
                successCallback(result);
            },function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }

        function put(url, data, successCallback, failure) {
            authenticationService.setHeader();
            $http.put(url, data).then(function (result) {
                successCallback(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }

        function del(url, data, successCallback, failure) {
            authenticationService.setHeader();
            $http.delete(url, data).then(function (result) {
                successCallback(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.');
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }
    }

})(angular.module('tk.common'));