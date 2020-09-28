using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlanetTemplate
{
	public string name;
	public GameObject prefab;
}

[CreateAssetMenu]
public class PlanetsData : ScriptableObject 
{
	public int incrementStepCount;
	public PlanetTemplate[] templates;
}
