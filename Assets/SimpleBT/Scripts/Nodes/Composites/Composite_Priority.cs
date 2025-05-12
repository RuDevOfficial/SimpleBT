using System.Linq;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Composite_Priority : Composite
    {
        private int _lastIndex = -1;
        private Condition[] _conditionals = null;
        
        protected override void Initialize() { AnalyzeAndFillArray(); }

        protected override Status ExecuteFlow()
        {
            for (int i = 0; i < _conditionals.Length; i++)
            {
                Status status = _conditionals[i].OnTick();
                if (status == Status.Failure) { continue; }
                
                if (status == Status.Success) {
                    if (_lastIndex != i) {
                        if (_lastIndex > 0) { _children[_lastIndex].OnAbort(); }
                        _lastIndex = i;
                        break;
                    }

                    break;
                }
                
                // If this line is ran through code it means no condition on
                // each composite has a conditiona clause or it
                // doesn't have an "always succeed" condition in the least important child
                _lastIndex = -1;
            }

            if (_lastIndex < 0) { return Status.Failure; }
            else
            {
                _childrenIndex = _lastIndex;
                return _children[_childrenIndex].OnTick();
            }
        }
        
        private void AnalyzeAndFillArray()
        {
            _conditionals = new Condition[_children.Count];
            
            for (var i = 0; i < _children.Count; i++) {
                if (_children[i] is Composite composite) { _conditionals[i] = composite.GetFirstConditional(); }
                else { Debug.LogError("All Children nodes of Composite_Priority must be a Composite. Returning Failure."); }
            }
        }

        public override void OnDrawGizmos()
        {
            if (_conditionals == null) return;

            foreach (Condition condition in _conditionals) { condition.OnDrawGizmos(); }
        }
    }
}
