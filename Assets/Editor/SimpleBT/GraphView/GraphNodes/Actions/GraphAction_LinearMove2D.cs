using System.Collections.Generic;
using System.Globalization;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphAction_LinearMove2D : GraphAction
    {
        TextField xPosField = new TextField("X Velocity: ");
        TextField yPosField = new TextField("Y Velocity: ");
        
        public string xVelocity, yVelocity;
        
        public GraphAction_LinearMove2D()
        {
            Title = "Linear Movement 2D";
            ClassReference = "Action_LinearMove2D";
        }
        
        public override void GenerateInterface()
        {
            base.GenerateInterface();
            
            xPosField.RegisterValueChangedCallback((evt) => { xVelocity = evt.newValue; });
            yPosField.RegisterValueChangedCallback((evt) => { yVelocity = evt.newValue; });
            
            //change style later
            xPosField.labelElement.style.minWidth = 10;
            yPosField.labelElement.style.minWidth = 10;
            
            extensionContainer.Add(xPosField);
            extensionContainer.Add(yPosField);
        }

        public override List<string> GetValues() { return new List<string>() {
            xVelocity.ToString(CultureInfo.InvariantCulture), 
            yVelocity.ToString(CultureInfo.InvariantCulture)
        }; }

        public override void ReloadValues(List<string> values)
        {
            xPosField.value = values[0];
            yPosField.value = values[1];
        }
    }

}
