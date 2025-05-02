using System.Collections.Generic;
using SimpleBT.Editor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "Custom Entries", menuName = "SimpleBT/Custom Entries")]
[System.Serializable]
public class CustomNodeEntries : SBTCustomEntryScriptable
{
    public override List<SearchTreeEntry> GetEntries(SearchWindowContext context)
    {
        return new List<SearchTreeEntry>()
        {
            new SearchTreeEntry(new GUIContent("This")) { level = 2, userData = "Custom_GraphAction_Potato"}
            // Entries must have minimum of level 2.
            // Userdata must start with "Custom_"
            // If custom nodes are within a namespace, make sure to add the namespace as well: "Custom_SimpleBT.Editor.GraphNodes.GraphAction_Debug"
                // (although it is recommended to not be in a namespace)
            // To enable these custom entries, create an Asset of this SO and add it onto the "Custom Entries" object field (right of the "Clear Blackboard" button)
            
            // Example:
            //new SearchTreeGroupEntry(new GUIContent("Section"), 2),
            //    new SearchTreeEntry(new GUIContent("Node Example", _icon)) { level = 3, userData = "Custom_GraphAction_Debug" },
        };
    }
}