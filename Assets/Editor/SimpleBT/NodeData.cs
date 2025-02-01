using System;
using UnityEngine;
using UnityEngine.Serialization;
[System.Serializable]
public class NodeData
{
    [SerializeReference] public BehaviourTreeNode Node;
}
