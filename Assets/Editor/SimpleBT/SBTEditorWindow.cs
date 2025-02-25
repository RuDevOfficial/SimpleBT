using System;
using System.Collections.Generic;
using System.IO;
using SimpleBT.Editor.BehaviorGeneration;
using SimpleBT.Editor.Utils;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using Object = UnityEngine.Object;

namespace SimpleBT.Editor
{
    //Editor
    using Editor.Data;
    using Editor.GraphNodes;
    using Editor.Blackboard;
    
    //Non-Editor
    using NonEditor.Tree;
    using NonEditor;
    
    [System.Serializable]
    public class SBTEditorWindow : EditorWindow, ISerializationCallbackReceiver
    {
        private readonly Rect _blackboardRect = new Rect(0, 30, 250, 350);
        
        private SBTGraphView _graph;
        private SBTBlackboardGraph _blackboardGraph;

        private Button _generateButton;
        private Button _removeComponentsButton;
        private Button _regenerateButton;

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
            SBTDataManager.SaveEditorToJson(this);
        }

        #endregion

        #region Button Updates
        
        private void OnSelectionChange()
        {
            Object selectedObject = Selection.activeObject;

            if (selectedObject == null) {
                _generateButton.SetEnabled(false);
                return;
            }
            
            if (AssetDatabase.Contains(selectedObject)) {
                OverwriteGraph(selectedObject);
                _generateButton.SetEnabled(false);
            }
            else
            {
                // Update respective buttons
                GameObject selectedGameObject = Selection.activeGameObject;

                if (selectedGameObject == null) { return; }
                
                selectedGameObject.TryGetComponent<TreeExecutor>(out TreeExecutor treeExecutor);
                selectedGameObject.TryGetComponent<SBTBlackboard>(out SBTBlackboard blackboard);

                _removeComponentsButton.SetEnabled(treeExecutor != null || blackboard != null);
                _regenerateButton.SetEnabled(blackboard != null);
                _generateButton.SetEnabled(blackboard == null);
            }
        }
        
