using UnityEngine;
using UnityEngine.Video;

public class VideoExplosionController : MonoBehaviour 
{
	[SerializeField] private AudioClip audioFX = default;
	[SerializeField] private float audioDelay = default;
	[SerializeField] private float enableVideoDelay = default;
	[SerializeField] private float disableVideoDelay = default;
	[SerializeField] private Sprite sprite = default;
	[SerializeField] private VideoPlayer videoPlayer = default;

	private new SpriteRenderer renderer;

	public delegate void EndReachedAction();
	public event EndReachedAction OnEndReached;
	
	void Start () 
	{
		renderer = GetComponent<SpriteRenderer>();
		renderer.sprite = sprite;
		renderer.enabled = false;
	}

	void OnEnable()
    {
		videoPlayer.loopPointReached += VideoEndReached;

    }
    
    void OnDisable()
    {
		videoPlayer.loopPointReached -= VideoEndReached;
    }

	public void Explode()
	{
		videoPlayer.Play();
		WaitingMan.Instance.WaitAndCallback(enableVideoDelay, () =>{
			renderer.enabled = true;
		});
		WaitingMan.Instance.WaitAndCallback(audioDelay, () => {
			AudioManager.Instance.PlayOneShoot2D(audioFX);
		});
	}

	private void VideoEndReached(VideoPlayer vp)
	{
		WaitingMan.Instance.WaitAndCallback(disableVideoDelay, () =>{
			vp.Stop();
			renderer.enabled = false;
            OnEndReached?.Invoke();
        });
	}

}
