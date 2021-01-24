using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionDelay : MonoBehaviour
{
    [SerializeField] 
    private float delay = 8.0f;
    [SerializeField]
    private bool triggerOnStart = true;
    [SerializeField]
    private UnityEvent action = default;

    void Start()
    {
        if (triggerOnStart)
                Trigger();
    }

    public void Trigger()
    {
        if (delay > 0.0f)
            StartCoroutine(WaitAndAction(delay, action));
        else
            action.Invoke();
    }

    IEnumerator WaitAndAction(float delay, UnityEvent action)
	{
		yield return new WaitForSeconds(delay);
		action.Invoke();
	}

    void OnDestroy() {
        StopAllCoroutines();    
    }
}
