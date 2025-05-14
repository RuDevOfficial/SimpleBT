using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [SerializeField]
    public class GraphAction_FleeToPosition3D : GraphAction_Follow2D
    {
        public GraphAction_FleeToPosition3D()
        {
            Title = "Flee to Position 3D";
            ClassReference = "Action_FleeToPosition3D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            var field = (TextField)extensionContainer.ElementAt(0);
            var label = (Label)field.ElementAt(0);
            label.text = "Position: ";
            
            field = (TextField)extensionContainer.ElementAt(3);
            label = (Label)field.ElementAt(0);
            label.text = "Distance: ";
        }
    }
}
