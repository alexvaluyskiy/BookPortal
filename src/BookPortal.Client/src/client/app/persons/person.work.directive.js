(function() {
    'use strict';

    angular
        .module('app.persons.view')
        .directive('personwork', personwork);

    personwork.$inject = ['recursionhelper'];
    
    function personwork(recursionhelper) {
        var controller = ['$scope', function ($scope) {
            function init() {
                var array = [];
                if ($scope.work.inplans) {
                    array.push($scope.work.worktypename.toLowerCase());
                }
                if ($scope.work.notfinished) {
                    array.push($scope.work.notfinished);
                }
                if ($scope.work.publishtype) {
                    array.push($scope.work.publishtype);
                }
                $scope.work.additionalinfo = array.join(', ');
            }

            init();
        }];

        return {
            restrict: 'E',
            scope: { work: '=', level: '=', worktype: '=' },
            controller: controller,
            templateUrl: 'app/persons/person.work.template.html',
            compile: function (element) {
                return recursionhelper.compile(element);
            }
        };
    }

})();