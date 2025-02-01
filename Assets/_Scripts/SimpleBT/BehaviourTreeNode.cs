using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class BehaviourTreeNode : Node
{
    [SerializeField] public string GUID;
    [SerializeField] public Rect Rect;

    public BehaviourTreeNode() { }
    
    public virtual void Instantiate() { GUID = Guid.NewGuid().ToString(); }

    public virtual void Draw() { }
}


