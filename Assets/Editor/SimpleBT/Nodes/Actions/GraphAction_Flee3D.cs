using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_Flee3D : GraphAction_Flee2D
    {
        public GraphAction_Flee3D()
        {
            Title = "Flee 3D";
            ClassReference = "Action_Flee3D";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            var field = (TextField)extensionContainer.ElementAt(3);
            var label = (Label)field.ElementAt(0);
            label.text = "Safe Distance: ";
        }
    }

}
