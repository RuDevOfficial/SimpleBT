using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public static class SimpleBTDataSystem
{
    public static void Save(string fileName, NodeData[] data)
    {
        NodeDataCollection collection = new NodeDataCollection() { nodes = data };
        
        CreateFolder("Assets", "SimpleBT");
        CreateFolder("Assets/SimpleBT", "Behaviours");
        
        string json = JsonUtility.ToJson(collection, true);
        
        try {
            File.WriteAllText($"Assets/SimpleBT/Behaviours/{fileName}.json", json);
        } catch (Exception e) {
            Debug.LogError(e);
        }
    }

    public static NodeDataCollection Load(string fileName)
    {
        if (!File.Exists($"Assets/SimpleBT/Behaviours/{fileName}.json")) { Debug.LogError($"JSON file {fileName} not found at that path."); }

        string jsonContent = default;

        try
        {
            jsonContent = File.ReadAllText($"Assets/SimpleBT/Behaviours/{fileName}.json");
        }
        catch (Exception e)
        {
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

    private static void CreateFolder(string path, string folderName)
    {
        if (AssetDatabase.IsValidFolder($"{path}/{folderName}") == false) { AssetDatabase.CreateFolder(path, folderName); }
    }
}