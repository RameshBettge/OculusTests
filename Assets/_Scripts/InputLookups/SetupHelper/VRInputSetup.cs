using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// TODO: Make sure the 'Last Applied' of the Lookup is updated.
// TODO: Create help-window reminding about apllying
// TODO: Call Copy From Lookup when switching hands (RISK: resetting options. Maybe create seperate button)
// TODO: Automatically call CopyFromLookup and controllers.write to list when switching vrinput lookup. Maybe check in update.

[ExecuteInEditMode]
public class VRInputSetup : MonoBehaviour
{
    public enum Hand { Right, Left };

    public VRInputLookup vrInputLookup;

    [HideInInspector] public Hand Controller;
    [HideInInspector] public VRController c
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
        if(num < 0) { return; }

        KeysUsed.Add(num);
    }
    public void AddAxis(int num, bool inverted)
    {
        string axisName = InputAxis.FromIntBool(num, inverted);

        if(axisName != "")
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

