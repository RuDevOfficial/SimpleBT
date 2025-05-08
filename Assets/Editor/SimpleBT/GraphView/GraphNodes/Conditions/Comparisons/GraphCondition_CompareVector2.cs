using System.Collections.Generic;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphCondition_CompareVector2 : GraphCondition_CompareTwoValues
    {
        public GraphCondition_CompareVector2()
        {
            Title = "Compare Vector2";
            ClassReference = "Condition_CompareVector2";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _comparisonField.choices.RemoveAt(_comparisonField.choices.Count - 1);
            _comparisonField.choices.RemoveAt(_comparisonField.choices.Count - 1);
        }
    }
}
