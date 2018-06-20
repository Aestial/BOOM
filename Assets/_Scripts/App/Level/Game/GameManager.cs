using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> 
{
	// Gameplay objects
	[Header("Objects")]
	[SerializeField] private ShipController ship;

	// Gameplay parameters
	[Header("Parameters")]
	[SerializeField] private int maxLifes = 3;
	[SerializeField] private int startLength;
	[Header("Waiting Times")]
	[SerializeField] private float waitTime;
	[SerializeField] private float endTime;
	[SerializeField] private float warmTime = 2.0f;

	private int lifes;

	// Sequence
	private GameSequence sequence;
	private int index;

	private delegate void Callback();

	public float WaitTime
	{
		get { return waitTime; }
		set { waitTime = value; }
	}

	// Use this for initialization
	void Start () 
	{
		StateManager.Instance.State = GameState.Start;
		this.lifes = this.maxLifes;
		// Sequences
		this.sequence = new GameSequence();
		// Restart helpers
		this.index = 0;
		// Warm and Start
		this.Warm();
	}
	
	public void CheckPlayerInput(string buttonName)
	{
		// Correct:
		if (this.sequence.IsSameActor(this.index, buttonName))
		{
			// Increment sequences index
			this.index++;
			// Debug.Log("Correct actor in sequence!");

			// If last in sequence
			if (this.index == this.sequence.length)
			{
				// Add one button to enemy sequence and show
				StateManager.Instance.State = GameState.Correct;
				WaitingMan.Instance.WaitAndCallback(this.endTime, () => {
					this.RestartPlayer();
					this.IncrementSequence();
					this.DisplaySequence();
				});
			}	
		}
		// Incorrect:
		else
		{
			StateManager.Instance.State = GameState.Incorrect;
			// Wait and Restart game
			WaitingMan.Instance.WaitAndCallback(this.endTime, () => {
				this.Restart();
			});
			// Debug.Log("Incorrect actor :(");
		}
	}

	private void Restart()
	{
		this.Start();
	}

	private void RestartPlayer()
	{
		this.index = 0;
	}

	private void Warm()
	{
		StateManager.Instance.State = GameState.Warm;
		WaitingMan.Instance.WaitAndCallback(this.warmTime, () => {
			this.InitializeSequence();
			this.DisplaySequence();
		});
	}

	private void InitializeSequence() 
	{
		for (int i = 0; i < this.startLength; i++)
		{
			this.IncrementSequence();
		}
	}

	private void IncrementSequence()
	{
		ShipActorController actor = this.ship.GetRandomActor();
		this.sequence.AddActor(actor);
	}

	private void DisplaySequence() 
	{
		StateManager.Instance.State = GameState.Enemy;
		this.ship.DisplaySequence(this.sequence.GetArray(), this.DisplaySequenceCallback);
	}

	private void DisplaySequenceCallback()
	{
		StateManager.Instance.State = GameState.Player;
	}

}
