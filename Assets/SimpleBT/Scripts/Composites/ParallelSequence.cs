using System.Linq;
using SimpleBT.Core;

namespace SimpleBT.Composite.Prebuilt
{
    // Ticks all children first.
    // If all return "Success" it succeeds
    // If any return "Failure" it fails
    // If no other conditions are met it keeps running the remaining children.
    // Any children that already returned something else than "Running" will stop ticking
    
    public class ParallelSequence : Composite
    {
        private Status[] _previousStatusCollection;
        
        public ParallelSequence() : base() { }

        public ParallelSequence(params INode[] nodes) : base(nodes)
        {
            _previousStatusCollection = new Status[_children.Count];
            for (int i = 0; i < _children.Count; i++) { _previousStatusCollection[i] = Status.Running; }
        }
        
        protected override Status Tick()
        {
            for (int i = 0; i < _children.Count; i++) {
                if (_previousStatusCollection[i] == Status.Running) { _previousStatusCollection[i] = _children[i].OnTick(); }
            }
            
            if (_previousStatusCollection.Any(state => state == Status.Failure)) { return Status.Failure; }
            if (_previousStatusCollection.All(state => state == Status.Success)) { return Status.Success; }
            
            return Status.Running;
        }
    }
}
