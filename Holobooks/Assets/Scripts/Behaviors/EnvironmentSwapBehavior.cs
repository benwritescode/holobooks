using UnityEngine;
using System.Collections;

public class EnvironmentSwapBehavior : MonoBehaviour
{

	// primary function takes in the EnvironmentType enumeration, then switches on it to decide which environment to display.


	void changeToEnvironment (EnvironmentType eType)
	{
		switch (eType) {
		case EnvironmentType.EnvironmentTypeINVALID:
		case EnvironmentType.EnvironmentTypeEND:
			{
				// Invalid enum passed in, do nothing.
			}
		case EnvironmentType.EnvironmentTypeLibrary:
			{
				// display library environment
			}
		case EnvironmentType.EnvironmentTypeBeach:
			{
				// display beach environment
			}
		case EnvironmentType.EnvironmentTypeSnow:
			{
				// display snowy environment.
			}
		}

	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
