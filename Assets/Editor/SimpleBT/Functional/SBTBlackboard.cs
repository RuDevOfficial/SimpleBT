using System.Collections.Generic;
using System.Linq;
using SimpleBT.Editor.Utils;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
public class SBTBlackboard : Blackboard
{
    private Texture2D _icon;
    public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
    
    public SBTBlackboard(GraphView associatedGraphView) : base (associatedGraphView)
    {
        this.addItemRequested = AddItem;
        this.editTextRequested = EditItem;
        
        _icon = new Texture2D(1, 1);
        _icon.SetPixel(0, 0, Color.clear); // Currently bugged

        SetBlackboard();
    }

    // Method based of the code by Mert Kirimgeri on YT https://www.youtube.com/watch?v=F4cTWOxMjMY&t=1091s&ab_channel=MertKirimgeri
    private void EditItem(Blackboard blackboard, VisualElement element, string newValue)
    {
        newValue = newValue.FilterValue();
        
        BlackboardField field = (BlackboardField)element;
        if (ExposedProperties.Any(x => x.PropertyName == newValue)) { EditorUtility.DisplayDialog("Error", "This property is already named, choose another one!", "OK"); return; }
        else if (field.text == "Self") { EditorUtility.DisplayDialog("Error", "You can't rename this variable!", "OK"); return; }
        else if (newValue == "Self") { EditorUtility.DisplayDialog("Error", "That name is prohibited!", "OK"); return; }

        
        var oldPropertyName = field.text;
        int propertyIndex = ExposedProperties.FindIndex(x => x.PropertyName == oldPropertyName);
        ExposedProperties[propertyIndex].PropertyName = newValue;
        field.text = newValue;
    }

    private void AddItem(Blackboard obj)
    {
        AddPropertyToBlackboard(new ExposedProperty());
    }

    // Method by Mert Kirimgeri on YT https://www.youtube.com/watch?v=F4cTWOxMjMY&t=1091s&ab_channel=MertKirimgeri
    public void AddPropertyToBlackboard(ExposedProperty exposedProperty)
    {
        var localPropertyName = exposedProperty.PropertyName;
        var localPropertyValue = exposedProperty.PropertyValue;

        while (ExposedProperties.Any(x => x.PropertyName == localPropertyName)) {
            localPropertyName = $"{localPropertyName}(1)";
        }
        
        var property = new ExposedProperty();
        property.PropertyName = localPropertyName;
        property.PropertyValue = localPropertyValue;
        ExposedProperties.Add(property);

        var container = new VisualElement();
        var blackboardField = new BlackboardField() { text = property.PropertyName, typeText = "string" };
        container.Add(blackboardField);

        var propertyValueTextField = new TextField("Value: ") { value = localPropertyValue };
        propertyValueTextField.RegisterValueChangedCallback(evt =>
        {
            var changingPropertyIndex = ExposedProperties.FindIndex(x => x.PropertyName == property.PropertyName);
            ExposedProperties[changingPropertyIndex].PropertyValue = evt.newValue;
        });
        var blackBoardValueRow = new BlackboardRow(blackboardField, propertyValueTextField);
        container.Add(blackBoardValueRow);
        
        Add(container);
    }

    void SetBlackboard()
    {
        BlackboardSection exposedSection = new BlackboardSection() { title = "Exposed Properties" };
        Add(exposedSection);
        
        ExposedProperty exposedProperty = new ExposedProperty();
        exposedProperty.PropertyName = "Self";
        exposedProperty.PropertyValue = ""; // This should populate on generation
        
        BlackboardField field = new BlackboardField(null, exposedProperty.PropertyName, "GameObject");
        exposedSection.Add(field);
    }
    
    public void Reset()
    {
        contentContainer.Clear();
        ExposedProperties.Clear();
        SetBlackboard();
    }
}