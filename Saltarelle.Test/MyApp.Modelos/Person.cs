using System;
using System.Runtime.CompilerServices;

namespace MyApp.Modelos
{

#if SALTARELLE
	[PreserveMemberCase]
	[Serializable]
#endif
	
	public class Person
	{
		public Person ()
		{
		}

		public int Id {get;set;}
		public string Name {get;set;}
		public DateTime Birthday {get;set;}
		public bool Active {get;set;}
		public DateTime? LastLog {get;set;}


	}
}

