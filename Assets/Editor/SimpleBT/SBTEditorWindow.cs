using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleBT;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Edge = UnityEditor.Experimental.GraphView.Edge;

[System.Serializable]
public class SBTEditorWindow : EditorWindow, ISerializationCallbackReceiver
{
    private SBTGraphView _graph;
    private SimpleTree _currentSimpleTree;

    private TextField _field;
    private static string _lastFieldValue;
    public string LastFieldValue => _lastFieldValue;
    
    private const string DATA_PATH = "Assets/SimpleBT/";
    
    [MenuItem("SimpleBT/Window")]
    public static void Open()
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
        label.text = "  File Name";
        label.style.alignSelf = Align.Center;
        toolbar.Add(label);
        
        _field = new TextField();
        _field.value = _lastFieldValue ?? "New Behaviour Tree";
        _field.RegisterValueChangedCallback(evt => { _lastFieldValue = evt.newValue; });
        toolbar.Add(_field);
        
        Button saveButton = new Button(Save);
        saveButton.text = "Save";
        toolbar.Add(saveButton);
        
        Button loadButton = new Button(Load);
        loadButton.text = "Load";
        toolbar.Add(loadButton);
        
        
        // Poner los datos que se hayan guardado antes
        SBTEditorData editorData = SimpleBTDataSystem.LoadEditorFromJson();

        _lastFieldValue = editorData.LastFileName;
        _field.value = _lastFieldValue;
    }

    private void OnDisable()
    {
        SimpleBTDataSystem.SaveEditorToJson(this);
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
                       
            List<string> toGUIDs = new List<string>();
            Port port = node.outputContainer.Q<Port>("");
            foreach (Edge edge in port.connections)
            {
                Node connectedNode = edge.input.node;
                BehaviourTreeNode btNode = (BehaviourTreeNode)connectedNode;
                toGUIDs.Add(btNode.GUID);
            }
            
            NodeData nodeData = new NodeData();
            nodeData.Node = node;
            nodeData.fromGUID = node.GUID;
            nodeData.toGUIDs = new List<string>(toGUIDs);
            
            nodesDatas[i] = nodeData;
        }

        SimpleBTDataSystem.SaveNodesToJson(_field.value, nodesDatas);
    }

    
    void Load()
    {
        NodeDataCollection collection = new NodeDataCollection(_field.value);
        
        _graph.DeleteElements(_graph.graphElements);
        
        foreach (NodeData data in collection.nodes)
        {
            // Generate all nodes First
            BehaviourTreeNode node = data.Node;
            node.SetPosition(node.Rect);
            node.Draw();
            node.RefreshPorts();
            node.RefreshExpandedState();
            _graph.AddElement(node);
        }

        foreach (NodeData data in collection.nodes)
        {
            BehaviourTreeNode fromNode = data.Node;
            if (data.toGUIDs.Count == 0) { continue; }
            
            Port fromPort = fromNode.outputContainer.Q<Port>("");
            foreach (string toGUID in data.toGUIDs)
            {
                BehaviourTreeNode node = _graph.GetNodeByGUID(toGUID);
                Port toPort = node.inputContainer.Q<Port>("");
                Edge edge = fromPort.ConnectTo(toPort);
                _graph.AddElement(edge);
            }
        }
    }

    public void OnBeforeSerialize()
    {
        if(!string.IsNullOrEmpty(_lastFieldValue)) { SimpleBTDataSystem.SaveEditorToJson(this); }
    }

    public void OnAfterDeserialize()
    {

    }
}
