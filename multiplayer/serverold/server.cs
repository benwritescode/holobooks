using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;

using System.IO;
using System.Text;

// For those threads
using System.Threading;



class Server
{

	const int port = 5555;
	const string my_ip = "127.0.0.1";
	Object lockMessageQueue = new Object ();


	Object lockStreamWriters = new object ();
	List<StreamWriter> streamWriters = new List<StreamWriter> ();



	public static void Main (string[] arg)
	{
		Console.WriteLine ("\n============\n\nHolobooks server starting\n============\n\n");



		this.StartThreads ();



		// start listening for clients.




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


	public void StartThreads ()
	{
		Thread listen = new Thread (new ThreadStart (this.ListenForClients));

		listen.Start ();

		// wait for listen
		listen.Join ();
	}

	public void ListenForClients ()
	{

		Console.WriteLine ("server: ListenForClients()\n");

		System.Net.IPAddress localAddress = IPAddress.Parse (my_ip);

		TcpListener tcpListener = new TcpListener (localAddress, port);

		while (tcpListener != null) {
			TcpClient client = tcpListener.AcceptTcpClient ();
			if (client != null) {
				Console.WriteLine ("New client connected!");



				StreamWriter writer = new StreamWriter (client.GetStream (), Encoding.ASCII);


				StreamReader reader = new StreamReader (client.GetStream (), Encoding.ASCII);
				lock (lockStreamWriters) {

					this.streamWriters.Insert (writer);
				}

				
			}
		}
			


	}

	public void ListenToClient (StreamReader reader)
	{


	}

}