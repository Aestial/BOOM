using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplayController : MonoBehaviour 
{
	[SerializeField] HealthLightController[] healthLights;
	private int amount;
	private int maxAmount;

	void Start () 
	{
		this.maxAmount = this.healthLights.Length - 1;
		this.amount = this.maxAmount;
	}

	private void Reset()
	{
		for (int i = 0; i <= this.maxAmount; i++)
		{
			this.healthLights[i].Illuminate(true);
		}
	}

	public void Set(int amount)
	{
		WaitingMan.Instance.WaitAndCallback(0.01f, () => {
			if (amount >= this.maxAmount)
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
		});
		
	}
}
