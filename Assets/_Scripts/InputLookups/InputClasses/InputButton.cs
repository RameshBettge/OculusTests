using UnityEngine;

[System.Serializable]
public class InputButton
{
    [SerializeField]
    public KeyCode button;

    public InputButton(int joystickNum)
    {
        string name = "JoystickButton" + joystickNum;
        
        button = (KeyCode)System.Enum.Parse(typeof(KeyCode), name);
    }

    public InputButton(KeyCode keyCode)
    {
        button = keyCode;
    }

    public bool IsPressed { get { return Input.GetKey(button); } }

    public bool OnDown { get { return Input.GetKeyDown(button); } }

    public bool OnUp { get { return Input.GetKeyUp(button); } }
}
