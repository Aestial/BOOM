using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour 
{
	[SerializeField] private AudioClip audioLoop = default;
	[SerializeField] private int mainMenuScene = 1;

	private const string audioLoopName = "MainMenuLoop";

	void Start() 
	{
		AudioManager.Instance.PlayLoop2D(audioLoopName, audioLoop, 0.85f, 0.0f, false);
	}

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
