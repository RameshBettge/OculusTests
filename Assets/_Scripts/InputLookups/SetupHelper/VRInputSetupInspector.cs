﻿using System.Collections;
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

        EditorGUILayout.LabelField("Axes", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("An Axis will be recognized best if it is NOT exactly 1 or -1. If axis is a joystick always move it either right or up!", MessageType.Warning, true);

        AxisParameters info = DrawAxisInfo("ThumbX", script.thumbX, script.thumbXInverted, script.ThumbXStatus);
        script.thumbX = info.num;
        script.thumbXInverted = info.inverted;

        info = DrawAxisInfo("ThumbY", script.thumbY, script.thumbYInverted, script.ThumbYStatus);
        script.thumbY = info.num;
        script.thumbYInverted = info.inverted;

        info = DrawAxisInfo("Index", script.index, script.indexInverted, script.IndexStatus);
        script.index = info.num;
        script.indexInverted = info.inverted;

        info = DrawAxisInfo("Grab", script.grab, script.grabInverted, script.GrabStatus);
        script.grab = info.num;
        script.grabInverted = info.inverted;

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

    AxisParameters DrawAxisInfo(string title, int axisNum, bool inverted, bool status)
    {
        AxisParameters output = new AxisParameters(axisNum, inverted);
        

        EditorGUILayout.LabelField(title, EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (AxisIsUnused(script.lastAxisUsed))
            {
                Debug.Log("Axis is re-assigned");

                RemoveAxis(axisNum, inverted);
                output.num = script.AxisToInt(script.lastAxisUsed);
                output.inverted = script.lastAxisNegative;
                script.AddAxis(output.num, output.inverted);
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveAxis(axisNum, inverted);
            output.num = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (status)
        {
            EditorGUILayout.TextField(AxisStatus(axisNum, inverted), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(AxisStatus(axisNum, inverted), keyStyle);
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
        bool unused = true;

        bool originalIsInverted;

        string[] parts = name.Split();

        string invertedName;
        string normalName;

        if(parts.Length > 1) // if there is '(inverted)' before the axis name
        {
            invertedName = name;
            normalName = parts[1];

            originalIsInverted = true;
        }
        else
        {
            normalName = name;
            invertedName = "(inverted) " + name;

            originalIsInverted = false;
        }

        if (script.AxesUsed.Contains(normalName))
        {
            if (originalIsInverted)
            {
                unused = false;
                Debug.LogError(name + " is already assigned in inverted variant.");
            }
            else
            {
                unused = false;
                Debug.LogError(name + " is already assigned.");
            }
        }
        else if (script.AxesUsed.Contains(invertedName))
        {
            if (originalIsInverted)
            {
                unused = false;
                Debug.LogError(name + " is already assigned.");
            }
            else
            {
                unused = false;
                Debug.LogError(name + " is already assigned in un-inverted variant.");
            }
        }

        Debug.Log(name + " is unused = " + unused);

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
        if(num < 0) { return; }
        string name = InputAxis.FromIntBool(num, inverted);

        if (script.AxesUsed.Contains(name))
        {
            script.AxesUsed.Remove(name);
        }
    }
}

