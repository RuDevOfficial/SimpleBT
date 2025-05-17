using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using Object = UnityEngine.Object;

namespace SimpleBT.Editor
{
    #region Usings
    // Editor
    using Data;
    using GraphNodes;
    using Blackboard;
    using Utils;
    using BehaviorGeneration;
    
    // Non-Editor
    using NonEditor.Tree;
    using NonEditor;
    #endregion
    
    [System.Serializable]
    public class SBTEditorWindow : EditorWindow
    {
        // Starting position and size for the blackboard upon generation
        private readonly Rect _blackboardRect = new Rect(0, 30, 250, 350);
        
        // When opening the window it generates Graph and BlackboardGraph references and stores them
        private SBTGraphView _graph;
        private SBTBlackboardGraph _blackboardGraph;

        // All toolbar UI elements
        #region Toolbar UI Elements
        private ToolbarSearchField _field;
        
        private ToolbarButton _generateButton;
        private ToolbarButton _removeComponentsButton;
        private ToolbarButton _regenerateButton;
        [SerializeReference] public ObjectField ObjectField;

        private TipsLabel _tipsButtonLabel;
        #endregion
        
        // This allows for the value to stay between closing and opening the window
        #region Fields
        // private
        private static string _lastFieldValue;
        private static string _lastFilePath;
        private static SBTCustomEntryScriptable _lastObjectScriptable;
        
        // public
        public string LastFieldValue => _lastFieldValue;
        public string LastFilePath => _lastFilePath;
        public SBTCustomEntryScriptable LastObjectScriptable => _lastObjectScriptable;
        #endregion
        
        // Required to create the window and use the tool
        [MenuItem("SimpleBT/Window")]
        public static void Open()
        {
            SBTEditorWindow wnd = GetWindow<SBTEditorWindow>();
            wnd.titleContent = new GUIContent("Simple BT");
        }

        private void Update()
        {
            _tipsButtonLabel?.Update();
        }

        #region Event Functions
        
        private void OnEnable()
        {
            GenerateGraph();
            GenerateVisualElements();
            LoadEditorData();
            Load(_lastFieldValue);
        }

        // Make sure to save any necessary data before its serialized
        private void OnDisable() { SBTDataManager.SaveEditorToJson(this); }
        
        // Filters the selected object on the Project tab to be a .simple file in order to load
        // It also filters if the current selection is a gameObject, which enables certain buttons for BT generation
        private void OnSelectionChange()
        {
            Object selectedObject = Selection.activeObject;

            if (selectedObject == null) { _generateButton.SetEnabled(false); return; }
            if (AssetDatabase.Contains(selectedObject)) { OverwriteGraph(selectedObject); _generateButton.SetEnabled(false); }
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
            if (SBTEditorUtils.TryGetBehaviorFile(selectedObject, out string fileName))
            {
                if (fileName != _field.value)
                {
                    Load(fileName, true);
                    _lastFilePath = AssetDatabase.GetAssetPath(Selection.activeObject);
                    _field.value = fileName;
                    _lastFieldValue = fileName;
                }
            }
        }

        #endregion
        
        #region OnEnable Child Event Functions

        private void GenerateGraph()
        {
            _graph = new SBTGraphView(this);
            _graph.StretchToParentSize();
            rootVisualElement.Add(_graph);
        }
        
