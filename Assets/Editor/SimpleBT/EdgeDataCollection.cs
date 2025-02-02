using UnityEditor.Experimental.GraphView;
using UnityEngine;
[System.Serializable]
public class EdgeDataCollection
{
    public EdgeData[] edges;
    
    public EdgeDataCollection() { }
    public EdgeDataCollection(EdgeData[] edges) { this.edges = edges; }
    public EdgeDataCollection(string fileName)
    {
    }
}
