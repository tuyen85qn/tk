(function (app) {
    app.filter('datetime', function ($filter) {
        return function (input) {
            if (input == null) { return ""; }

            var _date = $filter('date')(new Date(input),
                                        'yyyy-MM-dd HH:mm:ss');

            return _date.toUpperCase();

        };
    });

    app.filter('date', function ($filter) {
        return function (input) {
            if (input == null) { return ""; }

            var _date = $filter('date')(new Date(input),
                                        'yyyy-MM-dd');

            return _date.toUpperCase();

        };
    });
})(angular.module('tk.common'));