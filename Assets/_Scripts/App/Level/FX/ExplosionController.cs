using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ExplosionController : MonoBehaviour 
{
	[SerializeField] private AudioClip audioFX;
	[SerializeField] private float audioDelay;
	[SerializeField] private float enableDelay;
	[SerializeField] private Sprite sprite;
	[SerializeField] private VideoPlayer videoPlayer;

	[SerializeField] private BulletController[] bullets;

	private int count = 0;
	private new SpriteRenderer renderer;
	
	void Start () 
	{
		this.count = 0;
		this.renderer = this.GetComponent<SpriteRenderer>();
		this.renderer.sprite = this.sprite;
		this.renderer.enabled = false;
	}

	void OnEnable()
    {
		this.SubscribeToBullets(true);
    }
    
    void OnDisable()
    {
		this.SubscribeToBullets(false);
    }

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.Explode();
		}
	}

	public void Explode()
	{
		this.count = 0;
		this.videoPlayer.Stop();
		this.videoPlayer.Play();
		WaitingMan.Instance.WaitAndCallback(this.enableDelay, () =>{
			this.renderer.enabled = true;
		});
		WaitingMan.Instance.WaitAndCallback(this.audioDelay, () => {
			AudioManager.Instance.PlayOneShoot2D(this.audioFX);
		});
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
			this.Explode();
		}
	}


}
