using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipActorController : MonoBehaviour 
{
	[SerializeField] Color color;
	[SerializeField] float emissionIntensity;
	[SerializeField] AudioClip soundFX;

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
			// Debug.Log("Pressed: " + this.gameObject.name);
			this.Push(true);
			this.Illuminate(true);
		}
  	}
	
	void OnMouseUp()
	{
		if (this.enabled)
		{
			// Debug.Log("Active: " + this.gameObject.name);
			this.Push(false);
			this.Illuminate(false);
			this.PlaySound();
			// Execute suscripted events:
			if(OnClicked != null)
				OnClicked(this.gameObject.name);
		}
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

	private void PlaySound()
	{
		AudioManager.Instance.PlayOneShoot(this.soundFX);
	}

}
