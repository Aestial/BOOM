using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingMan : Singleton<WaitingMan> 
{
	public delegate void Callback();

	public void WaitAndCallback(float time, Callback callback)
	{
		StartCoroutine(WaitAndCallbackCoroutine(time, callback));
	}

	private IEnumerator WaitAndCallbackCoroutine(float time, Callback callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}
	 
}
