(function() {
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Movies02.Language
	var $Saltarelle_$Test_Movies02$Language = function() {
	};
	$Saltarelle_$Test_Movies02$Language.createInstance = function() {
		return $Saltarelle_$Test_Movies02$Language.$ctor();
	};
	$Saltarelle_$Test_Movies02$Language.$ctor = function() {
		var $this = {};
		$this.name = null;
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Movies02.Movie
	var $Saltarelle_$Test_Movies02$Movie = function() {
	};
	$Saltarelle_$Test_Movies02$Movie.createInstance = function() {
		return $Saltarelle_$Test_Movies02$Movie.$ctor();
	};
	$Saltarelle_$Test_Movies02$Movie.$ctor = function() {
		var $this = {};
		$this.title = null;
		$this.languages = null;
		$this.languages = [];
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Movies02.Templates
	var $Saltarelle_$Test_Movies02$Templates = function() {
	};
	$Saltarelle_$Test_Movies02$Templates.createInstance = function() {
		return $Saltarelle_$Test_Movies02$Templates.$ctor();
	};
	$Saltarelle_$Test_Movies02$Templates.$ctor = function() {
		var $this = {};
		$this.MovieTmpl = null;
		$this.DetailTmpl = null;
		$this.MovieTmpl = '#movieTemplate';
		$this.DetailTmpl = '#detailViewTemplate';
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.Movies02
	var $Saltarelle_Test_Movies02 = function() {
		this.$movies = null;
		this.$counter = 0;
		this.$app = null;
	};
	$Saltarelle_Test_Movies02.prototype = {
		$attach: function() {
			$(Function.mkdel(this, function() {
				this.$populateMovies();
				var templates = $Saltarelle_$Test_Movies02$Templates.$ctor();
				$.templates(templates);
				$('#movieDetail').on('click', '#addLanguageBtn', Function.mkdel(this, function(evt) {
					var languages = $.view(evt.currentTarget).data.languages;
					//. $.view( this ).data.languages;
					var $t2 = $.observable(languages);
					var $t3 = languages.get_count();
					var $t1 = $Saltarelle_$Test_Movies02$Language.$ctor();
					$t1.name = 'NewLanguage ' + (this.$counter++).toString();
					$t2.insert($t3, $t1);
				})).on('click', '.close', function(evt1) {
					var view = $.view(evt1.currentTarget);
					$.observable(view.parent.data).remove(view.index, 1);
					// Or equivalently:	$.observable( app.selectedItem.data.languages).remove( view.index, 1 );
					//return false; //??
				});
				$('#addMovieBtn').click(Function.mkdel(this, function(evt2) {
					var $t7 = $.observable(this.$movies);
					var $t8 = this.$movies.length;
					var $t4 = $Saltarelle_$Test_Movies02$Movie.$ctor();
					$t4.title = 'NewTitle ' + this.$counter.toString();
					var $t5 = [];
					var $t6 = $Saltarelle_$Test_Movies02$Language.$ctor();
					$t6.name = 'German';
					$t5.add($t6);
					$t4.languages = $t5;
					$t7.insert($t8, $t4);
					// Set selection on the added item
					this.$select($.view('#movieList tr:last'));
				}));
				$.observable(this.$app).observe('SelectedItem', Function.mkdel(this, function(evt3, args) {
					var selectedView = args.value;
					if (ss.isValue(selectedView)) {
						this.$linkDetailTmpl(selectedView.data);
					}
					else {
						$('#movieDetail').empty();
					}
				}));
				$.views.helpers({ app: this.$app, bgColor: this.get_$bgColor() });
				this.$linkMovieTmpl().on('click', 'tr', Function.mkdel(this, function(evt4) {
					this.$select($.view(evt4.currentTarget));
				})).on('click', '.close', Function.mkdel(this, function(evt5) {
					this.$select(null);
					var item = $.view(evt5.currentTarget);
					$.observable(this.$movies).remove(item.index, 1);
				}));
				$('#buttonShowData').click(Function.mkdel(this, function(evt6) {
					$('#console').append('<hr/>');
					$('#console').append($('#showData').render(this.$movies));
				}));
				$('#buttonDeleteLastLanguage').click(Function.mkdel(this, function(evt7) {
					if (this.$movies.length > 0) {
						var languages1 = this.$movies[this.$movies.length - 1].languages;
						$.observable(languages1).remove(languages1.get_count() - 1, 1);
					}
				}));
			}));
		},
		get_$bgColor: function() {
			var bc = function(index, selectedItem) {
				return ((ss.isValue(selectedItem) && selectedItem.index === index) ? 'yellow' : ((index % 2 === 0) ? '#fdfdfe' : '#efeff2'));
			};
			return bc;
		},
		$select: function(view) {
			if (!ss.referenceEquals(this.$app.SelectedItem, view)) {
				$.observable(this.$app).setProperty('SelectedItem', view);
			}
		},
		$populateMovies: function() {
			this.$counter = 0;
			this.$app = {};
			var $t1 = [];
			var $t2 = $Saltarelle_$Test_Movies02$Movie.$ctor();
			$t2.title = 'Meet Joe Black';
			var $t3 = [];
			var $t4 = $Saltarelle_$Test_Movies02$Language.$ctor();
			$t4.name = 'English';
			$t3.add($t4);
			var $t5 = $Saltarelle_$Test_Movies02$Language.$ctor();
			$t5.name = 'French';
			$t3.add($t5);
			$t2.languages = $t3;
			$t1.add($t2);
			var $t6 = $Saltarelle_$Test_Movies02$Movie.$ctor();
			$t6.title = 'Eyes Wide Shut';
			var $t7 = [];
			var $t8 = $Saltarelle_$Test_Movies02$Language.$ctor();
			$t8.name = 'German';
			$t7.add($t8);
			var $t9 = $Saltarelle_$Test_Movies02$Language.$ctor();
			$t9.name = 'French';
			$t7.add($t9);
			var $t10 = $Saltarelle_$Test_Movies02$Language.$ctor();
			$t10.name = 'Spanish';
			$t7.add($t10);
			$t6.languages = $t7;
			$t1.add($t6);
			this.$movies = $t1;
		},
		$linkMovieTmpl: function() {
			return $.link.MovieTmpl('#movieList', this.$movies);
		},
		$linkDetailTmpl: function(movie) {
			return $.link.DetailTmpl('#movieDetail', movie);
		}
	};
	$Saltarelle_Test_Movies02.main = function() {
		var $t1 = new $Saltarelle_Test_Movies02();
		$(Function.mkdel($t1, $t1.$attach));
	};
	Type.registerClass(null, 'Saltarelle.$Test.Movies02$Language', $Saltarelle_$Test_Movies02$Language, Object);
	Type.registerClass(null, 'Saltarelle.$Test.Movies02$Movie', $Saltarelle_$Test_Movies02$Movie, Object);
	Type.registerClass(null, 'Saltarelle.$Test.Movies02$Templates', $Saltarelle_$Test_Movies02$Templates, Object);
	Type.registerClass(global, 'Saltarelle.Test.Movies02', $Saltarelle_Test_Movies02, Object);
	$Saltarelle_Test_Movies02.main();
})();
