using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsController : MonoBehaviour 
{
	[SerializeField] private SpriteRenderer planet;
	
	public int energySteps = 4;

	void Start () 
	{
		this.planet.enabled = true;
	}

	public void DestroyPlanet()
	{
		this.planet.enabled = false;
	}
	
	void Update () 
	{
		
	}
}
