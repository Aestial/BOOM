using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MaterialEmission : MonoBehaviour 
{
	[SerializeField] private Color color = default;
	[SerializeField] private float intensity = 1.0f;
	[SerializeField] private bool activeOnStart = true;

	Material material;
	MeshRenderer meshRenderer;

	void Awake() 
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	void Start () 
	{		
		material = meshRenderer.material;
		material.SetColor("_EmissionColor", color * intensity);		
		Set(activeOnStart);
	}

	public void Set(bool on)
	{
		if (on)
			material.EnableKeyword("_EMISSION");
		else
			material.DisableKeyword("_EMISSION");
	}
}