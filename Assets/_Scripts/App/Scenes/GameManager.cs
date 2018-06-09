using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSequence
{
	public int index;
	public List <GameButton> sequence;

	public GameSequence() 
	{
		this.index = 0;
		this.sequence = new List<GameButton>();
	}

	// public AddRandomButton()
	// {
	// 	int randomIndex = Random.Range(0, )
	// 	GameButton button = 
	// 	this.sequence.Add()
	// }
}

public class GameManager : MonoBehaviour 
{
	[SerializeField] private GameButtons buttons;

	private GameSequence mPlayerSequence;

	// Use this for initialization
	void Start () 
	{
		this.mPlayerSequence = new GameSequence();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
