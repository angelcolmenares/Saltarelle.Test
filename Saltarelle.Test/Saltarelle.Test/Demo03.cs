using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using jQueryApi;
using System.Collections;


namespace Saltarelle.Test
{
	public class Demo03
	{
		Person person;

		public static void Main()
		{
			jQuery.OnDocumentReady(new Demo03().Attach);
		}
		
		void Attach() {
			jQuery.OnDocumentReady(() => {
				person = CreatePerson();
				Observable.ViewsHelpers(new {infoSummary=InfoSummary});
				Observable.ViewsConverters(new {upper=Upper});
				jQuery.Select("#myLinkedContent").Link( true, person );
				jQuery.Select("#buttonSetNameAndCityAndColorAndCars").Click(evt=>{
					SetNameAndCityAndColorAndCars();
				});
				jQuery.Select("#buttonSetCity").Click(evt=>{
					SetCity();
				});
			});
		}

		void SetNameAndCityAndColorAndCars() {

			var values = new JsDictionary<string,object>();
			values["cars.quantity"]=person.Cars.Quantity+1;
			values["cars.price"]=person.Cars.Price-3;
			values["lastName"]=person.LastName + "Plus";
			values["address.city"]= person.Address.City + "More";
			values["roleColor"]= person.RoleColor == "yellow" ? "#8DD" : "yellow";
			Observable.ToObjectObservable( person )
				.SetProperty(values);
		}

		void SetCity() {
			Observable.ToObjectObservable( person ).SetProperty( "address.city", person.Address.City + "Add" );
		}

	
		Func<string,string,string,int,string,string> InfoSummary{
			get{
				Func<string,string,string,int,string,string> info=(firstName, lastName, city, cars, html)=>{
					return (!string.IsNullOrEmpty(html))?
						("<b>" + firstName + " " + lastName + "</b> lives in <i>" + city +  "</i> and owns " + cars.ToString() + " cars."):
						(firstName + " " + lastName + " lives in " + city + " and owns " + cars.ToString() + " cars.");
				};
				return info;
			}
		}

		Func<string,string> Upper{
			get{
				Func<string,string> upper= val=>{
					return val.ToUpper();
				};
				return upper;
			}
		}

		[Serializable]
		class Car{
			public int Quantity{get;set;}
			public int Price{get;set;}
		}

		[Serializable]
		class Address {
			public string City{get;set;}
		}

		[Serializable]
		class Person{
			public string FirstName{get;set;}
			public string LastName {get;set;}
			public Car Cars {get;set;}
			public Address Address {get;set;}
			public string RoleColor{get;set;}
		}

		Person CreatePerson(){
			return new Person{
				FirstName="Jo",
				LastName="Proctor",
				Cars= new Car{Quantity=13, Price=150},
				Address= new Address{City="Redmon"},
				RoleColor="yellow"
			};
		}

	}
}

