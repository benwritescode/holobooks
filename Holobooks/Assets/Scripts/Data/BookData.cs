using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// lambda / action usages:
// http://www.blockypixel.com/2012/09/c-in-unity3d-dynamic-methods-with-lambda-expressions/

// dictionary usages:
//http://csharp.net-informations.com/collection/dictionary.htm

// Moby Dick in public domain, here is the data:
// useful for test data object in BookData() test method
// https://americanliterature.com/author/herman-melville/book/moby-dick-or-the-whale/chapter-1-loomings

// we will load the moby dick test data perhaps similarly to this:
// https://forum.unity3d.com/threads/read-from-textasset-line-by-line.327422/


public class BookData
{
	
	public BookReference reference;
	private Dictionary<int, PageData> pageDatas = new Dictionary<int, PageData> ();


	// test book data
	public BookData ()
	{
		this.reference = new BookReference ("Mobydick");
		string mobydickpage1 = "";

		this.pageDatas.Add (0, mobydickpage1);

	}

	public BookData (BookReference nReference)
	{

		this.reference = nReference;



	}

	// Takes in a pageNumber, and a callback. The callback's one and only argument is a PageData.
	// When get page is finished, it will call the lambda, "Action(PageData)" to return the requested page data to the caller.

	public void GetPageAndReturnWithCallback (int pageNumber, Action<PageData> action)
	{
		if (pageDatas.ContainsKey (pageNumber)) {
			action (pageDatas [pageNumber]);

		} else {
			// Call the book/library API with a new callback
			// our own callback will store the new page data
			// and then it will return the page data to action.
		}
	}


}
