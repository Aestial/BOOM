using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour 
{
	[SerializeField] private ShipActorController[] actors;
	
	private Notifier notifier;

	public delegate void DisplayCallback();

	void OnEnable()
    {
		// Subscribe to Actors Events
		this.SubscribeToActors(true);
    }
    
    void OnDisable()
    {
		// Unsubscribe from Actors Events
		this.SubscribeToActors(false);
    }

	void Awake () 
	{
		this.notifier = new Notifier();
		this.notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnStateEnter);
	}

	void Start ()
	{
		this.InitializeActors();
	}

	public ShipActorController GetRandomActor()
	{
		int actorLength = this.actors.Length;
		int randomIndex = Random.Range(0, actorLength);
		ShipActorController actor = this.actors[randomIndex];
		return actor;
	}
	
	public void DisplaySequence(ShipActorController[] sequence, DisplayCallback callback)
	{
		StartCoroutine(this.DisplaySequenceCoroutine(sequence, callback));
	}

	private void InitializeActors()
	{
		for (int i = 0; i < this.actors.Length; i++)
		{
			this.actors[i].id = i;
		}
	}

	private void EnableActors(bool enabled)
	{
		for (int i = 0; i < this.actors.Length; i++)
		{
			this.actors[i].enabled = enabled;
		}
	}

	private void SubscribeToActors(bool subscribe)
	{
		for (int i = 0; i < this.actors.Length; i++)
		{
			ShipActorController actor = this.actors[i];
			if (subscribe)
				actor.OnClicked += CheckInput;
			else 
				actor.OnClicked -= CheckInput;
		}
	}

	private void CheckInput(string name)
	{
		GameManager.Instance.CheckPlayerInput(name);
	}

	private void HandleOnStateEnter (params object[] args)
	{
		GameState state = (GameState)args[0];
		switch(state)
		{
			case GameState.Player:
				this.EnableActors(true);
				break;
			default:
				this.EnableActors(false);
				break;
		}
	}

	private IEnumerator DisplaySequenceCoroutine(ShipActorController[] sequence, DisplayCallback callback) 
	{
		float waitTime = GameManager.Instance.WaitTime;
		yield return new WaitForSeconds(waitTime);
		for (int i = 0; i < sequence.Length; i++)
		{
			int index = sequence[i].id;
			ShipActorController actor = this.actors[index];
			actor.Illuminate(true);
			yield return new WaitForSeconds(waitTime);
			actor.Illuminate(false);
			yield return new WaitForSeconds(waitTime/2);
		}
		callback();
	}
	
	private void OnDestroy()
    {
        this.notifier.UnsubcribeAll();
    }
}