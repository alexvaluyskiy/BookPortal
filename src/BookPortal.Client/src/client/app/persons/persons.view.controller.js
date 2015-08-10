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
                    getCountries()
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
                vm.person.works = _.groupBy(data, function(item) {
                     return item.work_type_id;
                });

                return vm.person.works;
            });
        }

        function getCountries() {
            return dataservice.getCountries().then(function (data) {
                vm.countries = data;

                return vm.countries;
            });
        }
    }
})();
