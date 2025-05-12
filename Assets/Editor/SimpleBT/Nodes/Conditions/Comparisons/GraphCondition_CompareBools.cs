using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphCondition_CompareBools : GraphCondition_CompareTwoValues
    {
        public GraphCondition_CompareBools()
        {
            Title = "Compare Bools";
            ClassReference = "Condition_CompareBools";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _comparisonField.choices = new List<string>() { "Equal", "NotEqual" };
        }
    }
}
