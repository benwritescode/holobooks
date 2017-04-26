using UnityEngine;
using System.Collections;
using System.IO;
using System;
using MiniJSON;



public enum OSType
{
	INVALID,
	Mac,
	Windows,
	Linux,
	END

}
	
namespace Configuration
{
	public class Config
	{
		static public OSType GetOS ()
		{
			//https://docs.unity3d.com/ScriptReference/SystemInfo-operatingSystem.html
			if (SystemInfo.operatingSystem.Contains ("Mac")) {
				return OSType.Mac;
			}
			if (SystemInfo.operatingSystem.Contains ("Windows")) {
				return OSType.Windows;
			}
			// If not, "Linux" is a good guess for now.
			return OSType.Linux;
		}

		static public string GetString (string key, string optional_default = "")
		{

			IDictionary dict = Config.GetConfigDictionary ();

			string value = "";

			try {
				if (dict.Contains (key)) {
					value = (string)dict [key];
				} else {
					Debug.LogWarning ("Missing value for key \"" + key + "\" from config dictionary. Did you set it in config.json?");
					if (optional_default != "")
						value = optional_default;
				}
			} catch (Exception e) {
				Debug.LogWarning ("Exception while attempting to get value for key \"" + key + "\" from config dictionary. Did you set it in config.json?");
				if (optional_default != "")
					value = optional_default;
			}
				
			return value;
		}

		static public int GetInt (string key, int optional_default = -1)
		{

			IDictionary dict = Config.GetConfigDictionary ();

			int value = optional_default;
			try {
				if (dict.Contains (key)) {
					value =Convert.ToInt32(dict[key]);
				} else {
					Debug.LogWarning ("Missing value for key \"" + key + "\" from config dictionary. Did you set it in config.json?");
					value = optional_default;
				}
			} catch (Exception e) {
				Debug.LogWarning ("Exception while attempting to get value for key \"" + key + "\" from config dictionary. Did you set it in config.json?");
				value = optional_default;
			}

			return value;
		}

		static public string GetIP ()
		{
			return Config.GetString ("ip");
		}

		static public string GetPort ()
		{
			return Config.GetString ("port");
		}


		static public string PythonPath ()
		{

			IDictionary dict = Config.GetConfigDictionary ();

			string path = "";


			try {
				
				switch (Config.GetOS ()) {

				case OSType.Mac:
					{
						path = "" + dict ["mac_python_path"];
						break;
					}
				case OSType.Windows:
					{
						path = "" + dict ["windows_python_path"];
						break;
					}
				default:
					{
						path = "" + dict ["linux_python_path"];
						break;
					}

				}
			} catch (Exception e) {
				Debug.LogWarning ("Unable to get Python path from config directory. Did you set the OS and the path for the OS in config.json?");
			}
            return "C:\\Python27\\python";

		}

		// This is for loading a configuration file
		// This works slightly different than on the server, because Unity doesn't have native JSON deserialization to Dictionary types.
		static public IDictionary GetConfigDictionary ()
		{

			IDictionary values = null;
			try {
				string path = Application.dataPath + "/config.json";
				string mystring = File.ReadAllText (path);
				values = (IDictionary)Json.Deserialize (mystring);

				//			Debug.Log ("GetConfigDictionary: Loaded ip: " + values ["ip"]);
				//			Debug.Log ("GetConfigDictionary: Loaded port: " + values ["port"]);
			} catch (Exception e) {
				Debug.Log ("GetConfigDictionary: Config file config.json is malformed. Could not load settings.");
				Debug.Log ("GetConfigDictionary: Exception: " + e.ToString ());
			}

			return values;
		}
	}
}



