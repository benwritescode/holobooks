using UnityEngine;
using System.Collections;

public class Page : MonoBehaviour {
	
	public void changePage(int number){
		Utils.applyMaterial(this, "n"+number.ToString());
	}
	// Use this for initialization
	void Start () {
//		changePage(1);
//		Utils.applyMaterial(gameObject, "page-5");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
