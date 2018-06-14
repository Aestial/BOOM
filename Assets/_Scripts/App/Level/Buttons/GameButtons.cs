using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameButton 
{
	public string name;
	public Color normalColor;
	public Color activeColor;
}

[CreateAssetMenu(fileName = "New Buttons Array", menuName = "Game Button Array")]
public class GameButtons : ScriptableObject 
{
	public GameButton[] buttons;
}
