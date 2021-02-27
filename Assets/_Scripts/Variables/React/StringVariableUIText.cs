using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Liquid.Variables;

public class StringVariableUIText : MonoBehaviour 
{
	[SerializeField] StringVariable variable = default;
	[SerializeField] TMP_Text text = default;

	[SerializeField] bool setOnStart = default;
	[SerializeField] bool setOnUpdate = default;
	
	private string value;

	void Start () 
	{
		value = text.text;
		if (setOnStart)
		{			
			Set();
		}
	}
	
	void Update () 
	{
		if (setOnUpdate && variable.Value != value)
		{
			Set();
		}
	}

	void Set ()
	{		
		value = value.Replace("{value}", variable.Value);
		text.text = value;
	}
}
