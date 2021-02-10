using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableChildRenderer : MonoBehaviour
{
    [SerializeField] bool enabledOnStart;
    Renderer[] renderers;

    public void EnableChildren(bool enabled)
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = enabled;
		}
	}

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();    
    }

    void Start()
    {
        EnableChildren(enabledOnStart);
    }    
}
