using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphCondition_CompareVector3 : GraphCondition_CompareTwoValues
    {
        public GraphCondition_CompareVector3()
        {
            Title = "Compare Vector3";
            ClassReference = "Condition_CompareVector3";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _comparisonField.choices.RemoveAt(_comparisonField.choices.Count - 1);
            _comparisonField.choices.RemoveAt(_comparisonField.choices.Count - 1);
        }
    }
}
