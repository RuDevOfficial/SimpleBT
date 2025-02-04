
namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class SequenceNode : CompositeNode
    {
        public SequenceNode() { NodeName = "Sequence"; }
    }
    
    [System.Serializable]
    public class SelectorNode : CompositeNode
    {
        public SelectorNode() { NodeName = "Selector"; }
    }
}
