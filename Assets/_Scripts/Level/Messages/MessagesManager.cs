using System;
using UnityEngine;
using TMPro;

public class MessagesManager : MonoBehaviour 
{
	[SerializeField] private SceneMessages messagesData = default;
	[SerializeField] private TMP_Text messagesText = default;
	[SerializeField] private Transform container = default;

	private Notifier notifier;

	void Awake () 
	{
		notifier = new Notifier();
		notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnStateEnter);
	}

	private void HandleOnStateEnter (params object[] args)
	{
		GameState state = (GameState)args[0];
		UpdateMessage(state);
	}	

	private void UpdateMessage(GameState state) 
	{
		messagesText.text = "";
		container.gameObject.SetActive(false);
		for (int i = 0; i < messagesData.messages.Length; i++)
		{
			if (messagesData.messages[i].state == state)
			{
				var message = messagesData.messages[i].message;
				if (!String.IsNullOrEmpty(message))
				{
					messagesText.text = message;
					container.gameObject.SetActive(true);
					return;
				}				
			}
		}
	}

	void OnDestroy()
	{
		notifier.UnsubcribeAll();
	}
}
