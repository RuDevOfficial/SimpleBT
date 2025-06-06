using System.Collections.Generic;
using UnityEngine;

namespace SimpleBT.NonEditor
{
    public class SBTBlackboard : MonoBehaviour
    {
        public List<BlackboardData> GraphData = new List<BlackboardData>();
        
        private Dictionary<string, object> _data = new Dictionary<string, object>();

        private void Awake() {
            foreach (BlackboardData b in GraphData) {
                b.Instantiate();
                _data.Add(b.Key, b.Value);
            }
            
            // For easy access to the agent that holds the blackboard
            _data.Add("SELF", gameObject);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// Returns a literal value of T if there is no key in the Dictionary.
        /// Use this for simple classes such as int, float, bool or Vectors.
        /// </summary>
        /// <param name="keyToGet">Dictionary Key</param>
        /// <typeparam name="T">Type Returned</typeparam>
        /// <returns></returns>
        public T GetValue<T>(string keyToGet)
        {
            if (_data.ContainsKey(keyToGet.ToUpper())) {
                _data.TryGetValue(keyToGet.ToUpper(), out object value);
                return (T)value;
            }
            else
            {
                object value = SBTNonEditorUtils.GetLiteral<T>(keyToGet);
                return (T)value;
            }
        }

        public bool ContainsKey(string key) { return _data.ContainsKey(key.ToUpper()); }

        /// <summary>
        /// Returns a literal value of T if the key is not in the dictionary.
        /// Use this instead of GetValue for complex classes such as GameObject
        /// </summary>
        /// <param name="keyToGet">Key of the dictionary</param>
        /// <param name="parameters">Array of extra parameters</param>
        /// <typeparam name="T">Type Returned</typeparam>
        /// <returns></returns>
        public T GetComplexValue<T>(string keyToGet)
        {
            string key = keyToGet.ToUpper();
            
            if (_data.ContainsKey(key)) {
                _data.TryGetValue(key, out object value);
                return (T)value;
            }
            else
            {
                object value = SBTNonEditorUtils.GetComplexLiteral<T>(keyToGet);
                return (T)value;
            }
        }

        public bool GetRawValue(string keyToGet, out object value)
        {
            bool success = _data.TryGetValue(keyToGet.ToUpper(), out value);
            return success;
        }

        public void AddValue(string key, object value)
        {
            key = key.ToUpper();
            
            if (_data.ContainsKey(key)) { _data[key] = value; }
            else { _data.Add(key, value); }
        }

        public void RemoveValue(string key)
        {
            key = key.ToUpper();
            
            if (_data.ContainsKey(key)) { _data.Remove(key); }
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
