using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphAction_SetActive : GraphAction
    {
        public GraphAction_SetActive() { NodeName = "Action_SetActive"; }
        public DropdownField Dropdown;
        public TextField field;
        
        public override void Draw()
        {
            base.Draw();

            field = new TextField("GameObject Name: ");

            VisualElement container = new VisualElement();
            TextElement label = new TextElement() { text = "Tag: " };
            label.style.alignSelf = Align.Center;

            // TODO somehow update this when a new tag is added on the editor
            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
            Dropdown = new DropdownField(tags.ToList(), 0);
            container.style.flexDirection = FlexDirection.Row;
            container.style.alignSelf = Align.Center;
            container.Add(label);
            container.Add(Dropdown);
            
            extensionContainer.Add(field);
            extensionContainer.Add(container);
        }

        public override List<string> GetValues()
        {
            List<string> values = new List<string>
            {
                Dropdown.value,
                field.value
            };
            
            return values;
        }

        public override void ReloadValues(List<string> values)
        {
            Dropdown.value = values[0];
            field.value = values[1];
        }
    }
}
