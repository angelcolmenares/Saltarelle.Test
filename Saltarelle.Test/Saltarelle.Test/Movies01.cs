using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using jQueryApi;

namespace Saltarelle.Test
{
	public class Movies01
	{
		List<Movie> movies ;
		int count;

		public static void Main()
		{
			jQuery.OnDocumentReady(new Movies01().Attach);
		}

		void Attach() {
			jQuery.OnDocumentReady(() => {
				
				PopulateMovies();			
				Templates templates = new Templates();
				Observable.Templates(templates);
				RenderMovieTemplate();
				LinkTextOnlyTemplate();
				LinkMovieTemplate();

				jQuery.Select("#buttonAddMovie").Click(AddMovie);
				jQuery.Select("#buttonRemoveLastMovie").Click(RemoveLastMovie);
				
			});
		}

		void AddMovie(jQueryEvent evt) {
			Observable.ToArrayObservable(movies)
				.Insert(movies.Count, 
				        new Movie{Name="new movie " + (++count).ToString(), ReleaseYear="YYYY"});
		}

		void RemoveLastMovie(jQueryEvent evt) {
			Observable.ToArrayObservable(movies).Remove(movies.Count-1,1);
		}

		void PopulateMovies(){

			count=0;
			movies = new List<Movie>();

			movies.Add(new Movie{ Name= "The Red Violin", ReleaseYear= "1998" });
			movies.Add(new Movie{ Name= "Eyes Wide Shut", ReleaseYear= "1999" });
			movies.Add(new Movie{ Name= "The Inheritance", ReleaseYear= "1976"});
		}


		void RenderMovieTemplate ( )
		{
			//"$.render.MovieTemplate(movies)"
			jQuery.Select("#renderedListContainer").Html(Observable.Render("MovieTemplate", movies));
		}


		void LinkTextOnlyTemplate ()
		{
			//$.link.TextOnlyTemplate(".linkedListPlaceholder" , movies, { target: "replace" });
			Observable.Link("TextOnlyTemplate",".linkedListPlaceholder", movies, new LinkOptions{Target="replace"});
		}

		//[InlineCode ("$.link.MovieTemplate({selector},{movies})")] // it works too
		void LinkMovieTemplate ()
		{
			Observable.Link("MovieTemplate","#linkedListContainer", movies);
		}


		[Serializable]
		[PreserveMemberCase]
		class Templates{

			public Templates(){
				MovieTemplate="#movieTemplate";
				TextOnlyTemplate="#textOnlyTemplate";
			}

			public string MovieTemplate {get;set;}
			public string TextOnlyTemplate {get;set;}
		}


		[Serializable]
		class Movie{
			public string Name {get;set;}
			public string ReleaseYear {get;set;}
		}

	}

}