using UnityEngine;
using Liquid.Actions;

[RequireComponent(typeof(CallbackDelay))]
public class HealthDisplayController : MonoBehaviour 
{
	[SerializeField] HealthLightController[] healthLights = default;
	private int amount;
	private int maxAmount;

	private CallbackDelay delay;

	void Start () 
	{
		delay = GetComponent<CallbackDelay>();
		maxAmount = healthLights.Length - 1;
		amount = maxAmount;
	}

	private void Reset()
	{
		for (int i = 0; i <= maxAmount; i++)
		{
			healthLights[i].Illuminate(true);
		}
	}

	public void Set(int amount)
	{
		delay.Invoke(0.01f, () => {
			if (amount >= maxAmount)
			{
				Reset();
			}
			else
			{
				for (int i = maxAmount; i > amount; i--)
				{
					healthLights[i].Illuminate(false);
				}
			}
			this.amount = amount;
		});
		
	}
}
