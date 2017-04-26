using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSearchResultManager : MonoBehaviour {
	public GameObject bookSearchResult;

	// Use this for initialization
	void Start () {


		for(int i =0 ; i<8;i++){
			GameObject go = Instantiate(bookSearchResult);
			go.transform.parent = transform;
			BookSearchItemManager bookItem = go.GetComponent<BookSearchItemManager> ();
			bookItem.setItem ("Title", "AUthor", "nyp.33433082228226");
		}
	}

	public void populateMenu(List<BookReference> li){
		for (int i = 0; i < li.Count; i++) {
			GameObject go = Instantiate(bookSearchResult);
			go.transform.parent = transform;
			BookSearchItemManager bookItem = go.GetComponent<BookSearchItemManager> ();
			bookItem.setItem (li[i].title, li[i].author, li[i].title);
		}
	}
		

}
