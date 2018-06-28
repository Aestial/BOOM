using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLightController : MonoBehaviour 
{
	[SerializeField] private MeshRenderer lightMesh;
	[SerializeField] private Color color;
	// [SerializeField] private Color offColor;
	[SerializeField] private float emissionIntensity;
	[SerializeField] private AudioClip soundFX;

	private Material material;

	void Start () 
	{
		this.material = this.lightMesh.material;
		this.Set();
	}

	public void Illuminate(bool on)
	{
		if (on)
		{
			this.material.EnableKeyword("_EMISSION");
		}
		else
		{
			this.material.DisableKeyword("_EMISSION");
			this.PlaySound();
		}
	}

	public void PlaySound()
	{
		AudioManager.Instance.PlayOneShoot2D(this.soundFX);
	}

	private void Set()
	{
		// this.material.color = this.color;
		// this.offColor = this.color;
		this.material.SetColor("_EmissionColor", this.color * this.emissionIntensity);
	}
}
