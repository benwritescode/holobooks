using UnityEngine;
using System.Collections;

public class TextDisplayBehavior : MonoBehaviour
{
	// TextMesh property. Find it and set it if it isn't already set.
	private TextMesh _mesh;

	private TextMesh mesh {
		get {
			return _mesh ?? (_mesh = this.gameObject.GetComponentInChildren<TextMesh> ());
		}
	}

	// default max number of lines = 27
	int maxLines = 27;

	// default line length = 52, with fixed width VeraMono font of size 30.
	int lineLength = 52;


	// SetText takes in a string argument, and divides it up into lines, then sets the child mesh to display the text.
	public void SetText (string text)
	{
		// TODO: this function inserts a dash whenever a word is interrupted
		// It would be better if it detected where the dash would land, and forced a new line in some cases.
		// There must be rules regarding when to create a dash, and when to just push the word to the next line.
		// It would be better to use such rules.

		string text2 = "";

		while (text.Length > 0) {
			if (text.Length > lineLength) {

				string section = text.Substring (0, lineLength);
				string lastbit = section.Substring (section.Length - 1);

				if (lastbit == " " || lastbit == "\n") {
					section += "\n";
				} else {
					section += "–\n";
				}
				text2 += section;
				text = text.Remove (0, lineLength);
			} else {

				text2 += text;
				text = "";
			}

		}


		this.mesh.text = text2;

	}



	// Probably for testing only.
	// In the future, we will load text from an API, and push it to this class using the public SetText function.
	private string loadText (string filename)
	{
		TextAsset asset = Resources.Load ("MobyDick") as TextAsset;

		print ("what is text asset");
		print (asset);
		string theText = asset.text;
		return theText;
	}

	// Use this for initialization
	void Start ()
	{
		// For sample purposes only. Normally we will access setText from the outside instead.
		this.SetText (this.loadText ("MobyDick"));
			

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
