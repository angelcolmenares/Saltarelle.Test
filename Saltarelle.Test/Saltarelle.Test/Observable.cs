using System.Collections.Generic;
using jQueryApi;
using System.Serialization;
using System.Html;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace Saltarelle.Test
{
	[IgnoreNamespace, Imported (IsRealType = true), ScriptName ("console")]
	public static class Console{

		[InlineCode ("console.debug({*args})")] 
		public static void Debug ( params  object[] args)
		{
		}

		[InlineCode ("console.dir({*args})")] 
		public static void Dir ( params  object[] args)
		{
		}

		[InlineCode ("console.error({*args})")] 
		public static void Error ( params  object[] args)
		{
		}

		[InlineCode ("console.info({*args})")] 
		public static void Info ( params  object[] args)
		{
		}

		[InlineCode ("console.log({*args})")] 
		public static void Log ( params  object[] args)
		{
		}
	}

	[IgnoreNamespace, Imported (IsRealType = true), ScriptName ("$")]
	public static class Extensions{

		[ScriptAlias ("$")]
		public static jQueryObject Empty(this jQueryObject element)
		{
			return null;
		}
	}
	

	[IgnoreNamespace, Imported (IsRealType = true), ScriptName ("$")]
	public static class Observable
	{
		[ScriptAlias ("$.observable")]
		public static ArrayObservable<T> ToArrayObservable<T> (IList<T> data)
		{
			return null;
		}

		[ScriptAlias ("$.observable")]
		public static ObjectObservable<T> ToObjectObservable<T> (T data)
		{
			return null;
		}

		[ScriptAlias ("$.templates")]
		public static void Templates <T>( T templates)
		{
		}
		
		[InlineCode ("$.view({element})")]
		public static ObservableEventData<T> View <T>(Element element )
		{
			return null;
		}
		
		[InlineCode ("$.view({selector})")]
		public static ObservableEventData<T> View <T>([SyntaxValidation ("cssSelector")] string selector )
		{
			return null;
		}
		
		[InlineCode ("$.views.helpers({anonType})")]
		public static void ViewsHelpers ( object anonType)
		{
		}

		[InlineCode ("$.views.converters({anonType})")]
		public static void ViewsConverters ( object anonType)
		{
		}

		[InlineCode ("$.link.{@template}({selector},{data})")] 
		public static jQueryObject Link<T> (string template, [SyntaxValidation ("cssSelector")] string selector, T data)
		{
			return null;
		}

		[InlineCode ("$.link.{@template}({selector},{data},{options})")] 
		public static jQueryObject Link<T> (string template, [SyntaxValidation ("cssSelector")] string selector,
		                                    T data, LinkOptions options)
		{
			return null;
		}

		[InlineCode ("$.render.{@template}({data})")]
		public static string Render<T> (string template, T data)
		{
			return null;
		}


		//$( "#showData" ).render( movies )
		[InlineCode ("{element}.render({data})")]
		public static jQueryObject Render<T>(this jQueryObject element, T data)
		{
			return null;
		}

		//$("#myLinkedContent").link( true, person );
		[InlineCode ("{element}.link({linkContent},{data})")]
		public static jQueryObject Link<T>(this jQueryObject element, bool linkContent, T data)
		{
			return null;
		}
	}

	[IgnoreNamespace, Imported (IsRealType = true)]
	public class ArrayObservable<T>{
		ArrayObservable(){}

		public  IList<T> Data(){
			return null;
		}

		public ArrayObservable<T> Insert( int index, T item){
			return null;
		}

		public ArrayObservable<T> Remove( int index, int numberOfItems){
			return null;
		}

		public ArrayObservable<T> Move (int oldIndex, int newIndex, int numToMove){
			return null;
		}

		public ArrayObservable<T> Refresh (List<T> newItems){
			return null;
		}

	}

	[IgnoreNamespace, Imported (IsRealType = true)]
	public class ObjectObservable<T>{

		public  T Data()
		{
			return default(T);
		}
		//o.observe("X",  function( event, args ) { console.log("argumentos",arguments);}) 
		//return Object ?? {bnd: ,cbNs} ; bnd[2]
		//observe: function(paths, callback) {

		public void Observe(string path, Action<jQueryEvent, ObjectObservableEventData<T>> callback )
		{
		}

		public void Unobserve(string path)
		{	
		}

		public ObjectObservable<T> SetProperty(string path, object value)
		{
			return null;
		}

		public ObjectObservable<T> SetProperty(JsDictionary<string,object> values)
		{
			return null;
		}

	}

	[Imported]
	[Serializable]
	[IgnoreNamespace]
	public class ObjectObservableEventData<T>{
		ObjectObservableEventData(){}
		public object OldValue{get{ return null; } set{} }
		public object Path{get{ return null; } set{} }
		public ObservableEventData<T> Value{get{ return null; } set{} }
	}

	[Imported]
	[Serializable]
	[IgnoreNamespace]
	public class ObservableEventData<T>{
		ObservableEventData(){}
		public int Index{get{ return 0; } set{} }
		public Element ParentElem {get{ return null; } set{} }
		public string Type {get{ return null; } set{} }
		public ObservableEventData<IList<T>> Parent {get{ return null; } set{} }
		public T Data {
			get { return default(T);}
			set {}
		}
	}


	[Imported]
	[Serializable]
	[IgnoreNamespace]
	public class LinkOptions{
		public string Target {get;set;}
	}


	[Imported]
	[Serializable]
	[IgnoreNamespace]
	[PreserveMemberCase]
	class SelectedObject<T>{

		public ObservableEventData<T> SelectedItem{
			get { return null;}
			set {}
		}
	}


}

/*
((DialogObject)jQuery.Select("#customerForm")).Close());
$( ".selector" ).dialog( "close" );
[InlineCode ("{this}.dialog('close')")]
public void Close ()
{
}
*/

