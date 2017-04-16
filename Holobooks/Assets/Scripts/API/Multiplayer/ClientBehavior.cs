// unity stuff
using UnityEngine;
using System.Collections;

// network stuff
using System.Net.Sockets;
using System.IO;

//string encodings
using System.Text;

// For Lists/Queues
using System.Collections.Generic;

// For threads
using System.Threading;

// You can create a JSON serializable object by using [Serializable] in front of the class
// https://docs.unity3d.com/Manual/JSONSerialization.html

// This class is for sending and receiving strings.
using System;

using MiniJSON;

// For getting IP / port from config.json
using Configuration;

public class ClientBehavior : MonoBehaviour
{

	// TODO:
	// unfortunately, Unity doesn't support StreamReader.AsyncReadLine()
	// instead, we will have to make a thread to consume packets asynchronously.
	// need to make a thread to read lines, a buffer to store them,
	// and then within update, we should call a delegate function whenever we consume a string from the buffer.

	// https://msdn.microsoft.com/en-us/library/system.net.sockets.tcpclient(v=vs.110).aspx

	TcpClient client;

	//https://msdn.microsoft.com/en-us/library/system.io.streamreader(v=vs.110).aspx
	StreamWriter writer;

	//https://msdn.microsoft.com/en-us/library/system.io.streamwriter(v=vs.110).aspx
	StreamReader reader;



	// Lock for messages queue.
	System.Object lockMessages = new System.Object ();
	// Messages queue of strings to callback on delegate.
	// When we receive a message from the server, we enqueue it here
	// Whenever the main thread runs update(), it checks for messages here
	// then calls the callback function with the messages, so the delegate knows a message has been received.
	private Queue<string> messagesQueue = new Queue<string> ();


	// See Microsoft tutorial for delegates/callbacks here:
	// Here is the delegate type for receiving a JSONString. (not yet converted to an object.)
	public delegate void ReceivedMessageCallback (string message);

	// private pointer to the callback function to call for our delegate.
	private List<ReceivedMessageCallback> delegate_callbacks = new List<ReceivedMessageCallback> ();

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("ClientBehavior: start()");
		try {


			int port = 5556;
			string host_ip = "127.0.0.1";

			port = Config.GetInt ("port", port);
			host_ip = Configuration.Config.GetString ("ip", host_ip);

			// get TCP client to connect to server
			client = new TcpClient (host_ip, port);

			// get writer to write to server
			writer = new StreamWriter (client.GetStream (), Encoding.ASCII);

			// get reader to listen to server
			reader = new StreamReader (client.GetStream (), Encoding.ASCII);

			// Start a new thread to listen to the server
			Thread newClientListenThread = new Thread (new ThreadStart (() => this.ListenToServer (reader)));
			newClientListenThread.Start ();


			if (client != null)
				Debug.Log ("Connected to server!");
			Debug.Log ("ClientBehavior: start(): writing message");


//			writer.WriteLine ("Client says \"Hello there!\"");
//			writer.Flush ();
//			Debug.Log ("ClientBehavior: start(): wrote message");
//			Debug.Log ("Received message back: " + reader.ReadLine ());


		} catch (System.Net.Sockets.SocketException e) {
			Debug.Log ("ClientBehavior: unable to connect to server socket.");
		}

	}

	public void SendStringMessage (string message)
	{
		// write a message to send to the server and all other clients
		writer.WriteLine (message);
		writer.Flush ();
	}

	public void RegisterAsDelegate (ReceivedMessageCallback my_callback)
	{

		Debug.Log ("ClientBehavior: RegisterAsDelegate: delegate callback registered.");
		// register a new callback
		this.delegate_callbacks.Add (my_callback);
	}

	public void DeregisterDelegates ()
	{
		// clear all delegate callback functions
	}

	// Update is called once per frame
	void Update ()
	{

		// Get a lock on the messages queue lock
		lock (lockMessages) {
			// now, while there are new messages on the messages queue, dequeue them and notify our delegate via the callback function.
			if (this.delegate_callbacks.Count > 0) {
				// don't start dequeue messages unless the count is greater than 0.
				// it could be no callbacks have been registered yet.
				// this helpful for testing with just one client and a message sent at start time.
				while (messagesQueue.Count > 0) {
					string message = messagesQueue.Dequeue ();
					foreach (ReceivedMessageCallback my_callback in this.delegate_callbacks) {
						// notify every registered delegate of the new message via its callback.
						my_callback (message);
					}

				}
			}
		}
	}



	// This is our primary thread to listen to the server.
	// Very similar to the reading thread on the server.
	public void ListenToServer (StreamReader reader)
	{
		Debug.Log ("ListenToServer: Connected to server! Listening for messages.");

		while (reader.BaseStream != null) {
			try {
				// get the string message.
				string my_message = reader.ReadLine ();


				if (null != my_message) {
					Debug.Log ("ListenToServer: Received message: " + my_message);

					// Obtain a lock on the messages queue
					// so that we can save the new message into the queue.
					lock (lockMessages) {
						// Once we obtain the lock, save the message in the messages queue.
						messagesQueue.Enqueue (my_message);
					}

				}
			} catch (System.IO.IOException e) {
				Debug.Log ("ListenToServer: Client disconnected during readLine().");
			}
		}

		Debug.Log ("ListenToServer: Disconnected from server.");


	}
}
