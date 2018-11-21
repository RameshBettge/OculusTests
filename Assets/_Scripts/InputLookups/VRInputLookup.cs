using UnityEngine;

// TODO: Make all variables private and create functions which return the input as bool or floats.

[CreateAssetMenu(fileName = "VRInputLookup", menuName = "Lookups/VRInput", order = 1)]
public class VRInputLookup : ScriptableObject
{
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


    // comfort functions

    public Vector2 GetLThumb()
    {
        float x = Input.GetAxis(lThumbX);
        float y = Input.GetAxis(lThumbY);

        return new Vector2(x, y);
    }

    public Vector2 GetRThumb()
    {
        float x = Input.GetAxis(rThumbX);
        float y = Input.GetAxis(rThumbY);

        return new Vector2(x, y);
    }
}