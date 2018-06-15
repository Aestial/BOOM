using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour 
{
	[SerializeField] private float waitTime;
	
	void Start () 
	{
		WaitingMan.Instance.WaitAndCallback(waitTime,() => 
		{
			AppManager.Instance.NextScene();
		});
	}
}
