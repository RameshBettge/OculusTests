using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEditor;


public class ReflectionUser : MonoBehaviour
{
    ReflectionSource source = new ReflectionSource();
    public FieldInfo[] fields;
    public PropertyInfo[] properties;

    public List<string> infos = new List<string>();


    private void Awake()
    {
        SetFieldsAndProperties();
    }

    public void SetFieldsAndProperties()
    {
        infos = new List<string>();

        const BindingFlags flags = BindingFlags.Public |
     BindingFlags.Instance | BindingFlags.Static;

        FieldInfo[] fields = source.GetType().GetFields(flags);
        foreach (FieldInfo fieldInfo in fields)
        {
            infos.Add("Field: " + fieldInfo.FieldType + " " + fieldInfo.Name);
        }
        PropertyInfo[] properties = source.GetType().GetProperties(flags);
        foreach (PropertyInfo propertyInfo in properties)
        {
            infos.Add("Property: " + propertyInfo.PropertyType + " " + propertyInfo.Name);
        }
    }

}


[ExecuteInEditMode]
[CustomEditor(typeof(ReflectionUser))]
public class ReflectionUserInspector : Editor
{


    ReflectionUser script;

    public override void OnInspectorGUI()
    {
        script = (ReflectionUser)target;

        script.SetFieldsAndProperties();

        foreach (string s in script.infos)
        {
            EditorGUILayout.LabelField(s);
        }


        //if (script.fields != null)
        //{

        //    foreach (FieldInfo fieldInfo in script.fields)
        //    {
        //        EditorGUILayout.LabelField(fieldInfo.FieldType + " " + "Field: " + fieldInfo.Name);

        //    }
        //}

        //if (script.properties != null)
        //{
        //    foreach (PropertyInfo propertyInfo in script.properties)
        //    {
        //        EditorGUILayout.LabelField(propertyInfo.PropertyType + " Property: " + propertyInfo.Name);
        //    }
        //}
    }
}
