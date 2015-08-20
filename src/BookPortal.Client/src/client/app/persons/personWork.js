(function () {
    'use strict';

    angular
        .module('app.persons.view')
        .directive('personWork', personWork);

    personWork.$inject = ['recursionhelper'];

    function personWork(recursionhelper) {
        var controller = [function () {
            var vm = this;

            function init() {
                vm.showlefttoolbar = vm.level === 1 && !vm.worktype.isnode && !vm.work.inplans;

                // innactive name
                if (!vm.work.workid) {
                    if (vm.work.rusname && vm.work.name) {
                        vm.work.innactivename = vm.work.rusname + ' / ' + vm.work.name;
                    } else if (vm.work.rusname) {
                        vm.work.innactivename = vm.work.rusname;
                    } else {
                        vm.work.innactivename = vm.work.name;
                    }
                }

                vm.hasrusname = vm.work.workid && vm.work.rusname;
                vm.hasname = vm.work.workid && vm.work.name;
                vm.hasfullname = vm.work.workid && vm.work.rusname && vm.work.name;

                // co-authors people
                if (vm.work.coauthortype) {
                    var coauthors = '';
                    switch (vm.work.coauthortype) {
                        case 'macycle':
                            coauthors = 'межавторский цикл';
                            break;
                        case 'coauthor':
                            coauthors = vm.work.persons.length === 1 ? 'Соавтор' : 'Соавторы';
                            break;
                        case 'editor':
                            coauthors = vm.work.persons.length === 1 ?
                                'Редактор-составитель' :
                                'Редакторы-составители';
                            break;
                        case 'author':
                            coauthors = vm.work.persons.length === 1 ? 'Автор' : 'Авторы';
                            break;
                    }

                    if (coauthors !== 'macycle') {
                        vm.work.coauthors = coauthors +
                            ': ' + _.map(vm.work.persons, function (item) { return item.name; }).join(', ');
                    } else {
                        vm.work.coauthors = coauthors;
                    }
                }

                // additional information
                var addinfo = [];
                if (vm.work.inplans) {
                    addinfo.push(vm.work.worktypename.toLowerCase());
                }
                if (vm.work.notfinished === true) {
                    addinfo.push('не закончено');
                }
                if (vm.work.publishtype !== undefined) {
                    switch (vm.work.publishtype) {
                        case 0:
                            addinfo.push(vm.level > 1 && vm.work.inplans ? 'еще не опубликовано' : 'не опубликовано');
                            break;
                        case 2:
                            addinfo.push('сетевая публикация');
                            break;
                        case 3:
                            addinfo.push('доступно в сети');
                            break;
                    }
                }
                vm.work.additionalinfo = addinfo.join(', ');

                // conditions
                vm.showadditionalyear = vm.level > 1 && vm.work.year > 0;
                vm.showisaddition = vm.level > 1 && vm.work.isaddition;
            }

            init();
        }];

        return {
            restrict: 'E',
            scope: { work: '=', level: '=', worktype: '=' },
            controller: controller,
            controllerAs: 'vm',
            bindToController: true,
            templateUrl: 'app/persons/personWork.html',
            compile: function (element) {
                return recursionhelper.compile(element);
            }
        };
    }
})();
