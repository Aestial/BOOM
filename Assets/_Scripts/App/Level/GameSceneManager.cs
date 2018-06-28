using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour 
{
	[SerializeField] private AudioClip audioLoop;
	[SerializeField] private int mainMenuScene = 1;

	[SerializeField] private Canvas startCanvas;
	[SerializeField] private Canvas endCanvas;

	private const string audioLoopName = "LevelLoop";
	private Notifier notifier;

	void Awake()
	{
		this.notifier = new Notifier();
		this.notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnStateEnter);
	}

	void Start()
	{	
		AudioManager.Instance.PlayLoop2D(audioLoopName, this.audioLoop);
	}

	public void ButtonAction(string actionName) 
	{
		switch(actionName)
		{
			case "Menu":
				AppManager.Instance.ChangeScene(this.mainMenuScene);
				break;
			case "Play":
			case "Again":
				GameManager.Instance.Restart();
				break;
			default:
				Debug.Log("Triggered default, please check button onClick actions");
				break;
		}
	}

	private void HandleOnStateEnter (params object[] args)
	{
		GameState state = (GameState)args[0];
		this.startCanvas.enabled = state == GameState.Start;
		this.endCanvas.enabled = state == GameState.End;
	}
	
	void OnDestroy()
	{
		AudioManager.Instance.StopLoop(audioLoopName);
		this.notifier.UnsubcribeAll();
	}
}
