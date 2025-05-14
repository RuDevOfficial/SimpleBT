using System;
using UnityEngine;

namespace SimpleBT.NonEditor
{
    // Scriptable Object generated from the SBTBlackboardGraph's exposed property list
    [System.Serializable]
    public class BlackboardData : ScriptableObject
    {
        public string Key; // The string key for the SBTBlackboard dictionary
        public string RawValue; // The raw value in string format
        public VariableType VariableType; // The type of variable that it will be converted to

        public object Value; // The object value converted from its raw value

        // Generates the value once on awake
        // Called from SBTBlackboard
        public void Instantiate()
        {
            Type type = VariableType.ConvertToType();
            var newValue = RawValue.ConvertValue(type) ?? RawValue.ConvertComplexValue(type);
            Value = newValue;
        }
    }
}
