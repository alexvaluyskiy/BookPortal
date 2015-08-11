(function () {
    'use strict';

    angular
        .module('app.core')
        .factory('dataservice', dataservice);

    dataservice.$inject = ['$http', '$q', 'exception', 'logger', 'localStorageService'];
    /* @ngInject */
    function dataservice($http, $q, exception, logger, localStorageService) {
        var mainServiceUrl = 'http://aspnet5-bookportal-web.azurewebsites.net';

        var service = {
            getPeople: getPeople,
            getMessageCount: getMessageCount,

            getCountries: getCountries,
            getLanguages: getLanguages,
            getWorkTypes: getWorkTypes,

            getAwards: getAwards,

            getAward: getAward,

            getSerie: getSerie,
            getSerieTree: getSerieTree,
            getSerieEditions: getSerieEditions,

            getWork: getWork,
            getWorkTranslations: getWorkTranslations,
            getWorkGenres: getWorkGenres,
            getWorkAwards: getWorkAwards,
            getWorkEditions: getWorkEditions,
            getWorkReviews: getWorkReviews,

            getPerson: getPerson,
            getPersonWorks: getPersonWorks
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

        function getCountries() {
            var countries = localStorageService.get("countries");

            if (countries === null) {
                var url = mainServiceUrl + '/api/countries';

                return $http.get(url)
                    .then(function(response) {
                        countries = _.reduce(response.data.result.rows, function(m, x) { m[x.countryid] = x.name; return m; }, {});
                        localStorageService.set("countries", countries);
                        return countries;
                    })
                    .catch(function(e) {
                        return exception.catcher('XHR Failed for getCountries')(e);
                    });
            }

            return $q.when(countries);
        }

        function getLanguages() {
            var languages = localStorageService.get("languages");

            if (languages === null) {
                var url = mainServiceUrl + '/api/languages';

                return $http.get(url)
                    .then(function (response) {
                        languages = _.reduce(response.data.result.rows, function (m, x) { m[x.languageid] = x.name; return m; }, {});
                        localStorageService.set("languages", languages);
                        return languages;
                    })
                    .catch(function (e) {
                        return exception.catcher('XHR Failed for getLanguages')(e);
                    });
            }

            return $q.when(languages);
        }

        function getWorkTypes() {
            var worktypes = localStorageService.get("worktypes");

            if (worktypes === null) {
                var url = mainServiceUrl + '/api/worktypes';

                return $http.get(url)
                    .then(function (response) {
                        worktypes = _.reduce(response.data.result.rows, function (m, x) { m[x.worktypeid] = x; return m; }, {});
                        localStorageService.set("worktypes", worktypes);
                        return worktypes;
                    })
                    .catch(function (e) {
                        return exception.catcher('XHR Failed for getWorkTypes')(e);
                    });
            }

            return $q.when(worktypes);
        }

        // GET AWARD_LIST
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

        // GET AWARD_VIEW
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

        // GET SERIE_VIEW
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

        // GET WORK_VIEW
        function getWork(workId) {
            var url = mainServiceUrl + '/api/works/' + workId;

            return $http.get(url)
                .then(function (response) {
                    return response.data.result;
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getWork')(e);
                });
        }

        function getWorkTranslations(workId) {
            var url = mainServiceUrl + '/api/works/' + workId + '/translations';

            return $http.get(url)
                .then(function (response) {
                    return _.map(response.data.result.rows, function (item) { return item });
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getWorkTranslations')(e);
                });
        }

        function getWorkGenres(workId) {
            var url = mainServiceUrl + '/api/works/' + workId + '/genres';

            return $http.get(url)
                .then(function (response) {
                    return _.map(response.data.result.rows, function (item) { return item });
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getWorkGenres')(e);
                });
        }

        function getWorkAwards(workId) {
            var url = mainServiceUrl + '/api/works/' + workId + '/awards';

            return $http.get(url)
                .then(function (response) {
                    return _.map(response.data.result.rows, function (item) { return item });
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getWorkAwards')(e);
                });
        }

        function getWorkEditions(workId) {
            var url = mainServiceUrl + '/api/works/' + workId + '/editions';

            return $http.get(url)
                .then(function (response) {
                    return _.map(response.data.result.rows, function (item) { return item });
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getWorkEditions')(e);
                });
        }

        function getWorkReviews(workId) {
            var url = mainServiceUrl + '/api/works/' + workId + '/reviews';

            return $http.get(url)
                .then(function (response) {
                    return _.map(response.data.result.rows, function (item) { return item });
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getWorkReviews')(e);
                });
        }

        // GET PERSON_VIEW
        function getPerson(personId) {
            var url = mainServiceUrl + '/api/persons/' + personId;

            return $http.get(url)
                .then(function (response) {
                    return response.data.result;
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getPerson')(e);
                });
        }

        function getPersonWorks(personId) {
            var url = mainServiceUrl + '/api/persons/' + personId + '/works';

            return $http.get(url)
                .then(function (response) {
                    return _.map(response.data.result.rows, function (item) { return item });
                })
                .catch(function (e) {
                    return exception.catcher('XHR Failed for getPersonWorks')(e);
                });
        }



    }
})();
