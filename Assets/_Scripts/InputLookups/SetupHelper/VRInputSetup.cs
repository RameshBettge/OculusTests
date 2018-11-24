using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

// TODO: Automatically call CopyFromLookup and controllers.write to list when switching vrInputLookup. Maybe check in update.
// TODO: Remind that one has to click in game window for input to be recognized.
// TODO: Add reminder to 'apply' setting at the bottom!
// TODO: Create warning in log if a key is pressed, which is assigned to other controller already 
// TODO: Maybe add a script reference which is nothing but a manual and helper for trouble shooting. (similar to how every script is represented on top in inspector)

[ExecuteInEditMode]
public class VRInputSetup : MonoBehaviour
{


    public enum Hand { Right, Left };

    public VRInputLookup vrInputLookup;

    [HideInInspector] public Hand Controller;
    [HideInInspector]
    public VRController c
    {
        get { return Controller == Hand.Left ? vrInputLookup.Left : vrInputLookup.Right; }
    }

    [HideInInspector]
    public int lastButtonPressed;
    [HideInInspector]
    public string lastAxisUsed;
    [HideInInspector]
    public bool lastAxisNegative;

    [HideInInspector]
    public List<string> AxesUsed;
    [HideInInspector]
    public List<int> KeysUsed;


    // saved buttons
    [HideInInspector] public int button1 = -1;
    [HideInInspector] public int button1_Touch = -1;

    [HideInInspector] public int button2 = -1;
    [HideInInspector] public int button2_Touch = -1;

    [HideInInspector] public int index_Touch = -1;

    [HideInInspector] public int thumb_Touch = -1;
    [HideInInspector] public int thumb_Press = -1;

    // saved axes
    [HideInInspector] public int thumbX = -1;
    [HideInInspector] public bool thumbXInverted = false;

    [HideInInspector] public int thumbY = -1;
    [HideInInspector] public bool thumbYInverted = false;

    [HideInInspector] public int index = -1;
    [HideInInspector] public bool indexInverted = false;

    [HideInInspector] public int grab = -1;
    [HideInInspector] public bool grabInverted = false;


    [HideInInspector] public bool Button1Status;
    [HideInInspector] public bool Button1_TouchStatus;

    [HideInInspector] public bool Button2Status;
    [HideInInspector] public bool Button2_TouchStatus;

    [HideInInspector] public bool Index_TouchStatus;

    [HideInInspector] public bool Thumb_TouchStatus;
    [HideInInspector] public bool Thumb_PressStatus;

    [HideInInspector] public bool ThumbXStatus;
    [HideInInspector] public bool ThumbYStatus;

    [HideInInspector] public bool IndexStatus;
    [HideInInspector] public bool GrabStatus;


    private void SetStatuses()
    {
        // Buttons
        Button1_TouchStatus = ButtonIsPressed(button1_Touch) ? true : false;
        Button1Status = ButtonIsPressed(button1) ? true : false;

        Button2_TouchStatus = ButtonIsPressed(button2_Touch) ? true : false;
        Button2Status = ButtonIsPressed(button2) ? true : false;

        Index_TouchStatus = ButtonIsPressed(index_Touch) ? true : false;

        Thumb_TouchStatus = ButtonIsPressed(thumb_Touch) ? true : false;
        Thumb_PressStatus = ButtonIsPressed(thumb_Press) ? true : false;

        // Axes
        ThumbXStatus = AxisIsUsed(thumbX, thumbXInverted) ? true : false;
        ThumbYStatus = AxisIsUsed(thumbY, thumbYInverted) ? true : false;

        IndexStatus = AxisIsUsed(index, indexInverted) ? true : false;
        GrabStatus = AxisIsUsed(grab, grabInverted) ? true : false;
    }

