using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_CompareVector2 : Condition_CompareParameters
    {
        protected override bool Check()
        {
            Vector2 firstValue = blackboard.GetValue<Vector2>(_keyValue);
            Vector2 secondValue = blackboard.GetValue<Vector2>(_keySecondValue);
            Comparison comparison = SBTNonEditorUtils.GetEnumByString<Comparison>(_keyComparison);

            switch (comparison)
            {
                case Comparison.Equal: return firstValue == secondValue;
                case Comparison.NotEqual: return firstValue != secondValue;
                case Comparison.Less: return firstValue.magnitude < secondValue.magnitude;
                case Comparison.LessOrEqual: return firstValue.magnitude <= secondValue.magnitude;
                case Comparison.Greater: return firstValue.magnitude > secondValue.magnitude;
                case Comparison.GreaterOrEqual: return firstValue.magnitude >= secondValue.magnitude;
                default: return false;
            }
        }
    }
}
