'use strict';
angular.module('contactsListApp', ['ngRoute','AdalAngular'])
.config(['$routeProvider', '$httpProvider', 'adalAuthenticationServiceProvider', function ($routeProvider, $httpProvider, adalProvider) {

    $routeProvider.when("/Home", {
        controller: "homeCtrl",
        templateUrl: "/App/Views/Home.html",
    }).when("/Contacts", {
        controller: "contactsCtrl",
        templateUrl: "/App/Views/Contacts.html",
        requireADLogin: true,
    }).when("/UserData", {
        controller: "userDataCtrl",
        templateUrl: "/App/Views/UserData.html",
    }).otherwise({ redirectTo: "/Home" });

    var endpoints = { 
        "https://contactslistapipreet.azurewebsites.net/": "9361d4bf-5398-4497-a471-66b4973625fa"
        //"https://localhost:44300/": "{your client id}"
    };

    adalProvider.init(
        {
            instance: 'https://login.microsoftonline.com/', 
            tenant: 'preetsinghcandorsoftwaresol.onmicrosoft.com',
            clientId: '9361d4bf-5398-4497-a471-66b4973625fa',
            extraQueryParameter: 'nux=1',
            endpoints: endpoints
            //cacheLocation: 'localStorage', // enable this for IE, as sessionStorage does not work for localhost.
        },
        $httpProvider
        );
   
}]);
