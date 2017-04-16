using UnityEngine;
using System.Collections;



public class SpeechToTextTester : MonoBehaviour
{

	SpeechToText mySpeechToText;
	AudioClip clip;

	// Use this for initialization
	void Start ()
	{
		mySpeechToText = GameObject.FindObjectOfType<SpeechToText> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown ("space")) {
			clip = Microphone.Start (null, true, 60, 44100);

		}

		if (Input.GetKeyUp ("space")) {
			Microphone.End (null);
			mySpeechToText.ConvertClipToTextWithCallback (clip, this.RecognizedText);

		}
	}



	void RecognizedText (string text)
	{

		Debug.Log ("SpeechToText output: " + text);

	}
}
