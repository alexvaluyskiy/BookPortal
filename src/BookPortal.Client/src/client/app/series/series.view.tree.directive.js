(function() {
    'use strict';

    angular
        .module('app.series.view')
        .directive('seriesviewtree', seriesviewtree);

    seriesviewtree.$inject = ['recursionhelper'];
    
    function seriesviewtree(recursionhelper) {
        var directive = {
            restrict: 'E',
            scope: { family: '=', serieid: '@' },
            template:
            '<p>' +
              '<b ng-if="family.serie_id == serieid">{{ family.name }}</b>' +
              '<a ng-if="family.serie_id != serieid" ui-sref="seriesview({ serieId: family.serie_id })">{{ family.name }}</a>' +
            '</p>' +
                '<ul>' +
                    '<li ng-repeat="child in family.series">' +
                        '<seriesviewtree family="child" serieid={{serieid}}></seriesviewtree>' +
                    '</li>' +
                '</ul>',
            compile: function (element) {
                return recursionhelper.compile(element);
            }
        };
        return directive;
    }

})();