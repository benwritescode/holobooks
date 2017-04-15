using UnityEngine;
using System.Collections;


// we may need to implement something like this for word wrapping on the text mesh... if our page is text...
// http://answers.unity3d.com/questions/190800/wrapping-a-textmesh-text.html


public class BookObjectBehavior : MonoBehaviour
{

	
	BookData data;


	// Use this for initialization
	void Start ()
	{
		// create a default book data for testing for now.
		data = new BookData ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
