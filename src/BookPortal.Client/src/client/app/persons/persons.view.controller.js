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
                data = [
                    {
                        "workid": 1,
                        "rusname": "Гиперион",
                        "name": "Hyperion",
                        "year": 1989,
                        "votescount": 4615, // TODO
                        "rating": 8.81, // TODO
                        "usermark": 8, // TODO
                        "worktypelevel": 6,
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
                        "rating": 6.89,
                        "worktypelevel": 6
                    },
                    {
                        "workid": 596601,
                        "rusname": "4-й роман о Джо Курце",
                        "worktypelevel": 6,
                        "worktypename": "Роман",
                        "inplans": true,
                        "notfinished": "не окончен",
                        "publishtype": "не опубликован"
                    },
                    {
                        "workid": 33891,
                        "name": "Presents of Mind",
                        "year": 1985,
                        "worktypelevel": 8,
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
                    },
                    {
                        "workid": 92,
                        "rusname": "Песни Гипериона",
                        "name": "Hyperion Cantos",
                        "year": 1989,
                        "votescount": 2026,
                        "rating": 9.04,
                        "worktypelevel": 1,
                        "childworks": [
                            {
                                "workid": 1,
                                "rusname": "Гиперион",
                                "name": "Hyperion",
                                "year": 1989, // TODO: если это циклы, то в API не проставлять год
                                "votescount": 4615,
                                "rating": 8.81,
                                "usermark": 8
                            }
                        ]
                    },
                    {
                        "workid": 131904,
                        "rusname": "Жертвоприношение",
                        "name": "The Offering",
                        "year": 1990,
                        "votescount": 80,
                        "rating": 6.00,
                        "worktypelevel": 24
                    },
                    {
                        "workid": 622,
                        "rusname": "Вспоминая Сири",
                        "name": "Remembering Siri",
                        "year": 1983,
                        "votescount": 657,
                        "rating": 8.69,
                        "worktypelevel": 8,
                        "bonustext": "вошел в состав романа «Гиперион»"
                    }
                ];

                // processing element before rendering
                data = _.map(data, function (item) {
                    var array = [];
                    if (item.inplans) {
                        array.push(item.worktypename.toLowerCase());
                    }
                    if (item.notfinished) {
                        array.push(item.notfinished);
                    }
                    if (item.publishtype) {
                        array.push(item.publishtype);
                    }
                    item.additionalinfo = array.join(', ');

                    return item;
                });

                // filtering in plans works
                vm.person.worksinplans = _.filter(data, function (item) { return item.inplans === true; });

                // processing another works
                data = _.filter(data, function (item) { return item.inplans !== true; });
                data = _.groupBy(data, function (item) { return item.worktypelevel; });

                vm.person.works = data;
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
