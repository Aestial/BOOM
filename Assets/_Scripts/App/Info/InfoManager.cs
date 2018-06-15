using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour 
{
	[SerializeField] private int mainMenuScene = 1;

	public void ButtonAction(string actionName) 
	{
		switch(actionName)
		{
			case "Back":
				AppManager.Instance.ChangeScene(this.mainMenuScene);
				break;
			default:
				Debug.Log("Triggered default, please check button onClick actions");
				break;
		}
	}
}
