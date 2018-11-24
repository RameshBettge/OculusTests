using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// TODO: Make last applied un-changeable

[ExecuteInEditMode]
[CustomEditor(typeof(VRInputSetup))]
public class VRInputSetupInspector : Editor
{
    VRController c
    {
        get
        {
            return script.currentController;
        }
    }

    GUIStyle keyStyle = new GUIStyle();
    GUIStyle selectedKeyStyle = new GUIStyle();



    int LastButton { get { return script.lastButtonPressed; } }

    VRInputSetup script;

    string assigned = "Assigned to: ";

    public override void OnInspectorGUI()
    {
        

        selectedKeyStyle.fontSize = 12;

        selectedKeyStyle.fontSize = 12;
        selectedKeyStyle.normal.textColor = Color.red;
        selectedKeyStyle.fontStyle = FontStyle.Bold;


        EditorGUILayout.HelpBox("This script is used to assign button inputs to a VRInputSetup(Scriptable Object). " +
            "Click the button next to a variable to assign the last pressed button/ the last used axis to it. " +
            "Only usable in Play-Mode.", MessageType.Info, true);
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Choose controller for setup
        EditorGUILayout.HelpBox("WARNING: Settings are void if they are not applied. Please apply first and only then switch Controllers.", MessageType.Warning, true);
        if (GUILayout.Button("Switch Controller to SetUp", GUILayout.Height(30)))
        {
            if (script.Controller == VRInputSetup.Hand.Left) { script.Controller = VRInputSetup.Hand.Right; }
            else if (script.Controller == VRInputSetup.Hand.Right) { script.Controller = VRInputSetup.Hand.Left; }
            script.CopyFromLookup();


            Debug.LogWarning("Switched to " + HandStatus() + " controller.");
        }

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField("Current Controller: " + HandStatus());
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();
        EditorGUILayout.Space();


        if (GUILayout.Button("Apply", GUILayout.Height(50)))
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

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        script = (VRInputSetup)base.target;

        // ------------------------ Buttons

        script.button1_Touch = DrawButtonInfo("Button1_Touch", script.button1_Touch, script.Button1_TouchStatus);
        script.button2_Touch = DrawButtonInfo("Button2_Touch", script.button2_Touch, script.Button2_TouchStatus);

        script.index_Touch = DrawButtonInfo("Index_Touch", script.index_Touch, script.Index_TouchStatus);
        script.thumb_Touch = DrawButtonInfo("Thumb_Touch", script.thumb_Touch, script.Thumb_TouchStatus);

        script.button1 = DrawButtonInfo("Button1", script.button1, script.Button1Status);
        script.button2 = DrawButtonInfo("Button2", script.button2, script.Button2Status);

        script.thumb_Press = DrawButtonInfo("Thumb_Press", script.thumb_Press, script.Thumb_PressStatus);

        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.LabelField("Last Button Pressed: ", EditorStyles.boldLabel);
        EditorGUILayout.TextField(script.lastButtonPressed.ToString());
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //  ------------------------------- Axes
        #region Axes

        EditorGUILayout.LabelField("Axes", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("An Axis will be recognized best if it is NOT exactly 1 or -1. If axis is a joystick always move it either right or up!", MessageType.Warning, true);

        EditorGUILayout.LabelField("Thumb X", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (AxisIsUnused(script.lastAxisUsed))
            {
                RemoveAxis(script.thumbX, script.thumbXInverted);
                script.thumbX = script.AxisToInt(script.lastAxisUsed);
                script.thumbXInverted = script.lastAxisNegative;
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveAxis(script.thumbX, script.thumbXInverted);
            script.thumbX = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (script.ThumbXStatus)
        {
            EditorGUILayout.TextField(AxisStatus(script.thumbX, script.thumbXInverted), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(AxisStatus(script.thumbX, script.thumbXInverted), keyStyle);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Thumb Y", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (AxisIsUnused(script.lastAxisUsed))
            {
                RemoveAxis(script.thumbY, script.thumbYInverted);
                script.thumbY = script.AxisToInt(script.lastAxisUsed);
                script.thumbYInverted = script.lastAxisNegative;

            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveAxis(script.thumbY, script.thumbYInverted);
            script.thumbY = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (script.ThumbYStatus)
        {
            EditorGUILayout.TextField(AxisStatus(script.thumbY, script.thumbYInverted), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(AxisStatus(script.thumbY, script.thumbYInverted), keyStyle);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Index", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (AxisIsUnused(script.lastAxisUsed))
            {
                RemoveAxis(script.index, script.indexInverted);
                script.index = script.AxisToInt(script.lastAxisUsed);
                script.indexInverted = script.lastAxisNegative;

            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveAxis(script.index, script.indexInverted);
            script.index = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (script.IndexStatus)
        {
            EditorGUILayout.TextField(AxisStatus(script.index, script.indexInverted), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(AxisStatus(script.index, script.indexInverted), keyStyle);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Grab", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (AxisIsUnused(script.lastAxisUsed))
            {
                RemoveAxis(script.grab, script.grabInverted);
                script.grab = script.AxisToInt(script.lastAxisUsed);
                script.grabInverted = script.lastAxisNegative;
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveAxis(script.grab, script.grabInverted);
            script.grab = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (script.GrabStatus)
        {
            EditorGUILayout.TextField(AxisStatus(script.grab, script.grabInverted), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(AxisStatus(script.grab, script.grabInverted), keyStyle);
        }
        EditorGUI.EndDisabledGroup();


        #endregion

        EditorGUILayout.Space();
        EditorGUILayout.Space();


        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.LabelField("Last Axis used: ", EditorStyles.boldLabel);
        EditorGUILayout.TextField(script.lastAxisUsed);
        EditorGUI.EndDisabledGroup();




    }

    int DrawButtonInfo(string title, int currentKey, bool status)
    {
        int output = currentKey; // return input

        EditorGUILayout.LabelField(title, EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(currentKey);
                output = script.lastButtonPressed;
                script.AddButton(output, script.KeysUsedCurrent);

            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(currentKey);
            output = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (status)
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(currentKey), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(currentKey), keyStyle);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        return output;
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
        if (script.KeysUsedCurrent.Contains(keyNum))
        {
            Debug.LogError("JoystickButton" + keyNum + " is already used by this Controller!");
            return false;
        }
        if (script.KeysUsedOther.Contains(keyNum))
        {
            Debug.LogError("JoystickButton" + keyNum + " is already used by the other Controller!");
            return false;
        }

        return true;
    }


    string JoystickStatus(int num)
    {
        if (num == -1) { return "Unassigned!"; }
        else
        {
            return "JoystickButton" + num;
        }
    }
    string AxisStatus(int num, bool inverted)
    {
        if (num == -1) { return "Unassigned!"; }
        else
        {
            return InputAxis.FromIntBool(num, inverted);
        }
    }

    void RemoveKey(int num)
    {
        if (script.KeysUsedCurrent.Contains(num))
        {
            script.KeysUsedCurrent.Remove(num);
        }
        else if (script.KeysUsedOther.Contains(num))
        {
            script.KeysUsedOther.Remove(num);
        }
    }

    void RemoveAxis(int num, bool inverted)
    {
        string name = InputAxis.FromIntBool(num, inverted);

        if (script.AxesUsed.Contains(name))
        {
            script.AxesUsed.Remove(name);
        }
    }
}

