(function() {
	'use strict';
	angular
		.module('OwnersAndPetsApp')
		.factory('Pets', Pets);
	Pets.$inject = ['$resource'];
	function Pets($resource) {
		var resourceUrl = 'api/owners/:ownerId/:petId';
		return $resource(resourceUrl, {}, {
			'query': {
				method: 'GET',
				isArray: true,
				transformResponse: function (data) {
					if (data) {
						data = angular.fromJson(data).Item1;
					}
					return data;
				}
			},
			'get': {
				method: 'GET',
				transformResponse: function (data) {
					if (data) {
						var pages = angular.fromJson(data).Item2;
						var name = angular.fromJson(data).Item3;
					}
					return { count: pages, owner: name };
				}
			},
			'update': { method: 'PUT' }
		});
	}
})();