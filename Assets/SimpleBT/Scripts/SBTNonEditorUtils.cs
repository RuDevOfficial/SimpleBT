using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SimpleBT.Core;
using SimpleBT.NonEditor;
using SimpleBT.NonEditor.Nodes;
using UnityEngine;

public static class SBTNonEditorUtils
{
    private static Dictionary<VariableType, Type> variableTypes = new Dictionary<VariableType, Type>()
    {
        { VariableType.String, typeof(string) },
        { VariableType.Bool, typeof(bool) },
        { VariableType.Int, typeof(int) },
        { VariableType.Float, typeof(float) },
        { VariableType.GameObject, typeof(GameObject) },
        { VariableType.Vector2, typeof(Vector2) },
        { VariableType.Vector3, typeof(Vector3) }
    };

    
    public static object ConvertValue(this string valueToConvert, Type type, string variableName)
    {
        string errorPopupDialogue = "";
            
        if (type == typeof(int)) { return int.Parse(valueToConvert, NumberStyles.Integer, CultureInfo.InvariantCulture); }
        if (type == typeof(float)) { return float.Parse(valueToConvert, NumberStyles.Float, CultureInfo.InvariantCulture); }
        if (type == typeof(bool)) { return bool.Parse(valueToConvert); }
        if (type == typeof(string)) { return valueToConvert; }
        if (type == typeof(GameObject)) { return GameObject.Find(valueToConvert); }

        if (type == typeof(Vector2))
        {
            string[] substring = valueToConvert.Split(',');

            if (substring.Length != 2) { errorPopupDialogue += $"You are trying to convert ({variableName}) with ({valueToConvert}) to type ({type}). \nDid you forget to add or remove a comma?\n"; }
            else {
                Vector2 vector = new Vector2();

                for (int i = 0; i < 2; i++)
                {
                    substring[i] = FilterValue(substring[i], filterNumbers: false);
                    string number = substring[i];

                    if (number.Contains('.') == false) { vector[i] = int.Parse(number); }
                    else { vector[i] = float.Parse(number, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat); }
                }
                
                return vector;
            }
        }

        if (type == typeof(Vector3))
        {
            string[] substring = valueToConvert.Split(',');

            if (substring.Length != 3) { errorPopupDialogue += $"You are trying to convert ({variableName}) with ({valueToConvert}) to type ({type}). \nDid you forget to add or remove a comma?\n"; }
            else {
                Vector3 vector = new Vector3();
                
                for (int i = 0; i < 3; i++)
                {
                    substring[i] = FilterValue(substring[i], filterNumbers: false);
                    string number = substring[i];

                    if (number.Contains('.') == false) { vector[i] = int.Parse(number); }
                    else { vector[i] = float.Parse(number, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat); }
                }
                
                return vector;  
            }
        }

        if (string.IsNullOrEmpty(errorPopupDialogue))
        {
            errorPopupDialogue = $"Couldn't convert variable ({variableName}) of type ({type}). Not supported.\n"; 
            Debug.LogError(errorPopupDialogue);
        }
        else { Debug.LogError("Conversion Error: " + errorPopupDialogue); }
        return null;
    }
    
    public static string FilterValue(this string value, bool filterNumbers = true)
    {
        string filteredValue;

        if (filterNumbers) {
            filteredValue = new string(value.Where(
                c => Char.IsLetter(c) || // Only letters but...
                     c == '_').ToArray()); // '_' allowed
        }
        else
        {
            filteredValue = new string(value.Where(
                    c => 
                        Char.IsLetter(c) || // Only letters but...
                        c == '_' || // '_' allowed
                        Char.IsNumber(c) ||
                        c == '.')// floats need this char
                .ToArray()); 
        }
            
        return filteredValue;
    }
    
    public static Type ConvertToType(this VariableType variableType)
    {
        return variableTypes[variableType];
    }

    public static void PopulateTreeList(
        this BehaviorTree tree,
        string name,
        string guid,
        List<string> values)
    {
        try
        {
            Type toNodeType = Type.GetType($"SimpleBT.NonEditor.Nodes.{name}");
            Node generatedNode = (Node)ScriptableObject.CreateInstance(toNodeType);
            generatedNode.GUID = guid;
            generatedNode.name = (tree.CompleteNodeList.Count + 1).ToString();

            if (generatedNode is INodeKeyAssignable nodeInterface) {
                nodeInterface.AssignKeys(values);
            }
            
            tree.CompleteNodeList.Add(generatedNode);
        }
        catch
        {
            Debug.LogError($"Error generating tree because of node {name}\n" +
                      "Are you sure its inside the namespace SimpleBT.NonEditor.Nodes?\n" +
                      "Are you sure the GraphNode title matches the other class?");
        }
    }

    /// <summary>
    /// Converts a string to a literal value depending on the type declared.
    /// Returns null if the type is not supported.
    /// </summary>
    /// <param name="keyToGet">The string value to transform</param>
    /// <typeparam name="T">Value type</typeparam>
    /// <returns></returns>
    public static T GetLiteral<T>(string keyToGet)
    {
        object value = null;
        
        if (typeof(T) == typeof(float))
        {
            if (float.TryParse(keyToGet,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out var newFloat)) {
                value = newFloat;
                return (T)value;
            }
        }
    
        else if (typeof(T) == typeof(int))
        {
            if (int.TryParse(keyToGet, out var newInt)) {
                value = newInt;
                return (T)value;
            }
        }
    
        else if (typeof(T) == typeof(bool))
        {
            if (bool.TryParse(keyToGet, out var newBool)) {
                value = newBool;
                return (T)value;
            }
        }
            
        else if (typeof(T) == typeof(Vector2))
        {
            value = keyToGet.ConvertValue(typeof(Vector2), keyToGet.ToUpper());
            return (T)value;
        }

        // ENUMS
            
        else if (typeof(T) == typeof(Status))
        {
            value = Enum.Parse<Status>(keyToGet);
            return (T)value;
        }
            
        else if (typeof(T) == typeof(ConditionType))
        {
            value = Enum.Parse<ConditionType>(keyToGet);
            return (T)value;
        }

        return (T)value;
    }
}
