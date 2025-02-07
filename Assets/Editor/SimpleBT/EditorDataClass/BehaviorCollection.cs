using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.Editor.Data
{
    [System.Serializable]
    public class BehaviorCollection
    {
        public NodeCollection NodeCollection;
        [FormerlySerializedAs("blackboardCollection")] public BlackboardCollection BlackboardCollection;
        
        public BehaviorCollection() { }
        public BehaviorCollection(NodeData[] nodeDatas, List<ExposedProperty> exposedProperties, Vector2 blackboardPosition, Vector2 blackboardScale)
        {
            NodeCollection = new NodeCollection(nodeDatas);
            BlackboardCollection = new BlackboardCollection(exposedProperties);
        }

        public BehaviorCollection(string fileName)
        {
            NodeCollection = new NodeCollection();
            BlackboardCollection = new BlackboardCollection();
            
            BehaviorCollection collection = SimpleBTDataSystem.LoadBehaviorCollectionToJson(fileName);
            
            //GraphCollection
            NodeCollection.Nodes = collection.NodeCollection.Nodes != null ? collection.NodeCollection.Nodes : Array.Empty<NodeData>();
            NodeCollection.ViewportPosition = collection.NodeCollection.ViewportPosition;
            NodeCollection.ViewportScale = collection.NodeCollection.ViewportScale;
            
            //BlackboardCollection
            BlackboardCollection.ExposedProperties = collection.BlackboardCollection.ExposedProperties;
        }
    }
}
