(function (app) {
    app.filter('reportStatusFilter', function () {
        return function (input) {
            if (input == true)
                return 'Có báo cáo';
            else
                return 'Không báo cáo';
        }
    });
})(angular.module('tk.common'));