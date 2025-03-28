using System.Collections.Generic;

namespace SimpleBT.Core
{
    public abstract class ExecutionNode : Node
    {
        private bool _initialized = false;
        protected abstract void Initialize();
        public override Status OnTick() {
            if (_initialized == false) { _initialized = true; Initialize(); }
            return Tick();
        }
    }
}
