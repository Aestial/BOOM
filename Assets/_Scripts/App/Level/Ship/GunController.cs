﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunController : MonoBehaviour 
{
	[SerializeField] private BulletController[] bullets = default;

	[SerializeField] private AudioClip shootSoundFX = default;

	[SerializeField]
	UnityEvent OnExplode;
	
	private int count;

	void Start () 
	{
		this.count = 0;
	}

	void OnEnable()
    {
		this.SubscribeToBullets(true);
	 }
    
    void OnDisable()
    {
		this.SubscribeToBullets(false);
  }

	public void Shoot()
	{
		AudioManager.Instance.PlayOneShoot2D(this.shootSoundFX);
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
			OnExplode.Invoke();
		}
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.S))
		{
			this.Shoot();
		}	
	}
}
