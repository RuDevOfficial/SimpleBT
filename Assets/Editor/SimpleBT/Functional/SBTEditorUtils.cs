using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SimpleBT.Editor.Data;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

using UnityEngine;
using UnityEngine.UIElements;

using Node = UnityEditor.Experimental.GraphView.Node;
using Object = UnityEngine.Object;

namespace SimpleBT.Editor.Utils
{
    using Functional;
    
    public static class SBTEditorUtils
    {
        /// <summary>
        /// Generates a port from the target Node and adds it to the proper input/output container
        /// </summary>
        /// <param name="node">The target node</param>
        /// <param name="direction">Input or Output</param>
        /// <param name="capacity">Holds one or many</param>
        /// <param name="name">Name of the port</param>
        public static void GeneratePort(this Node node, Direction direction, Port.Capacity capacity, string name = "")
        {
            Port port = node.InstantiatePort(Orientation.Vertical, direction, capacity, typeof(bool));
            port.portName = name;
        
            if (direction == Direction.Input) { node.inputContainer.Add(port); }
            else { node.outputContainer.Add(port); }
        }
        
        /// <summary>
        /// Creates a folder only when it doesn't exist (if it's already a valid location)
        /// </summary>
        /// <param name="path">The path the folder will be created in</param>
        /// <param name="folderName">The folder's name</param>
        public static void CreateFolder(string path, string folderName)
        {
            if (AssetDatabase.IsValidFolder($"{path}/{folderName}") == false) { AssetDatabase.CreateFolder(path, folderName); }
        }

        /// <summary>
        /// Returns true if the object contains a ".simple" extension at the end
        /// </summary>
        /// <param name="obj">Selected object</param>
        /// <param name="fileName">Output of the file name of the behavior</param>
        /// <returns></returns>
        public static bool TryGetBehaviorFile(Object obj, out string fileName)
        {
            fileName = null;
            
            string path = AssetDatabase.GetAssetPath(obj);
            string fileNameAndPath = Path.GetFileName(path);

            if (!fileNameAndPath.Contains(".simple")) return false;
            
            fileName = Path.GetFileNameWithoutExtension(path);
            return true;
        }
        
        // Modified method by DanjelRicci in discussions.unity.com
        /// <summary>
        /// Runs over the directory and returns all behaviors from the target data
        /// path obtained by loading the SBTSettingsData json file
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllBehaviorNames()
        {
            List<string> behaviorNames = new List<string>();

            SBTSettingsData data = SBTDataManager.LoadSettingsFromJson();
            if (Directory.Exists(data.GraphDataPath)) {
                string[] assets = Directory.GetFiles(data.GraphDataPath);
                foreach (string assetPath in assets) {
                    if (assetPath.Contains(".simple") && !assetPath.Contains(".meta")) {
                        behaviorNames.Add(AssetDatabase.LoadMainAssetAtPath(assetPath).name);
                    }
                }
            }

            return behaviorNames;
        }

        public static void ShowTemplatePopupWindow(DropdownMenuAction obj)
        {
            CreateFolder("Assets", "SimpleBT");
            CreateFolder("Assets/SimpleBT", "Custom");
            CreateFolder("Assets/Editor/SimpleBT", "Custom");
            
            ScriptableObject.CreateInstance<SBTCustomNodeCreationWindow>();
        }

