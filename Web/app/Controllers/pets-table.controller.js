(function () {
	'use strict';
	angular
		.module('OwnersAndPetsApp')
		.controller('PetsTableController', PetsTableController);
	PetsTableController.$inject = ['$scope', '$state', '$stateParams', 'Pets'];

	function PetsTableController($scope, $state, $stateParams, Pets) {
		var ctrl = this;

		ctrl.page = { ownerId: $stateParams.ownerId, id: $stateParams.page, per_page: $stateParams.per_page };
		ctrl.pets = Pets.query({ ownerId: ctrl.page.ownerId, page: ctrl.page.id, per_page: ctrl.page.per_page });
		ctrl.pages = Pets.get({ page: ctrl.page.id, per_page: ctrl.page.per_page });
		ctrl.save = save;
		ctrl.remove = remove;

		function save() {
			Pets.save({ ownerId: ctrl.page.ownerId }, { Name: ctrl.name, OwnerId: ctrl.page.ownerId }, onSuccess, onError);
		}

		function remove(pet) {
			Pets.delete({ ownerId: ctrl.page.ownerId, petId: pet.Id }, onSuccess, onError);
		}

		function onSuccess(result) {
			ctrl.pets = Pets.query({ ownerId: ctrl.page.ownerId, page: ctrl.page.id, per_page: ctrl.page.per_page });
			ctrl.pages = Pets.get({ page: ctrl.page.id, per_page: ctrl.page.per_page });
		}

		function onError() {

		}
	}
})();