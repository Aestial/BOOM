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
	[SerializeField] private int startLength;
	[Header("Waiting Times")]
	[SerializeField] private float waitTime;
	[SerializeField] private float endTime;
	[SerializeField] private float warmTime = 2.0f;

	public float WaitTime
	{
		get { return waitTime; }
		set { waitTime = value; }
	}

	// Sequences
	public GameSequence mPlayerSequence;
	public GameSequence mEnemySequence;
	public int playerIndex;

	private delegate void Callback();

	// Use this for initialization
	void Start () 
	{
		StateManager.Instance.State = GameState.Start;
		// Sequences
		this.mPlayerSequence = new GameSequence();
		this.mEnemySequence = new GameSequence();
		// Restart helpers
		this.playerIndex = 0;		
		// Warm and Start
		this.Warm();
	}

	private void Warm()
	{
		StateManager.Instance.State = GameState.Warm;
		WaitingMan.Instance.WaitAndCallback(this.warmTime, () => {
			this.StartEnemySequence();
			this.ShowEnemySequence();
		});
	}

	private void Restart()
	{
		this.Start();
	}

	public void VerifyPlayerInput(string buttonName)
	{
		// Correct:
		if (this.mEnemySequence.IsSameActor(playerIndex, buttonName))
		{
			Debug.Log("Well Done!!");
			// Increment sequences index
			playerIndex++;
			// Add button to player sequence
			// this.mPlayerSequence.AddActor(button);
			// If last in sequence
			if (playerIndex == this.mEnemySequence.length)
			{
				// Add one button to enemy sequence and show
				StateManager.Instance.State = GameState.Correct;
				WaitingMan.Instance.WaitAndCallback(this.endTime, () => {
					this.RestartPlayer();
					this.IncrementEnemySequence();
					this.ShowEnemySequence();
				});
				
			}
		}
		// Incorrect:
		else
		{
			Debug.Log("You loser!");
			StateManager.Instance.State = GameState.Incorrect;
			// Wait and Restart game
			WaitingMan.Instance.WaitAndCallback(this.endTime, () => {
				this.Restart();
			});
		}
	}

	private void RestartPlayer()
	{
		this.playerIndex = 0;
	}

	private void StartEnemySequence() 
	{
		for (int i = 0; i < this.startLength; i++)
		{
			this.IncrementEnemySequence();
		}
	}

	private void IncrementEnemySequence()
	{
		ShipActorController actor = this.ship.GetRandomActor();
		this.mEnemySequence.AddActor(actor);
	}

	private void ShowEnemySequence() 
	{
		StateManager.Instance.State = GameState.Enemy;
		this.ship.ShowSequence(this.mEnemySequence.sequence.ToArray());
	}

}
