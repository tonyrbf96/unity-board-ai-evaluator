using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(GameTypeDrowdownAttribute))]
public class GameTypeDrowdownAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var dropAttr = (GameTypeDrowdownAttribute)attribute;

        if (dropAttr.collection == null)
        {
            GUI.Label(position, $"Null collection in {property.displayName} attribute", EditorStyles.miniBoldLabel);
            return;
        }

        if (dropAttr.collection.Length == 0)
        {
            GUI.Label(position, $"Empty collection in {property.displayName} attribute", EditorStyles.miniBoldLabel);
            return;
        }

        var options = dropAttr.collection;
        string[] strOptions = new string[options.Length];
        for (int i = 0; i < options.Length; i++)
            strOptions[i] = GetGameName(options[i]);

        var index = -1;
        for (int i = 0; i < strOptions.Length; i++)
        {
           if (property.stringValue == strOptions[i])
                        index = i;
        }

        if (index == -1)
            index = 0;

        dropAttr.selectedIndex = index;
        dropAttr.selectedIndex = EditorGUI.Popup(position, property.displayName, dropAttr.selectedIndex, strOptions);
        

        // Get the string value of the Game Id
        property.stringValue = GetGameName(options[dropAttr.selectedIndex]);
    }

    string GetGameName(Type type)
    {
        return  type.Name;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}
