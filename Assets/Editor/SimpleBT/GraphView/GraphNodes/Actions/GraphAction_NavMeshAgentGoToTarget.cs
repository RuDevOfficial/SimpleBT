using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphAction_NavMeshAgentGoToTarget : GraphAction
    {
        private TextField _targetField;
        [SerializeField] private string _keyTarget;

        public GraphAction_NavMeshAgentGoToTarget()
        {
            Title = "NavMeshAgent\nGo To Target";
            ClassReference = "Action_NavMeshAgentGoToTarget";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _targetField = new TextField("Target Object: ");
            _targetField.RegisterValueChangedCallback(evt => _keyTarget = evt.newValue);
            
            extensionContainer.Add(_targetField);
        }

        public override List<string> GetValues() { return new List<string>() { _keyTarget }; }
        public override void ReloadValues(List<string> values) { _targetField.value = values[0]; }
    }
}
