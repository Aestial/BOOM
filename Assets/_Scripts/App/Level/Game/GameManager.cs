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
	[SerializeField] private float startWaitTime;
	[SerializeField] private float waitTimeDecrement;
	[SerializeField] private float endTime;
	[SerializeField] private float warmTime = 2.0f;

	// Gameplay Sounds
	[Header("Sound FX")]
	[SerializeField] AudioClip correctAudioFX;
	[SerializeField] AudioClip incorrectAudioFX;

	private float energy;
	private int currentStep;
	private int currentLength;
	private float currentWaitTime;
	// [SerializeField] 
	private int currentEnergySteps;
	
	private int health;
	
	// Sequence
	private GameSequence sequence;
	private int index;

	private delegate void Callback();

	public float WaitTime
	{
		get { return currentWaitTime; }
		set { currentWaitTime = value; }
	}

	private Notifier notifier;

	void Awake ()
	{	
		this.notifier = new Notifier();
		this.notifier.Subscribe(ExplosionController.ON_EXPLOSION_PEAK, HandleOnExplosionPeak);
		this.notifier.Subscribe(ExplosionController.ON_EXPLOSION_END, HandleOnExplosionEnd);
	}
	
	void Start () 
	{
		this.Restart();
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			this.Restart();
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			this.planets.Destroy();
			this.NewPlanet();
		}
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
				AudioManager.Instance.PlayOneShoot2D(this.correctAudioFX);
				this.IncrementEnergy();
				if (this.currentStep == this.currentEnergySteps)
				{
					StateManager.Instance.State = GameState.Shoot;
					this.ship.EnableLever(true);
				}
				else
				{
					WaitingMan.Instance.WaitAndCallback(this.endTime, () => {
						this.index = 0;
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
			AudioManager.Instance.PlayOneShoot2D(this.incorrectAudioFX);
			// Wait and Restart game
			WaitingMan.Instance.WaitAndCallback(this.endTime, () => {
				this.DecreaseHealth();
				if (this.health <= 0)
				{
					StateManager.Instance.State = GameState.Loser;
					WaitingMan.Instance.WaitAndCallback(this.endTime, () => {
						StateManager.Instance.State = GameState.End;
					});
				}
				else 
				{
					this.RestartSequence();
				}
			});
		}
	}

	public void Restart()
	{
		this.health = this.maxHealth;
		this.currentEnergySteps = this.planets.startEnergySteps;
		this.currentLength = this.startLength;
		this.currentWaitTime = this.startWaitTime;
		this.SetHealth(this.health);
		this.planets.Restart();
		this.NewPlanet();
	}

	private void NewPlanet()
	{
		this.planets.NewPlanet();
		if (this.planets.count > 0)
		{
			this.currentWaitTime -= this.waitTimeDecrement / this.planets.count;
			Debug.Log(this.currentWaitTime);
			this.currentLength = this.startLength + this.planets.count / 2;
			this.currentEnergySteps = this.planets.startEnergySteps + this.planets.count / 3;
		}
		this.RestartSequence();
	}

	private void RestartSequence()
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
		for (int i = 0; i < this.currentLength; i++)
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

	private void IncrementEnergy()
	{
		this.currentStep++;
		this.SetEnergy(this.currentStep);
	}

	private void SetEnergy(int step)
	{
		this.energy = step/(float)this.currentEnergySteps;
		this.ship.SetEnergy(this.energy);
	}

	private void DecreaseHealth()
	{
		this.health--;
		this.SetHealth(this.health);
	}

	private void SetHealth(int health)
	{
		this.ship.SetHealth(this.health - 1);
	}

	private void HandleOnExplosionPeak(params object[] args)
	{
		this.planets.Destroy();
	}

	private void HandleOnExplosionEnd(params object[] args)
	{
		StateManager.Instance.State = GameState.Winner;
		WaitingMan.Instance.WaitAndCallback(this.endTime, () => {
			this.NewPlanet();
		});
	}

	void OnDestroy ()
	{
		this.notifier.UnsubcribeAll();
	}
}
