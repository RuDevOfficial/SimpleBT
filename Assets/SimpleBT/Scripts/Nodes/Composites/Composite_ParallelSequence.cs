using System.Collections.Generic;
using System.Linq;
using SimpleBT.Core;

namespace SimpleBT.NonEditor.Nodes
{
    public class Composite_ParallelSequence : Composite
    {
        List<Status> _childrenStatus = new List<Status>();

        private void Awake() { SetToDefaults(); }

        protected override Status ExecuteFlow()
        {
            for (int i = 0; i < _children.Count; i++) {
                if (_childrenStatus[i] == Status.Running) { _childrenStatus[i] = _children[i].OnTick(); }
            }
            
            if (_childrenStatus.Any(status => status == Status.Failure)) { SetToDefaults(); return Status.Failure; }
            else if (_childrenStatus.All(status => status == Status.Success)) { SetToDefaults(); return Status.Success; }

            return Status.Running;
        }

        public override void OnAbort() {
            for (var i = 0; i < _children.Count; i++) {
                if (_childrenStatus[i] == Status.Running) { _children[i].OnAbort(); }
            }
        }

        void SetToDefaults() {
            _childrenStatus.Clear();
            for (int i = 0; i < _children.Count(); i++) { _childrenStatus.Add(Status.Running); }
        }

        public override void OnDrawGizmos() {
            foreach (var node in _children) { node.OnDrawGizmos(); }
        }
    }
}
