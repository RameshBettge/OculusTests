
using UnityEngine;

[CreateAssetMenu(fileName = "ReflectionController", menuName = "Lookups/ReflectionController", order = 1)]
public class ReflectionControllerLookup : ReflectionInputLookup
{
    public InputButton A;
    public InputButton B;
    public InputButton X;
    public InputButton Y;

    public bool b;
}

// Custom inspector may not need to be specified because of the way the custom inspector of the parent class was set up.

//[ExecuteInEditMode]
//[CustomEditor(typeof(ReflectionControllerLookup))]
//public class ReflectionControllerLookupInspector : ReflectionInputLookupInspector<ReflectionControllerLookup>
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//    }
//}

