using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_Wait : GraphAction
    {
        [SerializeReference] public TextField TextField;
        
        public GraphAction_Wait()
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
            
            extensionContainer.Add(TextField);
        }

        public override List<string> GetValues()
        {
            return new List<string>()
            {
                TextField.value
            };
        }

        public override void ReloadValues(List<string> values)
        {
            TextField.value = values[0];
        }
    }

}
