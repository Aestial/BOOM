using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Liquid
{
    using Variables;

    public class InverseBool : MonoBehaviour
    {
        [SerializeField] BoolVariable variable = default;
        [SerializeField] UnityEvent onTrue = default;
        [SerializeField] UnityEvent onFalse = default;

        public void Evaluate(bool value)
        {
            if (value)
            {  
                onFalse.Invoke();
                return;
            }
            onTrue.Invoke();
        }
        public void Evaluate()
        {
            Evaluate(variable.Value);
        }   
    }
    
}
