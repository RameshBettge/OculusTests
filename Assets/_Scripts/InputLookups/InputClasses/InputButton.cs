using UnityEngine;

[System.Serializable]
public class InputButton
{
    public readonly KeyCode kC;

    public InputButton(int joystickNum)
    {
        string name = "JoystickButton" + joystickNum;
        
        kC = (KeyCode)System.Enum.Parse(typeof(KeyCode), name);
    }

    public InputButton(KeyCode keyCode)
    {
        kC = keyCode;
    }

    public bool IsPressed { get { return Input.GetKey(kC); } }

    public bool OnDown { get { return Input.GetKeyDown(kC); } }

    public bool OnUp { get { return Input.GetKeyUp(kC); } }
}
