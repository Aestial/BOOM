using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateChangeHandler : MonoBehaviour 
{	
	[SerializeField] 
	private GameState state = default;
	[SerializeField] 
	private UnityEvent trueAction = default;
	[SerializeField] 
	private UnityEvent falseAction = default;
	
	private Notifier notifier;

	void Awake()
	{
		this.notifier = new Notifier();
		this.notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnStateEnter);
	}

	private void HandleOnStateEnter (params object[] args)
	{
		GameState state = (GameState)args[0];
		if (state == this.state)
			trueAction.Invoke();
		else 
			falseAction.Invoke();
	}
	
	void OnDestroy()
	{
		this.notifier.UnsubcribeAll();
	}
}
