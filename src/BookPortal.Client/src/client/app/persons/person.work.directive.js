(function() {
    'use strict';

    angular
        .module('app.persons.view')
        .directive('personwork', personwork);

    personwork.$inject = ['recursionhelper'];
    
    function personwork(recursionhelper) {
        var directive = {
            restrict: 'E',
            scope: { work: '=' },
            templateUrl: 'app/persons/person.work.template.html',
            compile: function (element) {
                return recursionhelper.compile(element);
            }
        };
        return directive;
    }

})();