namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphAction_UnparentGameObject : GraphAction_ParentObjectToSelf
    {
        public GraphAction_UnparentGameObject()
        {
            Title = "Unparent Object";
            ClassReference = "Action_UnparentGameObject";
        }
    }
}
