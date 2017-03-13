// I wasn't able to get MonoDevelop to recognize System.Net...
// This helped me get on the right track. I stopped using a single .cs file, and started using a .Net "solution" project to build, instead.
// http://mono.1490590.n4.nabble.com/quot-System-Net-not-found-quot-using-monodevelop-td1508820.html

// Mutex stuff: http://www.martyndavis.com/?p=440
// Good threading tutorial: http://www.mono-project.com/archived/threadsbeginnersguide/
// More on threading: http://www.mono-project.com/docs/tools+libraries/tools/gendarme/rules/concurrency/

// Tutorial on "waiting" for a signal in C#. This is useful when we are waiting for new messages to forward to other clients.

// Good tutorial on locking:

// Useful for TCP errors: https://msdn.microsoft.com/en-us/library/system.net.sockets.tcplistener.accepttcpclient(v=vs.110).aspx

// Useful tip for passing arguments to a new thread (by using a lambda function): 
// http://stackoverflow.com/questions/3360555/how-to-pass-parameters-to-threadstart-method-in-thread

using System;
using System.Collections;

// For List
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

using System.IO;
using System.Text;

// For threads
using System.Threading;

// Wraps a message
public struct MessageStruct
{
	public int clientid;
	public string message;
}


class Server
{

	const int port = 5556;
	const string my_ip = "45.56.115.75";


	Object lockMessages = new Object ();
	// MessageStruct holds a reference to basestream
	// This allows us to avoid sending a message back to the client that sent the message to us.
	private Queue<MessageStruct> messageStructsQueue = new Queue<MessageStruct> ();

	Object lockStreamWriters = new Object ();
	private List<StreamWriter> streamWriters = new List<StreamWriter> ();

	AutoResetEvent waitForNewMessages = new AutoResetEvent (false);


	public static void Main (string[] arg)
	{
		Console.WriteLine ("\n============\n\nHolobooks server starting\n============\n\n");

		// Initializing the server also starts listening for new clients.
		Server myServer = new Server ();
					
	}


	public Server ()
	{
		// initialize first threads here.
		Thread waitForNewClients = new Thread (new ThreadStart (this.WaitForNewClients));
		Thread writeMessagesToClients = new Thread (new ThreadStart (this.WriteMessagesToClients));

		waitForNewClients.Start ();
		writeMessagesToClients.Start ();


	}

	public void WaitForNewClients ()
	{

		Console.WriteLine ("WaitForNewClients:\n");
		System.Net.IPAddress localAddress = IPAddress.Parse (my_ip);

		TcpListener tcpListener = new TcpListener (localAddress, port);
		tcpListener.Start ();

		while (tcpListener != null) {
			Console.WriteLine ("WaitForNewClients: Listening for more clients...\n");

			TcpClient client = tcpListener.AcceptTcpClient ();
			if (client != null) {
				Console.WriteLine ("WaitForNewClients: New client connected!");


				StreamWriter writer = new StreamWriter (client.GetStream (), Encoding.ASCII);


				StreamReader reader = new StreamReader (client.GetStream (), Encoding.ASCII);
				lock (lockStreamWriters) {
					this.streamWriters.Add (writer);
				}
				// Start a new thread to listen to a client.
				Thread newClientListenThread = new Thread (new ThreadStart (() => this.ListenToClient (reader)));
				newClientListenThread.Start ();
				
			}
		}
	}

	public void WriteMessagesToClients ()
	{


		Queue<StreamWriter> writersToRemove = new Queue<StreamWriter> ();


		while (true) {
			// Wait for new messages before attempting to lock again.
			waitForNewMessages.WaitOne ();

			// Obtain a lock on the stream writers
			// so that we can write a message
			// Note: we are locking two locks here.
			// But since we are the only thread locking two locks, we don't have to worry about deadlock.
			// If another thread ever does need to lock two locks, just make sure they acquire the locks in the same order as this thread.

			// ENTER critical section
			lock (lockStreamWriters) {
				lock (lockMessages) {
					// Once we obtain the lock, write the message.
					while (messageStructsQueue.Count > 0) {

						MessageStruct message_struct = messageStructsQueue.Dequeue ();

						foreach (StreamWriter mywriter in this.streamWriters) {

							// Make sure the writer is still good. If it's not, we'll remove it.
							if (mywriter.BaseStream != null) {


								// check to make sure we aren't sending a message back to the sender.
								// != on the hashcode on the basestream guarantees that this message is only sent 
								// to all the other clients, instead of back to the sender.
								if (mywriter.BaseStream.GetHashCode () != message_struct.clientid) {
									try {
										Console.WriteLine ("WriteMessagesToClients: writing message: " + message_struct.message);

										mywriter.WriteLine (message_struct.message);
										// Flushing aggressively for now. 
										// This likely shouldn't cause performance issues, since our messages will have a relatively slow rate, anyway

										mywriter.Flush ();
									} catch (System.IO.IOException e) {
										writersToRemove.Enqueue (mywriter);
									}
								} else {
									// Console.WriteLine ("WriteMessagesToClients: can't write message back to client with hashcode ID: " + mywriter.BaseStream.GetHashCode ());

								}
							} else {
								// bad writer. Connection failed or client disconnected. Remove this writer from the writer list later.
								writersToRemove.Enqueue (mywriter);
							}
						}
					}
				}

				// Released lock on messages
				// But still have lock on stream writers list
				// Let's remove all the defunct stream writers with bad sockets now.
				// (we couldn't remove them earlier, because you can't alter a list while "foreach" looping through it.)
				while (writersToRemove.Count > 0) {
					Console.WriteLine ("WriteMessagesToClients: Client disconnected while attempting to write a message.");
					StreamWriter toRemove = writersToRemove.Dequeue ();
					this.streamWriters.Remove (toRemove);

				}
			}
			// LEAVE critical section


		}

	}

	public void ListenToClient (StreamReader reader)
	{
		Console.WriteLine ("ListenToClient: Client connected! Listening for messages.");

		while (reader.BaseStream != null) {
			try {

				MessageStruct my_struct;


				// wrap the message in a struct
				// this allows us to keep track of which stream a message came from

				string my_message = reader.ReadLine ();
				
				my_struct.clientid = reader.BaseStream.GetHashCode ();
				my_struct.message = my_message;

				if (null != my_struct.message) {
					Console.WriteLine ("ListenToClient: Received message: " + my_message);

					// Obtain a lock on the messages queue
					// so that we can save the new message into the queue.
					lock (lockMessages) {
						// Once we obtain the lock, save the message in the messages queue.
						messageStructsQueue.Enqueue (my_struct);
					}

					// Notify the WriteMessagesToClients thread that a new message is waiting to be written.
					waitForNewMessages.Set ();
				}
			} catch (System.IO.IOException e) {
				Console.WriteLine ("ListenToClient: Client disconnected during readLine().");
			}
		}

		Console.WriteLine ("ListenToClient: Client disconnected.");

	}

}
