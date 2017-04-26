using UnityEngine;
using System.Collections;


using System.Collections.Generic;

public class ParserTester : MonoBehaviour
{


	// Use this for initialization
	void Start ()
	{


	}

	// Update is called once per frame
	void Update ()
	{


		// To test, press space once.
		if (Input.GetKeyDown ("space")) {

		}

		if (Input.GetKeyUp ("space")) {

			// perform parsing test.
			string jsonToParse = ParserSpace.BookReferenceParser.GetJsonNamed ("holobooks_example.json");
			Debug.Log ("parsing: " + jsonToParse);

			List<BookReference> list = ParserSpace.BookReferenceParser.ParseResponseToReferences (jsonToParse, true);

		}
	}




}
