using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Text;

public class ClientBehavior : MonoBehaviour
{
	// https://msdn.microsoft.com/en-us/library/system.net.sockets.tcpclient(v=vs.110).aspx

	const int port = 5555;
	const string host_ip = "127.0.0.1";

	TcpClient client;

	//https://msdn.microsoft.com/en-us/library/system.io.streamreader(v=vs.110).aspx
	StreamWriter writer;

	//https://msdn.microsoft.com/en-us/library/system.io.streamwriter(v=vs.110).aspx
	StreamReader reader;

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("ClientBehavior: start()");
		try {
			
			client = new TcpClient ("localhost", port);
			writer = new StreamWriter (client.GetStream (), Encoding.ASCII);
			reader = new StreamReader (client.GetStream (), Encoding.ASCII);

			if (client != null)
				Debug.Log ("Connected to server!");

		} catch (System.Net.Sockets.SocketException e) {
			Debug.Log ("ClientBehavior: unable to connect to server socket.");
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (reader != null) {
			string my_message = reader.ReadLineAsync ();
			while (null != my_message) {
				Debug.Log ("Received message: " + my_message);

				my_message = null;
				my_message = reader.ReadLine ();
			}
		}


	}
}
