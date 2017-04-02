(function() {
	'use strict';
	angular
		.module('OwnersAndPetsApp')
		.factory('Owners', Owners);
	Owners.$inject = ['$resource'];
	function Owners($resource) {
		var resourceUrl = 'api/owners/:ownerId';
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
						data = angular.fromJson(data).Item2;
					}
					return {count: data};
				}
			},
			'update': { method: 'PUT' }
		});
	}
})();