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
	
		logtext.text= "HELLO WORLD!";
//		GameObject page = Instantiate(Resources.Load("P")) as GameObject;
	}

	void Update () {

	}
}
