using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour 
{
	[SerializeField] private BulletController[] bullets;
	[SerializeField] private ExplosionController explosion;
	
	private int count;

	public delegate void ExplosionEndAction();
	public event ExplosionEndAction OnExplosionEnd;

	// public delegate void 

	void Start () 
	{
		this.count = 0;
	}

	void OnEnable()
    {
		this.SubscribeToBullets(true);
		this.explosion.OnEndReached += ExplosionCallback;
    }
    
    void OnDisable()
    {
		this.SubscribeToBullets(false);
		this.explosion.OnEndReached -= ExplosionCallback;
    }

	public void Shoot()
	{
		for (int i = 0; i < this.bullets.Length; i++)
		{
			this.bullets[i].Shoot();
		}
	}

	private void SubscribeToBullets(bool subscribe)
	{
		for (int i = 0; i < this.bullets.Length; i++)
		{
			BulletController actor = this.bullets[i];
			if (subscribe)
				actor.AnimationEnd += CountToExplode;
			else 
				actor.AnimationEnd -= CountToExplode;
		}
	}

	private void CountToExplode() 
	{
		this.count++;
		if (this.count >= this.bullets.Length)
		{
			this.explosion.Explode();
		}
	}

	private void ExplosionCallback()
	{
		if(this.OnExplosionEnd != null)
		{
			this.OnExplosionEnd();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			this.Shoot();
		}	
	}
}
