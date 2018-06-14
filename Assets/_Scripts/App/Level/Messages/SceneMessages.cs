using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateMessage
{
	public GameState state;
	public string message;
}

[CreateAssetMenu(fileName = "New Scene Messages", menuName = "Scene Messages")]
public class SceneMessages : ScriptableObject 
{
	public StateMessage[] messages;
}
