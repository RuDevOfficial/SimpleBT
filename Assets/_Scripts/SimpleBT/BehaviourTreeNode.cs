using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class BehaviourTreeNode : Node
{
    [SerializeField] public string GUID;
    [SerializeField] public Rect Rect;

    public string NodeName;
    
    public BehaviourTreeNode() { }
    
    public virtual void Instantiate() { GUID = Guid.NewGuid().ToString(); }

    public virtual void Draw() { title = NodeName; }
}


