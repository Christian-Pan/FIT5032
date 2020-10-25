// Defining angularjs module
var app = angular.module('demoModule', []);

// You will need to use your newly created Web API here
var apiURL = "https://localhost:44370/api/";

app.controller('demoCtrl', function ($scope, $http, feedbackService) {

    $scope.showAdd = false;
    $scope.showEdit = false;
    $scope.feedbackData = "";
    $scope.noResult = false;

    feedbackService.GetAllRecords().then(function (d) {
        $scope.feedbackData = d.data; // Success
        console.log($scope.feedbackData);
    }, function () {
        $scope.noResult = true;
    });

    $scope.Feedback = {
        Id: '',
        Title: '',
        Content: ''
    };


    // Clear
    $scope.back = function () {
        $scope.showEdit = false;
        $scope.showAdd = false;
    }

    $scope.addButton = function () {
        $scope.showAdd = true;
        $scope.showEdit = false;
    }

    //Add New feedback
    $scope.save = function () {
        if ($scope.Feedback.Title != "" && $scope.Feedback.Content != "") {
            // Incremental primary key
            if ($scope.feedbackData.length > 0) {
                $scope.Feedback.Id = $scope.feedbackData[$scope.feedbackData.length - 1].Id + 1;
            } else {
                $scope.Feedback.Id = 0;
            }

            $http({
                method: 'POST',
                url: apiURL + "feedbacks",
                data: $scope.Feedback
            }).then(function successCallback(response) {
                $scope.feedbackData.push(response.data);
                $scope.addResult = true;
                $scope.clear();
                alert("Feedback Posted Successfully");
            }, function errorCallback(response) {
                $scope.noResult = true;
            });
        }
        else {
            alert('Please enter both the title and content.');
        }
    };

    $scope.edit = function (index, data) {
        $scope.updateIndex = index;
        $scope.showAdd = false;
        $scope.showEdit = true;
        $scope.Feedback = { Id: data.Id, Title: data.Title, Content: data.Content }
    }

    // Cancel feedback details
    $scope.cancel = function () {
        $scope.showEdit = false;
    }

    // Update product details
    $scope.update = function () {
        if ($scope.Feedback.Title != "" && $scope.Feedback.Content != "") {

            $http({
                method: 'PUT',
                url: apiURL + "feedbacks/" + $scope.Feedback.Id,
                data: $scope.Feedback
            }).then(function successCallback(response) {
                var id = $scope.Feedback.Id;
                var Feedback = $scope.Feedback;
                $scope.feedbackData[$scope.updateIndex] = Feedback;
                $scope.updateResult = true;
            }, function errorCallback(response) {
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please enter the required values.');
        }
    };

    // Delete feedback
    $scope.delete = function (index) {
        $http({
            method: 'DELETE',
            url: apiURL + "feedbacks/" + $scope.feedbackData[index].Id
        }).then(function successCallback(response) {
            $scope.deleteResult = true;
            $scope.clear();
        }, function errorCallback(response) {
            alert("Error : " + response.data.ExceptionMessage);
        });
    };

});

app.factory('feedbackService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get(apiURL + "feedbacks");
    }
    return fac;
});