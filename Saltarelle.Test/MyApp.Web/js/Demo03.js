(function() {
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Demo03.Address
	var $Saltarelle_$Test_Demo03$Address = function() {
	};
	$Saltarelle_$Test_Demo03$Address.createInstance = function() {
		return $Saltarelle_$Test_Demo03$Address.$ctor();
	};
	$Saltarelle_$Test_Demo03$Address.$ctor = function() {
		var $this = {};
		$this.city = null;
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Demo03.Car
	var $Saltarelle_$Test_Demo03$Car = function() {
	};
	$Saltarelle_$Test_Demo03$Car.createInstance = function() {
		return $Saltarelle_$Test_Demo03$Car.$ctor();
	};
	$Saltarelle_$Test_Demo03$Car.$ctor = function() {
		var $this = {};
		$this.quantity = 0;
		$this.price = 0;
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Demo03.Person
	var $Saltarelle_$Test_Demo03$Person = function() {
	};
	$Saltarelle_$Test_Demo03$Person.createInstance = function() {
		return $Saltarelle_$Test_Demo03$Person.$ctor();
	};
	$Saltarelle_$Test_Demo03$Person.$ctor = function() {
		var $this = {};
		$this.firstName = null;
		$this.lastName = null;
		$this.cars = null;
		$this.address = null;
		$this.roleColor = null;
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Demo03
	var $Saltarelle_Test_Demo03 = function() {
		this.$person = null;
	};
	$Saltarelle_Test_Demo03.prototype = {
		$attach: function() {
			$(Function.mkdel(this, function() {
				this.$person = this.$createPerson();
				$.views.helpers({ infoSummary: this.get_$infoSummary() });
				$.views.converters({ upper: this.get_$upper() });
				$('#myLinkedContent').link(true, this.$person);
				$('#buttonSetNameAndCityAndColorAndCars').click(Function.mkdel(this, function(evt) {
					this.$setNameAndCityAndColorAndCars();
				}));
				$('#buttonSetCity').click(Function.mkdel(this, function(evt1) {
					this.$setCity();
				}));
			}));
		},
		$setNameAndCityAndColorAndCars: function() {
			var values = {};
			values['cars.quantity'] = this.$person.cars.quantity + 1;
			values['cars.price'] = this.$person.cars.price - 3;
			values['lastName'] = this.$person.lastName + 'Plus';
			values['address.city'] = this.$person.address.city + 'More';
			values['roleColor'] = ((this.$person.roleColor === 'yellow') ? '#8DD' : 'yellow');
			$.observable(this.$person).setProperty(values);
		},
		$setCity: function() {
			$.observable(this.$person).setProperty('address.city', this.$person.address.city + 'Add');
		},
		get_$infoSummary: function() {
			var info = function(firstName, lastName, city, cars, html) {
				return (!String.isNullOrEmpty(html) ? ('<b>' + firstName + ' ' + lastName + '</b> lives in <i>' + city + '</i> and owns ' + cars.toString() + ' cars.') : (firstName + ' ' + lastName + ' lives in ' + city + ' and owns ' + cars.toString() + ' cars.'));
			};
			return info;
		},
		get_$upper: function() {
			var upper = function(val) {
				return val.toUpperCase();
			};
			return upper;
		},
		$createPerson: function() {
			var $t1 = $Saltarelle_$Test_Demo03$Person.$ctor();
			$t1.firstName = 'Jo';
			$t1.lastName = 'Proctor';
			var $t2 = $Saltarelle_$Test_Demo03$Car.$ctor();
			$t2.quantity = 13;
			$t2.price = 150;
			$t1.cars = $t2;
			var $t3 = $Saltarelle_$Test_Demo03$Address.$ctor();
			$t3.city = 'Redmon';
			$t1.address = $t3;
			$t1.roleColor = 'yellow';
			return $t1;
		}
	};
	$Saltarelle_Test_Demo03.main = function() {
		var $t1 = new $Saltarelle_Test_Demo03();
		$(Function.mkdel($t1, $t1.$attach));
	};
	Type.registerClass(null, 'Saltarelle.$Test.Demo03$Address', $Saltarelle_$Test_Demo03$Address, Object);
	Type.registerClass(null, 'Saltarelle.$Test.Demo03$Car', $Saltarelle_$Test_Demo03$Car, Object);
	Type.registerClass(null, 'Saltarelle.$Test.Demo03$Person', $Saltarelle_$Test_Demo03$Person, Object);
	Type.registerClass(global, 'Saltarelle.Test.Demo03', $Saltarelle_Test_Demo03, Object);
	$Saltarelle_Test_Demo03.main();
})();
