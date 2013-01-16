using System.Collections.Generic;
using jQueryApi;
using MyApp.Modelos;
using System.Serialization;
using System.Html;
using System;
using System.Runtime.CompilerServices;

namespace Saltarelle.Test
{
	/*
	[IgnoreNamespace, Imported (IsRealType = true), ScriptName ("$")]
	[Serializable]
	public class PluginObservable{

		[ScriptAlias("$.fn")]
		public static ArrayObservable<T> Observable<T>(){
			jQuery.Select("#main h2").Click(evt => jQuery.Select("#main p").SlideToggle());
			return null;
		}
	}

	public class ArrayObservable<T>{

	}
*/

	/*

	[IgnoreNamespace, Imported (IsRealType = true), ScriptName ("$")]
	[Serializable]
	public class ArrayObservable<T>{

	}

	static class MyPluginExtensions {
		[InstanceMethodOnFirstArgument]
		public static jQueryObject MyPlugin(this jQueryObject jq) { return null; }
		
		//[InlineCode("{this}.myPlugin('option', 'myOption', {value})")]
		public static jQueryObject MySetPluginOption(this jQueryObject jq, string value) { return null; }
	}
	
	//[ScriptAlias("$.fn")]
	public class MyPluginClass{
		public static void MyPlugin( string opts)
		{

			//if (Script.GetScriptType(opts) == "string" && (string)opts == "option") {
			//	string optionName = Arguments.GetArgument(1);
			//	object optionValue = Arguments.GetArgument(2);
			//	// Set the option
			//}
			//else {
			//// Construct the plugin
			//}

		}
	}

	*/

	public class Program {


		static void Main() {
			jQuery.OnDocumentReady(new Program().Attach);

		}

		 void Attach() {
			jQuery.OnDocumentReady(() => {

				var date = new DateTime(2013,12,31);

				jQuery.Select("#main h2").Click(evt => jQuery.Select("#main p").Html(date.ToString()));

				var person = new Person(){Name="Angel Ignacio Colmenares", Birthday= new DateTime(2013,12,31)};
				jQuery.Select("#main h2").Click(evt => jQuery.Select("#main p").SlideToggle());
				jQuery.Select("#main h3").Click(evt => jQuery.Select("#div-person").Html( string.Format("Name:'{0}' - Birthday:'{1}'", person.Name , person.Birthday)));
				jQuery.Select("#button-get-results").Click(Load);


			});
		}


		void Load(jQueryEvent evt) {
			var table = jQuery.Select("#table-persons");
			table.Hide();
			jQuery.Select("#div-loading-persons").Show();
			var req = jQuery.Ajax(new jQueryAjaxOptions { Url = "/json/persons.json", DataType = "json" });
			req.Success(data => {
				var persons = (List<Person>)data; 
				jQuery.Select("#div-loading-persons").Hide();
				table.Empty();
				table.Append(jQuery.FromHtml("<tr><td>Name</td><td>Profit to Date</td><td>&nbsp;</td></tr>"));
				foreach (var c in persons){
					table.Append(jQuery.FromHtml("<tr data-customer=\"" + Json.Stringify(c).HtmlEncode() +
					                             "\"><td>" + c.Name + "</td><td>" +
					                            FormatJsDate(ConvertToDate(c .Birthday ), "dd.MM.yyyy")+ "</td><td><a href=\"#editPerson\">Edit</a></td></tr>"));

				}
					
				table.Append(jQuery.FromHtml("<tr><td>&nbsp;</td><td>&nbsp;</td><td><a href=\"#newPerson\">New</a></td></tr>"));
				
				//table.Find("a").Click(EditCustomer);
				table.Show();
				var t=	Document.CreateElement("table") as TableElement;
				var r = t.InsertRow(0); 
				r.SetAttribute("hola", "hola");
				var cell = r.InsertCell(0);
				cell.InnerHTML = "123";
				cell = r.InsertCell(1);
				cell.InnerHTML = "456";
				Document.GetElementById("main").AppendChild(t);

				List<GridColumn<Person>> cols = new List<GridColumn<Person>>();
				cols.Add( new GridColumn<Person>(){CellRenderFunc= f=> f.Name});
				AddTable<Person>(Document.GetElementById("main"), cols, persons);

				HtmlGrid<Person> gp = new HtmlGrid<Person>();
				gp.DataSource=persons;
				gp.Columns.Add( new GridColumn<Person>(){CellRenderFunc= f=> f.Name});
				gp.AddTo(Document.GetElementById("main"));

			});
		}

		void AddTable<T>(Element parent, List<GridColumn<T>> columns, List<T> data){
			var t=	Document.CreateElement("table") as TableElement;

			var i=0;
			foreach (var r in data){
				var row = t.InsertRow(i++);
				foreach(var c in columns){
					var j=0;
					var td = row.InsertCell(j++);
					td.InnerHTML= c.CellRenderFunc(r).ToString();
				}

			}

			parent.AppendChild(t);

		}

		JsDate ConvertToDate(DateTime date){
			if(date==null) return null;
			var tick = long.Parse( date.ToString());
			var d = new DateTime(tick);
			return new JsDate( d.GetUtcFullYear(),d.GetUtcMonth(), d.GetUtcDate(),
			                    d.GetUtcHours(), d.GetUtcMinutes(), d.GetUtcSeconds());
		}

		string FormatJsDate(JsDate date, string format){
			if(date==null) return string.Empty;
			return date.Format(format);
		}

	}


	public class GridColumn<T>{

		public  Func<T,object> CellRenderFunc{
			get;set;
		}
	}

	public class HtmlGrid<T>{

		public HtmlGrid(){
			Columns = new List<GridColumn<T>>();
		}

		public List<GridColumn<T>> Columns {get;set;}

		public List<T> DataSource {get;set;}

		public void AddTo (Element parent)
		{
			var t=	Document.CreateElement("table") as TableElement;

			var i=0;
			foreach (var r in DataSource){
				var row = t.InsertRow(i++);
				foreach(var c in Columns){
					var j=0;
					var td = row.InsertCell(j++);
					td.InnerHTML= c.CellRenderFunc(r).ToString();
				}
				
			}

			parent.AppendChild(t);
		}


	}


}
