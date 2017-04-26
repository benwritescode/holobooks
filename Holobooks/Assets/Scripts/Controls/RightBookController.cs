using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBookController : MonoBehaviour {
	public  Page LeftPage;
	public  Page RightPage;
	private int LpageNum =0;
	private int RpageNum =1;
    private string volumeId;
	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device Controller{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake(){
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	void Update () {

		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
		{
			Vector2 touchpad = (Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0));
			print("Pressing Touchpad");

			if (touchpad.y > 0.7f)
			{
				print("Moving Up");
			}

			else if (touchpad.y < -0.7f)
			{
				print("Moving Down");
			}

			if (touchpad.x > 0.7f)
			{
				turnPageToRight();
				print("Moving Right");

			}

			else if (touchpad.x < -0.7f)
			{
				turnPageToLeft();
				print("Moving left");
			}

		}
		/*if (Controller.getA())
		{
			if(gameObject.name.Contains("right")){
				turnPageToRight();
			}else{
				turnPageToLeft();
			}
			Debug.Log(gameObject.name + " Trigger Press");
		}
*/

	}

	void turnPageToLeft(){
		if(LpageNum!=0){
			LpageNum-=2;
			RpageNum-=2;
			LeftPage.changePage(LpageNum,volumeId);
			RightPage.changePage(RpageNum,volumeId);
		}
	}

	void turnPageToRight(){
		if(RpageNum!=14){
			LpageNum+=2;
			RpageNum+=2;
			LeftPage.changePage(LpageNum,volumeId);
			RightPage.changePage(RpageNum,volumeId);
		}
	}

	public void setPages(Page l, Page r,string _volumeId){
		LeftPage = l;
		RightPage = r;
        volumeId = _volumeId;
	}


}
