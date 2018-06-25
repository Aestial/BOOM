using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoExplosionController : MonoBehaviour 
{
	[SerializeField] private AudioClip audioFX;
	[SerializeField] private float audioDelay;
	[SerializeField] private float enableVideoDelay;
	[SerializeField] private float disableVideoDelay;
	[SerializeField] private Sprite sprite;
	[SerializeField] private VideoPlayer videoPlayer;

	private new SpriteRenderer renderer;

	public delegate void EndReachedAction();
	public event EndReachedAction OnEndReached;
	
	void Start () 
	{
		this.renderer = this.GetComponent<SpriteRenderer>();
		this.renderer.sprite = this.sprite;
		this.renderer.enabled = false;
	}

	void OnEnable()
    {
		this.videoPlayer.loopPointReached += VideoEndReached;

    }
    
    void OnDisable()
    {
		this.videoPlayer.loopPointReached -= VideoEndReached;
    }

	public void Explode()
	{
		this.videoPlayer.Play();
		WaitingMan.Instance.WaitAndCallback(this.enableVideoDelay, () =>{
			this.renderer.enabled = true;
		});
		WaitingMan.Instance.WaitAndCallback(this.audioDelay, () => {
			AudioManager.Instance.PlayOneShoot2D(this.audioFX);
		});
	}

	private void VideoEndReached(VideoPlayer vp)
	{
		WaitingMan.Instance.WaitAndCallback(this.disableVideoDelay, () =>{
			vp.Stop();
			this.renderer.enabled = false;
			if (this.OnEndReached != null)
			{
				this.OnEndReached();
			}
		});
	}

}
