using UnityEngine;
using UnityEngine.Serialization;

namespace SimpleBT.Editor.Data
{
    [System.Serializable]
    public class SBTEditorData
    {
        public string LastFileName;
        public SBTSearchTreeEntryAddon Addon;
    
        public SBTEditorData(){  }
    }
}

