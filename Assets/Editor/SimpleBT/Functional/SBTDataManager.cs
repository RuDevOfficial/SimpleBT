using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleBT.Editor.Utils;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SimpleBT.Editor.Data
{
    using Editor.Blackboard;
    
    public static class SBTDataManager
    {
        public static void SaveBehaviorCollectionToJson(
            string fileName, 
            SBTGraphView graph, 
            NodeData[] nodeData, 
            List<ExposedProperty> exposedProperties)
        {
            SBTEditorUtils.CreateFolder("Assets", "SimpleBT");
            SBTEditorUtils.CreateFolder("Assets/SimpleBT", "GraphData");
            
            BehaviorCollection collection = new BehaviorCollection()
            {
                BehaviorName = fileName,
                NodeCollection = new NodeCollection()
                {
                    BehaviorName = fileName,
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

        public static BehaviorCollection LoadBehaviorCollectionToJson(string fileName, bool usedActiveSelection = false)
        {
            string objectPath = "";

            if (usedActiveSelection == false)
            {
                SBTSettingsData data = LoadSettingsFromJson();
                objectPath = data.GraphDataPath + $"/{fileName}.simple";
            }
            else { objectPath = AssetDatabase.GetAssetPath(Selection.activeObject); }

            if (!File.Exists(objectPath)) {
                EditorUtility.DisplayDialog("Error", $"JSON file {fileName} does not exist.", "OK");
                return null;
            }
            
            string jsonContent = File.ReadAllText(objectPath);
            BehaviorCollection behaviorCollection = JsonUtility.FromJson<BehaviorCollection>(jsonContent);
            return behaviorCollection;
        }

        public static void SaveEditorToJson(SBTEditorWindow window)
        {
            SBTEditorUtils.CreateFolder("Assets/SimpleBT", "EditorData");

            SBTEditorData data = new SBTEditorData()
            {
                LastFileName = window.LastFieldValue,
                LastFilePath = window.LastFilePath,
                scriptable = window.LastObjectScriptable
            };
            
            string json = JsonUtility.ToJson(data, true);
            
            try { File.WriteAllText($"Assets/SimpleBT/EditorData/EditorData.json", json); } 
            catch (Exception e) { Debug.LogError(e); }
        }
        
        public static SBTEditorData LoadEditorFromJson()
        {
            if (!File.Exists($"Assets/SimpleBT/EditorData/EditorData.json")) 
            { Debug.LogError($"JSON file EditorData not found at that path. Are you sure you didn't save first?"); }

            string jsonContent = default;

            string objectPath = "";
            
            SBTSettingsData data = LoadSettingsFromJson();
            objectPath = data.EditorDataPath; // Checks if you added the whole path including the .json file first
            if (objectPath.Contains(".json") == false) { objectPath = data.EditorDataPath + "/EditorData.json"; } // If you only added the path up to the file

            try { jsonContent = File.ReadAllText(objectPath); }
            catch { Debug.LogError($"Error reading JSON file EditorData"); return null; }

            SBTEditorData editorData;

            try { editorData = JsonUtility.FromJson<SBTEditorData>(jsonContent); } 
            catch { Debug.LogError($"Error parsing JSON file EditorData"); return null; }

            return editorData;
        }

        public static void SaveSettingsToJson(SbtSettingsWindow settingsWindow)
        {
            SBTEditorUtils.CreateFolder("Assets/SimpleBT", "SettingsData");

            SBTSettingsData data = new SBTSettingsData()
            {
                EditorDataPath = settingsWindow.EditorDataPath,
                GraphDataPath = settingsWindow.GraphDataPath
            };
            
            string json = JsonUtility.ToJson(data, true);
            
            try { File.WriteAllText($"Assets/SimpleBT/SettingsData/Settings.json", json); } 
            catch (Exception e) { Debug.LogError(e); }
        }

        public static SBTSettingsData LoadSettingsFromJson()
        {
            if (!File.Exists($"Assets/SimpleBT/SettingsData/Settings.json")) 
            { Debug.LogError($"JSON file Settings not found at that path. Are you sure you didn't save first?"); }

            string jsonContent = default;

            try { jsonContent = File.ReadAllText($"Assets/SimpleBT/SettingsData/Settings.json"); }
            catch { Debug.LogError($"Error reading JSON file SettingsData"); return null; }
            
            SBTSettingsData settingsWindow;
            
            try { settingsWindow = JsonUtility.FromJson<SBTSettingsData>(jsonContent); } 
            catch { Debug.LogError($"Error parsing JSON file SettingsData"); return null; }
            
            return settingsWindow;
        }
    }

}
