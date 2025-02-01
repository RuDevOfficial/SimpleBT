using System;
[System.Serializable]
public class NodeDataCollection
{
    public NodeData[] nodes;
    
    public NodeDataCollection() { }
    public NodeDataCollection(NodeData[] nodes) { this.nodes = nodes; }
    public NodeDataCollection(string fileName)
    {
        NodeDataCollection collection = SimpleBTDataSystem.Load(fileName);
        nodes = collection != null ? collection.nodes : Array.Empty<NodeData>();
    }
}
