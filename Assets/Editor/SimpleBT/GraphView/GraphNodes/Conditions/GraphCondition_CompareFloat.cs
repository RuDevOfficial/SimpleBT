using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    using Utils;
    using NonEditor.Nodes;
    
    [System.Serializable]
    public class GraphCondition_CompareFloat : GraphCondition_CompareTwoValues
    {
        public GraphCondition_CompareFloat()
        {
            Title = "Compare Float"; // Rename like: GraphCondition_DoSomething -> Do Something
            ClassReference = "Condition_CompareFloat"; // Rename like: GraphCondition_DoSomething -> Condition_DoSomething
        }
    }
}

