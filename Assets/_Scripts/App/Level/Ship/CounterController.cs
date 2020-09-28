using UnityEngine;
using UnityEngine.UI;

public class CounterController : MonoBehaviour 
{
	[SerializeField] private IntVariable planetCount = default;
	[SerializeField] private Text textUI = default;

	private int count;

	void Start () 
	{
		UpdateCount();
	}
	
	void Update () 
	{
		if (planetCount.RuntimeValue != count)
		{
			UpdateCount();	
		}
	}

	private void UpdateCount()
	{
		count = planetCount.RuntimeValue;
		textUI.text = count.ToString("D2");
	}
}
