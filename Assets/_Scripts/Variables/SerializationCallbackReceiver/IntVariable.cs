using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
	public int InitialValue;
	
	[NonSerialized]
	public int RuntimeValue;

	public int Value 
	{
		get { return RuntimeValue; } 
		set { RuntimeValue = value; }
	}

	public void OnAfterDeserialize()
	{
		this.RuntimeValue = this.InitialValue;
	}

	public void OnBeforeSerialize() 
	{
		this.InitialValue = this.RuntimeValue;
	}
}
