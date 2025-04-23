using System.Linq;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Composite_Priority : Composite
    {
        private int _lastIndex = -1;
        private bool _analyzed = false;

        private Condition[] _conditionals;
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override Status Tick()
        {
            if (_analyzed == false) { if (AnalyzeAndFillArray() == false) { return Status.Failure; } };
            
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

            return _lastIndex < 0 ? Status.Failure : _children[_lastIndex].OnTick();
        }

        private bool AnalyzeAndFillArray()
        {
            _conditionals = new Condition[_children.Count];
            
            for (int i = 0; i < _children.Count; i++) {
                if (_children[i] is not Composite composite)
                {
                    Debug.LogError("All Children nodes of Composite_Priority must be a Composite. Returning Failure.");
                    return false;
                }

                _conditionals[i] = composite.GetFirstConditional();
            }
                
            _analyzed = true;

            return true;
        }
    }
}
