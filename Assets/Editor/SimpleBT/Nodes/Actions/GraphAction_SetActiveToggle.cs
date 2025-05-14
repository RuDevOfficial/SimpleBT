namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
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
            extensionContainer.RemoveAt(2);
        }
    }
}
