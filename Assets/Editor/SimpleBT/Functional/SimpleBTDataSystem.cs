using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SimpleBT.Editor.Data
{
    public static class SimpleBTDataSystem
    {
        public static void SaveNodesToJson(string fileName, NodeData[] nodeData)
        {
            NodeDataCollection nodeDataCollection = new NodeDataCollection() { nodes = nodeData };
            
            CreateFolder("Assets", "SimpleBT");
            CreateFolder("Assets/SimpleBT", "Behaviours");
            
            string nodeJson = JsonUtility.ToJson(nodeDataCollection, true);
            
            try {
                File.WriteAllText($"Assets/SimpleBT/Behaviours/{fileName}.json", nodeJson);
            } catch (Exception e) {
                Debug.LogError(e);
            }
        }

        public static NodeDataCollection LoadNodesFromJson(string fileName)
        {
            if (!File.Exists($"Assets/SimpleBT/Behaviours/{fileName}.json")) { Debug.LogError($"JSON file {fileName} not found at that path."); }

            string jsonContent = default;

            try {
                jsonContent = File.ReadAllText($"Assets/SimpleBT/Behaviours/{fileName}.json");
            }
            catch (Exception e) {
                Debug.LogError($"Error reading JSON file {fileName}");
                return null;
            }

            NodeDataCollection collection;

            try {
                collection = JsonUtility.FromJson<NodeDataCollection>(jsonContent);
            } catch (Exception e) {
                Debug.LogError($"Error parsing JSON file {fileName}");
                return null;
            }

            return collection;
        }

        public static void SaveEditorToJson(SBTEditorWindow window)
        {
            CreateFolder("Assets/SimpleBT", "EditorData");
            
            SBTEditorData data = new SBTEditorData(window);
            
            string json = JsonUtility.ToJson(data, true);
            
            try {
                File.WriteAllText($"Assets/SimpleBT/EditorData/EditorData.json", json);
            } catch (Exception e) {
                Debug.LogError(e);
            }
        }
        
        public static SBTEditorData LoadEditorFromJson()
        {
            if (!File.Exists($"Assets/SimpleBT/EditorData/EditorData.json")) 
            { Debug.LogError($"JSON file EditorData not found at that path. Are you sure you didn't save first?"); }

            string jsonContent = default;

            try {
                jsonContent = File.ReadAllText($"Assets/SimpleBT/EditorData/EditorData.json");
            }
            catch {
                Debug.LogError($"Error reading JSON file EditorData");
                return null;
            }

            SBTEditorData editorData;

            try {
                editorData = JsonUtility.FromJson<SBTEditorData>(jsonContent);
            } catch {
                Debug.LogError($"Error parsing JSON file EditorData");
                return null;
            }

            return editorData;
        }
        
        private static void CreateFolder(string path, string folderName)
        {
            if (AssetDatabase.IsValidFolder($"{path}/{folderName}") == false) { AssetDatabase.CreateFolder(path, folderName); }
        }
    }
}
