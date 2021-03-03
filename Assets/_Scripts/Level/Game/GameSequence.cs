using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequence
{
	public int length;
	public List <ShipActorController> list;

	public GameSequence () 
	{
		this.length = 0;
		this.list = new List<ShipActorController>();
	}

	public void AddActor (ShipActorController actor) 
	{
		this.length++;
		this.list.Add(actor);
	}

	public bool IsSameActor (int index, string otherName)
	{
		ShipActorController actor = this.GetActor(index);
		return actor.name == otherName;
	}

	public ShipActorController[] GetArray()
	{
		return this.list.ToArray();
	}	

	private ShipActorController GetActor(int index) 
	{
		return this.list[index];
	}
}