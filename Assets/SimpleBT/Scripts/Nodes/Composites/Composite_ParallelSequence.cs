using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBT.Core;

namespace SimpleBT.NonEditor.Nodes
{
    public class Composite_ParallelSequence : Composite
    {
        List<Status> ChildrenStatus = new List<Status>();

        private void Awake() { SetToDefaults(); }

        protected override Status ExecuteFlow()
        {
            for (int i = 0; i < _children.Count; i++) {
                if (ChildrenStatus[i] == Status.Running) { ChildrenStatus[i] = _children[i].OnTick(); }
            }
            
            if (ChildrenStatus.Any(status => status == Status.Failure)) { SetToDefaults(); return Status.Failure; }
            else if (ChildrenStatus.All(status => status == Status.Success)) { SetToDefaults(); return Status.Success; }

            return Status.Running;
        }

        public override void OnAbort() {
            for (var i = 0; i < _children.Count; i++) {
                if (ChildrenStatus[i] == Status.Running) { _children[i].OnAbort(); }
            }
        }

        void SetToDefaults() {
            ChildrenStatus.Clear();
            for (int i = 0; i < _children.Count(); i++) { ChildrenStatus.Add(Status.Running); }
        }

        public override void OnDrawGizmos() {
            foreach (var node in _children) { node.OnDrawGizmos(); }
        }
    }
}
