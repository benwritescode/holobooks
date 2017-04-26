using UnityEngine;
using System.Collections;



public class SpeechToTextTester : MonoBehaviour
{

	SpeechToText mySpeechToText;
	AudioClip clip;

	System.Threading.Thread mainThread;



	// Use this for initialization
	// Check to make sure callback is always called on mainthread:
	// http://stackoverflow.com/questions/26452609/find-out-if-im-on-the-unity-thread

	void Start ()
	{
		mainThread = System.Threading.Thread.CurrentThread;

		mySpeechToText = GameObject.FindObjectOfType<SpeechToText> (); 
		//SpeechToText.instance;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown ("up")) {
			clip = Microphone.Start (null, true, 60, 44100);

		}

		if (Input.GetKeyUp ("up")) {
			Microphone.End (null);
			mySpeechToText.ConvertClipToTextWithCallback (clip, this.RecognizedText);

		}
	}



	void RecognizedText (string text)
	{

		Debug.Log ("SpeechToText output: " + text);
		Debug.Log ("Make sure we're on main thread by using MainThread only function: " + Application.dataPath);
		if (mainThread.Equals (System.Threading.Thread.CurrentThread)) {
			Debug.Log ("Yay, our callback was called on the main thread.");

		} else {
			Debug.LogWarning ("Uh oh, the callback wasn't called on the main thread.");
		}
			
	}
}
