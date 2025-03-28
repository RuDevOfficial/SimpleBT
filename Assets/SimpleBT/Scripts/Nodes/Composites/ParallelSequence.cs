using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBT.Core;

namespace SimpleBT.NonEditor.Nodes
{
    public class ParallelSequence : Composite
    {
        List<Status> ChildrenStatus = new List<Status>();

        private void Awake() { SetToDefaults(); }

        protected override Status Tick()
        {
            for (int i = 0; i < _children.Count; i++) {
                if (ChildrenStatus[i] == Status.Running) { ChildrenStatus[i] = _children[i].OnTick(); }
            }
            
            if (ChildrenStatus.Any(status => status == Status.Failure)) { SetToDefaults(); return Status.Failure; }
            if (ChildrenStatus.All(status => status == Status.Success)) { SetToDefaults(); return Status.Success; }

            return Status.Running;
        }

        void SetToDefaults() {
            ChildrenStatus.Clear();
            for (int i = 0; i < _children.Count(); i++) { ChildrenStatus.Add(Status.Running); }
        }
    }
}
