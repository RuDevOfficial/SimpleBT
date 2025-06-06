﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SimpleBT.Editor.Blackboard
{
    using NonEditor;
    using Utils;
    using UnityEditor.Experimental.GraphView;

    public class SBTBlackboardGraph : Blackboard
    {
        private Texture2D _icon;
        private BlackboardField _selectedField;
        private BlackboardSection _section;

        public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();

        private const string STARTING_PROPERTY_NAME = "ID";

        public SBTBlackboardGraph(GraphView associatedGraphView) : base(associatedGraphView)
        {
            this.addItemRequested = AddItem;
            this.editTextRequested = EditItem;

            _icon = new Texture2D(1, 1);
            _icon.SetPixel(0, 0, Color.clear); // Currently bugged

            // Removes the selected field (if it exists)
            RegisterCallback<KeyDownEvent>(evt =>
            {
                if (evt.keyCode != KeyCode.Delete || _selectedField == null) return;
                
                ExposedProperty property = ExposedProperties.Find(x => x.PropertyName == _selectedField.text);
                ExposedProperties.Remove(property);

                BlackboardRow row = _selectedField.GetFirstAncestorOfType<BlackboardRow>();
                VisualElement newElement = row.parent;
                newElement.Clear();
                newElement.RemoveFromHierarchy();

                _selectedField = null;
            });
        }

        // Method based of the code by Mert Kirimgeri on YT https://www.youtube.com/watch?v=F4cTWOxMjMY&t=1091s&ab_channel=MertKirimgeri
        private void EditItem(Blackboard blackboard, VisualElement element, string newValue)
        {
            newValue = newValue.FilterValue();

            if (string.IsNullOrEmpty(newValue)) {
                EditorUtility.DisplayDialog("Error", "Value cannot be empty!", "OK");
                return;
            }

            BlackboardField field = (BlackboardField)element;

            if (ExposedProperties.Any(x => string.Equals(x.PropertyName, newValue, StringComparison.CurrentCultureIgnoreCase))) {
                EditorUtility.DisplayDialog("Error", "This property is already named, choose another one!", "OK");
                return;
            }
            else if (field.text == STARTING_PROPERTY_NAME) {
                EditorUtility.DisplayDialog("Error", "You can't rename this variable!", "OK");
                return;
            }
            else if (newValue == STARTING_PROPERTY_NAME) {
                EditorUtility.DisplayDialog("Error", "That name is prohibited!", "OK");
                return;
            }

            var oldPropertyName = field.text;
            int propertyIndex = ExposedProperties.FindIndex(x => x.PropertyName == oldPropertyName);
            ExposedProperties[propertyIndex].PropertyName = newValue;
            field.text = newValue;
        }

        private void AddItem(Blackboard obj) { AddNewField(new ExposedProperty()); }

        // Method based on Mert Kirimgeri on YT https://www.youtube.com/watch?v=F4cTWOxMjMY&t=1091s&ab_channel=MertKirimgeri
        /// <summary>
        /// Adds a new field to the SBTBlackboardGraph class
        /// </summary>
        /// <param name="exposedProperty"></param>
        public void AddNewField(ExposedProperty exposedProperty)
        {
            if (exposedProperty.PropertyName == STARTING_PROPERTY_NAME
                && ExposedProperties.Count > 0) { return; }
            
            GenerateNewProperty(exposedProperty, out var localPropertyValue, out var property);

            var container = new VisualElement();
            var blackboardField = new BlackboardField() { text = property.PropertyName, typeText = "string" };
            blackboardField.RegisterCallback<PointerDownEvent>(evt => { _selectedField = blackboardField; });
            container.Add(blackboardField);

            // Create container that will be used for the blackboard row
            VisualElement rowContainer = new VisualElement();

            var propertyValueTextField = new TextField("Value: ") { value = localPropertyValue };
            propertyValueTextField.RegisterValueChangedCallback(evt =>
            {
                var changingPropertyIndex = ExposedProperties.FindIndex(x => x.PropertyName == property.PropertyName);
                ExposedProperties[changingPropertyIndex].PropertyRawValue = evt.newValue;
            });

            DropdownField dropdownField = new DropdownField(SBTEditorUtils.ReturnEnumToList<VariableType>(), (int)property.PropertyType, value =>
            {
                blackboardField.typeText = value;
                Enum.TryParse(value, out VariableType variableType);
                property.PropertyType = variableType;

                return value;
            });

            // Create container used to hold a text element and the dropdown field
            VisualElement typeContainer = new VisualElement();
            typeContainer.style.flexDirection = FlexDirection.Row;
            typeContainer.Add(new TextElement { text = "  Type: " });
            typeContainer.Add(dropdownField);

            rowContainer.Add(propertyValueTextField);
            rowContainer.Add(typeContainer);

            // Create the blackboard row and assign the new container
            var blackboardRow = new BlackboardRow(blackboardField, rowContainer);
            container.Add(blackboardRow);

            Add(container);
        }

        /// <summary>
        /// Generates a new property and makes sure the same name for 2 different variables cannot exist
        /// </summary>
        /// <param name="exposedProperty">The incoming property</param>
        /// <param name="localPropertyValue">Outgoing property value</param>
        /// <param name="property">Outgoing exposed property</param>
        private void GenerateNewProperty(ExposedProperty exposedProperty,
            out string localPropertyValue, out ExposedProperty property)
        {
            if (exposedProperty.PropertyName == STARTING_PROPERTY_NAME)
            {
                localPropertyValue = exposedProperty.PropertyRawValue;
                property = exposedProperty; 
                ExposedProperties.Add(property);
                return;
            }
            
            string localPropertyName = exposedProperty.PropertyName;
            localPropertyValue = exposedProperty.PropertyRawValue;

            while (ExposedProperties.Any(x => x.PropertyName == localPropertyName)) {
                localPropertyName = $"{localPropertyName}(1)";
            }

            property = new ExposedProperty();
            property.PropertyName = localPropertyName;
            property.PropertyRawValue = localPropertyValue;
            property.PropertyType = exposedProperty.PropertyType;
            ExposedProperties.Add(property);
        }

        public void Reset()
        {
            contentContainer.Clear();
            ExposedProperties.Clear();
        }
    }
}
