(function () {
    'use strict';

    var core = angular.module('app.core');

    core.config(toastrConfig);

    toastrConfig.$inject = ['toastr'];
    /* @ngInject */
    function toastrConfig(toastr) {
        toastr.options.timeOut = 4000;
        toastr.options.positionClass = 'toast-bottom-right';
    }

    var config = {
        appErrorPrefix: '[BookPortal.Client Error] ',
        appTitle: 'BookPortal.Client'
    };

    core.value('config', config);

    core.config(configure);

    configure.$inject = ['$logProvider', 'routerHelperProvider', 'exceptionHandlerProvider', 'localStorageServiceProvider', 'applicationInsightsServiceProvider'];
    /* @ngInject */
    function configure($logProvider, routerHelperProvider, exceptionHandlerProvider, localStorageServiceProvider, applicationInsightsServiceProvider) {
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }
        exceptionHandlerProvider.configure(config.appErrorPrefix);
        routerHelperProvider.configure({ docTitle: config.appTitle + ': ' });
        localStorageServiceProvider.setPrefix('BookPortal');
        applicationInsightsServiceProvider.configure('f7b8cd31-6851-42f6-80f7-85d2aef749d7',  {
            autoPageViewTracking: true,
            autoLogTracking: true,
            autoExceptionTracking: true
        });
    }
})();
