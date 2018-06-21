using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesManager : MonoBehaviour 
{
	[SerializeField] private SceneMessages messagesData;
	[SerializeField] private Text messagesText;

	private Notifier notifier;

	void Awake () 
	{
		this.notifier = new Notifier();
		this.notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnStateEnter);
	}

	private void HandleOnStateEnter (params object[] args)
	{
		GameState state = (GameState)args[0];
		this.UpdateMessage(state);
	}	

	private void UpdateMessage(GameState state) 
	{
		this.messagesText.text = "";
		for (int i = 0; i < this.messagesData.messages.Length; i++)
		{
			if (this.messagesData.messages[i].state == state)
			{
				this.messagesText.text = this.messagesData.messages[i].message;
			}
		}
	}
	void OnDestroy()
	{
		this.notifier.UnsubcribeAll();
	}
}
