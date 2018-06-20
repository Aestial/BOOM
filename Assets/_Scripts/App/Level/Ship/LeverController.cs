using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour 
{
	[SerializeField] private float maxRotation;
	[SerializeField] private AudioClip pullSoundFX;
	[SerializeField] private AudioClip releaseSoundFX;

	public new bool enabled;

	public delegate void ClickAction();
	public event ClickAction OnClicked;

	// Use this for initialization
	void Start () 
	{
		this.Set();
	}

	void OnMouseDown()
	{
		if (this.enabled)
		{
			// Debug.Log("Pressed: " + this.gameObject.name);
			this.Pull(true);
			this.PlaySound(this.pullSoundFX);
			// Execute suscripted events:
			if(this.OnClicked != null)
				this.OnClicked();
		}
  	}
	
	void OnMouseUp()
	{
		if (this.enabled)
		{
			// Debug.Log("Active: " + this.gameObject.name);
			this.Pull(false);
			this.PlaySound(this.releaseSoundFX);
		}
	}

	private void Set()
	{
		this.transform.parent.localEulerAngles = Vector3.zero;
	}

	private void Pull(bool on)
	{
		if (on)
			this.transform.parent.localEulerAngles += new Vector3(this.maxRotation, 0);
		else
			this.transform.parent.localEulerAngles -= new Vector3(this.maxRotation, 0);
	}
	
	private void PlaySound (AudioClip clip) 
	{
		AudioManager.Instance.PlayOneShoot(clip);
	}
}
