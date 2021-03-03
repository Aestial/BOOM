using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Liquid
{
    using Variables;

    public class BoolBranch : MonoBehaviour
    {
        [SerializeField] BoolVariable variable = default;
        [SerializeField] UnityEvent onTrue = default;
        [SerializeField] UnityEvent onFalse = default;
        [SerializeField] UnityEvent onStart = default;

        // Start is called before the first frame update
        void Start()
        {
            onStart.Invoke();
        }

        public void Evaluate()
        {
            if (variable.Value)
            {  
                onTrue.Invoke();
                return;
            }
            onFalse.Invoke();        
        }
    }
    
}
