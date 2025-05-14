using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.Editor.Data
{
    // Stores all graph node data from a behavior
    [System.Serializable]
    public class NodeCollection
    {
        public string BehaviorName;
        public NodeData[] Nodes;
        public Vector2 ViewportPosition;
        public Vector2 ViewportScale;
    
        public NodeCollection() { }
        public NodeCollection(NodeData[] nodes) { this.Nodes = nodes; }

        public NodeCollection(string behaviorName, NodeData[] nodes, SBTGraphView graph)
        {
            this.BehaviorName = behaviorName;
            this.Nodes = nodes;
            ViewportPosition = graph.viewTransform.position;
            ViewportScale = graph.viewTransform.scale;
        }
    }
}
