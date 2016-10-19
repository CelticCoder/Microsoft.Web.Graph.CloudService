'use strict';

angular.module('ApiExplorer', ['ngRoute', 'AdalAngular', 'ngAnimate', 'ui.bootstrap', 'ngProgress'])
    .config(['$routeProvider', '$httpProvider', 'adalAuthenticationServiceProvider', function ($routeProvider, $httpProvider, adalProvider) {

        $routeProvider.when("/Home", {
            controller: "ApiExplorerCtrl",
            templateUrl: "/App/Views/Home.html",
        }).otherwise({
            redirectTo: '/'
        });

        if (window.location.pathname.indexOf('zh-cn') > -1) {
            adalProvider.init({
                instance: 'https://login.partner.microsoftonline.cn/',
                tenant: 'common',
                clientId: '61a7b0d6-2bc9-48b6-8653-ef6b496815cb',
                endpoints: {
                    "https://microsoftgraph.chinacloudapi.cn": "https://microsoftgraph.chinacloudapi.cn"
                },
                cacheLocation: 'localStorage'
            },
                $httpProvider
            );
        }
        else {
            adalProvider.init({
                instance: 'https://login.microsoftonline.com/',
                tenant: 'common',
                clientId: '8a3eb86b-8149-4231-9ff3-3c50958ea0fd',
                endpoints: {
                    "https://graph.microsoft.com": "https://graph.microsoft.com"
                },
                cacheLocation: 'localStorage'
            },
                $httpProvider
            );
        }
        }]);
    
// localhost:80 - ce8bcfc6-7aad-4738-8e81-67da88652b85'
// v2 - 76a89b1b-d49c-42e0-859a-53324fe7eb6a
//test - ce268d90-5d39-403c-a3a0-8d463140d4a9
//real - 8a3eb86b-8149-4231-9ff3-3c50958ea0fd
//    "Calendars.Read":["https://outlook.office.com","https://graph.microsoft.com"],          \
//    "Calendars.ReadWrite":["https://outlook.office.com","https://graph.microsoft.com"],     \
//    "Contacts.Read":["https://outlook.office.com","https://graph.microsoft.com"],           \
//    "Contacts.ReadWrite":["https://outlook.office.com","https://graph.microsoft.com"],      \
//    "Files.Read":["https://graph.microsoft.com"],                                           \
//    "Files.ReadWrite":["https://graph.microsoft.com"],                                      \
//    "User.Read":["https://graph.microsoft.com"],                                            \
//    "User.ReadWrite":["https://graph.microsoft.com"],                                       \
//    "People.Read":["https://outlook.office.com","https://graph.microsoft.com"],             \
//    "People.ReadWrite":["https://outlook.office.com","https://graph.microsoft.com"]         \ 