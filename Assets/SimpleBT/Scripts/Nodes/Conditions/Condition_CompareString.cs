namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_CompareString : Condition_CompareParameters
    {
        protected override bool Check()
        {
            string value = _blackboard.GetValue<string>(_keyValue);
            string secondValue = _blackboard.GetValue<string>(_keySecondValue);
            Comparison comparison = SBTNonEditorUtils.GetEnumByString<Comparison>(_keyComparison);
            
            switch (comparison)
            {
                case Comparison.Equal: return value.Equals(secondValue);
                case Comparison.NotEqual: return !value.Equals(secondValue);
                default: return false;
            }
        }
    }

}




