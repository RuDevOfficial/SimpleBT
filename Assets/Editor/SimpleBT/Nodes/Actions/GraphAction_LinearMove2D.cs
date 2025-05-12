using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_LinearMove2D : GraphAction
    {
        private TextField _xPosTF;
        private TextField _yPosTF;

        [SerializeField] private string _keyXVelocity;
        [SerializeField] private string _keyYVelocity;
        
        public GraphAction_LinearMove2D()
        {
            Title = "Linear Movement 2D";
            ClassReference = "Action_LinearMove2D";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            _xPosTF = new TextField("X Position: ");
            _yPosTF = new TextField("Y Position: ");
            
            _xPosTF.RegisterValueChangedCallback((evt) => { _keyXVelocity = evt.newValue.ToString(CultureInfo.InvariantCulture); });
            _yPosTF.RegisterValueChangedCallback((evt) => { _keyYVelocity = evt.newValue.ToString(CultureInfo.InvariantCulture); });
            
            extensionContainer.Add(_xPosTF);
            extensionContainer.Add(_yPosTF);
        }

        public override List<string> GetValues() 
        { 
            return new List<string>() {
                _keyXVelocity, 
                _keyYVelocity
            };
        }

        public override void ReloadValues(List<string> values)
        {
            _xPosTF.value = values[0];
            _yPosTF.value = values[1];
        }
    }

}
