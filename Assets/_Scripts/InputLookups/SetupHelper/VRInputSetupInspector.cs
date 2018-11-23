using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(VRInputSetup))]
public class VRInputSetupInspector : Editor
{
    VRController c
    {
        get
        {
            return script.c;
        }
    }

    int LastButton { get { return script.lastButtonPressed; } }

    VRInputSetup script;


    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This script is used to assign button inputs to a VRInputSetup(Scriptable Object). " +
            "Click the button next to a variable to assign the last pressed button/ the last used axis to it. " +
            "Only usable in Play-Mode.", MessageType.Info, true);
        DrawDefaultInspector();

        EditorGUILayout.Space();

        // Choose controller for setup
        if (GUILayout.Button("Switch Controller to SetUp"))
        {
            if (script.Controller == VRInputSetup.Hand.Left) { script.Controller = VRInputSetup.Hand.Right; }
            else if (script.Controller == VRInputSetup.Hand.Right) { script.Controller = VRInputSetup.Hand.Left; }

            Debug.LogWarning("Switched to " + HandStatus() + " controller. If you haven't applied your settings, please switch back and do so before moving on!");
        }
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField(HandStatus());
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        script = (VRInputSetup)base.target;

        // Buttons
        #region Buttons

        EditorGUILayout.LabelField("Buttons", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Be sure to ALWAYS assign 'touched' buttons first!", MessageType.Warning, true);


        EditorGUILayout.LabelField("Button1_Touch", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.button1_Touch);
                script.button1_Touch = LastButton; //Assign key if it isn't already used
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.button1_Touch);
            script.button1_Touch = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField(JoystickStatus(script.button1_Touch));
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Button2_Touch", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.button2_Touch);
                script.button2_Touch = LastButton; //Assign key if it isn't already used
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.button2_Touch);
            script.button2_Touch = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField(JoystickStatus(script.button2_Touch));
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Index_Touch", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.index_Touch);
                script.index_Touch = LastButton; //Assign key if it isn't already used
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.index_Touch);
            script.index_Touch = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField(JoystickStatus(script.index_Touch));
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Thumb_Touch", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.thumb_Touch);
                script.thumb_Touch = LastButton; //Assign key if it isn't already used
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.thumb_Touch);
            script.thumb_Touch = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField(JoystickStatus(script.thumb_Touch));
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Button1", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.button1);
                script.button1 = LastButton; //Assign key if it isn't already used
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.button1);
            script.button1 = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField(JoystickStatus(script.button1));
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Button2", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.button2);
                script.button2 = LastButton; //Assign key if it isn't already used
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.button2);
            script.button2 = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField(JoystickStatus(script.button2));
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Thumb_Press", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.thumb_Press);
                script.thumb_Press = LastButton; //Assign key if it isn't already used
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.thumb_Press);
            script.thumb_Press = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField(JoystickStatus(script.thumb_Press));
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField("Last Button pressed: " + script.lastButtonPressed.ToString());
        EditorGUI.EndDisabledGroup();

        #endregion

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        #region Axes

        EditorGUILayout.LabelField("Axes", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("An Axis will be recognized best if it is NOT exactly 1 or -1. If axis is a joystick always move it either right or up!", MessageType.Warning, true);

        EditorGUILayout.LabelField("Thumb X", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (AxisIsUnused(script.lastAxisUsed))
            {
                script.thumbX = script.AxisToInt(script.lastAxisUsed); //Assign axis if it isn't already used
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.thumbX);
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField(AxisStatus(script.thumbX));
        EditorGUI.EndDisabledGroup();

        #endregion

        EditorGUILayout.Space();
        EditorGUILayout.Space();


        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField("Last Axis used: " + script.lastAxisUsed);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        if (GUILayout.Button("Apply", GUILayout.Height(30)))
        {
            if (script.vrInputLookup == null)
            {
                Debug.LogError("No Lookup assigned!");
                return;
            }

            VRController controller = script.Controller == VRInputSetup.Hand.Left ? script.vrInputLookup.Left : script.vrInputLookup.Right;
            controller.CopyFromSetup(script);
            script.vrInputLookup.UpdateLastApplied();

            string hand = HandStatus();
            Debug.LogWarning(hand + " controller's settings applied. Please remember to set up the other controller as well!");
        }

    }

    string HandStatus()
    {
        if (script == null) { return "script null"; }
        return script.Controller == VRInputSetup.Hand.Left ? "Left" : "Right";
    }


    bool AxisIsUnused(string name)
    {
        bool unused = !script.AxesUsed.Contains(name);
        if (unused) { script.AxesUsed.Add(name); }
        else
        {
            Debug.LogError(name + " is already used!");
        }

        return unused;
    }

    bool ButtonIsUnused(int keyNum)
    {
        bool unused = !script.KeysUsed.Contains(keyNum);
        if (unused) { script.KeysUsed.Add(keyNum); }
        else
        {
            Debug.LogError("JoystickButton" + keyNum + " is already used!");
        }

        return unused;
    }


    string JoystickStatus(int num)
    {
        if (num == -1) { return "Unassigned!"; }
        else
        {
            return "JoystickButton" + num;
        }
    }
    string AxisStatus(int num)
    {
        if (num == -1) { return "Unassigned!"; }
        else
        {
            return "Axis" + num;
        }
    }

    void RemoveKey(int num)
    {
        if (script.KeysUsed.Contains(num))
        {
            script.KeysUsed.Remove(num);
            Debug.Log(num + " was removed from the list.");

        }
        else
        {
            Debug.Log(num + " couldn't be removed - it isn't in the list.");
        }
    }

}

