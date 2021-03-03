using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StringVariableCallback : ScriptableObject, ISerializationCallbackReceiver
{
	public string InitialValue;
	
	[NonSerialized]
	public string RuntimeValue;

	public string Value 
	{
		get { return RuntimeValue; } 
		set { RuntimeValue = value; }
	}

	public void OnAfterDeserialize()
	{
		Debug.Log("On After Deserialize");
		
		this.RuntimeValue = this.InitialValue;
	}

	public void OnBeforeSerialize() 
	{
		Debug.Log("On Before Serialize");
		this.InitialValue = this.RuntimeValue;
	}
}
