using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_SetActive : GraphAction
    {
        private TextField _gameObjectNameTF;
        private DropdownField _dropdown;
        private Toggle _setActiveToggle;
        private Label _instanceIDLabel;

        [SerializeField] private string _keyObjectNameField;
        [SerializeField] private string _keyDropDown;
        [SerializeField] private string _keySetActiveToggle;
        [SerializeField] private string _keyInstanceID;
        
        
        public GraphAction_SetActive()
        {
            Title = "Set Active";
            ClassReference = "Action_SetActive";
            
            RegisterCallback<DragExitedEvent>(OnDragExited);
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            _gameObjectNameTF = new TextField("GameObject Name: ");
            _dropdown = new DropdownField("Tag:", UnityEditorInternal.InternalEditorUtility.tags.ToList(), 0);
            _setActiveToggle = new Toggle("Set Active:");
            _instanceIDLabel = new Label("Instance ID: ");
            
            _gameObjectNameTF.RegisterValueChangedCallback(evt => _keyObjectNameField = evt.newValue);
            _dropdown.RegisterValueChangedCallback(evt => _keyDropDown = evt.newValue);
            _setActiveToggle.RegisterValueChangedCallback(evt => _keySetActiveToggle = evt.newValue.ToString());
            _instanceIDLabel.RegisterValueChangedCallback(evt => _keyInstanceID = evt.newValue);

            _instanceIDLabel.visible = false;
            
            extensionContainer.Add(_gameObjectNameTF);
            extensionContainer.Add(_dropdown);
            extensionContainer.Add(_setActiveToggle);
            extensionContainer.Add(_instanceIDLabel);
        }

        public override List<string> GetValues()
        {
            List<string> values = new List<string>
            {
                _keyObjectNameField,
                _keyDropDown,
                _keySetActiveToggle,
                _keyInstanceID
            };
            
            return values;
        }

        public override void ReloadValues(List<string> values)
        {
            _gameObjectNameTF.value = values[0];
            _dropdown.value = values[1];
            _setActiveToggle.value = bool.Parse(values[2]);
            _instanceIDLabel.text = values[3];
        }
        
        private void OnDragExited(DragExitedEvent evt)
        {
            foreach (Object obj in DragAndDrop.objectReferences) {
                if (obj is not GameObject gameObject) continue;
                
                _gameObjectNameTF.value = gameObject.name;
                _dropdown.value = gameObject.tag;
                _instanceIDLabel.text = gameObject.GetInstanceID().ToString();
                break;
            }
        }
    }
}
