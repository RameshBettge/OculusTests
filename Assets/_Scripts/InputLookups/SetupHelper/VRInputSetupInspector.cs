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

        // The next 6 lines of code have caused a crash before. If it happens again, delete them and call DrawDefaultInspector() instead.
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((VRInputSetup)target), typeof(VRInputSetup), false);
        GUI.enabled = true;

        SerializedObject so = base.serializedObject;
        EditorGUILayout.PropertyField(so.FindProperty("vrInputLookup"), true);
        so.ApplyModifiedProperties();

        EditorGUILayout.HelpBox("This script is used to assign button inputs to a VRInputSetup(Scriptable Object).", MessageType.Info, true);

        Color bColor = GUI.backgroundColor;
        GUI.backgroundColor = new Color(0.5f, 1, 0.5f, 1);
        if (GUILayout.Button("Display Manual", GUILayout.Height(30)))
        {
            script.DisplayManual();
        }
        GUI.backgroundColor = bColor;



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

        GUI.backgroundColor = Color.yellow;
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
        GUI.backgroundColor = bColor;


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

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("Please remember to apply your settings!", MessageType.Warning, true);
    }

    // ----------------------------- Methods


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
            if (LastAxisIsUnused(InputAxis.FromIntBool(axisNum, inverted)))
            {
                RemoveAxis(axisNum, inverted);
                output.num = script.AxisToInt(script.lastAxisUsed);
                output.inverted = script.lastAxisNegative;
                script.AddAxis(output.num, output.inverted, script.AxesUsedCurrent);
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


    bool LastAxisIsUnused(string current)
    {
        string lastAxisUsed = script.lastAxisUsed;

        bool unused = true;

        bool originalIsInverted;

        string[] parts = lastAxisUsed.Split();

        string invertedName;
        string normalName;

        string oppositeName;

        if(parts.Length > 1) // if there is '(inverted)' before the axis name
        {
            invertedName = lastAxisUsed;
            normalName = parts[1];

            oppositeName = normalName;

            originalIsInverted = true;
        }
        else
        {
            normalName = lastAxisUsed;
            invertedName = "(inverted) " + lastAxisUsed;

            oppositeName = invertedName;

            originalIsInverted = false;
        }

        if(current == oppositeName) { return true; } // assigning the same axis but (un)inverted is possible.

        // TODO: Avoid having 12 if statements for creating specific error messages.

        // Check current controller
        if (script.AxesUsedCurrent.Contains(normalName))
        {
            if (originalIsInverted)
            {
                unused = false;
                Debug.LogError(lastAxisUsed + " is already assigned to current Controller in inverted variant.");
            }
            else
            {
                unused = false;
                Debug.LogError(lastAxisUsed + " is already assigned to current Controller.");
            }
        }
        else if (script.AxesUsedCurrent.Contains(invertedName))
        {
            if (originalIsInverted)
            {
                unused = false;
                Debug.LogError(lastAxisUsed + " is already assigned to current Controller.");
            }
            else
            {
                unused = false;
                Debug.LogError(lastAxisUsed + " is already assigned to current Controller in un-inverted variant.");
            }
        }

        // Check other controller
        if (script.AxesUsedOther.Contains(normalName))
        {
            if (originalIsInverted)
            {
                unused = false;
                Debug.LogError(lastAxisUsed + " is already assigned to other Controller in inverted variant.");
            }
            else
            {
                unused = false;
                Debug.LogError(lastAxisUsed + " is already assigned to other Controller.");
            }
        }
        else if (script.AxesUsedCurrent.Contains(invertedName))
        {
            if (originalIsInverted)
            {
                unused = false;
                Debug.LogError(lastAxisUsed + " is already assigned to other Controller.");
            }
            else
            {
                unused = false;
                Debug.LogError(lastAxisUsed + " is already assigned to other Controller in un-inverted variant.");
            }
        }


        if (unused)
        {

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
        if(num < 0) { return; }
        string name = InputAxis.FromIntBool(num, inverted);

        if (script.AxesUsedCurrent.Contains(name))
        {
            script.AxesUsedCurrent.Remove(name);
        }
        else if (script.AxesUsedOther.Contains(name))
        {
            script.AxesUsedOther.Remove(name);
        }
    }
}

