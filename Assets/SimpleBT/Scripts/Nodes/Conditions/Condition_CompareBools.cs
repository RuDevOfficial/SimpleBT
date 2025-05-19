namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_CompareBools : Condition_CompareParameters
    {
        protected override bool Check()
        {
            bool value = _blackboard.GetValue<bool>(_keyValue);
            bool secondValue = _blackboard.GetValue<bool>(_keySecondValue);
            Comparison comparison = SBTNonEditorUtils.GetEnumByString<Comparison>(_keyComparison);

            switch (comparison)
            {
                case Comparison.Equal: return value == secondValue;
                case Comparison.NotEqual: return value != secondValue;
                default: return false;
            }
        }
    }
}
