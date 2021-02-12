
using UnityEngine;
using UnityEngine.Events;

public class BoolStateEventTrigger : MonoBehaviour 
{
    [SerializeField] BoolEvent onChange;
    [SerializeField] UnityEvent onEnabled;
    [SerializeField] UnityEvent onDisabled;

    private bool _value;
    public bool Value 
    {
        get { return _value; }
        set {
            _value = value;
            onChange.Invoke(_value);
            if (_value)
                onEnabled.Invoke();
            else
                onDisabled.Invoke();
        }
    }
}
