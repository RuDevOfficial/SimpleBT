using System;
using System.Collections.Generic;
using SimpleBT;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SBTSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private SBTGraphView _graph;

    public void Initialize(SBTGraphView graph) { _graph = graph; }
    
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> entries = new List<SearchTreeEntry>()
        {
            new SearchTreeGroupEntry(new GUIContent("Create Node")),
            new SearchTreeEntry(new GUIContent("Root"))
            {
                level = 1,
                userData = new RootNode()
            },
            new SearchTreeGroupEntry(new GUIContent("Composite"), 1),
            new SearchTreeEntry(new GUIContent("Sequence"))
            {
                level = 2,
                userData = new CompositeNode()
            }
        };

        return entries;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        BehaviourTreeNode newNode = (BehaviourTreeNode)SearchTreeEntry.userData;
        Type nameType = newNode.GetType();
        var node = (BehaviourTreeNode)Activator.CreateInstance(nameType);
        Debug.Log(node.GetType());
        node.Instantiate();
        node.Draw();
        node.RefreshPorts();
        node.RefreshExpandedState();
        _graph.AddElement(node);

        return true;
    }
}
