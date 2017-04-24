using UnityEngine;
using System;
using System.Collections;

using System.Net;
using System.Collections.Generic;

using Configuration;

public class SessionLog : MonoBehaviour
{
	public List<BookReference> session;
	SendEmailBehavior sendEmailBehavior;

	void Start () 
	{
		this.session = new List<BookReference>();
		this.sendEmailBehavior = new SendEmailBehavior();
	}

	void Update ()
	{

	}

	public void appendReference(BookReference reference) 
	{
		this.session.Add (reference);

		Debug.Log ("New book reference added");
	}

	private string convertReferencesIntoText() {
		string json = JsonUtility.ToJson(this.session);

		return json;
	}

	public void sendSessionLog() {
		string message = convertReferencesIntoText();

		sendEmailBehavior.SendEmail (Config.GetString ("recipient_email"), message);

	}

}
