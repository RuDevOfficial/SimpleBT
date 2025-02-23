using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBT.Editor.GraphNodes;
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
                new SearchTreeGroupEntry(new GUIContent("Create Node")),
                new SearchTreeEntry(new GUIContent("Root", _icon)) { level = 1, userData = "RootGraphNode" },
                
                new SearchTreeEntry(new GUIContent("Condition", _icon)) { level = 1, userData = "ConditionGraphNode" },
                
                new SearchTreeGroupEntry(new GUIContent("Composite"), 1),
                new SearchTreeEntry(new GUIContent("Sequence", _icon)) { level = 2, userData = "SequenceGraphNode" },
                new SearchTreeEntry(new GUIContent("Selector", _icon)) { level = 2, userData = "SelectorGraphNode" },
                //new SearchTreeEntry(new GUIContent("ParallelSequenceNode", _icon)) { level = 2, userData = "ParallelSequenceGraphNode" },
                //new SearchTreeEntry(new GUIContent("ParallelSelectorNode", _icon)) { level = 2, userData = "ParallelSelectorGraphNode" },
                new SearchTreeEntry(new GUIContent("Random Sequence", _icon)) { level = 2, userData = "RandomSequenceGraphNode" },
                //new SearchTreeEntry(new GUIContent("ParallelMinSequenceNode", _icon)) { level = 2, userData = "ParallelMinSequenceGraphNode" },
                //new SearchTreeEntry(new GUIContent("ParallelMinSelectorNode", _icon)) { level = 2, userData = "ParallelMinSelectorGraphNode" },
                
                new SearchTreeGroupEntry(new GUIContent("Decorators"), 1),
                new SearchTreeEntry(new GUIContent("Repeat Forever", _icon)) { level = 2, userData = "GraphDecorator_RepeatForever" },

                new SearchTreeGroupEntry(new GUIContent("Actions"), 1),
                new SearchTreeEntry(new GUIContent("Wait", _icon)) { level = 2, userData = "GraphAction_Wait" },
                new SearchTreeEntry(new GUIContent("Set Active", _icon)) { level = 2, userData = "GraphAction_SetActive" },
                new SearchTreeEntry(new GUIContent("Set Active (Toggle)", _icon)) { level = 2, userData = "GraphAction_SetActiveToggle" },
                new SearchTreeEntry(new GUIContent("Debug", _icon)) { level = 2, userData = "GraphAction_Debug" },
                
                new SearchTreeGroupEntry(new GUIContent("Movement"), 2),
                new SearchTreeEntry(new GUIContent("Override Velocity", _icon)) { level = 3, userData = "GraphAction_OverrideVelocity" },
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
        protected virtual List<SearchTreeEntry> AddCustomEntries(SearchWindowContext context, List<SearchTreeEntry> entries)
        {
            return entries;
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
            Type type = Type.GetType($"SimpleBT.Editor.GraphNodes.{searchTreeEntry.userData}");
            
            var node = (GraphTreeNode)Activator.CreateInstance(type);
            
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

