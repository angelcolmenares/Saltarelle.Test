using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using jQueryApi;

namespace Saltarelle.Test
{
	public class Movies02
	{

		List<Movie> movies ;
		int counter;
		SelectedObject<Movie> app;

		public static void Main()
		{
			jQuery.OnDocumentReady(new Movies02().Attach);
		}
		
		void Attach() {
			jQuery.OnDocumentReady(() => {

				PopulateMovies();

				var templates = new Templates();
				Observable.Templates( templates);

				jQuery.Select ( "#movieDetail" )
					.On( "click", "#addLanguageBtn", evt=> {
						var languages = Observable.View<Movie>(evt.CurrentTarget).Data.Languages ; //. $.view( this ).data.languages;
						Observable.ToArrayObservable(languages).Insert( languages.Count, new Language {
							Name= "NewLanguage " + (counter++).ToString()
						});
					})
					.On( "click", ".close", evt=> {
							var view = Observable.View<Language>(evt.CurrentTarget);
							Observable.ToArrayObservable(view.Parent.Data).Remove(view.Index,1);
							// Or equivalently:	$.observable( app.selectedItem.data.languages).remove( view.index, 1 );
							//return false; //??
						});

				jQuery.Select("#addMovieBtn" ).Click(evt=> {
					Observable.ToArrayObservable(movies).Insert(movies.Count,
					new Movie {
						Title= "NewTitle " + counter.ToString() ,
						Languages= new List<Language>()
						{ new Language{Name= "German"} }
					});
					
					// Set selection on the added item
					Select(Observable.View<Movie>("#movieList tr:last"));
				});


				Observable.ToObjectObservable(app).Observe("SelectedItem", (evt,args)=>{
					var selectedView = args.Value;
					if ( selectedView !=null) 
					{
						LinkDetailTmpl( selectedView.Data );
					} 
					else 
					{
						jQuery.Select("#movieDetail").Empty(); 
					}
				});

				Observable.ViewsHelpers(new {app=app, bgColor=BgColor});

				LinkMovieTmpl()
					.On("click","tr", evt=>{
						Select( Observable.View<Movie>(evt.CurrentTarget) );
					})
					.On("click",".close", evt=>{
							Select();
							var item=Observable.View<Movie>(evt.CurrentTarget); 
							Observable.ToArrayObservable(movies).Remove( item.Index,1 );
					});

				jQuery.Select("#buttonShowData").Click(evt=>{
					jQuery.Select( "#console" ).Append("<hr/>");
					jQuery.Select( "#console" ).Append( jQuery.Select("#showData").Render(movies) );
				});


				jQuery.Select("#buttonDeleteLastLanguage").Click(evt=>{
					if ( movies.Count>0 ) {
						var languages = movies[ movies.Count - 1 ].Languages;
							Observable.ToArrayObservable(languages ).Remove( languages.Count - 1, 1 );
					}

				});

			});
		}

		Func<int,ObservableEventData<Movie>,string> BgColor{
			get{

				Func<int,ObservableEventData<Movie>,string> bc= (index, selectedItem)=>{ 
					return (selectedItem!=null && selectedItem.Index == index) ? "yellow" : ( (index%2==0) ? "#fdfdfe" : "#efeff2" );
				};
				return  bc;
			}
		}


		void Select( ObservableEventData<Movie> view= null ) {

			if (  app.SelectedItem != view ) {
				Observable.ToObjectObservable(app).SetProperty("SelectedItem", view);
			}
		}

		void PopulateMovies(){
			counter=0;
			app = new SelectedObject<Movie>();

			movies = new List<Movie>(){
				new Movie{Title="Meet Joe Black", Languages= new List<Language>(){ 
						new Language{Name= "English"}, new Language{Name= "French"} }},
				new Movie{Title="Eyes Wide Shut", Languages= new List<Language>()
						{ new Language{Name= "German"},
							new Language{Name= 	"French"},
						new Language{Name= "Spanish"}} }
			};
		}


		//[InlineCode ("$.link.MovieTmpl('#movieList',{movies})")] 
		jQueryObject LinkMovieTmpl ()
		{
			return Observable.Link("MovieTmpl","#movieList", movies );
		}

		//[InlineCode ("$.link.DetailTmpl('#movieDetail',{movie})")] 
		jQueryObject LinkDetailTmpl (SelectedObject<Movie> movie)
		{
			return Observable.Link("DetailTmpl","#movieDetail", movie );
		}

		[Serializable]
		[PreserveMemberCase]
		class  Templates{
			
			public Templates(){
				MovieTmpl="#movieTemplate";
				DetailTmpl="#detailViewTemplate";
			}
			
			public string MovieTmpl{
				get;set;
			}
			
			public string DetailTmpl{
				get;set;
			}
		}


		[Serializable]
		class Movie{
			public Movie(){
				Languages= new List<Language>();
			}
			public string Title {get;set;}
			public IList<Language> Languages {get;set;}

		}

		[Serializable]
		class Language{
			public string Name {get;set;}
		}

	}
	
}