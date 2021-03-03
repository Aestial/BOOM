using System;
using UnityEngine;

namespace Liquid.Variables
{
    [Serializable]
    public class BoolSerializable
    {
        public bool value;
        public BoolSerializable()
        {
            value = false;
        }           
        public BoolSerializable(bool value)
        {
            this.value = value;
        }
    }   

    [CreateAssetMenu]
    public class BoolVariable : JsonSerializer<BoolVariable>
    {
        [SerializeField]
        BoolSerializable serializable;

        public bool Value
        {
            get 
            {                
                serializable = GetFromFileOrCreate(() => { return new BoolSerializable(serializable.value); });
                Debug.Log("Getting: " + this.name + ". Value: " + serializable.value);
                return serializable.value;
            }
            set 
            { 
                serializable.value = value;
                Debug.Log("Setting: " + this.name + ". Value: " + value);
                Save(serializable);
            }
        }
    }
}