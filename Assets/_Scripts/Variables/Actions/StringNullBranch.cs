using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Liquid.Variables;

public class StringNullBranch : MonoBehaviour
{
    [SerializeField] StringVariable variable = default;
    [SerializeField] UnityEvent onTrue = default;
    [SerializeField] UnityEvent onFalse = default;
    [SerializeField] UnityEvent onStart = default;
    [SerializeField] bool evaluateOnStart = false;

    void Start()
    {
        onStart.Invoke();
        if (evaluateOnStart) Evaluate();
    }

    public void Evaluate()
    {
        if ( String.IsNullOrEmpty(variable.Value) )
        {  
            onTrue.Invoke();
            return;
        }
        onFalse.Invoke();        
    }
}
