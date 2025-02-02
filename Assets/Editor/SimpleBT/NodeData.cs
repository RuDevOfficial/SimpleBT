using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NodeData
{
    [SerializeReference] public BehaviourTreeNode Node;
    public string fromGUID;
    public List<string> toGUIDs = new List<string>();
}