(function () {
    'use strict';

    angular
        .module('app.works.view')
        .controller('WorksViewController', WorksViewController);

    WorksViewController.$inject = ['$http', '$q', 'dataservice', 'logger', '$stateParams'];
    /* @ngInject */
    function WorksViewController($http, $q, dataservice, logger, $stateParams) {
        var vm = this;
        vm.title = 'Произведение';
        vm.workId = $stateParams.workId || 1;

        activate();

        function activate() {
            getWork(vm.workId).then(function() {
                var promises = [
                    getWorkTranslations(vm.workId),
                    getWorkGenres(vm.workId),
                    getWorkAwards(vm.workId),
                    getWorkEditions(vm.workId),
                    getWorkReviews(vm.workId)
                ];
                return $q.all(promises).then(function () {
                    logger.info('Activated Works View');
                });
            });
        }

        function getWork(workId) {
            return dataservice.getWork(workId).then(function (data) {
                vm.work = data;

                // TODO: temp
                vm.work.persons = [
                    { id: 1, name: "Дэн Симмонс" }
                ];
                vm.work.is_plan = true;
                vm.work.published = false;
                vm.work.not_finished = true;

                vm.title = 'Произведение: ' + vm.work.rus_name;
                return vm.work;
            });
        }

        function getWorkTranslations(workId) {
            return dataservice.getWorkTranslations(workId).then(function (data) {
                vm.work.translations = data;
                return vm.work.translations;
            });
        }

        function getWorkGenres(workId) {
            return dataservice.getWorkGenres(workId).then(function (data) {
                vm.work.genres = data;
                return vm.work.genres;
            });
        }

        function getWorkAwards(workId) {
            return dataservice.getWorkAwards(workId).then(function (data) {
                vm.work.awards = data;
                return vm.work.awards;
            });
        }

        function getWorkEditions(workId) {
            return dataservice.getWorkEditions(workId).then(function (data) {
                vm.work.editions = data;
                return vm.work.editions;
            });
        }

        function getWorkReviews(workId) {
            return dataservice.getWorkReviews(workId).then(function (data) {
                vm.work.reviews = data;
                return vm.work.reviews;
            });
        }
    }
})();
