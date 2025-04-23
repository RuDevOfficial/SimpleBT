using System;
using System.Linq;
using SimpleBT.Editor.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.SimpleBT.Functional
{
    public class TemplateClassPopupWindow : EditorWindow
    {
        private void OnEnable()
        {
            TemplateClassPopupWindow window = GetWindow<TemplateClassPopupWindow>();
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
                    this.Close();
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
