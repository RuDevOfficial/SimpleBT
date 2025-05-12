using System;
using SimpleBT.Editor.Data;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

// Holds the user settings and stores them. Settings won't be applied until saved.
public class SbtSettingsWindow : EditorWindow
{
    [SerializeReference] private TextField editorDataPathTF;
    [SerializeReference] private TextField graphDataPathTF;
    [SerializeReference] private TextField styleDataPathTF;

    public string EditorDataPath = "";
    public string GraphDataPath = "";
    public string StyleDataPath = "";
    
    [MenuItem("SimpleBT/Settings")]
    public static void Open()
    {
        SbtSettingsWindow wnd = GetWindow<SbtSettingsWindow>();
        wnd.titleContent = new GUIContent("Settings");
    }

    private void OnEnable()
    {
        Toolbar toolbar = new Toolbar();
        Button saveButton = new Button(SaveData);
        saveButton.text = "Save";
        
        toolbar.Add(saveButton);
        rootVisualElement.Add(toolbar);
        
        editorDataPathTF = new TextField("Editor Data Path: ");
        graphDataPathTF = new TextField("Graph Data Path: ");
        styleDataPathTF = new TextField("Style Data Path: ");
        
        editorDataPathTF.RegisterValueChangedCallback(evt => EditorDataPath = evt.newValue);
        graphDataPathTF.RegisterValueChangedCallback(evt => GraphDataPath = evt.newValue);
        styleDataPathTF.RegisterValueChangedCallback(evt => StyleDataPath = evt.newValue);
        
        rootVisualElement.Add(editorDataPathTF);
        rootVisualElement.Add(graphDataPathTF);
        rootVisualElement.Add(styleDataPathTF);
        
        Load();
    }

    private void SaveData() { SBTDataManager.SaveSettingsToJson(this); }

    private void Load()
    {
        SBTSettingsData data = SBTDataManager.LoadSettingsFromJson();
        
        editorDataPathTF.value = data.EditorDataPath;
        graphDataPathTF.value = data.GraphDataPath;
        styleDataPathTF.value = data.StyleDataPath;
    }
}
