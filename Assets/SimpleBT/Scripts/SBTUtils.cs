using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBT.NonEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SimpleBT.Editor.Utils
{
    public static class SBTUtils
    {
        private static Dictionary<VariableType, Type> variableTypes = new Dictionary<VariableType, Type>()
        {
            { VariableType.String, typeof(string) },
            { VariableType.Bool, typeof(bool) },
            { VariableType.Int, typeof(int) },
            { VariableType.Float, typeof(float) },
            { VariableType.GameObject, typeof(GameObject) }
        };

        public static Type ConvertToType(this VariableType variableType)
        {
            return variableTypes[variableType];
        }
        
        public static void GeneratePort(this Node node, Direction direction, Port.Capacity capacity, string name = "")
        {
            Port port = node.InstantiatePort(Orientation.Vertical, direction, capacity, typeof(bool));
            port.portName = name;
        
            if (direction == Direction.Input) { node.inputContainer.Add(port); }
            else { node.outputContainer.Add(port); }
        }

        /// <summary>
        /// Returns a string with only letters or "_"
        /// </summary>
        public static string FilterValue(this string value)
        {
            string filteredValue = new string(value.Where(
                c => Char.IsLetter(c) || // Only letters but...
                     c == '_').ToArray()); // '_' allowed

            return filteredValue;
        }

        public static object ConvertValue(this string valueToConvert, Type type)
        {
            if (type == typeof(int)) { return int.Parse(valueToConvert); }
            if (type == typeof(float)) { return float.Parse(valueToConvert); }
            if (type == typeof(bool)) { return bool.Parse(valueToConvert); }
            if (type == typeof(string)) { return valueToConvert; }
            if (type == typeof(GameObject)) { return GameObject.Find(valueToConvert); }

            Debug.LogWarning($"Couldn't return variable of type {type}. Not supported.");
            return null;
        }

        public static T GetValue<T>(this string valueToGet)
        {
            object value = null;
            
            // TODO
            
            return (T)value;
        }
    } 
}
