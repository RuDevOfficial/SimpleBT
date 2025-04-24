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
            new SearchTreeGroupEntry(new GUIContent("Example Section 1"), 2),
                new SearchTreeEntry(new GUIContent("Example A", _icon)) { level = 3, userData = "Custom_GraphCondition_Example1" },
                new SearchTreeEntry(new GUIContent("Example B", _icon)) { level = 3, userData = "Custom_GraphCondition_Example2" },
                
            new SearchTreeGroupEntry(new GUIContent("Example Section 2"), 2),
                new SearchTreeEntry(new GUIContent("Example C", _icon)) { level = 3, userData = "Custom_GraphCondition_Example3" },
                new SearchTreeEntry(new GUIContent("Example D", _icon)) { level = 3, userData = "Custom_GraphCondition_Example4" },
        };
    }
}