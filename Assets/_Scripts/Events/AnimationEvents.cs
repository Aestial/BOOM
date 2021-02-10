using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class NamedEvent 
{
    public string name;
    public UnityEvent onEvent;
}

public class AnimationEvents : MonoBehaviour 
{
    [SerializeField]
    List<NamedEvent> events;
    
    public void TriggerEvent(string name)
	{   
        foreach(NamedEvent e in events)
        {
            if (e.name == name)
            {
                e.onEvent?.Invoke();
                return;
            }
        }
        Debug.LogWarning("Event not found");        
    }

}