using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SimpleBT.Editor
{
    // This script is the base class required to create a scriptable object asset that can
    // hold new nodes made by the user. This asset must be added on the "Custom Entries SO" Object Field to take effect
    
    [System.Serializable]
    public abstract class SBTCustomEntryScriptable : ScriptableObject
    {
        protected Texture2D _icon;
        
        private void Awake()
        {
            _icon = new Texture2D(1, 1);
            _icon.SetPixel(0, 0, Color.clear); // Currently bugged
        }

        /// <summary>
        /// Returns a list of custom entries for custom nodes.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract List<SearchTreeEntry> GetEntries(SearchWindowContext context);
    }
}
