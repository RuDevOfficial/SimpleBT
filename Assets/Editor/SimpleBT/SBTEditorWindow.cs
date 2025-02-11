using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleBT.Editor.Utils;
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
        private readonly Rect BLACKBOARD_RECT = new Rect(0, 30, 250, 400);
        
        private SBTGraphView _graph;
        private SBTBlackboard _blackboard;

        private Button _generateButton;

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
            LoadEditorData();
        }

        private void OnDisable() {
            // Make sure to save any necessary data before its serialized
            SimpleBTDataSystem.SaveEditorToJson(this);
        }

        #endregion

        private void OnSelectionChange()
        {
            Object selectedObject = Selection.activeObject;

            if (selectedObject == null)
            {
                _generateButton.SetEnabled(false);
                return;
            }
            
            if (AssetDatabase.Contains(selectedObject))
            {
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
                
                _generateButton.SetEnabled(false);
            }
            else
            {
                GameObject selectedGameObject = Selection.activeGameObject;

                if (selectedGameObject == null) { return; }

                // do stuff :D
                _generateButton.SetEnabled(true);
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

            _field = new TextField("Behavior Name: ");
            _field.value = _lastFieldValue ?? "New Behaviour Tree";
            _field.RegisterValueChangedCallback(evt =>
            {
                string newValue = evt.newValue;
                string filteredValue = newValue.FilterValue();
                        
                _lastFieldValue = filteredValue;
                _field.value = filteredValue;
            });
            toolbar.Add(_field);
            
            Button saveButton = new Button(Save);
            saveButton.text = "Save";
            toolbar.Add(saveButton);
            
            Button loadButton = new Button(() => { Load(_field.value); });
            loadButton.text = "Load";
            toolbar.Add(loadButton);

            _generateButton = new Button(Generate);
            _generateButton.text = "Generate";
            toolbar.Add(_generateButton);
            
            // Adding the Blackboard
            _blackboard = new SBTBlackboard(_graph);
            _blackboard.SetPosition(BLACKBOARD_RECT);
            _graph.Add(_blackboard);
        }
        
        private void LoadEditorData()
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
            NodeData[] nodesDataArray = SaveNodes();
            List<ExposedProperty> exposedProperties = SaveBlackboardProperties();

            SimpleBTDataSystem.SaveBehaviorCollectionToJson(
                _field.value,
                _graph, 
                nodesDataArray, 
                exposedProperties, 
                _blackboard);
        }

        private NodeData[] SaveNodes()
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
                if (port != null)
                {
                    foreach (Edge edge in port.connections)
                    {
                        Node connectedNode = edge.input.node;
                        GraphTreeNode btNode = (GraphTreeNode)connectedNode;
                        toGUIDs.Add(btNode.GUID);
                    }
                }
                
                NodeData nodeData = new NodeData();
                nodeData.Node = node;
                nodeData.fromGUID = node.GUID;
                nodeData.toGUIDs = new List<string>(toGUIDs);

                if (node is ConditionNode condNode)
                {
                    nodeData.VariableName = condNode.ConditionBox.VariableName.value;
                    nodeData.Condition = condNode.ConditionBox.ConditionType;
                    nodeData.VariableCheckName = condNode.ConditionBox.VariableChecked.value;
                }
                
                nodesDatas[i] = nodeData;
            }

            return nodesDatas;
        }

        private List<ExposedProperty> SaveBlackboardProperties() {
            return new List<ExposedProperty>(_blackboard.ExposedProperties);
        }
        
        /// <summary>
        /// This method loads the data in the JSON file and generates both nodes
        /// and port connections (and edges) separately
        /// </summary>
        private void Load(string fieldValue = null)
        {
            BehaviorCollection collection = SimpleBTDataSystem.LoadBehaviorCollectionToJson(fieldValue);
            
            //Delete all previous elements to not generate duplicates and empty the blackboard
            _graph.DeleteElements(_graph.graphElements);
            
            LoadNodes(collection);

            //Load ViewTransform
            _graph.viewTransform.position = collection.NodeCollection.ViewportPosition;
            _graph.viewTransform.scale = collection.NodeCollection.ViewportScale;
            
            //Load Blackboard Values
            _blackboard.Reset();
            collection.BlackboardCollection.ExposedProperties.ForEach(property => {
                _blackboard.AddPropertyToBlackboard(property);
            });
        }

        private void LoadNodes(BehaviorCollection collection)
        {
            //Generate nodes and add them to the graph
            foreach (NodeData data in collection.NodeCollection.Nodes)
            {
                // Generate all nodes First
                GraphTreeNode node = data.Node;
                node.SetPosition(node.Rect);
                node.Draw();

                if (node is ConditionNode condNode)
                {
                    condNode.ConditionBox.VariableName.value = data.VariableName;
                    condNode.ConditionBox.VariableChecked.value = data.VariableCheckName;
                    condNode.ConditionBox.ConditionType = data.Condition;
                }
                
                node.RefreshPorts();
                node.RefreshExpandedState();
                _graph.AddElement(node);
            }

            //Generates the connections and edges by obtaining the input
            //of the "fromNode" to the output port of the "toNode"
            foreach (NodeData data in collection.NodeCollection.Nodes)
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
        
        #region Generating Tree

        private void Generate()
        {
            GameObject selectedObject = Selection.activeGameObject;

            if (selectedObject.GetComponent<TreeExecutor>() != null) { return; }
            
            selectedObject.AddComponent<TreeExecutor>();
        }
        
        #endregion
    }
}

