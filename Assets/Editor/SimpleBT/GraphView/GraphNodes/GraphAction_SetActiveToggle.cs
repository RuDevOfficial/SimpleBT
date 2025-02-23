using System.Collections.Generic;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.GraphNodes
{
    public class GraphAction_SetActiveToggle : GraphAction_SetActive
    {
        public GraphAction_SetActiveToggle()
        {
            Title = "Set Active Toggle";
            ClassReference = "Action_SetActiveToggle";
        }

        public override void GenerateInterface()
        {
            base.GenerateInterface();

            // TODO this is such a mess should be changed, same for SetActive
            VisualElement element = extensionContainer.ElementAt(1).ElementAt(2);
            extensionContainer.ElementAt(1).Remove(element);
        }

        public override void ReloadValues(List<string> values) { }
    }
}
