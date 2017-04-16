using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using Configuration;


public class SendEmailBehavior : MonoBehaviour
{
	//static bool mailSent = false;
	// Use this for initialization
	void Start ()
	{
		//ServicePointManager.ServerCertificateValidationCallback = 
		//	delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
		//	return true;
		//};


		//string server = "smtp.gmail.com";
		
		//moved these up here after reading the link you sent me
		//string username = Config.GetString("gmail_username"); 
		//string password = Config.GetString("gmail_password");

		//SmtpClient client = new SmtpClient
							//{
							//	Host = server,
							//	Port = 587,
							//	EnableSsl = true,
							//	DeliveryMethod = SmtpDeliveryMethod.Network,
							//	UseDefaultCredentials = false,
							//	Credentials = new System.Net.NetWorkCredential(username,password)
							//};
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

		MailAddress fro = new MailAddress (username);
		MailAddress to = new MailAddress (toAddress);

		MailMessage mail = new MailMessage(fro,to);
		mail.Subject = "Subject";
		mail.Body = message;

		SmtpClient client = new SmtpClient();

		client.Host = "smtp.gmail.com";
		client.Port = 587;
		client.EnableSsl = true;
		client.DeliveryMethod = SmtpDeliveryMethod.Network;
		client.UseDefaultCredentials = false;
		client.Credentials = (ICredentialsByHost)new NetworkCredential(username, password);

		ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
			return true;
		};

		client.Send(mail);

		// use C# APIs to send an email from the user’s provided gmail username.

	}
}
