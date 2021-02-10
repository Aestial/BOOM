using UnityEngine;
using Liquid.Actions;

[RequireComponent(typeof(CallbackDelay))]
public class GameManager : Singleton<GameManager> 
{
	// Gameplay objects
	[Header("Objects")]
	[SerializeField] private ShipController ship = default;
	[SerializeField] private PlanetsController planets = default;

	// Gameplay parameters
	[Header("Parameters")]
	[SerializeField] private int maxHealth = 3;
	[SerializeField] private int startLength = 2;
	[Header("Waiting Times")]
	[SerializeField] private float startWaitTime = 0.5f;
	[SerializeField] private float waitTimeDecrement = 0.2f;
	[SerializeField] private float endTime = 0.8f;
	[SerializeField] private float warmTime = 2.0f;

	// Gameplay Sounds
	[Header("Sound FX")]
	[SerializeField] AudioClip correctAudioFX = default;
	[SerializeField] AudioClip incorrectAudioFX = default;

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

	private CallbackDelay delay;

	private delegate void Callback();

	public float WaitTime
	{
		get { return currentWaitTime; }
		set { currentWaitTime = value; }
	}

	private Notifier notifier;

	void Awake ()
	{	
		delay = GetComponent<CallbackDelay>();
		notifier = new Notifier();
	}
	
	void Start () 
	{
		StateManager.Instance.State = GameState.Start;
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			Restart();
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			planets.Destroy();
			NewPlanet();
		}
	}

	public void HandleOnExplosionPeak()
	{
		planets.Destroy();
	}

	public void HandleOnExplosionEnd()
	{
		StateManager.Instance.State = GameState.Winner;
		delay.Invoke(endTime, () => {
			NewPlanet();
		});
	}

	public void CheckPlayerInput(string buttonName)
	{
		// Correct:
		if (sequence.IsSameActor(index, buttonName))
		{
			// Increment sequences index
			index++;
			// If last in sequence
			if (index == sequence.length)
			{
				// Add one button to enemy sequence and show
				StateManager.Instance.State = GameState.Correct;
				AudioManager.Instance.PlayOneShoot2D(correctAudioFX);
				IncrementEnergy();
				if (currentStep == currentEnergySteps)
				{
					StateManager.Instance.State = GameState.Shoot;
					ship.EnableLever(true);
				}
				else
				{
					delay.Invoke(endTime, () => {
						index = 0;
						IncrementSequence();
						DisplaySequence();
					});	
				}
			}	
		}
		// Incorrect:
		else
		{
			StateManager.Instance.State = GameState.Incorrect;
			AudioManager.Instance.PlayOneShoot2D(incorrectAudioFX);
			// Wait and Restart game
			delay.Invoke(endTime, () => {
				DecreaseHealth();
				if (health <= 0)
				{
					StateManager.Instance.State = GameState.Loser;
					delay.Invoke(endTime, () => {
						StateManager.Instance.State = GameState.End;
					});
				}
				else 
				{
					RestartSequence();
				}
			});
		}
	}

	public void Restart()
	{
		health = maxHealth;
		currentEnergySteps = planets.startEnergySteps;
		currentLength = startLength;
		currentWaitTime = startWaitTime;
		SetHealth(health);
		planets.Restart();
		NewPlanet();
	}

	private void NewPlanet()
	{
		planets.NewPlanet();
		if (planets.count > 0)
		{
			currentWaitTime -= waitTimeDecrement / planets.count;
			currentLength = startLength + planets.count / 2;
			currentEnergySteps = planets.startEnergySteps + planets.count / 3;
		}
		RestartSequence();
	}

	private void RestartSequence()
	{
		// Sequences
		sequence = new GameSequence();
		// Restart helpers
		index = 0;
		currentStep = 0;
		SetEnergy(currentStep);
        // Warm and Start
        Warm();
	}

	private void Warm()
	{
		StateManager.Instance.State = GameState.Warm;
		delay.Invoke(warmTime, () => {
			InitializeSequence();
			DisplaySequence();
		});
	}

	private void InitializeSequence() 
	{
		for (int i = 0; i < currentLength; i++)
		{
			IncrementSequence();
		}
	}

	private void IncrementSequence()
	{
		ShipActorController actor = ship.GetRandomActor();
		sequence.AddActor(actor);
	}

	private void DisplaySequence() 
	{
		StateManager.Instance.State = GameState.Enemy;
		ship.DisplaySequence(sequence.GetArray(), DisplaySequenceCallback);
	}

	private void DisplaySequenceCallback()
	{
		StateManager.Instance.State = GameState.Player;
	}

	private void IncrementEnergy()
	{
		currentStep++;
		SetEnergy(currentStep);
	}

	private void SetEnergy(int step)
	{
		energy = step/(float)currentEnergySteps;
		ship.SetEnergy(energy);
	}

	private void DecreaseHealth()
	{
		health--;
		SetHealth(health);
	}

	private void SetHealth(int health)
	{
		ship.SetHealth(this.health - 1);
	}

	void OnDestroy ()
	{
		notifier.UnsubcribeAll();
	}
}
