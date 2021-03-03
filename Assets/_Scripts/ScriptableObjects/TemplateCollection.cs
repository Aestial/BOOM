using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PrefabTemplate
{
	public string name;
	public GameObject prefab;
}

[CreateAssetMenu]
public class TemplateCollection : ScriptableObject 
{
	public PrefabTemplate[] templates;
}
