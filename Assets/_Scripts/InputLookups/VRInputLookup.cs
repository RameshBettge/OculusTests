using UnityEngine;
using UnityEditor;

// TODO: Make all variables private and create functions which return the input as bool or floats.

[CreateAssetMenu(fileName = "VRInputLookup", menuName = "Lookups/VRInput", order = 1)]
public class VRInputLookup : ScriptableObject
{
    public VRController Right;
    public VRController Left;

    [Header("Right Controller")]
    public KeyCode rButton1 = KeyCode.Joystick1Button0;
    public KeyCode rButton1_Touch = KeyCode.Joystick1Button0;
    [Space(10)]

    public KeyCode rButton2 = KeyCode.Joystick1Button0;
    public KeyCode rButton2_Touch = KeyCode.Joystick1Button0;
    [Space(10)]

    public string rThumbX = "Horizontal";
    public string rThumbY = "Horizontal";
    public KeyCode rThumb_Touch = KeyCode.Joystick1Button0;
    public KeyCode rThumb_Press = KeyCode.Joystick1Button0;
    [Space(10)]


    public string rIndex = "Horizontal";
    //public string rIndex_NearTouch = "Horizontal";
    public KeyCode rIndex_Touch = KeyCode.Joystick1Button0;
    [Space(5)]
    public string rGrab = "Horizontal";


    [Header("Left Controller")]

    public KeyCode lButton1 = KeyCode.Joystick1Button0;
    public KeyCode lButton1_Touch = KeyCode.Joystick1Button0;
    [Space(10)]

    public KeyCode lButton2 = KeyCode.Joystick1Button0;
    public KeyCode lButton2_Touch = KeyCode.Joystick1Button0;
    [Space(10)]

    public string lThumbX = "Horizontal";
    public string lThumbY = "Horizontal";
    public KeyCode lThumb_Touch = KeyCode.Joystick1Button0;
    public KeyCode lThumb_Press = KeyCode.Joystick1Button0;
    [Space(10)]

    public string lIndex = "Horizontal";
    //public string lIndex_NearTouch = "Horizontal";
    public KeyCode lIndex_Touch = KeyCode.Joystick1Button0;
    [Space(5)]
    public string lGrab = "Horizontal";
    [Space(20)]

    [Tooltip("MM/DD/YY")]
    public string LastApplied = "WARNING: UNAPPLIED!";
}


[CustomEditor(typeof(VRInputLookup))]
public class VRControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        VRInputLookup script = (VRInputLookup)target;

        if (GUILayout.Button("Apply Settings"))
        {
            script.Right.Apply();
            script.Left.Apply();

            script.LastApplied = System.DateTime.Now.ToShortDateString() + " - " + System.DateTime.Now.ToShortTimeString();
        }
    }
}