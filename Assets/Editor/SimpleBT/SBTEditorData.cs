using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SBTEditorData
{
    public string LastFileName;
    
    public SBTEditorData(SBTEditorWindow window)
    {
        LastFileName = window.LastFieldValue;
    }
}
