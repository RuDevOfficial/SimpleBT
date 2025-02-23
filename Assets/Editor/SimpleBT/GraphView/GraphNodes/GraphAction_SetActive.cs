using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphAction_SetActive : GraphAction
    {
        public GraphAction_SetActive()
        {
            Title = "Set Active";
            ClassReference = "Action_SetActive";
            
            RegisterCallback<DragExitedEvent>(OnDragExited);
        }

        protected DropdownField Dropdown;
        protected TextField field;
        protected Toggle setActiveToggle;
        protected TextElement instanceIDLabel;

        private bool _gameObjectDropped = false;
        
        /*
         * TODO make it so if you modify manually the graph node values
         * it makes sure to remove the reference ID
        */ 
        public override void GenerateInterface()
        {
            base.GenerateInterface();

            field = new TextField("GameObject Name: ");
            field.ElementAt(0).style.minWidth = 10;

            VisualElement container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;
            container.style.alignSelf = Align.Center;
            
            // Toggle
            setActiveToggle = new Toggle("Set Active:");
            setActiveToggle.name = "SetActiveToggle";
            setActiveToggle.ElementAt(0).style.minWidth = 10;
            setActiveToggle.style.alignSelf = Align.Center;
            
            // Tag
            TextElement label = new TextElement() { text = "Tag:" };
            label.style.alignSelf = Align.Center;
            
            // Tag DropDown
            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
            Dropdown = new DropdownField(tags.ToList(), 0);
            
            container.Add(label);
            container.Add(Dropdown);
            container.Add(setActiveToggle); // At 1 3 element

            // Reference ID section
            VisualElement referenceIDContainer = new VisualElement();
            referenceIDContainer.style.flexDirection = FlexDirection.Row;
            Label IDLabel = new Label("Instance ID: ");
            instanceIDLabel = new Label("Drag and Drop to Get");
            referenceIDContainer.Add(IDLabel);
            referenceIDContainer.Add(instanceIDLabel);
            
            extensionContainer.Add(field);
            extensionContainer.Add(container); // At 1 element
            extensionContainer.Add(referenceIDContainer);
        }

        public override List<string> GetValues()
        {
            List<string> values = new List<string>
            {
                field.value,
                Dropdown.value,
                setActiveToggle.value.ToString(),
                instanceIDLabel.text
            };
            
            return values;
        }

        public override void ReloadValues(List<string> values)
        {
            field.value = values[0];
            Dropdown.value = values[1];
            setActiveToggle.value = bool.Parse(values[2]);
            instanceIDLabel.text = values[3];
        }
        
        private void OnDragExited(DragExitedEvent evt)
        {
            foreach (Object obj in DragAndDrop.objectReferences) {
                if (obj is GameObject gameObject)
                {
                    _gameObjectDropped = true;
                    field.value = gameObject.name;
                    Dropdown.value = gameObject.tag;
                    instanceIDLabel.text = gameObject.GetInstanceID().ToString();
                    _gameObjectDropped = false;
                    break;
                }
            }
        }
    }

}
