namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class ParallelSequenceGraphNode : CompositeNode
    {
        public ParallelSequenceGraphNode()
        {
            Title = "Parallel Sequence";
            ClassReference = "ParallelSequence";
        }
    }
}
