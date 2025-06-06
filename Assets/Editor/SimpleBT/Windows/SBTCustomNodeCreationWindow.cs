﻿using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.Functional
{
    using Utils;
    
    public class SBTCustomNodeCreationWindow : EditorWindow
    {
        private void OnEnable()
        {
            SBTCustomNodeCreationWindow window = GetWindow<SBTCustomNodeCreationWindow>();
            window.titleContent = new GUIContent("Create Custom Node");
        }

        private void CreateGUI()
        {
            string[] nodeTypes = Enum.GetNames(typeof(NodeType));
            
            DropdownField dropdownField = new DropdownField(nodeTypes.ToList(), 0);
            TextField nameField = new TextField("Node Name: ");

            Button button = new Button();
            button.text = "Create";
            button.clicked += () =>
            {
                string newName = nameField.value.FilterValue();

                if (string.IsNullOrEmpty(newName) == false)
                {
                    SBTEditorUtils.CreateTemplateNode(newName, Enum.Parse<NodeType>(dropdownField.value));
                    Close();
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", "Name is missing", "OK");
                }
            };
            
            rootVisualElement.Add(dropdownField);
            rootVisualElement.Add(nameField);
            rootVisualElement.Add(button);
        }
    }
    
    public enum NodeType { Action, Condition, Decorator, Composite }
}
