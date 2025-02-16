using System.Linq;
using SimpleBT.Core;

namespace SimpleBT.NonEditor.Nodes
{
    // Ticks all children first.
    // If any return "Success" it succeeds
    // If all return "Failure" it fails
    // If no other conditions are met it keeps running the remaining children.
    // Any children that already returned something else than "Running" will stop ticking
    
    using Composite;
    
    public class ParallelSelector : Composite
    {
        private Status[] _previousStatusCollection;
        
        public ParallelSelector() : base() { }

        public ParallelSelector(params Node[] nodes) : base(nodes)
        {
            _previousStatusCollection = new Status[_children.Count];
            for (int i = 0; i < _children.Count; i++) { _previousStatusCollection[i] = Status.Running; }
        }
        
        protected override Status Tick()
        {
            for (int i = 0; i < _children.Count; i++) {
                if (_previousStatusCollection[i] == Status.Running) { _previousStatusCollection[i] = _children[i].OnTick(); }
            }
            
            if (_previousStatusCollection.Any(state => state == Status.Success)) { return Status.Success; }
            if (_previousStatusCollection.All(state => state == Status.Failure)) { return Status.Failure; }
            
            return Status.Running;
        }
    }
}
