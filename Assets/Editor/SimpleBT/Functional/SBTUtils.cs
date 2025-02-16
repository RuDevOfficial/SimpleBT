using SimpleBT.NonEditor.Tree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace SimpleBT.Editor.Utils
{
    public static class SBTUtils
    {
        public static void GeneratePort(this Node node, Direction direction, Port.Capacity capacity, string name = "")
        {
            Port port = node.InstantiatePort(Orientation.Vertical, direction, capacity, typeof(bool));
            port.portName = name;
        
            if (direction == Direction.Input) { node.inputContainer.Add(port); }
            else { node.outputContainer.Add(port); }
        }
        
        public static void CreateFolder(string path, string folderName)
        {
            if (AssetDatabase.IsValidFolder($"{path}/{folderName}") == false) { AssetDatabase.CreateFolder(path, folderName); }
        }
    } 
}