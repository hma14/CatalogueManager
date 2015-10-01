$(function () {
    LoadAllCategories();
});

function LoadAllCategories() {
    
    var scope = angular.element('[ng-controller="CatalogueCtrl"]').scope();
    scope.$apply(function () {
        scope.getCategory();
    });
}



angular.module('CatalogueApp', [])
.controller('CatalogueCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.categories = [];
    $scope.isEdit = [];
    $scope.error = '';
    var saveState = {};
    var urlCategory = '/api/Categories/';
    var urlProduct = '/api/Products/';

    // Common function for GET
    $scope.get = function (url) {
        $http.get(url).success(function (data, status, headers, config) {
            $scope.categories = data;
            $scope.error = '';
        }).error(function (data, status, headers, config) {
            $scope.error = "Oops... GET went wrong: " + data.error + ", " + status;
        });
    };

    $scope.getCategory = function () {
        $scope.get(urlCategory);
    }

    $scope.getProduct = function () {
        $scope.get(urlProduct);
    }

    $scope.addCategory = function (categoryName, category) {
        var id = 0;
        if (category != null)
        {
            id = category.Id;
        }
        $http.post(urlCategory, { 'categoryName': categoryName, 'parentCategoryId': id })
        .success(function (data, status, headers, config) {
            location.reload();
        }).error(function (data, status, headers, config) {
            $scope.error = "Oops... POST went wrong: " + status;
        });
    };

    // Common function for DELETE
    $scope.delete = function (url, obj) {
        $http.delete(url + obj.Id, { 'Id': obj.Id })
        .success(function (data, status, headers, config) {
            location.reload();
        }).error(function (data, status, headers, config) {
            $scope.error = "Oops... DELETE went wrong: " + status;
        });
    };

    $scope.deleteCategory = function (category) {
        $scope.delete(urlCategory, category);
    };

    $scope.deleteProduct = function (product) {
        $scope.delete(urlProduct, product);
    }

    $scope.addProduct = function (name, description, price, category) {
        $http.post(urlProduct, { 'Name': name, 'Description': description, 'Price': price, 'CategoryId': category.Id })
        .success(function (data, status, headers, config) {
            location.reload();
        }).error(function (data, status, headers, config) {
            $scope.error = "Oops... POST went wrong: " + status;
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
        $http.put(urlProduct + prod.Id, { 'Id': prod.Id, 'Name': Name, 'Description': Description, 'Price': Price, 'CategoryId': categoryId })
        .success(function (data, status, headers, config) {
            $scope.isEdit[data.Id] = false;
            location.reload();
        }).error(function (data, status, headers, config) {
            $scope.error = "Oops... PUT went wrong: " + status;
        });
    };


    $scope.cancelProduct = function (data) {
        data = saveState;
        $scope.isEdit[data.Id] = false;
    };


}]);

