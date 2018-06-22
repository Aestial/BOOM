using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonAction
{
	Show,
	Push,
}

public class ShipActorController : MonoBehaviour 
{
	[SerializeField] private Color color;
	[SerializeField] private float emissionIntensity;
	[SerializeField] private AudioClip showSoundFX;
	[SerializeField] private AudioClip pushSoundFX;

	private Material material;

	public int id;
	public new bool enabled;

	public delegate void ClickAction(string name);
    public event ClickAction OnClicked;

	void Start () 
	{
		this.material = this.GetComponent<MeshRenderer>().material;
		this.Set();
		this.Illuminate(false);
	}
	void OnMouseDown()
	{
		if (this.enabled)
		{
			this.Push(true);
			this.Illuminate(true);
		}
  	}
	
	void OnMouseUp()
	{
		if (this.enabled)
		{
			this.Push(false);
			this.Illuminate(false);
			this.PlaySound(ButtonAction.Push, 1.18f);
			this.PlaySound(ButtonAction.Show, 0.83f);
			// Execute suscripted events:
			if(this.OnClicked != null)
				this.OnClicked(this.gameObject.name);
		}
	}

	public void Show(bool on)
	{
		this.Illuminate(on);
		if (on)
			this.PlaySound(ButtonAction.Show, 0.9f);
	}

	public void Illuminate(bool on)
	{
		if (on) 
			this.material.EnableKeyword("_EMISSION");
		else
			this.material.DisableKeyword("_EMISSION");
	}

	private void Set()
	{
		this.material.color = this.color;
		this.material.SetColor("_EmissionColor", this.color * this.emissionIntensity);
	}

	private void Push(bool on)
	{
		if (on)
			this.transform.localPosition += new Vector3(0.0f, 0.15f);
		else
			this.transform.localPosition -= new Vector3(0.0f, 0.15f);
	}

	private void PlaySound(ButtonAction action)
	{
		this.PlaySound(action, 1.0f);
	}

	private void PlaySound(ButtonAction action, float volume)
	{
		AudioClip clip;
		switch(action)
		{
			case ButtonAction.Show:
			clip = this.showSoundFX;
			break;
			case ButtonAction.Push:
			clip = this.pushSoundFX;
			break;
			default:
			clip = this.showSoundFX;
			break;
		}
		AudioManager.Instance.PlayOneShoot(clip, volume);
	}

}
