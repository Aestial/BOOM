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

	public void Explode()
	{
		EnableSprites(true);
		animation.Play();
		delay.Invoke(audioDelay, () => {
			AudioManager.Instance.PlayOneShoot2D(audioFX);
		});
	}

	public void EnableSprites(bool enabled)
	{
		for (int i = 0; i < sprites.Length; i++)
		{
			sprites[i].enabled = enabled;
		}
	}

	private void AnimationEndReached()
	{
		// Debug.Log("Explosion Controller: End of animation reached.");
		EnableSprites(false);
	}

	void OnDestroy ()
	{
		notifier.UnsubcribeAll();
	}

}
