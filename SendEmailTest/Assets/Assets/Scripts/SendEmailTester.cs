using UnityEngine;
using System.Collections;

using Configuration;

public class SpeechToTextTester : MonoBehaviour
{

	SendEmailBehavior sendEmailBehavior;

	// Use this for initialization
	void Start ()
	{
		sendEmailBehavior = GameObject.FindObjectOfType<SendEmailBehavior> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown ("space")) {
			// nothing

		}

		if (Input.GetKeyUp ("space")) {
			// send an email
			sendEmailBehavior.SendEmail (Config.GetString ("recipient_email"), "Hello this is a test message");


		}
	}



	void RecognizedText (string text)
	{

		Debug.Log ("SpeechToText output: " + text);

	}
}
