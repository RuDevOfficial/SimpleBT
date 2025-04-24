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
            new SearchTreeEntry(new GUIContent("Test :D", _icon)) { level = 2, userData = "Custom_GraphAction_Pedorra" },
        };
    }
}