using UnityEngine;
using System.Collections;

public class Page : MonoBehaviour {
	
	public void changePage(int number, string volumeId){
		Utils.applyMaterial(this, volumeId+"_"+number.ToString(),number);
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
