using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;

using System.IO;
using System.Text;

class Server
{

	const int port = 5555;
	const string my_ip = "127.0.0.1";


	static void Main ()
	{
		Console.WriteLine ("\n============\n\nHolobooks server starting\n============\n\n");


		System.Net.IPAddress localAddress = IPAddress.Parse (my_ip);
		TcpListener listenForClients = new TcpListener (localAddress, port);

		// start listening for clients.
		Console.WriteLine ("Listening for clients\n");
		listenForClients.Start ();

		TcpClient client = listenForClients.AcceptTcpClient ();
		if (client != null)
			Console.WriteLine ("Client connected!");

		StreamWriter writer = new StreamWriter (client.GetStream (), Encoding.ASCII);
		StreamReader reader = new StreamReader (client.GetStream (), Encoding.ASCII);

		Console.WriteLine ("Client connected! Listening for messages.");

		writer.WriteLine ("Hello client!");

		while (reader != null) {
			string my_message = reader.ReadLine ();
			if (null != my_message) {
				Console.WriteLine ("Received message: " + my_message);
				writer.WriteLine ("You sent: " + my_message); 
			}
		}



			
	}


}