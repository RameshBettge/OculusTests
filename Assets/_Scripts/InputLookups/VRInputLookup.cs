using UnityEngine;

// For every supported VR Headset, create one  class, which inherits from this.
// Assign every button.
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


    public string rJoyH = "Horizontal";
    public string rJoyV = "Horizontal";
    [Space(10)]

    [Header("Left Controller")]
    public KeyCode lButton1 = KeyCode.Joystick1Button0;
    public KeyCode lButton1_Touch = KeyCode.Joystick1Button0;
    [Space(10)]

    public KeyCode lButton2 = KeyCode.Joystick1Button0;
    public KeyCode lButton2_Touch = KeyCode.Joystick1Button0;
    [Space(10)]

    public string lJoyH = "Horizontal";
    public string lJoyV = "Horizontal";
}