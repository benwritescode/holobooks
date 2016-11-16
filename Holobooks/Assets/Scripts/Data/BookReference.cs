using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BookReference
{
	public string title;
	public string author;
	public int ISBN;
	public List<int> visitedPages = new List<int> ();
	public int currentPage = 0;


	public BookReference (string newTitle)
	{
		this.title = newTitle;
	}

}
