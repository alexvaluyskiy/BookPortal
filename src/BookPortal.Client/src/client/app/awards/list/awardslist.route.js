(function() {
    'use strict';

    angular
        .module('app.awardslist')
        .run(appRun);

    appRun.$inject = ['routerHelper'];
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'awardslist',
                config: {
                    url: '/awards',
                    templateUrl: 'app/awards/list/awardslist.html',
                    controller: 'AwardsListController',
                    controllerAs: 'vm',
                    title: 'Премии',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-lock"></i> Премии'
                    }
                }
            }
        ];
    }
})();
