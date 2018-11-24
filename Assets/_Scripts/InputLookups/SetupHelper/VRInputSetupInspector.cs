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





    int LastButton { get { return script.lastButtonPressed; } }

    VRInputSetup script;

    string assigned = "Assigned to: ";

    public override void OnInspectorGUI()
    {
        GUIStyle keyStyle = new GUIStyle();
        GUIStyle selectedKeyStyle = new GUIStyle();

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
                script.button1_Touch = LastButton;
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.button1_Touch);
            script.button1_Touch = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (script.Button1_TouchStatus)
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.button1_Touch), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.button1_Touch), keyStyle);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Button2_Touch", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.button2_Touch);
                script.button2_Touch = LastButton;
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.button2_Touch);
            script.button2_Touch = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (script.Button2_TouchStatus)
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.button2_Touch), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.button2_Touch), keyStyle);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Index_Touch", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.index_Touch);
                script.index_Touch = LastButton;
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.index_Touch);
            script.index_Touch = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (script.Index_TouchStatus)
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.index_Touch), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.index_Touch), keyStyle);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Thumb_Touch", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.thumb_Touch);
                script.thumb_Touch = LastButton;
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.thumb_Touch);
            script.thumb_Touch = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (script.Thumb_TouchStatus)
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.thumb_Touch), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.thumb_Touch), keyStyle);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Button1", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.button1);
                script.button1 = LastButton;
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.button1);
            script.button1 = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (script.Button1Status)
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.button1), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.button1), keyStyle);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Button2", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.button2);
                script.button2 = LastButton;
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.button2);
            script.button2 = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (script.Button2Status)
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.button2), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.button2), keyStyle);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Thumb_Press", EditorStyles.largeLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
        {
            if (ButtonIsUnused(script.lastButtonPressed))
            {
                RemoveKey(script.thumb_Press);
                script.thumb_Press = LastButton;
            }
        }
        if (GUILayout.Button("Reset"))
        {
            RemoveKey(script.thumb_Press);
            script.thumb_Press = -1;
        }
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        if (script.Thumb_PressStatus)
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.thumb_Press), selectedKeyStyle);
        }
        else
        {
            EditorGUILayout.TextField(assigned + JoystickStatus(script.thumb_Press), keyStyle);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.LabelField("Last Button Pressed: ", EditorStyles.boldLabel);
        EditorGUILayout.TextField(script.lastButtonPressed.ToString());
        EditorGUI.EndDisabledGroup();

        #endregion

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

