(function () {
    'use strict';

    angular
        .module('app.persons.view')
        .controller('PersonsViewController', PersonsViewController);

    PersonsViewController.$inject = ['$http', '$q', 'dataservice', 'logger', '$stateParams'];
    /* @ngInject */
    function PersonsViewController($http, $q, dataservice, logger, $stateParams) {
        var vm = this;
        vm.title = 'Персона';
        vm.personId = $stateParams.personId || 1;

        activate();

        function activate() {
            getPerson(vm.personId).then(function() {
                var promises = [
                    getPersonWorks(vm.personId),
                    getCountries(),
                    getWorkTypes()
                ];
                return $q.all(promises).then(function () {
                    logger.info('Activated Persons View');
                });
            });
        }

        function getPerson(personId) {
            return dataservice.getPerson(personId).then(function (data) {
                vm.person = data;

                vm.title = 'Персона: ' + vm.person.name;
                return vm.person;
            });
        }

        function getPersonWorks(personId) {
            return dataservice.getPersonWorks(personId).then(function (data) {
                var mockData = {
                    "6": [
                        {
                            "workid": 1,
                            "rusname": "Гиперион",
                            "name": "Hyperion",
                            "year": 1989,
                            "votescount": 4615, // TODO
                            "rating": 8.81, // TODO
                            "usermark": 8, // TODO
                            "root_cycle_work_id": 92, // TODO
                            "root_cycle_work_name": "Песни Гипериона", // TODO
                            "root_cycle_work_type_id": 13 // TODO
                        },
                        {
                            "workid": 603,
                            "rusname": "Горящий Эдем",
                            "name": "Fires of Eden",
                            "altname": "Костры Эдема",
                            "year": 1994,
                            "votescount": 225,
                            "rating": 6.89
                        }
                    ],
                    "8": [
                        {
                            "workid": 33891,
                            "name": "Presents of Mind",
                            "year": 1985,
                            "persons": [
                                {
                                    "personid": 396,
                                    "name": "Эдвард Брайант",
                                    "persontype": "author"
                                },
                                {
                                    "personid": 788,
                                    "name": "Конни Уиллис",
                                    "persontype": "author"
                                }
                            ],
                            "coauthortype": "coauthor", // TODO: coauthor | author | editor | macycle
                            "votescount": 1,
                            "rating": 7
                        }
                    ]
                }

                vm.person.works = mockData;
                // vm.person.works = _.groupBy(data, function (item) { return item.worktypelevel; });
                return vm.person.works;
            });
        }

        function getCountries() {
            return dataservice.getCountries().then(function (data) {
                vm.countries = data;
                return vm.countries;
            });
        }

        function getWorkTypes() {
            return dataservice.getWorkTypes().then(function (data) {
                vm.worktypes = _.reduce(data, function (m, x) { m[x.level] = x; return m; }, {});
                return vm.worktypes;
            });
        }
    }
})();
