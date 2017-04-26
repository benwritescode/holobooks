using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BookObjectManager : MonoBehaviour {
	public Page lPage;
	public Page rPage;
	public string volumeId;


	// Use this for initialization
	void Start () {
		GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);
//		populateBook ("hvd.32044020104550");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void populateBook(string _volumeId){
		volumeId = _volumeId;
		Utils.applyMaterial (lPage, volumeId, 0);
		Utils.applyMaterial (rPage, volumeId, 1);
	}

	private void ObjectGrabbed(object sender, InteractableObjectEventArgs e){
		Debug.Log("Im Grabbed");
		GameObject rightController = GameObject.FindGameObjectWithTag ("ViveRightController");
		RightBookController rc = rightController.GetComponent<RightBookController> ();
		rc.setPages (lPage, rPage);
	}

}
