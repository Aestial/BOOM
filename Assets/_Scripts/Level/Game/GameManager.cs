using UnityEngine;
using Liquid.Actions;

[RequireComponent(typeof(CallbackDelay))]
public class GameManager : Singleton<GameManager> 
{
	// Gameplay objects
	[Header("Objects")]
	[SerializeField] private ShipController m_Ship = default;
	[SerializeField] private PlanetsController m_Planets = default;
	// Gameplay parameters
	[Header("Player parameters")]
	[SerializeField] private int m_MaxHealth = 3;
    
	[Header("Sequence parameters")]
    [SerializeField] private float m_CommandDelayDecrement = 0.1f;
    [SerializeField] private int m_EnergyStepsInverseMultiplier = 4;
    [SerializeField] private int m_SequenceCountInverseMultiplier = 3; 
	[SerializeField] private float m_StartCommandDelay = 0.5f;
	[SerializeField] private int m_StartSequenceCount = 2;

	[Header("Game loop delays")]
    [SerializeField] private float m_PlanetDelay = 0.8f;
	[SerializeField] private float m_SequenceDelay = 2.0f;
	// Gameplay Sounds
	[Header("Sound FX")]
	[SerializeField] AudioClip m_CorrectAudioFX = default;
	[SerializeField] AudioClip m_IncorrectAudioFX = default;

	private float m_Energy;
	private int m_CurrentStep;
	private int m_CurrentSequenceCount;
	private float m_CurrentCommandDelay;
	// [SerializeField] 
	private int m_CurrentEnergySteps;
	
	private int m_Health;
	
	// Sequence
	private GameSequence sequence;
	private int index;

	private CallbackDelay delay;

	private delegate void Callback();

	public float WaitTime
	{
		get { return m_CurrentCommandDelay; }
		set { m_CurrentCommandDelay = value; }
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
			m_Planets.Destroy();
			NewPlanet();
		}
	}

	public void HandleOnExplosionPeak()
	{
		m_Planets.Destroy();
	}

	public void HandleOnExplosionEnd()
	{
		StateManager.Instance.State = GameState.Winner;
		delay.Invoke(m_PlanetDelay, () => {
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
				AudioManager.Instance.PlayOneShoot2D(m_CorrectAudioFX);
				AddEnergy(1);
				if (m_CurrentStep == m_CurrentEnergySteps)
				{
					StateManager.Instance.State = GameState.Shoot;
					m_Ship.EnableLever(true);
				}
				else
				{
					delay.Invoke(m_PlanetDelay, () => {
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
			AudioManager.Instance.PlayOneShoot2D(m_IncorrectAudioFX);
			// Wait and Restart game
			delay.Invoke(m_PlanetDelay, () => {
				AddHealth(-1);
				if (m_Health <= 0)
				{
					StateManager.Instance.State = GameState.Loser;
					delay.Invoke(m_PlanetDelay, () => {
						StateManager.Instance.State = GameState.End;
					});
				}
				else 
				{
                    // Use same sequence for ease
					delay.Invoke(m_PlanetDelay, () =>
                    {
                        index = 0;
                        DisplaySequence();
                    });
                    // Legacy:
                    // RestartSequence();
                }
			});
		}
	}

	public void Restart()
	{
		m_Health = m_MaxHealth;
		m_CurrentEnergySteps = m_Planets.startEnergySteps;
		m_CurrentSequenceCount = m_StartSequenceCount;
		m_CurrentCommandDelay = m_StartCommandDelay;
		SetHealth(m_Health);
		m_Planets.Restart();
		NewPlanet();
	}

	private void NewPlanet()
	{
		m_Planets.NewPlanet();
		if (m_Planets.count > 0)
		{
			m_CurrentCommandDelay -= m_CommandDelayDecrement / m_Planets.count;
			m_CurrentSequenceCount = m_StartSequenceCount + m_Planets.count / m_SequenceCountInverseMultiplier;
			m_CurrentEnergySteps = m_Planets.startEnergySteps + m_Planets.count / m_EnergyStepsInverseMultiplier;
		}
        if (m_Planets.count % 5 == 0)
            AddHealth(1);

        RestartSequence();
	}

	private void RestartSequence()
	{
		// Sequences
		sequence = new GameSequence();
		// Restart helpers
		index = 0;
		m_CurrentStep = 0;
		SetEnergy(m_CurrentStep);
        // Warm and Start
        Warm();
	}

	private void Warm()
	{
		StateManager.Instance.State = GameState.Warm;
		delay.Invoke(m_SequenceDelay, () => {
			InitializeSequence();
			DisplaySequence();
		});
	}

	private void InitializeSequence() 
	{
		for (int i = 0; i < m_CurrentSequenceCount; i++)
		{
			IncrementSequence();
		}
	}

	private void IncrementSequence()
	{
		ShipActorController actor = m_Ship.GetRandomActor();
		sequence.AddActor(actor);
	}

	private void DisplaySequence() 
	{
		StateManager.Instance.State = GameState.Enemy;
		m_Ship.DisplaySequence(sequence.GetArray(), DisplaySequenceCallback);
	}

	private void DisplaySequenceCallback()
	{
		StateManager.Instance.State = GameState.Player;
	}

	private void AddEnergy(int increment)
	{
		this.m_CurrentStep += increment;
		SetEnergy(m_CurrentStep);
	}

	private void SetEnergy(int step)
	{
		m_Energy = step/(float)m_CurrentEnergySteps;
		m_Ship.SetEnergy(m_Energy);
	}

	private void AddHealth(int increment)
    {
        this.m_Health += increment;
		if (this.m_Health > m_MaxHealth)
			this.m_Health = m_MaxHealth;

        SetHealth(m_Health);
    }

	private void SetHealth(int m_Health)
	{
		m_Ship.SetHealth(this.m_Health - 1);
	}

	void OnDestroy ()
	{
		notifier.UnsubcribeAll();
	}
}
