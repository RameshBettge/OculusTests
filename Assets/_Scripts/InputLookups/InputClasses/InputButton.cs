using UnityEngine;

[System.Serializable]
public class InputButton
{
    [SerializeField]
    public KeyCode button;

    public InputButton(int joystickNum)
    {
        button = JoystickFromInt(joystickNum);
    }

    public InputButton(KeyCode keyCode)
    {
        button = keyCode;
    }

    public bool IsPressed { get { return Input.GetKey(button); } }

    public bool OnDown { get { return Input.GetKeyDown(button); } }

    public bool OnUp { get { return Input.GetKeyUp(button); } }

    public static KeyCode JoystickFromInt(int num)
    {
        if(num < 0)
        {
            Debug.Log("JoystickButton" + num + " doesn't exist!");
        }

        string name = "JoystickButton" + num;

        return (KeyCode)System.Enum.Parse(typeof(KeyCode), name);
    }
}
