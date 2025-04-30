using System;
using SimpleBT.Editor.Data;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class SbtSettingsWindow : EditorWindow
{
    [SerializeReference] private TextField editorDataPathTf;
    [SerializeReference] private TextField graphDataPathTf;

    public string EditorDataPath = "";
    public string GraphDataPath = "";
    
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
        
        editorDataPathTf = new TextField("Editor Data Path: ");
        editorDataPathTf.RegisterValueChangedCallback(evt => EditorDataPath = evt.newValue);
        rootVisualElement.Add(editorDataPathTf);
        
        graphDataPathTf = new TextField("Graph Data Path: ");
        graphDataPathTf.RegisterValueChangedCallback(evt => GraphDataPath = evt.newValue);
        rootVisualElement.Add(graphDataPathTf);
        
        Load();
    }

    void SaveData() { SBTDataManager.SaveSettingsToJson(this); }

    void Load()
    {
        SBTSettingsData data = SBTDataManager.LoadSettingsFromJson();
        editorDataPathTf.value = data.EditorDataPath;
        graphDataPathTf.value = data.GraphDataPath;
    }
}
