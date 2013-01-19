(function() {
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Demo04.Currency
	var $Saltarelle_$Test_Demo04$Currency = function() {
	};
	$Saltarelle_$Test_Demo04$Currency.createInstance = function() {
		return $Saltarelle_$Test_Demo04$Currency.$ctor();
	};
	$Saltarelle_$Test_Demo04$Currency.$ctor = function() {
		var $this = {};
		$this.name = null;
		$this.label = null;
		$this.rate = 0;
		$this.symbol = null;
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Demo04.Movie
	var $Saltarelle_$Test_Demo04$Movie = function() {
	};
	$Saltarelle_$Test_Demo04$Movie.createInstance = function() {
		return $Saltarelle_$Test_Demo04$Movie.$ctor();
	};
	$Saltarelle_$Test_Demo04$Movie.$ctor = function() {
		var $this = {};
		$this.title = null;
		$this.ticketPrice = 0;
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Demo04.OrderDetails
	var $Saltarelle_$Test_Demo04$OrderDetails = function() {
	};
	$Saltarelle_$Test_Demo04$OrderDetails.createInstance = function() {
		return $Saltarelle_$Test_Demo04$OrderDetails.$ctor();
	};
	$Saltarelle_$Test_Demo04$OrderDetails.$ctor = function() {
		var $this = {};
		$this.name = null;
		$this.selectedMovie = 0;
		$this.selectedCurrency = 0;
		$this.request = null;
		$this.selectedMovie = -1;
		$this.selectedCurrency = 1;
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Demo04
	var $Saltarelle_Test_Demo04 = function() {
		this.$currencies = null;
		this.$movies = null;
		this.$orderDetails = null;
	};
	$Saltarelle_Test_Demo04.prototype = {
		$attach: function() {
			$(Function.mkdel(this, function() {
				this.$populateCurrencies();
				this.$populateMovies();
				this.$createOrderDetails();
				var contextHelpers = {};
				contextHelpers['app'] = {};
				contextHelpers['movies'] = this.$movies;
				contextHelpers['convertedPrice'] = this.get_$convertedPrice();
				contextHelpers['currencies'] = this.$currencies;
				contextHelpers['selectedTitle'] = this.get_$selectedTitle();
				$.templates('moviePurchaseTemplate', '#moviePurchaseTemplate');
				$.link.moviePurchaseTemplate('#moviePurchase', this.$orderDetails, contextHelpers);
				$('#submitOrder').click(Function.mkdel(this, function(evt) {
					window.alert('You ordered a movie ticket as follows:\n' + JSON.stringify(this.$orderDetails));
				}));
			}));
		},
		get_$convertedPrice: function() {
			var cp = Function.mkdel(this, function(selectedMovie, selectedCurrency) {
				var currency = this.$currencies[selectedCurrency];
				return ((selectedMovie !== -1) ? (currency.symbol + (this.$movies[selectedMovie].ticketPrice * currency.rate).toString()) : '');
			});
			return cp;
		},
		get_$selectedTitle: function() {
			var st = Function.mkdel(this, function(selectedMovie) {
				return ((selectedMovie !== -1) ? this.$movies[selectedMovie].title : '');
			});
			return st;
		},
		$populateCurrencies: function() {
			var $t1 = [];
			var $t2 = $Saltarelle_$Test_Demo04$Currency.$ctor();
			$t2.name = 'US';
			$t2.label = 'US Dollar';
			$t2.rate = 1;
			$t2.symbol = '$';
			$t1.add($t2);
			var $t3 = $Saltarelle_$Test_Demo04$Currency.$ctor();
			$t3.name = 'EUR';
			$t3.label = 'Euro';
			$t3.rate = 0.95;
			$t3.symbol = '€';
			$t1.add($t3);
			var $t4 = $Saltarelle_$Test_Demo04$Currency.$ctor();
			$t4.name = 'GB';
			$t4.label = 'Pound';
			$t4.rate = 0.63;
			$t4.symbol = '£';
			$t1.add($t4);
			this.$currencies = $t1;
		},
		$populateMovies: function() {
			var $t1 = [];
			var $t2 = $Saltarelle_$Test_Demo04$Movie.$ctor();
			$t2.title = 'The Red Violin';
			$t2.ticketPrice = 18;
			$t1.add($t2);
			var $t3 = $Saltarelle_$Test_Demo04$Movie.$ctor();
			$t3.title = 'The Inheritance';
			$t3.ticketPrice = 16.5;
			$t1.add($t3);
			var $t4 = $Saltarelle_$Test_Demo04$Movie.$ctor();
			$t4.title = 'The Incredible Hulk';
			$t4.ticketPrice = 21.99;
			$t1.add($t4);
			this.$movies = $t1;
		},
		$createOrderDetails: function() {
			this.$orderDetails = $Saltarelle_$Test_Demo04$OrderDetails.$ctor();
		}
	};
	$Saltarelle_Test_Demo04.main = function() {
		var $t1 = new $Saltarelle_Test_Demo04();
		$(Function.mkdel($t1, $t1.$attach));
	};
	Type.registerClass(null, 'Saltarelle.$Test.Demo04$Currency', $Saltarelle_$Test_Demo04$Currency, Object);
	Type.registerClass(null, 'Saltarelle.$Test.Demo04$Movie', $Saltarelle_$Test_Demo04$Movie, Object);
	Type.registerClass(null, 'Saltarelle.$Test.Demo04$OrderDetails', $Saltarelle_$Test_Demo04$OrderDetails, Object);
	Type.registerClass(global, 'Saltarelle.Test.Demo04', $Saltarelle_Test_Demo04, Object);
	$Saltarelle_Test_Demo04.main();
})();
