using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveController : MonoBehaviour {
	public  Page LeftPage;
	public  Page RightPage;
	private int LpageNum =1;
	private int RpageNum =2;

	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device Controller{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake(){
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		LeftPage.changePage(1);
		RightPage.changePage(2);
	}
	void Update () {

		if (Controller.GetAxis() != Vector2.zero)
		{
			slideBook (Controller.GetAxis ());
		}

		if (Controller.GetHairTriggerDown())
		{
			if(gameObject.name.Contains("right")){
				turnPageToRight();
			}else{
				turnPageToLeft();
			}
			Debug.Log(gameObject.name + " Trigger Press");
		}

		if (Controller.GetHairTriggerUp())
		{
			Debug.Log(gameObject.name + " Trigger Release");
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

	void turnPageToLeft(){
		if(LpageNum!=1){
			LpageNum-=2;
			RpageNum-=2;
			LeftPage.changePage(LpageNum);
			RightPage.changePage(RpageNum);
		}
	}

	void turnPageToRight(){
		if(RpageNum!=14){
			LpageNum+=2;
			RpageNum+=2;
			LeftPage.changePage(LpageNum);
			RightPage.changePage(RpageNum);
		}
	}

	void slideBook(Vector2 vec){
		LeftPage.transform.Translate (vec* 2 * Time.deltaTime);
		RightPage.transform.Translate (vec* 2 * Time.deltaTime);
	}

	void zoomBookIn(){
		LeftPage.transform.Translate (Vector3.forward* 2 * Time.deltaTime);
		RightPage.transform.Translate (Vector3.forward* 2 * Time.deltaTime);
	}

	void zoomBookOut(){
		LeftPage.transform.Translate (Vector3.back* 2 * Time.deltaTime);
		RightPage.transform.Translate (Vector3.back* 2 * Time.deltaTime);
	}

}
