"use strict";
// noinspection JSUnresolvedReference

angular.module("umbraco").controller("SIS.Controller", function ($scope, formHelper, $http) {

    $scope.createUrl = "";

    $scope.tabs = [
        {
            alias: "createChecksum",
            label: "Create checksum",
            active: true
        }, {
            alias: "generateScript",
            label: "Generate script",
            active: false
        }
    ];

    $scope.changeTab = function (selectedTab) {
        $scope.tabs.forEach(function (tab) {
            tab.active = false;
        });
        selectedTab.active = true;
    };

    $scope.createChecksum = function () {
        debugger;
        const data = $scope.createUrl;
        $http({
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            url: "/umbraco/backoffice/api/SubresourceIntegrityChecker/CreateChecksum",
            data: JSON.stringify({ url: data }),
        });
    };
});
