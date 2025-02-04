using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using Object = UnityEngine.Object;

namespace SimpleBT.Editor
{
    using SimpleBT.Editor.Data;
    using SimpleBT.Editor.GraphNodes;
    
    [System.Serializable]
    public class SBTEditorWindow : EditorWindow, ISerializationCallbackReceiver
    {
        private SBTGraphView _graph;

        #region Private Fields
        
        private TextField _field;
        private static string _lastFieldValue;
        
        #endregion
        
        #region Public Fields
        
        public string LastFieldValue => _lastFieldValue;
        
        #endregion
        
        [MenuItem("SimpleBT/Window")]
        public static void Open()
        {
            SBTEditorWindow wnd = GetWindow<SBTEditorWindow>();
            wnd.titleContent = new GUIContent("Simple BT");
        }

        #region Unity Event Functions
        
        private void OnEnable()
        {
            GenerateGraph();
            GenerateVisualElements();
            LoadPreviousData();
        }

        private void OnDisable() {
            // Make sure to save any necessary data before its serialized
            SimpleBTDataSystem.SaveEditorToJson(this);
        }

        #endregion

        private void OnSelectionChange()
        {
            Object selectedObject = Selection.activeObject;
            if (selectedObject == null) { return; }

            string path = AssetDatabase.GetAssetPath(selectedObject);
            string fileNameAndPath = Path.GetFileName(path);

            if (fileNameAndPath.Contains(".simple"))
            {
                string fileName = Path.GetFileNameWithoutExtension(path);
                if (fileName != _field.value)
                {
                    Load(fileName);
                    _field.value = fileName;
                    _lastFieldValue = fileName;
                }
            }
        }

        #region Initialization
        
        private void GenerateGraph()
        {
            _graph = new SBTGraphView(this);
            _graph.StretchToParentSize();
            rootVisualElement.Add(_graph);
        }
        
        private void GenerateVisualElements()
        {
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
            
            Button loadButton = new Button(() => { Load(); });
            loadButton.text = "Load";
            toolbar.Add(loadButton);
        }
        
        private void LoadPreviousData()
        {
            SBTEditorData editorData = SimpleBTDataSystem.LoadEditorFromJson();

            _lastFieldValue = editorData.LastFileName;
            _field.value = _lastFieldValue;
        }
        
        #endregion
        
        #region Saving & Loading
        
        /// <summary>
        /// This method saves the serializable data into a JSON file by checking which graphElements are nodes and obtaining the
        /// necessary data to then create a NodeData class which will hold the values
        /// </summary>
        private void Save()
        {
            List<GraphTreeNode> nodesList = new List<GraphTreeNode>();
            
            //Grabs all graphElement that is a node and saves it to a list (nodesList)
            _graph.graphElements.ForEach(element => {
                if (element is GraphTreeNode node) {
                    nodesList.Add(node);
                }
            });

            //Generates an array of nodeData and populates it
            //Variable "Node", "fromGUID" cannot be null
            //Variable "toGUID" can be null
            NodeData[] nodesDatas = new NodeData[nodesList.Count];
            for (int i = 0; i < nodesList.Count; i++)
            {
                //Sets the checked node's position Rect value in advance
                GraphTreeNode node = nodesList[i];
                node.Rect = node.GetPosition();
                           
                //Lists all GUIDs that come out of the output port of the checked node
                List<string> toGUIDs = new List<string>();
                Port port = node.outputContainer.Q<Port>("");
                foreach (Edge edge in port.connections)
                {
                    Node connectedNode = edge.input.node;
                    GraphTreeNode btNode = (GraphTreeNode)connectedNode;
                    toGUIDs.Add(btNode.GUID);
                }
                
                //Generates the NodeData class and populates the obtained data
                NodeData nodeData = new NodeData();
                nodeData.Node = node;
                nodeData.fromGUID = node.GUID;
                nodeData.toGUIDs = new List<string>(toGUIDs);
                
                nodesDatas[i] = nodeData;
            }

            SimpleBTDataSystem.SaveNodesToJson(_field.value, nodesDatas, _graph);
        }
        
        /// <summary>
        /// This method loads the data in the JSON file and generates both nodes
        /// and port connections (and edges) separately
        /// </summary>
        private void Load(string fieldValue = null)
        {
            BehaviourCollection collection;

            if (fieldValue == null) { collection = new BehaviourCollection(_field.value); }
            else { collection = new BehaviourCollection(fieldValue); }
            
            //Delete all previous elements to not generate duplicates
            _graph.DeleteElements(_graph.graphElements);
            
            //Generate nodes and add them to the graph
            foreach (NodeData data in collection.nodes)
            {
                // Generate all nodes First
                GraphTreeNode node = data.Node;
                node.SetPosition(node.Rect);
                node.Draw();
                node.RefreshPorts();
                node.RefreshExpandedState();
                _graph.AddElement(node);
            }

            //Generates the connections and edges by obtaining the input
            //of the "fromNode" to the output port of the "toNode"
            foreach (NodeData data in collection.nodes)
            {
                GraphTreeNode fromNode = data.Node;
                if (data.toGUIDs.Count == 0) { continue; }
                
                Port fromPort = fromNode.outputContainer.Q<Port>("");
                foreach (string toGUID in data.toGUIDs)
                {
                    GraphTreeNode node = _graph.GetNodeByGUID(toGUID);
                    Port toPort = node.inputContainer.Q<Port>("");
                    Edge edge = fromPort.ConnectTo(toPort);
                    _graph.AddElement(edge);
                }
            }
            
            //Viewport update
            _graph.viewTransform.position = collection.ViewportPosition;
            _graph.viewTransform.scale = collection.ViewportScale;
        }
        
        #endregion
        
        #region ISerializationCallbackReceiver Methods
        
        //Method to save on domain reload
        public void OnBeforeSerialize() {
            if(!string.IsNullOrEmpty(_lastFieldValue)) { SimpleBTDataSystem.SaveEditorToJson(this); }
        }

        //Left blank
        public void OnAfterDeserialize() { }
        
        #endregion
    }
}

