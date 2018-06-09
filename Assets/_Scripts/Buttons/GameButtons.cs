using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameButton 
{
	public string name;
	public Color normalColor;
	public Color activeColor;
	private int _id;
	public int id 
	{
		get { return _id; }
		set { _id = value; }
	} 
}

[CreateAssetMenu(fileName = "New Buttons Array", menuName = "Game Button Array")]
public class GameButtons : ScriptableObject 
{
	public GameButton[] buttons;
}
