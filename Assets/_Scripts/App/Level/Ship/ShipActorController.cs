using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipActorController : MonoBehaviour 
{
	[SerializeField] Color color;
	[SerializeField] float emissionIntensity;

	private Material material;

	public int id;

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
		// Debug.Log("Pressed: " + this.gameObject.name);
		this.Push(true);
		this.Illuminate(true);
  	}
	
	void OnMouseUp()
	{
		Debug.Log("Active: " + this.gameObject.name);
		this.Push(false);
		this.Illuminate(false);
		// Execute suscripted events:
		if(OnClicked != null)
			OnClicked(this.gameObject.name);
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

	public void Illuminate(bool on)
	{
		if (on) 
			this.material.EnableKeyword("_EMISSION");
		else
			this.material.DisableKeyword("_EMISSION");
	}
}
