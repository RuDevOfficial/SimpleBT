using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = UnityEditor.Experimental.GraphView.Node;

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

        public static bool TryGetBehaviorFile(Object obj, out string fileName)
        {
            fileName = null;
            
            string path = AssetDatabase.GetAssetPath(obj);
            string fileNameAndPath = Path.GetFileName(path);

            if (fileNameAndPath.Contains(".simple"))
            {
                fileName = Path.GetFileNameWithoutExtension(path);
                return true;
            }

            return false;
        }

        // Method by DanjelRicci in discussions.unity.com
        public static List<string> GetAllBehaviorNames()
        {
            List<string> behaviorNames = new List<string>();
            
            if (Directory.Exists("Assets/SimpleBT/GraphData")) {
                string[] assets = Directory.GetFiles("Assets/SimpleBT/GraphData");
                foreach (string assetPath in assets) {
                    if (assetPath.Contains(".simple") && !assetPath.Contains(".meta")) {
                        behaviorNames.Add(AssetDatabase.LoadMainAssetAtPath(assetPath).name);
                    }
                }
            }

            return behaviorNames;
        }
    } 
}