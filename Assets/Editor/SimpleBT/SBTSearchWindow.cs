using System;
using System.Collections.Generic;
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
                new SearchTreeEntry(new GUIContent("Root", _icon))
                {
                    level = 1,
                    userData = "RootNode"
                },
                new SearchTreeGroupEntry(new GUIContent("Composite"), 1),
                new SearchTreeEntry(new GUIContent("Sequence", _icon))
                {
                    level = 2,
                    userData = "SequenceNode"
                },
                new SearchTreeEntry(new GUIContent("Selector", _icon))
                {
                    level = 2,
                    userData = "SelectorNode"
                },
                new SearchTreeEntry(new GUIContent("Condition", _icon))
                {
                    level = 1,
                    userData = "ConditionNode"
                }
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
            if ((string)searchTreeEntry.userData == "RootNode")
            {
                bool rootLocated = false;
                _graph.graphElements.ForEach(element => { if (element is RootNode) { rootLocated = true; } });

                if (rootLocated == true) {
                    EditorUtility.DisplayDialog("Error", "There is already a Root node in the graph!", "OK");
                    return false;
                }
            }
            
            Vector2 localMousePosition = _graph.GetLocalMousePosition(context.screenMousePosition, true);
            Type type = Type.GetType($"SimpleBT.Editor.GraphNodes.{searchTreeEntry.userData}");
            
            var node = (GraphTreeNode)Activator.CreateInstance(type);
            
            node.Instantiate();
            node.SetPosition(new Rect(localMousePosition, Vector2.zero));
            node.Draw();
            node.RefreshPorts();
            node.RefreshExpandedState();
            
            _graph.AddElement(node);
    
            return true;
        }
    }
}

