angular.module('ApiExplorer')
    .controller('ApiExplorerCtrl', ['$scope', 'adalAuthenticationService', '$location', 'ApiExplorerSvc', function ($scope, adalService, $location, apiService) {
        var expanded = true;

        $scope.selectedOptions = "GET";
        $scope.selectedVersion = "v1.0";
        $scope.showJsonEditor = false;
        $scope.showDuration = false;
        $scope.showJsonViewer = true;
        $scope.showImage = false;

        initializeJsonViewer($scope, run, apiService);

        $scope.login = function () {
            adalService.login();
        };
        $scope.logout = function () {
            adalService.logOut();
        };
        $scope.isActive = function (viewLocation) {
            return viewLocation === $location.path();
        };
}]);

angular.module('ApiExplorer')
    .controller('DropdownCtrl', function ($scope, $log) {
        $scope.selectedOptions = "GET";

        $scope.items = [
            'GET',
            'POST',
            'PATCH',
            'DELETE'
          ];

        $scope.OnItemClick = function (selectedOption) {
            $log.log(selectedOption);
            $scope.selectedOptions = selectedOption;
            $scope.$parent.selectedOptions = selectedOption;
            if (selectedOption == 'POST' || selectedOption == 'PATCH') {
                $scope.$parent.showJsonEditor = true;
                initializeJsonEditor($scope.$parent);
            } else if (selectedOption == 'GET' || selectedOption == 'DELETE') {
                $scope.$parent.showJsonEditor = false;
            }
        }
    });

angular.module('ApiExplorer')
    .controller('VersionCtrl', function ($scope, $log) {
        $scope.selectedVersion = "v1.0";

        $scope.items = [
            'beta',
            'v1.0',
          ];

        $scope.OnItemClick = function (selectedVersion) {
            $log.log(selectedVersion);
            $scope.selectedVersion = selectedVersion;
            $scope.$parent.selectedVersion = selectedVersion;
            if (window.location.pathname.indexOf('zh-cn') > -1) {
                $scope.$parent.text = $scope.$parent.text.replace(/https:\/\/microsoftgraph.chinacloudapi.cn($|\/([\w]|\.)*($|\/))/, "https://microsoftgraph.chinacloudapi.cn/" + selectedVersion + "/");
            }
            else {
                $scope.$parent.text = $scope.$parent.text.replace(/https:\/\/graph.microsoft.com($|\/([\w]|\.)*($|\/))/, "https://graph.microsoft.com/" + selectedVersion + "/");
            }
        }
    });

angular.module('ApiExplorer')
    .controller('datalistCtrl', function ($scope, $log) {
        $scope.urlOptions = [];

        $scope.$parent.$on("clearUrls", function (event, args) {
            $scope.urlOptions = [];
        });

        $scope.$parent.$on("populateUrls", function (event, args) {
            if (window.location.pathname.indexOf('zh-cn') > -1) {
                $scope.urlOptions = [
                    /*"https://microsoftgraph.chinacloudapi.cn/v1.0/me",
                    "https://microsoftgraph.chinacloudapi.cn/v1.0/users",
                    "https://microsoftgraph.chinacloudapi.cn/v1.0/me/messages",
                    "https://microsoftgraph.chinacloudapi.cn/v1.0/drive",
                    "https://microsoftgraph.chinacloudapi.cn/v1.0/groups",
                    "https://microsoftgraph.chinacloudapi.cn/v1.0/devices",
                    "https://microsoftgraph.chinacloudapi.cn/beta/me",
                    "https://microsoftgraph.chinacloudapi.cn/beta/users",
                    "https://microsoftgraph.chinacloudapi.cn/beta/me/messages",
                    "https://microsoftgraph.chinacloudapi.cn/beta/drive",
                    "https://microsoftgraph.chinacloudapi.cn/beta/devices",
                    "https://microsoftgraph.chinacloudapi.cn/beta/groups",
                    "https://microsoftgraph.chinacloudapi.cn/beta/me/notes/notebooks"*/
                ];
            }
            else {
                $scope.urlOptions = [
                    /*"https://graph.microsoft.com/v1.0/me",
                    "https://graph.microsoft.com/v1.0/users",
                    "https://graph.microsoft.com/v1.0/me/messages",
                    "https://graph.microsoft.com/v1.0/drive",
                    "https://graph.microsoft.com/v1.0/groups",
                    "https://graph.microsoft.com/v1.0/devices",
                    "https://graph.microsoft.com/beta/me",
                    "https://graph.microsoft.com/beta/users",
                    "https://graph.microsoft.com/beta/me/messages",
                    "https://graph.microsoft.com/beta/drive",
                    "https://graph.microsoft.com/beta/devices",
                    "https://graph.microsoft.com/beta/groups",
                    "https://graph.microsoft.com/beta/me/notes/notebooks"*/
                ];
            }
        });
    });

angular.module('ApiExplorer').controller('FormCtrl', ['$scope', '$log', 'ApiExplorerSvc', 'ngProgressFactory', function ($scope, $log, apiService, ngProgressFactory) {
    if (window.location.pathname.indexOf('zh-cn') > -1) {
        $scope.text = 'https://microsoftgraph.chinacloudapi.cn/v1.0/';
    }
    else {
        $scope.text = 'https://graph.microsoft.com/v1.0/me';
    }
    $scope.duration = "";
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.listData = "requestList";
    $scope.photoData = "";
    $scope.responseHeaders = "";

    $scope.$emit('populateUrls');

    // custom link re-routing logic to resolve links
    $scope.$parent.$on("urlChange", function (event, args) {
        msGraphLinkResolution($scope, $scope.$parent.jsonViewer.getSession().getValue(), args);
    });

    $scope.submit = function () {
        $scope.$emit('clearUrls');
        if ($scope.text) {
            $scope.previousString = $scope.text;
            $log.log($scope.text);
            try {
                ga('send', 'event', 'GraphExplorer', $scope.selectedOptions + " " + $scope.text);
                MscomCustomEvent('ms.InteractionType', '4', 'ms.controlname', 'graphexplorer', 'ms.ea_action', $scope.selectedOptions, 'ms.contentproperties', $scope.text);
            }
            catch (error) {

            }

            if ($scope.userInfo.isAuthenticated) {
                $scope.showJsonViewer = true;
                $scope.showImage = false;

                $scope.progressbar.reset();
                $scope.progressbar.start();
                var postBody = "";
                if ($scope.jsonEditor != undefined) {
                    postBody = $scope.jsonEditor.getSession().getValue();
                }
                var startTime = new Date();
                var endTime = null;
                apiService.performQuery($scope.selectedOptions)($scope.text, postBody).success(function (results, status, headers, config) {
                    if (isImageResponse(headers)) { 
                        handleImageResponse($scope, apiService, headers);
                    } else if (isHtmlResponse(headers)) {  
                        handleHtmlResponse($scope, startTime, results, headers);
                    } else if (isXmlResponse(results)) {
                        handleXmlResponse($scope, startTime, results, headers);
                    } else {
                        handleJsonResponse($scope, startTime, results, headers);
                    }
                }).error(function (err, status) {
                    handleJsonResponse($scope, startTime, err, null);
                });
            }
        }
    };
}]);