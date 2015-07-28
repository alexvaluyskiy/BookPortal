(function () {
    'use strict';

    angular
        .module('app.core')
        .factory('dataservice', dataservice);

    dataservice.$inject = ['$http', '$q', 'exception', 'logger'];
    /* @ngInject */
    function dataservice($http, $q, exception, logger) {
        var service = {
            getPeople: getPeople,
            getMessageCount: getMessageCount,
            getAwards: getAwards
        };

        return service;

        function getMessageCount() { return $q.when(72); }

        function getPeople() {
            return $http.get('/api/people')
                .then(success)
                .catch(fail);

            function success(response) {
                return response.data;
            }

            function fail(e) {
                return exception.catcher('XHR Failed for getPeople')(e);
            }
        }

        function getAwards(sortType) {
            var url = 'http://aspnet5-bookportal-web.azurewebsites.net' + '/api/awards?isopened=true';

            if (sortType !== undefined) {
                url = url + '&sort=' + sortType;
            }

            return $http.get(url)
                .then(success)
                .catch(fail);

            function success(response) {
                return _.map(response.data.result.rows, function (item) { return item });
            }

            function fail(e) {
                return exception.catcher('XHR Failed for getAwards')(e);
            }
        }
    }
})();
