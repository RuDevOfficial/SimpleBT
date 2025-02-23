using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    using Utils;
    
    [System.Serializable]
    public class SequenceGraphNode : CompositeNode
    {
        public SequenceGraphNode()
        {
            Title = "Sequence";
            ClassReference = "Sequence";
        }
    }
}
