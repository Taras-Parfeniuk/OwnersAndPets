(function () {
	'use strict';
	angular
		.module('OwnersAndPetsApp')
		.config(stateConfig);
	stateConfig.$inject = ['$stateProvider'];

	function stateConfig($stateProvider) {
		$stateProvider
			.state('owners', {
				url: 'Owners',
				templateUrl: 'app/Views/owners.html',
				controller: 'OwnersTableController',
				controllerAs: 'ctrl',

				params: {
					page: 1,
					per_page: 3
				}
			})
			.state('pets', {
				parrent: 'owners',
				url: 'Owners/{ownerId}',
				templateUrl: 'app/Views/pets.html',
				controller: 'PetsTableController',
				controllerAs: 'ctrl',

				params: {
					page: 1,
					per_page: 3
				}
			});
	}
})();