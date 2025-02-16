using System;
using System.Linq;
using SimpleBT.Core;

namespace SimpleBT.NonEditor.Nodes
{
    using Composite;
    
    // Similar to ParallelSequence.
    // You can set up the minimum amount of "Success" needed to return "Success"
    public class ParallelMinSequence : Composite
    {
        private Status[] _previousStatusCollection;
        private readonly int _threshold;
        
        public ParallelMinSequence() : base() { }

        public ParallelMinSequence(int threshold, params Node[] nodes) : base(nodes)
        {
            _previousStatusCollection = new Status[_children.Count];
            for (int i = 0; i < _children.Count; i++) { _previousStatusCollection[i] = Status.Running; }

            _threshold = Math.Min(threshold, _children.Count - 1);
        }
        
        protected override Status Tick()
        {
            for (int i = 0; i < _children.Count; i++) {
                if (_previousStatusCollection[i] == Status.Running) { _previousStatusCollection[i] = _children[i].OnTick(); }
            }

            if (_previousStatusCollection.Any(state => state == Status.Failure)) { return Status.Failure; }
            if (_previousStatusCollection.Count(state => state == Status.Success) - 1 >= _threshold) { return Status.Success; }
            
            return Status.Running;
        }
    }
}
