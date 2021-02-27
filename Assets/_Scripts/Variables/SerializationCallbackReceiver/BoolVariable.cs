using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolVariable : ScriptableObject, ISerializationCallbackReceiver
{
	public bool InitialValue;
	
	[NonSerialized]
	public bool RuntimeValue;

	public bool Value 
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
