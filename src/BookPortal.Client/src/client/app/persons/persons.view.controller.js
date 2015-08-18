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

        vm.sortTypes = [
          { sortName: "по году публикации", sortValue: "year" },
          { sortName: "по рейтингу", sortValue: "rating" },
          { sortName: "по количеству оценок", sortValue: "markscount" },
          { sortName: "по русскому названию", sortValue: "rusname" },
          { sortName: "по оригинальному названию", sortValue: "name" }
        ];

        var sortColumns = [
          ['year', 'groupindex', 'name', 'rusname'],
          ['-rating', 'groupindex'],
          ['-votescount', 'groupindex'],
          ['rusname', 'groupindex'],
          ['name', 'groupindex']
        ];

        vm.selectedSortType = vm.sortTypes[0];
        vm.orderByDefinition = sortColumns[0];

        vm.changeSort = function () {
            switch (vm.selectedSortType.sortValue) {
                case "rating":
                    vm.orderByDefinition = sortColumns[1];
                    break;
                case "markscount":
                    vm.orderByDefinition = sortColumns[2];
                    break;
                case "rusname":
                    vm.orderByDefinition = sortColumns[3];
                    break;
                case "name":
                    vm.orderByDefinition = sortColumns[4];
                    break;
                default:
                    vm.orderByDefinition = sortColumns[0];
            }
        };

        activate();

        function activate() {
            getPerson(vm.personId).then(function () {
                var promises = [
                    getPersonWorks(vm.personId),
                    getPersonGenres(vm.personId),
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
                var imagesCdnUrl = "http://data.fantlab.org/images/";

                vm.person = data;
                vm.person.personimageurl = imagesCdnUrl + 'autors/' + vm.person.personid;
                vm.person.countryimageurl = imagesCdnUrl + 'flags/' + vm.person.countryid + '.png';

                if (vm.person.birthdate)
                    vm.person.birthdate = moment(vm.person.birthdate).locale('ru').format('LL');
                if (vm.person.deathdate)
                    vm.person.deathdate = moment(vm.person.deathdate).locale('ru').format('LL');

                vm.title = 'Персона: ' + vm.person.name;
                return vm.person;
            });
        }

        function getPersonWorks(personId) {
            return dataservice.getPersonWorks(personId).then(function (data) {
                // filtering in plans works
                vm.person.worksinplans = _.filter(data, function (item) { return item.inplans === true; });

                // processing and grouping another works
                data = _.filter(data, function (item) { return item.inplans !== true; });
                data = _.groupBy(data, function (item) { return item.worktypelevel; });
                data = Object
                      .keys(data)
                      .sort(function(a, b) {
                          if (+a < +b) return -1;
                          if (+a > +b) return 1;
                          return 0;
                      })
                      .map(function (key) { return data[key]; });

                vm.person.works = data;
                return vm.person.works;
            });
        }

        function getPersonGenres(personId) {
            return dataservice.getPersonGenres(personId).then(function (data) {
                vm.person.genres = data;
                return vm.person.genres;
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
