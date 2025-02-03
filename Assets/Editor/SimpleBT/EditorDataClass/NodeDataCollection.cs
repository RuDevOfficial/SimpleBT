using System;

namespace SimpleBT.Editor.Data
{
    [System.Serializable]
    public class NodeDataCollection
    {
        public NodeData[] nodes;
    
        public NodeDataCollection() { }
        public NodeDataCollection(NodeData[] nodes) { this.nodes = nodes; }
        public NodeDataCollection(string fileName)
        {
            NodeDataCollection collection = SimpleBTDataSystem.LoadNodesFromJson(fileName);
            nodes = collection != null ? collection.nodes : Array.Empty<NodeData>();
        }
    }
}
