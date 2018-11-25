using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEditor;


public class ReflectionUser : MonoBehaviour
{

    public ReflectionSource source = new ReflectionSource();
    public FieldInfo[] fields;
    public PropertyInfo[] properties;

    //private void Awake()
    //{
    //    SetFieldsAndProperties();
    //}

    //public void SetFieldsAndProperties()
    //{
    //    infos = new List<string>();

    //    const BindingFlags flags = BindingFlags.Public |
    // BindingFlags.Instance | BindingFlags.Static;

    //    FieldInfo[] fields = source.GetType().GetFields(flags);
    //    foreach (FieldInfo fieldInfo in fields)
    //    {
    //        infos.Add("Field: " + fieldInfo.FieldType + " " + fieldInfo.Name);
    //    }
    //    PropertyInfo[] properties = source.GetType().GetProperties(flags);
    //    foreach (PropertyInfo propertyInfo in properties)
    //    {
    //        infos.Add("Property: " + propertyInfo.PropertyType + " " + propertyInfo.Name);
    //    }
    //}

}


[ExecuteInEditMode]
[CustomEditor(typeof(ReflectionUser))]
public class ReflectionUserInspector : Editor
{
    public ReflectionSource source = new ReflectionSource();
    public FieldInfo[] fields;

    ReflectionUser script;

    public override void OnInspectorGUI()
    {
        script = (ReflectionUser)target;

        SetFieldsAndProperties();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        int inspectorFloat = EditorGUILayout.IntField(0);
        if (inspectorFloat != 0)
        {
            Debug.Log("InspectorFloat set to: " + inspectorFloat);
        }

    }

    public void SetFieldsAndProperties()
    {
        DrawDefaultInspector();

        if (script.source == null) { return; }

        const BindingFlags flags = BindingFlags.Public |
              BindingFlags.Instance | BindingFlags.Static;

        FieldInfo[] fields = source.GetType().GetFields(flags);
        foreach (FieldInfo fieldInfo in fields)
        {
            EditorGUILayout.LabelField("Field: " + fieldInfo.FieldType + " " + fieldInfo.Name);
            //if(fieldInfo.FieldType == System.Type.GetType("Int32"))
            if (fieldInfo.FieldType == typeof(System.Int32))
            {
                EditorGUILayout.LabelField(fieldInfo.Name + " Value:");
                int inspectorFloat = EditorGUILayout.IntField((int)fieldInfo.GetValue(script.source));
                if (inspectorFloat != 0)
                {
                    fieldInfo.SetValue(script.source, inspectorFloat);
                }
                EditorGUILayout.Space();

            }
            else if (fieldInfo.FieldType == typeof(Reference))
            {
                EditorGUILayout.LabelField("IS REFERENCE!");

            }
        }
    }
}
