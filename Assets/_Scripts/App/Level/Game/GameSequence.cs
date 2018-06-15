using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequence
{
	public int length;
	public List <ShipActorController> sequence;

	public GameSequence () 
	{
		this.length = 0;
		this.sequence = new List<ShipActorController>();
	}

	public bool IsSameActor (int index, string otherName)
	{
		ShipActorController actor = this.GetActor(index);
		return actor.name == otherName;
	}

	public void AddActor (ShipActorController actor) 
	{
		this.length++;
		this.sequence.Add(actor);
		// Posible test??
		//int listLenght = this.sequence.ToArray().Length;
	}

	private ShipActorController GetActor(int index) 
	{
		return this.sequence[index];
	}
}