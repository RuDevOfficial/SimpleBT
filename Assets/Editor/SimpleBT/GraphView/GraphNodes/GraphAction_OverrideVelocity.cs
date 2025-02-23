using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphAction_OverrideVelocity : GraphAction
    {
        public GraphAction_OverrideVelocity()
        {
            Title = "Override Velocity 2D";
            ClassReference = "Action_RigidbodyOverrideVelocity2D";
        }

        private Vector3Field _vectorField;
        private Toggle _toggle;
        private bool _normalized = false;

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            _toggle = new Toggle("Normalize: ");
            _toggle.RegisterValueChangedCallback(evt => { _normalized = evt.newValue; });
            _toggle.ElementAt(0).style.minWidth = 10;
            
            _vectorField = new Vector3Field();
            extensionContainer.Add(_vectorField);
            extensionContainer.Add(_toggle);
        }

        public override List<string> GetValues()
        {
            return new List<string>()
            {
                _vectorField.value.x.ToString(CultureInfo.InvariantCulture),
                _vectorField.value.y.ToString(CultureInfo.InvariantCulture),
                _vectorField.value.z.ToString(CultureInfo.InvariantCulture),
                _normalized.ToString()
            };
        }

        public override void ReloadValues(List<string> values)
        {
            Vector3 vector = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
            _vectorField.value = vector;
            
            _toggle.value = bool.Parse(values[3]);
        }
    }
}
