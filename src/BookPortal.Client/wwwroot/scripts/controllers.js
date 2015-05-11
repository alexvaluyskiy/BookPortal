'use strict';

/* Controllers */
var awardCatApp = angular.module('awardCatApp', []);

function getImagesUrl() {
    return 'http://data.fantlab.org/images';
}

function getDefaultAwardSort() {
    return 'lang';
}

function Award(data) {
    this.awardId = data.id;
    this.rusname = data.rusname;
    this.name = data.name;
    this.countryId = data.country.id;
    this.countryName = data.country.name;
    this.awardClose = data.isclosed === 1 ? getImagesUrl() + '/sclosed.gif' : getImagesUrl() + '/sclosedna.gif';
    this.awardType = data.awardtype === 1 ? 'общелитературная' : 'фантастическая';
    //this.nominationsCount = data.nomi_count;
    //this.contestsCount = data.contests_count;
    //this.minDate = data.min_date.replace(/-\d{2}-\d{2}/, "");
    //this.maxDate = data.max_date.replace(/-\d{2}-\d{2}/, "");
    this.awardUrl = "/award" + data.award_id;
    this.iconScr = getImagesUrl() + '/awards/icons/' + data.id + '_icon';
    this.countryImageScr = getImagesUrl() + '/flags/' + data.country.id + ".png";
}

awardCatApp.controller('AwardListCtrl', ['$scope', '$http', function awardListCtrl($scope, $http) {
    function loadDataFromService(sort) {
        $http.get('http://localhost:5000/api/awards?sort=' + sort).success(function (data) {
            $scope.awards = $.map(data.result.rows, function (item) { return new Award(item.values); });
        });
    }

    loadDataFromService(getDefaultAwardSort());

    $scope.sortTypes = [
      { sortName: "по названию", sortValue: "name" },
      { sortName: "по стране", sortValue: "country" },
      { sortName: "по типу", sortValue: "type" },
      { sortName: "по языку", sortValue: "lang" },
    ];

    $scope.currentSort = $scope.sortTypes[3];

    $scope.changeSort = function () {
        loadDataFromService($scope.currentSort.sortValue);
    }
}]);