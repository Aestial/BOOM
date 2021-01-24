using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour 
{	
	[SerializeField] private Canvas startCanvas = default;
	[SerializeField] private Canvas endCanvas = default;
	private Notifier notifier;

	void Awake()
	{
		this.notifier = new Notifier();
		this.notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnStateEnter);
	}

	public void ButtonAction(string actionName) 
	{
		switch(actionName)
		{
			case "Menu":
				// AppManager.Instance.ChangeScene(this.mainMenuScene);
				// AudioManager.Instance.PlayOneShoot2D(bwdClip);
				break;
			case "Play":
			case "Again":
				GameManager.Instance.Restart();
				// AudioManager.Instance.PlayOneShoot2D(fwdClip);
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
		this.notifier.UnsubcribeAll();
	}
}
