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
    private static readonly Dictionary<VariableType, Type> variableTypes = new Dictionary<VariableType, Type>()
    {
        { VariableType.String, typeof(string) },
        { VariableType.Bool, typeof(bool) },
        { VariableType.Int, typeof(int) },
        { VariableType.Float, typeof(float) },
        { VariableType.GameObject, typeof(GameObject) },
        { VariableType.Vector2, typeof(Vector2) },
        { VariableType.Vector3, typeof(Vector3) }
    };
    
    /// <summary>
    /// Converts a string value into another value. Check GitHub to see the compatible list.
    /// </summary>
    /// <param name="valueToConvert"></param>
    /// <param name="type">The type the value will be converted to</param>
    /// <returns></returns>
    public static object ConvertValue(this string valueToConvert, Type type)
    {
        if (type == typeof(int)) { return int.Parse(valueToConvert, NumberStyles.Integer, CultureInfo.InvariantCulture); }
        if (type == typeof(float)) { return float.Parse(valueToConvert, NumberStyles.Float, CultureInfo.InvariantCulture); }
        if (type == typeof(bool)) { return bool.Parse(valueToConvert); }
        if (type == typeof(string)) { return valueToConvert; }

        if (type == typeof(Vector2))
        {
            string[] substring = valueToConvert.Split(',');

            if (substring.Length == 2) {
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

            if (substring.Length != 3) {
            }
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

        return null;
    }
    
    /// <summary>
    /// Same function as ConvertValue, but for complex classes such as GameObject
    /// </summary>
    /// <param name="valueToConvert">The string value to convert</param>
    /// <param name="type">The type the value will be converted to</param>
    /// <returns></returns>
    public static object ConvertComplexValue(this string valueToConvert, Type type)
    {
        string[] substring = valueToConvert.Split(',');
        
        if (type == typeof(GameObject))
        {
            string name = "", tag = "Untagged", inID = "";
            try { name = substring[0].FilterValue(false); } catch { } // Ignored
            try { tag = substring[1].FilterValue(false); } catch { } // Ignored
            try { inID = substring[2].FilterValue(false); } catch { } // Ignored
            
            int.TryParse(inID, out int instanceID);

            object value = null;
            
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag)) {
                if (instanceID != 0) { if(obj.GetInstanceID() == instanceID) { value = obj; break; } }
                else if (obj.name == name) { value = obj; break; }
            }

            return value;
        }
        
        return null;
    }
    
    /// <summary>
    /// Filters a string and removes certain characters from it.
    /// </summary>
    /// <param name="value">The string value itself before filtering</param>
    /// <param name="filterNumbers">If set to true it will filter all numbers out</param>
    /// <returns></returns>
    public static string FilterValue(this string value, bool filterNumbers = true)
    {
        string filteredValue;

        if (filterNumbers) {
            filteredValue = new string(value.Where(
                c => char.IsLetter(c) || // Only letters but...
                     c == '_').ToArray()); // '_' allowed
        }
        else
        {
            filteredValue = new string(value.Where(
                    c => 
                        char.IsLetter(c) || // Only letters but...
                        c == '_' || // '_' allowed
                        char.IsNumber(c) ||
                        c == '.')// floats need this char
                .ToArray()); 
        }
            
        return filteredValue;
    }
    
    /// <summary>
    /// Returns a type based of the variableTypes dictionary
    /// </summary>
    /// <param name="variableType"></param>
    /// <returns></returns>
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
            Type toNodeType = Type.GetType($"SimpleBT.NonEditor.Nodes.{name}") ?? Type.GetType(name);

            Node generatedNode;
            List<string> filteredValues = null;
            
            if (toNodeType == typeof(Action_Any))
            {
                filteredValues = values[1].Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                for (int i = filteredValues.Count - 1; i >= 0; i--) {
                    string subString = filteredValues[i];
                    subString = FilterValue(subString, filterNumbers: false);
                    if (string.IsNullOrEmpty(subString) || string.IsNullOrWhiteSpace(subString)) { filteredValues.RemoveAt(i); }
                    else { filteredValues[i] = subString; }
                }

                filteredValues.Reverse();

                bool.TryParse(values[2], out bool isBuiltIn);
                
                Type customType = Type.GetType(
                    isBuiltIn == false ? 
                        $"Action_{values[0]}" : 
                        $"SimpleBT.NonEditor.Nodes.Action_{values[0]}");

                generatedNode = (Node)ScriptableObject.CreateInstance(customType);
            }
            else { generatedNode = (Node)ScriptableObject.CreateInstance(toNodeType); }
            
            generatedNode.GUID = guid;
            generatedNode.name = name;

            if (generatedNode is INodeKeyAssignable nodeInterface) {
                nodeInterface.AssignKeys(toNodeType == typeof(Action_Any) ? filteredValues : values);
            }
            
            tree.CompleteNodeList.Add(generatedNode);
        }
        catch
        {
            Debug.LogError($"Error generating tree because of node {name}\n" +
                      "Are you sure its inside the namespace SimpleBT.NonEditor.Nodes?\n" +
                      "If the node is custom, is it in a namespace?\n" +
                      "Are you sure the GraphNode title matches the other class?\n" +
                      "Are you sure you put INodeKeyAssignable and also put the values on the GraphNode?");
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
        object value;
        
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

            value = false;
            return (T)value;
        }
            
        else if (typeof(T) == typeof(Vector2))
        {
            value = keyToGet.ConvertValue(typeof(Vector2));
            return (T)value;
        }
        
        else if (typeof(T) == typeof(Vector3))
        {
            value = keyToGet.ConvertValue(typeof(Vector3));
            return (T)value;
        }

        else if (typeof(T).IsEnum)
        {
            value = Enum.Parse(typeof(T), keyToGet.ToUpper(), true);
            return (T)value;
        }
        
        value = keyToGet;

        return (T)value;
    }

    public static T GetComplexLiteral<T>(string parameters)
    {
        string[] substring = parameters.Split(',');
        object value = null;

        if (typeof(T) == typeof(GameObject))
        {
            string name = "", tag = "Untagged", inID = "";
            try { name = substring[0].FilterValue(false); } catch { }
            try { tag = substring[1].FilterValue(false); } catch { }
            try { inID = substring[2].FilterValue(false); } catch { }
            
            int.TryParse(inID, out int instanceID);

            if (tag == "Untagged") { GameObject.Find(name); }
            else
            {
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag)) {
                    if (instanceID != 0) { if(obj.GetInstanceID() == instanceID) { value = obj; break; } }
                    else if (obj.name == name) { value = obj; break; }
                }
            }
        }
            
        return (T)value;
    }

    public static void MovePosition(this Rigidbody2D rb2D, Vector2 position, RigidbodyMoveFlag ignoreFlag)
    {
        switch (ignoreFlag)
        {
            case RigidbodyMoveFlag.X: rb2D.MovePosition(new Vector2(rb2D.gameObject.transform.position.x, position.y)); break;
            case RigidbodyMoveFlag.Y: rb2D.MovePosition(new Vector2(position.x, rb2D.gameObject.transform.position.y)); break;
            default: rb2D.MovePosition(position); break;
        }
    }

    public static void MovePosition(this Rigidbody rb, Vector3 position, RigidbodyMoveFlag ignoreFlag)
    {
        switch (ignoreFlag)
        {
            case RigidbodyMoveFlag.X: rb.MovePosition(new Vector3(rb.gameObject.transform.position.x, position.y, position.z)); break;
            case RigidbodyMoveFlag.Y: rb.MovePosition(new Vector3(position.x, rb.gameObject.transform.position.y, position.z)); break;
            case RigidbodyMoveFlag.Z: rb.MovePosition(new Vector3(position.x, position.y, rb.gameObject.transform.position.z)); break;
            case RigidbodyMoveFlag.XY: rb.MovePosition(new Vector3(rb.gameObject.transform.position.x, rb.gameObject.transform.position.y, position.z)); break;
            case RigidbodyMoveFlag.XZ: rb.MovePosition(new Vector3(rb.gameObject.transform.position.x, position.y, rb.gameObject.transform.position.z)); break;
            case RigidbodyMoveFlag.YZ: rb.MovePosition(new Vector3(position.x, rb.gameObject.transform.position.y, rb.gameObject.transform.position.z)); break;
            case RigidbodyMoveFlag.NONE: default: rb.MovePosition(position); break;
        }
    }
    
    public static T GetEnumByString<T>(string value) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), value);
    }

    private static Keyframe GetKeyFrame(string keyFrameValues, char splitChar)
    {
        string[] values = keyFrameValues.Split(splitChar);
        
        var newKeyFrame = new Keyframe {
            time = MathF.Round(float.Parse(values[0], NumberStyles.Float, NumberFormatInfo.InvariantInfo), 2),
            value = MathF.Round(float.Parse(values[1], NumberStyles.Float, NumberFormatInfo.InvariantInfo), 2),
            inTangent = MathF.Round(float.Parse(values[2], NumberStyles.Float, NumberFormatInfo.InvariantInfo), 2),
            outTangent = MathF.Round(float.Parse(values[3], NumberStyles.Float, NumberFormatInfo.InvariantInfo), 2),
            inWeight = MathF.Round(float.Parse(values[4], NumberStyles.Float, NumberFormatInfo.InvariantInfo), 2),
            outWeight = MathF.Round(float.Parse(values[5], NumberStyles.Float, NumberFormatInfo.InvariantInfo), 2),
            weightedMode = GetEnumByString<WeightedMode>(values[6])
        };
        

        return newKeyFrame;
    }

    public static Keyframe[] GetKeyFrames(string longValueString, char firstSplitChar, char secondSplitChar)
    {
        var keyFrames = new List<Keyframe>();
        var keyFrameValues = longValueString.Split(firstSplitChar).ToList();
        for (int i = 0; i < keyFrameValues.Count; i++)
        {
            var keyFrame = GetKeyFrame(keyFrameValues[i], secondSplitChar);
            keyFrames.Add(keyFrame);
        }

        return keyFrames.ToArray();
    }
}
