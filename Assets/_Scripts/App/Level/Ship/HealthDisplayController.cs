using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplayController : MonoBehaviour 
{
	[SerializeField] HealthLightController[] healthLights;

	// [Range(0, 3)][SerializeField] 
	private int amount;
	private int maxAmount;

	void Start () 
	{
		this.maxAmount = this.healthLights.Length - 1;
		this.amount = this.maxAmount;
		// Turn all on
		WaitingMan.Instance.WaitAndCallback(0.01f, () => {
			this.Set(amount);
		});	
	}

	private void Reset()
	{
		for (int i = 0; i < this.healthLights.Length; i++)
		{
			this.healthLights[i].Illuminate(true);
		}
	}

	public void Set(int amount)
	{
		if (amount >= maxAmount)
		{
			this.Reset();
		}
		else
		{
			for (int i = this.maxAmount; i > amount; i--)
			{
				this.healthLights[i].Illuminate(false);
			}
		}
		this.amount = amount;
	}
}
