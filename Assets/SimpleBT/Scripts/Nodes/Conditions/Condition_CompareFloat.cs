using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_CompareFloat : Condition_CompareParameters
    {
        protected override bool Check()
        {
            float value = blackboard.GetValue<float>(_keyValue);
            float secondValue = blackboard.GetValue<float>(_keySecondValue);
            Comparison comparison = SBTNonEditorUtils.GetEnumByString<Comparison>(_keyComparison);

            switch (comparison)
            {
                case Comparison.Equal: return Mathf.Approximately(value, secondValue);
                case Comparison.NotEqual: return !Mathf.Approximately(value, secondValue);
                case Comparison.Less: return value < secondValue;
                case Comparison.LessOrEqual: return value <= secondValue;
                case Comparison.Greater: return value > secondValue;
                case Comparison.GreaterOrEqual: return value >= secondValue;
                default: return false;
            }
        }
    }
}
