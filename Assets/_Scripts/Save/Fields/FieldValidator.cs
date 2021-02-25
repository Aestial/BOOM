using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldValidator : MonoBehaviour
{
    [SerializeField] UnityEvent onStart = default;
    [SerializeField] UnityEvent onValid = default;
    [SerializeField] UnityEvent onInvalid = default;

    public void Validate (string value)
    {
        if (String.IsNullOrEmpty(value))
        {
            onInvalid.Invoke();            
        }
        else 
        {
            onValid.Invoke();
        }
    }

    void Start()
    {
        onStart.Invoke();
    }

}
