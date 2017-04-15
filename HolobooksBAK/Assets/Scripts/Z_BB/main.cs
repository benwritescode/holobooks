using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class main : MonoBehaviour {
	public  Page LeftPage;
	public  Page RightPage;
	private int LpageNum =1;
	private int RpageNum =2;
	public Text logtext;


	void Start () {
		LeftPage.changePage(1);
		RightPage.changePage(2);
		logtext.text= "HELLO WORLD!";
//		GameObject page = Instantiate(Resources.Load("P")) as GameObject;
	}

	void Update () {
		if( Input.GetKeyDown( KeyCode.LeftArrow ) ){
			logtext.text = "LEFT BUTTON PRESSED";
			logtext.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
			if(LpageNum!=1){
				LpageNum-=2;
				RpageNum-=2;
				LeftPage.changePage(LpageNum);
				RightPage.changePage(RpageNum);
			}
		}

		if( Input.GetKeyUp( KeyCode.RightArrow ) ){
			logtext.text = "RIGHT BUTTON PRESSED";
			logtext.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
			if(RpageNum!=14){
				LpageNum+=2;
				RpageNum+=2;
				LeftPage.changePage(LpageNum);
				RightPage.changePage(RpageNum);
			}
		}
	}
}
