(function() {
    'use strict';

    angular
        .module('app.seriesview')
        .run(appRun);

    appRun.$inject = ['routerHelper'];
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'seriesview',
                config: {
                    url: '/series/:serieId',
                    templateUrl: 'app/series/view/seriesview.html',
                    controller: 'SeriesViewController',
                    controllerAs: 'vm',
                    title: 'Просмотр серии',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-lock"></i> Просмотр серии'
                    }
                }
            }
        ];
    }
})();
