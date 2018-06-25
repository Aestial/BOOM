using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour 
{
	[SerializeField] private ShipActorController[] actors;
	[SerializeField] private LeverController lever;
	[SerializeField] private GunController gun;
	[SerializeField] private EnergyBarController energyBar;
	[SerializeField] private HealthDisplayController healthDisplay;
	[Header("FX")]
	[SerializeField] private ParticleSystem fireFX;
	
	private Notifier notifier;

	private float energyAmount;

	public delegate void DisplayCallback();

	void Awake () 
	{
		this.notifier = new Notifier();
		this.notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnStateEnter);
	}

	void Start ()
	{
		this.SetFire(false);
		this.InitializeActors();
	}


	void OnEnable()
    {
		// Subscribe to Actors Events
		this.SubscribeToActors(true);
		this.lever.OnClicked += LeverAction;
    }
    
    void OnDisable()
    {
		// Unsubscribe from Actors Events
		this.SubscribeToActors(false);
		this.lever.OnClicked -= LeverAction;
    }
	
	public void DisplaySequence(ShipActorController[] sequence, DisplayCallback callback)
	{
		StartCoroutine(this.DisplaySequenceCoroutine(sequence, callback));
	}

	public void EnableLever(bool enabled)
	{
		this.lever.enabled = enabled;
	}

	public ShipActorController GetRandomActor()
	{
		int actorLength = this.actors.Length;
		int randomIndex = Random.Range(0, actorLength);
		ShipActorController actor = this.actors[randomIndex];
		return actor;
	}

	public void SetHealth(int health)
	{
		this.healthDisplay.Set(health);
	}

	private void CheckInput(string name)
	{
		GameManager.Instance.CheckPlayerInput(name);
	}


	private void EnableActors(bool enabled)
	{
		for (int i = 0; i < this.actors.Length; i++)
		{
			this.actors[i].enabled = enabled;
		}
	}

	private void SetFire(bool on)
	{
		if (on)
			this.fireFX.Play();
		else
			this.fireFX.Stop();
	}

	private void InitializeActors()
	{
		for (int i = 0; i < this.actors.Length; i++)
		{
			this.actors[i].id = i;
		}
	}

	private void LeverAction()
	{
		this.gun.Shoot();
		this.SetEnergy(0);
	}

	public void SetEnergy(float value)
	{
		this.energyAmount = value;
		this.energyBar.Set(value);
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

	private void HandleOnStateEnter (params object[] args)
	{
		GameState state = (GameState)args[0];
		this.EnableActors(state == GameState.Player);
		this.SetFire(state == GameState.Loser || state == GameState.End);
		switch(state)
		{
			default:
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
			actor.Show(true);
			yield return new WaitForSeconds(waitTime);
			actor.Show(false);
			yield return new WaitForSeconds(waitTime/2);
		}
		callback();
	}
	
	private void OnDestroy()
    {
        this.notifier.UnsubcribeAll();
    }
}