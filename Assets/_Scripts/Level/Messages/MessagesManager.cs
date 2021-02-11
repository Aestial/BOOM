using UnityEngine;
using TMPro;

public class MessagesManager : MonoBehaviour 
{
	[SerializeField] private SceneMessages messagesData = default;
	[SerializeField] private TMP_Text messagesText = default;

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
		for (int i = 0; i < messagesData.messages.Length; i++)
		{
			if (messagesData.messages[i].state == state)
			{
				messagesText.text = messagesData.messages[i].message;
			}
		}
	}
	void OnDestroy()
	{
		notifier.UnsubcribeAll();
	}
}
