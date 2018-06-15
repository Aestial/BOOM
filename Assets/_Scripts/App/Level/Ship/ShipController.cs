using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour 
{
	[SerializeField] private ShipActorController[] actors;
	
	private Notifier notifier;

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
		for (int i = 0; i < this.actors.Length; i++)
		{
			this.actors[i].id = i;
		}
	}

	public ShipActorController GetRandomActor()
	{
		int actorLength = this.actors.Length;
		int randomIndex = Random.Range(0, actorLength);
		ShipActorController actor = this.actors[randomIndex];
		return actor;
	}
	
	public void ShowSequence(ShipActorController[] sequence)
	{
		StartCoroutine(this.ShowSequenceCoroutine(sequence));
	}

	private void VerifyInput(string name)
	{
		GameManager.Instance.VerifyPlayerInput(name);
	}

	private void SubscribeToActors(bool subscribe)
	{
		for (int i = 1; i < this.actors.Length; i++)
		{
			ShipActorController actor = this.actors[i];
			
			if(subscribe)
				actor.OnClicked += VerifyInput;
			else 
				actor.OnClicked -= VerifyInput;
		}
	}

	private void HandleOnStateEnter (params object[] args)
	{
		GameState state = (GameState)args[0];
		switch(state)
		{
			case GameState.Player:
				// this.EnableButtons(true);
				break;
			default:
				// this.EnableButtons(false);
				break;
		}
	}	

	private IEnumerator ShowSequenceCoroutine(ShipActorController[] sequence) 
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
		StateManager.Instance.State = GameState.Player;
	}
	
	private void OnDestroy()
    {
        this.notifier.UnsubcribeAll();
    }
}