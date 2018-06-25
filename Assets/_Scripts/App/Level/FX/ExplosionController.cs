using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ExplosionController : MonoBehaviour 
{
	[SerializeField] private AudioClip audioFX;
	[SerializeField] private float audioDelay;
	[SerializeField] private new AnimationController animation;
	
	private SpriteRenderer[] sprites;
	private new SpriteRenderer renderer;

	private Notifier notifier;
	public const string ON_EXPLOSION_PEAK = "OnExplosionPeak";
	public const string ON_EXPLOSION_END = "OnExplosionEnd";

	void Awake ()
	{
		this.notifier = new Notifier();
	}
	
	void Start () 
	{
		this.sprites = this.animation.GetComponentsInChildren<SpriteRenderer>();
		this.EnableSprites(false);
	}

	void OnEnable()
    {
		this.animation.OnAnimationEnd += this.AnimationEndReached;
		this.animation.OnAnimationEvent += this.AnimationEventReached;
    }
    
    void OnDisable()
    {
		this.animation.OnAnimationEnd -= AnimationEndReached;
		this.animation.OnAnimationEvent -= this.AnimationEventReached;
    }

	public void Explode()
	{
		this.EnableSprites(true);
		this.animation.Play();
		WaitingMan.Instance.WaitAndCallback(this.audioDelay, () => {
			AudioManager.Instance.PlayOneShoot2D(this.audioFX);
		});
	}

	private void EnableSprites(bool enabled)
	{
		for (int i = 0; i < this.sprites.Length; i++)
		{
			this.sprites[i].enabled = enabled;
		}
	}

	private void AnimationEventReached(string name)
	{
		// Debug.Log("Explosion Controller: Peak animation reached.");
		switch(name)
		{
			case "Peak":
			this.notifier.Notify(ON_EXPLOSION_PEAK);
			break;
			default:
			break;
		}
	}

	private void AnimationEndReached()
	{
		// Debug.Log("Explosion Controller: End of animation reached.");
		this.EnableSprites(false);
		this.notifier.Notify(ON_EXPLOSION_END);
	}

	void OnDestroy ()
	{
		this.notifier.UnsubcribeAll();
	}

}
