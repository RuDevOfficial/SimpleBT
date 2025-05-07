using System.Linq;
using System.Collections.Generic;
using SimpleBT.Editor.GraphNodes;
using SimpleBT.NonEditor.Nodes;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphCondition_CompareBool : GraphCondition_Compare
    {
        public GraphCondition_CompareBool()
        {
            Title = "Compare Bool"; // Rename like: GraphCondition_DoSomething -> Do Something
            ClassReference = "Condition_CompareBool"; // Rename like: GraphCondition_DoSomething -> Condition_DoSomething
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _comparisonField.choices = new List<string>() { "True", "False" };
            _comparisonField.value = "True";
        }
    }

}
