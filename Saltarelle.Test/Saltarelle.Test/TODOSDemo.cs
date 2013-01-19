using System;
using jQueryApi;

namespace Saltarelle.Test
{
	public class TODOSDemo
	{
		public static void Main()
		{
			jQuery.OnDocumentReady(new Demo04().Attach);
		}
		
		void Attach() {
			jQuery.OnDocumentReady(() => {
			});

		}


	}
}

