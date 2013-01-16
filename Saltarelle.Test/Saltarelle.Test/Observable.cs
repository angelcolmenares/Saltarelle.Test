using System.Collections.Generic;
using jQueryApi;
using System.Serialization;
using System.Html;
using System;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Saltarelle.Test
{

	[IgnoreNamespace, Imported (IsRealType = true), ScriptName ("$")]
	public static class JsRender
	{
		[ScriptAlias ("$.templates")]
		public static void Templates <T>( T templates)
		{
		}
	}


	[IgnoreNamespace, Imported (IsRealType = true), ScriptName ("$")]
	public static class Observable
	{
		[ScriptAlias ("$.observable")]
		public static ArrayObservable<T> CreateArrayObservable<T> (IList<T> data)
		{
			return null;
		}

		[ScriptAlias ("$.observable")]
		public static ObjectObservable<T> CreateObjectObservable<T> (T data)
		{
			return null;
		}

	}


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

	public class ObjectObservable<T>{

		public  T Data()
		{
			return default(T);
		}
		//o.observe("X",  function( event, args ) { console.log("argumentos",arguments);}) 
		//return Object ?? {bnd: ,cbNs} ; bnd[2]
		//observe: function(paths, callback) {

		public void Observe(string path, Action<CallbackEvent<T>, CallbackObject> callback )
		{
		}

		public void Unobserve(string path)
		{	
		}

		public ObjectObservable<T> SetProperty(string path, object value)
		{
			return null;
		}
	}

	[Imported]
	[Serializable]
	[IgnoreNamespace]
	public class CallbackObject{
		public object OldValue{get{ return null; } set{} }
		public object Path{get{ return null; } set{} }
		public object Value{get{ return null; } set{} }
	}

	[Imported]
	[Serializable]
	[IgnoreNamespace]
	public class CallbackEvent<T>{
		public T Target{get{ return default(T); } set{} }
	}



}

