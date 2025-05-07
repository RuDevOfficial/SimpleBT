using System;
using System.Collections.Generic;
using SimpleBT.NonEditor.Nodes;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    // If the Condition does NOT require any keys to then instantiate you can remove:
    // The "INodeKeyAssignable" interface and AssignKeys method
    public class Condition_CompareString : Condition_CompareParameters
    {
        public override bool Check()
        {
            string value = blackboard.GetValue<string>(_keyValue);
            string secondValue = blackboard.GetValue<string>(_keySecondValue);
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




