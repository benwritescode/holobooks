using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSearchResultManager : MonoBehaviour {
	public GameObject bookSearchResult;
	public List<BookReference> bookList;
	public GameObject downloadMenu;
	private List<GameObject> itemList;
	// Use this for initialization
	void Start () {
//		bookList = Utils.runSearchScript ("Catcher in the Rye", "title");
//		populateMenu (bookList);
	}

	public void populateMenu(List<BookReference> li){
		for (int i = 0; i < li.Count; i++) {
			GameObject go = Instantiate(bookSearchResult);
			go.transform.parent = transform;
//			itemList.Add (go);
			BookSearchItemManager bookItem = go.GetComponent<BookSearchItemManager> ();
			bookItem.setItem (li[i].titles[0], li[i].authors[0], li[i].id, li[i]);
		}
	}

	public void cleanUp(){
		for (int i = 0; i < itemList.Count; i++) {
			Destroy (itemList [i]);
		}
	}

	public void createSentObj(BookReference obj){
		GameObject go = Instantiate(bookSearchResult);
		go.transform.parent = transform;
		BookSearchItemManager bookItem = go.GetComponent<BookSearchItemManager> ();
		bookItem.setItem (obj.titles[0], obj.authors[0], obj.id, obj);
	}

}
