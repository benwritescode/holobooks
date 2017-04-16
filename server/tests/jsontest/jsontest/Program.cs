using System;

// deserialize JSON string into dictionary using just .Net
// http://stackoverflow.com/questions/1207731/how-can-i-deserialize-json-to-a-simple-dictionarystring-string-in-asp-net
// no external library necessary. Nice.

using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.IO;


namespace jsontest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

			var serializer = new JavaScriptSerializer (); //using System.Web.Script.Serialization;
			string mystring = File.ReadAllText (@"./config.json");

			Console.WriteLine ("Attempting to deserialize string: " + mystring);
			Dictionary<string, string> values = serializer.Deserialize<Dictionary<string, string>> (mystring);

			Console.WriteLine ("IP: " + values ["ip"]);
			Console.WriteLine ("Port: " + values ["port"]);

		}
	}
}
