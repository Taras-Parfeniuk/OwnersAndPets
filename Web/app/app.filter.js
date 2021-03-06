﻿(function () {
	'use strict';

	var app = angular.module('OwnersAndPetsApp');

	app.filter('range', function () {
		return function (input, total) {
			total = parseInt(total);

			for (var i = 1; i <= total; i++) {
				input.push(i);
			}

			return input;
		};
	});
})()