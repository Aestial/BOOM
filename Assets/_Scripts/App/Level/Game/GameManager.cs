using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> 
{
	// Gameplay objects
	[Header("Objects")]
	[SerializeField] private ShipController ship;
	[SerializeField] private PlanetsController planets;

	// Gameplay parameters
	[Header("Parameters")]
	[SerializeField] private int maxHealth = 3;
	[SerializeField] private int startLength;
	[Header("Waiting Times")]
	[SerializeField] private float waitTime;
	[SerializeField] private float endTime;
	[SerializeField] private float warmTime = 2.0f;

	private float energy;
	private int currentStep;
	// [SerializeField] 
	private int energySteps;
	
	private int health;
	
	// Sequence
	private GameSequence sequence;
	private int index;

	private delegate void Callback();

	public float WaitTime
	{
		get { return waitTime; }
		set { waitTime = value; }
	}

	void Awake ()
	{
		this.health = this.maxHealth;
		this.energySteps = this.planets.energySteps;
	}

	// Use this for initialization
	void Start () 
	{
		StateManager.Instance.State = GameState.Start;
		// Sequences
		this.sequence = new GameSequence();
		// Restart helpers
		this.index = 0;
		this.currentStep = 0;
		this.SetEnergy(this.currentStep);
		// Warm and Start
		this.Warm();
	}

	void OnEnable()
    {
		// Subscribe to Actors Events
		this.ship.OnExplosionEnd += ExplosionCallback;
    }
    
    void OnDisable()
    {
		// Unsubscribe from Actors Events
		this.ship.OnExplosionEnd += ExplosionCallback;
    }
	
	public void CheckPlayerInput(string buttonName)
	{
		// Correct:
		if (this.sequence.IsSameActor(this.index, buttonName))
		{
			// Increment sequences index
			this.index++;
			// If last in sequence
			if (this.index == this.sequence.length)
			{
				// Add one button to enemy sequence and show
				StateManager.Instance.State = GameState.Correct;
				this.IncrementEnergy();
				if (this.currentStep == this.energySteps)
				{
					StateManager.Instance.State = GameState.Shoot;
					this.ship.EnableLever(true);
				}
				else
				{
					WaitingMan.Instance.WaitAndCallback(this.endTime, () => {
						this.RestartPlayer();
						this.IncrementSequence();
						this.DisplaySequence();
					});	
				}
				
			}	
		}
		// Incorrect:
		else
		{
			StateManager.Instance.State = GameState.Incorrect;
			// Wait and Restart game
			WaitingMan.Instance.WaitAndCallback(this.endTime, () => {
				this.DecreaseHealth();
				if (this.health <= 0)
				{
					StateManager.Instance.State = GameState.Loser;
				}
				else 
				{
					this.Restart();
				}
			});
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

	private void ExplosionCallback()
	{
		this.planets.DestroyPlanet();
		StateManager.Instance.State = GameState.Winner;
	}

	private void IncrementEnergy()
	{
		this.currentStep++;
		this.SetEnergy(this.currentStep);
	}

	private void SetEnergy(int step)
	{
		this.energy = step/(float)this.energySteps;
		this.ship.SetEnergy(this.energy);
	}

	private void DecreaseHealth()
	{
		this.health--;
		this.ship.SetHealth(this.health - 1);
	}

}
