using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CreateAssetMenu(fileName = "ReflectionController", menuName = "Lookups/ReflectionController", order = 1)]
public class ReflectionControllerLookup : ReflectionInputLookup
{
    public InputButton A;
    public InputButton B;
    public InputButton X;
    public InputButton Y;
}

[ExecuteInEditMode]
[CustomEditor(typeof(ReflectionControllerLookup))]
public class ReflectionControllerLookupInspector : ReflectionInputLookupInspector<ReflectionControllerLookup>
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

