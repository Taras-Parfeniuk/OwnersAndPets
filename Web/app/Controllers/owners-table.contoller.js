(function () {
	'use strict';
	angular
		.module('OwnersAndPetsApp')
		.controller('OwnersTableController', OwnersTableController);
	OwnersTableController.$inject = ['$scope', '$state', '$stateParams', 'Owners'];

	function OwnersTableController($scope, $state, $stateParams, Owners) {
		var ctrl = this;

		ctrl.page = { id: $stateParams.page, per_page: $stateParams.per_page };
		ctrl.owners = Owners.query({ page: ctrl.page.id, per_page: ctrl.page.per_page });
		ctrl.pages = Owners.get({ page: ctrl.page.id, per_page: ctrl.page.per_page });
		ctrl.save = save;
		ctrl.remove = remove;

		function save() {
			Owners.save({}, ctrl.name, onSuccess, onError);
		}

		function remove(owner) {
			Owners.delete({ ownerId: owner.Id }, onSuccess, onError);
		}

		function onSuccess(result) {
			ctrl.owners = Owners.query({ page: ctrl.page.id, per_page: ctrl.page.per_page });
			ctrl.pages = Owners.get({ page: ctrl.page.id, per_page: ctrl.page.per_page });
		}

		function onError() {

		}
	}
})();