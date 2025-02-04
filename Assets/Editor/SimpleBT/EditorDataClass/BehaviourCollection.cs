using System;
using UnityEngine;

namespace SimpleBT.Editor.Data
{
    [System.Serializable]
    public class BehaviourCollection
    {
        public NodeData[] nodes;
        public Vector2 ViewportPosition;
        public Vector2 ViewportScale;
    
        public BehaviourCollection() { }
        public BehaviourCollection(NodeData[] nodes) { this.nodes = nodes; }
        public BehaviourCollection(string fileName)
        {
            BehaviourCollection collection = SimpleBTDataSystem.LoadNodesFromJson(fileName);
            nodes = collection != null ? collection.nodes : Array.Empty<NodeData>();
            ViewportPosition = collection != null ? collection.ViewportPosition : Vector2.zero;
            ViewportScale = collection != null ? collection.ViewportScale : Vector2.one;
        }
    }
}
