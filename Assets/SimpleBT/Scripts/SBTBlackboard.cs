using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor
{
    public class SBTBlackboard : MonoBehaviour
    {
        public List<BlackboardData> Data = new List<BlackboardData>();
        
        // Fields
        public List<string> RawData = new List<string>();
    
        Dictionary<string, object> _data = new Dictionary<string, object>();
        
        // Methods
        public void Start()
        {
            FieldInfo[] fields = GetType().GetFields();
    
            foreach (FieldInfo field in fields)
            {
                _data.Add(field.Name.ToUpper(), field.GetValue(this));
            }
            
            Debug.Log(Data[0].Value);
        }

        public T GetValue<T>(string keyToGet)
        {
            object value = null;
    
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
            
            string key = keyToGet.ToUpper();
            if (_data.ContainsKey(key)) { value = _data[key]; }
    
            return (T)value;
        }
    
        public void AddNewRawVariable(string key, string value, string type)
        {
            RawData.Add(key.ToUpper() + "," + value + "," + type);
        }

        public void AddNewVariable(string key, object value)
        {
            _data.Add(key.ToUpper(), value);
        }
    }

    public enum VariableType
    {
        String,
        Int,
        Float,
        Bool,
        GameObject
    }
}