        private void GenerateVisualElements()
        {
            // Toolbar itself does not need to hold a reference and must be added to the rootVisualElement just after graph view has been added
            Toolbar toolbar = new Toolbar();
            toolbar.AddToClassList("Toolbar");
            rootVisualElement.Add(toolbar);

            // Buttons that do not require to keep a reference
            ToolbarButton saveButton = new ToolbarButton(Save) { text = "Save" };
            ToolbarButton loadButton = new ToolbarButton(() => { Load(_field.value); }) { text = "Load" };
            ToolbarButton clearBlackboardButton = new ToolbarButton(ClearBlackboard) { text = "Clear Blackboard" };
            
            // UI elements that DO require to keep a reference
            _field = new ToolbarSearchField() { value = _lastFieldValue ?? "New Behaviour Tree" };
            _generateButton = new ToolbarButton(GenerateBehavior) { text = "Generate" };
            _regenerateButton = new ToolbarButton(() => { Save(); GenerateBlackboard(); GenerateBehaviorTree(); }) { text = "Save & Regenerate" }; 
            _removeComponentsButton = new ToolbarButton(RemoveComponents) { text = "Remove Components" };
            ObjectField = new ObjectField();
            
            // ValueChangedCallback overrides
            _field.RegisterValueChangedCallback(evt =>
            {
                var newValue = evt.newValue;
                var filteredValue = newValue.FilterValue();
                        
                _lastFieldValue = filteredValue;
                _field.value = filteredValue;
            });
            ObjectField.RegisterValueChangedCallback(evt => _lastObjectScriptable = (SBTCustomEntryScriptable)evt.newValue);
            
            // Overriding Object Field Parameters
            ObjectField.objectType = typeof(SBTCustomEntryScriptable);
            ObjectField.style.maxWidth = 300;
            ObjectField.label = "Custom Entries SO: ";
            
            // UI elements added in order (left to right)
            toolbar.Add(_field);
            toolbar.Add(saveButton);
            toolbar.Add(loadButton);
            toolbar.Add(_generateButton);
            toolbar.Add(_regenerateButton);
            toolbar.Add(_removeComponentsButton);
            toolbar.Add(clearBlackboardButton);
            toolbar.Add(ObjectField);
            
            // Adding the Blackboard
            _blackboardGraph = new SBTBlackboardGraph(_graph);
            _blackboardGraph.SetPosition(_blackboardRect);
            _graph.Add(_blackboardGraph);
            
            // Adding the tips section
            Toolbar tipsToolBar = new Toolbar();

            _tipsButtonLabel = new TipsLabel() { text = "This is a default tip. More to come every 10 seconds" };
            _tipsButtonLabel.SetDefaults();
            tipsToolBar.Add(_tipsButtonLabel);
            
            rootVisualElement.Add(tipsToolBar);
        }

        private void LoadEditorData()
        {
            SBTEditorData editorData = SBTDataManager.LoadEditorFromJson();

            _lastFieldValue = editorData.LastFileName;
            _field.value = _lastFieldValue;

            _lastObjectScriptable = editorData.scriptable;
            ObjectField.value = _lastObjectScriptable;
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

        private List<ExposedProperty> SaveBlackboardProperties() { return new List<ExposedProperty>(_blackboardGraph.ExposedProperties); }
        
        /// <summary>
        /// This method loads the data in the JSON file and generates both nodes
        /// and port connections (and edges) separately
        /// </summary>
        private void Load(string fieldValue = null, bool loadWithSelection = false)
        {
            BehaviorCollection collection = SBTDataManager.LoadBehaviorCollectionToJson(fieldValue, loadWithSelection);
            
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

        private void ClearBlackboard() { _blackboardGraph?.Reset(); }

        #endregion
        
        #region Generation

        // Parent method that takes care of both Behavior and Blackboard generation logic
        private void GenerateBehavior()
        {
            GameObject selectedObject = Selection.activeGameObject;

            TreeExecutor executor = null;
            if (selectedObject.TryGetComponent(out TreeExecutor component)) { executor = component; }
            executor = selectedObject.AddComponent<TreeExecutor>();
            
            _removeComponentsButton.SetEnabled(true);
            _regenerateButton.SetEnabled(true);
            _generateButton.SetEnabled(false);
            
            GenerateBehaviorTree(executor);
            GenerateBlackboard();
        }

        // Makes sure that the behavior is saved before generating any behaviors
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

            SBTBehaviorGeneration.Generate(collection.NodeCollection, executor);
            return;
        }

        private void GenerateBehaviorTree()
        {
            TreeExecutor executor = Selection.activeGameObject.GetComponent<TreeExecutor>();
            GenerateBehaviorTree(executor);
        }

        // Upon generating the blackboard checks if there's already a Blackboard script,
        // if it's the case it clears it before re-adding all parameters
        private void GenerateBlackboard()
        {
            GameObject selectedObject = Selection.activeGameObject;
            SBTBlackboard blackboard = null;

            // Filter
            if (selectedObject.TryGetComponent<SBTBlackboard>(out SBTBlackboard bb)) {
                blackboard = bb; 
                blackboard.GraphData.Clear();
            }
            else { blackboard = selectedObject.AddComponent<SBTBlackboard>(); }
            
            // Adding stored blackboard parameters
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

        // Executed when "Remove Components" is pressed and updates certain buttons
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

