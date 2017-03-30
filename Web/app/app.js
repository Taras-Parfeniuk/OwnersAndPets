(function () {
	'use strict'

	var app = angular.module('OwnersAndPetsApp', ['ngResource', 'ngRoute', 'ui.router']);
	app.run(run);

	run.$inject = ['$state'];
	function run($state) {
		$state.go('owners');
	}
})();