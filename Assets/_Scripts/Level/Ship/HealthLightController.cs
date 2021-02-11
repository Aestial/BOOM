using UnityEngine;

public class HealthLightController : MonoBehaviour 
{
	[SerializeField] private MeshRenderer lightMesh = default;
	[SerializeField] private Color color = default;
	// [SerializeField] private Color offColor;
	[SerializeField] private float emissionIntensity = default;
	[SerializeField] private AudioClip soundFX = default;

	private Material material;

	void Start () 
	{
		material = lightMesh.material;
		Set();
	}

	public void Illuminate(bool on)
	{
		if (on)
		{
			material.EnableKeyword("_EMISSION");
		}
		else
		{
			material.DisableKeyword("_EMISSION");
			PlaySound();
		}
	}

	public void PlaySound()
	{
		AudioManager.Instance.PlayOneShoot2D(soundFX);
	}

	private void Set()
	{
		// this.material.color = this.color;
		// this.offColor = this.color;
		material.SetColor("_EmissionColor", color * emissionIntensity);
	}
}
