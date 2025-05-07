using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    using Utils;
    using NonEditor.Nodes;
    
    [System.Serializable]
    public class GraphCondition_CompareString : GraphCondition_CompareTwoValues
    {
        public GraphCondition_CompareString()
        {
            Title = "Compare String"; // Rename like: GraphCondition_DoSomething -> Do Something
            ClassReference = "Condition_CompareString"; // Rename like: GraphCondition_DoSomething -> Condition_DoSomething
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _comparisonField.choices = new List<string>() { "Equal", "NotEqual" };
        }
    }
}

