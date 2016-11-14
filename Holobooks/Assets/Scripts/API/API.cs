using UnityEngine;
using System.Collections;
using Holobook.WebRequest;
public class API : MonoBehaviour {


	public void test(){
		WebRequest.Request("http://minrva-dev.library.illinois.edu:8080/api/catalog/search?loc=uiu_undergrad&query=cat&type=all&page=1&format=Book", 3.5f, this, (WebRequest request) =>
			{
				OnRequestDone(request);
			});

	}
	private void OnRequestDone(WebRequest request)
	{
		switch (request.GetState())
		{
		case WebRequest.State.DONE:
			Debug.Log("RECIEVE : "+request.www.text);
			break;
		}
	}
	void Start () {
		test();
	}
	

}
