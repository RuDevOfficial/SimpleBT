using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SimpleBT.Editor
{
    using Editor.GraphNodes;
    
    public class SBTSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private SBTGraphView _graph;
        private Texture2D _icon;
        private string ParentBehaviorName;
    
        public void Initialize(SBTGraphView graph)
        {
            _graph = graph;
            _icon = new Texture2D(1, 1);
            _icon.SetPixel(0, 0, Color.clear); // Currently bugged
        }
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> entries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Spawn Node")),
                
                new SearchTreeEntry(new GUIContent("Root", _icon)) { level = 1, userData = "RootGraphNode" },
                
                new SearchTreeGroupEntry(new GUIContent("Conditions"), 1),
                    new SearchTreeEntry(new GUIContent("Always Succeed", _icon)) { level = 2, userData = "GraphCondition_AlwaysSucceed" },
                    new SearchTreeEntry(new GUIContent("Always Fail", _icon)) { level = 2, userData = "GraphCondition_AlwaysFail" },
                    new SearchTreeEntry(new GUIContent("Comparison Condition", _icon)) { level = 2, userData = "GraphCondition_Comparison" },
                    new SearchTreeEntry(new GUIContent("Is Near Ledge 2D", _icon)) { level = 2, userData = "GraphCondition_IsNearLedge2D" },
                    new SearchTreeEntry(new GUIContent("Is at Minimum Distance", _icon)) { level = 2, userData = "GraphCondition_IsAtMinimumDistance" },
                    new SearchTreeEntry(new GUIContent("Is GameObject Close 2D", _icon)) { level = 2, userData = "GraphCondition_IsGameObjectClose2D" },
                    new SearchTreeEntry(new GUIContent("Is GameObject Close 3D", _icon)) { level = 2, userData = "GraphCondition_IsGameObjectClose3D" },

                new SearchTreeEntry(new GUIContent("Branch", _icon)) { level = 1, userData = "BehaviorTreeGraphNode" },
                
                new SearchTreeGroupEntry(new GUIContent("Blackboard"), 1),
                    new SearchTreeEntry(new GUIContent("Remove Key", _icon)) { level = 2, userData = "GraphAction_RemoveKey" },
                    new SearchTreeEntry(new GUIContent("Invert Numerical Value", _icon)) { level = 2, userData = "GraphAction_InvertNumValue" },
                
                new SearchTreeGroupEntry(new GUIContent("Composite"), 1),
                    new SearchTreeEntry(new GUIContent("Sequence", _icon)) { level = 2, userData = "GraphComposite_Sequence" },
                    new SearchTreeEntry(new GUIContent("Selector", _icon)) { level = 2, userData = "GraphComposite_Selector" },
                    new SearchTreeEntry(new GUIContent("Parallel Sequence", _icon)) { level = 2, userData = "GraphComposite_ParallelSequence" },
                    new SearchTreeEntry(new GUIContent("Random Sequence", _icon)) { level = 2, userData = "GraphComposite_RandomSequence" },
                    new SearchTreeEntry(new GUIContent("Priority", _icon)) { level = 2, userData = "GraphComposite_Priority" },
                
                new SearchTreeGroupEntry(new GUIContent("Decorators"), 1),
                    new SearchTreeEntry(new GUIContent("Repeat Forever", _icon)) { level = 2, userData = "GraphDecorator_RepeatForever" },
                    new SearchTreeEntry(new GUIContent("Execute Once With Delay", _icon)) { level = 2, userData = "GraphDecorator_ExecuteOnceWithDelay" },

                new SearchTreeGroupEntry(new GUIContent("Flow"), 1),
                    new SearchTreeEntry(new GUIContent("Wait X Seconds", _icon)) { level = 2, userData = "GraphAction_Wait" },
                
                new SearchTreeGroupEntry(new GUIContent("Actions"), 1),
                    new SearchTreeGroupEntry(new GUIContent("General"), 2),
                        new SearchTreeEntry(new GUIContent("Always Succeed", _icon)) { level = 3, userData = "GraphAction_AlwaysSucceed" },
                        new SearchTreeEntry(new GUIContent("Send Message", _icon)) { level = 3, userData = "GraphAction_SendMessage" },
                        new SearchTreeEntry(new GUIContent("Override Tag", _icon)) { level = 3, userData = "GraphAction_OverrideTag" },
                        new SearchTreeEntry(new GUIContent("Parent GameObject to Self", _icon)) { level = 3, userData = "GraphAction_ParentObjectToSelf" },
                        new SearchTreeEntry(new GUIContent("Unparent GameObject", _icon)) { level = 3, userData = "GraphAction_UnparentGameObject" },
                        new SearchTreeEntry(new GUIContent("Override GameObject Position 3D", _icon)) { level = 3, userData = "GraphAction_OverrideGameObjectPosition3D" },
                        new SearchTreeEntry(new GUIContent("Destroy GameObject", _icon)) { level = 3, userData = "GraphAction_DestroyGameObject" },
                        new SearchTreeEntry(new GUIContent("Set Active", _icon)) { level = 3, userData = "GraphAction_SetActive" },
                        new SearchTreeEntry(new GUIContent("Set Active (Toggle)", _icon)) { level = 3, userData = "GraphAction_SetActiveToggle" },
                        new SearchTreeEntry(new GUIContent("Store Random Position 3D", _icon)) { level = 3, userData = "GraphAction_StoreRandomPosition3D" },
                    new SearchTreeGroupEntry(new GUIContent("Movement"), 2),
                        new SearchTreeEntry(new GUIContent("Stop", _icon)) { level = 3, userData = "GraphAction_Stop" },
                        new SearchTreeGroupEntry(new GUIContent("2D"), 3),
                            new SearchTreeEntry(new GUIContent("Follow 2D", _icon)) { level = 4, userData = "GraphAction_Follow2D" },
                            new SearchTreeEntry(new GUIContent("Flee 2D", _icon)) { level = 4, userData = "GraphAction_Flee2D" },
                            new SearchTreeEntry(new GUIContent("Go To Position 2D", _icon)) { level = 4, userData = "GraphAction_GoToPosition2D" },
                            new SearchTreeEntry(new GUIContent("Linear Move 2D", _icon)) { level = 4, userData = "GraphAction_LinearMove2D" },
                        new SearchTreeGroupEntry(new GUIContent("3D"), 3),
                            new SearchTreeEntry(new GUIContent("Follow 3D", _icon)) { level = 4, userData = "GraphAction_Follow3D" },
                            new SearchTreeEntry(new GUIContent("Flee 3D", _icon)) { level = 4, userData = "GraphAction_Flee3D" },
                            new SearchTreeEntry(new GUIContent("Go To Position 3D", _icon)) { level = 4, userData = "GraphAction_GoToPosition3D" },
                        
                new SearchTreeGroupEntry(new GUIContent("Other"), 1),
                    new SearchTreeEntry(new GUIContent("Debug", _icon)) { level = 2, userData = "GraphAction_Debug" },
                    
                new SearchTreeGroupEntry(new GUIContent("Custom"), 1),
            };

            AddCustomEntries(context, entries);
    
            return entries;
        }

        /// <summary>
        /// Method used to add additional custom entries.
        /// This should be called from a class that inherits SBTSearchWindow and should be overwritten.
        /// </summary>
        /// <param name="context"> The SearchWindowContext parameter </param>
        /// <param name="entries"> The pre-built list of entries </param>
        /// <returns></returns>
        private void AddCustomEntries(SearchWindowContext context, List<SearchTreeEntry> entries)
        {
            SBTSearchTreeEntryAddon addon = (SBTSearchTreeEntryAddon)_graph.EditorReference.ObjectField.value;
            if (addon != null) { entries.AddRange(addon.GetEntries(context)); }
        }
    
        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            // Check if there was already a root node on the graph
            if ((string)searchTreeEntry.userData == "RootGraphNode")
            {
                bool rootLocated = false;
                _graph.graphElements.ForEach(element => { if (element is RootGraphNode) { rootLocated = true; return; } });

                if (rootLocated == true) {
                    EditorUtility.DisplayDialog("Error", "There is already a Root node in the graph!", "OK");
                    return false;
                }
            }
            else
            {
                bool rootLocated = false;
                _graph.graphElements.ForEach(element => { if (element is RootGraphNode) { rootLocated = true; return; } });

                if (rootLocated == false) {
                    EditorUtility.DisplayDialog("Error", "There is no Root node! Add it first!", "OK");
                    return false;
                }
            }
            
            Vector2 localMousePosition = _graph.GetLocalMousePosition(context.screenMousePosition, true);
            string dataString = searchTreeEntry.userData.ToString();
            Type type;

            if (dataString.Contains("Custom_"))
            {
                dataString = dataString.Remove(0, 7);
                type = Type.GetType($"{dataString}");
            }
            else { type = Type.GetType($"SimpleBT.Editor.GraphNodes.{dataString}"); }

            var node = (GraphTreeNode)Activator.CreateInstance(type);
            
            if (node is BehaviorTreeGraphNode btNode) { btNode.ReferencedBehaviorName = _graph.EditorReference.LastFieldValue; }
            node.Instantiate();
            node.SetPosition(new Rect(localMousePosition, Vector2.zero));
            node.Set();
            node.GenerateInterface();
            node.RefreshPorts();
            node.RefreshExpandedState();
            
            _graph.AddElement(node);
    
            return true;
        }
    }

}

