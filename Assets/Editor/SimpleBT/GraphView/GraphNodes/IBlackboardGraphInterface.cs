using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    public interface IBlackboardGraphInterface
    {
        List<string> GetValues();
        void ReloadValues(List<string> values);
    }
}
