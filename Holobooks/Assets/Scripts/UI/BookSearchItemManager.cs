using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookSearchItemManager : MonoBehaviour {
	public Text title;
	public Text author;
	public string volumeId;
	public Button downloadBtn;
	// Use this for initialization
	void Start () {
		downloadBtn.onClick.AddListener(DownloadBook);
	}
	

	public void DownloadBook(){
		Debug.Log (volumeId);
		Utils.runDownloadScript (volumeId);
	}

	public void setItem(string _title, string _author, string _volumeId){
		title.text = _title;
		author.text = _author;
		volumeId = _volumeId;
	}
}
