using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SimpleBT.Editor.Data
{
    using Editor.Blackboard;
    
    public static class SimpleBTDataSystem
    {
        public static void SaveBehaviorCollectionToJson(
            string fileName, 
            SBTGraphView graph, 
            NodeData[] nodeData, 
            List<ExposedProperty> exposedProperties)
        {
            CreateFolder("Assets", "SimpleBT");
            CreateFolder("Assets/SimpleBT", "GraphData");
            
            BehaviorCollection collection = new BehaviorCollection()
            {
                NodeCollection = new NodeCollection(nodeData, graph)
                {
                    Nodes = nodeData,
                    ViewportPosition = graph.viewTransform.position,
                    ViewportScale = graph.viewTransform.scale
                },
                BlackboardCollection = new BlackboardCollection()
                {
                    ExposedProperties = exposedProperties
                }
            };
            
            string collectionJson = JsonUtility.ToJson(collection, true);
            
            try { File.WriteAllText($"Assets/SimpleBT/GraphData/{fileName}.simple", collectionJson); } 
            catch (Exception e) { Debug.LogError(e); }
        }

        public static BehaviorCollection LoadBehaviorCollectionToJson(string fileName)
        {
            if (!File.Exists($"Assets/SimpleBT/GraphData/{fileName}.simple")) {
                EditorUtility.DisplayDialog("Error", $"JSON file {fileName} does not exist.", "OK");
                return null;
            }

            string jsonContent = File.ReadAllText($"Assets/SimpleBT/GraphData/{fileName}.simple");
            BehaviorCollection behaviorCollection = JsonUtility.FromJson<BehaviorCollection>(jsonContent);
            return behaviorCollection;
        }

        public static void SaveEditorToJson(SBTEditorWindow window)
        {
            CreateFolder("Assets/SimpleBT", "EditorData");

            SBTEditorData data = new SBTEditorData() { LastFileName = window.LastFieldValue, };
            
            string json = JsonUtility.ToJson(data, true);
            
            try { File.WriteAllText($"Assets/SimpleBT/EditorData/EditorData.json", json); } 
            catch (Exception e) { Debug.LogError(e); }
        }
        
        public static SBTEditorData LoadEditorFromJson()
        {
            if (!File.Exists($"Assets/SimpleBT/EditorData/EditorData.json")) 
            { Debug.LogError($"JSON file EditorData not found at that path. Are you sure you didn't save first?"); }

            string jsonContent = default;

            try { jsonContent = File.ReadAllText($"Assets/SimpleBT/EditorData/EditorData.json"); }
            catch { Debug.LogError($"Error reading JSON file EditorData"); return null; }

            SBTEditorData editorData;

            try { editorData = JsonUtility.FromJson<SBTEditorData>(jsonContent); } 
            catch { Debug.LogError($"Error parsing JSON file EditorData"); return null; }

            return editorData;
        }
        
        private static void CreateFolder(string path, string folderName)
        {
            if (AssetDatabase.IsValidFolder($"{path}/{folderName}") == false) { AssetDatabase.CreateFolder(path, folderName); }
        }
    }

}
