angular.module('quoteRequest')
    .controller('quoteController', ["$http", "$scope","filterFilter", function ($http, $scope, filterFilter) {
		var vm = this;

		vm.init = init;
        vm.requestQuote = requestQuote;

		function init() {

			vm.quoteRequest = {
				broker: undefined,
				insuredName: undefined,
				trade: undefined,
				covers: [],
				revenue: undefined,
				numberOfEmployees: undefined
			};
            var url = "api/quote/trades";
            $http.get(url)
                .then(function (data, status, headers, config) {
                    console.log(data);
                    vm.trades = data.data;
                    vm.coverValues = ["500k", "1M", "5M"];
                    vm.covers = [
                        {
                            id: 1,
                            name: "D&O",
                            selected: false,
                            value: undefined
                        },
                        {
                            id: 2,
                            name: "PI",
                            selected: false,
                            value: undefined
                        },
                        {
                            id: 3,
                            name: "EL",
                            selected: false,
                            value: undefined
                        }];

                });
        }

        // Helper method to get selected covers
        vm.selectedCovers = function selectedCovers() {
            return filterFilter(vm.covers, { selected: true });
        };

        function requestQuote() {
            vm.quoteRequest.covers = filterFilter(vm.covers, { selected: true });
            var url = "api/quote/request";
            var data = JSON.stringify(vm.quoteRequest);
            var headers = {
                'Content-Type': 'application/json'
            };

            $http.post(url, data, headers)
            .then(function (data, status, headers, config) {
                console.log(data);
                    alert('Your quote request was successfully submitted via email!');
                });
		}
	}]);