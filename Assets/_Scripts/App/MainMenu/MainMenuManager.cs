﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainMenuScenes
{
	Self = 1,
	Level = 2,
	Info = 3,
}

public class MainMenuManager : MonoBehaviour 
{
	[SerializeField] private AudioClip audioLoop;

	private const string audioLoopName = "MainMenuLoop";

	void Start()
	{
		AudioManager.Instance.PlayLoop2D(audioLoopName, this.audioLoop);
	}
	public void MenuButtonAction(string sceneName) 
	{
		int scene = (int)MainMenuScenes.Self;
		switch(sceneName)
		{
			case "Level":
				scene = (int)MainMenuScenes.Level;
				break;
			case "Info":
				scene = (int)MainMenuScenes.Info;
				break;
			default:
				scene = (int)MainMenuScenes.Self;
				break;
		}
		AppManager.Instance.ChangeScene(scene);
	}
	void OnDestroy()
	{
		AudioManager.Instance.StopLoop(audioLoopName);
	}
}
