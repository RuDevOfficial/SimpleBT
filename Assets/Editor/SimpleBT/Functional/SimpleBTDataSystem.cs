using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SimpleBT.Editor.Data
{
    public static class SimpleBTDataSystem
    {
        public static void SaveNodesToJson(string fileName, NodeData[] nodeData, SBTGraphView graph)
        {
            BehaviourCollection behaviourCollection = new BehaviourCollection()
            {
                nodes = nodeData,
                ViewportPosition = graph.viewTransform.position,
                ViewportScale = graph.viewTransform.scale
            };
            
            CreateFolder("Assets", "SimpleBT");
            CreateFolder("Assets/SimpleBT", "Behaviours");
            
            string nodeJson = JsonUtility.ToJson(behaviourCollection, true);
            
            try {
                File.WriteAllText($"Assets/SimpleBT/Behaviours/{fileName}.simple", nodeJson);
            } catch (Exception e) {
                Debug.LogError(e);
            }
        }

        public static BehaviourCollection LoadNodesFromJson(string fileName)
        {
            if (!File.Exists($"Assets/SimpleBT/Behaviours/{fileName}.simple")) { Debug.LogError($"JSON file {fileName} not found at that path."); }

            string jsonContent = default;

            try {
                jsonContent = File.ReadAllText($"Assets/SimpleBT/Behaviours/{fileName}.simple");
            }
            catch {
                Debug.LogError($"Error reading JSON file {fileName}");
                return null;
            }

            BehaviourCollection collection;

            try {
                collection = JsonUtility.FromJson<BehaviourCollection>(jsonContent);
            } catch {
                Debug.LogError($"Error parsing JSON file {fileName}");
                return null;
            }

            return collection;
        }

        public static void SaveEditorToJson(SBTEditorWindow window)
        {
            CreateFolder("Assets/SimpleBT", "EditorData");

            SBTEditorData data = new SBTEditorData()
            {
                LastFileName = window.LastFieldValue,
            };
            
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
