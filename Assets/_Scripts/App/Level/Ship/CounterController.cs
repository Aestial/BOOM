using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterController : MonoBehaviour 
{
	[SerializeField] private IntVariable planetCount;
	[SerializeField] private Text textUI;

	private int count;

	void Start () 
	{
		this.UpdateCount();
	}
	
	void Update () 
	{
		if (this.planetCount.RuntimeValue != this.count)
		{
			this.UpdateCount();	
		}
	}

	private void UpdateCount()
	{
		this.count = this.planetCount.RuntimeValue;
		this.textUI.text = this.count.ToString("D2");
	}
}
