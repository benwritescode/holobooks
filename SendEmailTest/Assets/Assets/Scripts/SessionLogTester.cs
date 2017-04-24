using UnityEngine;
using System.Collections;

using Configuration;

public class SessionLogTester : MonoBehaviour
{
	SessionLog log;

	void Start()
	{
		log = GameObject.FindObjectOfType<SessionLog> ();
	}

	void Update ()
	{
		
	}
}