'use strict';
/* Controllers */
var htmlTemplateController = angular.module('htmlTemplateController', []);
htmlTemplateController.controller('htmlTemlateListController', ['$scope',
  function ($scope) {
      $scope.templates =
           [{ name: 'app1.html', url: '/content/templates/app1.html' }
           , { name: 'app1.1.html', url: '/content/templates/app1.1.html' }
           , { name: 'app1.2.html', url: '/content/templates/app1.2.html' }];
      $scope.template = $scope.templates[0];
  }]);