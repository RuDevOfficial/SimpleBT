using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_Wait : GraphAction
    {
        [SerializeReference] public TextField TextField;
        [SerializeReference] public DropdownField DropDown;

        public GraphAction_Wait() : base()
        {
            Title = "Wait";
            ClassReference = "Action_Wait";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            TextField = new TextField("Seconds: ");
            TextField.value = "0";
            TextField.ElementAt(0).style.minWidth = 10;
            TextField.RegisterValueChangedCallback(evt =>
            {
                if (int.TryParse(evt.newValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int seconds)) {
                    TextField.value = Mathf.Max(0, seconds).ToString();
                }
            });
            
            string[] conditions = Enum.GetNames(typeof(ActionStatus));
            DropDown = new DropdownField(conditions.ToList(), 0);
            
            extensionContainer.Add(TextField);
            extensionContainer.Add(DropDown);
        }

        public override List<string> GetValues()
        {
            return new List<string>()
            {
                TextField.value,
                DropDown.value
            };
        }

        public override void ReloadValues(List<string> values)
        {
            TextField.value = values[0];
            DropDown.value = values[1];
        }
    }
}
