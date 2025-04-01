using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphCondition_IsAtMinimumDistance : GraphCondition
    {
        private TextField _targetTextField;
        private TextField _distanceTextField;

        public string KeyTarget;
        public string KeyDistance;

        public GraphCondition_IsAtMinimumDistance()
        {
            Title = "Is at Minimum Distance";
            ClassReference = "Condition_IsAtMinimumDistance";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _targetTextField = new TextField("Target: ");
            _targetTextField.RegisterValueChangedCallback(evt => KeyTarget = evt.newValue);
            
            _distanceTextField = new TextField("Distance: ");
            _distanceTextField.RegisterValueChangedCallback(evt => KeyDistance = evt.newValue);
            
            extensionContainer.Add(_targetTextField);
            extensionContainer.Add(_distanceTextField);
        }

        public override List<string> GetValues() { return new List<string>() { KeyTarget, KeyDistance }; }

        public override void ReloadValues(List<string> values) {
            _targetTextField.value = values[0];
            _distanceTextField.value = values[1];
        }
    }
}
