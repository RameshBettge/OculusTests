using UnityEngine;

[System.Serializable]
public class InputButton
{
    public readonly KeyCode kC;

    public InputButton(int joystickNum)
    {
        string name = "JoystickButton" + joystickNum;
        //Debug.Log(name);
        kC = (KeyCode)System.Enum.Parse(typeof(KeyCode), name);
        //Debug.Log(kC.ToString());
    }

    public InputButton(KeyCode keyCode)
    {
        kC = keyCode;
    }

    public bool IsPressed { get { return Input.GetKey(kC); } }

    public bool OnDown { get { return Input.GetKeyDown(kC); } }

    public bool OnUp { get { return Input.GetKeyUp(kC); } }
}
