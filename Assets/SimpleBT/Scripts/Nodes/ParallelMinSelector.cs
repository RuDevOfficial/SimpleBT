using System;
using System.Linq;

namespace SimpleBT.NonEditor.Nodes
{
    using Composite;
    using Core;
    
    // Similar to ParallelSequence.
    // You can set up the minimum amount of "Failure" needed to return "Failure"
    public class ParallelMinSelector : Composite
    {
        private Status[] _previousStatusCollection;
        private int _threshold;
        
        public ParallelMinSelector() : base() { }
        
        public ParallelMinSelector(int threshold, params Node[] nodes) : base(nodes)
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

            if (_previousStatusCollection.Any(state => state == Status.Success)) { return Status.Success; }
            if (_previousStatusCollection.Count(state => state == Status.Failure) - 1 >= _threshold) { return Status.Failure; }

            return Status.Running;
        }
    }

}
