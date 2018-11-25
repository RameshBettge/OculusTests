using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public abstract class ReflectionInputLookup : ScriptableObject
{
    public List<KeyCode> usedKeys = new List<KeyCode>();

    [HideInInspector]
    public bool DisplayDefault = true;

    public void AddToList(KeyCode kC)
    {
        usedKeys.Add(kC);
    }

    public void RemoveFromList(KeyCode kC)
    {
        if (usedKeys.Contains(kC))
        {
            usedKeys.Remove(kC);
        }
    }

}


[ExecuteInEditMode]
[CustomEditor(typeof(ReflectionInputLookup), true)]
public class ReflectionInputLookupInspector : Editor
{
    public ReflectionSource source = new ReflectionSource();
    public FieldInfo[] fields;

    ReflectionInputLookup script;

    public override void OnInspectorGUI()
    {
        script = (ReflectionInputLookup)target;

        script.DisplayDefault = EditorGUILayout.Toggle("Display Default Inspector", script.DisplayDefault);

        if (script.DisplayDefault)
        {
            DrawDefaultInspector();
        }
        else
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromScriptableObject((ReflectionInputLookup) target), typeof(ReflectionInputLookup), false);
            GUI.enabled = true;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }


        SetFieldsAndProperties();
    }

    public void SetFieldsAndProperties()
    {

        const BindingFlags flags = BindingFlags.Public |
              BindingFlags.Instance | BindingFlags.Static;

        FieldInfo[] fields = script.GetType().GetFields(flags);
        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType == typeof(List<KeyCode>)) { return; } // Don't display the list

            EditorGUILayout.LabelField("Field: " + fieldInfo.FieldType + " " + fieldInfo.Name);
            //if(fieldInfo.FieldType == System.Type.GetType("Int32"))
            if (fieldInfo.FieldType == typeof(System.Int32))
            {
                EditorGUILayout.LabelField(fieldInfo.Name + " Value:");
                int inspectorFloat = EditorGUILayout.IntField((int)fieldInfo.GetValue(script));
                fieldInfo.SetValue(script, inspectorFloat);
                EditorGUILayout.Space();

            }
            else if (fieldInfo.FieldType == typeof(InputButton))
            {
                //PropertyInfo[] subFields = fieldInfo.GetType().GetProperties(flags);
                FieldInfo[] subFields = typeof(InputButton).GetFields(flags);

                InputButton currentKey = (InputButton)fieldInfo.GetValue(script);

                if (currentKey == null) { Debug.LogError("First field of Reflection key is not a keycode!"); continue; }

                KeyCode value = (KeyCode)subFields[0].GetValue(currentKey);

                KeyCode newValue = (KeyCode)EditorGUILayout.EnumPopup(value);


                if (!script.usedKeys.Contains(value)) { script.AddToList(value); }

                if (value != newValue || newValue == KeyCode.None)
                {
                    if (script.usedKeys.Contains(newValue) && newValue != KeyCode.None)
                    {
                        Debug.LogError("Cannot assign " + newValue + " - it is already used!");
                    }
                    else
                    {
                        script.RemoveFromList(value);
                        script.AddToList(newValue);

                        subFields[0].SetValue(currentKey, newValue);
                    }
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
        }
    }
}

public enum myEnum
{
    One, Two, Four
}
