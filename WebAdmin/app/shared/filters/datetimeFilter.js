(function (app) {
    app.filter('datetime', function ($filter) {
        return function (input) {
            if (input == null) { return ""; }

            var _date = $filter('date')(new Date(input),
                                        'dd-MM-yyyy HH:mm:ss');

            return _date.toUpperCase();

        };
    });

    app.filter('date', function ($filter) {
        return function (input) {
            if (input == null) { return ""; }

            var _date = $filter('date')(new Date(input),
                                        'dd-MM-yyyy');

            return _date.toUpperCase();

        };
    });
    app.filter('brDateFilter', function () {
        return function (dateSTR) {
            var o = dateSTR.replace(/-/g, "/"); // Replaces hyphens with slashes
            return Date.parse(o + " -0000"); // No TZ subtraction on this sample
        }
    });
})(angular.module('tk.common'));