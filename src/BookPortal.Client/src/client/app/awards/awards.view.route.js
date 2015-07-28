(function() {
    'use strict';

    angular
        .module('app.awards.view')
        .run(appRun);

    appRun.$inject = ['routerHelper'];
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'awardsview',
                config: {
                    url: '/awards/:awardId',
                    templateUrl: 'app/awards/awards.view.html',
                    controller: 'AwardsViewController',
                    controllerAs: 'vm',
                    title: 'Просмотр премии',
                }
            }
        ];
    }
})();
