using System;
using UnityEngine;

namespace SimpleBT.NonEditor
{
    [System.Serializable]
    public class BlackboardData : ScriptableObject
    {
        public string Key;
        public string RawValue;
        public VariableType VariableType;

        public object Value;

        public void Instantiate()
        {
            Type type = VariableType.ConvertToType();
            var newValue = RawValue.ConvertValue(type, Key);
            Value = newValue;
        }

        void Awake() { Instantiate(); }
    }
}
