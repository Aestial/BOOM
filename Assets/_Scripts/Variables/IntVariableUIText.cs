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
		if (intVar.RuntimeValue != value)
			UpdateUI();
	}

	void UpdateUI ()
	{
		value = intVar.RuntimeValue;
		text.text = value.ToString(format);
	}
}
