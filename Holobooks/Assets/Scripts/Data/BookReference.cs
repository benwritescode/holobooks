using UnityEngine;
using System.Collections;

public class BookReference
{
	public string title;
	public string author;
	public int ISBN;
	public List<int> visitedPages = new ArrayList<int> ();
	public int currentPage = 0;


	public BookReference (string newTitle)
	{
		this.title = newTitle;
	}

}
