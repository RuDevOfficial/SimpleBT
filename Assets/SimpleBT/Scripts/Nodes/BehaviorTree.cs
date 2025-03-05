using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SimpleBT.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor.Tree
{
    [System.Serializable]
    public class BehaviorTree : Node
    {
        public Node Root = null;
        public List<Node> CompleteNodeList = new List<Node>();
        
        public override Status OnTick() { return Tick(); }
        protected override Status Tick() { return Root == null ? Status.Failure : Root.OnTick(); }
        
        public override void RegisterBlackboard(SBTBlackboard sbtBlackboard) { Root?.RegisterBlackboard(sbtBlackboard); }

        public Node GetNodeByGUID(string guid)
        {
            foreach (var node in CompleteNodeList.Where(node => node.GUID == guid)) { return node; }
            if (this.GUID == guid) { return Root;}
            
            return null;
        }
        
        public void LinkNodes(string fromGuid, string toGuid, BehaviorTree tree)
        {
            Node fromNode = tree.GetNodeByGUID(fromGuid);
            Node toNode = tree.GetNodeByGUID(toGuid);

            if (fromNode is INodeMother nodeMother) {
                nodeMother.AddChild(toNode);
            }
        }

        public void AssignRoot(Node node) { Root = node; }
    }
}
