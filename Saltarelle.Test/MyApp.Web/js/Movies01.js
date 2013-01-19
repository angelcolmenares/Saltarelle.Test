(function() {
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Movies01.Movie
	var $Saltarelle_$Test_Movies01$Movie = function() {
	};
	$Saltarelle_$Test_Movies01$Movie.createInstance = function() {
		return $Saltarelle_$Test_Movies01$Movie.$ctor();
	};
	$Saltarelle_$Test_Movies01$Movie.$ctor = function() {
		var $this = {};
		$this.name = null;
		$this.releaseYear = null;
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Movies01.Templates
	var $Saltarelle_$Test_Movies01$Templates = function() {
	};
	$Saltarelle_$Test_Movies01$Templates.createInstance = function() {
		return $Saltarelle_$Test_Movies01$Templates.$ctor();
	};
	$Saltarelle_$Test_Movies01$Templates.$ctor = function() {
		var $this = {};
		$this.MovieTemplate = null;
		$this.TextOnlyTemplate = null;
		$this.MovieTemplate = '#movieTemplate';
		$this.TextOnlyTemplate = '#textOnlyTemplate';
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Movies01
	var $Saltarelle_Test_Movies01 = function() {
		this.$movies = null;
		this.$count = 0;
	};
	$Saltarelle_Test_Movies01.prototype = {
		$attach: function() {
			$(Function.mkdel(this, function() {
				this.$populateMovies();
				var templates = $Saltarelle_$Test_Movies01$Templates.$ctor();
				$.templates(templates);
				this.$renderMovieTemplate();
				this.$linkTextOnlyTemplate();
				this.$linkMovieTemplate();
				$('#buttonAddMovie').click(Function.mkdel(this, this.$addMovie));
				$('#buttonRemoveLastMovie').click(Function.mkdel(this, this.$removeLastMovie));
			}));
		},
		$addMovie: function(evt) {
			var $t2 = $.observable(this.$movies);
			var $t3 = this.$movies.length;
			var $t1 = $Saltarelle_$Test_Movies01$Movie.$ctor();
			$t1.name = 'new movie ' + (++this.$count).toString();
			$t1.releaseYear = 'YYYY';
			$t2.insert($t3, $t1);
		},
		$removeLastMovie: function(evt) {
			$.observable(this.$movies).remove(this.$movies.length - 1, 1);
		},
		$populateMovies: function() {
			this.$count = 0;
			this.$movies = [];
			var $t2 = this.$movies;
			var $t1 = $Saltarelle_$Test_Movies01$Movie.$ctor();
			$t1.name = 'The Red Violin';
			$t1.releaseYear = '1998';
			$t2.add($t1);
			var $t4 = this.$movies;
			var $t3 = $Saltarelle_$Test_Movies01$Movie.$ctor();
			$t3.name = 'Eyes Wide Shut';
			$t3.releaseYear = '1999';
			$t4.add($t3);
			var $t6 = this.$movies;
			var $t5 = $Saltarelle_$Test_Movies01$Movie.$ctor();
			$t5.name = 'The Inheritance';
			$t5.releaseYear = '1976';
			$t6.add($t5);
		},
		$renderMovieTemplate: function() {
			//"$.render.MovieTemplate(movies)"
			$('#renderedListContainer').html($.render.MovieTemplate(this.$movies));
		},
		$linkTextOnlyTemplate: function() {
			//$.link.TextOnlyTemplate(".linkedListPlaceholder" , movies, { target: "replace" });
			$.link.TextOnlyTemplate('.linkedListPlaceholder', this.$movies, { target: 'replace' });
		},
		$linkMovieTemplate: function() {
			$.link.MovieTemplate('#linkedListContainer', this.$movies);
		}
	};
	$Saltarelle_Test_Movies01.main = function() {
		var $t1 = new $Saltarelle_Test_Movies01();
		$(Function.mkdel($t1, $t1.$attach));
	};
	Type.registerClass(null, 'Saltarelle.$Test.Movies01$Movie', $Saltarelle_$Test_Movies01$Movie, Object);
	Type.registerClass(null, 'Saltarelle.$Test.Movies01$Templates', $Saltarelle_$Test_Movies01$Templates, Object);
	Type.registerClass(global, 'Saltarelle.Test.Movies01', $Saltarelle_Test_Movies01, Object);
	$Saltarelle_Test_Movies01.main();
})();
