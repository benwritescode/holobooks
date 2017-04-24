using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
	SessionLog log;
	BookReference reference;

	void OnGUI () {

		log = GameObject.FindObjectOfType<SessionLog> ();

		GUI.Box (new Rect (10, 10, 100, 90), "GUI");

		if(GUI.Button(new Rect(20,40,80,20), "New Reference")) {
			log.appendReference (reference);
			Debug.Log ("New reference added");
		}

		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Session Exit")) {
			log.sendSessionLog ();
			Debug.Log ("Session log sent");
		}
	}
}


