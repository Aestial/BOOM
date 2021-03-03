using System;
using UnityEngine;

namespace Liquid.Variables
{
    [Serializable]
    public class IntSerializable
    {
        public int value;
        public IntSerializable()
        {
            value = 0;
        }           
        public IntSerializable(int value)
        {
            this.value = value;
        }
    }   

    [CreateAssetMenu]
    public class IntVariable : JsonSerializer<IntVariable>
    {
        [SerializeField]
        IntSerializable serializable;

        public int Value
        {
            get 
            {
                if (serializable.value == 0)
                    serializable = GetFromFileOrCreate(() => { return new IntSerializable(); });
                Debug.Log("Getting: " + this.name + ". Value: " + serializable.value);
                return serializable.value;
            }
            set 
            { 
                if (serializable.value == 0)
                    serializable = GetFromFileOrCreate(() => { return new IntSerializable(value); });
                else
                    serializable.value = value;
                Debug.Log("Setting: " + this.name + ". Value: " + value);
                Save(serializable);
            }
        }
    }
}