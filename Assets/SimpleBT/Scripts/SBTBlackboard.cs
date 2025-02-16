using System.Collections.Generic;
using UnityEngine;

namespace SimpleBT.NonEditor
{
    public class SBTBlackboard : MonoBehaviour
    {
        public List<BlackboardData> Data = new List<BlackboardData>();
        
        public T GetValue<T>(string keyToGet)
        {
            
            object value = null;
    
            /*
            if (typeof(T) == typeof(float))
            {
                if (float.TryParse(keyToGet,
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out var newFloat))
                {
                    value = newFloat;
                    return (T)value;
                }
            }
    
            else if (typeof(T) == typeof(int))
            {
                if (int.TryParse(keyToGet, out var newInt))
                {
                    value = newInt;
                    return (T)value;
                }
            }
    
            else if (typeof(T) == typeof(bool))
            {
                if (bool.TryParse(keyToGet, out var newBool))
                {
                    value = newBool;
                    return (T)value;
                }
            }
    
            else if (typeof(T) == typeof(string))
            {
                if (!_data.ContainsKey(keyToGet.ToUpper()))
                {
                    value = keyToGet;
                    return (T)value;
                }
            }
            */
            
            string key = keyToGet.ToUpper();
            //if (_data.ContainsKey(key)) { value = _data[key]; }
    
            return (T)value;
        }
    
        public void Set()
        {
            BlackboardData blackboardData = ScriptableObject.CreateInstance<BlackboardData>();
            blackboardData.name = "Self";
            blackboardData.Key = "Self";
            blackboardData.RawValue = gameObject.name;
            blackboardData.VariableType = VariableType.GameObject;
            blackboardData.Value = this;
            
            Data.Add(blackboardData);
        }
    }

    public enum VariableType
    {
        String,
        Int,
        Float,
        Bool,
        GameObject,
        Vector2,
        Vector3,
    }
}
