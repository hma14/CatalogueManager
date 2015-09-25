$(function () {
    LoadAllCategories();
});

function LoadAllCategories() {
    
    var scope = angular.element('[ng-controller="CatalogueCtrl"]').scope();
    scope.$apply(function () {
        scope.getAllCategories();
    });
}



angular.module('CatalogueApp', [])
.config(['$compileProvider', function ($compileProvider) {
    $compileProvider.debugInfoEnabled(true);
}])
.controller('CatalogueCtrl', function ($scope, $http) {
    $scope.categories = [];
    $scope.isEdit = [];
    $scope.error = '';
    var saveState = {};

    $scope.getAllCategories = function () {
        $http.get('/api/Categories/').success(function (data, status, headers, config) {
            $scope.categories = data;
            $scope.error = '';
        }).error(function (data, status, headers, config) {
            $scope.error = "Oops... GET went wrong";
        });
    };

    $scope.addCategory = function (categoryName, category) {
        $http.post('/api/Categories/', { 'categoryName': categoryName, 'parentCategoryId': category.Id })
        .success(function (data, status, headers, config) {
            location.reload();
        }).error(function (data, status, headers, config) {
            $scope.error = "Oops... POST went wrong";
        });
    };

    $scope.deleteCategory = function (category) {
        $http.delete('/api/Categories/' + category.Id, { 'Id': category.Id })
        .success(function (data, status, headers, config) {
            location.reload();
        }).error(function (data, status, headers, config) {
            $scope.error = "Oops... DELETE went wrong";
        });
    };

    $scope.addProduct = function (name, description, price, category) {
        $http.post('/api/Products/', { 'Name': name, 'Description': description, 'Price': price, 'CategoryId': category.Id })
        .success(function (data, status, headers, config) {
            location.reload();
        }).error(function (data, status, headers, config) {
            $scope.error = "Oops... POST went wrong";
        });

    };

    $scope.deleteProduct = function (data) {
        $http.delete('/api/Products/' + data.Id)
        .success(function (data, status, headers, config) {
            location.reload();
        }).error(function (data, status, headers, config) {
            $scope.error = "Oops... DELETE went wrong";
        });

    };

    $scope.editProduct = function (data) {
        $scope.isEdit[data.Id] = true;        
        $scope.Name = data.Name;
        $scope.Description = data.Description;
        $scope.Price = data.Price;

        saveState = data;
    };

    $scope.updateProduct = function (prod, Name, Description, Price, categoryId) {      
        $http.put('/api/Products/' + prod.Id, { 'Id': prod.Id, 'Name': Name, 'Description': Description, 'Price': Price, 'CategoryId': categoryId })
        .success(function (data, status, headers, config) {
            $scope.isEdit[data.Id] = false;
            location.reload();
        }).error(function (data, status, headers, config) {
            $scope.error = "Oops... PUT went wrong";
        });
    };


    $scope.cancelProduct = function (data) {
        data = saveState;
        $scope.isEdit[data.Id] = false;
    };


});

