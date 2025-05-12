using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphCondition_CompareBool : GraphCondition_Compare
    {
        public GraphCondition_CompareBool()
        {
            Title = "Compare Bool"; 
            ClassReference = "Condition_CompareBool";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _comparisonField.choices = new List<string>() { "True", "False" };
            _comparisonField.value = "True";
        }
    }

}