    bool ButtonIsPressed(int num)
    {
        if (num < 0) { return false; }

        if (Input.GetKey(InputButton.JoystickFromInt(num)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool AxisIsUsed(int num, bool inverted)
    {
        float min = 0.5f;

        if (num < 0) { return false; }

        string name = InputAxis.FromInt(num);

        float input = Input.GetAxis(name);

        if (input > min && !inverted) { return true; }
        if (input < -min && inverted) { return true; }

        return false;
    }


    private void Awake()
    {
        KeysUsed = new List<int>();
        AxesUsed = new List<string>();

        CopyFromLookup();

        //AddControllerKeys(vrInputLookup.Right);
        //AddControllerKeys(vrInputLookup.Left);
    }

    public void CopyFromLookup()
    {
        c.WriteToSetup(this);

        vrInputLookup.Right.WriteIntoList(this);
        vrInputLookup.Left.WriteIntoList(this);
    }

    //void AddControllerKeys(VRController c) //may be obsolete because this is done in CopyfromLookup
    //{
    //    AddKeyInt(button1);
    //    AddKeyInt(button1_Touch);
    //    AddKeyInt(button2);
    //    AddKeyInt(button2_Touch);
    //    AddKeyInt(index_Touch);
    //    AddKeyInt(thumb_Touch);
    //    AddKeyInt(thumb_Press);
    //}

    public void AddButton(int num) // TODO: add similar function for axes
    {
        if (num < 0) { return; }

        KeysUsed.Add(num);
    }
    public void AddAxis(int num, bool inverted)
    {
        string axisName = InputAxis.FromIntBool(num, inverted);

        if (axisName != "")
        {
            AxesUsed.Add(axisName);
        }
    }


    // Uses duplicate code from KeyCodeToInt()
    public int AxisToInt(string axisName)
    {
        char[] chars = axisName.ToCharArray();
        char lastC = chars[chars.Length - 1];

        int result = (int)char.GetNumericValue(lastC);

        char secondLastC = chars[chars.Length - 2];
        if (System.Char.IsDigit(secondLastC))
        {
            result += ((int)char.GetNumericValue(secondLastC)) * 10;
        }

        return result;
    }

    public bool KeyCodeToInt(KeyCode kC, out int out_result, bool enableWarning = false)
    {
        string warning = "Key pressed is not a joystick button!";

        out_result = -1;
        char[] chars = kC.ToString().ToCharArray();

        // Check if it is a joystick button
        if (chars[0] != 'J' || chars[1] != 'o')
        {
            if (enableWarning)
            {
                Debug.LogWarning(warning);
            }

            return false;
        }

        // check if there is a number to be parsed.
        char lastC = chars[chars.Length - 1];
        if (!System.Char.IsDigit(lastC))
        {
            if (enableWarning)
            {
                Debug.LogWarning(warning);
            }
            return false;
        }

        out_result = (int)char.GetNumericValue(lastC);
        // check if the number at the end has 2 digits.
        char secondLastC = chars[chars.Length - 2];
        if (System.Char.IsDigit(secondLastC))
        {
            out_result += ((int)char.GetNumericValue(secondLastC)) * 10;
        }

        return true;
    }

    void Update()
    {
        SetStatuses();

        for (int i = 1; i < 29; i++)
        {
            string axis = "Axis" + i;
            float v = Input.GetAxis(axis);

            if (v > 0.5f && v < 0.98f)
            {
                lastAxisUsed = InputAxis.FromIntBool(i, false);
                lastAxisNegative = false;
            }
            if (v < -0.5f && v > -0.98f)
            {
                lastAxisUsed = InputAxis.FromIntBool(i, true);
                lastAxisNegative = true;
            }
        }

        //foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        //{
        //    int num;
        //    if(!KeyCodeToInt(key, out num)) { return; }
        //    if (Input.GetKeyDown(key) && !KeysUsed.Contains(num))
        //    {
        //        lastButtonPressed = num;
        //    }
        //}
        for (int i = 0; i < 20; i++) // Checks only the main joystick buttons
        {
            string name = "JoystickButton" + i;

            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), name);
            if (Input.GetKeyDown(key) && !KeysUsed.Contains(i))
            {

                lastButtonPressed = i;
            }
        }
    }


}

