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
	
	private Notifier notifier;

	private float energyAmount;

	public delegate void DisplayCallback();

	public delegate void ExplosionEndAction();
	public event ExplosionEndAction OnExplosionEnd;

	void Awake () 
	{
		this.notifier = new Notifier();
		this.notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnStateEnter);
	}

	void Start ()
	{
		this.InitializeActors();
	}


	void OnEnable()
    {
		// Subscribe to Actors Events
		this.SubscribeToActors(true);
		this.lever.OnClicked += LeverAction;
		this.gun.OnExplosionEnd += ExplosionCallback;
    }
    
    void OnDisable()
    {
		// Unsubscribe from Actors Events
		this.SubscribeToActors(false);
		this.lever.OnClicked -= LeverAction;
		this.gun.OnExplosionEnd -= ExplosionCallback;
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

	private void ExplosionEndCallback()
	{

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

	private void ExplosionCallback()
	{
		if(this.OnExplosionEnd != null)
		{
			this.OnExplosionEnd();
		}
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