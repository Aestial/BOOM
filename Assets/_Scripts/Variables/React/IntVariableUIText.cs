using UnityEngine;
using UnityEngine.UI;

public class IntVariableUIText : MonoBehaviour 
{
	[SerializeField] IntVariable intVar = default;
	[SerializeField] Text text = default;
	[SerializeField] string format = "D2";

	private int value;

	void Start () 
	{
		UpdateUI();
	}
	
	void Update () 
	{
		if (intVar.Value != value)
			UpdateUI();
	}

	void UpdateUI ()
	{
		value = intVar.Value;
		text.text = value.ToString(format);
	}
}
