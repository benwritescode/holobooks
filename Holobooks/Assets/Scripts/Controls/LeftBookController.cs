using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBookController : MonoBehaviour {
	public GameObject book;
	public GameObject downloadMenu;
	public BookSearchResultManager searchMenu;

	private SteamVR_TrackedObject trackedObj;

	SpeechToText mySpeechToText;
	AudioClip clip;


	private SteamVR_Controller.Device Controller{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake(){
		mySpeechToText = GameObject.FindObjectOfType<SpeechToText> ();
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Update () {

//		if (Controller.GetAxis() != Vector2.zero)
//		{
//			slideBook (Controller.GetAxis ().x,Controller.GetAxis().y);
//		}
//
//

		if(Controller.GetPressDown(SteamVR_Controller.ButtonMask.Axis0)){
			clip = Microphone.Start (null, true, 60, 44100);
		}

		if(Controller.GetPressUp(SteamVR_Controller.ButtonMask.Axis0)){
            Debug.Log("HELLOOOO");
			Microphone.End (null);
			mySpeechToText.ConvertClipToTextWithCallback (clip, this.RecognizedText);
		}


		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
            Debug.Log("PRESS");
			if(gameObject.name.Contains("right")){
				zoomBookOut ();
			}else{
				zoomBookIn ();
			}
		}

		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
			Debug.Log(gameObject.name + " Grip Release");
		}

	}



	void slideBook(float x, float y){
		Vector3 trans = new Vector3 (x,0, y);
		book.transform.Translate (trans* 1 * Time.deltaTime);
	}

		
	void RecognizedText (string text)
	{
		Debug.Log ("SpeechToText output: " + text);
		List<BookReference> li = Utils.runSearchScript (text, "title");
		downloadMenu.SetActive (true);
		searchMenu.populateMenu (li);

	}
	void zoomBookIn(){
	}

	void zoomBookOut(){
	}
	
	

}
