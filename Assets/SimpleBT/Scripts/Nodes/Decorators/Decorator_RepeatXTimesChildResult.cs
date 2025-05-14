using SimpleBT.Core;

namespace SimpleBT.NonEditor.Nodes
{
    public class Decorator_RepeatXTimesChildResult : Decorator_RepeatXTimes
    {
        protected override Status Tick()
        {
            _times += 1;
            Status result = Child.OnTick();
            if (_times < _maxTimes) return Status.Running;
            
            _times = 0;
            return result;
        }
    }
}
