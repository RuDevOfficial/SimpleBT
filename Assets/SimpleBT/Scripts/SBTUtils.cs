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
            object value = null;

            if (type == typeof(int)) { value = int.Parse(valueToConvert); }

            return value;

            /*
            if (float.TryParse(valueToConvert,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out var newFloat))
            {
                value = newFloat;
                return (float)value;
            }

            if (int.TryParse(valueToConvert, out var newInt))
            {
                value = newInt;
                return (int)value;
            }

            if (bool.TryParse(valueToConvert, out var newBool))
            {
                value = newBool;
                return (bool)value;
            }

            // if no other variable was catched then it is returned as string
            return valueToConvert;*/
        }
    } 
}
