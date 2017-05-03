using UnityEngine;
using System.Collections;

//https://docs.unity3d.com/Manual/JSONSerialization.html
//http://answers.unity3d.com/questions/311653/namespace-error-with-serializable.html


// Simple testing class illustrating interaction with the ClientBehavior class.
// ClientBehavior does all the heavy lifting for network interactions and threading and mutexes.
// This class can simply grab the client behavior, register as a delegate, and then start sending and receiving (serialized) JSON strings.
public class SendBookReferenceManager : MonoBehaviour
{
	public static SendBookReferenceManager instance;
	ClientBehavior myclientbehavior;
	public GameObject menu;
	public BookSearchResultManager m;
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

	public void SendMessages (BookReference obj)
	{
		Debug.Log ("Sending message object: " + obj.ToString ());
		string jsonstring = JsonUtility.ToJson (obj);
		myclientbehavior.SendStringMessage (jsonstring);

	}


	void ReceivedStringMessage (string message)
	{
		menu.SetActive (true);
		Debug.Log ("attempting to convert json string: " + message);
		BookReference myReference = JsonUtility.FromJson<BookReference> (message);
		m.createSentObj(myReference);
	}


}
