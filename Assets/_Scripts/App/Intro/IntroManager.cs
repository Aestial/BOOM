﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour 
{
	[SerializeField] private AudioClip audioLoop;
	private const string audioLoopName = "MainMenuLoop";

	[SerializeField] private float waitTime;
	
	void Start () 
	{
        // Start persistent audio loop 
        AudioManager.Instance.PlayLoop2D(audioLoopName, audioLoop, 0.85f, 0.0f, false);
        // Wait time (minimum)
        WaitingMan.Instance.WaitAndCallback(waitTime,() => 
		{
			AppManager.Instance.NextScene();
		});
	}
}
