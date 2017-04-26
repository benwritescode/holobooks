using UnityEngine;
using System.Collections;
using System.IO;
using System;
using MiniJSON;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserSpace
{
	public class BookReferenceParser
	{

		public static List<BookReference> ParseResponseToReferences (string jsonresponse, bool verbose = false)
		{

			if (verbose)
				Debug.Log ("bookReferenceParser: ParseResponseToReferences");
			List<BookReference> bookList = new List<BookReference> ();

			IDictionary TopLevel = (IDictionary)Json.Deserialize (jsonresponse);


			List<string> keysOfInterest = new List<string> ();

			keysOfInterest.Add ("title");
			keysOfInterest.Add ("author");
			keysOfInterest.Add ("id");

			List<IDictionary> foundDictionaries = new List<IDictionary> ();


			FindDictionariesContainingKeysInObject (TopLevel, keysOfInterest, foundDictionaries, "TopLevel");

			foreach (IDictionary dict in foundDictionaries) {
				BookReference newRef = new BookReference (dict);
				bookList.Add (newRef);
			}



			if (verbose)
				foreach (BookReference bookRef in bookList) {

					Debug.Log ("Found book: " + bookRef.ToString ());
				}

			return bookList;

		}

		public static string GetJsonNamed (string name)
		{
			string mystring = "";
			try {
				string path = Application.dataPath + "/Resources/" + name;
				mystring = File.ReadAllText (path);

			} catch (Exception e) {
				Debug.Log ("BookReferenceParser: Couldn't find file named " + name);
				Debug.Log ("GetConfigDictionary: Exception: " + e.ToString ());
			}

			return mystring;
		}




		// keys I want to grab for sure:
		// title
		// author
		// id


		// This function recursively searches for IDictionaries that have all of the correct keys. (and potentially more keys)
		// Whenever it finds an IDictionary which matches the profile, it adds it to the foundDictionaries array (passed in by reference by the user)
		// The upside of this is that you can quickly find just the stuff you want in an IDictionary translated from JSON,
		// without repetitive casting or a lot of manual work.
		private static void FindDictionariesContainingKeysInObject (System.Object obj, List<string> keys, List<IDictionary> foundDictionaries, string dictName = "", bool verbose = false)
		{
			if (verbose)
				Debug.Log ("Testing dictionary named " + dictName);
			// any found Dictionaries are stored in the foundDictionaries array
			if (obj as IDictionary != null) {
				IDictionary dict = obj as IDictionary;

				ICollection<string> dictkeys = dict.Keys as ICollection<string>;


				bool containsAll = true;
				foreach (string key in keys) {
					if (!dictkeys.Contains<string> (key)) {

						containsAll = false;
					}
						
				}

				if (containsAll) {
					foundDictionaries.Add (dict);
				} else {
					if (verbose) {
						if (dictkeys.Contains ("title"))
							dictName = dict ["title"] as string;
						Debug.Log ("Dictionary " + dictName + " doesn't match.");

					}
				}

				foreach (DictionaryEntry e in dict) {
					FindDictionariesContainingKeysInObject (e.Value as System.Object, keys, foundDictionaries, e.Key as string);
				}

			} 

			if ((obj as List<System.Object>) != null) {
				foreach (System.Object obj2 in obj as ICollection<System.Object>) {
					FindDictionariesContainingKeysInObject (obj2, keys, foundDictionaries, "ListItem");
				}
			}


		}

		private static void PrintDictionary (IDictionary dict, string name = "DictionaryName", string indentation = "", List<string> skipkeys = null)
		{

			skipkeys = skipkeys ?? new List<String> ();

			if (skipkeys.Contains (name)) {
				return;
			}


			Debug.Log (indentation + "DICTIONARY : " + name);

			foreach (DictionaryEntry e in dict) {
				if (e.Value as IDictionary != null) {
					PrintDictionary (e.Value as IDictionary, e.Key.ToString (), indentation + "=", skipkeys);
				} else if (e.Value as List<System.Object> != null) {
					PrintList (e.Value as List<System.Object>, e.Key.ToString (), indentation + "=", skipkeys);
				} else {

					if (!skipkeys.Contains (e.Key.ToString ()))
						Debug.Log ("=" + indentation + e.Key.ToString () + "                      " + e.Value.GetType ().ToString ());

				}

			
			}
		}

		private static void PrintList (List<System.Object> list, string name = "ListName", string indentation = "", List<string> skipkeys = null)
		{


			skipkeys = skipkeys ?? new List<String> ();

			Debug.Log (indentation + name + " is a list");

			foreach (System.Object e in list) {
				if (e as IDictionary != null) {
					PrintDictionary (e as IDictionary, "ListItem", indentation + "=", skipkeys = skipkeys);
				} else if (e as List<System.Object> != null) {
					PrintList (e as List<System.Object>, "ListItem", indentation + "=", skipkeys = skipkeys);
				} else {
					
					Debug.Log ("=" + indentation + " ListItem                      " + e.GetType ().ToString ());

				}

			}


		}
	}



}

// referenced:
// for multiplication of strings
// http://stackoverflow.com/questions/532892/can-i-multiply-a-string-in-c

// for empty list optional param
// http://stackoverflow.com/questions/6947470/c-how-to-use-empty-liststring-as-optional-paramete