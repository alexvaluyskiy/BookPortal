(function () {
    'use strict';

    angular
        .module('app.series.view')
        .controller('SeriesViewController', SeriesViewController);

    function Serie(data) {
        var imagesCdnUrl = "http://data.fantlab.org/images/";

    }

    SeriesViewController.$inject = ['$http', '$q', 'dataservice', 'logger', '$stateParams'];
    /* @ngInject */
    function SeriesViewController($http, $q, dataservice, logger, $stateParams) {
        var vm = this;
        vm.title = 'Серия';
        vm.changeSort = changeSort;

        vm.sortTypes = [
          { sortName: "по умолчанию", sortValue: "order" },
          { sortName: "по названию", sortValue: "name" },
          { sortName: "по автору", sortValue: "authors" },
          { sortName: "по году", sortValue: "year" }
        ];

        vm.selectedSortType = vm.sortTypes[1];

        activate();

        function activate() {
            var promises = [
                getSerie($stateParams.serieId),
                getSerieTree($stateParams.serieId),
                getSerieEditions($stateParams.serieId, vm.selectedSortType.sortValue)];
            return $q.all(promises).then(function () {
                logger.info('Activated Serie View');
            });
        }

        function getSerie(serieId) {
            return dataservice.getSerie(serieId).then(function (data) {
                vm.serie = data;
                vm.title = 'Серия: ' + vm.serie.name;
                return vm.serie;
            });
        }

        function getSerieTree(serieId) {
            return dataservice.getSerieTree(serieId).then(function (data) {
                vm.serietree = data;
                return vm.serietree;
            });
        }

        function getSerieEditions(serieId, sortType) {
            return dataservice.getSerieEditions(serieId, sortType).then(function (data) {
                vm.serieeditions = data;
                return vm.serieeditions;
            });
        }

        function changeSort() {
            activate();
        }
    }
})();
