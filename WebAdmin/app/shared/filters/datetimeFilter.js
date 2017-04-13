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
    app.filter('strDate', function ($filter) {
        return function (input) {
            if (input == null)
            {
                return "";
            }

            return input.slice(0, 10);
            
        };
    });
})(angular.module('tk.common'));