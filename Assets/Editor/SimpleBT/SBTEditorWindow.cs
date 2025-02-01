using System;
using System.Collections.Generic;
using System.Reflection;
using SimpleBT;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class SBTEditorWindow : EditorWindow
{
    private SBTGraphView _graph;
    private SimpleTree _currentSimpleTree;

    private TextField _field;
    
    private const string DATA_PATH = "Assets/SimpleBT/";
    
    [MenuItem("SimpleBT/Window")]
    public static void ShowExample()
    {
        SBTEditorWindow wnd = GetWindow<SBTEditorWindow>();
        wnd.titleContent = new GUIContent("Simple BT");
    }

    private void OnEnable()
    {
        _graph = new SBTGraphView(this);
        _graph.StretchToParentSize();
        rootVisualElement.Add(_graph);
        
        // Adding Toolbar for Saving
        Toolbar toolbar = new Toolbar();
        rootVisualElement.Add(toolbar);

        TextElement label = new TextElement();
        label.text = "   File Name";
        label.style.alignSelf = Align.Center;
        toolbar.Add(label);
        
        _field = new TextField();
        _field.value = "New Behaviour Tree";
        toolbar.Add(_field);
        
        Button saveButton = new Button(Save);
        saveButton.text = "Save";
        toolbar.Add(saveButton);
        
        Button loadButton = new Button(Load);
        loadButton.text = "Load";
        toolbar.Add(loadButton);
    }

    void Save()
    {
        List<BehaviourTreeNode> nodesList = new List<BehaviourTreeNode>();

        _graph.graphElements.ForEach(element => {
            if (element is BehaviourTreeNode node) {
                nodesList.Add(node);
            }
        });

        NodeData[] nodesDatas = new NodeData[nodesList.Count];
        for (int i = 0; i < nodesList.Count; i++)
        {
            BehaviourTreeNode node = nodesList[i];
            node.Rect = node.GetPosition();
            
            NodeData nodeData = new NodeData()
            {
                Node = nodesList[i]
            };
            
            nodesDatas[i] = nodeData;
        }
        
        SimpleBTDataSystem.Save(_field.value, nodesDatas);
    }

    void Load()
    {
        NodeDataCollection collection = new NodeDataCollection(_field.value);
        
        foreach (NodeData data in collection.nodes)
        {
            BehaviourTreeNode node = data.Node;
            node.SetPosition(node.Rect);
            node.Draw();
            node.RefreshPorts();
            node.RefreshExpandedState();
            _graph.AddElement(node);
        }
    }
}
