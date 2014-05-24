'use strict';
/* Controllers */
var phonecatControllers = angular.module('phonecatControllers', []);
//phonecatControllers.controller('PhoneListCtrl', ['$scope', '$http',
//  function ($scope, $http) {
//      $http.get('data/phoneData.json').success(function (data) {
//          $scope.phones = data;
//      });
//      $scope.orderProp = 'age';
//  }]);
phonecatControllers.controller('PhoneListCtrl', ['$scope', 'Phone',
  function ($scope, Phone) {
      $scope.phones = Phone.query();
      $scope.orderProp = 'age';
      $scope.totalPrice = '$450';
      $scope.HideAnimation = function () {
          var animation = document.getElementById("viewLoader");
          animation.style.display ="none"; 
      }
  }]);


//phonecatControllers.controller('PhoneDetailCtrl', ['$scope', '$routeParams', '$http',
//  function ($scope, $routeParams, $http) {
//      $scope.orderProp = 'name';
//      $http.get('data/' + $routeParams.phoneId + '.json').success(function (data) {
//          $scope.phone = data;
//          $scope.mainImageUrl = data.images[0];
//      });

//      $scope.setImage = function (imageUrl) {
//          $scope.mainImageUrl = imageUrl;
//      }
//  }]);

phonecatControllers.controller('PhoneDetailCtrl', ['$scope', '$routeParams', 'Phone',
  function ($scope, $routeParams, Phone) {
      $scope.orderProp = 'name';
      $scope.phone = Phone.get({ phoneId: $routeParams.phoneId }, function (phone)
      {
          $scope.mainImageUrl = phone.images[0];
      });

      $scope.setImage = function (imageUrl) {
          $scope.mainImageUrl = imageUrl;
      }
  }]);

