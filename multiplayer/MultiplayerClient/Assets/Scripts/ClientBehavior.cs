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

		client = new TcpClient (host_ip, port);
		writer = StreamWriter (client.GetStream (), Encoding.ASCII);
		reader = StreamReader (client.GetStream (), Encoding.ASCII);

	}
	
	// Update is called once per frame
	void Update ()
	{
		string my_message;
		while (my_message = reader.ReadLine ()) {
			Debug.Log ("Received message: " + my_message);
		}

	}
}
