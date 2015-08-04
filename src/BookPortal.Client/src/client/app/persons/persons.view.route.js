(function() {
    'use strict';

    angular
        .module('app.persons.view')
        .run(appRun);

    appRun.$inject = ['routerHelper'];
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'personsview',
                config: {
                    url: '/persons/:personId',
                    templateUrl: 'app/persons/persons.view.html',
                    controller: 'PersonsViewController',
                    controllerAs: 'vm',
                    title: 'Просмотр персоны',
                    settings: {
                        nav: 10,
                        content: '<i class="fa fa-lock"></i> Просмотр персоны'
                    }
                }
            }
        ];
    }
})();
