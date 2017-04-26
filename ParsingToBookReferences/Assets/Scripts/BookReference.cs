using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BookReference
{
	public List<string> titles;
	public List<string> authors;
	public string id;


	public List<int> visitedPages = new List<int> ();
	public int currentPage = 0;


	public BookReference (string newTitle, string newAuthor, string newid = "fakeid")
	{
		this.titles = new List<string> ();
		titles.Add (newTitle);
		this.authors = new List<string> ();
		authors.Add (newAuthor);

		this.id = newid;
	}

	public BookReference (IDictionary dict, bool verbose = false)
	{
		this.titles = new List<string> ();
		this.authors = new List<string> ();

		this.id = "fakeid";

		// create a new book reference based on the data in the IDictionary
		// this function assumes the IDictionary was created by something like the class in this commit called ParserSpace

		foreach (DictionaryEntry entry in dict) {

			if (verbose)
			if (entry.Key.ToString () == "title" || entry.Key.ToString () == "author" || entry.Key.ToString () == "id") {
				Debug.Log ("found entry: " + entry.Key);
				Debug.Log ("found entry value: " + entry.Value + " with type: " + entry.Value.GetType ());

			}

			if (entry.Key.ToString () == "title") {
				foreach (System.Object obj in entry.Value as List<System.Object>) {
					this.titles.Add (obj as string);
				}
			}
			if (entry.Key.ToString () == "author") {
				foreach (System.Object obj in entry.Value as List<System.Object>) {
					this.authors.Add (obj as string);
				}
			}
				
			if (entry.Key.ToString () == "id") {
				this.id = entry.Value.ToString ();
			}
		}



	}

	public string ToString ()
	{
		string description = id;

		if (authors.Count > 0)
			description = authors [0] + ", " + description;
		
		if (titles.Count > 0)
			description = titles [0] + ", " + description; 

		return description;
	}

}
