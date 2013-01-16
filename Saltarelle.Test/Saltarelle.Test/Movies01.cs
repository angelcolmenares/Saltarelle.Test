using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using System.Diagnostics.CodeAnalysis;

namespace Saltarelle.Test
{
	public class Movies01
	{
		List<Movie> movies ;
		int count;

		public static void Main(){
			/*
			List<int> ai = new List<int>();
			var observable= Observable.CreateArrayObservable(ai);
			observable.Insert(0, 2);
			observable.Data();
			
			var t = new Test{Id=1, Name="Billy"};
			
			var to= Observable.CreateObjectObservable(t);
			
			to.Observe("Name", (evt,obj)=>{
				Window.Alert(string.Format("Path:'{0}', OldValue: '{1}'  NewValue: '{2}'", obj.Path, obj.OldValue, obj.Value));
			});
			
			to.SetProperty("Name", "Angel Ignacio");
			to.SetProperty("Name", "Claudia");
			
			to.Unobserve("Name");
			to.SetProperty("Name", "Edgar");
			*/

			jQuery.OnDocumentReady(new Movies01().Attach);


		}

		void Attach() {
			jQuery.OnDocumentReady(() => {
				
				PopulateMovies();			
				TestTemplates templates = new TestTemplates{movieTemplate="#movieTemplate", textOnlyTemplate="#textOnlyTemplate"};
				JsRender.Templates(templates);
				jQuery.Select("#renderedListContainer").Html(MyJsRender.RenderMovies(movies));

				//$.link.textOnlyTemplate( ".linkedListPlaceholder", movies, { target: "replace" });
				MyJsRender.LinkTextOnlyTemplate(".linkedListPlaceholder", movies, new LinkOptions{target="replace"});
				//$.link.movieTemplate( "#linkedListContainer", movies );
				MyJsRender.LinkMovieTemplate("#linkedListContainer", movies );


				jQuery.Select("#buttonAddMovie").Click(AddMovie);
				jQuery.Select("#buttonRemoveLastMovie").Click(RemoveLastMovie);
				
			});
		}

		void AddMovie(jQueryEvent evt) {
			Observable.CreateArrayObservable(movies).Insert(movies.Count, new Movie{name="new movie " + (++count).ToString(), releaseYear="YYYY"});
		}

		void RemoveLastMovie(jQueryEvent evt) {
			Observable.CreateArrayObservable(movies).Remove(movies.Count-1,1);
		}

		void PopulateMovies(){

			count=0;
			movies = new List<Movie>();

			movies.Add(new Movie{ name= "The Red Violin", releaseYear= "1998" });
			movies.Add(new Movie{ name= "Eyes Wide Shut", releaseYear= "1999" });
			movies.Add(new Movie{ name= "The Inheritance", releaseYear= "1976"});
		}
	}

	[Serializable]
	[PreserveMemberCase]
	public class Test{
		public int Id {get;set;}
		public string Name {get;set;}
	}
	
	[Serializable]
	[PreserveMemberCase]
	public class TestTemplates{

		public string movieTemplate {get;set;}
		public string textOnlyTemplate {get;set;}
	}

	[Serializable]
	[PreserveMemberCase]
	public class Movie{
		public string name {get;set;}
		public string releaseYear {get;set;}
	}

	[Serializable]
	[PreserveMemberCase]
	public class LinkOptions{
		public string target {get;set;}
	}



	[IgnoreNamespace, Imported (IsRealType = true), ScriptName ("$")]
	public static class MyJsRender
	{
		[ScriptAlias ("$.render.movieTemplate")]
		public static string RenderMovies ( List<Movie> movies)
		{
			return null;
		}

		//$.link.textOnlyTemplate( ".linkedListPlaceholder", movies, { target: "replace" });
		[ScriptAlias ("$.link.textOnlyTemplate")]
		public static void LinkTextOnlyTemplate ([SyntaxValidation ("cssSelector")] string selector, List<Movie> movies, LinkOptions options)
		{

		}

		//[InlineCode ("{this}.accordion('option', {optionName}, {value})")]
		//$.link.movieTemplate( "#linkedListContainer", movies );
		[ScriptAlias ("$.link.movieTemplate")]
		//[InlineCode ("$.link.movieTemplate({selector},{movies})")] // it works too
		public static void LinkMovieTemplate ([SyntaxValidation ("cssSelector")] string selector, List<Movie> movies)
		{	
		}
	}


}

