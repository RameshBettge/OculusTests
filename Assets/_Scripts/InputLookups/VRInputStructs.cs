using UnityEngine;
using UnityEditor;

[System.Serializable]
[HideInInspector]
public class InputButton
{
    public KeyCode kC;

    public InputButton(int joystickNum)
    {
        string name = "JoystickButton" + joystickNum;
        //Debug.Log(name);
        kC = (KeyCode)System.Enum.Parse(typeof(KeyCode), name);
        //Debug.Log(kC.ToString());

    }

    public bool IsPressed { get { return Input.GetKey(kC); } }

    public bool OnDown { get { return Input.GetKeyDown(kC); } }

    public bool OnUp { get { return Input.GetKeyUp(kC); } }
}



public class InputAxis
{
    string name;

    public InputAxis(string axisName)
    {
        name = axisName;
    }

    public float Value { get { return Input.GetAxis(name); } }
    public float RawValue { get { return Input.GetAxisRaw(name); } }
}

public class InputAxisPair
{
    InputAxis x;
    InputAxis y;

    public InputAxisPair(InputAxis x, InputAxis y)
    {
        this.x = x;
        this.y = y;
    }

    public float X { get { return x.Value; } }
    public float RawX { get { return x.RawValue; } }

    public float Y { get { return y.Value; } }
    public float RawY { get { return y.RawValue; } }

    public Vector2 Vec2 { get { return new Vector2(x.Value, y.Value); } }
    public Vector2 RawVec2 { get { return new Vector2(x.RawValue, y.RawValue); } }
}