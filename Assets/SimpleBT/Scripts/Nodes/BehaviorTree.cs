﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SimpleBT.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.NonEditor.Nodes
{
    [System.Serializable]
    public class BehaviorTree : Node, INodeKeyAssignable
    {
        public Node Root = null;
        public List<Node> CompleteNodeList = new List<Node>();
        public string RelatedBranch;
        
        public override Status OnTick() { return Tick(); }
        protected override Status Tick() { return Root == null ? Status.Failure : Root.OnTick(); }
        
        public override void RegisterBlackboard(SBTBlackboard sbtBlackboard) { Root?.RegisterBlackboard(sbtBlackboard); }

        public Node GetNodeByGUID(string guid)
        {
            foreach (var node in CompleteNodeList.Where(node => node.GUID == guid)) {
                return node;
            }
            
            return this.GUID == guid ? Root : null;
        }
        
        public void LinkNodes(string fromGuid, string toGuid)
        {
            Node fromNode = GetNodeByGUID(fromGuid);
            Node toNode = GetNodeByGUID(toGuid);

            if (fromNode is INodeMother nodeMother) {
                nodeMother.AddChild(toNode);
            }
        }

        public void AssignRoot(Node node) { Root = node; }
        public void AssignKeys(List<string> keys)
        {
            RelatedBranch = keys[1];
        }
    }
}
