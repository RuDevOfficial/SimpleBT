using System;
using System.Collections.Generic;
using SimpleBT.Core;
using UnityEngine;

namespace SimpleBT.NonEditor.Nodes
{
    public class Condition_Comparison : Condition, INodeConditional, INodeKeyAssignable
    {
        public string KeyFirstVariable;
        public string KeySecondVariable;
        public string KeyCondition;

        private object varAObject, varBObject;
        private Type varAType, varBType;

        private ConditionType condition;

        public void AssignKeys(List<string> values)
        {
            KeyFirstVariable = values[0];
            KeyCondition = values[1];
            KeySecondVariable = values[2];
        }

        protected override void Initialize()
        {
            if (blackboard.GetRawValue(KeyFirstVariable, out varAObject) == false)
            {
                varAObject = blackboard.GetValue<float>(KeyFirstVariable);
            }

            varAType = varAObject?.GetType();

            if (blackboard.GetRawValue(KeySecondVariable, out varBObject) == false
                && condition != ConditionType.Null && condition != ConditionType.NotNull)
            {
                varBObject = blackboard.GetValue<float>(KeySecondVariable);
            }

            if (condition != ConditionType.Null && condition != ConditionType.NotNull)
            {
                varBType = varBObject?.GetType();
            }

            condition = blackboard.GetValue<ConditionType>(KeyCondition);
        }

        public override bool Check()
        {
            if (varAType == typeof(int) || varAType == typeof(float)
                && varBType == typeof(int) || varBType == typeof(float))
            {
                float valueA = Convert.ToSingle(varAObject);
                float valueB = Convert.ToSingle(varBObject);

                switch (condition)
                {
                    case ConditionType.Equal: return Mathf.Approximately(valueA, valueB);
                    case ConditionType.NotEqual: return !Mathf.Approximately(valueA, valueB);
                    case ConditionType.Less_Than: return valueA < valueB;
                    case ConditionType.Less_Or_Equal_Than: return valueA <= valueB;
                    case ConditionType.More_Than: return valueA > valueB;
                    case ConditionType.More_Or_Equal_Than: return valueA >= valueB;
                    default: return false;
                }
            }
            else
            {
                switch (condition)
                {
                    case ConditionType.Equal: return varAObject == varBObject;
                    case ConditionType.NotEqual: return varAObject != varBObject;
                    case ConditionType.Null: return varAObject == null;
                    case ConditionType.NotNull: return varAObject != null;
                    default: return false;
                }
            }
        }
    }
}