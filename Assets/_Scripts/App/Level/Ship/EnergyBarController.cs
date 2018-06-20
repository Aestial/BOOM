using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct FillInfo
{
	public Color color;
	public float fillAmount;
}

[ExecuteInEditMode]
public class EnergyBarController : MonoBehaviour 
{
	[SerializeField] private FillInfo empty;
	[SerializeField] private Color medium;
	[SerializeField] private FillInfo full;

	[SerializeField] private Image fillImage;

	[Range(0.0f, 1.0f)]
	[SerializeField] private float amount;

	void Start () 
	{
		this.Set(0.0f);
	}
	
	#if UNITY_EDITOR
	void Update () 
	{
		this.Set(this.amount);
	}
	#endif

	public void Set(float amount)
	{
		// Empty case
		if (amount <= 0.0f)
		{
			this.fillImage.color = this.empty.color;
			this.fillImage.fillAmount = this.empty.fillAmount;
		}
		// Full case
		else if (amount >= 1.0f)
		{
			this.fillImage.color = this.full.color;
			this.fillImage.fillAmount = this.full.fillAmount;
		}
		// Lower Half
		else if (amount <= 0.5f)
		{
			this.fillImage.fillAmount = Mathf.Lerp(this.empty.fillAmount, this.full.fillAmount, amount);
			this.fillImage.color = Color.Lerp(this.empty.color, this.medium, amount * 2);
		}
		else
		{
			this.fillImage.fillAmount = Mathf.Lerp(this.empty.fillAmount, this.full.fillAmount, amount);
			this.fillImage.color = Color.Lerp(this.medium, this.full.color, (amount - 0.5f) * 2);
		}
		this.amount = amount;
	}
}
