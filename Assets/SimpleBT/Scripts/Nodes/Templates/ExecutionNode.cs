using System.Collections.Generic;

namespace SimpleBT.Core
{
    public abstract class ExecutionNode : Node, INodeKeyAssignable
    {  
        protected bool _initialized = false;
        
        public abstract void AssignKeys(List<string> keys);
        
        protected abstract void Initialize();
        public override Status OnTick() {
            if (_initialized == false) { _initialized = true; Initialize(); }
            return Tick();
        }
    }
}
