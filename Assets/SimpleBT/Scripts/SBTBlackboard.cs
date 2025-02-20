using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor
{
    public class SBTBlackboard : MonoBehaviour
    {
        public List<BlackboardData> GraphData = new List<BlackboardData>();
        
        private Dictionary<string, object> _data = new Dictionary<string, object>();

        private void Start() {
            foreach (BlackboardData b in GraphData) {
                b.Instantiate();
                _data.Add(b.Key, b.Value);
            }
        }

        public T GetValue<T>(string keyToGet)
        {
            object value = null;
            
            if (typeof(T) == typeof(float))
            {
                if (float.TryParse(keyToGet,
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out var newFloat)) {
                    value = newFloat;
                    return (T)value;
                }
            }
    
            else if (typeof(T) == typeof(int))
            {
                if (int.TryParse(keyToGet, out var newInt)) {
                    value = newInt;
                    return (T)value;
                }
            }
    
            else if (typeof(T) == typeof(bool))
            {
                if (bool.TryParse(keyToGet, out var newBool)) {
                    value = newBool;
                    return (T)value;
                }
            }
    
            else if (typeof(T) == typeof(string))
            {
                if(!_data.ContainsKey(keyToGet.ToUpper()))
                {
                    value = keyToGet;
                    return (T)value;
                }
            }
            
            string key = keyToGet.ToUpper();
            _data.TryGetValue(key, out value);
            return (T)value;
        }

        public bool GetRawValue(string keyToGet, out object value)
        {
            bool success = _data.TryGetValue(keyToGet.ToUpper(), out value);
            return success;
        }
        
        public Type GetValueType(string key)
        {
            if(_data.ContainsKey(key)) { return _data[key].GetType(); }

            return null;
        }
        
        public void AddValue(string key, object value) { _data.Add(key, value); }
        
        public void Set()
        {
            BlackboardData blackboardData = ScriptableObject.CreateInstance<BlackboardData>();
            blackboardData.name = "SELF";
            blackboardData.Key = "SELF";
            blackboardData.RawValue = gameObject.name;
            blackboardData.VariableType = VariableType.GameObject;
            blackboardData.Value = this;
            
            GraphData.Add(blackboardData);
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
