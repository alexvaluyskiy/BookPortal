(function() {
    'use strict';

    angular
        .module('app.series.view')
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
                    templateUrl: 'app/series/series.view.html',
                    controller: 'SeriesViewController',
                    controllerAs: 'vm',
                    title: 'Просмотр серии',
                    settings: {
                        nav: 10,
                        content: '<i class="fa fa-lock"></i> Просмотр серии'
                    }
                }
            }
        ];
    }
})();
