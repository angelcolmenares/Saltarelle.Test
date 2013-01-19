using System;
using System.Linq;
using jQueryApi;
using System.Html;
using System.Collections.Generic;
using System.Html.Data;
using System.Serialization;

namespace Saltarelle.Test
{
	public class TODOSDemo
	{
		Todos todos;
		bool useStorage;
		Storage localStorage;

		public static void Main()
		{
			jQuery.OnDocumentReady(new TODOSDemo().Attach);
		}
		
		void Attach() {
			jQuery.OnDocumentReady(() => {
				Start();
				Observable.ViewsHelpers(new {
					remainingMessage=RemainingMessage,
					completedMessage=CompletedMessage,
					onAfterChange=OnAfterChange
				});

				Observable.Templates(new Templates());

				jQuery.Select( "#new-todo" ).Keypress(  ev=>{

					if(ev.Which==13){
						Observable.ToArrayObservable(todos.Items)
							.Insert( todos.Items.Count, new Item{ Content= ((InputElement)ev.CurrentTarget).Value , Done= false });
						todos.DoneChanged( 0 );
						((InputElement)ev.CurrentTarget).Value="";

					}

				} ); 

				jQuery.Select( ".todo-clear" ).Click( evt=> {
					todos.ClearCompleted();
				});

				jQuery.Select( "#todo-stats" ).Link( true, todos );


				Observable.Link("itemTemplate","#todo-list", todos.Items )
					.On( "click", ".todo-destroy", ev=> {
						var view = Observable.View<Item>(ev.CurrentTarget);
						todos.RemoveItem( view.Index, view.Data );
					})
					.On( "dblclick", "li",  ev => {
							todos.Edit( Observable.View<Item>(ev.CurrentTarget));
						})
					.On( "keypress", "input", ev => {
							if ( ev.Which == 13 ) {
								ev.CurrentTarget.Blur();
							}
					});

			});

		}

		void Start(){
			useStorage=true;
			localStorage= Window.LocalStorage;

			todos= new Todos();
			var t = localStorage.GetItem("JsViewsTodos");

			todos.Items=  t!=null? Json.Parse<List<Item>>(t.ToString()): new List<Item>(); 
			todos.Completed= todos.Items.Count(f=>f.Done);
			todos.Remaining= todos.Items.Count- todos.Completed;

		}


		Func<int,string> RemainingMessage {
			get{
				Func<int,string> rm = (remaining)=>{
					return remaining>0 ? ( remaining + " item" + ( remaining > 1 ? "s" : "" ) + " left" ) : "";
				};
				return rm;
			}
		}

		Func<int,string> CompletedMessage {
			get{
				Func<int,string> cm = (completed)=>{
					return completed>0 ? ( "Clear " + completed + " completed item" + ( completed > 1 ? "s" : "" )) : "";
				};
				return cm;
			}
		}

		Action<jQueryEvent> OnAfterChange {
			get
			{
				Action<jQueryEvent> ac = ev=>{
					switch( ev.Type ) {
					case "change":
						var view = Observable.View<Item>(ev.Target);
						switch(ev.Target.ClassName) {
						case "check":
							todos.DoneChanged( view.Data.Done ? 1 : -1 );
							break;
						case "todo-input":
							todos.ContentChanged( view );
							break;
						}
						//break;
					//case "arrayChange":
						if ( useStorage ) {
							localStorage.SetItem( "JsViewsTodos", Json.Stringify( todos.Items) );
						}
						break;
					}


				};
				return ac;

			}
		}
			
		[Serializable]
		class Templates{
			
			public Templates(){
				ItemTemplate= "#item-template";
				EditTemplate= "#edit-template";
			}
			
			public string ItemTemplate{
				get;set;
			}
			
			public string EditTemplate{
				get;set;
			}
		}
		




		[Serializable]
		class Todos{

			//JsView editing=null;

			public List<Item> Items {get;set;}

			public int Completed {get;set;}
			public int Remaining {get;set;}

			public void Edit(ObservableEventData<Item> view ) {
				//editing = view;
				view.Tmpl= "editTemplate";
				view.Refresh();
				jQuery.FromObject ( view.Contents("input", true) ).Focus();

			}

		 	public void RemoveItem(int index, Item item ) {
				Observable.ToArrayObservable(Items).Remove( index, 1 );
				DoneChanged( item.Done ? -1 : 0 );
			}

			public void DoneChanged(int incr ) {
				var completed = Completed + incr;
				JsDictionary<string,object> values = new JsDictionary<string,object>();
				values["completed"]=completed;
				values["remaining"]= Items.Count- completed;
				Observable.ToObjectObservable( this ).SetProperty(values);
			}

			public void ContentChanged(ObservableEventData<Item> view ) {
				view.Tmpl = "itemTemplate";
				view.Refresh();
			}

			public void ClearCompleted( ) {
				var l = Items.Count;
				while ( --l >=0) {
					if ( Items [ l ].Done) {
						RemoveItem( l, Items[ l ]);
					}
				}
			}

		}

		[Serializable]
		class Item{
			public string Content {get;set;}
			public bool Done {get;set;}
		}


	}
}

