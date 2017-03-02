using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBookController : MonoBehaviour {
	public GameObject book;


	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device Controller{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake(){
		trackedObj = GetComponent<SteamVR_TrackedObject>();

	}
	void Update () {

		if (Controller.GetAxis() != Vector2.zero)
		{
			slideBook (Controller.GetAxis ().x,Controller.GetAxis().y);
		}


		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
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
		Vector3 trans = new Vector3 (-x,0, -y);
		book.transform.Translate (trans* 2 * Time.deltaTime);
	}

	void zoomBookIn(){
	}

	void zoomBookOut(){
	}

}
