using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_CompareParameters : Condition, INodeKeyAssignable
    {
        [SerializeField] protected string _keyValue;
        [SerializeField] protected string _keyComparison;
        [SerializeField] protected string _keySecondValue;
        
        public void AssignKeys(List<string> keys)
        {
            _keyValue = keys[0];
            _keyComparison = keys[1];
            _keySecondValue = keys[2];
        }

        protected override void Initialize() { }

        public override bool Check()
        {
            object value = blackboard.GetValue<object>(_keyValue);
            object secondValue = blackboard.GetValue<object>(_keySecondValue);
            Comparison comparison = SBTNonEditorUtils.GetEnumByString<Comparison>(_keyComparison);
            
            switch (comparison)
            {
                case Comparison.Equal: return value.Equals(secondValue);
                case Comparison.NotEqual: return !value.Equals(secondValue);
                case Comparison.Null: return value == null;
                case Comparison.NotNull: return value != null;
                default: return false;
            }
        }
    }
    
    public enum Comparison { Equal, NotEqual, Less, LessOrEqual, Greater, GreaterOrEqual, Null, NotNull }
}
