
using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Events
{
    public class BoolStateEventTrigger : MonoBehaviour 
    {
        [SerializeField] BoolEvent onChange;
        [SerializeField] UnityEvent onEnabled;
        [SerializeField] UnityEvent onDisabled;

        private bool state;
        public bool State 
        {
            get { return state; }
            set {
                state = value;
                onChange.Invoke(state);
                if (state)
                    onEnabled.Invoke();
                else
                    onDisabled.Invoke();
            }
        }
    }   
}