using System;
using SimpleBT.Editor.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor
{
    [System.Serializable]
    public class BlackboardData : ScriptableObject
    {
        [FormerlySerializedAs("Field")] public string Key;
        public string RawValue;
        public VariableType VariableType;

        public object Value;

        public void Instantiate()
        {
            Type type = VariableType.ConvertToType();
            var newValue = RawValue.ConvertValue(type);
            Value = newValue;
        }

        private void Awake() { Instantiate(); }
    }
}
