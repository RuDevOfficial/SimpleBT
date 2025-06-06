﻿using System.Collections.Generic;
using System.Linq;
using SimpleBT.NonEditor;
using SimpleBT.NonEditor.Nodes;
using UnityEngine;

namespace SimpleBT.Core
{
    public abstract class Composite : Node, INodeMother
    {
        [SerializeReference] protected List<Node> _children = new List<Node>();
        protected int _childrenIndex = 0;

        // Checks if the composite has any children fist
        protected override Status Tick() { return _children.Count == 0 ? Status.Success : ExecuteFlow(); }
        public override void OnAbort() { _children[_childrenIndex].OnAbort(); }
        protected abstract Status ExecuteFlow();
        
        public override void RegisterBlackboard(SBTBlackboard sbtBlackboard)
        {
            base.RegisterBlackboard(sbtBlackboard);
            foreach(Node node in _children) { node.RegisterBlackboard(sbtBlackboard); }
        }
        
        public Condition GetFirstConditional() {
            for (int i = 0; i < _children.Count; i++) {
                if (_children[i] is Condition condition) {
                    return condition;
                }
            }
            
            Debug.LogWarning("This composite doesn't have an initial conditional");
            return null;
        }

        public void AddChild(Node child) { _children.Add(child); }

        public override void OnDrawGizmos()
        {
            foreach (var node in _children.Where(node => node == _children[_childrenIndex])) {
                node.OnDrawGizmos();
            }
        }
    }
}
