(function () {
    'use strict';

    angular
        .module('app.persons.view')
        .directive('personwork', personwork);

    personwork.$inject = ['recursionhelper'];

    function personwork(recursionhelper) {
        var controller = ['$scope', function ($scope) {
            function init() {
                $scope.showlefttoolbar = $scope.level === 1 && !$scope.worktype.isnode && !$scope.work.inplans;

                // innactive name
                if (!$scope.work.workid) {
                    if ($scope.work.rusname.length > 0 && $scope.work.name.length > 0) {
                        $scope.work.innactivename = $scope.work.rusname + ' / ' + $scope.work.name;
                    } else if ($scope.work.rusname.length > 0) {
                        $scope.work.innactivename = $scope.work.rusname;
                    } else {
                        $scope.work.innactivename = $scope.work.name;
                    }
                }

                $scope.hasrusname = $scope.work.workid && $scope.work.rusname.length > 0;
                $scope.hasname = $scope.work.workid && $scope.work.name.length > 0;
                $scope.hasfullname = $scope.work.workid &&
                    $scope.work.rusname.length > 0 &&
                    $scope.work.name.length > 0;

                // co-authors people
                if ($scope.work.coauthortype) {
                    var coauthors = '';
                    switch ($scope.work.coauthortype) {
                        case 'macycle':
                            coauthors = 'межавторский цикл';
                            break;
                        case 'coauthor':
                            coauthors = $scope.work.persons.length === 1 ? 'Соавтор' : 'Соавторы';
                            break;
                        case 'editor':
                            coauthors = $scope.work.persons.length === 1 ?
                                'Редактор-составитель' :
                                'Редакторы-составители';
                            break;
                        case 'author':
                            coauthors = $scope.work.persons.length === 1 ? 'Автор' : 'Авторы';
                            break;
                    }

                    if (coauthors !== 'macycle') {
                        $scope.work.coauthors = coauthors +
                            ': ' + _.map($scope.work.persons, function (item) { return item.name; }).join(', ');
                    } else {
                        $scope.work.coauthors = coauthors;
                    }
                }

                // additional information
                var addinfo = [];
                if ($scope.work.inplans) {
                    addinfo.push($scope.work.worktypename.toLowerCase());
                }
                if ($scope.work.notfinished) {
                    addinfo.push($scope.work.notfinished);
                }
                if ($scope.work.publishtype) {
                    addinfo.push($scope.work.publishtype);
                }
                $scope.work.additionalinfo = addinfo.join(', ');

                // conditions
                $scope.showadditionalyear = $scope.level > 1 && $scope.work.year > 0;
                $scope.showisaddition = $scope.level > 1 && $scope.work.isaddition;
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