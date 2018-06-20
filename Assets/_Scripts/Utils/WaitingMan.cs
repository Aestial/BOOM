using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingMan : Singleton<WaitingMan> 
{
	public delegate void Callback();

	#region Singleton 
	
	public bool iAmFirst;

    void Awake()
    {
       DontDestroyOnLoad(Instance);

       AudioManager[] audioManagers = FindObjectsOfType(typeof(AudioManager)) as AudioManager[];

       if(audioManagers.Length > 1)
       {
           for(int i = 0; i < audioManagers.Length; i++)
           {
               if(!audioManagers[i].iAmFirst)
               {
                   DestroyImmediate(audioManagers[i].gameObject);
               }
           }
       }
       else
       {
           iAmFirst = true;
       }
    }
	#endregion

	public void WaitAndCallback(float time, Callback callback)
	{
        if (time > 0.0f)
		    StartCoroutine(WaitAndCallbackCoroutine(time, callback));
        else
            callback();
	}

	private IEnumerator WaitAndCallbackCoroutine(float time, Callback callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}
	 
}
