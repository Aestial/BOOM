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
		messagesText.text = "";
		for (int i = 0; i < messagesData.messages.Length; i++)
		{
			if (messagesData.messages[i].state == state)
			{
				messagesText.text = messagesData.messages[i].message;
			}
		}
	}
}
