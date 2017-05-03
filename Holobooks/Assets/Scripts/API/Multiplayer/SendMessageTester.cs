using UnityEngine;
using System.Collections;

//https://docs.unity3d.com/Manual/JSONSerialization.html
//http://answers.unity3d.com/questions/311653/namespace-error-with-serializable.html



// System.Serializable works with classes or structs. I tested with both.
// I believe our current message format in the Unity project is classes.
// We could switch to structs if desired.
[System.Serializable]
public class MessageObject
{
	public int my_num;
	public string my_message;

	public override string ToString ()
	{
		return "[" + my_num + "," + my_message + "]";
	}

}

// Simple testing class illustrating interaction with the ClientBehavior class.
// ClientBehavior does all the heavy lifting for network interactions and threading and mutexes.
// This class can simply grab the client behavior, register as a delegate, and then start sending and receiving (serialized) JSON strings.
public class SendMessageTester : MonoBehaviour
{
	public static SendMessageTester instance;
	ClientBehavior myclientbehavior;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			return;  
	}
	// Use this for initialization
	void Start ()
	{
		myclientbehavior = GameObject.FindObjectOfType<ClientBehavior> ();
		myclientbehavior.RegisterAsDelegate (this.ReceivedStringMessage);
	
	}

	public void SendMessages ()
	{

		BookReference myReference = new BookReference ("Ben", "Ben", "Ben");

//		MessageObject some_object = new MessageObject ();
//		some_object.my_num = 5;
//		some_object.my_message = "Your test worked! Good job";

		Debug.Log ("Sending message object: " + myReference.ToString ());

		string jsonstring = JsonUtility.ToJson (myReference);

		myclientbehavior.SendStringMessage (jsonstring);

	}
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyUp ("space")) {
			
		}
	}

	void ReceivedStringMessage (string message)
	{
		Debug.Log ("attempting to convert json string: " + message);
		// Convert from JSON back into struct type
		BookReference myReference = JsonUtility.FromJson<BookReference> (message);

		Debug.Log ("Received message object: " + myReference.ToString ());
		
	}
}
