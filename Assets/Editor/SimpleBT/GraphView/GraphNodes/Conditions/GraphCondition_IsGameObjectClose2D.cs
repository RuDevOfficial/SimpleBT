using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphCondition_IsGameObjectClose2D : GraphCondition
    {
        private DropdownField _tagTF;
        private TextField _radiusTF;

        private TextField _parameterTF;
        private Toggle _storeToggle;

        [SerializeField] private string _keyTag = "Untagged";
        [SerializeField] private string _keyRadius;
        [SerializeField] private string _keyBlackboardToggle;
        [SerializeField] private string _keyParameter;
        
        public GraphCondition_IsGameObjectClose2D()
        {
            Title = "Is GameObject Close 2D";
            ClassReference = "Condition_IsGameObjectClose2D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _tagTF = new DropdownField("Tags: ", UnityEditorInternal.InternalEditorUtility.tags.ToList(), 0);
            _radiusTF = new TextField("Radius: ");
            _storeToggle = new Toggle("Store Value: ");
            _parameterTF = new TextField("Parameter: ");
            
            _tagTF.RegisterValueChangedCallback(evt => _keyTag = evt.newValue);
            _radiusTF.RegisterValueChangedCallback(evt => _keyRadius = evt.newValue);
            _storeToggle.RegisterValueChangedCallback(evt =>
            {
                _keyBlackboardToggle = evt.newValue.ToString();
                _parameterTF.enabledSelf = evt.newValue;
            });
            _parameterTF.RegisterValueChangedCallback(evt => _keyParameter = evt.newValue);

            if (string.IsNullOrEmpty(_keyBlackboardToggle)) { _parameterTF.enabledSelf = false; }
            if (_keyBlackboardToggle == "False") { _parameterTF.enabledSelf = false; }
            if (_keyBlackboardToggle == "True") { _parameterTF.enabledSelf = true; }
            
            extensionContainer.Add(_tagTF);
            extensionContainer.Add(_radiusTF);
            extensionContainer.Add(_storeToggle);
            extensionContainer.Add(_parameterTF);
        }

        public override List<string> GetValues() { return new List<string>()
        {
            _keyTag,
            _keyRadius,
            _keyBlackboardToggle,
            _keyParameter
        }; }

        public override void ReloadValues(List<string> values)
        {
            _tagTF.value = values[0];
            _radiusTF.value = values[1];
            _storeToggle.value = bool.Parse(values[2]);
            _parameterTF.value = values[3];
        }
    }

}
