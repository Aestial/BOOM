using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequence
{
	public int length;
	public List <GameButton> sequence;

	public GameSequence() 
	{
		this.length = 0;
		this.sequence = new List<GameButton>();
	}

	private GameButton GetButton(int index) 
	{
		return this.sequence[index];
	}

	public bool IsSameButton (int index, GameButton other)
	{
		GameButton button = this.GetButton(index);
		return button.name == other.name;
	}

	public void AddButton(GameButton button) 
	{
		this.length++;
		this.sequence.Add(button);
		// Posible test??
		//int listLenght = this.sequence.ToArray().Length;
	}
}