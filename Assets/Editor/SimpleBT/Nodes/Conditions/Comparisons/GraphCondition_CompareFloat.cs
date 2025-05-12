namespace SimpleBT.Editor.GraphNodes
{
    [System.Serializable]
    public class GraphCondition_CompareFloat : GraphCondition_CompareTwoValues
    {
        public GraphCondition_CompareFloat()
        {
            Title = "Compare Float"; // Rename like: GraphCondition_DoSomething -> Do Something
            ClassReference = "Condition_CompareFloat"; // Rename like: GraphCondition_DoSomething -> Condition_DoSomething
        }
    }
}

