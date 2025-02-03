using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

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
    } 
}
