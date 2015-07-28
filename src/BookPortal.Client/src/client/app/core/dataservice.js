(function () {
    'use strict';

    angular
        .module('app.core')
        .factory('dataservice', dataservice);

    dataservice.$inject = ['$http', '$q', 'exception', 'logger'];
    /* @ngInject */
    function dataservice($http, $q, exception, logger) {
        var mainServiceUrl = 'http://aspnet5-bookportal-web.azurewebsites.net';

        var service = {
            getPeople: getPeople,
            getMessageCount: getMessageCount,
            getAwards: getAwards,
            getAward: getAward,
            getSerie: getSerie,
            getSerieTree: getSerieTree,
            getSerieEditions: getSerieEditions
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
            var url = mainServiceUrl + '/api/awards?isopened=true';

            if (sortType !== undefined) {
                url = url + '&sort=' + sortType;
            }

            return $http.get(url)
                .then(function (response) {
                    return _.map(response.data.result.rows, function (item) { return item });
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getAwards')(e);
                });
        }

        function getAward(awardId) {
            var url = mainServiceUrl + '/api/awards/' + awardId;

            return $http.get(url)
                .then(function (response) {
                    return response.data.result;
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getAward')(e);
                });
        }

        function getSerie(serieId) {
            var url = mainServiceUrl + '/api/series/' + serieId;

            return $http.get(url)
                .then(function (response) {
                    return response.data.result;
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getSerie')(e);
                });
        }

        function getSerieTree(serieId) {
            var url = mainServiceUrl + '/api/series/' + serieId + '/tree';

            return $http.get(url)
                .then(function (response) {
                    return response.data.result;
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getSerieTree')(e);
                });
        }

        function getSerieEditions(serieId, sortType) {
            var url = mainServiceUrl + '/api/series/' + serieId + '/editions';

            url = url + "?sort=" + sortType;

            return $http.get(url)
                .then(function (response) {
                    return _.map(response.data.result.rows, function (item) { return item });
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getSerieEditions')(e);
                });
        }
    }
})();
