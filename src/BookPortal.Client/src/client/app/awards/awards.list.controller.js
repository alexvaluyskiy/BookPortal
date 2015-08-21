(function () {
    'use strict';

    angular
        .module('app.awards.list')
        .controller('AwardsListController', AwardsListController);

    function Award(data) {
        var imagesCdnUrl = 'http://data.fantlab.org/images/';

        function getFullName(rusname, name) {
            if (rusname !== '' && name !== '') {
                return rusname + ' / ' + name;
            } else if (name !== '') {
                return name;
            } else {
                return rusname;
            }
        }

        function formatDate(date) {
            if (date !== undefined) {
                return date.replace(/-\d{2}-\d{2}/, '');
            } else {
                return '';
            }
        }

        this.id = data.awardid;
        this.awardUrl = '/award/' + this.id;
        this.awardIconUrl = imagesCdnUrl + 'awards/icons/' + this.id + '_icon';
        this.fullName = getFullName(data.rusname, data.name);
        this.countryUrl = imagesCdnUrl + 'flags/' + data.countryid + '.png';
        this.countryName = data.countryname;
        this.nominationsCount = 4;
        this.contestsCount = 5;
        this.awardClosedUrl = data.awardclosed === true ? imagesCdnUrl + 'sclosed.gif' : imagesCdnUrl + '/sclosedna.gif';
        this.firstAwardYear = formatDate(data.firstcontestdate);
        this.lastAwardYear = formatDate(data.lastcontestdate);
    }

    AwardsListController.$inject = ['$http', '$q', 'dataservice', 'logger'];
    /* @ngInject */
    function AwardsListController($http, $q, dataservice, logger) {
        var vm = this;
        vm.awards = [];
        vm.title = 'Список премий';
        vm.changeSort = changeSort;

        vm.sortTypes = [
          { sortName: 'по названию', sortValue: 'rusname' },
          { sortName: 'по стране', sortValue: 'country' },
          { sortName: 'по номеру', sortValue: 'id' },
          { sortName: 'по языку', sortValue: 'language' }
        ];

        vm.selectedSortType = vm.sortTypes[2];

        activate();

        function activate() {
            var promises = [getAwards(vm.selectedSortType.sortValue)];
            return $q.all(promises).then(function () {
                logger.info('Activated Awards List');
            });
        }

        function getAwards(sortType) {
            return dataservice.getAwards(sortType).then(function (data) {
                vm.awards = _.map(data, function (item) { return new Award(item); });
                return vm.awards;
            });
        }

        function changeSort() {
            activate();
        }
    }
})();
