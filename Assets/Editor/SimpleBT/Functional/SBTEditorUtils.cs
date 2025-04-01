using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Node = UnityEditor.Experimental.GraphView.Node;
using Object = UnityEngine.Object;

namespace SimpleBT.Editor.Utils
{
    public static class SBTEditorUtils
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

        public static TextField CreateTextField(this VisualElement element, string text, EventCallback<ChangeEvent<string>> actionEvent)
        {
            TextField field = new TextField(text + ": ");
            field.UnregisterValueChangedCallback(actionEvent);
            element.Add(field);

            return field;
        }

        public static Toggle CreateToggle(this VisualElement element, string text, EventCallback<ChangeEvent<bool>> actionEvent)
        {
            Toggle toggle = new Toggle(text + ": ");
            toggle.RegisterValueChangedCallback(actionEvent);
            element.Add(toggle);

            return toggle;
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