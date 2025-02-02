using System;
using System.Collections.Generic;
using SimpleBT;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class SBTSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private SBTGraphView _graph;
    private Texture2D _icon;

    public void Initialize(SBTGraphView graph)
    {
        _graph = graph;
        
        _icon = new Texture2D(1, 1);
        _icon.SetPixel(0, 0, Color.clear);
    }
    
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> entries = new List<SearchTreeEntry>()
        {
            new SearchTreeGroupEntry(new GUIContent("Create Node")),
            new SearchTreeEntry(new GUIContent("Root", _icon))
            {
                level = 1,
                userData = new RootNode()
            },
            new SearchTreeGroupEntry(new GUIContent("Composite"), 1),
            new SearchTreeEntry(new GUIContent("Sequence", _icon))
            {
                level = 2,
                userData = new SequenceNode()
            }
        };

        return entries;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        Vector2 localMousePosition = _graph.GetLocalMousePosition(context.screenMousePosition, true);
        
        BehaviourTreeNode newNode = (BehaviourTreeNode)SearchTreeEntry.userData;
        Type nameType = newNode.GetType();
        var node = (BehaviourTreeNode)Activator.CreateInstance(nameType);
        node.Instantiate();
        node.SetPosition(new Rect(localMousePosition, Vector2.zero));
        node.Draw();
        node.RefreshPorts();
        node.RefreshExpandedState();
        _graph.AddElement(node);

        return true;
    }
}