        private void OverwriteGraph(Object selectedObject)
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
        }

        #endregion
        
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

            _field = new TextField("Behavior Name: ") { value = _lastFieldValue ?? "New Behaviour Tree" };
            _field.RegisterValueChangedCallback(evt =>
            {
                var newValue = evt.newValue;
                var filteredValue = newValue.FilterValue();
                        
                _lastFieldValue = filteredValue;
                _field.value = filteredValue;
            });
            toolbar.Add(_field);
            
            Button saveButton = new Button(Save) { text = "Save" };
            Button loadButton = new Button(() => { Load(_field.value); }) { text = "Load" };
            _generateButton = new Button(GenerateGameObjectComponents) { text = "Generate Tree & Blackboard" };
            _regenerateButton = new Button(() =>
            {
                GenerateBlackboard();
                GenerateBehaviorTree();
            }) { text = "Regenerate" }; 
            _removeComponentsButton = new Button(RemoveComponents) { text = "Remove Components" };
            
            toolbar.Add(saveButton);
            toolbar.Add(loadButton);
            toolbar.Add(_generateButton);
            toolbar.Add(_regenerateButton);
            toolbar.Add(_removeComponentsButton);
            
            // Adding the Blackboard
            _blackboardGraph = new SBTBlackboardGraph(_graph);
            _blackboardGraph.SetPosition(_blackboardRect);
            _graph.Add(_blackboardGraph);
        }
        
        private void LoadEditorData()
        {
            SBTEditorData editorData = SBTDataManager.LoadEditorFromJson();

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

            SBTDataManager.SaveBehaviorCollectionToJson(
                _field.value,
                _graph,
                nodesDataArray,
                exposedProperties);
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
                
                NodeData nodeData = new NodeData {
                    Node = node,
                    fromGUID = node.GUID,
                    toGUIDs = new List<string>(toGUIDs),
                    Values = node.GetValues()
                };
                
                nodesDatas[i] = nodeData;
            }

            return nodesDatas;
        }

        private List<ExposedProperty> SaveBlackboardProperties() {
            return new List<ExposedProperty>(_blackboardGraph.ExposedProperties);
        }
        
        /// <summary>
        /// This method loads the data in the JSON file and generates both nodes
        /// and port connections (and edges) separately
        /// </summary>
        private void Load(string fieldValue = null)
        {
            BehaviorCollection collection = SBTDataManager.LoadBehaviorCollectionToJson(fieldValue);
            
            //Delete all previous elements to not generate duplicates and empty the blackboard
            _graph.DeleteElements(_graph.graphElements);
            
            LoadNodes(collection);

            //Load ViewTransform
            _graph.viewTransform.position = collection.NodeCollection.ViewportPosition;
            _graph.viewTransform.scale = collection.NodeCollection.ViewportScale;
            
            //Load Blackboard Values
            _blackboardGraph.Reset();
            collection.BlackboardCollection.ExposedProperties.ForEach(property => {
                _blackboardGraph.AddNewField(property);
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
                node.Set();
                node.GenerateInterface();
                try { node.ReloadValues(data.Values); } catch { Debug.LogWarning($"Could not reload values of node type {node.GetType()}"); }
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
                foreach (string toGuid in data.toGUIDs)
                {
                    GraphTreeNode node = _graph.GetNodeByGUID(toGuid);
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
            if(!string.IsNullOrEmpty(_lastFieldValue)) { SBTDataManager.SaveEditorToJson(this); }
        }

        //Left blank
        public void OnAfterDeserialize() { }
        
        #endregion
        
        #region Generating Tree, Blackboard & Object Cleanup

        private void GenerateGameObjectComponents()
        {
            // Editor Part
            GameObject selectedObject = Selection.activeGameObject;

            TreeExecutor executor = null;
            if (selectedObject.TryGetComponent(out TreeExecutor component)) { executor = component; }
            executor = selectedObject.AddComponent<TreeExecutor>();
            
            _removeComponentsButton.SetEnabled(true);
            _regenerateButton.SetEnabled(true);
            _generateButton.SetEnabled(false);
            
            //TODO Tree Generation Part
            GenerateBehaviorTree(executor);

            //TODO Blackboard Generation
            GenerateBlackboard();
        }

        private void GenerateBehaviorTree(TreeExecutor executor)
        {
            BehaviorCollection collection = SBTDataManager.LoadBehaviorCollectionToJson(_lastFieldValue);

            if (collection == null)
            {
                EditorUtility.DisplayDialog("Error",
                    $"There is no JSON file of name {_lastFieldValue} to generate. Save the behavior before generating it",
                    "OK");
                return;
            }

            SBTBehaviorGeneration.Generate(_graph, collection.NodeCollection, executor);
            return;
        }

        private void GenerateBehaviorTree()
        {
            TreeExecutor executor = Selection.activeGameObject.GetComponent<TreeExecutor>();
            GenerateBehaviorTree(executor);
        }

        private void GenerateBlackboard()
        {
            GameObject selectedObject = Selection.activeGameObject;
            SBTBlackboard blackboard = null;

            if (selectedObject.TryGetComponent<SBTBlackboard>(out SBTBlackboard bb))
            {
                blackboard = bb; 
                blackboard.GraphData.Clear();
            }
            else { blackboard = selectedObject.AddComponent<SBTBlackboard>(); }
            
            blackboard.Set(); // Adds "Self" to itself before any value
            foreach (ExposedProperty property in _blackboardGraph.ExposedProperties)
            {
                if (property.PropertyName == "Self") { continue; }
                
                BlackboardData data = ScriptableObject.CreateInstance<BlackboardData>();
                data.name = property.PropertyName.ToUpper();
                data.Key = property.PropertyName.ToUpper();
                data.RawValue = property.PropertyRawValue;
                data.VariableType = property.PropertyType;
                
                blackboard.GraphData.Add(data);
            }
        }

        private void RemoveComponents()
        {
            GameObject gameObject = Selection.activeGameObject;
            if (gameObject == null) { return; }
            
            TreeExecutor executor = gameObject.GetComponent<TreeExecutor>();
            SBTBlackboard blackboard = gameObject.GetComponent<SBTBlackboard>();

            if (executor != null)
            {
                DestroyImmediate(executor);
                DestroyImmediate(blackboard);
            }
            
            _removeComponentsButton.SetEnabled(false);
            _regenerateButton.SetEnabled(false);
            _generateButton.SetEnabled(true);
        }
        
        #endregion
    }
}

