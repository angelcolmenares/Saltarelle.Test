(function() {
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.TODOSDemo.Item
	var $Saltarelle_$Test_TODOSDemo$Item = function() {
	};
	$Saltarelle_$Test_TODOSDemo$Item.createInstance = function() {
		return $Saltarelle_$Test_TODOSDemo$Item.$ctor();
	};
	$Saltarelle_$Test_TODOSDemo$Item.$ctor = function() {
		var $this = {};
		$this.content = null;
		$this.done = false;
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.TODOSDemo.Templates
	var $Saltarelle_$Test_TODOSDemo$Templates = function() {
	};
	$Saltarelle_$Test_TODOSDemo$Templates.createInstance = function() {
		return $Saltarelle_$Test_TODOSDemo$Templates.$ctor();
	};
	$Saltarelle_$Test_TODOSDemo$Templates.$ctor = function() {
		var $this = {};
		$this.itemTemplate = null;
		$this.editTemplate = null;
		$this.itemTemplate = '#item-template';
		$this.editTemplate = '#edit-template';
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.TODOSDemo.Todos
	var $Saltarelle_$Test_TODOSDemo$Todos = function() {
	};
	$Saltarelle_$Test_TODOSDemo$Todos.createInstance = function() {
		return $Saltarelle_$Test_TODOSDemo$Todos.$ctor();
	};
	$Saltarelle_$Test_TODOSDemo$Todos.$edit = function($this, view) {
		//editing = view;
		view.tmpl = 'editTemplate';
		view.refresh();
		$(view.contents('input', true)).focus();
	};
	$Saltarelle_$Test_TODOSDemo$Todos.$removeItem = function($this, index, item) {
		$.observable($this.items).remove(index, 1);
		$Saltarelle_$Test_TODOSDemo$Todos.$doneChanged($this, (item.done ? -1 : 0));
	};
	$Saltarelle_$Test_TODOSDemo$Todos.$doneChanged = function($this, incr) {
		var completed = $this.completed + incr;
		var values = {};
		values['completed'] = completed;
		values['remaining'] = $this.items.length - completed;
		$.observable($this).setProperty(values);
	};
	$Saltarelle_$Test_TODOSDemo$Todos.$contentChanged = function($this, view) {
		view.tmpl = 'itemTemplate';
		view.refresh();
	};
	$Saltarelle_$Test_TODOSDemo$Todos.$clearCompleted = function($this) {
		var l = $this.items.length;
		while (--l >= 0) {
			if ($this.items[l].done) {
				$Saltarelle_$Test_TODOSDemo$Todos.$removeItem($this, l, $this.items[l]);
			}
		}
	};
	$Saltarelle_$Test_TODOSDemo$Todos.$ctor = function() {
		var $this = {};
		$this.items = null;
		$this.completed = 0;
		$this.remaining = 0;
		return $this;
	};
	////////////////////////////////////////////////////////////////////////////////
	// Saltarelle.Test.TODOSDemo
	var $Saltarelle_Test_TODOSDemo = function() {
		this.$todos = null;
		this.$useStorage = false;
		this.$localStorage = null;
	};
	$Saltarelle_Test_TODOSDemo.prototype = {
		$attach: function() {
			$(Function.mkdel(this, function() {
				this.$start();
				$.views.helpers({ remainingMessage: this.get_$remainingMessage(), completedMessage: this.get_$completedMessage(), onAfterChange: this.get_$onAfterChange() });
				$.templates($Saltarelle_$Test_TODOSDemo$Templates.$ctor());
				$('#new-todo').keypress(Function.mkdel(this, function(ev) {
					if (ev.which === 13) {
						var $t2 = $.observable(this.$todos.items);
						var $t3 = this.$todos.items.length;
						var $t1 = $Saltarelle_$Test_TODOSDemo$Item.$ctor();
						$t1.content = ev.currentTarget.value;
						$t1.done = false;
						$t2.insert($t3, $t1);
						$Saltarelle_$Test_TODOSDemo$Todos.$doneChanged(this.$todos, 0);
						ev.currentTarget.value = '';
					}
				}));
				$('.todo-clear').click(Function.mkdel(this, function(evt) {
					$Saltarelle_$Test_TODOSDemo$Todos.$clearCompleted(this.$todos);
				}));
				$('#todo-stats').link(true, this.$todos);
				$.link.itemTemplate('#todo-list', this.$todos.items).on('click', '.todo-destroy', Function.mkdel(this, function(ev1) {
					var view = $.view(ev1.currentTarget);
					$Saltarelle_$Test_TODOSDemo$Todos.$removeItem(this.$todos, view.index, view.data);
				})).on('dblclick', 'li', Function.mkdel(this, function(ev2) {
					$Saltarelle_$Test_TODOSDemo$Todos.$edit(this.$todos, $.view(ev2.currentTarget));
				})).on('keypress', 'input', function(ev3) {
					if (ev3.which === 13) {
						ev3.currentTarget.blur();
					}
				});
			}));
		},
		$start: function() {
			this.$useStorage = true;
			this.$localStorage = window.localStorage;
			this.$todos = $Saltarelle_$Test_TODOSDemo$Todos.$ctor();
			var t = this.$localStorage.getItem('JsViewsTodos');
			this.$todos.items = (ss.isValue(t) ? JSON.parse(t.toString()) : []);
			this.$todos.completed = Enumerable.from(this.$todos.items).count(function(f) {
				return f.done;
			});
			this.$todos.remaining = this.$todos.items.length - this.$todos.completed;
		},
		get_$remainingMessage: function() {
			var rm = function(remaining) {
				return ((remaining > 0) ? (remaining + ' item' + ((remaining > 1) ? 's' : '') + ' left') : '');
			};
			return rm;
		},
		get_$completedMessage: function() {
			var cm = function(completed) {
				return ((completed > 0) ? ('Clear ' + completed + ' completed item' + ((completed > 1) ? 's' : '')) : '');
			};
			return cm;
		},
		get_$onAfterChange: function() {
			var ac = Function.mkdel(this, function(ev) {
				switch (ev.type) {
					case 'change': {
						var view = $.view(ev.target);
						switch (ev.target.className) {
							case 'check': {
								$Saltarelle_$Test_TODOSDemo$Todos.$doneChanged(this.$todos, (view.data.done ? 1 : -1));
								break;
							}
							case 'todo-input': {
								$Saltarelle_$Test_TODOSDemo$Todos.$contentChanged(this.$todos, view);
								break;
							}
						}
						if (this.$useStorage) {
							this.$localStorage.setItem('JsViewsTodos', JSON.stringify(this.$todos.items));
						}
						break;
					}
				}
			});
			return ac;
		}
	};
	$Saltarelle_Test_TODOSDemo.main = function() {
		var $t1 = new $Saltarelle_Test_TODOSDemo();
		$(Function.mkdel($t1, $t1.$attach));
	};
	Type.registerClass(null, 'Saltarelle.$Test.TODOSDemo$Item', $Saltarelle_$Test_TODOSDemo$Item, Object);
	Type.registerClass(null, 'Saltarelle.$Test.TODOSDemo$Templates', $Saltarelle_$Test_TODOSDemo$Templates, Object);
	Type.registerClass(null, 'Saltarelle.$Test.TODOSDemo$Todos', $Saltarelle_$Test_TODOSDemo$Todos, Object);
	Type.registerClass(global, 'Saltarelle.Test.TODOSDemo', $Saltarelle_Test_TODOSDemo, Object);
	$Saltarelle_Test_TODOSDemo.main();
})();
