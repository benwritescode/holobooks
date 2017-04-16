using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Configuration;


public class SendEmailBehavior : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


	public void SendEmail (string toAddress, string message)
	{
		Debug.Log ("SendEmailBehavior: SendEmail to " + toAddress + " : " + message);

		// get username and password from config.cs
		string username = Config.GetString ("gmail_username");
		string password = Config.GetString ("gmail_password");


		// use C# APIs to send an email from the user’s provided gmail username.

	}
}
