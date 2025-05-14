using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_NavMeshAgentGoToTarget : GraphAction
    {
        private TextField _targetTF;
        
        [SerializeField] private string _keyTarget;

        public GraphAction_NavMeshAgentGoToTarget()
        {
            Title = "NavMeshAgent\nGo To Target";
            ClassReference = "Action_NavMeshAgentGoToTarget";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            _targetTF = new TextField("Target Object: ");
            _targetTF.RegisterValueChangedCallback(evt => _keyTarget = evt.newValue);
            extensionContainer.Add(_targetTF);
        }

        public override List<string> GetValues() { return new List<string>() { _keyTarget }; }
        public override void ReloadValues(List<string> values) { _targetTF.value = values[0]; }
    }
}
