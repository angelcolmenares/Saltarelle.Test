using System;
using System.Collections.Generic;
using jQueryApi;
using System.Html;
using System.Serialization;

namespace Saltarelle.Test
{
	public class Demo04
	{

		List<Currency> currencies;
		List<Movie> movies;
		OrderDetails orderDetails;

		public static void Main()
		{
			jQuery.OnDocumentReady(new Demo04().Attach);
		}
		
		void Attach() {
			jQuery.OnDocumentReady(() => {
				PopulateCurrencies();
				PopulateMovies();
				CreateOrderDetails();

				var contextHelpers = new JsDictionary<string,object>();
				contextHelpers["app"]= new {};
				contextHelpers["movies"]= movies;
				contextHelpers["convertedPrice"]= ConvertedPrice;
				contextHelpers["currencies"]= currencies;
				contextHelpers["selectedTitle"]=SelectedTitle;

				Observable.Templates( "moviePurchaseTemplate", "#moviePurchaseTemplate" );
				Observable.Link("moviePurchaseTemplate", "#moviePurchase", orderDetails, contextHelpers );

				jQuery.Select( "#submitOrder" ).Click(evt=>{
					Window.Alert("You ordered a movie ticket as follows:\n"+
					             Json.Stringify(orderDetails));
				});


			});
		}

		Func<int,int,string> ConvertedPrice{
			get
			{
				Func<int,int,string> cp = (selectedMovie, selectedCurrency)=>{
					var currency = currencies[selectedCurrency];
					return (selectedMovie != -1)?
						currency.Symbol +(movies[selectedMovie].TicketPrice*currency.Rate).ToString():
						"";
				};
				return cp;
			}
		}

		Func<int, string> SelectedTitle{
			get
			{
				Func<int, string> st = (selectedMovie) =>{
					return (selectedMovie!=-1) ? movies[selectedMovie].Title : "";
				};
				return st;
			}
		}

		void PopulateCurrencies(){
			currencies = new List<Currency>(){
				new Currency{ Name="US", Label="US Dollar", Rate= 1.0m, Symbol= "$" },
				new Currency{ Name="EUR", Label="Euro", Rate= 0.95m, Symbol= "€" },
				new Currency{ Name="GB", Label="Pound", Rate= 0.63m, Symbol= "£" }
			};
		}

		void PopulateMovies(){
			movies = new List<Movie>(){
				new Movie {Title= "The Red Violin",TicketPrice=18 },
				new Movie {Title= "The Inheritance",TicketPrice=16.5m },
				new Movie {Title= "The Incredible Hulk",TicketPrice=21.99m },
			};
		}

		void CreateOrderDetails(){
			orderDetails= new OrderDetails();
		}


		[Serializable]
		class Currency{
			public string  Name {get;set;}
			public string  Label {get;set;}
			public decimal Rate {get;set;}
			public string  Symbol {get;set;}
		}


		[Serializable]
		class OrderDetails {
			public OrderDetails(){
				SelectedMovie=-1;
				SelectedCurrency=1;
			}
			public string Name{get;set;}
			public int SelectedMovie{get;set;}
			public int SelectedCurrency {get;set;}
			public string Request {get;set;}
		}

		[Serializable]
		class Movie{
			public string Title{get;set;}
			public decimal TicketPrice {get;set;}
		}
	}
}

