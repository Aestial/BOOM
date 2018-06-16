using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour 
{
	[SerializeField] private AudioClip audioLoop;
	[SerializeField] private int mainMenuScene = 1;

	private const string audioLoopName = "InfoLoop";

	void Start() 
	{
		AudioManager.Instance.PlayLoop2D(audioLoopName, this.audioLoop);
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
	
	void OnDestroy()
	{
		AudioManager.Instance.StopLoop(audioLoopName);
	}
}
