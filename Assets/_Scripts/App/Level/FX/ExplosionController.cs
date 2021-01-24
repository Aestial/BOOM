using UnityEngine;
using Liquid.Actions;

[RequireComponent(typeof(CallbackDelay))]
public class ExplosionController : MonoBehaviour 
{
	[SerializeField] private AudioClip audioFX = default;
	[SerializeField] private float audioDelay = default;
	[SerializeField] private new AnimationController animation = default;
	
	private SpriteRenderer[] sprites;
	private new SpriteRenderer renderer;

	private CallbackDelay delay;

	private Notifier notifier;
	public const string ON_EXPLOSION_PEAK = "OnExplosionPeak";
	public const string ON_EXPLOSION_END = "OnExplosionEnd";

	void Awake ()
	{
		notifier = new Notifier();
		delay = GetComponent<CallbackDelay>();
	}
	
	void Start () 
	{
		sprites = animation.GetComponentsInChildren<SpriteRenderer>();
		EnableSprites(false);
	}

	void OnEnable()
    {
		animation.OnAnimationEnd += AnimationEndReached;
		animation.OnAnimationEvent += AnimationEventReached;
    }
    
    void OnDisable()
    {
		animation.OnAnimationEnd -= AnimationEndReached;
		animation.OnAnimationEvent -= AnimationEventReached;
    }

	public void Explode()
	{
		EnableSprites(true);
		animation.Play();
		delay.Invoke(audioDelay, () => {
			AudioManager.Instance.PlayOneShoot2D(audioFX);
		});
	}

	private void EnableSprites(bool enabled)
	{
		for (int i = 0; i < sprites.Length; i++)
		{
			sprites[i].enabled = enabled;
		}
	}

	private void AnimationEventReached(string name)
	{
		// Debug.Log("Explosion Controller: Peak animation reached.");
		switch(name)
		{
			case "Peak":
			notifier.Notify(ON_EXPLOSION_PEAK);
			break;
			default:
			break;
		}
	}

	private void AnimationEndReached()
	{
		// Debug.Log("Explosion Controller: End of animation reached.");
		EnableSprites(false);
		notifier.Notify(ON_EXPLOSION_END);
	}

	void OnDestroy ()
	{
		notifier.UnsubcribeAll();
	}

}
