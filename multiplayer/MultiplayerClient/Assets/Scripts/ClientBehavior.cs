using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Text;

public class ClientBehavior : MonoBehaviour
{

	// TODO:
	// unfortunately, Unity doesn't support StreamReader.AsyncReadLine()
	// instead, we will have to make a thread to consume packets asynchronously.
	// need to make a thread to read lines, a buffer to store them,
	// and then within update, we should call a delegate function whenever we consume a string from the buffer.

	// https://msdn.microsoft.com/en-us/library/system.net.sockets.tcpclient(v=vs.110).aspx

	const int port = 5556;
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
			Debug.Log ("ClientBehavior: start(): writing message");


			writer.WriteLine ("Client says \"Hello there!\"");
			writer.Flush ();
			Debug.Log ("ClientBehavior: start(): wrote message");


			Debug.Log ("Received message back: " + reader.ReadLine ());


		} catch (System.Net.Sockets.SocketException e) {
			Debug.Log ("ClientBehavior: unable to connect to server socket.");
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (reader != null) {
//			string my_message = reader.ReadLine ();
//			while (null != my_message) {
//				Debug.Log ("Received message: " + my_message);
//
//				my_message = null;
//				my_message = reader.ReadLine ();
//			}
//		}
//

	}
}
