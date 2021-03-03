using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Liquid.Variables;

public class LeaderboardVariableUIText : MonoBehaviour 
{
	[SerializeField] LeaderboardVariable variable = default;
	[SerializeField] TMP_Text text = default;

	[SerializeField] bool setOnStart = default;
	[SerializeField] bool setOnUpdate = default;
	
	private string value;

	void Start () 
	{
		if (setOnStart)
		{			
			Set();
		}
	}
	
	void Update () 
	{
		if (setOnUpdate)
		{
			Set();
		}
	}

	public void Set ()
	{
		text.text = variable.ToString();
	}
}
