using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Actions 
{
	public class CallbackDelay : MonoBehaviour
	{
		public delegate void Callback();

		public void Invoke(float delay, Callback callback)
		{
			if (delay > 0.0f)
				StartCoroutine(InvokeCoroutine(delay, callback));
			else
				callback();
		}

		private IEnumerator InvokeCoroutine(float delay, Callback callback)
		{
			yield return new WaitForSeconds(delay);
			callback();
		}
		
	}
}
