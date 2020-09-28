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
	[SerializeField] private FillInfo empty = default;
	[SerializeField] private Color medium = default;
	[SerializeField] private FillInfo full = default;

	[SerializeField] private Image fillImage = default;

	[Range(0.0f, 1.0f)]
	[SerializeField] private float amount;

	void Start () 
	{
		Set(0.0f);
	}
	
	#if UNITY_EDITOR
	void Update () 
	{
        Set(amount);
	}
	#endif

	public void Set(float amount)
	{
		// Empty case
		if (amount <= 0.0f)
		{
            fillImage.color = empty.color;
			fillImage.fillAmount = empty.fillAmount;
		}
		// Full case
		else if (amount >= 1.0f)
		{
			fillImage.color = full.color;
			fillImage.fillAmount = full.fillAmount;
		}
		// Lower Half
		else if (amount <= 0.5f)
		{
            fillImage.fillAmount = Mathf.Lerp(empty.fillAmount, full.fillAmount, amount);
			fillImage.color = Color.Lerp(empty.color, medium, amount * 2);
		}
		else
		{
			fillImage.fillAmount = Mathf.Lerp(empty.fillAmount, full.fillAmount, amount);
            fillImage.color = Color.Lerp(medium, full.color, (amount - 0.5f) * 2);
		}
		this.amount = amount;
	}
}
