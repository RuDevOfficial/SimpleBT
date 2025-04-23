using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SimpleBT.Editor
{
    [CreateAssetMenu(fileName = "Custom Entry List", menuName = "SimpleBT/CustomEntryList")]
    public class SBTSearchTreeEntryAddon : ScriptableObject
    {
        // All entries must be at level 2 or more
        public List<SearchTreeEntry> GetEntries(SearchWindowContext context)
        {
            return new List<SearchTreeEntry>()
            {
                new SearchTreeEntry(new GUIContent("teste")) { level = 2, userData = "Custom_GraphAction_NewTest" },
            };
        }
    }
}
