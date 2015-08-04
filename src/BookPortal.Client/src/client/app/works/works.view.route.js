(function() {
    'use strict';

    angular
        .module('app.works.view')
        .run(appRun);

    appRun.$inject = ['routerHelper'];
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'worksview',
                config: {
                    url: '/works/:workId',
                    templateUrl: 'app/works/works.view.html',
                    controller: 'WorksViewController',
                    controllerAs: 'vm',
                    title: 'Просмотр произведения',
                    settings: {
                        nav: 10,
                        content: '<i class="fa fa-lock"></i> Просмотр произведения'
                    }
                }
            }
        ];
    }
})();
