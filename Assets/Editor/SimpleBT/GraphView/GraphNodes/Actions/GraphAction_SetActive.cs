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
        public GraphAction_SetActive()
        {
            Title = "Set Active";
            ClassReference = "Action_SetActive";
            
            RegisterCallback<DragExitedEvent>(OnDragExited);
        }

        [SerializeReference] public DropdownField Dropdown;
        [SerializeReference] public TextField Field;
        [SerializeReference] public Toggle SetActiveToggle;
        [SerializeReference] public Label InstanceIDLabel;

        private bool _gameObjectDropped = false;
        
        /*
         * TODO make it so if you modify manually the graph node values
         * it makes sure to remove the reference ID
        */ 
        public override void GenerateInterface()
        {
            base.GenerateInterface();

            Field = new TextField("GameObject Name: ");
            Field.ElementAt(0).style.minWidth = 10;

            VisualElement container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;
            container.style.alignSelf = Align.Center;
            
            // Toggle
            SetActiveToggle = new Toggle("Set Active:");
            SetActiveToggle.name = "SetActiveToggle";
            SetActiveToggle.ElementAt(0).style.minWidth = 10;
            SetActiveToggle.style.alignSelf = Align.Center;
            
            // Tag
            TextElement label = new TextElement() { text = "Tag:" };
            label.style.alignSelf = Align.Center;
            
            // Tag DropDown
            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
            Dropdown = new DropdownField(tags.ToList(), 0);
            
            container.Add(label);
            container.Add(Dropdown);
            container.Add(SetActiveToggle); // At 1 3 element

            // Reference ID section
            VisualElement referenceIDContainer = new VisualElement();
            referenceIDContainer.style.flexDirection = FlexDirection.Row;
            Label IDLabel = new Label("Instance ID: ");
            InstanceIDLabel = new Label("Drag and Drop to Get");
            referenceIDContainer.Add(IDLabel);
            referenceIDContainer.Add(InstanceIDLabel);
            
            extensionContainer.Add(Field);
            extensionContainer.Add(container); // At 1 element
            extensionContainer.Add(referenceIDContainer);
        }

        public override List<string> GetValues()
        {
            List<string> values = new List<string>
            {
                Field.value,
                Dropdown.value,
                SetActiveToggle.value.ToString(),
                InstanceIDLabel.text
            };
            
            return values;
        }

        public override void ReloadValues(List<string> values)
        {
            Field.value = values[0];
            Dropdown.value = values[1];
            SetActiveToggle.value = bool.Parse(values[2]);
            InstanceIDLabel.text = values[3];
        }
        
        private void OnDragExited(DragExitedEvent evt)
        {
            foreach (Object obj in DragAndDrop.objectReferences) {
                if (obj is GameObject gameObject)
                {
                    _gameObjectDropped = true;
                    Field.value = gameObject.name;
                    Dropdown.value = gameObject.tag;
                    InstanceIDLabel.text = gameObject.GetInstanceID().ToString();
                    _gameObjectDropped = false;
                    break;
                }
            }
        }
    }
}
