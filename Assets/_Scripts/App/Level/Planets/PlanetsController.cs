using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsController : MonoBehaviour 
{
	[SerializeField] private SpriteRenderer planet;
	[SerializeField] private IntVariable planetCount;
	[SerializeField] private PlanetsData data;
	
	public int startEnergySteps = 2;

	private int m_Count;

	public int count
	{
		get { return this.m_Count; }
		set 
		{ 
			this.m_Count = value;
			this.planetCount.RuntimeValue = value;
		}
	}

	void Start () 
	{
		this.m_Count = this.planetCount.RuntimeValue;
		this.planet.enabled = true;
	}

	public void NewPlanet()
	{
		this.ViewPlanet(true);
		int length = this.data.templates.Length;
		int index = Random.Range(0, length);
		this.planet.sprite = this.data.templates[index].sprite;
		Debug.Log("New Planet: " + this.data.templates[index].name + " Index: " + index);
	}
	
	public void Destroy()
	{
		this.ViewPlanet(false);
		this.count++;
	}

	public void Restart()
	{
		this.count = 0;
	}

	private void ViewPlanet(bool on)
	{
		this.planet.enabled = on;
	}
}
