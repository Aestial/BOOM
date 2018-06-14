using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> 
{
	// Gameplay parameters
	public float waitTime;
	[SerializeField] private float endTime;
	[SerializeField] private float warmTime = 2.0f;
	[SerializeField] private int startLength;

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
		this.WaitAndCallback(this.warmTime, () => {
			this.StartEnemySequence();
			this.ShowEnemySequence();
		});
		// StartCoroutine(WarmCoroutine());
	}

	private void Restart()
	{
		this.Start();
	}

	// private IEnumerator WarmCoroutine()
	// {
	// 	yield return new WaitForSeconds(this.warmTime);
	// 	this.StartEnemySequence();
	// 	this.ShowEnemySequence();
	// }

	public void VerifyPlayerInput(GameButton button)
	{
		// Correct:
		if (this.mEnemySequence.IsSameButton(playerIndex, button))
		{
			Debug.Log("Well Done!!");
			// Increment sequences index
			playerIndex++;
			// Add button to player sequence
			this.mPlayerSequence.AddButton(button);
			// If last in sequence
			if (playerIndex == this.mEnemySequence.length)
			{
				// Add one button to enemy sequence and show
				StateManager.Instance.State = GameState.Correct;
				this.WaitAndCallback(this.endTime, () => {
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
			this.WaitAndCallback(this.endTime, () => {
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
		GameButton button = this.GetRandomButton();
		this.mEnemySequence.AddButton(button);
	}

	private GameButton GetRandomButton()
	{
		int randomIndex = Random.Range(0, ButtonsManager.Instance.numButtons);
		GameButton button = ButtonsManager.Instance.mButtons[randomIndex];
		return button;
	}

	private void ShowEnemySequence() 
	{
		StateManager.Instance.State = GameState.Enemy;
		ButtonsManager.Instance.ShowSequence(this.mEnemySequence.sequence.ToArray());
	}

	#region Callback Helper

	private void WaitAndCallback(float time, Callback callback)
	{
		StartCoroutine(WaitAndCallbackCoroutine(time, callback));
	}

	private IEnumerator WaitAndCallbackCoroutine(float time, Callback callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}
	
	#endregion
}
