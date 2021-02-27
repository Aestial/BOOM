using System;
using UnityEngine;

namespace Liquid.Variables
{
    [Serializable]
    public class StringSerializable
    {
        public string value;
        public StringSerializable()
        {
            value = "";
        }           
        public StringSerializable(string value)
        {
            this.value = value;
        }
    }   

    [CreateAssetMenu]
    public class StringVariable : JsonSerializer<StringVariable>
    {
        [SerializeField]
        StringSerializable serializable;

        public string Value
        {
            get 
            {
                if (String.IsNullOrEmpty(serializable.value))
                    serializable = GetFromFileOrCreate(() => { return new StringSerializable(); });
                Debug.Log("Getting: " + this.name + ". Value: " + serializable.value);
                return serializable.value;
            }
            set 
            { 
                if (String.IsNullOrEmpty(serializable.value))
                    serializable = new StringSerializable(value);
                else
                    serializable.value = value;
                Debug.Log("Setting: " + this.name + ". Value: " + value);
                Save(serializable);
            }
        }
    }
}