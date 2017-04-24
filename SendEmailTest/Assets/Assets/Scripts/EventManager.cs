using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
	SessionLog log;
	BookReference reference;

	bool sessionOptions = false;

	void OnGUI () {

		log = GameObject.FindObjectOfType<SessionLog> ();

		GUI.Box (new Rect (10, 10, 200, 170), "GUI");

		if(GUI.Button(new Rect(20,40,100,20), "New Reference")) {
			log.appendReference (reference);
			Debug.Log ("New reference added");
		}

		// Make the second button.
		if (GUI.Button (new Rect (20, 70, 100, 20), "Session Exit")) {
			sessionOptions = true;
		}
			
		if (sessionOptions) {
			if(GUI.Button(new Rect(20, 100, 100, 20), "Send Session")) {
				log.sendSessionLog ();
				Debug.Log ("Session log sent");
			}				
			if (GUI.Button (new Rect (20, 130, 100, 20), "Cancel")) {
				sessionOptions = false;
				Debug.Log ("Cancel");
			}			
		}

			

	}
}


