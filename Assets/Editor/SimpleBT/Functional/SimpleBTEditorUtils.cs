using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;

namespace SimpleBT.Editor.Utils
{
    public static class SimpleBTEditorUtils
    {
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
    } 
}
