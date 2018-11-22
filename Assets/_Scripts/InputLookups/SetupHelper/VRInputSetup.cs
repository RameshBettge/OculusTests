using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class VRInputSetup : MonoBehaviour
{
    public enum Hand { Right, Left };

    public VRInputLookup vrInputLookup;

    public Hand Controller;


    [HideInInspector]
    public KeyCode lastButtonPressed;
    [HideInInspector]
    public string lastAxisUsed;
    [HideInInspector]
    public bool lastAxisNegative;

    
    public List<KeyCode> KeysUsed;

    private void Awake()
    {
        AddControllerKeys(vrInputLookup.Right);
        AddControllerKeys(vrInputLookup.Left);
    }

    void AddControllerKeys(VRController c)
    {
        AddKey(c.Button1);
        AddKey(c.Button1_Touch);
        AddKey(c.Button2);
        AddKey(c.Button2_Touch);
        AddKey(c.Index_Touch);
        AddKey(c.Thumb_Touch);
        AddKey(c.Thumb_Press);
    }

    void AddKey(InputButton b)
    {
        if (b == null) { return; }
        if(b.kC == KeyCode.None) { return; }

        if (KeysUsed.Contains(b.kC))
        {
            Debug.LogError(b.kC.ToString() + " is used multiple Times!");
        }

        KeysUsed.Add(b.kC);
    }

    void Update()
    {
        for (int i = 1; i < 29; i++)
        {
            string axis = "Axis" + i;
            float v = Input.GetAxis(axis);

            if (v > 0.1f && v < 0.98f)
            {
                lastAxisUsed = "Axis" + i;
                lastAxisNegative = false;
            }
            if (v < -0.1f && v > -0.98f)
            {
                lastAxisUsed = "Axis" + i;
                lastAxisNegative = true;
            }
        }

        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key) && !KeysUsed.Contains(key))
            {
                lastButtonPressed = key;
            }
        }
    }
}

[ExecuteInEditMode]
[CustomEditor(typeof(VRInputSetup))]
public class VRInputSetupInspector : Editor
{
    VRController c
    {
        get
        {
            if (script.Controller == VRInputSetup.Hand.Left)
            {
                return script.vrInputLookup.Left;
            }
            else
            {
                return script.vrInputLookup.Right;
            }
        }
    }

    VRInputSetup script;


    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This script is used to assign button inputs to a VRInputSetup(Scriptable Object). " +
            "Click the button next to a variable to assign the last pressed button/ the last used axis to it. " +
            "Only usable in Play-Mode.", MessageType.Info, true);
        DrawDefaultInspector();

        EditorGUILayout.Space();

        script = (VRInputSetup)target;



        // Buttons
        EditorGUILayout.HelpBox("Be sure to ALWAYS assign 'touched' buttons first!", MessageType.Warning, true);

        if (GUILayout.Button("Button1"))
        {
            if(Unused(script.lastButtonPressed)) c.Button1 = new InputButton(script.lastButtonPressed); //Assign key if it isn't already used
        }
        if (GUILayout.Button("Reset Button1"))
        {
            RemoveKey(c.Button1);
            c.Button1 = null;
        }
        EditorGUILayout.TextField("Assigned to: " + Status(c.Button1));




        EditorGUILayout.HelpBox("An Axis will be recognized best if it is NOT exactly 1 or -1. Be sure to always assign 'touched' buttons first!", MessageType.Warning, true);

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField("Last Button pressed: " + script.lastButtonPressed.ToString());
        EditorGUILayout.TextField("Last Axis used: " + script.lastAxisUsed);
        EditorGUI.EndDisabledGroup();
    }

    bool Unused(KeyCode key)
    {
        bool unused = !script.KeysUsed.Contains(key);
        if (unused) { script.KeysUsed.Add(key); }

        Debug.Log(unused);
        return unused;
    }


    string Status(InputButton b)
    {
        if (b == null) { return "Unset"; }
        else
        {
            return b.kC.ToString();
        }
    }
    void RemoveKey(InputButton b)
    {
        if (script.KeysUsed.Contains(b.kC))
        {
            script.KeysUsed.Remove(b.kC);
        }
    }

}