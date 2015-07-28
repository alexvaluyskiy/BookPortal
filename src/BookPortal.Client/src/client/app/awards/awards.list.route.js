(function() {
    'use strict';

    angular
        .module('app.awards.list')
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
                    templateUrl: 'app/awards/awards.list.html',
                    controller: 'AwardsListController',
                    controllerAs: 'vm',
                    title: 'Список премий',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-lock"></i> Список премий'
                    }
                }
            }
        ];
    }
})();
