using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SimpleBT.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor.Tree
{
    [System.Serializable]
    public class BehaviourTree : Node
    {
        public Node Root = null;
        public List<Node> CompleteNodeList = new List<Node>();
        
        public Status OnTick() { return Tick(); }
        
        protected override Status Tick() { return Root == null ? Status.Failure : Root.OnTick(); }
        
        private void Awake() { Build(); }

        public void RegisterBlackboard(SBTBlackboard sbtBlackboard) { Root.RegisterBlackboard(sbtBlackboard); }

        protected virtual void Build() { }

        public Node GetNodeByGuid(string guid)
        {
            foreach (var node in CompleteNodeList.Where(node => node.GUID == guid)) { return node; }
            if (this.GUID == guid) { return Root;}
            
            return null;
        }
        
        public void LinkNodes(string fromGuid, string toGuid, BehaviourTree tree)
        {
            Node fromNode = tree.GetNodeByGuid(fromGuid);
            Node toNode = tree.GetNodeByGuid(toGuid);
        
            fromNode.AddChild(toNode);
        }

        public void AssignRoot(Node node)
        {
            Root = node;
        }
    }
}