        /// <summary>
        /// Generates the appropriate node scripts onto the Custom folders in both SimpleBT folders
        /// Also generates the folders if they didn't exist previously
        /// </summary>
        /// <param name="name">Name given for the new node</param>
        /// <param name="nodeType">Type of node</param>
        public static void CreateTemplateNode(string name, NodeType nodeType)
        {
            Type projectWindowUtilType = typeof(ProjectWindowUtil);
            MethodInfo getActiveFolderPath = projectWindowUtilType.GetMethod("GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);
            object obj = getActiveFolderPath.Invoke(null, Array.Empty<object>());
            string pathToCurrentFolder = obj.ToString();

            string graphTemplatePath;
            string actionTemplatePath;
            
            switch (nodeType)
            {
                case NodeType.Action:
                    CreateFolder("Assets/SimpleBT/Custom", "Actions");
                    CreateFolder("Assets/Editor/SimpleBT/Custom", "GraphActions");

                    // Template Path
                    graphTemplatePath = "Assets/ScriptTemplates/01-SBT Script Templates__GraphAction-GraphAction_Name.cs.txt";
                    actionTemplatePath = "Assets/ScriptTemplates/10-SBT Script Templates__Action-Action_Name.cs.txt";
                    
                    // Create Templates
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile(graphTemplatePath, $"GraphAction_{name}.cs");
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile(actionTemplatePath, $"Action_{name}.cs");
                    
                    // ProjectWindowUtil is weird, for some reason the previous asset created only exists if another script asset is created after
                    // which means I need to make an empty "delete.cs" asset for the previous one to actually exist
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile("Assets/ScriptTemplates/00-SBT Script Templates__Empty-Empty.cs.txt", "delete_this.cs");
                    
                    // Refresh
                    AssetDatabase.MoveAsset($"{pathToCurrentFolder}/GraphAction_{name}.cs", $"Assets/Editor/SimpleBT/Custom/GraphActions/GraphAction_{name}.cs");
                    AssetDatabase.MoveAsset($"{pathToCurrentFolder}/Action_{name}.cs", $"Assets/SimpleBT/Custom/Actions/Action_{name}.cs");
                    break;
                                
                case NodeType.Condition:
                    CreateFolder("Assets/SimpleBT/Custom", "Conditions");
                    CreateFolder("Assets/Editor/SimpleBT/Custom", "GraphConditions");
                    
                    // Template Path
                    graphTemplatePath = "Assets/ScriptTemplates/02-SBT Script Templates__GraphCondition-GraphCondition_Name.cs.txt";
                    actionTemplatePath = "Assets/ScriptTemplates/11-SBT Script Templates__Condition-Condition_Name.cs.txt";
                    
                    // Create Templates
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile(graphTemplatePath, $"GraphCondition_{name}.cs");
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile(actionTemplatePath, $"Condition_{name}.cs");
                    
                    // ProjectWindowUtil is weird, for some reason the previous asset created only exists if another script asset is created after
                    // which means I need to make an empty "delete.cs" asset for the previous one to actually exist
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile("Assets/ScriptTemplates/00-SBT Script Templates__Empty-Empty.cs.txt", "delete_this.cs");
                    
                    // Refresh
                    AssetDatabase.MoveAsset($"{pathToCurrentFolder}/GraphCondition_{name}.cs", $"Assets/Editor/SimpleBT/Custom/GraphConditions/GraphCondition_{name}.cs");
                    AssetDatabase.MoveAsset($"{pathToCurrentFolder}/Condition_{name}.cs", $"Assets/SimpleBT/Custom/Conditions/Condition_{name}.cs");
                    break;
                
                case NodeType.Decorator:
                    CreateFolder("Assets/SimpleBT/Custom", "Decorators");
                    CreateFolder("Assets/Editor/SimpleBT/Custom", "GraphDecorators");
                    
                    // Template Path
                    graphTemplatePath = "Assets/ScriptTemplates/04-SBT Script Templates__GraphDecorator-GraphDecorator_Name.cs.txt";
                    actionTemplatePath = "Assets/ScriptTemplates/13-SBT Script Templates__Decorator-Decorator_Name.cs.txt";
                    
                    // Create Templates
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile(graphTemplatePath, $"GraphDecorator_{name}.cs");
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile(actionTemplatePath, $"Decorator_{name}.cs");
                    
                    // ProjectWindowUtil is weird, for some reason the previous asset created only exists if another script asset is created after
                    // which means I need to make an empty "delete.cs" asset for the previous one to actually exist
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile("Assets/ScriptTemplates/00-SBT Script Templates__Empty-Empty.cs.txt", "delete_this.cs");
                    
                    // Refresh
                    AssetDatabase.MoveAsset($"{pathToCurrentFolder}/GraphDecorator_{name}.cs", $"Assets/Editor/SimpleBT/Custom/GraphDecorators/GraphDecorator_{name}.cs");
                    AssetDatabase.MoveAsset($"{pathToCurrentFolder}/Decorator_{name}.cs", $"Assets/SimpleBT/Custom/Decorators/Decorator_{name}.cs");
                    break;
                
                default:
                    CreateFolder("Assets/SimpleBT/Custom", "Composites");
                    CreateFolder("Assets/Editor/SimpleBT/Custom", "GraphComposites");
                    
                    // Template Path
                    graphTemplatePath = "Assets/ScriptTemplates/03-SBT Script Templates__GraphComposite-GraphComposite_Name.cs.txt";
                    actionTemplatePath = "Assets/ScriptTemplates/12-SBT Script Templates__Composite-Composite_Name.cs.txt";
                    
                    // Create Templates
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile(graphTemplatePath, $"GraphComposite_{name}.cs");
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile(actionTemplatePath, $"Composite_{name}.cs");
                    
                    // ProjectWindowUtil is weird, for some reason the previous asset created only exists if another script asset is created after
                    // which means I need to make an empty "delete.cs" asset for the previous one to actually exist
                    ProjectWindowUtil.CreateScriptAssetFromTemplateFile("Assets/ScriptTemplates/00-SBT Script Templates__Empty-Empty.cs.txt", "delete_this.cs");
                    
                    // Refresh
                    AssetDatabase.MoveAsset($"{pathToCurrentFolder}/GraphComposite_{name}.cs", $"Assets/Editor/SimpleBT/Custom/GraphComposites/GraphComposite_{name}.cs");
                    AssetDatabase.MoveAsset($"{pathToCurrentFolder}/Composite_{name}.cs", $"Assets/SimpleBT/Custom/Composites/Composite_{name}.cs");
                    break;
            }

            AssetDatabase.Refresh();
        }

        public static void CreateCustomEntriesSOAsset(DropdownMenuAction obj)
        {
            string templatePath = "Assets/ScriptTemplates/20-SBT Script Templates__CustomNodeEntries-CustomNodeEntries.cs.txt";
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, $"CustomNodeEntries.cs");
            AssetDatabase.Refresh();
        }

        public static List<String> ReturnEnumToList<T>() where T : Enum { return Enum.GetNames(typeof(T)).ToList(); }
    } 
}