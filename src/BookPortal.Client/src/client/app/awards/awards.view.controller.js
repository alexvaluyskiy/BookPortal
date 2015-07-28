(function () {
    'use strict';

    angular
        .module('app.awards.view')
        .controller('AwardsViewController', AwardsViewController);

    AwardsViewController.$inject = ['$http', '$q', 'dataservice', 'logger', '$stateParams'];
    /* @ngInject */
    function AwardsViewController($http, $q, dataservice, logger, $stateParams) {
        var vm = this;
        vm.title = 'Просмотр премии';

        activate();

        function activate() {
            var promises = [getAward($stateParams.awardId)];
            return $q.all(promises).then(function () {
                logger.info('Activated Awards View');
            });
        }

        function getAward(awardId) {
            return dataservice.getAward(awardId).then(function (data) {
                vm.award = data;
                return vm.award;
            });
        }
    }
})();
