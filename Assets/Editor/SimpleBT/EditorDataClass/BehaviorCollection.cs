using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.Editor.Data
{
    [System.Serializable]
    public class BehaviorCollection
    {
        public string BehaviorName;
        public NodeCollection NodeCollection;
        public BlackboardCollection BlackboardCollection;
        
        public BehaviorCollection() { }
        public BehaviorCollection(string name, NodeData[] nodeDatas, List<ExposedProperty> exposedProperties)
        {
            BehaviorName = name;
            NodeCollection = new NodeCollection(nodeDatas);
            BlackboardCollection = new BlackboardCollection(exposedProperties);
        }

        public BehaviorCollection(string fileName)
        {
            NodeCollection = new NodeCollection();
            BlackboardCollection = new BlackboardCollection();
            
            BehaviorCollection collection = SBTDataManager.LoadBehaviorCollectionToJson(fileName);
            
            BehaviorName = collection.BehaviorName;
            
            //GraphCollection
            NodeCollection.Nodes = collection.NodeCollection.Nodes != null ? collection.NodeCollection.Nodes : Array.Empty<NodeData>();
            NodeCollection.ViewportPosition = collection.NodeCollection.ViewportPosition;
            NodeCollection.ViewportScale = collection.NodeCollection.ViewportScale;
            
            //BlackboardCollection
            BlackboardCollection.ExposedProperties = collection.BlackboardCollection.ExposedProperties;
        }
    }
}
