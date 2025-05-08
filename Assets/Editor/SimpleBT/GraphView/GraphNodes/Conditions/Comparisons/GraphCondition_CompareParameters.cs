using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphCondition_CompareParameters : GraphCondition_CompareTwoValues
    {
        public GraphCondition_CompareParameters()
        {
            Title = "Compare\nBlackboard Values";
            ClassReference = "Condition_CompareParameters";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _comparisonField.choices = new List<string>(){"Equal", "NotEqual", "Null", "NotNull"};
        }
    }
}
