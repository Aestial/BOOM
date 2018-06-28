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

	public void OnAfterDeserialize()
	{
		this.RuntimeValue = this.InitialValue;
	}

	public void OnBeforeSerialize() {}
}
