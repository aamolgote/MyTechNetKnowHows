'use strict';
/* App Module */
var phonecatApp = angular.module('phonecatApp', [
  'ngRoute',
  'phonecatControllers',
  'phonecatServices'
]);
//phonecatApp.config(function ($locationProvider) {
//    $locationProvider.html5Mode(true);
//});

var scriptPrefix = ""
phonecatApp.config(['$routeProvider',
  function ($routeProvider) {
      
      $routeProvider.
        when('/', {
            templateUrl: 'SPA/PhoneList.html',
            controller: 'PhoneListCtrl'
        }).
        when('/phones/:phoneId', {
            templateUrl: 'SPA/PhoneDetail.html',
            controller: 'PhoneDetailCtrl'
        }).
        when('/CheckOut', {
            templateUrl: 'SPA/CheckOut.html',
            controller: 'PhoneListCtrl'
        }).
        when('/MakePayment', {
            templateUrl: 'SPA/MakePayment.html',
            controller: 'PhoneListCtrl'
        }).
        when('/RegisteredCreditCards', {
            templateUrl: 'Home/RegisteredCreditCards',
            controller: 'PhoneListCtrl'
        }).
        otherwise({
            redirectTo: '/'
        });
  }]);

