using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.Editor.Data
{
    [System.Serializable]
    public class NodeCollection
    { 
        public NodeData[] Nodes;
        public Vector2 ViewportPosition;
        public Vector2 ViewportScale;
    
        public NodeCollection() { }
        public NodeCollection(NodeData[] nodes) { this.Nodes = nodes; }

        public NodeCollection(NodeData[] nodes, SBTGraphView graph)
        {
            this.Nodes = nodes;
            ViewportPosition = graph.viewTransform.position;
            ViewportScale = graph.viewTransform.scale;
        }
    }
}
